/***
 * 
 *    Title: 精霊の建設と運営
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // 建築用プレハブ
    public GameObject buildingTreePrefab;
    // この精霊の攻撃範囲の可視化
    public GameObject rangeImage;

    // ユーザーインターフェイスマネージャ
    private UiManager uiManager;
    // ツリー表示用のレベルUIキャンバス
    private Canvas canvas;
    // この精霊の衝突者
    private Collider2D bodyCollider;
    // 表示された精霊ツリー
    private BuildingTree activeBuildingTree;

    /// <summary>
    /// 有効イベントを発生させます
    /// </summary>
    void OnEnable()
    {
        EventManager.StartListening("GamePaused", GamePaused);
        EventManager.StartListening("UserClick", UserClick);
		EventManager.StartListening("UserUiClick", UserClick);
    }

    /// <summary>
    /// 無効イベントを発生させる
    /// </summary>
    void OnDisable()
    {
        EventManager.StopListening("GamePaused", GamePaused);
        EventManager.StopListening("UserClick", UserClick);
		EventManager.StopListening("UserUiClick", UserClick);
    }

    /// <summary>
    /// このインスタンスを開始
    /// </summary>
    void Start()
    {
        uiManager = FindObjectOfType<UiManager>();
        // このキャンバスを使用して精霊のツリーUIを配置する
        Canvas[] canvases = Resources.FindObjectsOfTypeAll<Canvas>();
        foreach (Canvas canv in canvases)
        {
            if (canv.CompareTag("LevelUI"))
            {
                canvas = canv;
                break;
            }
        }
        bodyCollider = GetComponent<Collider2D>();
        Debug.Assert(uiManager && canvas && bodyCollider, "Wrong initial parameters");
    }

    /// <summary>
    /// 精霊ツリーを開き
    /// </summary>
    private void OpenBuildingTree()
    {
        if (buildingTreePrefab != null)
        {
            // ツリーを作成
            activeBuildingTree = Instantiate<GameObject>(buildingTreePrefab, canvas.transform).GetComponent<BuildingTree>();
            // 決めた位置の上に置く
            activeBuildingTree.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            activeBuildingTree.myTower = this;
            //　レイキャストを無効にする
            bodyCollider.enabled = false;
        }
    }

    /// <summary>
    /// ツリーを閉じます
    /// </summary>
    private void CloseBuildingTree()
    {
        if (activeBuildingTree != null)
        {
            Destroy(activeBuildingTree.gameObject);
            // レイキャストを有効にする
            bodyCollider.enabled = true;
        }
    }

    /// <summary>
    /// 精霊を召喚
    /// </summary>
    /// <param name="towerPrefab">精霊を召喚</param>
    public void BuildTower(GameObject towerPrefab)
    {
        // アクティブな選択肢を閉じる
        CloseBuildingTree();
        Price price = towerPrefab.GetComponent<Price>();
        // もし霊力があれば
        if (uiManager.SpendGold(price.price) == true)
        {
            // 新しい精霊を作り、それを同じ位置に置く
            GameObject newTower = Instantiate<GameObject>(towerPrefab, transform.parent);
            newTower.transform.position = transform.position;
            newTower.transform.rotation = transform.rotation;
            // 古い精霊を破壊する
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ゲームの一時停止時にレイキャストを無効にし、召喚のツリーを閉じる
    /// </summary>
    /// <param name="obj">オブジェクト</param>
    /// <param name="param">パラメータ</param>
    private void GamePaused(GameObject obj, string param)
    {
        if (param == bool.TrueString) // 一時停止中
        {
            CloseBuildingTree();
            bodyCollider.enabled = false;
        }
        else // 停止していない
        {
            bodyCollider.enabled = true;
        }
    }

    /// <summary>
    /// ユーザークリック
    /// </summary>
    /// <param name="obj">オブジェクト</param>
    /// <param name="param">パラメータ</param>
    private void UserClick(GameObject obj, string param)
    {
        if (obj == gameObject) // この精霊がクリックされた
        {
            // 攻撃範囲を表示
            ShowRange(true);
            if (activeBuildingTree == null)
            {
                // 開いているツリーがない場合
                OpenBuildingTree();
            }
        }
        else // その他のクリック
        {
            // 攻撃範囲を隠す
            ShowRange(false);
            // アクティブなツリーを閉じる
            CloseBuildingTree();
        }
    }

    /// <summary>
    /// ディスプレイ攻撃範囲
    /// </summary>
    /// <param name="condition"><c>true</c>条件に設定されている場合</param>
    private void ShowRange(bool condition)
    {
        if (rangeImage != null)
        {
            rangeImage.SetActive(condition);
        }
    }
}
