/***
 * 
 *    Title: AIがターゲットを攻撃できるようにします
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateAttack : AiState
{
	[Space(10)]
    // キャプチャポイントに最も近い攻撃ターゲット
    public bool useTargetPriority = false;
    // この受動的なイベントの状態になる
    public AiState passiveAiState;

    // 攻撃の標的
    private GameObject target;
    // このフレーム中に発見された潜在的なターゲットを含むリスト
    private List<GameObject> targetsList = new List<GameObject>();
    // 私の近接攻撃タイプであればそれを使う
    private IAttack meleeAttack;
    // 私の遠隔攻撃の種類
    private IAttack rangedAttack;
    // 最後の攻撃の種類が作成されます
    private IAttack myLastAttack;
    // もしあれば私のナビゲーションエージェント
    NavAgent nav;
    // この状態を終了する前に1フレームの新しいターゲットを待つことができます
    private bool targetless;

    /// <summary>
    /// このインスタンスを目覚めさせる
    /// </summary>
    public override void Awake()
    {
		base.Awake();
        meleeAttack = GetComponentInChildren<AttackMelee>() as IAttack;
        rangedAttack = GetComponentInChildren<AttackRanged>() as IAttack;
        nav = GetComponent<NavAgent>();
        Debug.Assert(meleeAttack != null || rangedAttack != null, "Wrong initial parameters");
    }

    /// <summary>
    /// 状態終了イベントを発生させます
    /// </summary>
    /// <param name="previousState">以前の状態</param>
    /// <param name="newState">新しい状態</param>
	public override void OnStateExit(AiState previousState, AiState newState)
    {
        LoseTarget();
    }

    /// <summary>
    /// このインスタンスの更新を修正しました
    /// </summary>
    void FixedUpdate ()
    {
        // ターゲットがない場合 - 新しいターゲットを取得しよう
        if ((target == null) && (targetsList.Count > 0))
        {
            target = GetTopmostTarget();
            if ((target != null) && (nav != null))
            {
                // ターゲットを見る
                nav.LookAt(target.transform);
            }
        }
        // ターゲットはありません
        if (target == null)
        {
            if (targetless == false)
            {
                targetless = true;
            }
            else
            {
                // 複数のターゲットを持たない場合は、この状態から抜け出す
                aiBehavior.ChangeState(passiveAiState);
            }
        }
    }

    /// <summary>
    /// リストから優先度の高いターゲットを取得します
    /// </summary>
    /// <returns>一番上のターゲット</returns>
    private GameObject GetTopmostTarget()
    {
        GameObject res = null;
        if (useTargetPriority == true) // キャプチャポイントまでの距離を最小にしてターゲットを取得する
        {
            float minPathDistance = float.MaxValue;
            foreach (GameObject ai in targetsList)
            {
                if (ai != null)
                {
                    AiStatePatrol aiStatePatrol = ai.GetComponent<AiStatePatrol>();
                    float distance = aiStatePatrol.GetRemainingPath();
                    if (distance < minPathDistance)
                    {
                        minPathDistance = distance;
                        res = ai;
                    }
                }
            }
        }
        else // リストから最初のターゲットを取得する
        {
            res = targetsList[0];
        }
        // 潜在的なターゲットのリストをクリア
        targetsList.Clear();
        return res;
    }

    /// <summary>
    /// 現在のターゲットを失う
    /// </summary>
    private void LoseTarget()
    {
        target = null;
        targetless = false;
        myLastAttack = null;
    }

    /// <summary>
    /// トリガーイベントを発生させます
    /// </summary>
    /// <param name="trigger">引き金</param>
    /// <param name="my">じぶんの</param>
    /// <param name="other">その他</param>
    public override bool OnTrigger(AiState.Trigger trigger, Collider2D my, Collider2D other)
	{
		if (base.OnTrigger(trigger, my, other) == false)
		{
			switch (trigger)
			{
			case AiState.Trigger.TriggerStay:
				TriggerStay(my, other);
				break;
			case AiState.Trigger.TriggerExit:
				TriggerExit(my, other);
				break;
			}
		}
		return false;
	}

    /// <summary>
    /// 滞在をトリガーします
    /// </summary>
    /// <param name="my">じぶんの</param>
    /// <param name="other">その他</param>
	private void TriggerStay(Collider2D my, Collider2D other)
    {
        if (target == null) // 潜在的なターゲットリストに新しいターゲットを追加する
        {
            targetsList.Add(other.gameObject);
        }
        else // 現在のターゲットを攻撃する
        {
            // これが私の現在の目標であれば
            if (target == other.gameObject)
            {
                if (my.name == "MeleeAttack") // ターゲットが近接攻撃範囲にある場合
                {
                    // 近接攻撃型の場合
                    if (meleeAttack != null)
                    {
                        // 私の最後の攻撃タイプを覚えている
                        myLastAttack = meleeAttack as IAttack;
                        // 近接攻撃を試みる
                        meleeAttack.Attack(other.transform);
                    }
                }
                else if (my.name == "RangedAttack") // ターゲットが遠隔攻撃範囲内にある場合
                {
                    // 私が遠隔攻撃タイプを持っているなら
                    if (rangedAttack != null)
                    {
                        // 近接攻撃範囲外のターゲット
                        if ((meleeAttack == null)
                            || ((meleeAttack != null) && (myLastAttack != meleeAttack)))
                        {
                            // 私の最後の攻撃タイプを覚えている
                            myLastAttack = rangedAttack as IAttack;
                            // 遠隔攻撃を試みる
                            rangedAttack.Attack(other.transform);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 出口をトリガーします
    /// </summary>
    /// <param name="my">じぶんの</param>
    /// <param name="other">その他</param>
	private void TriggerExit(Collider2D my, Collider2D other)
    {
        if (other.gameObject == target)
        {
            // 攻撃範囲を終了したら目標を失う
            LoseTarget();
        }
    }
}
