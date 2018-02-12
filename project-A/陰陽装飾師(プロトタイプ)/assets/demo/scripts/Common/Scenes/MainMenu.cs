using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // クリック時に開始するシーンの名前
    public string startSceneName;
    public string helpName;

    public RectTransform help_all;
    public GameObject left;
    public GameObject right;

    // 新しいゲームを開始する
    public void NewGame()
	{
        GameObject.Find("DataManager").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Button_Click"));

        StartCoroutine(Sample(startSceneName));
	}

    // コルーチン  
    private IEnumerator Sample(string name)
    {
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene(name);
    }

    /// <summary>
    /// ヘルプ画面に移動
    /// </summary>
    public void Help()
	{
        GameObject.Find("DataManager").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Button_Click"));

        StartCoroutine(Sample(helpName));
    }

    /// <summary>
    /// 終了
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    public void scroll(bool next)
    {
        left.GetComponent<Button>().interactable = false;
        right.GetComponent<Button>().interactable = false;

        if (next == true)
        {
            left.SetActive(true);
            if (help_all.localPosition.x == -1920 * 2)
            {
                right.SetActive(false);
            }
            StartCoroutine(update(true, help_all.localPosition));            
        }
        else
        {
            right.SetActive(true);
            if (help_all.localPosition.x == -1920)
            {
                left.SetActive(false);
            }
            StartCoroutine(update(false, help_all.localPosition));
        }        
    }

    private IEnumerator update(bool next, Vector3 start_pos)
    {
        while (true)
        {
            if (next == true)
            {
                help_all.localPosition = Vector3.Lerp(help_all.localPosition, start_pos - new Vector3(1920, 0, 0), 0.2f);

                if ((start_pos.x - 1920) - help_all.localPosition.x > -10.0f)
                {
                    help_all.localPosition = start_pos - new Vector3(1920, 0, 0);

                    left.GetComponent<Button>().interactable = true;
                    right.GetComponent<Button>().interactable = true;
                    
                    yield break;
                }
            }
            else
            {
                help_all.localPosition = Vector3.Lerp(help_all.localPosition, start_pos + new Vector3(1920, 0, 0), 0.2f);

                if ((start_pos.x + 1920) - help_all.localPosition.x < 10.0f)
                {
                    help_all.localPosition = start_pos + new Vector3(1920, 0, 0);

                    left.GetComponent<Button>().interactable = true;
                    right.GetComponent<Button>().interactable = true;
                    
                    yield break;
                }
            }

            yield return null;
        }
        
    }
}
