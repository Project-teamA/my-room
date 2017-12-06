/***
 * 
 *    Title:  UIの主体
 *           
 *    Description:　全てUIを定義している
 *                     
 *           1：Display 表示状態
 *           2：Hiding 隠す状態
 *           3：ReDisplay 再表示状態
 *           4：Freeze 凍結状態  
 */
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

namespace SUIFW
{
	public class BaseUIForm : MonoBehaviour {
        /*　*/
        private UIType _CurrentUIType=new UIType();

        /* */
        //今のUIフォームのタイプ
        public UIType CurrentUIType
	    {
	        get { return _CurrentUIType; }
	        set { _CurrentUIType = value; }
	    }


        #region  四種マドの状態

        /// <summary>
        /// 表示状態
        /// </summary>
	    public virtual void Display()
	    {
	        this.gameObject.SetActive(true);
            //モーダルフォーム呼び出しを設定する(ポップアップ形式で)
            if (_CurrentUIType.UIForms_Type==UIFormType.PopUp)
            {
                UIMaskMgr.GetInstance().SetMaskWindow(this.gameObject,_CurrentUIType.UIForm_LucencyType);
            }
	    }

        /// <summary>
        /// 隠す状態
        /// </summary>
	    public virtual void Hiding()
	    {
            this.gameObject.SetActive(false);
            //モーダルフォーム呼び出しをキャンセルする
            if (_CurrentUIType.UIForms_Type == UIFormType.PopUp)
            {
                UIMaskMgr.GetInstance().CancelMaskWindow();
            }
        }

        /// <summary>
        /// 再表示状態
        /// </summary>
	    public virtual void Redisplay()
	    {
            this.gameObject.SetActive(true);
            //モーダルフォーム呼び出しを設定する(ポップアップ形式で)
            if (_CurrentUIType.UIForms_Type == UIFormType.PopUp)
            {
                UIMaskMgr.GetInstance().SetMaskWindow(this.gameObject, _CurrentUIType.UIForm_LucencyType);
            }
        }

        /// <summary>
        /// 凍結状態 
        /// </summary>
	    public virtual void Freeze()
	    {
            this.gameObject.SetActive(true);
        }


        #endregion

        #region 普段での処理

        /// <summary>
        /// ボタンの処理
        /// </summary>
        /// <param name="buttonName">ボタンの名前</param>
        /// <param name="delHandle">処理方法</param>
	    protected void RigisterButtonObjectEvent(string buttonName,EventTriggerListener.VoidDelegate  delHandle)
	    {
            GameObject goButton = UnityHelper.FindTheChildNode(this.gameObject, buttonName).gameObject;
            //ボタンの処理方法を入力
            if (goButton != null)
            {
                EventTriggerListener.Get(goButton).onClick = delHandle;
            }	    
        }

        /// <summary>
        /// UIフォームを開く
        /// </summary>
        /// <param name="uiFormName"></param>
	    protected void OpenUIForm(string uiFormName)
	    {
            UIManager.GetInstance().ShowUIForms(uiFormName);
        }

        /// <summary>
        /// 現在のUIフォームを閉じる
        /// </summary>
	    protected void CloseUIForm()
	    {
	        string strUIFromName = string.Empty;            //処理した後のUIFrom 名前
	        int intPosition = -1;

            strUIFromName=GetType().ToString();             //名前+タイプ
            intPosition=strUIFromName.IndexOf('.');
            if (intPosition!=-1)
            {
                //“.”の部分を取リ切る
                strUIFromName = strUIFromName.Substring(intPosition + 1);
            }

            UIManager.GetInstance().CloseUIForms(strUIFromName);
        }

        /// <summary>
        /// メッセージを送信する
        /// </summary>
        /// <param name="msgType">メッセージのタイプ</param>
        /// <param name="msgName">メッセージの名前</param>
        /// <param name="msgContent">メッセージの内容</param>
	    protected void SendMessage(string msgType,string msgName,object msgContent)
	    {
            KeyValuesUpdate kvs = new KeyValuesUpdate(msgName,msgContent);
            MessageCenter.SendMessage(msgType, kvs);	    
        }

        /// <summary>
        /// メッセージを受け取る
        /// </summary>
        /// <param name="messagType">メッセージのタイプ</param>
        /// <param name="handler">メッセージ</param>
	    public void ReceiveMessage(string messagType,MessageCenter.DelMessageDelivery handler)
	    {
            MessageCenter.AddMsgListener(messagType, handler);
	    }

        /// <summary>
        /// 表示言語
        /// </summary>
        /// <param name="id"></param>
	    public string Show(string id)
        {
            string strResult = string.Empty;

            strResult = LauguageMgr.GetInstance().ShowText(id);
            return strResult;
        }

	    #endregion

    }
}