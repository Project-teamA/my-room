/***
 * 
 *    Title: UIフォームのタイプ   
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW
{
	public class UIType {
        //スタックコレクションをclear
        public bool IsClearStack = false;
        //UIフォームのタイプ
        public UIFormType UIForms_Type = UIFormType.Normal;
        //UIフォームの表示タイプ
        public UIFormShowMode UIForms_ShowMode = UIFormShowMode.Normal;
        //UIフォームの透明度タイプ
        public UIFormLucenyType UIForm_LucencyType = UIFormLucenyType.Lucency;

	}
}