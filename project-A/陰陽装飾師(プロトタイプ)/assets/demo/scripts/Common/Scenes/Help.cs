/***
 * 
 *    Title: ヘルプ
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Help : MonoBehaviour {

    // 終了するシーン
    public string exitSceneName;

    /// <summary>
    /// シーンを終了します
    /// </summary>
    public void Exit()
    {
        GameObject.Find("DataManager").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Button_Click"));

        StartCoroutine(Sample(exitSceneName));
    }

    // コルーチン  
    private IEnumerator Sample(string name)
    {
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene(name);
    }
}
