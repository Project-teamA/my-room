/***
 * 
 *    Title: 近接武器で攻撃する
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMelee : MonoBehaviour, IAttack
{
    // 損害額
    public int damage = 1;
    // 攻撃間のクールダウン
    public float cooldown = 1f;

    // このAIのアニメーションコントローラ
    private Animator anim;
    // クールダウン計算のカウンター
    private float cooldownCounter;

    /// <summary>
    /// このインスタンスを目覚めさせる
    /// </summary>
    void Awake()
    {
		anim = GetComponentInParent<Animator>();
        cooldownCounter = cooldown;
    }

    /// <summary>
    /// このインスタンスを更新します
    /// </summary>
    void FixedUpdate()
    {
        if (cooldownCounter < cooldown)
        {
			cooldownCounter += Time.fixedDeltaTime;
        }
    }

    /// <summary>
    /// クールダウンが終了した場合、指定されたターゲットを攻撃する
    /// </summary>
    /// <param name="target">ターゲット</param>
    public void Attack(Transform target)
    {
        if (cooldownCounter >= cooldown)
        {
            cooldownCounter = 0f;
            Smash(target);
        }
    }

    /// <summary>
    /// 近接攻撃をする
    /// </summary>
    /// <param name="target">ターゲット</param>
    private void Smash(Transform target)
    {
        if (target != null)
        {
            // ターゲットがダメージを受ける可能性がある場合
            DamageTaker damageTaker = target.GetComponent<DamageTaker>();
            if (damageTaker != null)
            {
                damageTaker.TakeDamage(damage);
            }
            if (anim != null)
            {
				anim.SetTrigger("attackMelee");
            }
        }
    }
}
