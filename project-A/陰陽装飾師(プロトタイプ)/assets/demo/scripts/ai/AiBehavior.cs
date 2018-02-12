/***
 * 
 *    Title: すべてのAI状態を制御するためのメインスクリプト
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBehavior : MonoBehaviour
{
    // この状態は開始時にアクティブになります
    public AiState defaultState;

    // このAIのすべての州のリスト
	private List<AiState> aiStates = new List<AiState>();
    // 前の状態
    private AiState previousState;
    // アクティブ状態
    private AiState currentState;

    /// <summary>
    /// このインスタンスを開始します
    /// </summary>
    void Start()
    {
        // このゲームオブジェクトからすべてのAI状態を取得する
        AiState[] states = GetComponents<AiState>();
        if (states.Length > 0) 
        {
			foreach (AiState state in states)
            {
                // リストに状態を追加する
                aiStates.Add(state);
            }
            if (defaultState != null)
            {
                // アクティブ状態と以前の状態をデフォルト状態に設定する
                previousState = currentState = defaultState;
                if (currentState != null)
                {
                    // アクティブ状態にする
                    ChangeState(currentState);
                }
                else
                {
                    Debug.LogError("Incorrect default AI state " + defaultState);
                }
            }
            else
            {
                Debug.LogError("AI have no default state");
            }
        } 
        else 
        {
            Debug.LogError("No AI states found");
        }
    }

    /// <summary>
    /// AIをデフォルト状態に設定します
    /// </summary>
    public void GoToDefaultState()
    {
        previousState = currentState;
		currentState = defaultState;
        NotifyOnStateExit();
        DisableAllStates();
        EnableNewState();
        NotifyOnStateEnter();
    }

    /// <summary>
    /// Ai状態を変更する
    /// </summary>
    /// <param name="state">状態</param>
	public void ChangeState(AiState state)
    {
		if (state != null)
        {
            // リストでそのような状態を見つけようとする
            foreach (AiState aiState in aiStates)
            {
                if (state == aiState)
                {
                    previousState = currentState;
                    currentState = aiState;
                    NotifyOnStateExit();
                    DisableAllStates();
                    EnableNewState();
                    NotifyOnStateEnter();
                    return;
                }
            }
            Debug.Log("No such state " + state);
            // そのような状態がない場合は、デフォルト状態になります
            GoToDefaultState();
            Debug.Log("Go to default state " + aiStates[0]);
        }
    }

    /// <summary>
    /// すべてのAI状態コンポーネントをオフにします
    /// </summary>
    private void DisableAllStates()
    {
		foreach (AiState aiState in aiStates) 
        {
			aiState.enabled = false;
        }
    }

    /// <summary>
    /// アクティブなAI状態コンポーネントをオンにします
    /// </summary>
    private void EnableNewState()
    {
		currentState.enabled = true;
    }

    /// <summary>
    /// 状態終了通知を以前の状態に送信します
    /// </summary>
    private void NotifyOnStateExit()
    {
		previousState.OnStateExit(previousState, currentState);
    }

    /// <summary>
    /// 新しい状態に通知を入力状態に送信します
    /// </summary>
    private void NotifyOnStateEnter()
    {
		currentState.OnStateEnter(previousState, currentState);
    }

    /// <summary>
    /// トリガーイベントを発生させます
    /// </summary>
    /// <param name="trigger">引き金</param>
    /// <param name="my">じぶんの</param>
    /// <param name="other">その他</param>
    public void OnTrigger(AiState.Trigger trigger, Collider2D my, Collider2D other)
    {
		if (currentState == null)
		{
			Debug.Log("Current sate is null");
		}
		currentState.OnTrigger(trigger, my, other);
    }
}
