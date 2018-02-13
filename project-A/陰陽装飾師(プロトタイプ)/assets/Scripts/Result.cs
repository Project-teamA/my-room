using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour {

    private GameObject DataManager;
    private List<string> comment = new List<string>();

    public List<Text> comment_text;
    public GameObject shikigami;

    // Use this for initialization
    void Start () {

        //Time.timeScale = 1;

        DataManager = GameObject.Find("DataManager");

        if (DataManager.GetComponent<DataManager>().read_advaice_mode() == 0)
        {
            shikigami.GetComponent<Image>().sprite = Resources.Load<Sprite>("shikigami/water/body");
            StartCoroutine(Delay("shikigami/water/voice"));
        }
        else if (DataManager.GetComponent<DataManager>().read_advaice_mode() == 1)
        {
            shikigami.GetComponent<Image>().sprite = Resources.Load<Sprite>("shikigami/fire/body");
            StartCoroutine(Delay("shikigami/fire/voice"));
        }
        else if (DataManager.GetComponent<DataManager>().read_advaice_mode() == 2)
        {
            shikigami.GetComponent<Image>().sprite = Resources.Load<Sprite>("shikigami/wood/body");
            StartCoroutine(Delay("shikigami/wood/voice"));
        }
        else if (DataManager.GetComponent<DataManager>().read_advaice_mode() == 3)
        {
            shikigami.GetComponent<Image>().sprite = Resources.Load<Sprite>("shikigami/metal/body");
            StartCoroutine(Delay("shikigami/metal/voice"));
        }
        else if (DataManager.GetComponent<DataManager>().read_advaice_mode() == 4)
        {
            shikigami.GetComponent<Image>().sprite = Resources.Load<Sprite>("shikigami/earth/body");
            StartCoroutine(Delay("shikigami/earth/voice"));
        }

        for (int i = 0; i < DataManager.GetComponent<DataManager>().read_comment().Count; i++)
        {
            comment.Add(DataManager.GetComponent<DataManager>().read_comment()[i]);
            comment_text[i].text = comment[i];
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Button_Clicked()
    {
        GameObject.Find("DataManager").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sound/SE/Button_Click"));
    }

    public void To_Title()
    {
        StartCoroutine(Sample("Title"));
    }

    public void To_Select()
    {
        StartCoroutine(Sample("Select"));
    }

    public void To_Game()
    {
        StartCoroutine(Sample("Game"));
    }

    // コルーチン  
    private IEnumerator Sample(string name)
    {
        Button_Clicked();

        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene(name);
    }

    // コルーチン  
    private IEnumerator Delay(string pass)
    {
        yield return new WaitForSeconds(2.0f);

        DataManager.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>(pass));
    }

}
