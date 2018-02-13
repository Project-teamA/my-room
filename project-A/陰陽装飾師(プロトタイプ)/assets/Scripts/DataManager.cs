using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

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

    private bool[] current_scene = new bool[5];

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

    private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            // this is the first instance -make it persist
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            // this must be aduplicate from a scene reload  - DESTROY!
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sound/BGM/Title_BGM");
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
                gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sound/BGM/Title_BGM");
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
                gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sound/BGM/Help_BGM");
                gameObject.GetComponent<AudioSource>().Play();

                current_scene[1] = false;

                current_scene[0] = true;
            }
           
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Select"))
        {
            if (current_scene[2] == true)
            {
                gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sound/BGM/Select_BGM");
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
                gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sound/BGM/Game_BGM");
                gameObject.GetComponent<AudioSource>().Play();

                current_scene[3] = false;

                current_scene[4] = true;
            }
           
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Result"))
        {
            if (current_scene[4] == true)
            {
                gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sound/BGM/Result_BGM");
                gameObject.GetComponent<AudioSource>().Play();

                current_scene[4] = false;

                current_scene[0] = true;
                current_scene[2] = true;
                current_scene[3] = true;
            }
            
        }
    }
}
