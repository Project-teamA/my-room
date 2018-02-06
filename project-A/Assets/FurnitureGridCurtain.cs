//このクラスはFurnitureGridクラスの分割部分であり，家電のグリッドデータを生成するGetGridDataCurtainメソッドが実装されている
//
//机のFurnitureTypeはCurtain
//
//窓側が北向きになるように設定
//
//parametaの長さは1
//
//カーテンは壁掛け
//(ここからは自分の勝手な判断)
//



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //UnityEventを使用するため
using UnityEngine.EventSystems; //

public partial class FurnitureGrid : MonoBehaviour
{
    partial void GetGridDataCurtain(int furniture_ID)
    {
        switch (furniture_ID)
        {
            case 1:
            default:
                {
                    //カーテン1
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
                    object_type_ = ObjectType.WallMounted;
                    children_number_ = 1;
                    center_point_ = new int[2] { 12, 1 }; //中心のグリッド座標
                    put_point_ = new int[2] { 12, 1 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 2 }; //1
                    grid_point_[2] = new int[2] { 25, 0 }; //2
                    grid_point_[3] = new int[2] { 25, 2 }; //3

                    texture_ = Resources.Load<Texture2D>("curtain/curtain_1_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.26F, 0.45F); //0
                    uv_coordinate_[1] = new Vector2(0.26F, 0.55F); //1
                    uv_coordinate_[2] = new Vector2(0.74F, 0.45F); //2
                    uv_coordinate_[3] = new Vector2(0.74F, 0.55F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //色の指定
                    color_name_ = new ColorName[1]; //色
                    color_name_[0] = ColorName.Black;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ
                    material_type_[0] = MaterialType.Glass;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Othrewise;

                    //形状指定
                    form_type_ = new FormType[1]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Sound;

                    //五行の設定
                    elements_wood_ = 40; //木の気
                    elements_fire_ = 10; //火の気
                    elements_earth_ = 5; //土の気
                    elements_metal_ = 2; //金の気
                    elements_water_ = 10; //水の気

                    //陰陽の設定
                    yin_yang_ = 63; //陰陽の設定
                    break;
                }

            case 2:
                {
                    //カーテン2
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
                    object_type_ = ObjectType.WallMounted;
                    children_number_ = 1;
                    center_point_ = new int[2] { 12, 1 }; //中心のグリッド座標
                    put_point_ = new int[2] { 12, 1 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 2 }; //1
                    grid_point_[2] = new int[2] { 25, 0 }; //2
                    grid_point_[3] = new int[2] { 25, 2 }; //3

                    texture_ = Resources.Load<Texture2D>("curtain/curtain_2_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.29F, 0.46F); //0
                    uv_coordinate_[1] = new Vector2(0.29F, 0.54F); //1
                    uv_coordinate_[2] = new Vector2(0.66F, 0.46F); //2
                    uv_coordinate_[3] = new Vector2(0.66F, 0.54F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //色の指定
                    color_name_ = new ColorName[1]; //色
                    color_name_[0] = ColorName.Black;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ
                    material_type_[0] = MaterialType.Glass;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Othrewise;

                    //形状指定
                    form_type_ = new FormType[1]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Sound;

                    //五行の設定
                    elements_wood_ = 40; //木の気
                    elements_fire_ = 10; //火の気
                    elements_earth_ = 5; //土の気
                    elements_metal_ = 2; //金の気
                    elements_water_ = 10; //水の気

                    //陰陽の設定
                    yin_yang_ = 63; //陰陽の設定
                    break;
                }

            case 3:
                {
                    //カーテン3
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
                    object_type_ = ObjectType.WallMounted;
                    children_number_ = 1;
                    center_point_ = new int[2] { 12, 1 }; //中心のグリッド座標
                    put_point_ = new int[2] { 12, 1 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 2 }; //1
                    grid_point_[2] = new int[2] { 25, 0 }; //2
                    grid_point_[3] = new int[2] { 25, 2 }; //3

                    texture_ = Resources.Load<Texture2D>("curtain/curtain_3_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.19F, 0.45F); //0
                    uv_coordinate_[1] = new Vector2(0.19F, 0.51F); //1
                    uv_coordinate_[2] = new Vector2(0.81F, 0.45F); //2
                    uv_coordinate_[3] = new Vector2(0.81F, 0.51F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //色の指定
                    color_name_ = new ColorName[1]; //色
                    color_name_[0] = ColorName.Black;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ
                    material_type_[0] = MaterialType.Glass;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Othrewise;

                    //形状指定
                    form_type_ = new FormType[1]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Sound;

                    //五行の設定
                    elements_wood_ = 40; //木の気
                    elements_fire_ = 10; //火の気
                    elements_earth_ = 5; //土の気
                    elements_metal_ = 2; //金の気
                    elements_water_ = 10; //水の気

                    //陰陽の設定
                    yin_yang_ = 63; //陰陽の設定
                    break;
                }

            case 4:
                {
                    //カーテン4
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
                    object_type_ = ObjectType.WallMounted;
                    children_number_ = 1;
                    center_point_ = new int[2] { 12, 1 }; //中心のグリッド座標
                    put_point_ = new int[2] { 12, 1 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 2 }; //1
                    grid_point_[2] = new int[2] { 25, 0 }; //2
                    grid_point_[3] = new int[2] { 25, 2 }; //3

                    texture_ = Resources.Load<Texture2D>("curtain/curtain_4_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.14F, 0.51F); //0
                    uv_coordinate_[1] = new Vector2(0.14F, 0.54F); //1
                    uv_coordinate_[2] = new Vector2(0.86F, 0.51F); //2
                    uv_coordinate_[3] = new Vector2(0.86F, 0.54F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //色の指定
                    color_name_ = new ColorName[1]; //色
                    color_name_[0] = ColorName.Black;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ
                    material_type_[0] = MaterialType.Glass;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Othrewise;

                    //形状指定
                    form_type_ = new FormType[1]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Sound;

                    //五行の設定
                    elements_wood_ = 40; //木の気
                    elements_fire_ = 10; //火の気
                    elements_earth_ = 5; //土の気
                    elements_metal_ = 2; //金の気
                    elements_water_ = 10; //水の気

                    //陰陽の設定
                    yin_yang_ = 63; //陰陽の設定
                    break;
                }

            case 5:
                {
                    //カーテン5
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
                    object_type_ = ObjectType.WallMounted;
                    children_number_ = 1;
                    center_point_ = new int[2] { 12, 1 }; //中心のグリッド座標
                    put_point_ = new int[2] { 12, 1 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 2 }; //1
                    grid_point_[2] = new int[2] { 25, 0 }; //2
                    grid_point_[3] = new int[2] { 25, 2 }; //3

                    texture_ = Resources.Load<Texture2D>("curtain/curtain_5_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.3F, 0.45F); //0
                    uv_coordinate_[1] = new Vector2(0.3F, 0.51F); //1
                    uv_coordinate_[2] = new Vector2(0.7F, 0.45F); //2
                    uv_coordinate_[3] = new Vector2(0.7F, 0.51F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //色の指定
                    color_name_ = new ColorName[1]; //色
                    color_name_[0] = ColorName.Black;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ
                    material_type_[0] = MaterialType.Glass;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Othrewise;

                    //形状指定
                    form_type_ = new FormType[1]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Sound;

                    //五行の設定
                    elements_wood_ = 40; //木の気
                    elements_fire_ = 10; //火の気
                    elements_earth_ = 5; //土の気
                    elements_metal_ = 2; //金の気
                    elements_water_ = 10; //水の気

                    //陰陽の設定
                    yin_yang_ = 63; //陰陽の設定
                    break;
                }

            case 6:
                {
                    //カーテン6
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
                    object_type_ = ObjectType.WallMounted;
                    children_number_ = 1;
                    center_point_ = new int[2] { 12, 1 }; //中心のグリッド座標
                    put_point_ = new int[2] { 12, 1 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 2 }; //1
                    grid_point_[2] = new int[2] { 25, 0 }; //2
                    grid_point_[3] = new int[2] { 25, 2 }; //3

                    texture_ = Resources.Load<Texture2D>("curtain/curtain_6_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.285F, 0.465F); //0
                    uv_coordinate_[1] = new Vector2(0.285F, 0.531F); //1
                    uv_coordinate_[2] = new Vector2(0.715F, 0.465F); //2
                    uv_coordinate_[3] = new Vector2(0.715F, 0.531F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //色の指定
                    color_name_ = new ColorName[1]; //色
                    color_name_[0] = ColorName.Black;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ
                    material_type_[0] = MaterialType.Glass;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Othrewise;

                    //形状指定
                    form_type_ = new FormType[1]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Sound;

                    //五行の設定
                    elements_wood_ = 40; //木の気
                    elements_fire_ = 10; //火の気
                    elements_earth_ = 5; //土の気
                    elements_metal_ = 2; //金の気
                    elements_water_ = 10; //水の気

                    //陰陽の設定
                    yin_yang_ = 63; //陰陽の設定
                    break;
                }
        }
    }
}
