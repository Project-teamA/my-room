/***
 * 
 *    Title: AIがアイドル状態で動作できるようにします
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateIdle : AiState
{
    /// <summary>
    /// 状態入力イベントを発生させます
    /// </summary>
    /// <param name="previousState">以前の状態</param>
    /// <param name="newState">新しい状態</param>
	public override void OnStateEnter(AiState previousState, AiState newState)
    {
		if (anim != null)
		{
            // アニメーションを再生する
            anim.SetTrigger("idle");
		}
    }
}
