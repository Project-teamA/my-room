//このクラスはFurnitureGridクラスの分割部分であり，机のグリッドデータを生成するGetGridDataDeskメソッドが実装されている
//
//机のFurnitureTypeはDesk
//
//椅子の位置が南になるように形状設定
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
    partial void GetGridDataDesk(int furniture_ID)
    {
        switch (furniture_ID)
        {
            case 1:
            default:
                {
                    //机タイプ1

                    object_type_ = ObjectType.Normal;
                    children_number_ = 2;
                    center_point_ = new int[2] { 10, 9 }; //中心のグリッド座標
                    put_point_ = new int[2] { 10, 9 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 8;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 21 }; //1
                    grid_point_[2] = new int[2] { 10, 0 }; //2
                    grid_point_[3] = new int[2] { 10, 21 }; //3

                    grid_point_[4] = new int[2] { 10, 7 }; //4
                    grid_point_[5] = new int[2] { 10, 14 }; //5
                    grid_point_[6] = new int[2] { 17, 7 }; //6
                    grid_point_[7] = new int[2] { 17, 14 }; //7

                    texture_ = Resources.Load<Texture2D>("desk/desk_1_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.12F, 0.3F); //0
                    uv_coordinate_[1] = new Vector2(0.12F, 0.7F); //1
                    uv_coordinate_[2] = new Vector2(0.5F, 0.3F); //2
                    uv_coordinate_[3] = new Vector2(0.5F, 0.7F); //3

                    uv_coordinate_[4] = new Vector2(0.5F, 0.432F); //0
                    uv_coordinate_[5] = new Vector2(0.5F, 0.568F); //1
                    uv_coordinate_[6] = new Vector2(0.805F, 0.432F); //2
                    uv_coordinate_[7] = new Vector2(0.805F, 0.568F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };
                    triangles_[1] = new int[4] { 4, 5, 6, 7 };

                    //枠線
                    outline_index_ = new int[16] { 0, 2, 2, 4, 4, 6, 6, 7, 7, 5, 5, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[8] { false, false, true, false, true, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //----------------------------------------------------

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(5);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Marble);
                    material_type_weight_.Add(5);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Oblong);
                    form_type_weight_.Add(4);

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(4);

                    form_type_.Add(FormType.High);
                    form_type_weight_.Add(3);

                    form_type_.Add(FormType.Novel);
                    form_type_weight_.Add(1);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    //------------------------------------------------------

                    furniture_subtype_.Add(FurnitureType.Cabinet);

                    break;
                }

            case 2:
                {
                    //机タイプ2

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 8, 4 }; //中心のグリッド座標
                    put_point_ = new int[2] { 8, 4 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 8 }; //1
                    grid_point_[2] = new int[2] { 16, 0 }; //2
                    grid_point_[3] = new int[2] { 16, 8 }; //3

                    texture_ = Resources.Load<Texture2D>("desk/desk_2_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.31F, 0.31F); //0
                    uv_coordinate_[1] = new Vector2(0.31F, 0.69F); //1
                    uv_coordinate_[2] = new Vector2(0.69F, 0.31F); //2
                    uv_coordinate_[3] = new Vector2(0.69F, 0.69F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { true, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //----------------------------------------------------

                    color_name_.Add(ColorName.Ocher);
                    color_name_weight_.Add(5);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(5);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(3);

                    form_type_.Add(FormType.Oblong);
                    form_type_weight_.Add(2);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    break;
                }

            case 3:
                {
                    //机タイプ3

                    object_type_ = ObjectType.Normal;
                    children_number_ = 2;
                    center_point_ = new int[2] { 8, 9 }; //中心のグリッド座標
                    put_point_ = new int[2] { 8, 9 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 8;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 5 }; //1
                    grid_point_[2] = new int[2] { 8, 0 }; //2
                    grid_point_[3] = new int[2] { 8, 5 }; //3

                    grid_point_[4] = new int[2] { 0, 5 }; //4
                    grid_point_[5] = new int[2] { 0, 13 }; //5
                    grid_point_[6] = new int[2] { 16, 5 }; //6
                    grid_point_[7] = new int[2] { 16, 13 }; //7

                    texture_ = Resources.Load<Texture2D>("desk/desk_3_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.35F, 0.29F); //0
                    uv_coordinate_[1] = new Vector2(0.35F, 0.45F); //1
                    uv_coordinate_[2] = new Vector2(0.5F, 0.29F); //2
                    uv_coordinate_[3] = new Vector2(0.5F, 0.45F); //3

                    uv_coordinate_[4] = new Vector2(0.35F, 0.45F); //4
                    uv_coordinate_[5] = new Vector2(0.35F, 0.73F); //5
                    uv_coordinate_[6] = new Vector2(0.65F, 0.45F); //6
                    uv_coordinate_[7] = new Vector2(0.65F, 0.73F); //7

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };
                    triangles_[1] = new int[4] { 4, 5, 6, 7 };

                    //枠線
                    outline_index_ = new int[16] { 0, 2, 2, 3, 3, 6, 6, 7, 7, 5, 5, 4, 4, 1, 1, 0 };
                    blueflag_index_ = new bool[8] { false, false, true, false, false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //----------------------------------------------------

                    color_name_.Add(ColorName.Ocher);
                    color_name_weight_.Add(5);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(5);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(3);

                    form_type_.Add(FormType.Oblong);
                    form_type_weight_.Add(2);

                    form_type_.Add(FormType.High);
                    form_type_weight_.Add(1);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    characteristic_.Add(Characteristic.Clutter);
                    characteristic_weight_.Add(1);

                    break;
                }

            case 4:
                {
                    //机タイプ4

                    object_type_ = ObjectType.Normal;
                    children_number_ = 2;
                    center_point_ = new int[2] { 8, 9 }; //中心のグリッド座標
                    put_point_ = new int[2] { 8, 9 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 8;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 14 }; //1
                    grid_point_[2] = new int[2] { 14, 0 }; //2
                    grid_point_[3] = new int[2] { 14, 14 }; //3

                    grid_point_[4] = new int[2] { 0, 14 }; //4
                    grid_point_[5] = new int[2] { 0, 28 }; //5
                    grid_point_[6] = new int[2] { 35, 14 }; //6
                    grid_point_[7] = new int[2] { 35, 28 }; //7

                    texture_ = Resources.Load<Texture2D>("desk/desk_4_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.0F, 0.0F); //0
                    uv_coordinate_[1] = new Vector2(0.0F, 0.5F); //1
                    uv_coordinate_[2] = new Vector2(0.4F, 0.0F); //2
                    uv_coordinate_[3] = new Vector2(0.4F, 0.5F); //3

                    uv_coordinate_[4] = new Vector2(0.0F, 0.5F); //4
                    uv_coordinate_[5] = new Vector2(0.0F, 1.0F); //5
                    uv_coordinate_[6] = new Vector2(1.0F, 0.5F); //6
                    uv_coordinate_[7] = new Vector2(1.0F, 1.0F); //7

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };
                    triangles_[1] = new int[4] { 4, 5, 6, 7 };

                    //枠線
                    outline_index_ = new int[16] { 0, 2, 2, 3, 3, 6, 6, 7, 7, 5, 5, 4, 4, 1, 1, 0 };
                    blueflag_index_ = new bool[8] { false, true, true, false, false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //----------------------------------------------------

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(6);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(7);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(5);

                    form_type_.Add(FormType.Oblong);
                    form_type_weight_.Add(3);

                    form_type_.Add(FormType.High);
                    form_type_weight_.Add(3);

                    form_type_.Add(FormType.Novel);
                    form_type_weight_.Add(1);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    break;
                }

            case 5:
                {
                    //机タイプ5

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 8, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 8, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 7 }; //1
                    grid_point_[2] = new int[2] { 16, 0 }; //2
                    grid_point_[3] = new int[2] { 16, 7 }; //3

                    texture_ = Resources.Load<Texture2D>("desk/desk_5_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.31F, 0.335F); //0
                    uv_coordinate_[1] = new Vector2(0.31F, 0.665F); //1
                    uv_coordinate_[2] = new Vector2(0.69F, 0.335F); //2
                    uv_coordinate_[3] = new Vector2(0.69F, 0.665F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { true, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //----------------------------------------------------

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(4);

                    color_name_.Add(ColorName.Silver);
                    color_name_weight_.Add(1);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(3);

                    material_type_.Add(MaterialType.Metal);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(3);

                    form_type_.Add(FormType.Oblong);
                    form_type_weight_.Add(3);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    break;
                }

            case 6:
                {
                    //机タイプ6

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 8, 4 }; //中心のグリッド座標
                    put_point_ = new int[2] { 8, 4 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 8 }; //1
                    grid_point_[2] = new int[2] { 17, 0 }; //2
                    grid_point_[3] = new int[2] { 17, 8 }; //3

                    texture_ = Resources.Load<Texture2D>("desk/desk_6_grid"); //テクスチャはそのうち変える
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
                    parameta_[0] = 0; //ダミー

                    //----------------------------------------------------

                    color_name_.Add(ColorName.Black);
                    color_name_weight_.Add(4);

                    color_name_.Add(ColorName.Red);
                    color_name_weight_.Add(2);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Marble);
                    material_type_weight_.Add(4);

                    material_type_.Add(MaterialType.Glass);
                    material_type_weight_.Add(1);

                    material_type_.Add(MaterialType.Metal);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(3);

                    form_type_.Add(FormType.Oblong);
                    form_type_weight_.Add(3);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Sound);
                    characteristic_weight_.Add(3);

                    characteristic_.Add(Characteristic.Clutter);
                    characteristic_weight_.Add(2);

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    //------------------------------------------------------

                    furniture_subtype_.Add(FurnitureType.ConsumerElectronics);

                    break;
                }

            case 7:
                {
                    //机タイプ7

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 8, 4 }; //中心のグリッド座標
                    put_point_ = new int[2] { 8, 4 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 8 }; //1
                    grid_point_[2] = new int[2] { 17, 0 }; //2
                    grid_point_[3] = new int[2] { 17, 8 }; //3

                    texture_ = Resources.Load<Texture2D>("desk/desk_7_grid"); //テクスチャはそのうち変える
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
                    parameta_[0] = 0; //ダミー

                    //----------------------------------------------------

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(4);

                    color_name_.Add(ColorName.Silver);
                    color_name_weight_.Add(1);

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(1);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(3);

                    material_type_.Add(MaterialType.Metal);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Oblong);
                    form_type_weight_.Add(3);

                    form_type_.Add(FormType.Ellipse);
                    form_type_weight_.Add(2);

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(2);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    //------------------------------------------------------

                    break;
                }

            case 8:
                {
                    //机タイプ8

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 8, 4 }; //中心のグリッド座標
                    put_point_ = new int[2] { 8, 4 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 9 }; //1
                    grid_point_[2] = new int[2] { 24, 0 }; //2
                    grid_point_[3] = new int[2] { 24, 9 }; //3

                    texture_ = Resources.Load<Texture2D>("desk/desk_8_grid"); //テクスチャはそのうち変える
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
                    parameta_[0] = 0; //ダミー

                    //----------------------------------------------------

                    color_name_.Add(ColorName.Ocher);
                    color_name_weight_.Add(3);

                    color_name_.Add(ColorName.DarkGray);
                    color_name_weight_.Add(3);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Plastic);
                    material_type_weight_.Add(3);

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(3);

                    material_type_.Add(MaterialType.Glass);
                    material_type_weight_.Add(1);

                    material_type_.Add(MaterialType.Metal);
                    material_type_weight_.Add(1);

                    material_type_.Add(MaterialType.Paper);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(3);

                    form_type_.Add(FormType.Oblong);
                    form_type_weight_.Add(3);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Sound);
                    characteristic_weight_.Add(3);

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    //------------------------------------------------------

                    break;
                }

            case 9:
                {
                    //机タイプ9

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 9, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 9, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 7 }; //1
                    grid_point_[2] = new int[2] { 19, 0 }; //2
                    grid_point_[3] = new int[2] { 19, 7 }; //3

                    texture_ = Resources.Load<Texture2D>("desk/desk_9_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.12F, 0.425F); //0
                    uv_coordinate_[1] = new Vector2(0.12F, 0.575F); //1
                    uv_coordinate_[2] = new Vector2(0.88F, 0.425F); //2
                    uv_coordinate_[3] = new Vector2(0.88F, 0.575F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { true, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //----------------------------------------------------

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(4);

                    color_name_.Add(ColorName.Blue);
                    color_name_weight_.Add(1);

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(1);

                    color_name_.Add(ColorName.Ocher);
                    color_name_weight_.Add(1);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Marble);
                    material_type_weight_.Add(5);

                    material_type_.Add(MaterialType.Ceramic);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(3);

                    form_type_.Add(FormType.Oblong);
                    form_type_weight_.Add(3);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    characteristic_.Add(Characteristic.Luxury);
                    characteristic_weight_.Add(2);

                    //------------------------------------------------------

                    break;
                }

            case 10:
                {
                    //机タイプ10

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 8, 5 }; //中心のグリッド座標
                    put_point_ = new int[2] { 8, 5 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 11 }; //1
                    grid_point_[2] = new int[2] { 17, 0 }; //2
                    grid_point_[3] = new int[2] { 17, 11 }; //3

                    texture_ = Resources.Load<Texture2D>("desk/desk_10_grid"); //テクスチャはそのうち変える
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
                    parameta_[0] = 0; //ダミー

                    //----------------------------------------------------

                    color_name_.Add(ColorName.Brown);
                    color_name_weight_.Add(4);

                    color_name_.Add(ColorName.Gold);
                    color_name_weight_.Add(1);

                    color_name_.Add(ColorName.Purple);
                    color_name_weight_.Add(1);

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(1);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(5);

                    material_type_.Add(MaterialType.Paper);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(2);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    characteristic_.Add(Characteristic.Luxury);
                    characteristic_weight_.Add(2);

                    //------------------------------------------------------

                    break;
                }

            case 11:
                {
                    //机タイプ11

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 15, 24 }; //中心のグリッド座標
                    put_point_ = new int[2] { 15, 24 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 49 }; //1
                    grid_point_[2] = new int[2] { 31, 0 }; //2
                    grid_point_[3] = new int[2] { 31, 49 }; //3

                    texture_ = Resources.Load<Texture2D>("desk/desk_11_grid"); //テクスチャはそのうち変える
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
                    parameta_[0] = 0; //ダミー

                    //----------------------------------------------------

                    color_name_.Add(ColorName.Ocher);
                    color_name_weight_.Add(6);

                    color_name_.Add(ColorName.Black);
                    color_name_weight_.Add(3);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Wooden);
                    material_type_weight_.Add(5);

                    material_type_.Add(MaterialType.Glass);
                    material_type_weight_.Add(2);

                    material_type_.Add(MaterialType.NaturalFibre);
                    material_type_weight_.Add(2);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(3);

                    form_type_.Add(FormType.High);
                    form_type_weight_.Add(3);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    characteristic_.Add(Characteristic.Sound);
                    characteristic_weight_.Add(3);

                    //------------------------------------------------------

                    furniture_subtype_.Add(FurnitureType.Cabinet);
                    furniture_subtype_.Add(FurnitureType.ConsumerElectronics);


                    break;
                }

            case 12:
                {
                    //机タイプ12

                    object_type_ = ObjectType.Normal;
                    children_number_ = 2;
                    center_point_ = new int[2] { 8, 9 }; //中心のグリッド座標
                    put_point_ = new int[2] { 8, 9 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 8;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 12 }; //1
                    grid_point_[2] = new int[2] { 11, 0 }; //2
                    grid_point_[3] = new int[2] { 11, 12 }; //3

                    grid_point_[4] = new int[2] { 0, 12 }; //4
                    grid_point_[5] = new int[2] { 0, 22 }; //5
                    grid_point_[6] = new int[2] { 24, 12 }; //6
                    grid_point_[7] = new int[2] { 24, 22 }; //7

                    texture_ = Resources.Load<Texture2D>("desk/desk_12_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.0F, 0.0F); //0
                    uv_coordinate_[1] = new Vector2(0.0F, 0.544F); //1
                    uv_coordinate_[2] = new Vector2(0.458F, 0.0F); //2
                    uv_coordinate_[3] = new Vector2(0.458F, 0.544F); //3

                    uv_coordinate_[4] = new Vector2(0.0F, 0.544F); //4
                    uv_coordinate_[5] = new Vector2(0.0F, 1.0F); //5
                    uv_coordinate_[6] = new Vector2(1.0F, 0.544F); //6
                    uv_coordinate_[7] = new Vector2(1.0F, 1.0F); //7

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };
                    triangles_[1] = new int[4] { 4, 5, 6, 7 };

                    //枠線
                    outline_index_ = new int[16] { 0, 2, 2, 3, 3, 6, 6, 7, 7, 5, 5, 4, 4, 1, 1, 0 };
                    blueflag_index_ = new bool[8] { false, true, true, false, false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //----------------------------------------------------

                    color_name_.Add(ColorName.Black);
                    color_name_weight_.Add(4);

                    color_name_.Add(ColorName.Red);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(1);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Marble);
                    material_type_weight_.Add(6);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(3);

                    form_type_.Add(FormType.High);
                    form_type_weight_.Add(2);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    //------------------------------------------------------

                    furniture_subtype_.Add(FurnitureType.Cabinet);

                    break;
                }

        }
    }
}