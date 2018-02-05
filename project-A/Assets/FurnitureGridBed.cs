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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 12; //横の長さの半分
                    parameta_[1] = 12; //縦の長さの半分
                    parameta_[2] = 0; //左下の頂点番号
                    parameta_[3] = 1; //左上の頂点番号
                    parameta_[4] = 2; //右下の頂点番号
                    parameta_[5] = 3; //右上の頂点番号

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
                    //ベッドタイプ2
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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 12; //横の長さの半分
                    parameta_[1] = 12; //縦の長さの半分
                    parameta_[2] = 0; //左下の頂点番号
                    parameta_[3] = 1; //左上の頂点番号
                    parameta_[4] = 2; //右下の頂点番号
                    parameta_[5] = 3; //右上の頂点番号

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
                    //ベッドタイプ3
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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 12; //横の長さの半分
                    parameta_[1] = 12; //縦の長さの半分
                    parameta_[2] = 0; //左下の頂点番号
                    parameta_[3] = 1; //左上の頂点番号
                    parameta_[4] = 2; //右下の頂点番号
                    parameta_[5] = 3; //右上の頂点番号

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
                    //ベッドタイプ4
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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 12; //横の長さの半分
                    parameta_[1] = 12; //縦の長さの半分
                    parameta_[2] = 0; //左下の頂点番号
                    parameta_[3] = 1; //左上の頂点番号
                    parameta_[4] = 2; //右下の頂点番号
                    parameta_[5] = 3; //右上の頂点番号

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
                    //ベッドタイプ5
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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 12; //横の長さの半分
                    parameta_[1] = 12; //縦の長さの半分
                    parameta_[2] = 0; //左下の頂点番号
                    parameta_[3] = 1; //左上の頂点番号
                    parameta_[4] = 2; //右下の頂点番号
                    parameta_[5] = 3; //右上の頂点番号

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
                    //ベッドタイプ6
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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 12; //横の長さの半分
                    parameta_[1] = 12; //縦の長さの半分
                    parameta_[2] = 0; //左下の頂点番号
                    parameta_[3] = 1; //左上の頂点番号
                    parameta_[4] = 2; //右下の頂点番号
                    parameta_[5] = 3; //右上の頂点番号

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
                    //ベッドタイプ7
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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 12; //横の長さの半分
                    parameta_[1] = 12; //縦の長さの半分
                    parameta_[2] = 0; //左下の頂点番号
                    parameta_[3] = 1; //左上の頂点番号
                    parameta_[4] = 2; //右下の頂点番号
                    parameta_[5] = 3; //右上の頂点番号

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