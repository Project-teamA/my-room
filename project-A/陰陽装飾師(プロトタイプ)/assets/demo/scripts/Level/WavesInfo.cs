/***
 * 
 *    Title: 敵は時間を振る
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WavesInfo : MonoBehaviour
{
    // デフォルトで波の間に
    public float defaultWaveTimeout = 10f;
    //　波の間のTO
    public List<float> wavesTimeouts = new List<float>();

    // 編集モードでのみ実行
#if UNITY_EDITOR

    // アクティブなスポーナーとレベルのリスト
    private SpawnPoint[] spawners;

	void Start()
	{
		spawners = FindObjectsOfType<SpawnPoint>();
	}

	void Update()
	{
		int wavesCount = 0;
        // スポンサーからの波の最大数を取得する
        foreach (SpawnPoint spawner in spawners)
		{
			if (spawner.waves.Count > wavesCount)
			{
				wavesCount = spawner.waves.Count;
			}
		}
        // 波のタイムアウトで実際のリストを表示する
        if (wavesTimeouts.Count < wavesCount)
		{
			int i;
			for (i = wavesTimeouts.Count; i < wavesCount; ++i)
			{
				wavesTimeouts.Add (defaultWaveTimeout);
			}
		}
		else if (wavesTimeouts.Count > wavesCount)
		{
			wavesTimeouts.RemoveRange (wavesCount, wavesTimeouts.Count - wavesCount);
		}
	}

	#endif
}
