//GridError.cs(家具グリッド同士が衝突したら赤く表示, 家具グリッドオブジェクト一つ一つにアタッチされる)
//この仕様は変更する可能性大
//
// 2017年12月4日 更新(菅原涼太)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridError : MonoBehaviour {

    //他の家具グリッドにぶつかっているときに家具グリッドを赤にする(他オブジェクトは無視)
    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "furniture_grid")
        {
            GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);
        }
    }

    //他のグリッドにぶつかっていなかったら家具グリッドを白にする(他オブジェクトは無視)
    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.tag == "furniture_grid")
        {
            GetComponent<MeshRenderer>().material.color = new Color(255, 255, 255);
        }
    }
}
