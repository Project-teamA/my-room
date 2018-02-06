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
                    //カラー = 
                    //材質 =
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //
                    //五行
                    //
                    //木 = 
                    //火 = 
                    //土 = 
                    //金 = 
                    //水 = 
                    //
                    //陰陽 = 

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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //色の指定
                    color_name_ = new ColorName[3]; //色
                    color_name_[0] = ColorName.Blue;
                    color_name_[1] = ColorName.Green;
                    color_name_[2] = ColorName.White;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ
                    material_type_[0] = MaterialType.Natural;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Dot;

                    //形状指定
                    form_type_ = new FormType[1]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Soft;

                    //五行の設定
                    elements_wood_ = 18; //木の気
                    elements_fire_ = 0; //火の気
                    elements_earth_ = 18; //土の気
                    elements_metal_ = 10; //金の気
                    elements_water_ = 26; //水の気

                    //陰陽の設定
                    yin_yang_ = -12; //陰陽の設定
                    break;
                }

            case 2:
                {
                    //カーペットタイプ2(長方形)
                    //カラー = 
                    //材質 =
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //
                    //五行
                    //
                    //木 = 
                    //火 = 
                    //土 = 
                    //金 = 
                    //水 = 
                    //
                    //陰陽 = 


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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //色の指定
                    color_name_ = new ColorName[3]; //色
                    color_name_[0] = ColorName.Blue;
                    color_name_[1] = ColorName.Green;
                    color_name_[2] = ColorName.White;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ
                    material_type_[0] = MaterialType.Natural;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Dot;

                    //形状指定
                    form_type_ = new FormType[1]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Soft;

                    //五行の設定
                    elements_wood_ = 18; //木の気
                    elements_fire_ = 0; //火の気
                    elements_earth_ = 18; //土の気
                    elements_metal_ = 10; //金の気
                    elements_water_ = 26; //水の気

                    //陰陽の設定
                    yin_yang_ = -12; //陰陽の設定
                    break;
                }

            case 3:
                {
                    //カーペットタイプ3(長方形)
                    //カラー = 
                    //材質 =
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //
                    //五行
                    //
                    //木 = 
                    //火 = 
                    //土 = 
                    //金 = 
                    //水 = 
                    //
                    //陰陽 = 


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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //色の指定
                    color_name_ = new ColorName[3]; //色
                    color_name_[0] = ColorName.Blue;
                    color_name_[1] = ColorName.Green;
                    color_name_[2] = ColorName.White;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ
                    material_type_[0] = MaterialType.Natural;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Dot;

                    //形状指定
                    form_type_ = new FormType[1]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Soft;

                    //五行の設定
                    elements_wood_ = 18; //木の気
                    elements_fire_ = 0; //火の気
                    elements_earth_ = 18; //土の気
                    elements_metal_ = 10; //金の気
                    elements_water_ = 26; //水の気

                    //陰陽の設定
                    yin_yang_ = -12; //陰陽の設定
                    break;
                }

            case 4:
                {
                    //カーペットタイプ4(長方形)
                    //カラー = 
                    //材質 =
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //
                    //五行
                    //
                    //木 = 
                    //火 = 
                    //土 = 
                    //金 = 
                    //水 = 
                    //
                    //陰陽 = 


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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //色の指定
                    color_name_ = new ColorName[3]; //色
                    color_name_[0] = ColorName.Blue;
                    color_name_[1] = ColorName.Green;
                    color_name_[2] = ColorName.White;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ
                    material_type_[0] = MaterialType.Natural;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Dot;

                    //形状指定
                    form_type_ = new FormType[1]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Soft;

                    //五行の設定
                    elements_wood_ = 18; //木の気
                    elements_fire_ = 0; //火の気
                    elements_earth_ = 18; //土の気
                    elements_metal_ = 10; //金の気
                    elements_water_ = 26; //水の気

                    //陰陽の設定
                    yin_yang_ = -12; //陰陽の設定
                    break;
                }

            case 5:
                {
                    //カーペットタイプ5(長方形)
                    //カラー = 
                    //材質 =
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //
                    //五行
                    //
                    //木 = 
                    //火 = 
                    //土 = 
                    //金 = 
                    //水 = 
                    //
                    //陰陽 = 


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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //色の指定
                    color_name_ = new ColorName[3]; //色
                    color_name_[0] = ColorName.Blue;
                    color_name_[1] = ColorName.Green;
                    color_name_[2] = ColorName.White;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ
                    material_type_[0] = MaterialType.Natural;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Dot;

                    //形状指定
                    form_type_ = new FormType[1]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Soft;

                    //五行の設定
                    elements_wood_ = 18; //木の気
                    elements_fire_ = 0; //火の気
                    elements_earth_ = 18; //土の気
                    elements_metal_ = 10; //金の気
                    elements_water_ = 26; //水の気

                    //陰陽の設定
                    yin_yang_ = -12; //陰陽の設定
                    break;
                }

            case 6:
                {
                    //カーペットタイプ6(長方形)
                    //カラー = 
                    //材質 =
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //
                    //五行
                    //
                    //木 = 
                    //火 = 
                    //土 = 
                    //金 = 
                    //水 = 
                    //
                    //陰陽 = 


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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //色の指定
                    color_name_ = new ColorName[3]; //色
                    color_name_[0] = ColorName.Blue;
                    color_name_[1] = ColorName.Green;
                    color_name_[2] = ColorName.White;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ
                    material_type_[0] = MaterialType.Natural;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Dot;

                    //形状指定
                    form_type_ = new FormType[1]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Soft;

                    //五行の設定
                    elements_wood_ = 18; //木の気
                    elements_fire_ = 0; //火の気
                    elements_earth_ = 18; //土の気
                    elements_metal_ = 10; //金の気
                    elements_water_ = 26; //水の気

                    //陰陽の設定
                    yin_yang_ = -12; //陰陽の設定
                    break;
                }

            case 7:
                {
                    //カーペットタイプ7(長方形)
                    //カラー = 
                    //材質 =
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //
                    //五行
                    //
                    //木 = 
                    //火 = 
                    //土 = 
                    //金 = 
                    //水 = 
                    //
                    //陰陽 = 


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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //色の指定
                    color_name_ = new ColorName[3]; //色
                    color_name_[0] = ColorName.Blue;
                    color_name_[1] = ColorName.Green;
                    color_name_[2] = ColorName.White;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ
                    material_type_[0] = MaterialType.Natural;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Dot;

                    //形状指定
                    form_type_ = new FormType[1]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Soft;

                    //五行の設定
                    elements_wood_ = 18; //木の気
                    elements_fire_ = 0; //火の気
                    elements_earth_ = 18; //土の気
                    elements_metal_ = 10; //金の気
                    elements_water_ = 26; //水の気

                    //陰陽の設定
                    yin_yang_ = -12; //陰陽の設定
                    break;
                }

            case 8:
                {
                    //カーペットタイプ8(長方形)
                    //カラー = 
                    //材質 =
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //
                    //五行
                    //
                    //木 = 
                    //火 = 
                    //土 = 
                    //金 = 
                    //水 = 
                    //
                    //陰陽 = 


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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //色の指定
                    color_name_ = new ColorName[3]; //色
                    color_name_[0] = ColorName.Blue;
                    color_name_[1] = ColorName.Green;
                    color_name_[2] = ColorName.White;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ
                    material_type_[0] = MaterialType.Natural;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Dot;

                    //形状指定
                    form_type_ = new FormType[1]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Soft;

                    //五行の設定
                    elements_wood_ = 18; //木の気
                    elements_fire_ = 0; //火の気
                    elements_earth_ = 18; //土の気
                    elements_metal_ = 10; //金の気
                    elements_water_ = 26; //水の気

                    //陰陽の設定
                    yin_yang_ = -12; //陰陽の設定
                    break;
                }

            case 9:
                {
                    //カーペットタイプ9(長方形)
                    //カラー = 
                    //材質 =
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //
                    //五行
                    //
                    //木 = 
                    //火 = 
                    //土 = 
                    //金 = 
                    //水 = 
                    //
                    //陰陽 = 


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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };
                    
                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //色の指定
                    color_name_ = new ColorName[3]; //色
                    color_name_[0] = ColorName.Blue;
                    color_name_[1] = ColorName.Green;
                    color_name_[2] = ColorName.White;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ
                    material_type_[0] = MaterialType.Natural;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Dot;

                    //形状指定
                    form_type_ = new FormType[1]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Soft;

                    //五行の設定
                    elements_wood_ = 18; //木の気
                    elements_fire_ = 0; //火の気
                    elements_earth_ = 18; //土の気
                    elements_metal_ = 10; //金の気
                    elements_water_ = 26; //水の気

                    //陰陽の設定
                    yin_yang_ = -12; //陰陽の設定
                    break;
                }

            case 10:
                {
                    //カーペットタイプ10(長方形)
                    //カラー = 
                    //材質 =
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //
                    //五行
                    //
                    //木 = 
                    //火 = 
                    //土 = 
                    //金 = 
                    //水 = 
                    //
                    //陰陽 = 


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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //色の指定
                    color_name_ = new ColorName[3]; //色
                    color_name_[0] = ColorName.Blue;
                    color_name_[1] = ColorName.Green;
                    color_name_[2] = ColorName.White;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ
                    material_type_[0] = MaterialType.Natural;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Dot;

                    //形状指定
                    form_type_ = new FormType[1]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Soft;

                    //五行の設定
                    elements_wood_ = 18; //木の気
                    elements_fire_ = 0; //火の気
                    elements_earth_ = 18; //土の気
                    elements_metal_ = 10; //金の気
                    elements_water_ = 26; //水の気

                    //陰陽の設定
                    yin_yang_ = -12; //陰陽の設定
                    break;
                }

            case 11:
                {
                    //カーペットタイプ11(長方形)
                    //カラー = 
                    //材質 =
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //
                    //五行
                    //
                    //木 = 
                    //火 = 
                    //土 = 
                    //金 = 
                    //水 = 
                    //
                    //陰陽 = 


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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //色の指定
                    color_name_ = new ColorName[3]; //色
                    color_name_[0] = ColorName.Blue;
                    color_name_[1] = ColorName.Green;
                    color_name_[2] = ColorName.White;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ
                    material_type_[0] = MaterialType.Natural;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Dot;

                    //形状指定
                    form_type_ = new FormType[1]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Soft;

                    //五行の設定
                    elements_wood_ = 18; //木の気
                    elements_fire_ = 0; //火の気
                    elements_earth_ = 18; //土の気
                    elements_metal_ = 10; //金の気
                    elements_water_ = 26; //水の気

                    //陰陽の設定
                    yin_yang_ = -12; //陰陽の設定
                    break;
                }
        }
    }
}