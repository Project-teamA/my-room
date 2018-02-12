/***
 * 
 *    Title: ボタンハンドラ
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    /// <summary>
    /// ボタンが押された
    /// </summary>
    /// <param name="buttonName">ボタン名</param>
    public void ButtonPressed(string buttonName)
	{
        // ボタンのプリエスに関するグローバルイベントを送信する
        EventManager.TriggerEvent("ButtonPressed", gameObject, buttonName);
	}
}
