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
                    //カラー = 茶色, 1
                    //材質 = 木製, 1
                    //模様 = なし(その他), 1
                    //形状 = 楕円形，横長, 2
                    //その他 = なし, 1
                    //
                    //五行
                    //
                    //木 =
                    //火 = 
                    //土 = 
                    //金 =
                    //水 =
                    //
                    //陰陽 = 0

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 16, 16 }; //中心のグリッド座標
                    put_point_ = new int[2] { 16, 16 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 32 }; //1
                    grid_point_[2] = new int[2] { 32, 0 }; //2
                    grid_point_[3] = new int[2] { 32, 32 }; //3

                    texture_ = Resources.Load<Texture2D>("table/table_1_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.25F, 0.02F); //0
                    uv_coordinate_[1] = new Vector2(0.25F, 0.98F); //1
                    uv_coordinate_[2] = new Vector2(0.75F, 0.02F); //2
                    uv_coordinate_[3] = new Vector2(0.75F, 0.98F); //3

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
                    color_name_[0] = ColorName.Brown;

                    //材質の指定
                    material_type_ = new MaterialType[1]; //マテリアルタイプ
                    material_type_[0] = MaterialType.Wooden;

                    //模様の指定
                    pattern_type_ = new PatternType[1]; //模様タイプ
                    pattern_type_[0] = PatternType.Othrewise;

                    //形状指定
                    form_type_ = new FormType[2]; //形状タイプ
                    form_type_[0] = FormType.Ellipse;
                    form_type_[1] = FormType.Oblong;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Otherwise;

                    //五行の設定
                    elements_wood_ = 40; //木の気
                    elements_fire_ = 0; //火の気
                    elements_earth_ = 10; //土の気
                    elements_metal_ = 0; //金の気
                    elements_water_ = 0; //水の気

                    //陰陽の設定
                    yin_yang_ = 0; //陰陽の設定
                    break;
                }
          
        }
    }
}
