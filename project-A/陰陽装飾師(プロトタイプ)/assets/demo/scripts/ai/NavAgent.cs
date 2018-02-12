/***
 * 
 *    Title: 移動および回転操作
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavAgent : MonoBehaviour
{
    // スピード（m / s）
    public float speed = 1f;
    // 移動可能
    [HideInInspector]
    public bool move = true;
    // ターニングできる
    [HideInInspector]
    public bool turn = true;
    // 目的地の位置
    [HideInInspector]
    public Vector2 destination;
    // 速度ベクトル
    [HideInInspector]
    public Vector2 velocity;

    // 最後のフレームの位置
    private Vector2 prevPosition;

    /// <summary>
    /// 有効イベントを発生させます
    /// </summary>
    void OnEnable()
    {
        prevPosition = transform.position;
    }

    /// <summary>
    /// このインスタンスを更新します
    /// </summary>
    void FixedUpdate()
    {
        // 移動が許可されている場合
        if (move == true)
        {
            // 目的地点に向かって移動する
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.fixedDeltaTime);
        }
        // 速度を計算する
        Vector2 velocity = (Vector2)transform.position - prevPosition;
		velocity /= Time.fixedDeltaTime;
        // 旋回が許可されている場合
        if (turn == true)
        {
            SetSpriteDirection(destination - (Vector2)transform.position);
        }
        // 最後の位置を保存
        prevPosition = transform.position;
    }

    /// <summary>
    /// スプライトの方向をx軸に設定します
    /// </summary>
    /// <param name="direction">方向</param>
    private void SetSpriteDirection(Vector2 direction)
    {
        if (direction.x > 0f && transform.localScale.x < 0f) // 右の方へ
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x < 0f && transform.localScale.x > 0f) // 左に
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    /// <summary>
    /// 方向を見ます
    /// </summary>
    /// <param name="direction">方向</param>
    public void LookAt(Vector2 direction)
    {
        SetSpriteDirection(direction);
    }

    /// <summary>
    /// ターゲットを見ます
    /// </summary>
    /// <param name="target">ターゲット</param>
    public void LookAt(Transform target)
    {
        SetSpriteDirection(target.position - transform.position);
    }
}
