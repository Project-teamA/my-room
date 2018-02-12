/***
 * 
 *    Title: 敵の移動経路
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Pathway : MonoBehaviour
{
#if UNITY_EDITOR
    /// <summary>
    /// このインスタンスを更新します
    /// </summary>
    void Update()
    {
        Waypoint[] waypoints = GetComponentsInChildren<Waypoint>();
        if (waypoints.Length > 1)
        {
            int idx;
            for (idx = 1; idx < waypoints.Length; idx++)
            {
                // 編集モードでパスウェイに沿って青い線を引く
                Debug.DrawLine(waypoints[idx - 1].transform.position, waypoints[idx].transform.position, new Color(0.7f, 0f, 0f));
            }
        }
    }
#endif

    /// <summary>
    /// 指定された位置に最も近いウェイポイントを取得します
    /// </summary>
    /// <returns>最も近いウェイポイント</returns>
    /// <param name="position">ポジション</param>
    public Waypoint GetNearestWaypoint(Vector3 position)
    {
        float minDistance = float.MaxValue;
        Waypoint nearestWaypoint = null;
        foreach (Waypoint waypoint in GetComponentsInChildren<Waypoint>())
        {
            if (waypoint.GetHashCode() != GetHashCode())
            {
                // ウェイポイントまでの距離を計算する
                Vector3 vect = position - waypoint.transform.position;
                float distance = vect.magnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestWaypoint = waypoint;
                }
            }
        }
        return nearestWaypoint;
    }

    /// <summary>
    /// この経路上の次のウェイポイントを取得します
    /// </summary>
    /// <returns>次のウェイポイント</returns>
    /// <param name="currentWaypoint">現在のウェイポイント</param>
    /// <param name="loop"><c> true </ c>ループに設定されている場合</param>
    public Waypoint GetNextWaypoint(Waypoint currentWaypoint, bool loop)
    {
        Waypoint res = null;
        int idx = currentWaypoint.transform.GetSiblingIndex();
        if (idx < (transform.childCount - 1))
        {
            idx += 1;
        }
        else
        {
            idx = 0;
        }
        if (!(loop == false && idx == 0))
        {
            res = transform.GetChild(idx).GetComponent<Waypoint>(); 
        }
        return res;
    }

    /// <summary>
    /// 指定されたウェイポイントからの残りのパス距離を取得します
    /// </summary>
    /// <returns>パスの距離</returns>
    /// <param name="fromWaypoint">ウェイポイントから</param>
    public float GetPathDistance(Waypoint fromWaypoint)
    {
        Waypoint[] waypoints = GetComponentsInChildren<Waypoint>();
        bool hitted = false;
        float pathDistance = 0f;
        int idx;
        // 残りの経路を計算する
        for (idx = 0; idx < waypoints.Length; ++idx)
        {
            if (hitted == true)
            {
                // ウェイポイントと結果の距離を加算する
                Vector2 distance = waypoints[idx].transform.position - waypoints[idx - 1].transform.position;
                pathDistance += distance.magnitude;
            }
            if (waypoints[idx] == fromWaypoint)
            {
                hitted = true;
            }
        }
        return pathDistance;
    }
}
