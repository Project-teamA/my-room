//このクラスはFurnitureGridクラスの分割部分であり，タンスのグリッドデータを生成するGetGridDataCabinetメソッドが実装されている
//
//タンスのFurnitureTypeはCabinet
//
//parametaの長さは1
//parameta_[0] = ダミー
//
//(ここからは自分の勝手な判断)
//
//今のところ・・・
//五行の合計が50になるように設定
//陰陽の制限はなし


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //UnityEventを使用するため
using UnityEngine.EventSystems; //

public partial class FurnitureGrid : MonoBehaviour
{
    partial void GetGridDataCabinet(int furniture_ID)
    {
        switch (furniture_ID)
        {

            case 1:
            default:
                {
                    //タンスタイプ1

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 4, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 4, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 6 }; //1
                    grid_point_[2] = new int[2] { 9, 0 }; //2
                    grid_point_[3] = new int[2] { 9, 6 }; //3

                    texture_ = Resources.Load<Texture2D>("cabinet/cabinet_1_grid");
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.36F, 0.335F); //0
                    uv_coordinate_[1] = new Vector2(0.36F, 0.665F); //1
                    uv_coordinate_[2] = new Vector2(0.64F, 0.335F); //2
                    uv_coordinate_[3] = new Vector2(0.64F, 0.665F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { true, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0;

                    //-------------------------------------------------

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(5);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.Marble);
                    material_type_weight_.Add(4);

                    material_type_.Add(MaterialType.Metal);
                    material_type_weight_.Add(1);

                    //-------------------------------------------------

                    //-------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    form_type_.Add(FormType.High);
                    form_type_weight_.Add(2);

                    form_type_.Add(FormType.Vertical);
                    form_type_weight_.Add(2);

                    //--------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    //--------------------------------------------------

                    break;
                }

            case 2:
                {
                    //タンスタイプ2

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 4, 2 }; //中心のグリッド座標
                    put_point_ = new int[2] { 4, 2 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 5 }; //1
                    grid_point_[2] = new int[2] { 9, 0 }; //2
                    grid_point_[3] = new int[2] { 9, 5 }; //3

                    texture_ = Resources.Load<Texture2D>("cabinet/cabinet_2_grid");
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.4F, 0.335F); //0
                    uv_coordinate_[1] = new Vector2(0.4F, 0.665F); //1
                    uv_coordinate_[2] = new Vector2(0.6F, 0.335F); //2
                    uv_coordinate_[3] = new Vector2(0.6F, 0.665F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { true, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0;

                    //-------------------------------------------------

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(5);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(4);

                    material_type_.Add(MaterialType.Metal);
                    material_type_weight_.Add(1);

                    //-------------------------------------------------

                    //-------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    form_type_.Add(FormType.Vertical);
                    form_type_weight_.Add(3);

                    form_type_.Add(FormType.High);
                    form_type_weight_.Add(2);

                    //--------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    //--------------------------------------------------

                    break;
                }

            case 3:
                {
                    //タンスタイプ3

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 5, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 5, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 6 }; //1
                    grid_point_[2] = new int[2] { 10, 0 }; //2
                    grid_point_[3] = new int[2] { 10, 6 }; //3

                    texture_ = Resources.Load<Texture2D>("cabinet/cabinet_3_grid");
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.4F, 0.335F); //0
                    uv_coordinate_[1] = new Vector2(0.4F, 0.665F); //1
                    uv_coordinate_[2] = new Vector2(0.6F, 0.335F); //2
                    uv_coordinate_[3] = new Vector2(0.6F, 0.665F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { true, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0;

                    //-------------------------------------------------

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(5);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.Plastic);
                    material_type_weight_.Add(4);

                    material_type_.Add(MaterialType.Metal);
                    material_type_weight_.Add(1);

                    //-------------------------------------------------

                    //-------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    form_type_.Add(FormType.Vertical);
                    form_type_weight_.Add(3);

                    form_type_.Add(FormType.High);
                    form_type_weight_.Add(2);

                    //--------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    //--------------------------------------------------

                    break;
                }

            case 4:
                {
                    //タンスタイプ4

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 8, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 8, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 6 }; //1
                    grid_point_[2] = new int[2] { 16, 0 }; //2
                    grid_point_[3] = new int[2] { 16, 6 }; //3

