using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene_Management : MonoBehaviour
{
    public enum Scene { Title, How_to_play, Game, Result };

    public Scene scene_;

    private GameObject Title;
    private GameObject How_to_play;
    private GameObject Game;
    private GameObject Room;
    private GameObject Result;
    private GameObject Mode;
    private GameObject OK;
    private GameObject Luck;

    private GameObject Grid_Manager;
    private GameObject Furniture_Manager;

    // 左クリックしたオブジェクトを取得する関数(3D)
    public GameObject getClickObject()
    {
        GameObject result = null;

        // 左クリックされた場所のオブジェクトを取得
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                result = hit.collider.gameObject;
            }
        }
        return result;
    }

    // Use this for initialization
    void Start()
    {
        scene_ = Scene.Title;

        Title = GameObject.Find("Canvas/Title");
        How_to_play = GameObject.Find("Canvas/How_to_play");
        Game = GameObject.Find("Canvas/Game");
        Room = GameObject.Find("Canvas/Room");
        Result = GameObject.Find("Canvas/Result");
        Mode = GameObject.Find("Canvas/Mode");
        Mode.GetComponent<Text>().text = "タイトル";
        OK = GameObject.Find("Canvas/OK");
        Luck = GameObject.Find("Canvas/Luck");

        Grid_Manager = GameObject.Find("Grid_Manager");
        Furniture_Manager = GameObject.Find("Furniture_Manager");

        Title.SetActive(true);
        How_to_play.SetActive(false);
        Game.SetActive(false);
        Room.SetActive(false);
        Result.SetActive(false);
        Mode.SetActive(true);
        OK.SetActive(true);
        Luck.SetActive(false);

        Grid_Manager.SetActive(false);
        Furniture_Manager.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (scene_ == Scene.Title)
        {
            if (getClickObject() == OK)
            {
                Title.SetActive(false);
                How_to_play.SetActive(true);
                scene_ = Scene.How_to_play;
                Mode.GetComponent<Text>().text = "遊び方";
            }
            
        }
        else if (scene_ == Scene.How_to_play)
        {
            if (getClickObject() == OK)
            {
                How_to_play.SetActive(false);
                Game.SetActive(true);
                Room.SetActive(true);
                Luck.SetActive(true);
                scene_ = Scene.Game;
                Mode.GetComponent<Text>().text = "ゲーム";               
                Grid_Manager.SetActive(true);
                Furniture_Manager.SetActive(true);
            }
        }
        else if (scene_ == Scene.Game)
        {
            if (getClickObject() == OK)
            {
                Game.SetActive(false);
                Room.SetActive(false);
                Result.SetActive(true);
                scene_ = Scene.Result;
                Mode.GetComponent<Text>().text = "リザルト";
                Luck.SetActive(false);
                Grid_Manager.SetActive(false);
                Furniture_Manager.SetActive(false);
            }
        }
        else if (scene_ == Scene.Result)
        {
            if (getClickObject() == OK)
            {
                Result.SetActive(false);
                Title.SetActive(true);
                scene_ = Scene.Title;
                Mode.GetComponent<Text>().text = "タイトル";
            }
        }

    }
}
