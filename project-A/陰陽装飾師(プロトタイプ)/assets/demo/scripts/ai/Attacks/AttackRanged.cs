/***
 * 
 *    Title: 遠隔武器で攻撃する
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRanged : MonoBehaviour, IAttack
{
    // 損害額
    public int damage = 1;
    // 攻撃間のクールダウン
    public float cooldown = 1f;
    // 矢印のプレハブ
    public GameObject arrowPrefab;
    // この位置から矢印が発射されます
    public Transform firePoint;

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
        Debug.Assert(arrowPrefab && firePoint, "Wrong initial parameters");
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
            Fire(target);
        }
    }

    /// <summary>
    /// 遠隔攻撃をする
    /// </summary>
    /// <param name="target">ターゲット</param>
    private void Fire(Transform target)
    {
        if (target != null)
        {
            // 矢印の作成
            GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
            IBullet bullet = arrow.GetComponent<IBullet>();
            bullet.SetDamage(damage);
            bullet.Fire(target);
            if (anim != null)
            {
				anim.SetTrigger("attackRanged");
            }
        }
    }
}
