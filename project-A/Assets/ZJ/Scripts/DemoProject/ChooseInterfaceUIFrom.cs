/***
 * 
 *    Title: 選択フォーム
 *   
 */
using System.Collections;
using System.Collections.Generic;
using SUIFW;
using UnityEngine;

namespace DemoProject
{
    public class ChooseInterfaceUIFrom : BaseUIForm
    {
        void Awake()
        {
            //フォームの性質
            CurrentUIType.UIForms_Type = UIFormType.PopUp;  //ポップアップフォーム
            CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Translucence;
            CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;

            //ボタンイベント：終了
            RigisterButtonObjectEvent("Btn_Close",
                P => CloseUIForm()
                );
            //ボタンイベント：ソファ
            RigisterButtonObjectEvent("BtnTicket",
                P =>
                {
                    //サブフォームを開く
                    OpenUIForm(ProConst.PRO_DETAIL_UIFORM);
                    //传递数据
                    string[] strArray = new string[] { "ソファ", "size" };
                    SendMessage("Props", "sofa", strArray);
                }
                );

            //ボタンイベント：テーブル 
            RigisterButtonObjectEvent("BtnShoe",
                P =>
                {
                    //サブフォームを開く
                    OpenUIForm(ProConst.PRO_DETAIL_UIFORM);
                    //传递数据
                    string[] strArray = new string[] { "テーブル", "size" };
                    SendMessage("Props", "table", strArray);
                }
                );

            //ボタンイベント：椅子
            RigisterButtonObjectEvent("BtnCloth",
                P =>
                {
                    //サブフォームを開く
                    OpenUIForm(ProConst.PRO_DETAIL_UIFORM);
                    //传递数据
                    string[] strArray = new string[] { "椅子", "size" };
                    SendMessage("Props", "chair", strArray);
                }
                );
        }

    }
}