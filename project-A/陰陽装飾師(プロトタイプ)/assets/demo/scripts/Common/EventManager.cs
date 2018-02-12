/***
 * 
 *    Title: イベントタイプ
 *   
 */

using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MyEvent : UnityEvent<GameObject, string>
{

}

/// <summary>
/// メッセージシステム
/// </summary>
public class EventManager : MonoBehaviour
{
    // シングルトン
    public static EventManager instance;

    // イベントリスト
    private Dictionary<string, MyEvent> eventDictionary = new Dictionary<string, MyEvent>();

    /// <summary>
    /// destroyイベントを発生させます
    /// </summary>
    void OnDestroy()
	{
		instance = null;
	}

    /// <summary>
    /// 指定されたイベントの再生を開始します
    /// </summary>
    /// <param name="eventName">イベント名</param>
    /// <param name="listener">リスナー</param>
    public static void StartListening(string eventName, UnityAction<GameObject, string> listener)
    {
		if (instance == null)
		{
			instance = FindObjectOfType(typeof(EventManager)) as EventManager;
			if (instance == null)
			{
				Debug.Log("シーンにイベントマネージャーがいない");
				return;
			}
		}
        MyEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new MyEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    /// <summary>
    /// 指定されたイベントの再生を停止する
    /// </summary>
    /// <param name="eventName">イベント名</param>
    /// <param name="listener">リスナー</param>
    public static void StopListening(string eventName, UnityAction<GameObject, string> listener)
    {
		if (instance == null)
		{
			return;
		}
        MyEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    /// <summary>
    /// 指定されたイベントをトリガーします
    /// </summary>
    /// <param name="eventName">イベント名</param>
    /// <param name="obj">オブジェクト</param>
    /// <param name="param">パラメータ</param>
    public static void TriggerEvent(string eventName, GameObject obj, string param)
    {
		if (instance == null)
		{
			return;
		}
        MyEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(obj, param);
        }
    }
}
