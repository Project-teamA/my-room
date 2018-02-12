/***
 * 
 *    Title: AI状態の基本クラス
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AiState : MonoBehaviour
{
    //　召喚の許可されるトリガータイプ
    public enum Trigger
	{
		TriggerEnter,   // コライダーでの入力
        TriggerStay,    // コライダー滞在中
        TriggerExit,    // コライダー出口で
        Damage,         // 奪取された被害
        Cooldown        // 一部のクールダウンが終了しました
    }

	[Serializable]
    // どのトリガでもAI状態の変化を指定できます
    public class AiTransaction
	{
		public Trigger trigger;
		public AiState newState;
	}
    // このAI状態に対して指定されたトランザクションのリスト
    public AiTransaction[] specificTransactions;

    // このAIのアニメーションコントローラ
    protected Animator anim;
    // このオブジェクトのAI動作
    protected AiBehavior aiBehavior;

    /// <summary>
    /// このインスタンスを目覚めさせる
    /// </summary>
    public virtual void Awake()
	{
		aiBehavior = GetComponent<AiBehavior> ();
		anim = GetComponentInParent<Animator>();
		Debug.Assert (aiBehavior, "Wrong initial parameters");
	}

    /// <summary>
    /// 状態入力イベントを発生させます
    /// </summary>
    /// <param name="previousState">以前の状態</param>
    /// <param name="newState">新しい状態</param>
    public virtual void OnStateEnter(AiState previousState, AiState newState)
	{
		
	}

    /// <summary>
    /// 状態終了イベントを発生させます
    /// </summary>
    /// <param name="previousState">以前の状態</param>
    /// <param name="newState">新しい状態</param>
    public virtual void OnStateExit(AiState previousState, AiState newState)
	{

	}

    /// <summary>
    /// トリガーイベントを発生させます
    /// </summary>
    /// <param name="trigger">引き金</param>
    /// <param name="my">じぶんの</param>
    /// <param name="other">その他</param>
    public virtual bool OnTrigger(Trigger trigger, Collider2D my, Collider2D other)
	{
		bool res = false;
        // このAI状態にこのトリガーの特定のトランザクションがあるかどうかを確認する
        foreach (AiTransaction transaction in specificTransactions)
		{
			if (trigger == transaction.trigger)
			{
				aiBehavior.ChangeState(transaction.newState);
				res = true;
				break;
			}
		}
		return res;
	}
}
