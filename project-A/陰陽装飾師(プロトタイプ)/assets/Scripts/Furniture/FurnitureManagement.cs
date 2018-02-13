//FurnitureManagement.cs(家具グリッドを出したり消したり回転させたり移動させたりの一括管理)
//
//
// 2017年2月1日 更新(菅原涼太)
// 衝突のエラーフラグを出力する関数の実装
// 家具の最大数定数，家具最大数を出力する関数の実装

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Evaluation Evaluation;
    public Grid_Manager Grid_Manager;
    public bl_GalleryManager Gallery_Manager;

    public GameObject Menu;
    public GameObject Current_Mode;
    public GameObject Add_;
    public GameObject Change_Mode_;
    public bool move_furniture = true;

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

        //int layerMask = LayerMask.GetMask(layer);

        //if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        if (Physics.Raycast(ray, out hit))
        {
            clicked_object = hit.collider.gameObject;
        }
        return clicked_object;
    }

    public void Add_ChangeMode(bool tf)
    {
        if (tf == true)
        {
            Add_.SetActive(true);
            Change_Mode_.SetActive(true);
        }
        else
        {
            Add_.SetActive(false);
            Change_Mode_.SetActive(false);
        }
    }

    public void Button_Clicked()
    {
        GameObject.Find("DataManager").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sound/SE/Button_Click"));
    }

    public void Add()
    {
        Button_Clicked();
        Add_ChangeMode(false);
        move_furniture = false;
        Gallery_Manager.mode_ = bl_GalleryManager.Mode.Add;
        Gallery_Manager.OpenGallery_type();
        Gallery_Manager.Init_type();
    }

    public void Change()
    {
        Button_Clicked();
        Gallery_Manager.mode_ = bl_GalleryManager.Mode.Change;
        Gallery_Manager.OpenGallery_furniture();
        Gallery_Manager.Init_furniture(furniture_grid_[target_number_]);
    }

    public void Delete()
    {
        Button_Clicked();
        RemoveFurniture();
        Add_ChangeMode(true);
        Menu.SetActive(false);
        move_furniture = true;
    }

    public void Property()
    {
        Button_Clicked();
        Gallery_Manager.mode_ = bl_GalleryManager.Mode.Property;
        Gallery_Manager.FullWindow_property(furniture_grid_[target_number_]);
        Gallery_Manager.OpenFullWindow();
    }

    public void Exit()
    {
        Button_Clicked();
        Add_ChangeMode(true);
        Menu.SetActive(false);
        move_furniture = true;
    }

    //ここからインターフェースで呼び出されるであろう関数群***********************************************************************************************************************************************************

    public void Change_Mode()
    {
        Button_Clicked();

        if (mode_ == TransformMode.Translate)
        {
            Current_Mode.GetComponent<Image>().sprite = Resources.Load<Sprite>("Button/Rotate_Mode");
            //Debug.Log("Change_Mode");
            mode_ = TransformMode.Rotate;
        }
        else if (mode_ == TransformMode.Rotate)
        {
            Current_Mode.GetComponent<Image>().sprite = Resources.Load<Sprite>("Button/Transform_Mode");
            //Debug.Log("Change_Mode");
            mode_ = TransformMode.Translate;
        }
    }

    //エラー(衝突)の名称判定
    //
    //戻り値
    // true = 衝突関連のエラー在り，false = 衝突関連のエラー無し
    public bool CollisionError()
    {
        bool error_flag = false;
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            if (furniture_grid_[i].furniture_grid().GetComponent<GridError>().errored())
            {
                error_flag = true;
                break;
            }
        }
        return error_flag;
    }

    //値を代入して家具グリッドを追加(基本的に外部で呼び出される)
    public void AddFurniture(int category_ID, int grid_ID)
    {
        if (furniture_grid_.Count < 50)
        {
            object_number_ += 1;

            FurnitureGrid temp = new FurnitureGrid();
            temp.Init(category_ID, grid_ID);
            temp.Translate(Grid_Manager.point(Grid_Manager.Grid_x_max / 2, Grid_Manager.Grid_y_max / 2).pos);

            furniture_grid_.Add(temp);
        }

        furniture_direction(furniture_grid_[furniture_grid_.Count - 1]);
        Debug.Log(furniture_grid_[furniture_grid_.Count - 1].placed_direction());
        Update_Evaluate();
    }

    public void ChangeFurniture(int category_ID, int grid_ID)
    {
        if (furniture_grid_.Count < 50)
        {
            FurnitureGrid temp_out = furniture_grid_[target_number_];
            Destroy(furniture_grid_[target_number_].furniture_grid());
            Destroy(furniture_grid_[target_number_].line_parent());

            FurnitureGrid temp_in = new FurnitureGrid();
            temp_in.Init(category_ID, grid_ID);
            temp_in.Translate(temp_out.grid_position());

            furniture_grid_[target_number_] = temp_in;           
        }

        furniture_direction(furniture_grid_[target_number_]);
        Debug.Log(furniture_grid_[furniture_grid_.Count - 1].placed_direction());
        Update_Evaluate();
    }

    //番号を指定して家具グリッドを削除
    public void RemoveFurniture()
    {
        if (target_number_ > -1 && target_number_ < furniture_grid_.Count)
        {
            object_number_ -= 1;
            Destroy(furniture_grid_[target_number_].furniture_grid());
            Destroy(furniture_grid_[target_number_].line_parent());
            furniture_grid_.RemoveAt(target_number_);
        }

        Update_Evaluate();
    }

    public bool Grid_out()
    {        
        furniture_grid_[target_number_].Translate(Target_pos());

        bool error = false;
        int count = 0;

        for (int k = 0; k < furniture_grid_[target_number_].vertices_number(); k++)
        {
            //Debug.Log(furniture_grid_[target_number_].vertices(k).ToString("f10"));

            for (int i = Grid_Manager.Grid_y_min; i < Grid_Manager.Grid_y_max; i++)
            {
                for (int j = Grid_Manager.Grid_x_min; j < Grid_Manager.Grid_x_max; j++)
                {
                    float vertices_x = furniture_grid_[target_number_].vertices(k).x + furniture_grid_[target_number_].grid_position().x;
                    float vertices_y = furniture_grid_[target_number_].vertices(k).y + furniture_grid_[target_number_].grid_position().y;

                    float grid_x_min = Grid_Manager.point(j, i).pos.x - (Grid_Manager.Grid_density / 2.0f);
                    float grid_y_min = Grid_Manager.point(j, i).pos.y - (Grid_Manager.Grid_density / 2.0f);
                    float grid_x_max = Grid_Manager.point(j, i).pos.x + (Grid_Manager.Grid_density / 2.0f);
                    float grid_y_max = Grid_Manager.point(j, i).pos.y + (Grid_Manager.Grid_density / 2.0f);

                    if (grid_x_min < vertices_x && vertices_x < grid_x_max &&
                        grid_y_min < vertices_y && vertices_y < grid_y_max)
                    {
                        count++;
                    }
                }
            }
        }

        //Debug.Log("頂点数 " + furniture_grid_[target_number_].vertices_number());
        //Debug.Log("count " + count);

        if (count < furniture_grid_[target_number_].vertices_number())
        {
            error = true;
        }

        return error;
    }

    //public bool Room_out()
    //{
    //    bool error = false;
    //
    //    for (int k = 0; k < furniture_grid_[target_number_].vertices_number(); k++)
    //    {
    //        for (int i = Grid_Manager.Grid_y_min; i < Grid_Manager.Grid_y_max; i++)
    //        {
    //            for (int j = Grid_Manager.Grid_x_min; j < Grid_Manager.Grid_x_max; j++)
    //            {
    //                if (error == false)
    //                {
    //                    float vertices_x = furniture_grid_[target_number_].vertices(k).x + furniture_grid_[target_number_].grid_position().x;
    //                    float vertices_y = furniture_grid_[target_number_].vertices(k).y + furniture_grid_[target_number_].grid_position().y;
    //
    //                    float grid_x_min = Grid_Manager.point(j, i).pos.x - (Grid_Manager.Grid_density / 2.0f);
    //                    float grid_y_min = Grid_Manager.point(j, i).pos.y - (Grid_Manager.Grid_density / 2.0f);
    //                    float grid_x_max = Grid_Manager.point(j, i).pos.x + (Grid_Manager.Grid_density / 2.0f);
    //                    float grid_y_max = Grid_Manager.point(j, i).pos.y + (Grid_Manager.Grid_density / 2.0f);
    //
    //                    if (grid_x_min < vertices_x && vertices_x < grid_x_max &&
    //                        grid_y_min < vertices_y && vertices_y < grid_y_max)
    //                    {
    //                        if (Grid_Manager.point(j, i).inside == false)
    //                        {
    //                            error = true;
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //
    //    return error;
    //}

    public Vector3 Target_pos()
    {
        Vector3 target_pos = new Vector3();

        float dist_min = Vector3.Distance(
               Grid_Manager.point(Grid_Manager.Grid_x_min, Grid_Manager.Grid_y_min).pos,
               furniture_grid_[target_number_].grid_position());

        for (int i = Grid_Manager.Grid_y_min; i < Grid_Manager.Grid_y_max; i++)
        {
            for (int j = Grid_Manager.Grid_x_min; j < Grid_Manager.Grid_x_max; j++)
            {
                float dist_temp = Vector3.Distance(
                    Grid_Manager.point(j, i).pos,
                    furniture_grid_[target_number_].grid_position());

                if (dist_temp < dist_min)
                {
                    dist_min = dist_temp;
                    target_pos = Grid_Manager.point(j, i).pos;
                }

            }
        }

        return target_pos;
    }

    public FurnitureGrid furniture_direction(FurnitureGrid furniture_grid_temp)
    {
        //Grid_Manager.Test_sphere(Grid_Manager.Grid_x_max / 2, Grid_Manager.Grid_y_max / 2, 0.3f, Color.red);

        Vector3 target = furniture_grid_temp.furniture_grid().transform.position - Grid_Manager.point(Grid_Manager.Grid_x_max / 2, Grid_Manager.Grid_y_max / 2).pos;
        Vector3 source = new Vector3(1.0f, 0, 0);

        float angle =
        Mathf.Acos(
            ((source.x * target.x) + (source.y * target.y))
            /
            (Mathf.Sqrt(Mathf.Pow(source.x, 2) + Mathf.Pow(source.y, 2)) * Mathf.Sqrt(Mathf.Pow(target.x, 2) + Mathf.Pow(target.y, 2)))
            ) * Mathf.Rad2Deg;

        Debug.Log("角度 " + angle + ", " + "位置 " + target.y);

        if (0 <= angle && angle <= 15)
        {
            furniture_grid_temp.set_direction(FurnitureGrid.PlacedDirection.East);
        }
        else if (15 < angle && angle <= 75)
        {
            if (target.y > 0)
            {
                furniture_grid_temp.set_direction(FurnitureGrid.PlacedDirection.NorthEast);
            }
            else if (target.y < 0)
            {
                furniture_grid_temp.set_direction(FurnitureGrid.PlacedDirection.SouthEast);
            }
        }
        else if (75 < angle && angle <= 105)
        {
            if (target.y > 0)
            {
                furniture_grid_temp.set_direction(FurnitureGrid.PlacedDirection.North);
            }
            else if (target.y < 0)
            {
                furniture_grid_temp.set_direction(FurnitureGrid.PlacedDirection.South);
            }
        }
        else if (105 < angle && angle <= 165)
        {
            if (target.y > 0)
            {
                furniture_grid_temp.set_direction(FurnitureGrid.PlacedDirection.NorthWest);
            }
            else if (target.y < 0)
            {
                furniture_grid_temp.set_direction(FurnitureGrid.PlacedDirection.SouthWest);
            }
        }
        else if (165 < angle && angle <= 180)
        {
            furniture_grid_temp.set_direction(FurnitureGrid.PlacedDirection.West);
        }

        return furniture_grid_temp;
    }

    //コンストラクタ
    private void Start()
    {
        
    }

    public void Room(Evaluation.Room room, Evaluation.Direction direction, int[] norma_luck, int advaice_mode)
    {
        //AddFurniture(0, 1, new Vector3(-4.0f, -1.0f, 0));
        //AddFurniture(1, 1, new Vector3(3.0f, 1.0f, 0));
        //AddFurniture(2, 1, new Vector3(4.0f, -3.0f, 0));
        //AddFurniture(3, 1, new Vector3(0.0f, 1.0f, 0));
        //AddFurniture(7, 1, new Vector3(7.0f, 1.0f, 0));       
        //AddFurniture(8, 1, new Vector3(1.5f, -5.5f, 0));               
        //AddFurniture(9, 1, new Vector3(6.5f, -3.0f, 0));

        //for (int i = 0; i < furniture_grid_.Count; i++)
        //{
        //    furniture_direction(furniture_grid_[i], new Vector3(0.0f, 0.0f, 0));
        //}

        Evaluation.Init(room, direction, norma_luck, advaice_mode, furniture_grid_, Grid_Manager);
    }

    public void Update_Evaluate()
    {
        Evaluation.UpdateGrid(furniture_grid_);
        Evaluation.EvaluationTotal();
        Evaluation.UpdateLuckText();
        Evaluation.Comment_Text();

        Debug.Log("Evaluation_End");
    }

    //家具グリッドのマウス操作(左クリックとドラッグ)

    Vector3 start_pos = new Vector3();
    Quaternion start_rot = new Quaternion();

    public void MouseLeftDown(GameObject clicked_object)
    {
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            if (clicked_object == furniture_grid_[i].furniture_grid())
            {
                target_number_ = i;
            }
        } //i

        if (target_number_ > -1 && target_number_ < furniture_grid_.Count)
        {
            screen_point_ = Camera.main.WorldToScreenPoint(furniture_grid_[target_number_].furniture_grid().transform.position);
            point_offset_ = furniture_grid_[target_number_].furniture_grid().transform.position
                - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screen_point_.z));

            if (mode_ == TransformMode.Translate)
            {
                start_pos = furniture_grid_[target_number_].grid_position();

            }
            else if (mode_ == TransformMode.Rotate)
            {
                start_rot = furniture_grid_[target_number_].transform_rotation();
                furniture_grid_[target_number_].Rotate(90);
            }
        }
    }

    public void MouseLeftState()
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

    public void MouseLeftUp()
    {
        if (target_number_ > -1 && target_number_ < furniture_grid_.Count)
        {
            if (mode_ == TransformMode.Translate)
            {
                if (Grid_out() == false)
                {
                    //if (Room_out() == true)
                    //{
                    //    furniture_grid_[target_number_].Translate(start_pos);
                    //    Debug.Log("間取り外");
                    //}
                }
                else
                {
                    furniture_grid_[target_number_].Translate(start_pos);
                    Debug.Log("グリッド外");
                }
            }
            else if (mode_ == TransformMode.Rotate)
            {
                if (Grid_out() == false)
                {
                    //if (Room_out() == true)
                    //{
                    //    furniture_grid_[target_number_].Rotate(-90);
                    //    Debug.Log("間取り外");
                    //}
                }
                else
                {
                    furniture_grid_[target_number_].Rotate(-90);
                    Debug.Log("グリッド外");
                }
            }
            furniture_direction(furniture_grid_[target_number_]);
            Debug.Log(furniture_grid_[target_number_].placed_direction());
            Update_Evaluate();
        }
    }

    public void MouseRightDown(GameObject clicked_object)
    {
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            if (clicked_object == furniture_grid_[i].furniture_grid())
            {
                target_number_ = i;
            }
        } //i

        Add_ChangeMode(false);

        Menu.transform.position = clicked_object.transform.position + new Vector3(2.0f, 2.0f, -0.1f);
        Menu.SetActive(true);
        move_furniture = false;
    }

    GameObject clicked_object = null;
    bool clicked_furniture = false;

    public void MouseOperation()
    {
        //if (Menu.activeSelf == false)
        //{
            if (Input.GetMouseButtonDown(0))
            {
                clicked_object = ClickObject();
                //Debug.Log(clicked_object);
                if (clicked_object != null)
                {
                    if (clicked_object.layer != 5)
                    {
                        clicked_furniture = true;
                        MouseLeftDown(clicked_object);
                    }
                    else
                    {
                        clicked_furniture = false;
                    }
                }
                else
                {
                    clicked_furniture = false;
                }
            }

            if (clicked_furniture == true)
            {
                if (Input.GetMouseButton(0))
                {
                    MouseLeftState();
                }

                if (Input.GetMouseButtonUp(0))
                {
                    MouseLeftUp();
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                clicked_object = ClickObject();

                if (clicked_object != null)
                {
                    if (clicked_object.layer == 17)
                    {
                        MouseRightDown(clicked_object);
                    }
                }
            }
        //}
       
    }

    public LevelManager levelmanager;

    private void Update()
    {        
        if (move_furniture == true)
        {
            MouseOperation(); //マウス操作

            //これを入れないと枠線とグリッドが合わない
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                furniture_grid_[i].AdjustmentLine();
            }

            if (CollisionError())
            {
                Debug.Log("重ねられない家具同士が重なっています．");
            }
        }
    }// Update
}
