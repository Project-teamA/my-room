using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    // 新しいゲームを開始する
    public void Select()
	{       
        StartCoroutine(Load_Scene("Select"));
	}

    /// <summary>
    /// ヘルプ画面に移動
    /// </summary>
    public void Help()
	{        
        StartCoroutine(Load_Scene("Help"));
    }

    /// <summary>
    /// 終了
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    // コルーチン  
    private IEnumerator Load_Scene(string name)
    {
        GameObject.Find("DataManager").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sound/SE/Button_Click"));

        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene(name);
    }
}
