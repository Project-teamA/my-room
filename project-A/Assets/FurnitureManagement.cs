//FurnitureManagement.cs(家具グリッドを出したり消したり回転させたり移動させたりの一括管理)
//
// 2017年12月12日 更新(菅原涼太)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureManagement : MonoBehaviour {
    public enum TransformMode { Translate, Rotate }; //(Translate = 移動モード，Rotate = 回転モード)

    private int target_number_ = -1; //クリックまたはドラッグされている家具グリッドをもったクラスのナンバー 何もクリックしていない場合-1(アクセス不可)
    private Vector3 screen_point_; //オブジェクトのスクリーン上での位置 (アクセス不可)
    private Vector3 point_offset_; //オブジェクト中心とのオフセット (アクセス不可)
    private List<FurnitureGrid> furniture_grid_ = new List<FurnitureGrid>(); //FurnitureGrid.csで実装されているクラスのリスト(最大50)
    private int object_number_ = 0; //FurnitureGridクラス生成回数
    private TransformMode mode_ = TransformMode.Translate; //現在のモード(初期モードは移動モード)

    //インスタンス化されているFurnitureGridクラスをすべて取得
    public List<FurnitureGrid> furniture_grid_all()
    {
        return furniture_grid_;
    }

    //インスタンス化されているFurnitureGridクラスを配列番号指定で取得
    public FurnitureGrid furniture_grid(int number)
    {
        if(number > -1 && number < furniture_grid_.Count)
        {
            return furniture_grid_[number];
        }
        return null;
    }

    //インスタンス化されているFurnitureGridクラスをオブジェクトネームで取得
    public FurnitureGrid furniture_grid(string name)
    {
        for(int i = 0; i< furniture_grid_.Count; ++i)
        {
            if(furniture_grid_[i].furniture_grid().name == name)
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

    // コンストラクタの代わり?
    private void Start ()
    {   
    }

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
    private void Update () {

        if(Input.GetKeyDown(KeyCode.T))
        {
            mode_ = TransformMode.Translate;
            Debug.Log("移動モード");
        } //Input.GetKeyDown(KeyCode.T)
        else if(Input.GetKeyDown(KeyCode.R))
        {
            mode_ = TransformMode.Rotate;
            Debug.Log("回転モード");
        } //Input.GetKeyDown(KeyCode.R)

        if (furniture_grid_.Count < 50)
        {
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                object_number_ += 1;
                string object_name = "furniture_grid_" + object_number_.ToString();

                furniture_grid_.Add(gameObject.AddComponent<FurnitureGrid>());
                furniture_grid_[furniture_grid_.Count - 1].Init(0, object_name);
            } //Input.GetKeyDown(KeyCode.Keypad0)
            else if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                object_number_ += 1;
                string object_name = "furniture_grid_" + object_number_.ToString();
                furniture_grid_.Add(gameObject.AddComponent<FurnitureGrid>());
                furniture_grid_[furniture_grid_.Count - 1].Init(1, object_name);
            } //Input.GetKeyDown(KeyCode.Keypad1)
            else if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                object_number_ += 1;
                string object_name = "furniture_grid_" + object_number_.ToString();
                furniture_grid_.Add(gameObject.AddComponent<FurnitureGrid>());
                furniture_grid_[furniture_grid_.Count - 1].Init(2, object_name);
            } //Input.GetKeyDown(KeyCode.Keypad2)
            else if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                object_number_ += 1;
                string object_name = "furniture_grid_" + object_number_.ToString();
                furniture_grid_.Add(gameObject.AddComponent<FurnitureGrid>());
                furniture_grid_[furniture_grid_.Count - 1].Init(3, object_name);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad4)) //カーペット
            {
                object_number_ += 1;
                string object_name = "furniture_grid_" + object_number_.ToString();
                furniture_grid_.Add(gameObject.AddComponent<FurnitureGrid>());
                furniture_grid_[furniture_grid_.Count - 1].Init(4, object_name);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad5)) //壁掛け
            {
                object_number_ += 1;
                string object_name = "furniture_grid_" + object_number_.ToString();
                furniture_grid_.Add(gameObject.AddComponent<FurnitureGrid>());
                furniture_grid_[furniture_grid_.Count - 1].Init(5, object_name);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad6)) //ドア
            {
                object_number_ += 1;
                string object_name = "furniture_grid_" + object_number_.ToString();
                furniture_grid_.Add(gameObject.AddComponent<FurnitureGrid>());
                furniture_grid_[furniture_grid_.Count - 1].Init(6, object_name);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad7)) //天井
            {
                object_number_ += 1;
                string object_name = "furniture_grid_" + object_number_.ToString();
                furniture_grid_.Add(gameObject.AddComponent<FurnitureGrid>());
                furniture_grid_[furniture_grid_.Count - 1].Init(7, object_name);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad8)) //窓
            {
                object_number_ += 1;
                string object_name = "furniture_grid_" + object_number_.ToString();
                furniture_grid_.Add(gameObject.AddComponent<FurnitureGrid>());
                furniture_grid_[furniture_grid_.Count - 1].Init(8, object_name);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad9)) //ベッド
            {
                object_number_ += 1;
                string object_name = "furniture_grid_" + object_number_.ToString();
                furniture_grid_.Add(gameObject.AddComponent<FurnitureGrid>());
                furniture_grid_[furniture_grid_.Count - 1].Init(9, object_name);
            }

        } //furniture_grid_.Count < 50

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
            } //clicked_object != null


            if (target_number_ > -1 && target_number_ < furniture_grid_.Count)
            {
                screen_point_ = Camera.main.WorldToScreenPoint(furniture_grid_[target_number_].furniture_grid().transform.position);
                point_offset_ = furniture_grid_[target_number_].furniture_grid().transform.position
                    - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screen_point_.z));
                if (mode_ == TransformMode.Translate)
                {
                } //(mode_ == Mode.Translate
                else if (mode_ == TransformMode.Rotate)
                {
                    furniture_grid_[target_number_].Rotate();
                } //(mode_ == Mode.Rotate
            } //target_number_ > -1 && target_number_ < furniture_grid_.Count
        } //Input.GetMouseButtonDown(0)
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
            } //target_number_ > -1 && target_number_ < furniture_grid_.Count
        } //Input.GetMouseButton(0)
        else if (Input.GetMouseButtonDown(1))
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


            if (target_number_ > -1 && target_number_ < furniture_grid_.Count)
            {
                Destroy(furniture_grid_[target_number_].furniture_grid() );
                furniture_grid_.RemoveAt(target_number_);
            } //target_number_ > -1 && target_number_ < furniture_grid_.Count
        } //GetMouseButtonDown(1)
        else
        {
            target_number_ = -1;
        }
    }// Update
}
