/***
 * 
 *    Title: 特定の選択肢
 *    
 */
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using SUIFW;
using UnityEngine;
using UnityEngine.UI;

namespace DemoProject
{
	public class PropDetailUIForm : BaseUIForm
	{
	    public Text TxtName;                                //フォームの表示名

        void Awake () 
        {
            //フォームの性質
            CurrentUIType.UIForms_Type = UIFormType.PopUp;
		    CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;
		    CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Translucence;

            /* ボタンイベント  */
            RigisterButtonObjectEvent("BtnClose",
                p=>CloseUIForm()
                );

            /*  メッセージを受け入れる   */
            ReceiveMessage("Props", 
                p =>
                {
                    if (TxtName)
                    {
                        string[] strArray = p.Values as string[];
                        TxtName.text = strArray[0];
                        //print("size： "+strArray[1]);
                    }
                }
           );

        }//Awake_end
		
	}
}