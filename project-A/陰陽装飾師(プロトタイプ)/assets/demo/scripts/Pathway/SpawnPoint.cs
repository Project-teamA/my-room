/***
 * 
 *    Title: 敵のスポーナー
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnPoint : MonoBehaviour
{
    /// <summary>
    /// 敵の波の構造
    /// </summary>
    [System.Serializable]
    public class Wave
    {
        // ウェーブ実行前の遅延
        public float delayBeforeWave;
        // この波の敵のリスト
        public List<GameObject> enemies;
    }

    // 敵は指定された間隔で異なる速度を持つでしょう
    public float speedRandomizer = 0.2f;
    // 波の中で敵の間に遅れが生じる
    public float unitSpawnDelay = 0.8f;
    // このスポーナのための波のリスト
    public List<Wave> waves;
    // このリストはランダムな敵の発動に使用されます
    [HideInInspector]
	public List<GameObject> randomEnemiesList = new List<GameObject>();

    // 敵はこの経路に沿って移動する
    private Pathway path;
    // 遅延カウンタ
    private float counter;
    // アクティブなスポーンされた敵を持つバッファ
    private List<GameObject> activeEnemies = new List<GameObject>();
    // すべての敵が生まれました
    private bool finished = false;

    /// <summary>
    /// このインスタンスを目覚めさせる
    /// </summary>
    void Awake ()
    {
        path = GetComponentInParent<Pathway>();
        Debug.Assert(path != null, "Wrong initial parameters");
    }

    /// <summary>
    ///　有効イベントを発生させます
    /// </summary>
    void OnEnable()
    {
        EventManager.StartListening("UnitDie", UnitDie);
        EventManager.StartListening("WaveStart", WaveStart);
    }

    /// <summary>
    /// 無効イベントを発生させます
    /// </summary>
    void OnDisable()
    {
        EventManager.StopListening("UnitDie", UnitDie);
        EventManager.StopListening("WaveStart", WaveStart);
    }

    /// <summary>
    /// このインスタンスを更新します
    /// </summary>
    void Update()
    {
        // すべてのスポーンされた敵が死んでいる場合
        if ((finished == true) && (activeEnemies.Count <= 0))
        {
			EventManager.TriggerEvent("AllEnemiesAreDead", null, null);
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 波を実行します
    /// </summary>
    /// <returns>波</returns>
    private IEnumerator RunWave(int waveIdx)
    {
        if (waves.Count > waveIdx)
        {
            yield return new WaitForSeconds(waves[waveIdx].delayBeforeWave); 
            foreach (GameObject enemy in waves[waveIdx].enemies)
            {
                GameObject prefab = null;
                prefab = enemy;
                // 敵プレハブが指定されていない場合 - ランダムな敵を生み出す
                if (prefab == null && randomEnemiesList.Count > 0)
				{
					prefab = randomEnemiesList[Random.Range (0, randomEnemiesList.Count)];
				}
				if (prefab == null)
				{
					Debug.LogError("敵プレハブを持っていない。 レベルマネージャーまたはスポーンポイントで敵を指定してください");
				}
                // 敵を作る
                GameObject newEnemy = Instantiate(prefab, transform.position, transform.rotation);
                // パスウェイを設定する
                newEnemy.GetComponent<AiStatePatrol>().path = path;
                NavAgent agent = newEnemy.GetComponent<NavAgent>();
                // 速度オフセットを設定する
                agent.speed = Random.Range(agent.speed * (1f - speedRandomizer), agent.speed * (1f + speedRandomizer));
                // リストに敵を追加する
                activeEnemies.Add(newEnemy);
                // 次の敵が走るまでに待つ
                yield return new WaitForSeconds(unitSpawnDelay);
            }
            if (waveIdx + 1 == waves.Count)
            {
                finished = true;
            }
        }
    }

    /// <summary>
    /// ユニット死亡
    /// </summary>
    /// <param name="obj">オブジェクト</param>
    /// <param name="param">パラメート</param>
    private void UnitDie(GameObject obj, string param)
    {
        // これがアクティブな敵であれば
        if (activeEnemies.Contains(obj) == true)
        {
            // それをバッファから削除する
            activeEnemies.Remove(obj);
        }
    }

    // 受信した波形開始イベント
    private void WaveStart(GameObject obj, string param)
    {
        int waveIdx;
        int.TryParse(param, out waveIdx);
        StartCoroutine("RunWave", waveIdx);
    }

    /// <summary>
    /// 破壊イベントを発生させます
    /// </summary>
    void OnDestroy()
	{
		StopAllCoroutines();
	}
}
