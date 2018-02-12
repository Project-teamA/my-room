/***
 * 
 *    Title: 矢印飛行軌道
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BulletArrow : MonoBehaviour, IBullet
{
    // 損害額
    [HideInInspector] int damage = 1;
    // 最大寿命
    public float lifeTime = 3f;
    // 開始速度
    public float speed = 3f;
    // 一定の加速
    public float speedUpOverTime = 0.5f;
    // ターゲットがこの距離より近い場合、それはヒットされます
    public float hitDistance = 0.2f;
    // 弾道軌道オフセット（ターゲットまでの距離）
    public float ballisticOffset = 0.5f;
    // 飛行中に弾丸を回転させない
    public bool freezeRotation = false;
    // この弾丸は単一のターゲットにダメージを与えません、 もしあればAOEダメージのみ
    public bool aoeDamageOnly = false;

    // この位置から弾丸が発射された
    private Vector2 originPoint;
    // 目標を目指す
    private Transform target;
    // 最後の目標位置
    private Vector2 aimPoint;
    // 弾道オフセットなしの現在の位置
    private Vector2 myVirtualPosition;
    // 最後のフレームの位置
    private Vector2 myPreviousPosition;
    // 加速計算のためのカウンタ
    private float counter;
    // この箇条書きのイメージ
    private SpriteRenderer sprite;

    /// <summary>
    /// この弾のダメージ量を設定します
    /// </summary>
    /// <param name="damage">ダメージ</param>
    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    /// <summary>
    /// この弾のダメージ量を取得する
    /// </summary>
    /// <returns>ダメージ</returns>
    public int GetDamage()
	{
		return damage;
	}

    /// <summary>
    /// 指定されたターゲットに向かって射撃する
    /// </summary>
    /// <param name="target">ターゲット</param>
    public void Fire(Transform target)
    {
        sprite = GetComponent<SpriteRenderer>();
        // 飛行の方向がわからないので、最初のフレームでスプライトを無効にする
        sprite.enabled = false;
        originPoint = myVirtualPosition = myPreviousPosition = transform.position;
        this.target = target;
        aimPoint = target.position;
        // 生涯の後に弾を破壊する
        Destroy(gameObject, lifeTime);
    }

    /// <summary>
    /// このインスタンスを更新します
    /// </summary>
    void FixedUpdate ()
    {
		counter += Time.fixedDeltaTime;
        // 加速を加える
        speed += Time.fixedDeltaTime * speedUpOverTime;
        if (target != null)
        {
            aimPoint = target.position;
        }
        // ファイアポイントから目的地までの距離を計算する
        Vector2 originDistance = aimPoint - originPoint;
        // 残りの距離を計算する
        Vector2 distanceToAim = aimPoint - (Vector2)myVirtualPosition;
        // 目標に向かって動く
        myVirtualPosition = Vector2.Lerp(originPoint, aimPoint, counter * speed / originDistance.magnitude);
        // 軌道にバリスティックオフセットを加える
        transform.position = AddBallisticOffset(originDistance.magnitude, distanceToAim.magnitude);
        // 弾丸を軌道に向かって回転させる
        LookAtDirection2D((Vector2)transform.position - myPreviousPosition);
        myPreviousPosition = transform.position;
        sprite.enabled = true;
        // ヒットするには十分に近い
        if (distanceToAim.magnitude <= hitDistance)
        {
            if (target != null)
            {
                // 弾丸が単一のターゲットにダメージを与える必要がある場合
                if (aoeDamageOnly == false)
				{
                    // ターゲットがダメージを受ける可能性がある場合
                    DamageTaker damageTaker = target.GetComponent<DamageTaker>();
					if (damageTaker != null)
					{
						damageTaker.TakeDamage(damage);
					}
				}
            }
            // 弾丸を破壊する
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 2dの方向を見ます
    /// </summary>
    /// <param name="direction">方向</param>
    private void LookAtDirection2D(Vector2 direction)
    {
        if (freezeRotation == false)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    /// <summary>
    /// 軌道にバリスティックオフセットを追加します
    /// </summary>
    /// <returns>バリスティックオフセット</returns>
    /// <param name="originDistance">原点距離</param>
    /// <param name="distanceToAim">目標までの距離</param>
    private Vector2 AddBallisticOffset(float originDistance, float distanceToAim)
    {
        if (ballisticOffset > 0f)
        {
            // 洞のオフセットを計算する
            float offset = Mathf.Sin(Mathf.PI * ((originDistance - distanceToAim) / originDistance));
            offset *= originDistance;
            // 軌道にオフセットを追加する
            return (Vector2)myVirtualPosition + (ballisticOffset * offset * Vector2.up);
        }
        else
        {
            return myVirtualPosition;
        }
    }
}
