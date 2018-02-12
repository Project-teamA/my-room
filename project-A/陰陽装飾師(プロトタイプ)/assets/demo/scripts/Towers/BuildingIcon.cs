/***
 * 
 *    Title: UI icon　と精霊ツリー
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingIcon : MonoBehaviour
{
    // プレハブのアイコン
    public GameObject towerPrefab;

    // 霊力のテキストフィールド
    private Text price;
    // ツリー
    private BuildingTree myTree;

    /// <summary>
    /// 有効イベントを発生させます
    /// </summary>
    void OnEnable()
    {
        EventManager.StartListening("UserUiClick", UserUiClick);
    }

    /// <summary>
    /// 無効イベントを発生させます
    /// </summary>
    void OnDisable()
    {
		EventManager.StopListening("UserUiClick", UserUiClick);
    }

    /// <summary>
    /// このインスタンスを目覚めさせる
    /// </summary>
    void Awake()
    {
        // 親オブジェクトからツリーを取得する
        myTree = transform.GetComponentInParent<BuildingTree>();
        price = GetComponentInChildren<Text>();
        Debug.Assert(price && myTree, "間違った初期パラメータ");
        if (towerPrefab == null)
        {
            // このアイコンにプレハブがない場合 - アイコンを隠す
            gameObject.SetActive(false);
        }
        else
        {
            // ディスプレイ霊力
            price.text = towerPrefab.GetComponent<Price>().price.ToString();
        }
    }

    /// <summary>
    /// ユーザーのUIをクリックします
    /// </summary>
    /// <param name="obj">オブジェクト</param>
    /// <param name="param">パラメータ</param>
	private void UserUiClick(GameObject obj, string param)
    {
        // このアイコンをクリックすると
        if (obj == gameObject)
        {
            // 精霊を召喚
            myTree.Build(towerPrefab);
        }
    }
}
