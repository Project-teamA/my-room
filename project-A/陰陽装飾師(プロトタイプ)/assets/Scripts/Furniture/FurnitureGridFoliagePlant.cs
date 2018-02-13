//このクラスはFurnitureGridクラスの分割部分であり，観葉植物のグリッドデータを生成するGetGridDataFoliagePlantメソッドが実装されている
//
//机のFurnitureTypeはFoliagePlant
//
//方向指定なし
//
//parametaの長さは1
//parameta_[0] = ダミー
//


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //UnityEventを使用するため
using UnityEngine.EventSystems; //

public partial class FurnitureGrid : MonoBehaviour
{
    partial void GetGridDataFoliagePlant(int furniture_ID)
    {
        switch (furniture_ID)
        {
            case 1:
            default:
                {
                    //観葉植物タイプ1

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

                    texture_ = Resources.Load<Texture2D>("foliage/foliage_1_grid");
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

                    color_name_.Add(ColorName.Green);
                    color_name_weight_.Add(3);

                    color_name_.Add(ColorName.Cream);
                    color_name_weight_.Add(1);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.ArtificialFoliage);
                    material_type_weight_.Add(1);

                    material_type_.Add(MaterialType.Ceramic);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    pattern_type_.Add(PatternType.Border);
                    pattern_type_weight_.Add(1);

                    //------------------------------------------------------

                    form_type_.Add(FormType.Square);
                    form_type_weight_.Add(1);

                    form_type_.Add(FormType.Vertical);
                    form_type_weight_.Add(1);

                    //-----------------------------------------------------

                    break;
                }

            case 2:
                {
                    //観葉植物タイプ2

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 3, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 3, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 6 }; //1
                    grid_point_[2] = new int[2] { 7, 0 }; //2
                    grid_point_[3] = new int[2] { 7, 6 }; //3

                    texture_ = Resources.Load<Texture2D>("foliage/foliage_2_grid");
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

                    color_name_.Add(ColorName.Green);
                    color_name_weight_.Add(3);

                    color_name_.Add(ColorName.Ocher);
                    color_name_weight_.Add(1);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(3);

                    material_type_.Add(MaterialType.Marble);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    pattern_type_.Add(PatternType.Irregularity);
                    pattern_type_weight_.Add(1);

                    //------------------------------------------------------

                    form_type_.Add(FormType.Sharp);
                    form_type_weight_.Add(1);

                    form_type_.Add(FormType.Triangle);
                    form_type_weight_.Add(1);

                    //-----------------------------------------------------

                    break;
                }

            case 3:
                {
                    //観葉植物タイプ3

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

                    texture_ = Resources.Load<Texture2D>("foliage/foliage_3_grid");
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

                    color_name_.Add(ColorName.LightBlue);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.LightGreen);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(1);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(3);

                    material_type_.Add(MaterialType.Plastic);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Round);
                    form_type_weight_.Add(1);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Flower);
                    characteristic_weight_.Add(1);

                    characteristic_.Add(Characteristic.Smell);
                    characteristic_weight_.Add(1);

                    //------------------------------------------------------

                    break;
                }

            case 4:
                {
                    //観葉植物タイプ4

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 2, 2 }; //中心のグリッド座標
                    put_point_ = new int[2] { 2, 2 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 5 }; //1
                    grid_point_[2] = new int[2] { 4, 0 }; //2
                    grid_point_[3] = new int[2] { 4, 5 }; //3

                    texture_ = Resources.Load<Texture2D>("foliage/foliage_4_grid");
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

                    color_name_.Add(ColorName.Green);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.Pink);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(1);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.ArtificialFoliage);
                    material_type_weight_.Add(1);

                    material_type_.Add(MaterialType.Ceramic);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Round);
                    form_type_weight_.Add(1);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Flower);
                    characteristic_weight_.Add(1);

                    //------------------------------------------------------

                    break;
                }

            case 5:
                {
                    //観葉植物タイプ5

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

                    texture_ = Resources.Load<Texture2D>("foliage/foliage_5_grid");
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

                    color_name_.Add(ColorName.Green);
                    color_name_weight_.Add(3);

                    color_name_.Add(ColorName.Cream);
                    color_name_weight_.Add(1);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.ArtificialFoliage);
                    material_type_weight_.Add(1);

                    material_type_.Add(MaterialType.Ceramic);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Sharp);
                    form_type_weight_.Add(1);

                    form_type_.Add(FormType.Square);
                    form_type_weight_.Add(1);

                    //-----------------------------------------------------


                    break;
                }

            case 6:
                {
                    //観葉植物タイプ6

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

                    texture_ = Resources.Load<Texture2D>("foliage/foliage_6_grid");
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.0F, 0.001F); //0
                    uv_coordinate_[1] = new Vector2(0.0F, 1.099F); //1
                    uv_coordinate_[2] = new Vector2(1.0F, 0.001F); //2
                    uv_coordinate_[3] = new Vector2(1.0F, 1.099F); //3

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

                    color_name_.Add(ColorName.Green);
                    color_name_weight_.Add(3);

                    color_name_.Add(ColorName.Black);
                    color_name_weight_.Add(1);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(3);

                    material_type_.Add(MaterialType.Plastic);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Round);
                    form_type_weight_.Add(1);

                    //------------------------------------------------------

                    break;
                }

            case 7:
                {
                    //観葉植物タイプ7

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 2, 4 }; //中心のグリッド座標
                    put_point_ = new int[2] { 2, 4 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 8 }; //1
                    grid_point_[2] = new int[2] { 4, 0 }; //2
                    grid_point_[3] = new int[2] { 4, 8 }; //3

                    texture_ = Resources.Load<Texture2D>("foliage/foliage_7_grid");
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

                    color_name_.Add(ColorName.Green);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.Red);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(1);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(3);

                    material_type_.Add(MaterialType.Ceramic);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Oblong);
                    form_type_weight_.Add(1);

                    form_type_.Add(FormType.Square);
                    form_type_weight_.Add(1);

                    //------------------------------------------------------

                    characteristic_.Add(Characteristic.Flower);
                    characteristic_weight_.Add(1);

                    characteristic_.Add(Characteristic.Smell);
                    characteristic_weight_.Add(1);

                    break;
                }

            case 8:
                {
                    //観葉植物タイプ8

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 3, 1 }; //中心のグリッド座標
                    put_point_ = new int[2] { 3, 1 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 3 }; //1
                    grid_point_[2] = new int[2] { 7, 0 }; //2
                    grid_point_[3] = new int[2] { 7, 3 }; //3

                    texture_ = Resources.Load<Texture2D>("foliage/foliage_8_grid");
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

                    color_name_.Add(ColorName.Green);
                    color_name_weight_.Add(3);

                    color_name_.Add(ColorName.Orange);
                    color_name_weight_.Add(1);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(3);

                    material_type_.Add(MaterialType.Ceramic);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Oblong);
                    form_type_weight_.Add(1);

                    form_type_.Add(FormType.Round);
                    form_type_weight_.Add(1);

                    //------------------------------------------------------

                    break;
                }
        }
    }
}