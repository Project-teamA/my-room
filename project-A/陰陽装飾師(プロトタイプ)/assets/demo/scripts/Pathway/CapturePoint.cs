/***
 * 
 *    Title: この時点で敵が起き上がると、プレイヤーは負けます
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePoint : MonoBehaviour
{
    /// <summary>
    /// イベントを発生させます
    /// </summary>
    /// <param name="other">その他</param>
    void OnTriggerEnter2D(Collider2D other)
    {
		Destroy(other.gameObject);
		EventManager.TriggerEvent("Captured", other.gameObject, null);
    }
}
