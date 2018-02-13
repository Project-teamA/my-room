//このクラスはFurnitureGridクラスの分割部分であり，テーブルのグリッドデータを生成するGetGridDataTableメソッドが実装されている
//
//テーブルのFurnitureTypeはTable
//
//方位の指定は特になし
//
//parametaの長さは1
//parameta_[0] = ダミー
//
//(ここからは自分の勝手な判断)
//
//table_5_gridは除外

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //UnityEventを使用するため
using UnityEngine.EventSystems; //

public partial class FurnitureGrid : MonoBehaviour
{
    partial void GetGridDataTable(int furniture_ID)
    {
        switch (furniture_ID)
        {
            case 1:
            default:
                {
                    //テーブルタイプ1(木製)

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 13, 13 }; //中心のグリッド座標
                    put_point_ = new int[2] { 13, 13 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 26 }; //1
                    grid_point_[2] = new int[2] { 26, 0 }; //2
                    grid_point_[3] = new int[2] { 26, 26 }; //3

                    texture_ = Resources.Load<Texture2D>("table/table_1_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.26F, 0.035F); //0
                    uv_coordinate_[1] = new Vector2(0.26F, 0.965F); //1
                    uv_coordinate_[2] = new Vector2(0.74F, 0.035F); //2
                    uv_coordinate_[3] = new Vector2(0.74F, 0.965F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //-------------------------------------------------

                    color_name_.Add(ColorName.Red);
                    color_name_weight_.Add(7);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.Plastic);
                    material_type_weight_.Add(6);

                    //-------------------------------------------------

                    pattern_type_.Add(PatternType.Dot);
                    pattern_type_weight_.Add(3);

                    //-------------------------------------------------

                    form_type_.Add(FormType.Round);
                    form_type_weight_.Add(5);

                    //--------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(1);

                    //--------------------------------------------------

                    break;
                }

            case 2:
                {
                    //テーブルタイプ2(木製)

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 10, 12 }; //中心のグリッド座標
                    put_point_ = new int[2] { 10, 12 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 24 }; //1
                    grid_point_[2] = new int[2] { 20, 0 }; //2
                    grid_point_[3] = new int[2] { 20, 24 }; //3

                    texture_ = Resources.Load<Texture2D>("table/table_2_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.36F, 0.185F); //0
                    uv_coordinate_[1] = new Vector2(0.36F, 0.815F); //1
                    uv_coordinate_[2] = new Vector2(0.64F, 0.185F); //2
                    uv_coordinate_[3] = new Vector2(0.64F, 0.815F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //-------------------------------------------------

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(6);

                    color_name_.Add(ColorName.Red);
                    color_name_weight_.Add(2);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(5);

                    material_type_.Add(MaterialType.Leather);
                    material_type_weight_.Add(2);

                    material_type_.Add(MaterialType.Ceramic);
                    material_type_weight_.Add(1);

                    //-------------------------------------------------

                    //-------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    //--------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    characteristic_.Add(Characteristic.Smell);
                    characteristic_weight_.Add(2);

                    //--------------------------------------------------

                    break;
                }

            case 3:
                {
                    //テーブルタイプ2(木製)

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 12, 12 }; //中心のグリッド座標
                    put_point_ = new int[2] { 12, 12 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 24 }; //1
                    grid_point_[2] = new int[2] { 24, 0 }; //2
                    grid_point_[3] = new int[2] { 24, 24 }; //3

                    texture_ = Resources.Load<Texture2D>("table/table_3_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.315F, 0.145F); //0
                    uv_coordinate_[1] = new Vector2(0.315F, 0.855F); //1
                    uv_coordinate_[2] = new Vector2(0.685F, 0.145F); //2
                    uv_coordinate_[3] = new Vector2(0.685F, 0.855F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //-------------------------------------------------

                    color_name_.Add(ColorName.DarkGray);
                    color_name_weight_.Add(7);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.Plastic);
                    material_type_weight_.Add(6);

                    //-------------------------------------------------

                    pattern_type_.Add(PatternType.Stripe);
                    pattern_type_weight_.Add(3);

                    //-------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(5);

                    //--------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(1);

                    //--------------------------------------------------

                    break;
                }

            case 4:
                {
                    //テーブルタイプ4

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 8, 6 }; //中心のグリッド座標
                    put_point_ = new int[2] { 8, 6 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 13 }; //1
                    grid_point_[2] = new int[2] { 16, 0 }; //2
                    grid_point_[3] = new int[2] { 16, 13 }; //3

                    texture_ = Resources.Load<Texture2D>("table/table_4_grid"); //テクスチャはそのうち変える
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

                    //-------------------------------------------------

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(7);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(5);

                    material_type_.Add(MaterialType.NaturalFibre);
                    material_type_weight_.Add(2);

                    //-------------------------------------------------

                    pattern_type_.Add(PatternType.Leaf);
                    pattern_type_weight_.Add(2);

                    //-------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    //--------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    //--------------------------------------------------

                    break;
                }

            case 5:
                {
                    //テーブルタイプ5

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 12, 12 }; //中心のグリッド座標
                    put_point_ = new int[2] { 12, 12 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 23 }; //1
                    grid_point_[2] = new int[2] { 24, 0 }; //2
                    grid_point_[3] = new int[2] { 24, 23 }; //3

                    texture_ = Resources.Load<Texture2D>("table/table_5_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.0F, 0.0F); //0
                    uv_coordinate_[1] = new Vector2(0.0F, 1.0F); //1
                    uv_coordinate_[2] = new Vector2(0.995F, 0.0F); //2
                    uv_coordinate_[3] = new Vector2(0.995F, 1.0F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //-------------------------------------------------

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(4);

                    color_name_.Add(ColorName.Ocher);
                    color_name_weight_.Add(4);

                    color_name_.Add(ColorName.Green);
                    color_name_weight_.Add(1);

                    color_name_.Add(ColorName.Purple);
                    color_name_weight_.Add(1);

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(1);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(6);

                    material_type_.Add(MaterialType.Ceramic);
                    material_type_weight_.Add(1);

                    material_type_.Add(MaterialType.Glass);
                    material_type_weight_.Add(1);

                    material_type_.Add(MaterialType.NaturalFibre);
                    material_type_weight_.Add(1);

                    //-------------------------------------------------

                    //-------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(5);

                    //--------------------------------------------------

                    characteristic_.Add(Characteristic.Flower);
                    characteristic_weight_.Add(3);

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    characteristic_.Add(Characteristic.Western);
                    characteristic_weight_.Add(2);

                    //--------------------------------------------------

                    furniture_subtype_.Add(FurnitureType.foliage);

                    break;
                }

            case 6:
                {
                    //テーブルタイプ6

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 15, 11 }; //中心のグリッド座標
                    put_point_ = new int[2] { 15, 11 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 22 }; //1
                    grid_point_[2] = new int[2] { 30, 0 }; //2
                    grid_point_[3] = new int[2] { 30, 22 }; //3

                    texture_ = Resources.Load<Texture2D>("table/table_6_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.025F, 0.328F); //0
                    uv_coordinate_[1] = new Vector2(0.025F, 0.672F); //1
                    uv_coordinate_[2] = new Vector2(0.975F, 0.328F); //2
                    uv_coordinate_[3] = new Vector2(0.975F, 0.672F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //-------------------------------------------------

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(4);

                    color_name_.Add(ColorName.Cream);
                    color_name_weight_.Add(4);

                    color_name_.Add(ColorName.Green);
                    color_name_weight_.Add(1);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(6);

                    material_type_.Add(MaterialType.Ceramic);
                    material_type_weight_.Add(2);

                    material_type_.Add(MaterialType.Metal);
                    material_type_weight_.Add(1);

                    //-------------------------------------------------

                    pattern_type_.Add(PatternType.Zigzag);
                    pattern_type_weight_.Add(3);

                    //-------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(5);

                    //--------------------------------------------------

                    characteristic_.Add(Characteristic.Luxury);
                    characteristic_weight_.Add(3);

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    characteristic_.Add(Characteristic.Western);
                    characteristic_weight_.Add(2);

                    characteristic_.Add(Characteristic.Light);
                    characteristic_weight_.Add(1);

                    //--------------------------------------------------

                    break;
                }

            case 7:
                {
                    //テーブルタイプ7

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 16, 9 }; //中心のグリッド座標
                    put_point_ = new int[2] { 16, 9 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 18 }; //1
                    grid_point_[2] = new int[2] { 32, 0 }; //2
                    grid_point_[3] = new int[2] { 32, 18 }; //3

                    texture_ = Resources.Load<Texture2D>("table/table_7_grid"); //テクスチャはそのうち変える
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

                    //-------------------------------------------------

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(3);

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.Silver);
                    color_name_weight_.Add(2);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(3);

                    material_type_.Add(MaterialType.ChemicalFibre);
                    material_type_weight_.Add(2);

                    material_type_.Add(MaterialType.Metal);
                    material_type_weight_.Add(2);

                    //-------------------------------------------------

                    //-------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(6);

                    //--------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    //--------------------------------------------------

                    break;
                }

            case 8:
                {
                    //テーブルタイプ2(木製)

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 12, 12 }; //中心のグリッド座標
                    put_point_ = new int[2] { 12, 12 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 24 }; //1
                    grid_point_[2] = new int[2] { 24, 0 }; //2
                    grid_point_[3] = new int[2] { 24, 24 }; //3

                    texture_ = Resources.Load<Texture2D>("table/table_8_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.28F, 0.055F); //0
                    uv_coordinate_[1] = new Vector2(0.28F, 0.945F); //1
                    uv_coordinate_[2] = new Vector2(0.72F, 0.055F); //2
                    uv_coordinate_[3] = new Vector2(0.72F, 0.945F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //-------------------------------------------------

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(5);

                    color_name_.Add(ColorName.Gray);
                    color_name_weight_.Add(3);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.Glass);
                    material_type_weight_.Add(3);

                    material_type_.Add(MaterialType.NaturalFibre);
                    material_type_weight_.Add(2);

                    material_type_.Add(MaterialType.Metal);
                    material_type_weight_.Add(2);

                    //-------------------------------------------------

                    //-------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(5);

                    //--------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    //--------------------------------------------------

                    break;
                }

            case 9:
                {
                    //テーブルタイプ9

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 13, 13 }; //中心のグリッド座標
                    put_point_ = new int[2] { 13, 13 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 26 }; //1
                    grid_point_[2] = new int[2] { 26, 0 }; //2
                    grid_point_[3] = new int[2] { 26, 26 }; //3

                    texture_ = Resources.Load<Texture2D>("table/table_9_grid"); //テクスチャはそのうち変える
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

                    //-------------------------------------------------

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(5);

                    color_name_.Add(ColorName.Beige);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.Cream);
                    color_name_weight_.Add(2);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.NaturalFibre);
                    material_type_weight_.Add(4);