                    texture_ = Resources.Load<Texture2D>("cabinet/cabinet_4_grid");
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.28F, 0.38F); //0
                    uv_coordinate_[1] = new Vector2(0.28F, 0.63F); //1
                    uv_coordinate_[2] = new Vector2(0.66F, 0.38F); //2
                    uv_coordinate_[3] = new Vector2(0.66F, 0.63F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { true, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0;

                    //-------------------------------------------------

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(5);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(4);

                    material_type_.Add(MaterialType.Metal);
                    material_type_weight_.Add(1);

                    //-------------------------------------------------

                    //-------------------------------------------------

                    form_type_.Add(FormType.High);
                    form_type_weight_.Add(2);

                    form_type_.Add(FormType.Novel);
                    form_type_weight_.Add(2);

                    form_type_.Add(FormType.Oblong);
                    form_type_weight_.Add(1);

                    //--------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    //--------------------------------------------------

                    break;
                }

            case 5:
                {
                    //タンスタイプ5

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 5, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 5, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 6 }; //1
                    grid_point_[2] = new int[2] { 10, 0 }; //2
                    grid_point_[3] = new int[2] { 10, 6 }; //3

                    texture_ = Resources.Load<Texture2D>("cabinet/cabinet_5_grid");
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.29F, 0.26F); //0
                    uv_coordinate_[1] = new Vector2(0.29F, 0.75F); //1
                    uv_coordinate_[2] = new Vector2(0.71F, 0.26F); //2
                    uv_coordinate_[3] = new Vector2(0.71F, 0.75F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { true, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0;


                    //-------------------------------------------------

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(5);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(5);

                    //-------------------------------------------------

                    //-------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    form_type_.Add(FormType.High);
                    form_type_weight_.Add(3);

                    form_type_.Add(FormType.Vertical);
                    form_type_weight_.Add(3);

                    //--------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    //--------------------------------------------------

                    break;
                }

            case 6:
                {
                    //タンスタイプ6

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 7, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 7, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 6 }; //1
                    grid_point_[2] = new int[2] { 14, 0 }; //2
                    grid_point_[3] = new int[2] { 14, 6 }; //3

                    texture_ = Resources.Load<Texture2D>("cabinet/cabinet_6_grid");
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.25F, 0.3F); //0
                    uv_coordinate_[1] = new Vector2(0.25F, 0.7F); //1
                    uv_coordinate_[2] = new Vector2(0.75F, 0.3F); //2
                    uv_coordinate_[3] = new Vector2(0.75F, 0.7F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { true, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0;

                    //-------------------------------------------------

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(5);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.Marble);
                    material_type_weight_.Add(5);

                    //-------------------------------------------------

                    //-------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    form_type_.Add(FormType.Oblong);
                    form_type_weight_.Add(3);

                    //--------------------------------------------------

                    characteristic_.Add(Characteristic.Luxury);
                    characteristic_weight_.Add(3);

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    //--------------------------------------------------

                    break;
                }

            case 7:
                {
                    //タンスタイプ7

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 4, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 4, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 5 }; //1
                    grid_point_[2] = new int[2] { 8, 0 }; //2
                    grid_point_[3] = new int[2] { 8, 5 }; //3

                    texture_ = Resources.Load<Texture2D>("cabinet/cabinet_7_grid");
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.3F, 0.3F); //0
                    uv_coordinate_[1] = new Vector2(0.3F, 0.73F); //1
                    uv_coordinate_[2] = new Vector2(0.7F, 0.3F); //2
                    uv_coordinate_[3] = new Vector2(0.7F, 0.73F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { true, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0;

                    //-------------------------------------------------

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(5);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.Marble);
                    material_type_weight_.Add(5);

                    //-------------------------------------------------

                    //-------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(3);

                    form_type_.Add(FormType.High);
                    form_type_weight_.Add(2);

                    //--------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    //--------------------------------------------------

                    break;
                }

            case 8:
                {
                    //タンスタイプ8

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 6, 2 }; //中心のグリッド座標
                    put_point_ = new int[2] { 6, 2 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 4 }; //1
                    grid_point_[2] = new int[2] { 12, 0 }; //2
                    grid_point_[3] = new int[2] { 12, 4 }; //3

                    texture_ = Resources.Load<Texture2D>("cabinet/cabinet_8_grid");
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.19F, 0.3F); //0
                    uv_coordinate_[1] = new Vector2(0.19F, 0.7F); //1
                    uv_coordinate_[2] = new Vector2(0.81F, 0.3F); //2
                    uv_coordinate_[3] = new Vector2(0.81F, 0.7F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { true, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0;

                    //-------------------------------------------------

                    color_name_.Add(ColorName.Ocher);
                    color_name_weight_.Add(5);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(5);

                    material_type_.Add(MaterialType.Metal);
                    material_type_weight_.Add(1);

                    //-------------------------------------------------

                    //-------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    form_type_.Add(FormType.Oblong);
                    form_type_weight_.Add(3);

                    //--------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    //--------------------------------------------------

                    break;
                }

            case 9:
                {
                    //タンスタイプ9

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 5, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 5, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 6 }; //1
                    grid_point_[2] = new int[2] { 10, 0 }; //2
                    grid_point_[3] = new int[2] { 10, 6 }; //3

                    texture_ = Resources.Load<Texture2D>("cabinet/cabinet_9_grid");
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.37F, 0.35F); //0
                    uv_coordinate_[1] = new Vector2(0.37F, 0.65F); //1
                    uv_coordinate_[2] = new Vector2(0.63F, 0.35F); //2
                    uv_coordinate_[3] = new Vector2(0.63F, 0.65F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { true, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0;

                    //-------------------------------------------------

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(5);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.Plastic);
                    material_type_weight_.Add(5);

                    //-------------------------------------------------

                    //-------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    form_type_.Add(FormType.High);
                    form_type_weight_.Add(4);

                    form_type_.Add(FormType.Vertical);
                    form_type_weight_.Add(4);

                    form_type_.Add(FormType.Novel);
                    form_type_weight_.Add(1);

                    //--------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    //--------------------------------------------------


                    break;
                }

            case 10:
                {
                    //タンスタイプ10

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 16, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 16, 3 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 6 }; //1
                    grid_point_[2] = new int[2] { 32, 0 }; //2
                    grid_point_[3] = new int[2] { 32, 6 }; //3

                    texture_ = Resources.Load<Texture2D>("cabinet/cabinet_10_grid");
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.13F, 0.37F); //0
                    uv_coordinate_[1] = new Vector2(0.13F, 0.63F); //1
                    uv_coordinate_[2] = new Vector2(0.87F, 0.37F); //2
                    uv_coordinate_[3] = new Vector2(0.87F, 0.63F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { true, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0;

                    //-------------------------------------------------

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(5);

                    color_name_.Add(ColorName.Black);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.Gray);
                    color_name_weight_.Add(1);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.Marble);
                    material_type_weight_.Add(5);

                    material_type_.Add(MaterialType.Plastic);
                    material_type_weight_.Add(2);

                    material_type_.Add(MaterialType.NaturalFibre);
                    material_type_weight_.Add(1);

                    //-------------------------------------------------

                    //-------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    form_type_.Add(FormType.High);
                    form_type_weight_.Add(3);

                    form_type_.Add(FormType.Oblong);
                    form_type_weight_.Add(3);

                    //--------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    //--------------------------------------------------

                    break;
                }

            case 11:
                {
                    //タンスタイプ11

                    object_type_ = ObjectType.Normal;
                    children_number_ = 2;
                    center_point_ = new int[2] { 22, 22 }; //中心のグリッド座標
                    put_point_ = new int[2] { 22, 22 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 8;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 18, 0 }; //0
                    grid_point_[1] = new int[2] { 18, 18 }; //1
                    grid_point_[2] = new int[2] { 27, 0 }; //2
                    grid_point_[3] = new int[2] { 27, 18 }; //3

                    grid_point_[4] = new int[2] { 0, 18 }; //4
                    grid_point_[5] = new int[2] { 0, 27 }; //5
                    grid_point_[6] = new int[2] { 27, 18 }; //6
                    grid_point_[7] = new int[2] { 27, 27 }; //7

                    texture_ = Resources.Load<Texture2D>("cabinet/cabinet_11_grid");
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.67F, 0.0F); //0
                    uv_coordinate_[1] = new Vector2(0.67F, 0.67F); //1
                    uv_coordinate_[2] = new Vector2(1.0F, 0.0F); //2
                    uv_coordinate_[3] = new Vector2(1.0F, 0.67F); //3

                    uv_coordinate_[4] = new Vector2(0.0F, 0.67F); //4
                    uv_coordinate_[5] = new Vector2(0.0F, 1.0F); //5
                    uv_coordinate_[6] = new Vector2(1.0F, 0.67F); //6
                    uv_coordinate_[7] = new Vector2(1.0F, 1.0F); //7

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };
                    triangles_[1] = new int[4] { 4, 5, 6, 7 };

                    //枠線
                    outline_index_ = new int[16] { 0, 2, 2, 3, 3, 6, 6, 7, 7, 5, 5, 4, 4, 1, 1, 0 };
                    blueflag_index_ = new bool[8] { false, false, false, false, false, false, true, true };

                    //パラメータの設定
                    parameta_[0] = 0;

                    //-------------------------------------------------

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(6);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(6);

                    material_type_.Add(MaterialType.Glass);
                    material_type_weight_.Add(1);

                    material_type_.Add(MaterialType.Metal);
                    material_type_weight_.Add(1);

                    //-------------------------------------------------

                    //-------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(6);

                    form_type_.Add(FormType.High);
                    form_type_weight_.Add(3);

                    //--------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    //--------------------------------------------------

                    break;
                }

            case 12:
                {
                    //タンスタイプ12

                    object_type_ = ObjectType.Normal;
                    children_number_ = 2;
                    center_point_ = new int[2] { 26, 24 }; //中心のグリッド座標
                    put_point_ = new int[2] { 26, 24 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 8;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 22, 0 }; //0
                    grid_point_[1] = new int[2] { 22, 20 }; //1
                    grid_point_[2] = new int[2] { 38, 0 }; //2
                    grid_point_[3] = new int[2] { 38, 20 }; //3

                    grid_point_[4] = new int[2] { 0, 20 }; //4
                    grid_point_[5] = new int[2] { 0, 36 }; //5
                    grid_point_[6] = new int[2] { 38, 20 }; //6
                    grid_point_[7] = new int[2] { 38, 36 }; //7

                    texture_ = Resources.Load<Texture2D>("cabinet/cabinet_12_grid");
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.579F, 0.0F); //0
                    uv_coordinate_[1] = new Vector2(0.579F, 0.5567F); //1
                    uv_coordinate_[2] = new Vector2(1.0F, 0.0F); //2
                    uv_coordinate_[3] = new Vector2(1.0F, 0.556F); //3

                    uv_coordinate_[4] = new Vector2(0.0F, 0.556F); //4
                    uv_coordinate_[5] = new Vector2(0.0F, 1.0F); //5
                    uv_coordinate_[6] = new Vector2(1.0F, 0.556F); //6
                    uv_coordinate_[7] = new Vector2(1.0F, 1.0F); //7

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };
                    triangles_[1] = new int[4] { 4, 5, 6, 7 };

                    //枠線
                    outline_index_ = new int[16] { 0, 2, 2, 3, 3, 6, 6, 7, 7, 5, 5, 4, 4, 1, 1, 0 };
                    blueflag_index_ = new bool[8] { false, false, false, false, false, false, true, true };

                    //パラメータの設定
                    parameta_[0] = 0;

                    //-------------------------------------------------

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(7);

                    color_name_.Add(ColorName.Red);
                    color_name_weight_.Add(2);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(7);

                    material_type_.Add(MaterialType.Glass);
                    material_type_weight_.Add(2);

                    //-------------------------------------------------

                    //-------------------------------------------------

                    form_type_.Add(FormType.High);
                    form_type_weight_.Add(3);

                    form_type_.Add(FormType.Novel);
                    form_type_weight_.Add(2);

                    //--------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    //--------------------------------------------------

                    break;
                }

            case 13:
                {
                    //タンスタイプ13

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 18, 2 }; //中心のグリッド座標
                    put_point_ = new int[2] { 18, 2 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 3 }; //1
                    grid_point_[2] = new int[2] { 36, 0 }; //2
                    grid_point_[3] = new int[2] { 36, 3 }; //3

                    texture_ = Resources.Load<Texture2D>("cabinet/cabinet_13_grid");
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.0F, 0.0F); //0
                    uv_coordinate_[1] = new Vector2(0.0F, 1.0F); //1
                    uv_coordinate_[2] = new Vector2(1.0F, 0.0F); //2
                    uv_coordinate_[3] = new Vector2(1.0F, 1.0F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { true, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0;

                    //-------------------------------------------------

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(5);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(4);

                    //-------------------------------------------------

                    //-------------------------------------------------

                    form_type_.Add(FormType.Oblong);
                    form_type_weight_.Add(5);

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    form_type_.Add(FormType.High);
                    form_type_weight_.Add(3);

                    //--------------------------------------------------

                    break;
                }

            case 14:
                {
                    //タンスタイプ14

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 17, 2 }; //中心のグリッド座標
                    put_point_ = new int[2] { 17, 2 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 4 }; //1
                    grid_point_[2] = new int[2] { 34, 0 }; //2
                    grid_point_[3] = new int[2] { 34, 4 }; //3

                    texture_ = Resources.Load<Texture2D>("cabinet/cabinet_14_grid");
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.0F, 0.0F); //0
                    uv_coordinate_[1] = new Vector2(0.0F, 1.0F); //1
                    uv_coordinate_[2] = new Vector2(1.0F, 0.0F); //2
                    uv_coordinate_[3] = new Vector2(1.0F, 1.0F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { true, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0;

                    //-------------------------------------------------

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(5);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(4);

                    material_type_.Add(MaterialType.Paper);
                    material_type_weight_.Add(2);

                    //-------------------------------------------------

                    //-------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    form_type_.Add(FormType.Oblong);
                    form_type_weight_.Add(4);

                    form_type_.Add(FormType.High);
                    form_type_weight_.Add(3);

                    //--------------------------------------------------

                    break;
                }
        }

    }

}