using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Help : MonoBehaviour {

    public RectTransform help_all;
    public GameObject left;
    public GameObject right;

    public void Title()
    {
        GameObject.Find("DataManager").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sound/SE/Button_Click"));

        StartCoroutine(LoadScene("Title"));
    }

    // コルーチン  
    private IEnumerator LoadScene(string name)
    {
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene(name);
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
