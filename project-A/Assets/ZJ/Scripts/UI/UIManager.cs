/***
 * 
 *    Title: UI管理 
 *    Description: UI全体の中核
 *     
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW
{
	public class UIManager : MonoBehaviour {
        /* フィールド */
        private static UIManager _Instance = null;
        //UIフォームのデフォルトパス（パラメータ1：フォームのデフォルト名、2：フォームのデフォルトパス）
        private Dictionary<string, string> _DicFormsPaths;
        //すべてのUIフォームをキャッシュする
        private Dictionary<string, BaseUIForm> _DicALLUIForms;
        //現在表示されているUIフォーム
        private Dictionary<string, BaseUIForm> _DicCurrentShowUIForms;
        //すべての[Inverted]フォームの現在のフォームを格納する "Stack"コレクションを定義する
        private Stack<BaseUIForm> _StaCurrentUIForms;
        //UIルートノード
        private Transform _TraCanvasTransfrom = null;
        //ノードの全画面表示
        private Transform _TraNormal = null;
        //固定表示ノード
        private Transform _TraFixed = null;
        //ポップアップノード
        private Transform _TraPopUp = null;
        //UI管理スクリプトのノード
        private Transform _TraUIScripts = null;


        /// <summary>
        /// 例を得る
        /// </summary>
        /// <returns></returns>
	    public static UIManager GetInstance()
	    {
	        if (_Instance==null)
	        {
	            _Instance = new GameObject("_UIManager").AddComponent<UIManager>();
	        }
	        return _Instance;
	    }

        //コアデータを初期化し、 "UIフォームパス"をコレクションにロードします。
        public void Awake()
	    {
            //フィールドの初期化
            _DicALLUIForms = new Dictionary<string, BaseUIForm>();
            _DicCurrentShowUIForms=new Dictionary<string, BaseUIForm>();
            _DicFormsPaths=new Dictionary<string, string>();
            _StaCurrentUIForms = new Stack<BaseUIForm>();
            //ロードを初期化する（ルートUIフォーム）キャンバスのデフォルト
            InitRootCanvasLoading();
            //UIルートノードを取得する、フルスクリーンノード、固定ノード、ポップアップノード
            _TraCanvasTransfrom = GameObject.FindGameObjectWithTag(SysDefine.SYS_TAG_CANVAS).transform;
	        _TraNormal = UnityHelper.FindTheChildNode(_TraCanvasTransfrom.gameObject, SysDefine.SYS_NORMAL_NODE);
            _TraFixed = UnityHelper.FindTheChildNode(_TraCanvasTransfrom.gameObject, SysDefine.SYS_FIXED_NODE);
            _TraPopUp = UnityHelper.FindTheChildNode(_TraCanvasTransfrom.gameObject, SysDefine.SYS_POPUP_NODE);
            _TraUIScripts = UnityHelper.FindTheChildNode(_TraCanvasTransfrom.gameObject,SysDefine.SYS_SCRIPTMANAGER_NODE);

            //このスクリプトを「ルートUIフォーム」の子として考えてください。
            this.gameObject.transform.SetParent(_TraUIScripts, false);
            //シーンの遷移中に「ルートUIフォーム」を破棄することはできません
            DontDestroyOnLoad(_TraCanvasTransfrom);
            //UIフォームのプリセットパスデータを初期化する
            InitUIFormsPathData();
	    }

        /// <summary>
        /// UIフォームを表示（開く）
        ///機能：
        /// 1：UIフォームの名前に従って、「すべてのUIフォーム」キャッシュコレクションにロードされます
        /// 2：異なるUIフォームの "表示モード"に応じて、それぞれ異なるローディングプロセス
        /// </summary>
        /// <param name="uiFormName">UIフォームのデフォルト名</param>
        public void ShowUIForms(string uiFormName)
        {
            BaseUIForm baseUIForms=null;                    //UIフォーム基本クラス

            //パラメータを確認する
            if (string.IsNullOrEmpty(uiFormName)) return;

            //UIフォームの名前に従って、「すべてのUIフォーム」キャッシュコレクションにロードされます
            baseUIForms = LoadFormsToAllUIFormsCatch(uiFormName);
            if (baseUIForms == null) return;

            //データ内の「スタックセット」をクリアするかどうか
            if (baseUIForms.CurrentUIType.IsClearStack)
            {
                ClearStackArray();
            }

            //異なるUIフォームの表示モードに応じて、それぞれ異なるローディングプロセス
            switch (baseUIForms.CurrentUIType.UIForms_ShowMode)
            {                    
                case UIFormShowMode.Normal:                 //標準表示ウィンドウモード
                    //現在のフォームを現在のフォームコレクションに読み込みます。
                    LoadUIToCurrentCache(uiFormName);
                    break;
                case UIFormShowMode.ReverseChange:          //「リバーススイッチ」ウィンドウモードが必要です
                    PushUIFormToStack(uiFormName);
                    break;
                case UIFormShowMode.HideOther:              //他隠すウィンドウモード
                    EnterUIFormsAndHideOther(uiFormName);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 閉じるフォーム（前のフォームに戻る）
        /// </summary>
        /// <param name="uiFormName"></param>
        public void CloseUIForms(string uiFormName)
        { 
            BaseUIForm baseUiForm;                          //フォーム基本クラス

            //パラメータチェック
            if (string.IsNullOrEmpty(uiFormName)) return;
            //"すべてのUIフォーム"コレクション、レコードがない場合は、直接戻ります
            _DicALLUIForms.TryGetValue(uiFormName,out baseUiForm);
            if(baseUiForm==null ) return;
            //異なるUIフォームの表示モードに応じて、それぞれ異なる処理
            switch (baseUiForm.CurrentUIType.UIForms_ShowMode)
	        {
                case UIFormShowMode.Normal:
                    //通常のフォームを閉じる
                    ExitUIForms(uiFormName);
                    break;
                case UIFormShowMode.ReverseChange:
                    //フォームを元に戻す
                    PopUIFroms();
                    break;
                case UIFormShowMode.HideOther:
                    //他のフォームを非表示にする
                    ExitUIFormsAndDisplayOther(uiFormName);
                    break;

		        default:
                    break;
	        }
        }

        #region  "UIマネージャ"の内部コアデータを表示、テスト使用

        /// <summary>
        /// "すべてのUIフォーム"コレクションの数を表示します。
        /// </summary>
        /// <returns></returns>
        public int ShowALLUIFormCount()
        {
            if (_DicALLUIForms != null)
            {
                return _DicALLUIForms.Count;
            }
            else {
                return 0;
            }   
        }

        /// <summary>
        /// "現在のフォーム"コレクションの数を表示します。
        /// </summary>
        /// <returns></returns>
        public int ShowCurrentUIFormsCount()
        {
            if (_DicCurrentShowUIForms != null)
            {
                return _DicCurrentShowUIForms.Count;
            }
            else
            {
                return 0;
            }           
        }

        /// <summary>
        /// 現在のスタックコレクションのフォーム数を表示します。
        /// </summary>
        /// <returns></returns>
        public int ShowCurrentStackUIFormsCount()
        {
            if (_StaCurrentUIForms != null)
            {
                return _StaCurrentUIForms.Count;
            }
            else
            {
                return 0;
            }           
        }

        #endregion

        #region 方法
        //ロードを初期化する（ルートUIフォーム）キャンバスのデフォルト
        private void InitRootCanvasLoading()
	    {
	        ResourcesMgr.GetInstance().LoadAsset(SysDefine.SYS_PATH_CANVAS, false);
	    }

        /// <summary>
        /// UIフォームの名前に従って、「すべてのUIフォーム」キャッシュコレクションにロードされます
        /// 機能：「すべてのUIフォーム」コレクションがロードされたかをチェックします。
        /// </summary>
        /// <param name="uiFormsName">UIフォーム（デフォルト）の名前</param>
        /// <returns></returns>
	    private BaseUIForm LoadFormsToAllUIFormsCatch(string uiFormsName)
	    {
	        BaseUIForm baseUIResult = null;                 //ロードされたリターンUIフォーム基本クラス

            _DicALLUIForms.TryGetValue(uiFormsName, out baseUIResult);
            if (baseUIResult==null)
	        {
                //指定された「UIフォーム」をロードします。
                baseUIResult = LoadUIForm(uiFormsName);
	        }

	        return baseUIResult;
	    }

        /// <summary>
        /// 指定された「UIフォーム」をロードします
        ///機能：
        /// 1： "UIフォーム名"に従って、プリセットクローン本体をロードします。
        /// 2：プリセットクローンの異なるスクリプトの異なる「位置情報」に従って、「ルートフォーム」の下に異なるノードをロードします。
        /// 3：新しく作成されたUIクローンを非表示にします。
        /// 4：クローンを「すべてのUIフォーム」（キャッシュ）コレクションに追加します。
        /// 
        /// </summary>
        /// <param name="uiFormName">UIフォーム名</param>
        private BaseUIForm LoadUIForm(string uiFormName)
        {
            string strUIFormPaths = null;                   //UIフォームパス
            GameObject goCloneUIPrefabs = null;             //作成されたUIクローンプリセット
            BaseUIForm baseUiForm=null;                     //フォーム基本クラス


            //UIフォーム名に従って、対応するロード・パスを取得します。
            _DicFormsPaths.TryGetValue(uiFormName, out strUIFormPaths);
            //「UIフォーム名」に従って、「プリセットクローン本体」を読み込み、
            if (!string.IsNullOrEmpty(strUIFormPaths))
            {
                goCloneUIPrefabs = ResourcesMgr.GetInstance().LoadAsset(strUIFormPaths, false);
            }
            //"UI Clone"の親ノードを設定する（クローン内のスクリプトの異なる "Location Information"に基づいて）
            if (_TraCanvasTransfrom != null && goCloneUIPrefabs != null)
            {
                baseUiForm = goCloneUIPrefabs.GetComponent<BaseUIForm>();
                if (baseUiForm == null)
                {
                    Debug.Log("baseUiForm==null! ,baseUIFormのサブクラススクリプトがフォームのプリセットオブジェクトにロードされているかどうか確認してください！ パラメータ uiFormName=" + uiFormName);
                    return null;
                }
                switch (baseUiForm.CurrentUIType.UIForms_Type)
                {
                    case UIFormType.Normal:                 //ノーマルフォームノード
                        goCloneUIPrefabs.transform.SetParent(_TraNormal, false);
                        break;
                    case UIFormType.Fixed:                  //固定フォームノード
                        goCloneUIPrefabs.transform.SetParent(_TraFixed, false);
                        break;
                    case UIFormType.PopUp:                  //ポップアップノード
                        goCloneUIPrefabs.transform.SetParent(_TraPopUp, false);
                        break;
                    default:
                        break;
                }

                //隠す設定
                goCloneUIPrefabs.SetActive(false);
                //クローンを「すべてのUIフォーム」コレクションに追加します。
                _DicALLUIForms.Add(uiFormName, baseUiForm);
                return baseUiForm;
            }
            else
            {
                Debug.Log("_TraCanvasTransfrom==null Or goCloneUIPrefabs==null!! ,Plese Check!, パラメータuiFormName=" + uiFormName); 
            }

            Debug.Log("予期しないエラーが発生しました。パラメータを確認してください uiFormName=" + uiFormName);
            return null;
        }//Mehtod_end

        /// <summary>
        /// 現在のフォームを現在のフォームコレクションに読み込みます。
        /// </summary>
        /// <param name="uiFormName">フォームのデフォルト名</param>
	    private void LoadUIToCurrentCache(string uiFormName)
	    {
	        BaseUIForm baseUiForm;                          //UIフォーム基本クラス
            BaseUIForm baseUIFormFromAllCache;              //「すべてのフォームコレクション」から取得するフォーム

            //「表示」コレクションにUIフォーム全体がある場合は、直接返す
            _DicCurrentShowUIForms.TryGetValue(uiFormName, out baseUiForm);
	        if (baseUiForm != null) return;
            //現在のフォームを「表示中」のコレクションに読み込みます
            _DicALLUIForms.TryGetValue(uiFormName, out baseUIFormFromAllCache);
            if (baseUIFormFromAllCache!=null)
	        {
                _DicCurrentShowUIForms.Add(uiFormName, baseUIFormFromAllCache);
                baseUIFormFromAllCache.Display();           //現在のフォームを表示する
            }
	    }

        /// <summary>
        /// UIフォームをスタックに挿入する
        /// </summary>
        /// <param name="uiFormName">フォームの名前</param>
        private void PushUIFormToStack(string uiFormName)
        { 
            BaseUIForm baseUIForm;                          //UIフォーム

            //ジャッジ "スタック"コレクション、他のフォームがあるかどうか、あったら "凍結"処理があります。
            if (_StaCurrentUIForms.Count>0)
            {
                BaseUIForm topUIForm=_StaCurrentUIForms.Peek();
                //凍結処理
                topUIForm.Freeze();
            }
            //ジャッジ "UI All Forms"コレクションには、指定されたUIフォームがあるかどうか、処理があります。
            _DicALLUIForms.TryGetValue(uiFormName, out baseUIForm);
            if (baseUIForm!=null)
            {
                //現在のウィンドウにステータスの表示状態
                baseUIForm.Display();
                //指定されたUIフォームをスタック操作に配置します。
                _StaCurrentUIForms.Push(baseUIForm);
            }else{
                Debug.Log("baseUIForm==null,Please Check, パラメータ uiFormName=" + uiFormName);
            }
        }

        /// <summary>
        /// 指定されたUIフォームを終了する
        /// </summary>
        /// <param name="strUIFormName"></param>
        private void ExitUIForms(string strUIFormName)
        { 
            BaseUIForm baseUIForm;                          //フォーム基本クラス

            //「コレクションの表示」にレコードがない場合は、それが直接返されます。
            _DicCurrentShowUIForms.TryGetValue(strUIFormName, out baseUIForm);
            if(baseUIForm==null) return ;
            //指定したフォームを非表示としてマークされ、表示コレクションから削除する。
            baseUIForm.Hiding();
            _DicCurrentShowUIForms.Remove(strUIFormName);
        }

        //（「リバーススイッチ」属性）フォームのポップアップロジック
        private void PopUIFroms()
        { 
            if(_StaCurrentUIForms.Count>=2)
            {
                //スタック処理
                BaseUIForm topUIForms = _StaCurrentUIForms.Pop();
                //隠す処理
                topUIForms.Hiding();
                //スタックの後、次のフォームは "再表示"処理を行います。
                BaseUIForm nextUIForms = _StaCurrentUIForms.Peek();
                nextUIForms.Redisplay();
            }
            else if (_StaCurrentUIForms.Count ==1)
            {
                //スタック処理
                BaseUIForm topUIForms = _StaCurrentUIForms.Pop();
                //隠す処理
                topUIForms.Hiding();
            }
        }

        /// <summary>
        /// （「その他のプロパティを隠す」）フォームを開き、他のフォームを非表示にします
        /// </summary>
        /// <param name="strUIName">指定したフォームを開く</param>
        private void EnterUIFormsAndHideOther(string strUIName)
        { 
            BaseUIForm baseUIForm;                          //UIフォーム基本クラス
            BaseUIForm baseUIFormFromALL;                   //コレクションからのUIフォームの基本クラス


            //パラメータチェック
            if (string.IsNullOrEmpty(strUIName)) return;

            _DicCurrentShowUIForms.TryGetValue(strUIName, out baseUIForm);
            if (baseUIForm != null) return;

            //コレクションとスタックを表示中のすべてのフォームを非表示にします。
            foreach (BaseUIForm baseUI in _DicCurrentShowUIForms.Values)
            {
                baseUI.Hiding();
            }
            foreach (BaseUIForm staUI in _StaCurrentUIForms)
            {
                staUI.Hiding();
            }

            //現在のフォームを「フォームの表示」コレクションに追加し、表示処理をします。
            _DicALLUIForms.TryGetValue(strUIName, out baseUIFormFromALL);
            if (baseUIFormFromALL!=null)
            {
                _DicCurrentShowUIForms.Add(strUIName, baseUIFormFromALL);
                //フォームが表示
                baseUIFormFromALL.Display();
            }
        }

        /// <summary>
        /// （「その他のプロパティを隠す」）フォームを開き、他のフォームを表示にします
        /// </summary>
        /// <param name="strUIName">指定したフォームを開</param>
        private void ExitUIFormsAndDisplayOther(string strUIName)
        {
            BaseUIForm baseUIForm;                          //UIフォーム基本クラス


            //パラメータチェック
            if (string.IsNullOrEmpty(strUIName)) return;

            _DicCurrentShowUIForms.TryGetValue(strUIName, out baseUIForm);
            if (baseUIForm == null) return;

            //現在のフォームが非表示になり、 "表示中"のコレクションがこのフォームがあれば削除されます
            baseUIForm.Hiding();
            _DicCurrentShowUIForms.Remove(strUIName);

            //すべてのフォームを表示
            foreach (BaseUIForm baseUI in _DicCurrentShowUIForms.Values)
            {
                baseUI.Redisplay();
            }
            foreach (BaseUIForm staUI in _StaCurrentUIForms)
            {
                staUI.Redisplay();
            }
        }

        /// <summary>
        /// データ内の「スタックセット」をクリアするかどうか
        /// </summary>
        /// <returns></returns>
        private bool ClearStackArray()
        {
            if (_StaCurrentUIForms != null && _StaCurrentUIForms.Count>=1)
            {
                //クリアする
                _StaCurrentUIForms.Clear();
                return true;
            }

            return false;
        }

        /// <summary>
        /// UIフォームのプリセットパスデータを初期化する
        /// </summary>
	    private void InitUIFormsPathData()
	    {
            IConfigManager configMgr = new ConfigManagerByJson(SysDefine.SYS_PATH_UIFORMS_CONFIG_INFO);
            if (configMgr!=null)
            {
                _DicFormsPaths = configMgr.AppSetting;
            }
	    }

	    #endregion

    }//class_end
}