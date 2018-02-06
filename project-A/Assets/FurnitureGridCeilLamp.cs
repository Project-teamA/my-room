//このクラスはFurnitureGridクラスの分割部分であり，天井ランプのグリッドデータを生成するGetGridDataCeilLampメソッドが実装されている
//
//天井ランプのFurnitureTypeはCeilLamp
//
//方位の指定は特になし
//
//天井掛けである．
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
    partial void GetGridDataCeilLamp(int furniture_ID)
    {
        switch (furniture_ID)
        {
            case 1:
            default:
                {
                    //天井ランプタイプ1
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

                    object_type_ = ObjectType.CeilingHook;
                    children_number_ = 1;
                    center_point_ = new int[2] { 5, 5 }; //中心のグリッド座標
                    put_point_ = new int[2] { 5, 5 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 10 }; //1
                    grid_point_[2] = new int[2] { 10, 0 }; //2
                    grid_point_[3] = new int[2] { 10, 10 }; //3

                    texture_ = Resources.Load<Texture2D>("ceillamp/ceillamp_1_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.24F, 0.05F); //0
                    uv_coordinate_[1] = new Vector2(0.24F, 0.95F); //1
                    uv_coordinate_[2] = new Vector2(0.76F, 0.05F); //2
                    uv_coordinate_[3] = new Vector2(0.76F, 0.95F); //3

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
                    //天井ランプタイプ2
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

                    object_type_ = ObjectType.CeilingHook;
                    children_number_ = 1;
                    center_point_ = new int[2] { 4, 4 }; //中心のグリッド座標
                    put_point_ = new int[2] { 4, 4 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 8 }; //1
                    grid_point_[2] = new int[2] { 8, 0 }; //2
                    grid_point_[3] = new int[2] { 8, 8 }; //3

                    texture_ = Resources.Load<Texture2D>("ceillamp/ceillamp_2_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.31F, 0.12F); //0
                    uv_coordinate_[1] = new Vector2(0.31F, 0.88F); //1
                    uv_coordinate_[2] = new Vector2(0.69F, 0.12F); //2
                    uv_coordinate_[3] = new Vector2(0.69F, 0.88F); //3

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