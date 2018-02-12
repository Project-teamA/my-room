/***
 * 
 *    Title: AIコリジョンマスクの動的フィルタ
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiColliderTrigger : MonoBehaviour
{
    // 衝突検出のために許可されたオブジェクトタグ
    public List<string> tags = new List<string>();

    // 私のコライダー
    private Collider2D col;
    // 親オブジェクトのAI動作コンポーネント
    private AiBehavior aiBehavior;

    /// <summary>
    /// このインスタンスを目覚めさせる
    /// </summary>
    void Awake()
    {
        col = GetComponent<Collider2D>();
        aiBehavior = GetComponentInParent<AiBehavior>();
        Debug.Assert(col && aiBehavior, "Wrong initial parameters");
		col.enabled = false;
    }

    /// <summary>
    /// このインスタンスを開始します
    /// </summary>
    void Start()
	{
		col.enabled = true;
	}

    /// <summary>
    /// このインスタンスが指定されたタグで許可されているタグかどうかを判定します
    /// </summary>
    /// <returns>このインスタンスが指定されたタグにタグが許可されている場合は<c> true </ c>。 それ以外の場合は<c> false </ c>です</returns>
    /// <param name="tag">タグ</param>
    private bool IsTagAllowed(string tag)
    {
        bool res = false;
        if (tags.Count > 0)
        {
            foreach (string str in tags)
            {
                if (str == tag)
                {
                    res = true;
                    break;
                }
            }
        }
        else
        {
            res = true;
        }
        return res;
    }

    /// <summary>
    /// Enterイベントを発生させます
    /// </summary>
    /// <param name="other">その他</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsTagAllowed(other.tag) == true)
        {
            // このイベントに関するAIの動作を通知する
            aiBehavior.OnTrigger(AiState.Trigger.TriggerEnter, col, other);
        }
    }

    /// <summary>
    /// ステイ2dイベントを発生させます
    /// </summary>
    /// <param name="other">その他</param>
    void OnTriggerStay2D(Collider2D other)
    {
        if (IsTagAllowed(other.tag) == true)
        {
            // このイベントに関するAIの動作を通知する
            aiBehavior.OnTrigger(AiState.Trigger.TriggerStay, col, other);
        }
    }

    /// <summary>
    /// トリガー出口2Dイベントを発生させます
    /// </summary>
    /// <param name="other">その他</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (IsTagAllowed(other.tag) == true)
        {
            // このイベントに関するAIの動作を通知する
            aiBehavior.OnTrigger(AiState.Trigger.TriggerExit, col, other);
        }
    }
}
