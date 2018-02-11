//FurnitureManagement.cs(家具グリッドを出したり消したり回転させたり移動させたりの一括管理)
//
//
// 2017年2月1日 更新(菅原涼太)
// 衝突のエラーフラグを出力する関数の実装
// 家具の最大数定数，家具最大数を出力する関数の実装

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureManagement : MonoBehaviour
{
    public enum TransformMode { Translate, Rotate }; //(Translate = 移動モード，Rotate = 回転モード)

    private int target_number_ = -1; //クリックまたはドラッグされている家具グリッドをもったクラスのナンバー 何もクリックしていない場合-1(アクセス不可)
    private Vector3 screen_point_; //オブジェクトのスクリーン上での位置 (アクセス不可)
    private Vector3 point_offset_; //オブジェクト中心とのオフセット (アクセス不可)
    private List<FurnitureGrid> furniture_grid_ = new List<FurnitureGrid>(); //FurnitureGrid.csで実装されているクラスのリスト(最大50)
    private int object_number_ = 0; //FurnitureGridクラス生成回数
    private TransformMode mode_ = TransformMode.Translate; //現在のモード(初期モードは移動モード)

    private const int MaxFurnitureNum_ = 50; //家具の最大数(定数)

    //インスタンス化されているFurnitureGridクラスをすべて取得
    public List<FurnitureGrid> furniture_grid_all()
    {
        return furniture_grid_;
    }

    //インスタンス化されているFurnitureGridクラスを配列番号指定で取得
    public FurnitureGrid furniture_grid(int number)
    {
        if (number > -1 && number < furniture_grid_.Count)
        {
            return furniture_grid_[number];
        }
        return null;
    }

    //インスタンス化されているFurnitureGridクラスをオブジェクトネームで取得
    public FurnitureGrid furniture_grid(string name)
    {
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            if (furniture_grid_[i].furniture_grid().name == name)
            {
                return furniture_grid_[i];
            }
        }
        return null;
    }

    //家具の総数(消されたもの含む)を取得
    public int object_number()
    {
        return object_number_;
    }

    //現在のモード(移動モードor回転モード)を取得
    public TransformMode mode()
    {
        return mode_;
    }

    //家具の最大数を取得
    public int MaxFurnitureNum()
    {
        return MaxFurnitureNum_;
    }

    //クリックされたオブジェクトを取得
    private GameObject ClickObject()
    {
        GameObject clicked_object = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit))
        {
            clicked_object = hit.collider.gameObject;
        }
        return clicked_object;
    }


    //ここからインターフェースで呼び出されるであろう関数群***********************************************************************************************************************************************************

    //移動モードに変更
    public void ChangeTranslate()
    {
        mode_ = TransformMode.Translate;
    }

    //回転モードに変更
    public void ChangeRotate()
    {
        mode_ = TransformMode.Rotate;
    }

    //エラー(衝突)の名称判定
    //
    //戻り値
    // true = 衝突関連のエラー在り，false = 衝突関連のエラー無し
    public bool CollisionError()
    {
        bool error_flag = false;
        for(int i = 0; i < furniture_grid_.Count; ++i)
        {
            if(furniture_grid_[i] .furniture_grid().GetComponent<GridError>().errored())
            {
                error_flag = true;
                break;
            }
        }
        return error_flag;
    }

    //値を代入して家具グリッドを追加(基本的に外部で呼び出される)
    public void AddFurniture(int category_ID , int furniture_ID)
    {
        //ここでも50までで制限しているが，本来は最大数判定を外側でやる．
        if (furniture_grid_.Count < MaxFurnitureNum() )
        {
            object_number_ += 1;
            string object_name = "furniture_grid_" + object_number_.ToString();

            furniture_grid_.Add(gameObject.AddComponent<FurnitureGrid>());
            furniture_grid_[furniture_grid_.Count - 1].Init(category_ID, furniture_ID, object_name);
        }
    }

    //番号を指定して家具グリッドを削除
    public void RemoveFurniture(int target_number)
    {
        if (target_number > -1 && target_number < furniture_grid_.Count)
        {
            Destroy(furniture_grid_[target_number].furniture_grid());
            Destroy(furniture_grid_[target_number].line_parent());
            furniture_grid_.RemoveAt(target_number);
        }
    }

    //家具グリッドのマウス操作(左クリックとドラッグ)
    public void MouseOperation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //どのグリッドをクリックしたか判定
            GameObject clicked_object = ClickObject();
            if (clicked_object != null)
            {
                for (int i = 0; i < furniture_grid_.Count; ++i)
                {
                    if (clicked_object.transform.root.gameObject == furniture_grid_[i].furniture_grid())
                    {
                        target_number_ = i;
                    }
                } //i
            }

            if (target_number_ > -1 && target_number_ < furniture_grid_.Count)
            {
                screen_point_ = Camera.main.WorldToScreenPoint(furniture_grid_[target_number_].furniture_grid().transform.position);
                point_offset_ = furniture_grid_[target_number_].furniture_grid().transform.position
                    - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screen_point_.z));
                if (mode_ == TransformMode.Translate)
                {
                }
                else if (mode_ == TransformMode.Rotate)
                {
                    furniture_grid_[target_number_].Rotate();
                }
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (target_number_ > -1 && target_number_ < furniture_grid_.Count)
            {
                if (mode_ == TransformMode.Translate)
                {
                    Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screen_point_.z);
                    Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + point_offset_;
                    furniture_grid_[target_number_].Translate(currentPosition);

                } //mode_ == Mode.Translate
                else if (mode_ == TransformMode.Rotate)
                {
                } //mode_ == Mode.Rotate
            }
        }
        else
        {
            target_number_ = -1;
        }
    }


    //ここまでインターフェースで呼び出されるであろう関数群***********************************************************************************************************************************************************

    // 1フレームごとに更新(マウス入力やキーボード入力をここで処理している)
    //
    //[t]キーで移動モード
    //[r]キーで回転モード
    //
    //(家具グリッドの出現上限は50個)
    //
    //(移動モードのとき)
    //家具グリッドを左ドラッグで家具移動
    //
    //(回転モードのとき)
    //家具グリッドを左クリックで家具90°回転
    //
    //家具グリッドを右クリックで家具グリッド削除
    private void Update()
    {
        //これを入れないと枠線とグリッドが合わない
        for (int i =0; i < furniture_grid_.Count; ++i )
        {
            furniture_grid_[i].AdjustmentLine();
        }

        //ここからそのうち削除する部分----------------------------------------------------------------------------------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.T))
        {
            ChangeTranslate();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeRotate();
        }


        if (furniture_grid_.Count < MaxFurnitureNum() )
        {
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                AddFurniture(10, 1); //家具グリッド追加
            }
            else if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                AddFurniture(10, 2); //家具グリッド追加
            }
            else if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                AddFurniture(10, 3); //家具グリッド追加
            }
            else if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                AddFurniture(10, 4); //家具グリッド追加
            }
            else if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                AddFurniture(10, 5); //家具グリッド追加
            }
            else if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                AddFurniture(10, 6); //家具グリッド追加
            }
            else if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                AddFurniture(10, 7); //家具グリッド追加
            }
            else if (Input.GetKeyDown(KeyCode.Keypad7))
            {
                AddFurniture(10, 8); //家具グリッド追加
            }
            else if (Input.GetKeyDown(KeyCode.Keypad8))
            {
                AddFurniture(10, 9); //家具グリッド追加
            }
            else if (Input.GetKeyDown(KeyCode.Keypad9))
            {
                AddFurniture(10, 10); //家具グリッド追加
            }
        }
        else
        {
            Debug.Log("家具が最大数まで達しています．");
        }
        //そのうち消す部分ここまで---------------------------------------------------------------------------------------------------------------------------------------

       

        if (Input.GetMouseButtonDown(1))
        {
            //どのグリッドをクリックしたか判定
            GameObject clicked_object = ClickObject();
            if (clicked_object != null)
            {
                for (int i = 0; i < furniture_grid_.Count; ++i)
                {
                    if (clicked_object.transform.root.gameObject == furniture_grid_[i].furniture_grid())
                    {
                        target_number_ = i;
                    }
                } //i
            } //clicked_object != null

            RemoveFurniture(target_number_); //家具グリッド削除

        } //GetMouseButtonDown(1)
        else
        {
            MouseOperation(); //マウス操作(左クリックとドラッグ)
        }

        //ここからそのうち消す
        if(mode() == TransformMode.Translate)
        {
            Debug.Log("移動モードです．");
        }
        else
        {
            Debug.Log("回転モードです．");
        }

        if(CollisionError())
        {
            Debug.Log("重ねられない家具同士が重なっています．");
        }

        //ここまでそのうち消す

    }// Update
}
