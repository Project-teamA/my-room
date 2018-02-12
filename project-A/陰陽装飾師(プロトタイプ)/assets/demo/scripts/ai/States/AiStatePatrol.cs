/***
 * 
 *    Title: AIが指定されたパスで移動できるようにします
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStatePatrol : AiState
{
	[Space(10)]
	[HideInInspector]
    // 指定されたパス
    public Pathway path;
    // 最後のポイントに達した後にパスをループする必要がありますか？
    public bool loop = false;

    // このゲームオブジェクトのナビゲーションエージェント
    NavAgent navAgent;
    // 現在の目的地
    private Waypoint destination;

    /// <summary>
    /// このインスタンスを目覚めさせる
    /// </summary>
	public override void Awake()
    {
		base.Awake();
        navAgent = GetComponent<NavAgent>();
        Debug.Assert (navAgent, "Wrong initial parameters");
    }

    /// <summary>
    /// 状態入力イベントを発生させます
    /// </summary>
    /// <param name="previousState">以前の状態</param>
    /// <param name="newState">新しい状態</param>
	public override void OnStateEnter(AiState previousState, AiState newState)
    {
        if (path == null)
        {
            // パスがない場合 - それを見つけることを試みる
            path = FindObjectOfType<Pathway>();
            Debug.Assert(path, "Have no path");
        }
        if (destination == null)
        {
            // パスから次のウェイポイントを取得する
            destination = path.GetNearestWaypoint (transform.position);
        }
        // ナビゲーションエージェントの宛先を設定する
        navAgent.destination = destination.transform.position;
        // 移動を開始する
        navAgent.move = true;
		navAgent.turn = true;
        if (anim != null)
        {
            // アニメーションを再生する
            anim.SetTrigger("move");
        }
    }

    /// <summary>
    /// 状態終了イベントを発生させます
    /// </summary>
    /// <param name="previousState">以前の状態</param>
    /// <param name="newState">新しい状態</param>
	public override void OnStateExit(AiState previousState, AiState newState)
    {
        // 移動を停止する
        navAgent.move = false;
		navAgent.turn = false;
    }

    /// <summary>
    /// このインスタンスの更新を修正しました
    /// </summary>
    void FixedUpdate()
    {
        if (destination != null)
        {
            // 送信先に達した場合
            if ((Vector2)destination.transform.position == (Vector2)transform.position)
            {
                // 私のパスから次のウェイポイントを取得する
                destination = path.GetNextWaypoint (destination, loop);
                if (destination != null)
                {
                    // ナビゲーションエージェントの宛先を設定する
                    navAgent.destination = destination.transform.position;
                }
            }
        }
    }

    /// <summary>
    /// 経路上の残りの距離を取得します
    /// </summary>
    /// <returns>残りの経路</returns>
    public float GetRemainingPath()
    {
        Vector2 distance = destination.transform.position - transform.position;
        return (distance.magnitude + path.GetPathDistance(destination));
    }
}