                    material_type_.Add(MaterialType.Marble);
                    material_type_weight_.Add(3);

                    material_type_.Add(MaterialType.Ceramic);
                    material_type_weight_.Add(2);

                    //-------------------------------------------------

                    //-------------------------------------------------

                    form_type_.Add(FormType.Round);
                    form_type_weight_.Add(5);

                    //--------------------------------------------------

                    characteristic_.Add(Characteristic.Luxury);
                    characteristic_weight_.Add(4);

                    //--------------------------------------------------

                    break;
                }

            case 10:
                {
                    //テーブルタイプ10

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 12, 10 }; //中心のグリッド座標
                    put_point_ = new int[2] { 12, 10 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 21 }; //1
                    grid_point_[2] = new int[2] { 24, 0 }; //2
                    grid_point_[3] = new int[2] { 24, 21 }; //3

                    texture_ = Resources.Load<Texture2D>("table/table_10_grid"); //テクスチャはそのうち変える
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

                    //-------------------------------------------------

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(7);

                    color_name_.Add(ColorName.Black);
                    color_name_weight_.Add(1);

                    color_name_.Add(ColorName.LightBlue);
                    color_name_weight_.Add(1);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.Marble);
                    material_type_weight_.Add(4);

                    material_type_.Add(MaterialType.NaturalFibre);
                    material_type_weight_.Add(2);

                    material_type_.Add(MaterialType.ArtificialFoliage);
                    material_type_weight_.Add(1);

                    material_type_.Add(MaterialType.Ceramic);
                    material_type_weight_.Add(1);

                    material_type_.Add(MaterialType.Glass);
                    material_type_weight_.Add(1);

                    //-------------------------------------------------

                    pattern_type_.Add(PatternType.Flower);
                    pattern_type_weight_.Add(3);

                    pattern_type_.Add(PatternType.Arch);
                    pattern_type_weight_.Add(1);

                    //-------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(5);

                    //--------------------------------------------------

                    characteristic_.Add(Characteristic.Luxury);
                    characteristic_weight_.Add(4);

                    //--------------------------------------------------

                    break;
                }
        }
    }
}