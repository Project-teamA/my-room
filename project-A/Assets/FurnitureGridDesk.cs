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
                    outline_index_ = new int[9] { 0, 2, 4, 6, 7, 5, 3, 1, 0 };

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
                    form_type_ = new FormType[1]; //形状タイプ
                    form_type_[0] = FormType.Rectangle;

                    //その他特性の指定
                    characteristic_ = new Characteristic[1]; //その他特性
                    characteristic_[0] = Characteristic.Otherwise;

                    //五行の設定
                    elements_wood_ = 45; //木の気
                    elements_fire_ = 0; //火の気
                    elements_earth_ = 5; //土の気
                    elements_metal_ = 0; //金の気
                    elements_water_ = 0; //水の気

                    //陰陽の設定
                    yin_yang_ = 0; //陰陽の設定
                    break;
                }
        }
    }
}