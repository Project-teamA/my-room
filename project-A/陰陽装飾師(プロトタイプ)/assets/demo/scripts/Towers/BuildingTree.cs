/***
 * 
 *    Title: 精霊のツリー
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTree : MonoBehaviour
{
    /// <summary>
    /// ツリーを開く
    /// </summary>
    [HideInInspector]
    public Tower myTower;

    /// <summary>
    /// このインスタンスを開始します
    /// </summary>
    void Start()
    {
        Debug.Assert(myTower, "間違った初期パラメータ");
    }

    /// <summary>
    /// 精霊を召喚
    /// </summary>
    /// <param name="prefab">プレハブ</param>
    public void Build(GameObject prefab)
    {
        myTower.BuildTower(prefab);
    }
}
