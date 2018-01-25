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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //UnityEventを使用するため
using UnityEngine.EventSystems; //

public partial class FurnitureGrid : MonoBehaviour
{
    partial void GetGridDataTable(int grid_ID)
    {
        switch (grid_ID)
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
            //木 = 40
            //火 = 0
            //土 = 10
            //金 = 0
            //水 = 0
            //
            //陰陽 = 0
            case 12:
                object_type_ = ObjectType.Normal;
                children_number_ = 1;
                center_point_ = new int[2] { 5, 2 }; //中心のグリッド座標
                put_point_ = new int[2] { 5, 2 }; //上に乗る家具の中心が合わせる座標
                //使用する頂点グリッド
                vertices_number_ = 4;
                grid_point_ = new int[vertices_number_][];
                grid_point_[0] = new int[2] { 0, 0 }; //0
                grid_point_[1] = new int[2] { 0, 5 }; //1
                grid_point_[2] = new int[2] { 10, 0 }; //2
                grid_point_[3] = new int[2] { 10, 5 }; //3

                texture_ = Resources.Load<Texture2D>("プロトベッド2"); //テクスチャはそのうち変える
                Debug.Log(texture_);
                uv_coordinate_ = new Vector2[vertices_number_];
                uv_coordinate_[0] = new Vector2(0, 0); //0
                uv_coordinate_[1] = new Vector2(0, 1); //1
                uv_coordinate_[2] = new Vector2(1, 0); //2
                uv_coordinate_[3] = new Vector2(1, 1); //3

                children_grid_ = new GameObject[children_number_];
                //頂点インデックス生成
                triangles = new int[children_number_][];
                triangles[0] = new int[4] { 0, 1, 2, 3 };

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

            //*****************************************************************************************************************************************************************************************************************

            //テーブルタイプ2(プラスチック製)
            //カラー = 緑, 白, 2
            //材質 = プラスチック, 1
            //模様 = 市松模様, 1
            //形状 = 楕円形，横長, 2
            //その他 = なし, 1
            //
            //五行
            //
            //木 = 20
            //火 = 10
            //土 = 15
            //金 = 5
            //水 = 0
            //
            //陰陽 = 70
            case 13:
                object_type_ = ObjectType.Normal;
                children_number_ = 1;
                center_point_ = new int[2] { 2, 5 }; //中心のグリッド座標
                put_point_ = new int[2] { 2, 5 }; //上に乗る家具の中心が合わせる座標
                //使用する頂点グリッド
                vertices_number_ = 4;
                grid_point_ = new int[vertices_number_][];
                grid_point_[0] = new int[2] { 0, 0 }; //0
                grid_point_[1] = new int[2] { 0, 5 }; //1
                grid_point_[2] = new int[2] { 10, 0 }; //2
                grid_point_[3] = new int[2] { 10, 5 }; //3

                texture_ = Resources.Load<Texture2D>("プロトベッド2"); //テクスチャはそのうち変える
                Debug.Log(texture_);
                uv_coordinate_ = new Vector2[vertices_number_];
                uv_coordinate_[0] = new Vector2(0, 0); //0
                uv_coordinate_[1] = new Vector2(0, 1); //1
                uv_coordinate_[2] = new Vector2(1, 0); //2
                uv_coordinate_[3] = new Vector2(1, 1); //3

                children_grid_ = new GameObject[children_number_];
                //頂点インデックス生成
                triangles = new int[children_number_][];
                triangles[0] = new int[4] { 0, 1, 2, 3 };

                //パラメータの設定
                parameta_[0] = 0; //ダミー

                //色の指定
                color_name_ = new ColorName[2]; //色
                color_name_[0] = ColorName.Green;
                color_name_[1] = ColorName.White;

                //材質の指定
                material_type_ = new MaterialType[1]; //マテリアルタイプ
                material_type_[0] = MaterialType.Plastic;

                //模様の指定
                pattern_type_ = new PatternType[1]; //模様タイプ
                pattern_type_[0] = PatternType.Check;

                //形状指定
                form_type_ = new FormType[2]; //形状タイプ
                form_type_[0] = FormType.Ellipse;
                form_type_[1] = FormType.Oblong;

                //その他特性の指定
                characteristic_ = new Characteristic[1]; //その他特性
                characteristic_[0] = Characteristic.Otherwise;

                //五行の設定
                elements_wood_ = 20; //木の気
                elements_fire_ = 10; //火の気
                elements_earth_ = 15; //土の気
                elements_metal_ = 5; //金の気
                elements_water_ = 0; //水の気

                //陰陽の設定
                yin_yang_ = 70; //陰陽の設定
                break;

            //*****************************************************************************************************************************************************************************************************************


            //テーブルタイプ3(ガラス)
            //カラー = 黒，茶色, 2
            //材質 = ガラス, 1
            //模様 = なし(その他), 1
            //形状 = 楕円形，横長, 2
            //その他 = 高級, 1
            //
            //五行
            //
            //木 = 5
            //火 = 0
            //土 = 15
            //金 = 10
            //水 = 20
            //
            //陰陽 = -16
            case 14:
                object_type_ = ObjectType.Normal;
                children_number_ = 1;
                center_point_ = new int[2] { 2, 5 }; //中心のグリッド座標
                put_point_ = new int[2] { 2, 5 }; //上に乗る家具の中心が合わせる座標
                //使用する頂点グリッド
                vertices_number_ = 4;
                grid_point_ = new int[vertices_number_][];
                grid_point_[0] = new int[2] { 0, 0 }; //0
                grid_point_[1] = new int[2] { 0, 5 }; //1
                grid_point_[2] = new int[2] { 10, 0 }; //2
                grid_point_[3] = new int[2] { 10, 5 }; //3

                texture_ = Resources.Load<Texture2D>("プロトベッド2"); //テクスチャはそのうち変える
                Debug.Log(texture_);
                uv_coordinate_ = new Vector2[vertices_number_];
                uv_coordinate_[0] = new Vector2(0, 0); //0
                uv_coordinate_[1] = new Vector2(0, 1); //1
                uv_coordinate_[2] = new Vector2(1, 0); //2
                uv_coordinate_[3] = new Vector2(1, 1); //3

                children_grid_ = new GameObject[children_number_];
                //頂点インデックス生成
                triangles = new int[children_number_][];
                triangles[0] = new int[4] { 0, 1, 2, 3 };

                //パラメータの設定
                parameta_[0] = 0; //ダミー

                //色の指定
                color_name_ = new ColorName[2]; //色
                color_name_[0] = ColorName.Black;
                color_name_[1] = ColorName.Brown;

                //材質の指定
                material_type_ = new MaterialType[1]; //マテリアルタイプ
                material_type_[0] = MaterialType.Glass;

                //模様の指定
                pattern_type_ = new PatternType[1]; //模様タイプ
                pattern_type_[0] = PatternType.Othrewise;

                //形状指定
                form_type_ = new FormType[2]; //形状タイプ
                form_type_[0] = FormType.Ellipse;
                form_type_[1] = FormType.Oblong;

                //その他特性の指定
                characteristic_ = new Characteristic[1]; //その他特性
                characteristic_[0] = Characteristic.Luxury;

                //五行の設定
                elements_wood_ = 5; //木の気
                elements_fire_ = 0; //火の気
                elements_earth_ = 15; //土の気
                elements_metal_ = 10; //金の気
                elements_water_ = 20; //水の気

                //陰陽の設定
                yin_yang_ = -16; //陰陽の設定
                break;
        }
    }
}
