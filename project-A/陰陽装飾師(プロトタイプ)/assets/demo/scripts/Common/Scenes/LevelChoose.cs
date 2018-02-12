/***
 * 
 *    Title: レベルシーンマネージャを選択
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelChoose : MonoBehaviour
{
    // 終了するシーン
    public string exitSceneName;
    // 選択されたレベル
    public GameObject currentLevel;
    // すべてのレベル
    public List<GameObject> levelsPrefabs = new List<GameObject>();
    // レベル数の視覚的な表示
    public Transform togglesFolder;
    // アクティブトグルプレハブ
    public Toggle activeTogglePrefab;
    // 非アクティブなトグルプレハブ
    public Toggle inactiveTogglePrefab;
    // 次のレベルのボタン
    public Button nextLevelButton;
    // 前のレベルのボタン
    public Button prevLevelButton;

    // 選択のために最後に許可されたレベルの索引
    private int maxActiveLevelIdx;
    // 現在表示されているレベルのインデックス
    private int currentDisplayedLevelIdx;
    // アクティブなトグルリスト
    private List<Toggle> activeToggles = new List<Toggle>();

    /// <summary>
    /// このインスタンスを目覚めさせる
    /// </summary>
    void Awake()
	{
		maxActiveLevelIdx = -1;
		Debug.Assert(currentLevel && togglesFolder && activeTogglePrefab && inactiveTogglePrefab && nextLevelButton && prevLevelButton, "Wrong initial settings");
	}

    void Start()
    {
        int hitIdx = -1;
		int levelsCount = DataManager.instance.progress.openedLevels.Count;
		if (levelsCount > 0)
		{
            // 保存されたデータから最後に開いたレベルの名前を取得する
            string openedLevelName = DataManager.instance.progress.openedLevels[levelsCount - 1];

	        int idx;
			for (idx = 0; idx < levelsPrefabs.Count; ++idx)
	        {
                // レベルリストで最後に開いたレベルを見つけよう
                if (levelsPrefabs[idx].name == openedLevelName)
	            {
	                hitIdx = idx;
	                break;
	            }
	        }
		}
        // レベルが見つかりました
        if (hitIdx >= 0)
		{
			if (levelsPrefabs.Count > hitIdx + 1)
			{
				maxActiveLevelIdx = hitIdx + 1;
			}
			else
			{
				maxActiveLevelIdx = hitIdx;
			}
		}
        // レベルが見つかりません
        else
        {
			if (levelsPrefabs.Count > 0)
			{
				maxActiveLevelIdx = 0;
			}
			else
			{
				Debug.LogError("Have no levels prefabs!");
			}
		}
		if (maxActiveLevelIdx >= 0)
		{
			DisplayToggles();
			//DisplayLevel(maxActiveLevelIdx);
		}
    }

    /// <summary>
    /// レベル数の視覚的な表示
    /// </summary>
    private void DisplayToggles()
	{
		foreach (Toggle toggle in togglesFolder.GetComponentsInChildren<Toggle>())
		{
			Destroy(toggle.gameObject);
		}
		int cnt;
		for (cnt = 0; cnt < maxActiveLevelIdx + 1; cnt++)
		{
			GameObject toggle = Instantiate(activeTogglePrefab.gameObject, togglesFolder);
			activeToggles.Add(toggle.GetComponent<Toggle>());
		}
		if (maxActiveLevelIdx < levelsPrefabs.Count - 1)
		{
			Instantiate(inactiveTogglePrefab.gameObject, togglesFolder);
		}
	}

    /// <summary>
    /// 選択したレベルを表示します
    /// </summary>
    /// <param name="levelIdx">レベルインデックス</param>
    private void DisplayLevel(int levelIdx)
	{
		Transform parentOfLevel = currentLevel.transform.parent;
		Vector3 levelPosition = currentLevel.transform.position;
		Quaternion levelRotation = currentLevel.transform.rotation;
		Destroy(currentLevel);
		currentLevel = Instantiate(levelsPrefabs[levelIdx], parentOfLevel);
		currentLevel.name = levelsPrefabs[levelIdx].name;
		currentLevel.transform.position = levelPosition;
		currentLevel.transform.rotation = levelRotation;
		currentDisplayedLevelIdx = levelIdx;
		foreach (Toggle toggle in activeToggles)
		{
			toggle.isOn = false;
		}
		activeToggles[levelIdx].isOn = true;
		UpdateButtonsVisible (levelIdx);
	}

    /// <summary>
    /// ボタンを更新します
    /// </summary>
    /// <param name="levelIdx">レベルインデックス</param>
    private void UpdateButtonsVisible(int levelIdx)
	{
		prevLevelButton.interactable = levelIdx > 0 ? true : false;
		nextLevelButton.interactable = levelIdx < maxActiveLevelIdx ? true : false;
	}

    /// <summary>
    /// 次のレベルを表示します
    /// </summary>
    public void DisplayNextLevel()
	{
		if (currentDisplayedLevelIdx < maxActiveLevelIdx)
		{
			DisplayLevel(currentDisplayedLevelIdx + 1);
		}
	}

    /// <summary>
    /// 前のレベルを表示します
    /// </summary>
    public void DisplayPrevLevel()
	{
		if (currentDisplayedLevelIdx > 0)
		{
			DisplayLevel (currentDisplayedLevelIdx - 1);
		}
	}

    /// <summary>
    /// シーンを終了します
    /// </summary>
    public void Exit()
	{
        SceneManager.LoadScene(exitSceneName);
	}

    /// <summary>
    /// 選択したレベルに移動します
    /// </summary>
    
    public void GoToLevel()
	{
        SceneManager.LoadScene(currentLevel.name);
	}
}
