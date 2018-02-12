/***
 * 
 *    Title: レベル制御スクリプト
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // UIシーン、 レベルスタートでロード
    public string levelUiSceneName;
    // このレベルの最初霊力
    public int goldAmount = 20;
    // 敵が敗北する前にキャプチャポイントに到達できる回数
    public int defeatAttempts = 1;
    // このレベルの無作為に生成された敵のリスト
    public List<GameObject> allowedEnemies = new List<GameObject>();

    // ユーザーインターフェイスマネージャ
    private UiManager uiManager;
    // このレベルの敵の数
    private int spawnNumbers;
    // 現在のルーズカウンタ
    private int beforeLooseCounter;

    public GameObject Start_Menu;
    public GameObject Finish_Menu;
    public GameObject Timer;

    public GameObject icon;
    public GameObject icon_shikigami;
    public GameObject icon_youkai;
    public GameObject game;
    public GameObject game_shikigami;
    public GameObject game_youkai;
    public GameObject[] clear_luck = new GameObject[5];
    public GameObject[] room = new GameObject[5];

    public float counter;

    public Evaluation evaluation;
    public FurnitureManagement furnituremanagement;
    public GameObject GalleryManager;
    private GameObject DataManager;

    public GameObject Gage_Value_shikigami;
    public GameObject Gage_Value_youkai;

    private int advaice_mode;

    private int initial_value;
    public int base_value;
    public bool Game_Play = false;

    void Start()
	{
        DataManager = GameObject.Find("DataManager");
        advaice_mode = DataManager.GetComponent<DataManager>().read_advaice_mode();
        clear_luck[advaice_mode].SetActive(true);

        Timer.GetComponent<Text>().text = counter.ToString("f2");
    }

    private void Update()
    {
        if (Game_Play == true)
        {

            if (advaice_mode == 0)
            {
                Gage_Value_shikigami.GetComponent<RectTransform>().localScale = new Vector3((float)((evaluation.work_luck() + base_value) - initial_value) / (float)(base_value * 2), 1, 1);
            }
            else if (advaice_mode == 1)
            {
                Gage_Value_shikigami.GetComponent<RectTransform>().localScale = new Vector3((float)((evaluation.popular_luck() + base_value) - initial_value) / (float)(base_value * 2), 1, 1);
            }
            else if (advaice_mode == 2)
            {
                Gage_Value_shikigami.GetComponent<RectTransform>().localScale = new Vector3((float)((evaluation.health_luck() + base_value) - initial_value) / (float)(base_value * 2), 1, 1);                
            }
            else if (advaice_mode == 3)
            {
                Gage_Value_shikigami.GetComponent<RectTransform>().localScale = new Vector3((float)((evaluation.economic_luck() + base_value) - initial_value) / (float)(base_value * 2), 1, 1);
            }
            else if (advaice_mode == 4)
            {
                Gage_Value_shikigami.GetComponent<RectTransform>().localScale = new Vector3((float)((evaluation.love_luck() + base_value) - initial_value) / (float)(base_value * 2), 1, 1);
            }

            Gage_Value_youkai.GetComponent<RectTransform>().localScale = new Vector3(1 - Gage_Value_shikigami.GetComponent<RectTransform>().localScale.x, 1, 1);

            //勝利
            if (Gage_Value_shikigami.GetComponent<RectTransform>().localScale.x >= 1)
            {
                Gage_Value_shikigami.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                Gage_Value_youkai.GetComponent<RectTransform>().localScale = new Vector3(1 - Gage_Value_shikigami.GetComponent<RectTransform>().localScale.x, 1, 1);

                FinishGame(true);
            }
            
            counter -= Time.deltaTime;

            //敗北
            if (counter < 0)
            {
                counter = 0;
                FinishGame(false);
            }

            Timer.GetComponent<Text>().text = counter.ToString("f2");
        }        
    }

    public void StartGame()
    {
        GameObject.Find("DataManager").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Button_Click"));

        furnituremanagement.Room(DataManager.GetComponent<DataManager>().read_room(), DataManager.GetComponent<DataManager>().read_direction(), DataManager.GetComponent<DataManager>().read_norma_luck(), DataManager.GetComponent<DataManager>().read_advaice_mode());

        if (advaice_mode == 0)
        {
            icon_shikigami.GetComponent<Image>().sprite = Resources.Load<Sprite>("shikigami/water/icon");
            game_shikigami.GetComponent<Image>().sprite = Resources.Load<Sprite>("shikigami/water/game");
            icon_youkai.GetComponent<Image>().sprite = Resources.Load<Sprite>("youkai/work/icon");            
            game_youkai.GetComponent<Image>().sprite = Resources.Load<Sprite>("youkai/work/game");

            initial_value = evaluation.work_luck();
            Gage_Value_shikigami.GetComponent<Image>().color = Color.blue;
        }
        else if (advaice_mode == 1)
        {
            icon_shikigami.GetComponent<Image>().sprite = Resources.Load<Sprite>("shikigami/fire/icon");
            game_shikigami.GetComponent<Image>().sprite = Resources.Load<Sprite>("shikigami/fire/game");
            icon_youkai.GetComponent<Image>().sprite = Resources.Load<Sprite>("youkai/popular/icon");           
            game_youkai.GetComponent<Image>().sprite = Resources.Load<Sprite>("youkai/popular/game");

            initial_value = evaluation.popular_luck();
            Gage_Value_shikigami.GetComponent<Image>().color = Color.red;
        }
        else if (advaice_mode == 2)
        {
            icon_shikigami.GetComponent<Image>().sprite = Resources.Load<Sprite>("shikigami/wood/icon");
            game_shikigami.GetComponent<Image>().sprite = Resources.Load<Sprite>("shikigami/wood/game");
            icon_youkai.GetComponent<Image>().sprite = Resources.Load<Sprite>("youkai/health/icon");
            game_youkai.GetComponent<Image>().sprite = Resources.Load<Sprite>("youkai/health/game");

            initial_value = evaluation.health_luck();
            Gage_Value_shikigami.GetComponent<Image>().color = Color.green;
        }
        else if (advaice_mode == 3)
        {
            icon_shikigami.GetComponent<Image>().sprite = Resources.Load<Sprite>("shikigami/metal/icon");
            game_shikigami.GetComponent<Image>().sprite = Resources.Load<Sprite>("shikigami/metal/game");
            icon_youkai.GetComponent<Image>().sprite = Resources.Load<Sprite>("youkai/economic/icon");
            game_youkai.GetComponent<Image>().sprite = Resources.Load<Sprite>("youkai/economic/game");

            initial_value = evaluation.economic_luck();
            Gage_Value_shikigami.GetComponent<Image>().color = Color.yellow;
        }
        else if (advaice_mode == 4)
        {
            icon_shikigami.GetComponent<Image>().sprite = Resources.Load<Sprite>("shikigami/earth/icon");
            game_shikigami.GetComponent<Image>().sprite = Resources.Load<Sprite>("shikigami/earth/game");
            icon_youkai.GetComponent<Image>().sprite = Resources.Load<Sprite>("youkai/love/icon");
            game_youkai.GetComponent<Image>().sprite = Resources.Load<Sprite>("youkai/love/game");

            initial_value = evaluation.love_luck();
            Gage_Value_shikigami.GetComponent<Image>().color = Color.magenta;
        }

        icon.SetActive(true);
        game.SetActive(true);
        room[advaice_mode].SetActive(true);
        Start_Menu.SetActive(false);
        
        furnituremanagement.Add_.GetComponent<Button>().interactable = true;
        furnituremanagement.Change_Mode_.GetComponent<Button>().interactable = true;

        Game_Play = true;
    }

    public void FinishGame(bool win)
    {
        GalleryManager.GetComponent<bl_GalleryManager>().FullWindow.SetActive(false);
        GalleryManager.GetComponent<bl_GalleryManager>().Gallery_furniture.SetActive(false);
        GalleryManager.GetComponent<bl_GalleryManager>().Gallery_type.SetActive(false);

        furnituremanagement.Menu.SetActive(false); ;
        furnituremanagement.Add_.GetComponent<Button>().interactable = false;
        furnituremanagement.Change_Mode_.GetComponent<Button>().interactable = false;

        evaluation.set_is_finishedGame(true);
        evaluation.EvaluationTotal();
        evaluation.Comment_Text();

        Finish_Menu.SetActive(true);

        if (win == true)
        {
            Finish_Menu.GetComponent<Image>().sprite = Resources.Load<Sprite>("win");
        }
        else
        {
            Finish_Menu.GetComponent<Image>().sprite = Resources.Load<Sprite>("lose");
        }

        Game_Play = false;

        StartCoroutine(update());
    }

    public void ToResult()
    {
        GameObject.Find("DataManager").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Button_Click"));

        StartCoroutine(Sample("Result"));
    }

    // コルーチン  
    private IEnumerator Sample(string name)
    {
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene(name);
    }

    private IEnumerator update()
    {
        while (true)
        {
            Finish_Menu.GetComponent<RectTransform>().localScale = Vector3.Lerp(Finish_Menu.GetComponent<RectTransform>().localScale, new Vector3(1,1,1), 0.2f);

            if (Finish_Menu.GetComponent<RectTransform>().localScale.x - 1 < 0.1f)
            {
                Finish_Menu.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

                yield break;
            }
            yield return null;
        }

    }

    /// <summary>
    /// イベントを発生させます
    /// </summary>
    void OnEnable()
    {
        EventManager.StartListening("Captured", Captured);
        EventManager.StartListening("AllEnemiesAreDead", AllEnemiesAreDead);
    }

    /// <summary>
    /// 無効イベントを発生させます
    /// </summary>
    void OnDisable()
    {
        EventManager.StopListening("Captured", Captured);
        EventManager.StopListening("AllEnemiesAreDead", AllEnemiesAreDead);
    }

    /// <summary>
    /// 敵がキャプチャポイントに達しました
    /// </summary>
    /// <param name="obj">オブジェクト</param>
    /// <param name="param">パラメータ</param>
    private void Captured(GameObject obj, string param)
    {
		if (beforeLooseCounter > 0)
		{
			beforeLooseCounter--;
			uiManager.SetDefeatAttempts(beforeLooseCounter);
			if (beforeLooseCounter <= 0)
			{
                // 敗北
                uiManager.GoToDefeatMenu();
			}
		}
    }

    /// <summary>
    /// すべての敵は死んでいる
    /// </summary>
    /// <param name="obj">オブジェクト</param>
    /// <param name="param">パラメータ</param>
    private void AllEnemiesAreDead(GameObject obj, string param)
    {
        spawnNumbers--;
        // すべてのスポンサーで敵が死んだ
        if (spawnNumbers <= 0)
        {
            // 勝利
            uiManager.GoToVictoryMenu();
        }
    }
}
