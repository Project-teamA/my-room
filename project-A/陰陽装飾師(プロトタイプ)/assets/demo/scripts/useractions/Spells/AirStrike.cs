/***
 * 
 *    Title: 隕石、スターバーストなどの呪文
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirStrike : MonoBehaviour
{
    // FXの遅延
    public float[] delaysBeforeDamage = {0.5f};
    // ヒットごとのダメージ
    public int damage = 5;
    // ダメージ半径
    public float radius = 1f;
    // FXプレハブ
    public GameObject effectPrefab;
    // このタイムアウト後、FXは破壊されます
    public float effectDuration = 2f;

    // マシンの状態
    private enum MyState
	{
		WaitForClick,
		WaitForFX
	}
    // このインスタンスの現在の状態
    private MyState myState = MyState.WaitForClick;

    /// <summary>
    /// 有効イベントを発生させます
    /// </summary>
    void OnEnable()
	{
		EventManager.StartListening("UserClick", UserClick);
		EventManager.StartListening("UserUiClick", UserUiClick);
	}

    /// <summary>
    /// 無効イベントを発生させます
    /// </summary>
    void OnDisable()
	{
		EventManager.StopListening("UserClick", UserClick);
		EventManager.StopListening("UserUiClick", UserUiClick);
	}


	void Start()
	{
		Debug.Assert(effectPrefab, "Wrong initial settings");
	}

    /// <summary>
    /// ユーザクリックハンドラ
    /// </summary>
    /// <param name="obj">オブジェクト.</param>
    /// <param name="param">パラメータ.</param>
    private void UserClick(GameObject obj, string param)
	{
		if (myState == MyState.WaitForClick)
		{
            // 精霊でクリックされていない場合
            if (obj == null || obj.CompareTag("Tower") == false)
			{
                // FXを作成する
                transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
				GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
				Destroy(effect, effectDuration);
				EventManager.TriggerEvent("ActionStart", gameObject, null);
                // コミックを始める
                StartCoroutine(DamageCoroutine());
				myState = MyState.WaitForFX;
			}
            else //精霊でクリック
            {
				Destroy(gameObject);
			}
		}
	}

    /// <summary>
    /// ユーザUIクリックハンドラ
    /// </summary>
    /// <param name="obj">オブジェクト.</param>
    /// <param name="param">パラメータ.</param>
    private void UserUiClick(GameObject obj, string param)
	{
        //UIの代わりにゲームマップをクリックした場合
        if (myState == MyState.WaitForClick)
		{
			Destroy(gameObject);
		}
	}

    /// <summary>
    /// 遅延と量によって指定された損傷を行う
    /// </summary>
    /// <returns>コルーチン</returns>
    private IEnumerator DamageCoroutine()
	{
		foreach (float delay in delaysBeforeDamage)
		{
			yield return new WaitForSeconds(delay);

            // ターゲットを検索する
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
			foreach (Collider2D col in hits)
			{
				if (col.CompareTag("Enemy") == true)
				{
					DamageTaker damageTaker = col.GetComponent<DamageTaker>();
					if (damageTaker != null)
					{
						damageTaker.TakeDamage(damage);
					}
				}
			}
		}

		Destroy(gameObject);
	}

    /// <summary>
    /// 破壊イベントを発生させます
    /// </summary>
    void OnDestroy()
	{
		if (myState == MyState.WaitForClick)
		{
			EventManager.TriggerEvent("ActionCancel", gameObject, null);
		}
		StopAllCoroutines();
	}
}
