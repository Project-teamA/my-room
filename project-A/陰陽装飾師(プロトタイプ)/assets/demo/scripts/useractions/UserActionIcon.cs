/***
 * 
 *    Title: UIアイコンの呪文（ユーザアクション）による操作
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserActionIcon : MonoBehaviour
{
    // スペルのクールダウン
    public float cooldown = 10f;
    // スペルプレハブ
    public GameObject userActionPrefab;
    // 強調表示された状態のアイコン
    public GameObject highlightIcon;
    // クールダウン状態のアイコン
    public GameObject cooldownIcon;
    // クールダウンカウンター（UIテキスト）
    public Text cooldownText;

    //マシンの状態
    private enum MyState
	{
		Active,
		Highligted,
		Cooldown
	}
    // このインスタンスの現在の状態
    private MyState myState = MyState.Active;
    // アクティブなユーザーアクション、強調表示されるとインスタンス化される
    private GameObject activeUserAction;
    // クールダウンカウンター
    private float cooldownCounter;

    /// <summary>
    /// 有効イベントを発生させます
    /// </summary>
    void OnEnable()
	{
		EventManager.StartListening("UserUiClick", UserUiClick);
		EventManager.StartListening("ActionStart", ActionStart);
		EventManager.StartListening("ActionCancel", ActionCancel);
	}

    /// <summary>
    /// 無効イベントを発生させます
    /// </summary>
    void OnDisable()
	{
		EventManager.StopListening("UserUiClick", UserUiClick);
		EventManager.StopListening("ActionStart", ActionStart);
		EventManager.StopListening("ActionCancel", ActionCancel);
	}


	void Start()
	{
		Debug.Assert(userActionPrefab && highlightIcon && cooldownIcon && cooldownText, "Wrong initial settings");
		StopCooldown();
	}


	void Update()
	{
		if (myState == MyState.Cooldown)
		{
			if (cooldownCounter > 0f)
			{
				cooldownCounter -= Time.deltaTime;
				UpdateCooldownText();
			}
			else if (cooldownCounter <= 0f)
			{
				StopCooldown();
			}
		}
	}

    /// <summary>
    /// ユーザUIクリックハンドラ
    /// </summary>
    /// <param name="obj">オブジェクト.</param>
    /// <param name="param">パラメータ.</param>
    private void UserUiClick(GameObject obj, string param)
	{
		if (obj == gameObject)  // このアイコンをクリック
        {
			if (myState == MyState.Active)
			{
				highlightIcon.SetActive(true);
				activeUserAction = Instantiate(userActionPrefab);
				myState = MyState.Highligted;
			}
		}
		else if (myState == MyState.Highligted) // 他のUIでクリック
        {
			highlightIcon.SetActive(false);
			myState = MyState.Active;
		}
	}

    /// <summary>
    /// アクション開始ハンドラ
    /// </summary>
    /// <param name="obj">オブジェクト.</param>
    /// <param name="param">パラメータ.</param>
    private void ActionStart(GameObject obj, string param)
	{
		if (obj == activeUserAction)
		{
			activeUserAction = null;
			highlightIcon.SetActive(false);
			StartCooldown();
		}
	}

    /// <summary>
    /// アクションはハンドラをキャンセルします
    /// </summary>
    /// <param name="obj">オブジェクト.</param>
    /// <param name="param">パラメータ.</param>
    private void ActionCancel(GameObject obj, string param)
	{
		if (obj == activeUserAction)
		{
			activeUserAction = null;
			highlightIcon.SetActive(false);
			myState = MyState.Active;
		}
	}

    /// <summary>
    /// クールダウンを開始します
    /// </summary>
    private void StartCooldown()
	{
		myState = MyState.Cooldown;
		cooldownCounter = cooldown;
		cooldownIcon.gameObject.SetActive(true);
		cooldownText.gameObject.SetActive(true);
	}

    /// <summary>
    /// クールダウンを止める
    /// </summary>
    private void StopCooldown()
	{
		myState = MyState.Active;
		cooldownCounter = 0f;
		cooldownIcon.gameObject.SetActive(false);
		cooldownText.gameObject.SetActive(false);
	}

    /// <summary>
    /// クールダウンカウンターのテキストを更新します
    /// </summary>
    private void UpdateCooldownText()
	{
		cooldownText.text = ((int)Mathf.Ceil(cooldownCounter)).ToString();
	}
}
