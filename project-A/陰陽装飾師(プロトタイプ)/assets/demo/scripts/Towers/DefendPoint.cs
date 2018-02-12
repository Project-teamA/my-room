/***
 * 
 *    Title: ディフェンダーのためのポジション
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendPoint : MonoBehaviour
{
    // 防御ポイントのプレハブ
    public GameObject defendPointPrefab;

    //　この防御ポイントのための場所を守るリスト
    private List<Transform> defendPlaces = new List<Transform>();

    /// <summary>
    /// このインスタンスを目覚めさせる
    /// </summary>
    void Awake()
	{
		Debug.Assert(defendPointPrefab, "間違った初期設定");
        // 防御ポイントプレハブから場所を守り、場面に置く
        foreach (Transform defendPlace in defendPointPrefab.transform)
		{
			Instantiate(defendPlace.gameObject, transform);
		}
        // 場所を作成するリストを作成する
        foreach (Transform child in transform)
		{
			defendPlaces.Add(child);
		}
	}

    /// <summary>
    ///　防御ポイントリストを取得します
    /// </summary>
    /// <returns>防御ポイント</returns>
    public List<Transform> GetDefendPoints()
    {
		return defendPlaces;
    }
}
