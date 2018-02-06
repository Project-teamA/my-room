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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0;

                    //色の指定
                    color_name_ = new ColorName[1]; //色1個
                    color_name_[0] = ColorName.Blue;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ1個
                    material_type_[0] = MaterialType.Natural;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Othrewise;

                    //形状指定
                    form_type_ = new FormType[2]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;
                    form_type_[1] = FormType.Low;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Otherwise;

                    //五行の設定
                    elements_wood_ = 20; //木の気
                    elements_fire_ = 0; //火の気
                    elements_earth_ = 15; //土の気
                    elements_metal_ = 0; //金の気
                    elements_water_ = 15; //水の気

                    //陰陽の設定
                    yin_yang_ = -20; //陰陽の設定
                    break;
                }

            case 2:
                {
                    //タンスタイプ2
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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0;

                    //色の指定
                    color_name_ = new ColorName[1]; //色1個
                    color_name_[0] = ColorName.Blue;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ1個
                    material_type_[0] = MaterialType.Natural;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Othrewise;

                    //形状指定
                    form_type_ = new FormType[2]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;
                    form_type_[1] = FormType.Low;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Otherwise;

                    //五行の設定
                    elements_wood_ = 20; //木の気
                    elements_fire_ = 0; //火の気
                    elements_earth_ = 15; //土の気
                    elements_metal_ = 0; //金の気
                    elements_water_ = 15; //水の気

                    //陰陽の設定
                    yin_yang_ = -20; //陰陽の設定
                    break;
                }

            case 3:
                {
                    //タンスタイプ3
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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0;

                    //色の指定
                    color_name_ = new ColorName[1]; //色1個
                    color_name_[0] = ColorName.Blue;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ1個
                    material_type_[0] = MaterialType.Natural;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Othrewise;

                    //形状指定
                    form_type_ = new FormType[2]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;
                    form_type_[1] = FormType.Low;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Otherwise;

                    //五行の設定
                    elements_wood_ = 20; //木の気
                    elements_fire_ = 0; //火の気
                    elements_earth_ = 15; //土の気
                    elements_metal_ = 0; //金の気
                    elements_water_ = 15; //水の気

                    //陰陽の設定
                    yin_yang_ = -20; //陰陽の設定
                    break;
                }

            case 4:
                {
                    //タンスタイプ4
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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0;

                    //色の指定
                    color_name_ = new ColorName[1]; //色1個
                    color_name_[0] = ColorName.Blue;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ1個
                    material_type_[0] = MaterialType.Natural;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Othrewise;

                    //形状指定
                    form_type_ = new FormType[2]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;
                    form_type_[1] = FormType.Low;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Otherwise;

                    //五行の設定
                    elements_wood_ = 20; //木の気
                    elements_fire_ = 0; //火の気
                    elements_earth_ = 15; //土の気
                    elements_metal_ = 0; //金の気
                    elements_water_ = 15; //水の気

                    //陰陽の設定
                    yin_yang_ = -20; //陰陽の設定
                    break;
                }

            case 5:
                {
                    //タンスタイプ5
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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0;

                    //色の指定
                    color_name_ = new ColorName[1]; //色1個
                    color_name_[0] = ColorName.Blue;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ1個
                    material_type_[0] = MaterialType.Natural;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Othrewise;

                    //形状指定
                    form_type_ = new FormType[2]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;
                    form_type_[1] = FormType.Low;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Otherwise;

                    //五行の設定
                    elements_wood_ = 20; //木の気
                    elements_fire_ = 0; //火の気
                    elements_earth_ = 15; //土の気
                    elements_metal_ = 0; //金の気
                    elements_water_ = 15; //水の気

                    //陰陽の設定
                    yin_yang_ = -20; //陰陽の設定
                    break;
                }

            case 6:
                {
                    //タンスタイプ6
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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0;

                    //色の指定
                    color_name_ = new ColorName[1]; //色1個
                    color_name_[0] = ColorName.Blue;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ1個
                    material_type_[0] = MaterialType.Natural;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Othrewise;

                    //形状指定
                    form_type_ = new FormType[2]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;
                    form_type_[1] = FormType.Low;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Otherwise;

                    //五行の設定
                    elements_wood_ = 20; //木の気
                    elements_fire_ = 0; //火の気
                    elements_earth_ = 15; //土の気
                    elements_metal_ = 0; //金の気
                    elements_water_ = 15; //水の気

                    //陰陽の設定
                    yin_yang_ = -20; //陰陽の設定
                    break;
                }

            case 7:
                {
                    //タンスタイプ7
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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0;

                    //色の指定
                    color_name_ = new ColorName[1]; //色1個
                    color_name_[0] = ColorName.Blue;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ1個
                    material_type_[0] = MaterialType.Natural;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Othrewise;

                    //形状指定
                    form_type_ = new FormType[2]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;
                    form_type_[1] = FormType.Low;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Otherwise;

                    //五行の設定
                    elements_wood_ = 20; //木の気
                    elements_fire_ = 0; //火の気
                    elements_earth_ = 15; //土の気
                    elements_metal_ = 0; //金の気
                    elements_water_ = 15; //水の気

                    //陰陽の設定
                    yin_yang_ = -20; //陰陽の設定
                    break;
                }

            case 8:
                {
                    //タンスタイプ8
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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0;

                    //色の指定
                    color_name_ = new ColorName[1]; //色1個
                    color_name_[0] = ColorName.Blue;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ1個
                    material_type_[0] = MaterialType.Natural;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Othrewise;

                    //形状指定
                    form_type_ = new FormType[2]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;
                    form_type_[1] = FormType.Low;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Otherwise;

                    //五行の設定
                    elements_wood_ = 20; //木の気
                    elements_fire_ = 0; //火の気
                    elements_earth_ = 15; //土の気
                    elements_metal_ = 0; //金の気
                    elements_water_ = 15; //水の気

                    //陰陽の設定
                    yin_yang_ = -20; //陰陽の設定
                    break;
                }

            case 9:
                {
                    //タンスタイプ9
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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0;

                    //色の指定
                    color_name_ = new ColorName[1]; //色1個
                    color_name_[0] = ColorName.Blue;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ1個
                    material_type_[0] = MaterialType.Natural;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Othrewise;

                    //形状指定
                    form_type_ = new FormType[2]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;
                    form_type_[1] = FormType.Low;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Otherwise;

                    //五行の設定
                    elements_wood_ = 20; //木の気
                    elements_fire_ = 0; //火の気
                    elements_earth_ = 15; //土の気
                    elements_metal_ = 0; //金の気
                    elements_water_ = 15; //水の気

                    //陰陽の設定
                    yin_yang_ = -20; //陰陽の設定
                    break;
                }

            case 10:
                {
                    //タンスタイプ10
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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0;

                    //色の指定
                    color_name_ = new ColorName[1]; //色1個
                    color_name_[0] = ColorName.Blue;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ1個
                    material_type_[0] = MaterialType.Natural;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Othrewise;

                    //形状指定
                    form_type_ = new FormType[2]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;
                    form_type_[1] = FormType.Low;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Otherwise;

                    //五行の設定
                    elements_wood_ = 20; //木の気
                    elements_fire_ = 0; //火の気
                    elements_earth_ = 15; //土の気
                    elements_metal_ = 0; //金の気
                    elements_water_ = 15; //水の気

                    //陰陽の設定
                    yin_yang_ = -20; //陰陽の設定
                    break;
                }
        }
    }

}
