/***
 * 
 *    Title: 壁の作り
 *   
 */
using System.Collections;
using System.Collections.Generic;
using SUIFW;
using UnityEngine;
using UnityEngine.UI;

namespace DemoProject
{
    public class wallUIForm : BaseUIForm
    {
        public Text TxtwallName;                           //インターフェイス名
        public Text Btn_OK;                      //Button名

        public void Awake()
        {
            //フォームの性質を定義する（デフォルト値）
            base.CurrentUIType.UIForms_Type = UIFormType.Normal;
            base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;
            base.CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;
            /* ボタンにイベントをサインアップ */
            //RigisterButtonObjectEvent("Btn_OK", LogonSys);
            //ラムダ式言語
            RigisterButtonObjectEvent("Btn_OK", 
                p=>OpenUIForm(ProConst.Furniture_selection_UIFORM)
                );

        }


        public void Start()
        {
            //string strDisplayInfo = LauguageMgr.GetInstance().ShowText("LogonSystem");

            if (TxtwallName)
            {
                TxtwallName.text = Show("Wd_wall");
            }
            if (Btn_OK)
            {
                Btn_OK.text = Show("Btn_OK");
            }
        }

    }
}