/***
 * 
 *    Title: 精霊がクールダウンで新しいオブジェクトを生成できるようにします
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendersSpawner : MonoBehaviour
{
    // スポーン間のクールダウン
    public float cooldown = 10f;
    // バッファ内に生成されたオブジェクトの最大数
    public int maxNum = 2;
    // スポーンされたオブジェクトプレハブ
    public GameObject prefab;
    // 位置
    public Transform spawnPoint;

    // この精霊の防御ポイント
    private DefendPoint defPoint;
    //クールダウン計算のカウンター
    private float cooldownCounter;
    // スポーンされたオブジェクトを持つバッファ
    private Dictionary<GameObject, Transform> defendersList = new Dictionary<GameObject, Transform>();

    /// <summary>
    /// 有効イベントを発生させます
    /// </summary>
    void OnEnable()
    {
        EventManager.StartListening("UnitDie", UnitDie);
    }

    /// <summary>
    /// 無効イベントを発生させます
    /// </summary>
    void OnDisable()
    {
        EventManager.StopListening("UnitDie", UnitDie);
    }

    /// <summary>
    /// このインスタンスを開始します
    /// </summary>
    void Start()
	{
		Debug.Assert(spawnPoint, "間違った初期設定");
		BuildingPlace buildingPlace = GetComponentInParent<BuildingPlace>();
		defPoint = buildingPlace.GetComponentInChildren<DefendPoint>();
		cooldownCounter = cooldown;
        // 既存のすべてのディフェンダーをアップグレードする
        foreach (Transform point in defPoint.GetDefendPoints())
		{
            // 防御ポイントにすでに防御側がある場合
            AiBehavior defender = point.GetComponentInChildren<AiBehavior>();
			if (defender != null)
			{
                // 同じ場所に新しいディフェンダーを生み出す
                Spawn(defender.transform, point);
                // 古いディフェンダーを破壊する
                Destroy(defender.gameObject);
			}
		}
	}

    /// <summary>
    /// このインスタンスを更新します
    /// </summary>
    void FixedUpdate()
    {
		cooldownCounter += Time.fixedDeltaTime;
        if (cooldownCounter >= cooldown)
        {
            // クールダウンで新しいオブジェクトを生成しようとする
            if (TryToSpawn() == true)
            {
                cooldownCounter = 0f;
            }
            else
            {
                cooldownCounter = cooldown;
            }
        }
    }

    /// <summary>
    /// 可能であれば、フリーの防御位置を取得します
    /// </summary>
    /// <returns>フリーの防御位置</returns>
    /// <param name="index">インデックス.</param>
    private Transform GetFreeDefendPosition()
    {
        Transform res = null;
        List<Transform> points = defPoint.GetDefendPoints();
        foreach (Transform point in points)
        {
            // この点が既にビジーでない場合
            if (defendersList.ContainsValue(point) == false)
            {
                res = point;
                break;
            }
        }
        return res;
    }

    /// <summary>
    /// 新しいオブジェクトを生成しようとします
    /// </summary>
    /// <returns><c> true </ c>そうでない場合は<c> false </ c>を試していました</returns>
    private bool TryToSpawn()
    {
        bool res = false;
        // スポーンされたオブジェクトの数が最大許容数を下回る場合
        if ((prefab != null) && (defendersList.Count < maxNum))
        {
			Transform destination = GetFreeDefendPosition();
            // 自由な防御位置がある場合
            if (destination != null)
            {
                // 新しいディフェンダーを生み出す
                Spawn(spawnPoint, destination);
                res = true;
            }
        }
        return res;
    }

    /// <summary>
    /// 指定された位置と目的地にスポーンします
    /// </summary>
    /// <param name="position">ポジション</param>
    /// <param name="destination">行き先</param>
    private void Spawn(Transform position, Transform destination)
	{
        // 新しいオブジェクトを作成する
        GameObject obj = Instantiate<GameObject>(prefab, position.position, position.rotation);
        // ディフェンダーをディフェンディングポイントの子オブジェクトにする
        obj.transform.SetParent(destination);
        // 目的地の位置を設定する
        obj.GetComponent<AiStateMove>().destination = destination;
        // スポーンされたオブジェクトをバッファに追加する
        defendersList.Add(obj, destination);
	}

    /// <summary>
    /// ユニットのすべてのダイに上がる
    /// </summary>
    /// <param name="obj">オブジェクト</param>
    /// <param name="param">パラメータ</param>
    private void UnitDie(GameObject obj, string param)
    {
        // これが私のスポーンバッファからのオブジェクトの場合
        if (defendersList.ContainsKey(obj) == true)
        {
            // それをバッファから削除する
            defendersList.Remove(obj);
        }
    }
}
