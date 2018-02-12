/***
 * 
 *    Title: 保存されたデータ形式のバージョン。 保存されたデータフォーマットが実際のデータフォーマットと等しいかどうかをチェックする
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

[Serializable]
public class DataVersion
{
    public int major;
    public int minor;
}

/// <summary>
/// 保存されたゲーム進行データのフォーマット
/// </summary>
[Serializable]
public class GameProgressData
{
    public System.DateTime saveTime;						// 時間を節約
    public string lastCompetedLevel;                        // 最後に完了するレベルの名前
    public List<string> openedLevels = new List<string>();	// 再生可能なレベルのリスト
}

/// <summary>
/// ファイルからデータを保存して読み込みます
/// </summary>
public class DataManager : MonoBehaviour
{
    private Evaluation.Direction direction;
    private Evaluation.Room room;

    //(運気の)ノルマ変数
    //[0] = 仕事運，[1] = 人気運, [2] = 健康運, [3] = 金運, [4] = 恋愛運
    private int[] norma_luck_ = new int[5];

    //アドバイスモード(0 = 仕事運重視，1 = 人気運重視，2 = 健康運重視，3 = 金運重視，4 = 恋愛運重視, 5 = デフォルト(ノルマ重視))
    private int advaice_mode_;

    private List<string> comment_;

    public Evaluation.Direction read_direction()
    {
        return direction;
    }

    public void set_direction(Evaluation.Direction dir)
    {
        direction = dir;
    }

    public Evaluation.Room read_room()
    {
        return room;
    }

    public void set_room(Evaluation.Room ro)
    {
        room = ro;
    }

    public int[] read_norma_luck()
    {
        return norma_luck_;
    }

    public void set_norma_luck(int[] norma_luck)
    {
        norma_luck_ = norma_luck;
    }

    public int read_advaice_mode()
    {
        return advaice_mode_;
    }

    public void set_advaice_mode(int advaice_mode)
    {
        advaice_mode_ = advaice_mode;
    }

    public List<string> read_comment()
    {
        return comment_;
    }

    public void set_comment(List<string> com)
    {
        comment_ = com;
    }

    // ゲーム進行状況データコンテナ
    public GameProgressData progress = new GameProgressData();
    // シングルトン
    public static DataManager instance;

    // データバージョンのあるファイルの名前
    private string dataVersionFile = "/DataVersion.dat";
    // ゲーム進行データを含むファイルの名前
    private string gameProgressFile = "/GameProgress.dat";
    // データバージョンコンテナ
    private DataVersion dataVersion = new DataVersion();
    // デフォルトのゲーム進行状況データコンテナ
    private GameProgressData gameProgressDefaultData = new GameProgressData();

    /// <summary>
    /// このインスタンスを目覚めさせる
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            // データ形式のバージョン
            dataVersion.major = 1;
            dataVersion.minor = 0;

            // Defaltゲーム進行データ
            progress.saveTime = gameProgressDefaultData.saveTime = DateTime.MinValue;
            progress.lastCompetedLevel = gameProgressDefaultData.lastCompetedLevel = "";

            instance = this;
            DontDestroyOnLoad(gameObject);

            //DeleteGameProgress(); // デバッグ用です。 保存されたゲームの進行を削除するには、この行のコメントを外してください
            //Debug.Log("保存されたゲームの進行状況が削除");

            UpdateDataVersion();
            LoadGameProgress();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    //private enum scene { Title, Help, Select, Game, Result };
    private bool[] current_scene = new bool[5];

    private void Start()
    {
        gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Title_BGM");
        gameObject.GetComponent<AudioSource>().Play();

        for (int i = 0; i < current_scene.Length; i++)
        {
            if (i == 0)
            {
                current_scene[i] = true;
            }
            else
            {
                current_scene[i] = false;
            }

        }

    }

    private void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Title"))
        {
            if (current_scene[0] == true)
            {
                gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Title_BGM");
                gameObject.GetComponent<AudioSource>().Play();

                current_scene[0] = false;

                current_scene[1] = true;
                current_scene[2] = true;
            }
    
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Help"))
        {
            if (current_scene[1] == true)
            {
                gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Help_BGM");
                gameObject.GetComponent<AudioSource>().Play();

                current_scene[1] = false;

                current_scene[0] = true;
            }
           
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Select"))
        {
            if (current_scene[2] == true)
            {
                gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Select_BGM");
                gameObject.GetComponent<AudioSource>().Play();

                current_scene[2] = false;

                current_scene[0] = true;
                current_scene[3] = true;
            }
            
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
        {
            if (current_scene[3] == true)
            {
                gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Game_BGM");
                gameObject.GetComponent<AudioSource>().Play();

                current_scene[3] = false;

                current_scene[4] = true;
            }
           
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Result"))
        {
            if (current_scene[4] == true)
            {
                gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Result_BGM");
                gameObject.GetComponent<AudioSource>().Play();

                current_scene[4] = false;

                current_scene[0] = true;
                current_scene[2] = true;
                current_scene[3] = true;
            }
            
        }
    }

    /// <summary>
    /// データ形式のバージョンを更新します
    /// </summary>
    private void UpdateDataVersion()
    {
        if (File.Exists(Application.persistentDataPath + dataVersionFile) == true)
        {
            BinaryFormatter bfOpen = new BinaryFormatter();
            FileStream fileToOpen = File.Open(Application.persistentDataPath + dataVersionFile, FileMode.Open);
            DataVersion version = (DataVersion)bfOpen.Deserialize(fileToOpen);
            fileToOpen.Close();

            switch (version.major)
            {
                case 1:
                    // 保存されたデータのバージョンは1.xです
                    //必要に応じてデータを変換するハンドラ
                    break;
            }
        }
        BinaryFormatter bfCreate = new BinaryFormatter();
        FileStream fileToCreate = File.Create(Application.persistentDataPath + dataVersionFile);
        bfCreate.Serialize(fileToCreate, dataVersion);
        fileToCreate.Close();
    }

    /// <summary>
    /// 保存されたゲームデータを含むファイルを削除します。 デバッグ専用
    /// </summary>
    private void DeleteGameProgress()
	{
		File.Delete(Application.persistentDataPath + gameProgressFile);
	}

    /// <summary>
    /// ゲームの進行状況をファイルに保存します
    /// </summary>
    public void SaveGameProgress()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + gameProgressFile);
        progress.saveTime = DateTime.Now;
        bf.Serialize(file, progress);
        file.Close();
    }

    /// <summary>
    /// ファイルの進行状況を読み込みます
    /// </summary>
    public void LoadGameProgress()
    {
        if (File.Exists(Application.persistentDataPath + gameProgressFile) == true)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + gameProgressFile, FileMode.Open);
            progress = (GameProgressData)bf.Deserialize(file);
            file.Close();
        }
    }
}
