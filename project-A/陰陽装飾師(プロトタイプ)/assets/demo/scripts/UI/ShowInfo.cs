/***
 * 
 *    Title: 特別シートにユニット情報を表示する
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInfo : MonoBehaviour
{
    // ユニット名
    public Text unitName;
    // 表示するためのプライマリアイコン
    public Image primaryIcon;
    // 表示する主なテキスト
    public Text primaryText;
    // 表示用のセカンダリアイコン
    public Image secondaryIcon;
    // 表示用のセカンダリテキスト
    public Text secondaryText;

    /// <summary>
    /// 破壊イベントを発生させます
    /// </summary>
    void OnDestroy()
    {
		EventManager.StopListening("UserClick", UserClick);
    }

    void Awake()
    {
        Debug.Assert(unitName && primaryIcon && primaryText && secondaryIcon && secondaryText, "Wrong intial settings");
    }

    void Start()
    {
		EventManager.StartListening("UserClick", UserClick);
        HideUnitInfo();
    }

    /// <summary>
    /// ユニット情報を表示します
    /// </summary>
    /// <param name="info">Info.</param>
    public void ShowUnitInfo(UnitInfo info)
    {
		gameObject.SetActive(true);
        unitName.text = info.unitName;
        primaryText.text = info.primaryText;
        secondaryText.text = info.secondaryText;
        if (info.primaryIcon != null)
        {
            primaryIcon.sprite = info.primaryIcon;
            primaryIcon.gameObject.SetActive(true);
        }
        if (info.secondaryIcon != null)
        {
            secondaryIcon.sprite = info.secondaryIcon;
            secondaryIcon.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// ユニット情報を非表示にします
    /// </summary>
    public void HideUnitInfo()
    {
        unitName.text = primaryText.text = secondaryText.text = "";
        primaryIcon.gameObject.SetActive(false);
        secondaryIcon.gameObject.SetActive(false);
		gameObject.SetActive(false);
    }

    /// <summary>
    /// ユーザクリックハンドラ
    /// </summary>
    /// <param name="obj">オブジェクト</param>
    /// <param name="param">パラメート</param>
    private void UserClick(GameObject obj, string param)
    {
        HideUnitInfo();
        if (obj != null)
        {
            // クリックされたオブジェクトには表示する情報があります
            UnitInfo unitInfo = obj.GetComponent<UnitInfo>();
            if (unitInfo != null)
            {
                ShowUnitInfo(unitInfo);
            }
        }
    }
}
