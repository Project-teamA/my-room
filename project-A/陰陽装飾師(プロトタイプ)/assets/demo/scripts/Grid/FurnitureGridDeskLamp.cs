//このクラスはFurnitureGridクラスの分割部分であり，天井ランプのグリッドデータを生成するGetGridDataDeskLampメソッドが実装されている
//
//机ランプのFurnitureTypeはDeskLamp
//
//方位の指定は特になし
//
//
//parametaの長さは1
//parameta_[0] = ダミー
//
//(ここからは自分の勝手な判断)
//



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //UnityEventを使用するため
using UnityEngine.EventSystems; //

public partial class FurnitureGrid : MonoBehaviour
{
    partial void GetGridDataDeskLamp(int furniture_ID)
    {
        switch (furniture_ID)
        {
            case 1:
            default:
                {
                    //天井掛け1

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 2, 2 }; //中心のグリッド座標
                    put_point_ = new int[2] { 2, 2 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 5 }; //1
                    grid_point_[2] = new int[2] { 5, 0 }; //2
                    grid_point_[3] = new int[2] { 5, 5 }; //3
                    texture_ = Resources.Load<Texture2D>("desklamp/desklamp_1_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.41F, 0.32F); //0
                    uv_coordinate_[1] = new Vector2(0.41F, 0.68F); //1
                    uv_coordinate_[2] = new Vector2(0.59F, 0.32F); //2
                    uv_coordinate_[3] = new Vector2(0.59F, 0.68F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー


                    //----------------------------------------------------

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(1);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.ChemicalFibre);
                    material_type_weight_.Add(2);

                    material_type_.Add(MaterialType.Glass);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Round);
                    form_type_weight_.Add(1);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Light);
                    characteristic_weight_.Add(2);

                    characteristic_.Add(Characteristic.Warm);
                    characteristic_weight_.Add(1);

                    break;
                }

            case 2:
                {
                    //天井掛け2

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 2, 2 }; //中心のグリッド座標
                    put_point_ = new int[2] { 2, 2 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 5 }; //1
                    grid_point_[2] = new int[2] { 5, 0 }; //2
                    grid_point_[3] = new int[2] { 5, 5 }; //3
                    texture_ = Resources.Load<Texture2D>("desklamp/desklamp_2_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.4F, 0.319F); //0
                    uv_coordinate_[1] = new Vector2(0.4F, 0.681F); //1
                    uv_coordinate_[2] = new Vector2(0.6F, 0.319F); //2
                    uv_coordinate_[3] = new Vector2(0.6F, 0.681F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //----------------------------------------------------

                    color_name_.Add(ColorName.Black);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(1);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.NaturalFibre);
                    material_type_weight_.Add(2);

                    material_type_.Add(MaterialType.Ceramic);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    pattern_type_.Add(PatternType.Border);
                    pattern_type_weight_.Add(1);

                    //------------------------------------------------------

                    form_type_.Add(FormType.Low);
                    form_type_weight_.Add(1);

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(1);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Light);
                    characteristic_weight_.Add(2);

