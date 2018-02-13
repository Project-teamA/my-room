//このクラスはFurnitureGridクラスの分割部分であり，ベッドのグリッドデータを生成するGetGridDataBedメソッドが実装されている
//
//ベッドのFurnitureTypeはBed
//
//parametaの長さは6
//parameta_[0] = 横の長さの半分
//parameta_[1] = 縦の長さの半分
//parameta_[2] = 左下の頂点番号
//parameta_[3] = 左上の頂点番号
//parameta_[4] = 右下の頂点番号
//parameta_[5] = 右上の頂点番号
//
//必ず横の長さは偶数でなければならない
//
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
    partial void GetGridDataBed(int furniture_ID)
    {
        switch (furniture_ID)
        {
            case 1:
            default:
                {
                    //ベッドタイプ1
                    //
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

                    texture_ = Resources.Load<Texture2D>("bed/bed_1_grid");
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.18F, 0.34F); //0
                    uv_coordinate_[1] = new Vector2(0.18F, 0.7F); //1
                    uv_coordinate_[2] = new Vector2(0.82F, 0.34F); //2
                    uv_coordinate_[3] = new Vector2(0.82F, 0.7F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 12; //横の長さの半分
                    parameta_[1] = 12; //縦の長さの半分
                    parameta_[2] = 0; //左下の頂点番号
                    parameta_[3] = 1; //左上の頂点番号
                    parameta_[4] = 2; //右下の頂点番号
                    parameta_[5] = 3; //右上の頂点番号

                    //----------------------------------------------------

                    color_name_.Add(ColorName.Black);
                    color_name_weight_.Add(5);

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(5);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Cotton);
                    material_type_weight_.Add(4);

                    material_type_.Add(MaterialType.NaturalFibre);
                    material_type_weight_.Add(2);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Low);
                    form_type_weight_.Add(4);

                    form_type_.Add(FormType.Square);
                    form_type_weight_.Add(4);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Soft);
                    characteristic_weight_.Add(3);

                    break;
                }

            case 2:
                {
                    //ベッドタイプ2

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

                    texture_ = Resources.Load<Texture2D>("bed/bed_2_grid");
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.3F, 0.16F); //0
                    uv_coordinate_[1] = new Vector2(0.3F, 0.88F); //1
                    uv_coordinate_[2] = new Vector2(0.7F, 0.16F); //2
                    uv_coordinate_[3] = new Vector2(0.7F, 0.88F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 12; //横の長さの半分
                    parameta_[1] = 12; //縦の長さの半分
                    parameta_[2] = 0; //左下の頂点番号
                    parameta_[3] = 1; //左上の頂点番号
                    parameta_[4] = 2; //右下の頂点番号
                    parameta_[5] = 3; //右上の頂点番号

                    color_name_.Add(ColorName.Beige);
                    color_name_weight_.Add(4);

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(3);

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(3);

                    //------------------------------------------------------------

                    material_type_.Add(MaterialType.Cotton);
                    material_type_weight_.Add(4);

                    material_type_.Add(MaterialType.NaturalFibre);
                    material_type_weight_.Add(2);

                    //-------------------------------------------------------------

                    //---------------------------------------------------

                    form_type_.Add(FormType.Low);
                    form_type_weight_.Add(4);

                    form_type_.Add(FormType.Square);
                    form_type_weight_.Add(4);

                    //--------------------------------------------------------------

                    characteristic_.Add(Characteristic.Soft);
                    characteristic_weight_.Add(3);

                    characteristic_.Add(Characteristic.Warm);
                    characteristic_weight_.Add(3);

                    break;
                }

            case 3:
                {
                    //ベッドタイプ3

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 12, 12 }; //中心のグリッド座標
                    put_point_ = new int[2] { 12, 12 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 25 }; //1
                    grid_point_[2] = new int[2] { 23, 0 }; //2
                    grid_point_[3] = new int[2] { 23, 25 }; //3

                    texture_ = Resources.Load<Texture2D>("bed/bed_3_grid");
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.31F, 0.10F); //0
                    uv_coordinate_[1] = new Vector2(0.31F, 0.88F); //1
                    uv_coordinate_[2] = new Vector2(0.67F, 0.10F); //2
                    uv_coordinate_[3] = new Vector2(0.67F, 0.88F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 12; //横の長さの半分
                    parameta_[1] = 12; //縦の長さの半分
                    parameta_[2] = 0; //左下の頂点番号
                    parameta_[3] = 1; //左上の頂点番号
                    parameta_[4] = 2; //右下の頂点番号
                    parameta_[5] = 3; //右上の頂点番号

                    color_name_.Add(ColorName.Gray);
                    color_name_weight_.Add(4);

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(4);

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(2);

                    //---------------------------------------------------

                    material_type_.Add(MaterialType.Cotton);
                    material_type_weight_.Add(4);

                    material_type_.Add(MaterialType.NaturalFibre);
                    material_type_weight_.Add(2);

                    //---------------------------------------------------

                    //---------------------------------------------------

                    form_type_.Add(FormType.Low);
                    form_type_weight_.Add(4);

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    //---------------------------------------------------

                    characteristic_.Add(Characteristic.Luxury);
                    characteristic_weight_.Add(3);

                    break;
                }

            case 4:
                {
                    //ベッドタイプ4

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 10, 12 }; //中心のグリッド座標
                    put_point_ = new int[2] { 10, 12 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 25 }; //1
                    grid_point_[2] = new int[2] { 20, 0 }; //2
                    grid_point_[3] = new int[2] { 20, 25 }; //3

                    texture_ = Resources.Load<Texture2D>("bed/bed_4_grid");
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.24F, 0.33F); //0
                    uv_coordinate_[1] = new Vector2(0.24F, 0.72F); //1
                    uv_coordinate_[2] = new Vector2(0.76F, 0.33F); //2
                    uv_coordinate_[3] = new Vector2(0.76F, 0.72F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 12; //横の長さの半分
                    parameta_[1] = 12; //縦の長さの半分
                    parameta_[2] = 0; //左下の頂点番号
                    parameta_[3] = 1; //左上の頂点番号
                    parameta_[4] = 2; //右下の頂点番号
                    parameta_[5] = 3; //右上の頂点番号

                    color_name_.Add(ColorName.Gray);
                    color_name_weight_.Add(6);

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(4);

                    //---------------------------------------------------

                    material_type_.Add(MaterialType.Cotton);
                    material_type_weight_.Add(4);

                    material_type_.Add(MaterialType.ChemicalFibre);
                    material_type_weight_.Add(2);

                    //---------------------------------------------------

                    //---------------------------------------------------

                    form_type_.Add(FormType.Low);
                    form_type_weight_.Add(4);

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    //---------------------------------------------------

                    break;
                }

            case 5:
                {
                    //ベッドタイプ5

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 10, 12 }; //中心のグリッド座標
                    put_point_ = new int[2] { 10, 12 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 25 }; //1
                    grid_point_[2] = new int[2] { 20, 0 }; //2
                    grid_point_[3] = new int[2] { 20, 25 }; //3

                    texture_ = Resources.Load<Texture2D>("bed/bed_5_grid");
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.34F, 0.118F); //0
                    uv_coordinate_[1] = new Vector2(0.34F, 0.85F); //1
                    uv_coordinate_[2] = new Vector2(0.66F, 0.118F); //2
                    uv_coordinate_[3] = new Vector2(0.66F, 0.85F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 12; //横の長さの半分
                    parameta_[1] = 12; //縦の長さの半分
                    parameta_[2] = 0; //左下の頂点番号
                    parameta_[3] = 1; //左上の頂点番号
                    parameta_[4] = 2; //右下の頂点番号
                    parameta_[5] = 3; //右上の頂点番号

                    color_name_.Add(ColorName.Black);
                    color_name_weight_.Add(4);

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(4);

                    color_name_.Add(ColorName.Gray);
                    color_name_weight_.Add(2);

                    //---------------------------------------------------

                    material_type_.Add(MaterialType.Cotton);
                    material_type_weight_.Add(4);

                    material_type_.Add(MaterialType.ChemicalFibre);
                    material_type_weight_.Add(2);

                    //---------------------------------------------------

                    //---------------------------------------------------

                    form_type_.Add(FormType.Low);
                    form_type_weight_.Add(4);

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    //---------------------------------------------------

                    break;
                }

            case 6:
                {
                    //ベッドタイプ6

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 9, 12 }; //中心のグリッド座標
                    put_point_ = new int[2] { 9, 12 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 25 }; //1
                    grid_point_[2] = new int[2] { 18, 0 }; //2
                    grid_point_[3] = new int[2] { 18, 25 }; //3

                    texture_ = Resources.Load<Texture2D>("bed/bed_6_grid");
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.15F, 0.31F); //0
                    uv_coordinate_[1] = new Vector2(0.15F, 0.72F); //1
                    uv_coordinate_[2] = new Vector2(0.76F, 0.31F); //2
                    uv_coordinate_[3] = new Vector2(0.76F, 0.72F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 12; //横の長さの半分
                    parameta_[1] = 12; //縦の長さの半分
                    parameta_[2] = 0; //左下の頂点番号
                    parameta_[3] = 1; //左上の頂点番号
                    parameta_[4] = 2; //右下の頂点番号
                    parameta_[5] = 3; //右上の頂点番号

                    color_name_.Add(ColorName.DarkGray);
                    color_name_weight_.Add(6);

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(3);

                    color_name_.Add(ColorName.Black);
                    color_name_weight_.Add(1);

                    //------------------------------------------------

                    material_type_.Add(MaterialType.Cotton);
                    material_type_weight_.Add(4);

                    material_type_.Add(MaterialType.NaturalFibre);
                    material_type_weight_.Add(2);

                    //------------------------------------------------

                    pattern_type_.Add(PatternType.Check);
                    pattern_type_weight_.Add(1);

                    pattern_type_.Add(PatternType.Stripe);
                    pattern_type_weight_.Add(1);

                    //--------------------------------------------------

                    form_type_.Add(FormType.Low);
                    form_type_weight_.Add(4);

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    //--------------------------------------------------

                    characteristic_.Add(Characteristic.Soft);
                    characteristic_weight_.Add(3);

                    break;
                }

            case 7:
                {
                    //ベッドタイプ7

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 10, 12 }; //中心のグリッド座標
                    put_point_ = new int[2] { 10, 12 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 25 }; //1
                    grid_point_[2] = new int[2] { 20, 0 }; //2
                    grid_point_[3] = new int[2] { 20, 25 }; //3

                    texture_ = Resources.Load<Texture2D>("bed/bed_7_grid");
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.25F, 0.35F); //0
                    uv_coordinate_[1] = new Vector2(0.25F, 0.68F); //1
                    uv_coordinate_[2] = new Vector2(0.75F, 0.35F); //2
                    uv_coordinate_[3] = new Vector2(0.75F, 0.68F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 12; //横の長さの半分
                    parameta_[1] = 12; //縦の長さの半分
                    parameta_[2] = 0; //左下の頂点番号
                    parameta_[3] = 1; //左上の頂点番号
                    parameta_[4] = 2; //右下の頂点番号
                    parameta_[5] = 3; //右上の頂点番号

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(5);

                    color_name_.Add(ColorName.Gray);
                    color_name_weight_.Add(4);

                    color_name_.Add(ColorName.DarkGray);
                    color_name_weight_.Add(1);

                    //------------------------------------------------

                    material_type_.Add(MaterialType.Cotton);
                    material_type_weight_.Add(4);

                    material_type_.Add(MaterialType.NaturalFibre);
                    material_type_weight_.Add(2);

                    //------------------------------------------------

                    //------------------------------------------------

                    form_type_.Add(FormType.Low);
                    form_type_weight_.Add(4);

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    //------------------------------------------------

                    characteristic_.Add(Characteristic.Luxury);
                    characteristic_weight_.Add(3);

                    break;
                }

            case 8:
                {
                    //ベッドタイプ8

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 17, 15 }; //中心のグリッド座標
                    put_point_ = new int[2] { 17, 15 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 30 }; //1
                    grid_point_[2] = new int[2] { 34, 0 }; //2
                    grid_point_[3] = new int[2] { 34, 30 }; //3

                    texture_ = Resources.Load<Texture2D>("bed/bed_8_grid");
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
                    parameta_[0] = 15; //横の長さの半分
                    parameta_[1] = 17; //縦の長さの半分
                    parameta_[2] = 0; //左下の頂点番号
                    parameta_[3] = 1; //左上の頂点番号
                    parameta_[4] = 2; //右下の頂点番号
                    parameta_[5] = 3; //右上の頂点番号

                    color_name_.Add(ColorName.Beige);
                    color_name_weight_.Add(3);

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(3);

                    color_name_.Add(ColorName.Green);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.Ocher);
                    color_name_weight_.Add(2);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.Cotton);
                    material_type_weight_.Add(4);

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(3);

                    material_type_.Add(MaterialType.NaturalFibre);
                    material_type_weight_.Add(2);

                    //-------------------------------------------------

                    pattern_type_.Add(PatternType.Flower);
                    pattern_type_weight_.Add(4);

                    //-------------------------------------------------

                    form_type_.Add(FormType.Square);
                    form_type_weight_.Add(4);

                    form_type_.Add(FormType.Low);
                    form_type_weight_.Add(3);

                    //--------------------------------------------------

                    characteristic_.Add(Characteristic.Light);
                    characteristic_weight_.Add(3);

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    //--------------------------------------------------

                    furniture_subtype_.Add(FurnitureType.desklamp);


                    break;
                }

            case 9:
                {
                    //ベッドタイプ9

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 15, 15 }; //中心のグリッド座標
                    put_point_ = new int[2] { 15, 15 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 30 }; //1
                    grid_point_[2] = new int[2] { 30, 0 }; //2
                    grid_point_[3] = new int[2] { 30, 30 }; //3

                    texture_ = Resources.Load<Texture2D>("bed/bed_9_grid");
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
                    parameta_[0] = 15; //横の長さの半分
                    parameta_[1] = 15; //縦の長さの半分
                    parameta_[2] = 0; //左下の頂点番号
                    parameta_[3] = 1; //左上の頂点番号
                    parameta_[4] = 2; //右下の頂点番号
                    parameta_[5] = 3; //右上の頂点番号

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(3);

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(3);

                    color_name_.Add(ColorName.Black);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.Ocher);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.Red);
                    color_name_weight_.Add(2);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.Cotton);
                    material_type_weight_.Add(4);

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(3);

                    material_type_.Add(MaterialType.ChemicalFibre);
                    material_type_weight_.Add(2);

                    //-------------------------------------------------

                    pattern_type_.Add(PatternType.Check);
                    pattern_type_weight_.Add(4);

                    pattern_type_.Add(PatternType.Diamond);
                    pattern_type_weight_.Add(4);

                    //-------------------------------------------------

                    form_type_.Add(FormType.Low);
                    form_type_weight_.Add(4);

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    //--------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    characteristic_.Add(Characteristic.Light);
                    characteristic_weight_.Add(2);

                    //--------------------------------------------------

                    furniture_subtype_.Add(FurnitureType.desklamp);

                    break;
                }

            case 10:
                {
                    //ベッドタイプ10

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 11, 12 }; //中心のグリッド座標
                    put_point_ = new int[2] { 11, 12 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 25 }; //1
                    grid_point_[2] = new int[2] { 22, 0 }; //2
                    grid_point_[3] = new int[2] { 22, 25 }; //3

                    texture_ = Resources.Load<Texture2D>("bed/bed_10_grid");
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
                    parameta_[0] = 12; //横の長さの半分
                    parameta_[1] = 12; //縦の長さの半分
                    parameta_[2] = 0; //左下の頂点番号
                    parameta_[3] = 1; //左上の頂点番号
                    parameta_[4] = 2; //右下の頂点番号
                    parameta_[5] = 3; //右上の頂点番号

                    color_name_.Add(ColorName.Gray);
                    color_name_weight_.Add(7);

                    color_name_.Add(ColorName.Black);
                    color_name_weight_.Add(3);

                    //-------------------------------------------------

                    material_type_.Add(MaterialType.Cotton);
                    material_type_weight_.Add(4);

                    material_type_.Add(MaterialType.NaturalFibre);
                    material_type_weight_.Add(2);

                    //-------------------------------------------------

                    pattern_type_.Add(PatternType.Irregularity);
                    pattern_type_weight_.Add(4);

                    //-------------------------------------------------

                    form_type_.Add(FormType.Low);
                    form_type_weight_.Add(4);

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    //--------------------------------------------------

                    characteristic_.Add(Characteristic.Luxury);
                    characteristic_weight_.Add(3);

                    characteristic_.Add(Characteristic.Light);
                    characteristic_weight_.Add(2);

                    break;
                }
        }
    }
}