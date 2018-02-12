/***
 * 
 *    Title: 現在の敵の波を表示するタイマー
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class WavesTimer : MonoBehaviour
{
    // 残りのTOの可視化
    public Image timeBar;
    // 現在の波のテキストフィールド
    public Text currentWaveText;
    // 最大波のテキストフィールド
    public Text maxWaveNumberText;
    // 強調表示されたタイマーの効果
    public GameObject highlightedFX;
    // 強調表示されたエフェクトの継続時間
    public float highlightedTO = 0.2f;

    // このゲームレベルのウェーブディスクリプタ
    private WavesInfo wavesInfo;
    // ウェーブリスト
    private List<float> waves = new List<float>();
    // 現在の波
    private int currentWave;
    // 次の波の前に
    private float currentTimeout;
    // タイムカウンタ
    private float counter;
    // タイマーが停止しました
    private bool finished;

    /// <summary>
    /// 無効イベントを発生させます
    /// </summary>
    void OnDisable()
	{
		StopAllCoroutines ();
	}

    void Awake()
    {
		wavesInfo = FindObjectOfType<WavesInfo>();
		Debug.Assert(timeBar && highlightedFX && wavesInfo && timeBar && currentWaveText && maxWaveNumberText, "Wrong initial settings");
    }

	void Start()
    {
		highlightedFX.SetActive(false);
		waves = wavesInfo.wavesTimeouts;
        currentWave = 0;
        counter = 0f;
        finished = false;
        GetCurrentWaveCounter();
        maxWaveNumberText.text = waves.Count.ToString();
        currentWaveText.text = "0";
	}

    /// <summary>
    /// このインスタンスを更新します
    /// </summary>
    void FixedUpdate()
    {
        if (finished == false)
        {
            // 期限切れ
            if (counter <= 0f)
            {
                // 次の波の開始についてのイベントを送信する
                EventManager.TriggerEvent("WaveStart", null, currentWave.ToString());
                currentWave++;
                currentWaveText.text = currentWave.ToString();
                // 短時間タイマーをハイライト表示する
                StartCoroutine("HighlightTimer");
                // すべての波が送信されます
                if (GetCurrentWaveCounter() == false)
                {
                    finished = true;
                    // タイマー停止に関するイベントを送信する
                    EventManager.TriggerEvent("TimerEnd", null, null);
                    return;
                }
            }
			counter -= Time.fixedDeltaTime;
            if (currentTimeout > 0f)
            {
                timeBar.fillAmount = counter / currentTimeout;
            }
            else
            {
                timeBar.fillAmount = 0f;
            }
        }
	}

    /// <summary>
    /// 現在のウェーブタイムアウトを取得します。
    /// </summary>
    /// <returns><c> true </ c>、現在のwaveタイムアウトが得られた場合は<c> false </ c>です</returns>
    private bool GetCurrentWaveCounter()
    {
        bool res = false;
        if (waves.Count > currentWave)
        {
            counter = currentTimeout = waves[currentWave];
            res = true;
        }
        return res;
    }

    /// <summary>
    /// タイマーコルーチンをハイライト表示します
    /// </summary>
    /// <returns>タイマー</returns>
    private IEnumerator HighlightTimer()
	{
		highlightedFX.SetActive(true);
		yield return new WaitForSeconds(highlightedTO);
		highlightedFX.SetActive(false);
	}

    /// <summary>
    /// 破壊イベントを発生させます
    /// </summary>
    void OnDestroy()
	{
		StopAllCoroutines();
	}
}