                    break;
                }

            case 3:
                {
                    //天井掛け3

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 6, 1 }; //中心のグリッド座標
                    put_point_ = new int[2] { 6, 1 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 3 }; //1
                    grid_point_[2] = new int[2] { 12, 0 }; //2
                    grid_point_[3] = new int[2] { 12, 3 }; //3
                    texture_ = Resources.Load<Texture2D>("desklamp/desklamp_3_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.333F, 0.42F); //0
                    uv_coordinate_[1] = new Vector2(0.333F, 0.58F); //1
                    uv_coordinate_[2] = new Vector2(0.68F, 0.42F); //2
                    uv_coordinate_[3] = new Vector2(0.68F, 0.58F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //----------------------------------------------------

                    color_name_.Add(ColorName.Silver);
                    color_name_weight_.Add(2);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Metal);
                    material_type_weight_.Add(2);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Vertical);
                    form_type_weight_.Add(1);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Light);
                    characteristic_weight_.Add(2);

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(1);

                    break;
                }

            case 4:
                {
                    //天井掛け4

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 3, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 3, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 6 }; //1
                    grid_point_[2] = new int[2] { 6, 0 }; //2
                    grid_point_[3] = new int[2] { 6, 6 }; //3
                    texture_ = Resources.Load<Texture2D>("desklamp/desklamp_4_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.34F, 0.23F); //0
                    uv_coordinate_[1] = new Vector2(0.34F, 0.77F); //1
                    uv_coordinate_[2] = new Vector2(0.66F, 0.23F); //2
                    uv_coordinate_[3] = new Vector2(0.66F, 0.77F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //----------------------------------------------------

                    color_name_.Add(ColorName.Beige);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.Orange);
                    color_name_weight_.Add(1);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.NaturalFibre);
                    material_type_weight_.Add(2);

                    material_type_.Add(MaterialType.Metal);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    pattern_type_.Add(PatternType.Border);
                    pattern_type_weight_.Add(1);

                    //------------------------------------------------------

                    form_type_.Add(FormType.Round);
                    form_type_weight_.Add(1);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Light);
                    characteristic_weight_.Add(2);

                    characteristic_.Add(Characteristic.Luxury);
                    characteristic_weight_.Add(1);

                    characteristic_.Add(Characteristic.Warm);
                    characteristic_weight_.Add(1);

                    break;
                }

            case 5:
                {
                    //机ランプ5

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 3, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 3, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 6 }; //1
                    grid_point_[2] = new int[2] { 6, 0 }; //2
                    grid_point_[3] = new int[2] { 6, 6 }; //3
                    texture_ = Resources.Load<Texture2D>("desklamp/desklamp_5_grid"); //テクスチャはそのうち変える
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
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //----------------------------------------------------

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.Gold);
                    color_name_weight_.Add(1);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.ChemicalFibre);
                    material_type_weight_.Add(2);

                    material_type_.Add(MaterialType.Metal);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Round);
                    form_type_weight_.Add(1);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Light);
                    characteristic_weight_.Add(2);

                    characteristic_.Add(Characteristic.Luxury);
                    characteristic_weight_.Add(1);

                    break;
                }

            case 6:
                {
                    //机ランプ6

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 3, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 3, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 6 }; //1
                    grid_point_[2] = new int[2] { 6, 0 }; //2
                    grid_point_[3] = new int[2] { 6, 6 }; //3
                    texture_ = Resources.Load<Texture2D>("desklamp/desklamp_6_grid"); //テクスチャはそのうち変える
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
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //----------------------------------------------------

                    color_name_.Add(ColorName.Black);
                    color_name_weight_.Add(3);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Marble);
                    material_type_weight_.Add(2);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Round);
                    form_type_weight_.Add(1);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Light);
                    characteristic_weight_.Add(2);

                    break;
                }

            case 7:
                {
                    //机ランプ7

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 3, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 3, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 6 }; //1
                    grid_point_[2] = new int[2] { 6, 0 }; //2
                    grid_point_[3] = new int[2] { 6, 6 }; //3
                    texture_ = Resources.Load<Texture2D>("desklamp/desklamp_7_grid"); //テクスチャはそのうち変える
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
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //----------------------------------------------------

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(3);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Paper);
                    material_type_weight_.Add(2);

                    material_type_.Add(MaterialType.Plastic);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Round);
                    form_type_weight_.Add(1);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Light);
                    characteristic_weight_.Add(2);

                    characteristic_.Add(Characteristic.Warm);
                    characteristic_weight_.Add(1);

                    break;
                }

            case 8:
                {
                    //机ランプ8

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 3, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 3, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 7 }; //1
                    grid_point_[2] = new int[2] { 7, 0 }; //2
                    grid_point_[3] = new int[2] { 7, 7 }; //3
                    texture_ = Resources.Load<Texture2D>("desklamp/desklamp_8_grid"); //テクスチャはそのうち変える
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
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //----------------------------------------------------

                    color_name_.Add(ColorName.Beige);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(1);

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(1);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Glass);
                    material_type_weight_.Add(2);

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Novel);
                    form_type_weight_.Add(1);

                    form_type_.Add(FormType.Sharp);
                    form_type_weight_.Add(1);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Light);
                    characteristic_weight_.Add(2);

                    characteristic_.Add(Characteristic.Warm);
                    characteristic_weight_.Add(1);

                    break;
                }

            case 9:
                {
                    //机ランプ9

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 4, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 4, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 7 }; //1
                    grid_point_[2] = new int[2] { 8, 0 }; //2
                    grid_point_[3] = new int[2] { 8, 7 }; //3
                    texture_ = Resources.Load<Texture2D>("desklamp/desklamp_9_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.05F, 0.0F); //0
                    uv_coordinate_[1] = new Vector2(0.05F, 1.0F); //1
                    uv_coordinate_[2] = new Vector2(1.0F, 0.0F); //2
                    uv_coordinate_[3] = new Vector2(1.0F, 1.0F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //----------------------------------------------------

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.Ocher);
                    color_name_weight_.Add(2);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.NaturalFibre);
                    material_type_weight_.Add(2);

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------


                    //------------------------------------------------------

                    form_type_.Add(FormType.High);
                    form_type_weight_.Add(1);

                    form_type_.Add(FormType.Round);
                    form_type_weight_.Add(1);

                    form_type_.Add(FormType.Vertical);
                    form_type_weight_.Add(1);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Light);
                    characteristic_weight_.Add(2);

                    characteristic_.Add(Characteristic.Warm);
                    characteristic_weight_.Add(1);


                    break;
                }

            case 10:
                {
                    //机ランプ10

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 3, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 3, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 7 }; //1
                    grid_point_[2] = new int[2] { 7, 0 }; //2
                    grid_point_[3] = new int[2] { 7, 7 }; //3
                    texture_ = Resources.Load<Texture2D>("desklamp/desklamp_10_grid"); //テクスチャはそのうち変える
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
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //----------------------------------------------------

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(4);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.NaturalFibre);
                    material_type_weight_.Add(2);

                    material_type_.Add(MaterialType.Plastic);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Novel);
                    form_type_weight_.Add(1);

                    form_type_.Add(FormType.Round);
                    form_type_weight_.Add(1);

                    form_type_.Add(FormType.Vertical);
                    form_type_weight_.Add(1);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Light);
                    characteristic_weight_.Add(2);

                    characteristic_.Add(Characteristic.Warm);
                    characteristic_weight_.Add(1);


                    break;
                }

            case 11:
                {
                    //机ランプ11

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 3, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 3, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 6 }; //1
                    grid_point_[2] = new int[2] { 6, 0 }; //2
                    grid_point_[3] = new int[2] { 6, 6 }; //3
                    texture_ = Resources.Load<Texture2D>("desklamp/desklamp_11_grid"); //テクスチャはそのうち変える
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
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //----------------------------------------------------

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(1);

                    color_name_.Add(ColorName.Cream);
                    color_name_weight_.Add(1);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.NaturalFibre);
                    material_type_weight_.Add(2);

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    pattern_type_.Add(PatternType.Irregularity);
                    pattern_type_weight_.Add(1);

                    //------------------------------------------------------

                    form_type_.Add(FormType.Novel);
                    form_type_weight_.Add(1);

                    form_type_.Add(FormType.Round);
                    form_type_weight_.Add(1);

                    form_type_.Add(FormType.Vertical);
                    form_type_weight_.Add(1);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Light);
                    characteristic_weight_.Add(2);

                    characteristic_.Add(Characteristic.Warm);
                    characteristic_weight_.Add(1);

                    break;
                }

            case 12:
                {
                    //机ランプ12

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 3, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 3, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 6 }; //1
                    grid_point_[2] = new int[2] { 6, 0 }; //2
                    grid_point_[3] = new int[2] { 6, 6 }; //3
                    texture_ = Resources.Load<Texture2D>("desklamp/desklamp_12_grid"); //テクスチャはそのうち変える
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
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //----------------------------------------------------

                    color_name_.Add(ColorName.Black);
                    color_name_weight_.Add(4);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Paper);
                    material_type_weight_.Add(2);

                    material_type_.Add(MaterialType.Metal);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.High);
                    form_type_weight_.Add(1);

                    form_type_.Add(FormType.Round);
                    form_type_weight_.Add(1);

                    form_type_.Add(FormType.Vertical);
                    form_type_weight_.Add(1);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Light);
                    characteristic_weight_.Add(2);

                    break;
                }
        }
    }
}