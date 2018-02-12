//このクラスはFurnitureGridクラスの分割部分であり，カーペットのグリッドデータを生成するGetGridDataCarpetメソッドが実装されている
//
//カーペットのFurnitureTypeはCarpet
//
//方位の指定は特になし
//ただし部屋に応じてサイズが変わる(玄関だと玄関マットとみなされるため小さくなる．)
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
    partial void GetGridDataCarpet(int furniture_ID)
    {
        switch (furniture_ID)
        {
            case 1:
            default:
                {
                    //カーペットタイプ1(長方形)

                    object_type_ = ObjectType.Rugs;
                    children_number_ = 1;
                    center_point_ = new int[2] { 18, 15 }; //中心のグリッド座標
                    put_point_ = new int[2] { 18, 15 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 30 }; //1
                    grid_point_[2] = new int[2] { 36, 0 }; //2
                    grid_point_[3] = new int[2] { 36, 30 }; //3

                    texture_ = Resources.Load<Texture2D>("carpet/carpet_1_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.05F, 0.31F); //0
                    uv_coordinate_[1] = new Vector2(0.05F, 0.69F); //1
                    uv_coordinate_[2] = new Vector2(0.95F, 0.31F); //2
                    uv_coordinate_[3] = new Vector2(0.95F, 0.69F); //3

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
                    color_name_weight_.Add(5);

                    color_name_.Add(ColorName.Cream);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.Gold);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.Green);
                    color_name_weight_.Add(2);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.ChemicalFibre);
                    material_type_weight_.Add(4);

                    //----------------------------------------------------

                    pattern_type_.Add(PatternType.Arch);
                    pattern_type_weight_.Add(3);

                    pattern_type_.Add(PatternType.Flower);
                    pattern_type_weight_.Add(2);

                    pattern_type_.Add(PatternType.Leaf);
                    pattern_type_weight_.Add(2);

                    //------------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Luxury);
                    characteristic_weight_.Add(3);

                    break;
                }

            case 2:
                {
                    //カーペットタイプ2(長方形)

                    object_type_ = ObjectType.Rugs;
                    children_number_ = 1;
                    center_point_ = new int[2] { 21, 12 }; //中心のグリッド座標
                    put_point_ = new int[2] { 21, 12 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 24 }; //1
                    grid_point_[2] = new int[2] { 42, 0 }; //2
                    grid_point_[3] = new int[2] { 42, 24 }; //3

                    texture_ = Resources.Load<Texture2D>("carpet/carpet_2_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.05F, 0.35F); //0
                    uv_coordinate_[1] = new Vector2(0.05F, 0.65F); //1
                    uv_coordinate_[2] = new Vector2(0.95F, 0.35F); //2
                    uv_coordinate_[3] = new Vector2(0.95F, 0.65F); //3

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
                    color_name_weight_.Add(5);

                    color_name_.Add(ColorName.Black);
                    color_name_weight_.Add(3);

                    color_name_.Add(ColorName.Purple);
                    color_name_weight_.Add(3);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.ChemicalFibre);
                    material_type_weight_.Add(4);

                    //----------------------------------------------------

                    pattern_type_.Add(PatternType.Flower);
                    pattern_type_weight_.Add(4);

                    pattern_type_.Add(PatternType.Irregularity);
                    pattern_type_weight_.Add(2);

                    //------------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(5);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Luxury);
                    characteristic_weight_.Add(2);

                    break;
                }

            case 3:
                {
                    //カーペットタイプ3(長方形)

                    object_type_ = ObjectType.Rugs;
                    children_number_ = 1;
                    center_point_ = new int[2] { 20, 12 }; //中心のグリッド座標
                    put_point_ = new int[2] { 20, 12 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 24 }; //1
                    grid_point_[2] = new int[2] { 40, 0 }; //2
                    grid_point_[3] = new int[2] { 40, 24 }; //3

                    texture_ = Resources.Load<Texture2D>("carpet/carpet_3_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.16F, 0.39F); //0
                    uv_coordinate_[1] = new Vector2(0.16F, 0.645F); //1
                    uv_coordinate_[2] = new Vector2(0.905F, 0.39F); //2
                    uv_coordinate_[3] = new Vector2(0.905F, 0.645F); //3

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
                    color_name_weight_.Add(5);

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(4);

                    color_name_.Add(ColorName.Black);
                    color_name_weight_.Add(2);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.NaturalFibre);
                    material_type_weight_.Add(4);

                    //----------------------------------------------------

                    pattern_type_.Add(PatternType.Flower);
                    pattern_type_weight_.Add(3);

                    pattern_type_.Add(PatternType.Leaf);
                    pattern_type_weight_.Add(3);

                    pattern_type_.Add(PatternType.Diamond);
                    pattern_type_weight_.Add(1);

                    //------------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Luxury);
                    characteristic_weight_.Add(2);

                    break;
                }

            case 4:
                {
                    //カーペットタイプ4(長方形)

                    object_type_ = ObjectType.Rugs;
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

                    texture_ = Resources.Load<Texture2D>("carpet/carpet_4_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.283F, 0.09F); //0
                    uv_coordinate_[1] = new Vector2(0.283F, 0.89F); //1
                    uv_coordinate_[2] = new Vector2(0.685F, 0.09F); //2
                    uv_coordinate_[3] = new Vector2(0.685F, 0.89F); //3

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
                    color_name_weight_.Add(8);

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(3);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.NaturalFibre);
                    material_type_weight_.Add(4);

                    //----------------------------------------------------

                    pattern_type_.Add(PatternType.Flower);
                    pattern_type_weight_.Add(4);

                    //------------------------------------------------------

                    form_type_.Add(FormType.Round);
                    form_type_weight_.Add(5);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Luxury);
                    characteristic_weight_.Add(3);

                    break;
                }

            case 5:
                {
                    //カーペットタイプ5(長方形)

                    object_type_ = ObjectType.Rugs;
                    children_number_ = 1;
                    center_point_ = new int[2] { 20, 12 }; //中心のグリッド座標
                    put_point_ = new int[2] { 20, 12 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 24 }; //1
                    grid_point_[2] = new int[2] { 40, 0 }; //2
                    grid_point_[3] = new int[2] { 40, 24 }; //3

                    texture_ = Resources.Load<Texture2D>("carpet/carpet_5_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.071F, 0.345F); //0
                    uv_coordinate_[1] = new Vector2(0.071F, 0.655F); //1
                    uv_coordinate_[2] = new Vector2(0.929F, 0.345F); //2
                    uv_coordinate_[3] = new Vector2(0.929F, 0.655F); //3

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
                    color_name_weight_.Add(7);

                    color_name_.Add(ColorName.Black);
                    color_name_weight_.Add(4);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.ChemicalFibre);
                    material_type_weight_.Add(4);

                    //----------------------------------------------------

                    pattern_type_.Add(PatternType.Flower);
                    pattern_type_weight_.Add(6);

                    //------------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Luxury);
                    characteristic_weight_.Add(2);

                    break;
                }

            case 6:
                {
                    //カーペットタイプ6(長方形)

                    object_type_ = ObjectType.Rugs;
                    children_number_ = 1;
                    center_point_ = new int[2] { 21, 14 }; //中心のグリッド座標
                    put_point_ = new int[2] { 21, 14 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 28 }; //1
                    grid_point_[2] = new int[2] { 42, 0 }; //2
                    grid_point_[3] = new int[2] { 42, 28 }; //3

                    texture_ = Resources.Load<Texture2D>("carpet/carpet_6_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.19F, 0.4F); //0
                    uv_coordinate_[1] = new Vector2(0.19F, 0.63F); //1
                    uv_coordinate_[2] = new Vector2(0.9F, 0.4F); //2
                    uv_coordinate_[3] = new Vector2(0.9F, 0.63F); //3

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
                    color_name_weight_.Add(5);

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(3);

                    color_name_.Add(ColorName.Gray);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.Ocher);
                    color_name_weight_.Add(2);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.ChemicalFibre);
                    material_type_weight_.Add(4);

                    //----------------------------------------------------

                    pattern_type_.Add(PatternType.Tile);
                    pattern_type_weight_.Add(5);

                    //------------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    //-----------------------------------------------------

                    break;
                }

            case 7:
                {
                    //カーペットタイプ7(長方形)

                    object_type_ = ObjectType.Rugs;
                    children_number_ = 1;
                    center_point_ = new int[2] { 20, 14 }; //中心のグリッド座標
                    put_point_ = new int[2] { 20, 14 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 28 }; //1
                    grid_point_[2] = new int[2] { 40, 0 }; //2
                    grid_point_[3] = new int[2] { 40, 28 }; //3

                    texture_ = Resources.Load<Texture2D>("carpet/carpet_7_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.235F, 0.15F); //0
                    uv_coordinate_[1] = new Vector2(0.235F, 0.85F); //1
                    uv_coordinate_[2] = new Vector2(0.765F, 0.15F); //2
                    uv_coordinate_[3] = new Vector2(0.765F, 0.85F); //3

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

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(3);

                    color_name_.Add(ColorName.Gray);
                    color_name_weight_.Add(3);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.NaturalFibre);
                    material_type_weight_.Add(4);

                    //----------------------------------------------------

                    pattern_type_.Add(PatternType.Tile);
                    pattern_type_weight_.Add(5);

                    pattern_type_.Add(PatternType.Border);
                    pattern_type_weight_.Add(1);

                    pattern_type_.Add(PatternType.Diamond);
                    pattern_type_weight_.Add(1);

                    pattern_type_.Add(PatternType.Stripe);
                    pattern_type_weight_.Add(1);

                    pattern_type_.Add(PatternType.Wave);
                    pattern_type_weight_.Add(1);

                    //------------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(5);

                    //-----------------------------------------------------

                    break;
                }

            case 8:
                {
                    //カーペットタイプ8(長方形)

                    object_type_ = ObjectType.Rugs;
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

                    texture_ = Resources.Load<Texture2D>("carpet/carpet_8_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.315F, 0.145F); //0
                    uv_coordinate_[1] = new Vector2(0.315F, 0.85F); //1
                    uv_coordinate_[2] = new Vector2(0.685F, 0.145F); //2
                    uv_coordinate_[3] = new Vector2(0.685F, 0.85F); //3

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

                    color_name_.Add(ColorName.Cream);
                    color_name_weight_.Add(5);

                    color_name_.Add(ColorName.Beige);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.Gold);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.Green);
                    color_name_weight_.Add(2);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.NaturalFibre);
                    material_type_weight_.Add(4);

                    //----------------------------------------------------

                    pattern_type_.Add(PatternType.Round);
                    pattern_type_weight_.Add(6);

                    //------------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(5);

                    //-----------------------------------------------------

                    break;
                }

            case 9:
                {
                    //カーペットタイプ9(長方形)

                    object_type_ = ObjectType.Rugs;
                    children_number_ = 1;
                    center_point_ = new int[2] { 21, 14 }; //中心のグリッド座標
                    put_point_ = new int[2] { 21, 14 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 28 }; //1
                    grid_point_[2] = new int[2] { 42, 0 }; //2
                    grid_point_[3] = new int[2] { 42, 28 }; //3

                    texture_ = Resources.Load<Texture2D>("carpet/carpet_9_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.1F, 0.36F); //0
                    uv_coordinate_[1] = new Vector2(0.1F, 0.64F); //1
                    uv_coordinate_[2] = new Vector2(0.9F, 0.36F); //2
                    uv_coordinate_[3] = new Vector2(0.9F, 0.64F); //3

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

                    color_name_.Add(ColorName.Gray);
                    color_name_weight_.Add(7);

                    color_name_.Add(ColorName.Black);
                    color_name_weight_.Add(4);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.ChemicalFibre);
                    material_type_weight_.Add(4);

                    //----------------------------------------------------

                    pattern_type_.Add(PatternType.Tile);
                    pattern_type_weight_.Add(6);

                    //------------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    //-----------------------------------------------------

                    break;
                }

            case 10:
                {
                    //カーペットタイプ10(長方形)

                    object_type_ = ObjectType.Rugs;
                    children_number_ = 1;
                    center_point_ = new int[2] { 21, 14 }; //中心のグリッド座標
                    put_point_ = new int[2] { 21, 14 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 28 }; //1
                    grid_point_[2] = new int[2] { 42, 0 }; //2
                    grid_point_[3] = new int[2] { 42, 28 }; //3

                    texture_ = Resources.Load<Texture2D>("carpet/carpet_10_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.1F, 0.36F); //0
                    uv_coordinate_[1] = new Vector2(0.1F, 0.64F); //1
                    uv_coordinate_[2] = new Vector2(0.9F, 0.36F); //2
                    uv_coordinate_[3] = new Vector2(0.9F, 0.64F); //3

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
                    color_name_weight_.Add(10);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.NaturalFibre);
                    material_type_weight_.Add(4);

                    //----------------------------------------------------


                    //------------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    //-----------------------------------------------------

                    break;
                }

            case 11:
                {
                    //カーペットタイプ11(長方形)

                    object_type_ = ObjectType.Rugs;
                    children_number_ = 1;
                    center_point_ = new int[2] { 21, 14 }; //中心のグリッド座標
                    put_point_ = new int[2] { 21, 14 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 28 }; //1
                    grid_point_[2] = new int[2] { 42, 0 }; //2
                    grid_point_[3] = new int[2] { 42, 28 }; //3

                    texture_ = Resources.Load<Texture2D>("carpet/carpet_11_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.08F, 0.35F); //0
                    uv_coordinate_[1] = new Vector2(0.08F, 0.65F); //1
                    uv_coordinate_[2] = new Vector2(0.92F, 0.35F); //2
                    uv_coordinate_[3] = new Vector2(0.92F, 0.65F); //3

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

                    color_name_.Add(ColorName.Gray);
                    color_name_weight_.Add(4);

                    color_name_.Add(ColorName.Beige);
                    color_name_weight_.Add(3);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.NaturalFibre);
                    material_type_weight_.Add(4);

                    //----------------------------------------------------

                    pattern_type_.Add(PatternType.Flower);
                    pattern_type_weight_.Add(4);

                    //------------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    //-----------------------------------------------------

                    break;
                }
        }
    }
}