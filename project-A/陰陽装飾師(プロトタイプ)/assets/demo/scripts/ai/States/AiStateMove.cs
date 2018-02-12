/***
 * 
 *    Title: AIが目的地に向かって移動することを許可する
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateMove : AiState
{
	[Space(10)]
    // 移動のエンドポイント
    public Transform destination;
    // この受動的なイベントの状態になる
    public AiState passiveAiState;

    // このゲームオブジェクトのナビゲーションエージェント
    NavAgent navAgent;

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
        // ナビゲーションエージェントの宛先を設定する
        navAgent.destination = destination.position;
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
        // 送信先に達した場合
        if ((Vector2)transform.position == (Vector2)destination.position)
        {
            // 必要な方向を見てください
            navAgent.LookAt(destination.right);
            // パッシブ状態にする
            aiBehavior.ChangeState(passiveAiState);
        }
    }
}
