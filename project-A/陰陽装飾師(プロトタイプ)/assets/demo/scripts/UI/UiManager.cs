/***
 * 
 *    Title: ユーザーインターフェイスとイベントマネージャ
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    // レベルの終了後に読み込まれます
    public string exitSceneName;
    // 開始画面キャンバス
    public GameObject startScreen;
    // メニューのキャンバスを一時停止する
    public GameObject pauseMenu;
    // メニューキャンバスを無効にする
    public GameObject defeatMenu;
    // ビクトリーメニューキャンバス
    public GameObject victoryMenu;
    // レベルインタフェース
    public GameObject levelUI;
    // 利用可能な霊力
    public Text goldAmount;
    // 敗北する前の捕捉の試み
    public Text defeatAttempts;
    // 勝利とディフェンスメニューの表示遅延
    public float menuDisplayDelay = 1f;

    //ゲームは一時停止していますか
    private bool paused;
    // カメラは今ドラッグしています
    private bool cameraIsDragged;
    // カメラのドラッグ開始点の原点
    private Vector3 dragOrigin = Vector3.zero;
    // カメラコントロールコンポーネント
    private CameraControl cameraControl;

    /// <summary>
    /// このインスタンスを目覚めさせる
    /// </summary>
    void Awake()
	{
		cameraControl = FindObjectOfType<CameraControl>();
		Debug.Assert(cameraControl && startScreen && pauseMenu && defeatMenu && victoryMenu && levelUI && defeatAttempts && goldAmount, "Wrong initial parameters");
	}

    /// <summary>
    /// 有効イベントを発生させます
    /// </summary>
    void OnEnable()
    {
		EventManager.StartListening("UnitKilled", UnitKilled);
		EventManager.StartListening("ButtonPressed", ButtonPressed);
    }

    /// <summary>
    /// 無効イベントを発生させます
    /// </summary>
    void OnDisable()
    {
		EventManager.StopListening("UnitKilled", UnitKilled);
		EventManager.StopListening("ButtonPressed", ButtonPressed);
    }

    /// <summary>
    /// このインスタンスを開始します
    /// </summary>
    void Start()
    {
		PauseGame(true);
    }

    /// <summary>
    /// このインスタンスを更新します
    /// </summary>
    void Update()
    {
        if (paused == false)
        {
            // ユーザがマウスボタンを押す
            if (Input.GetMouseButtonDown(0) == true)
            {
                // UIコンポーネント上にポインタがあるかどうかを確認する
                GameObject hittedObj = null;
                PointerEventData pointerData = new PointerEventData(EventSystem.current);
                pointerData.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, results);
				if (results.Count > 0) // ポインタ上のUIコンポーネント
                {
                    // 検索結果にアクションアイコンが表示される
                    foreach (RaycastResult res in results)
					{
						if (res.gameObject.CompareTag("ActionIcon"))
						{
							hittedObj = res.gameObject;
							break;
						}
					}
                    // UIコンポーネントのユーザークリックデータを含むメッセージを送信する
                    EventManager.TriggerEvent("UserUiClick", hittedObj, null);
				}
                else // ポインタにUIコンポーネントがありません
                {
                    // 衝突の上にポインタがあるかどうかを確認する
                    RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward);
                    foreach (RaycastHit2D hit in hits)
                    {
                        //これが許可されている場合
                        if (hit.collider.gameObject.CompareTag("Tower")
                            ||  hit.collider.gameObject.CompareTag("Enemy")
                            ||  hit.collider.gameObject.CompareTag("Defender"))
                        {
                            hittedObj = hit.collider.gameObject;
                            break;
                        }
                    }
                    // ゲーム空間のユーザークリックデータでメッセージを送信する
                    EventManager.TriggerEvent("UserClick", hittedObj, null);
                }
                // ヒットしたオブジェクトがない場合 - カメラのドラッグを開始する
                if (hittedObj == null)
                {
                    cameraIsDragged = true;
                    dragOrigin = Input.mousePosition;
                }
            }
            if (Input.GetMouseButtonUp(0) == true)
            {
                // マウスリリース時にカメラをドラッグしない
                cameraIsDragged = false;
            }
            if (cameraIsDragged == true)
            {
                Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
                // カメラのドラッグ（反転）
                cameraControl.MoveX(-pos.x);
                cameraControl.MoveY(-pos.y);
            }
        }
    }

    /// <summary>
    /// 現在のシーンを停止し、新しいシーンを読み込む
    /// </summary>
    /// <param name="sceneName">シーン名</param>
    private void LoadScene(string sceneName)
    {
        Debug.Log("Load");
        EventManager.TriggerEvent("SceneQuit", null, null);
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// ゲームを再開する
    /// </summary>
	private void ResumeGame()
    {
        GoToLevel();
        PauseGame(false);
    }

    /// <summary>
    /// メインメニューに切り替えます
    /// </summary>
	private void ExitFromLevel()
    {
        Debug.Log("Load");
        LoadScene(exitSceneName);
    }

    /// <summary>
    /// すべてのUIキャンバスを閉じます
    /// </summary>
    private void CloseAllUI()
    {
		startScreen.SetActive (false);
        pauseMenu.SetActive(false);
        defeatMenu.SetActive(false);
        victoryMenu.SetActive(false);
    }

    /// <summary>
    /// ゲームを一時停止する
    /// </summary>
    /// <param name="pause"><c> true </ c>に設定すると一時停止します</param>
    private void PauseGame(bool pause)
    {
        paused = pause;
        // 休止時間を止める
        Time.timeScale = pause ? 0f : 1f;
		EventManager.TriggerEvent("GamePaused", null, pause.ToString());
    }

    /// <summary>
    /// 一時停止メニュー
    /// </summary>
	private void GoToPauseMenu()
    {
        PauseGame(true);
        CloseAllUI();
        pauseMenu.SetActive(true);
    }

    /// <summary>
    /// レベル
    /// </summary>
    private void GoToLevel()
    {
        CloseAllUI();
        levelUI.SetActive(true);
        PauseGame(false);
    }

    /// <summary>
    /// 失敗
    /// </summary>
    public void GoToDefeatMenu()
    {
		StartCoroutine("DefeatCoroutine");
    }

    /// <summary>
    /// 遅延後に敗北メニューを表示する.
    /// </summary>
    /// <returns>コルーチン</returns>
    private IEnumerator DefeatCoroutine()
	{
		yield return new WaitForSeconds(menuDisplayDelay);
		PauseGame(true);
		CloseAllUI();
		defeatMenu.SetActive(true);
	}

    /// <summary>
    /// 成功
    /// </summary>
    public void GoToVictoryMenu()
    {
		StartCoroutine("VictoryCoroutine");
    }

    /// <summary>
    /// 遅れて勝利メニューを表示する
    /// </summary>
    /// <returns>コルーチン</returns>
    private IEnumerator VictoryCoroutine()
	{
		yield return new WaitForSeconds(menuDisplayDelay);
		PauseGame(true);
		CloseAllUI();

        // --- ゲームの進行状況の自動保存 ---
        //完成したレベルの名前を取得する
        DataManager.instance.progress.lastCompetedLevel = SceneManager.GetActiveScene().name;
        // このレベルが以前に完了していないかどうかを確認する
        bool hit = false;
		foreach (string level in DataManager.instance.progress.openedLevels)
		{
			if (level == SceneManager.GetActiveScene().name)
			{
				hit = true;
				break;
			}
		}
		if (hit == false)
		{
			DataManager.instance.progress.openedLevels.Add(SceneManager.GetActiveScene().name);
		}
        // ゲームの進行状況を保存
        DataManager.instance.SaveGameProgress();

		victoryMenu.SetActive(true);
	}

    /// <summary>
    /// 現在のレベルを再開します
    /// </summary>
	private void RestartLevel()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        LoadScene(SceneManager.GetActiveScene().name);        
    }

    /// <summary>
    /// 現在の霊力を取得します
    /// </summary>
    /// <returns>霊力</returns>
    private int GetGold()
    {
        int gold;
        int.TryParse(goldAmount.text, out gold);
        return gold;
    }

    /// <summary>
    /// 霊力を設定
    /// </summary>
    /// <param name="gold">霊力</param>
	public void SetGold(int gold)
    {
        goldAmount.text = gold.ToString();
    }

    /// <summary>
    /// 霊力を追加
    /// </summary>
    /// <param name="gold">霊力</param>
    private void AddGold(int gold)
    {
        SetGold(GetGold() + gold);
    }

    /// <summary>
    /// もしあれば霊力を使う
    /// </summary>
    /// <returns><c>true</c>, 霊力を使用, <c>false</c> ほか.</returns>
    /// <param name="cost">コスト.</param>
    public bool SpendGold(int cost)
    {
        bool res = false;
        int currentGold = GetGold();
        if (currentGold >= cost)
        {
            SetGold(currentGold - cost);
            res = true;
        }
        return res;
    }

    /// <summary>
    /// 敗北の試行回数を設定します
    /// </summary>
    /// <param name="attempts">試み</param>
    public void SetDefeatAttempts(int attempts)
	{
		defeatAttempts.text = attempts.ToString();
	}

    /// <summary>
    /// 他のユニットによって殺されたユニット
    /// </summary>
    /// <param name="obj">オブジェクト</param>
    /// <param name="param">パラメータ</param>
	private void UnitKilled(GameObject obj, string param)
    {
        // これが敵なら
        if (obj.CompareTag("Enemy"))
        {
            Price price = obj.GetComponent<Price>();
            if (price != null)
            {
                // 敵を殺すために霊力を加える
                AddGold(price.price);
            }
        }
    }

    /// <summary>
    /// ボタンが押されたハンドラ
    /// </summary>
    /// <param name="obj">オブジェクト</param>
    /// <param name="param">パラメータ</param>
    private void ButtonPressed(GameObject obj, string param)
	{
		switch (param)
		{
		case "Pause":
			GoToPauseMenu();
			break;
		case "Resume":
			GoToLevel();
			break;
		case "Back":
			ExitFromLevel();
			break;
		case "Restart":
			RestartLevel();
			break;
		}
	}

    /// <summary>
    /// 破壊イベントを発生させます
    /// </summary>
    void OnDestroy()
	{
		StopAllCoroutines();
	}
}
