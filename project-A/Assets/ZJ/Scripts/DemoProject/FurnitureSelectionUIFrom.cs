/***
 * 
 *    Title: 家具の選択
 *   
 */
using System.Collections;
using System.Collections.Generic;
using SUIFW;
using UnityEngine;
using UnityEngine.UI;

namespace DemoProject
{
    public class FurnitureSelectionUIFrom : BaseUIForm
    {
        public Text TxtFSName;                           //インターフェイス名
        public Text Btn_OK;                      //Button名
        public Text Btn_return;                  //Button名
        public Text Btn_select;                  //Button名 

        public void Awake()
        {
            //フォームの性質
            CurrentUIType.UIForms_ShowMode = UIFormShowMode.HideOther;

            /* ボタンにイベントをサインアップ */
            RigisterButtonObjectEvent("Btn_OK",
                p => OpenUIForm(ProConst.ThreeD_UIFORM)
                );
            RigisterButtonObjectEvent("Btn_return",
                p => OpenUIForm(ProConst.WALL_FROMS)
                );
            RigisterButtonObjectEvent("Btn_select",
                p => OpenUIForm(ProConst.ChooseInterface_UIFrom)
                );
        }
    }
}
