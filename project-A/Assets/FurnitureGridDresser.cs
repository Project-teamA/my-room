
//このクラスはFurnitureGridクラスの分割部分であり，鏡(ドレッサー)のグリッドデータを生成するGetGridDataDresserメソッドが実装されている
//
//机のFurnitureTypeはDresser
//
//鏡面が北向きになるように形状設定
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
    partial void GetGridDataDresser(int grid_ID)
    {
        switch (grid_ID)
        {
            //鏡タイプ1(長方形)
            //カラー = 黒, 1
            //材質 = ガラス，木製, 2
            //模様 = なし(その他), 1
            //形状 = 長方形, 背が高い，2
            //その他 = なし, 1
            //
            //五行
            //
            //木 = 13
            //火 = 18
            //土 = 4
            //金 = 6
            //水 = 27
            //
            //陰陽 = -23
            case 18:
                object_type_ = ObjectType.Normal;
                children_number_ = 1;
                center_point_ = new int[2] { 2, 1 }; //中心のグリッド座標
                put_point_ = new int[2] { 2, 1 }; //上に乗る家具の中心が合わせる座標
                //使用する頂点グリッド
                vertices_number_ = 4;
                grid_point_ = new int[vertices_number_][];
                grid_point_[0] = new int[2] { 0, 0 }; //0
                grid_point_[1] = new int[2] { 0, 3 }; //1
                grid_point_[2] = new int[2] { 5, 0 }; //2
                grid_point_[3] = new int[2] { 5, 3 }; //3

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
                color_name_[0] = ColorName.Black;

                //材質の指定
                material_type_ = new MaterialType[2]; //マテリアルタイプ
                material_type_[0] = MaterialType.Glass;
                material_type_[1] = MaterialType.Wooden;

                //模様の指定
                pattern_type_ = new PatternType[1]; //模様タイプ
                pattern_type_[0] = PatternType.Othrewise;

                //形状指定
                form_type_ = new FormType[2]; //形状タイプ
                form_type_[0] = FormType.Rectangle;
                form_type_[1] = FormType.High;

                //その他特性の指定
                characteristic_ = new Characteristic[1]; //その他特性
                characteristic_[0] = Characteristic.Otherwise;

                //五行の設定
                elements_wood_ = 13; //木の気
                elements_fire_ = 18; //火の気
                elements_earth_ = 4; //土の気
                elements_metal_ = 6; //金の気
                elements_water_ = 27; //水の気

                //陰陽の設定
                yin_yang_ = -23; //陰陽の設定
                break;

            //**************************************************************************************************************************************************************************************************

            //鏡タイプ2(円形)
            //カラー = 白, 1
            //材質 = ガラス，木製, 2
            //模様 = 不規則パターン, 1
            //形状 = 円形，1
            //その他 = 高級, 1
            //
            //五行
            //
            //木 = 12
            //火 = 21
            //土 = 2
            //金 = 11
            //水 = 23
            //
            //陰陽 = 39
            case 19:
                object_type_ = ObjectType.Normal;
                children_number_ = 1;
                center_point_ = new int[2] { 2, 1 }; //中心のグリッド座標
                put_point_ = new int[2] { 2, 1 }; //上に乗る家具の中心が合わせる座標
                //使用する頂点グリッド
                vertices_number_ = 4;
                grid_point_ = new int[vertices_number_][];
                grid_point_[0] = new int[2] { 0, 0 }; //0
                grid_point_[1] = new int[2] { 0, 3 }; //1
                grid_point_[2] = new int[2] { 5, 0 }; //2
                grid_point_[3] = new int[2] { 5, 3 }; //3

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
                color_name_[0] = ColorName.White;

                //材質の指定
                material_type_ = new MaterialType[2]; //マテリアルタイプ
                material_type_[0] = MaterialType.Glass;
                material_type_[1] = MaterialType.Wooden;

                //模様の指定
                pattern_type_ = new PatternType[1]; //模様タイプ
                pattern_type_[0] = PatternType.Irregularity;

                //形状指定
                form_type_ = new FormType[1]; //形状タイプ
                form_type_[0] = FormType.Round;

                //その他特性の指定
                characteristic_ = new Characteristic[1]; //その他特性
                characteristic_[0] = Characteristic.Luxury;

                //五行の設定
                elements_wood_ = 12; //木の気
                elements_fire_ = 21; //火の気
                elements_earth_ = 2; //土の気
                elements_metal_ = 11; //金の気
                elements_water_ = 23; //水の気

                //陰陽の設定
                yin_yang_ = 39; //陰陽の設定
                break;

            //**************************************************************************************************************************************************************************************************

            //鏡タイプ3(正方形)
            //カラー = 銀, 1
            //材質 = ガラス，金属製, 2
            //模様 = 不規則パターン, 1
            //形状 = 正方形，1
            //その他 = 高級, 1
            //
            //五行
            //
            //木 = 2
            //火 = 20
            //土 = 4
            //金 = 22
            //水 = 20
            //
            //陰陽 = 51
            case 20:
                object_type_ = ObjectType.Normal;
                children_number_ = 1;
                center_point_ = new int[2] { 2, 1 }; //中心のグリッド座標
                put_point_ = new int[2] { 2, 1 }; //上に乗る家具の中心が合わせる座標
                //使用する頂点グリッド
                vertices_number_ = 4;
                grid_point_ = new int[vertices_number_][];
                grid_point_[0] = new int[2] { 0, 0 }; //0
                grid_point_[1] = new int[2] { 0, 3 }; //1
                grid_point_[2] = new int[2] { 5, 0 }; //2
                grid_point_[3] = new int[2] { 5, 3 }; //3

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
                color_name_[0] = ColorName.Silver;

                //材質の指定
                material_type_ = new MaterialType[2]; //マテリアルタイプ
                material_type_[0] = MaterialType.Glass;
                material_type_[1] = MaterialType.Metal;

                //模様の指定
                pattern_type_ = new PatternType[1]; //模様タイプ
                pattern_type_[0] = PatternType.Irregularity;

                //形状指定
                form_type_ = new FormType[1]; //形状タイプ
                form_type_[0] = FormType.Square;

                //その他特性の指定
                characteristic_ = new Characteristic[1]; //その他特性
                characteristic_[0] = Characteristic.Luxury;

                //五行の設定
                elements_wood_ = 2; //木の気
                elements_fire_ = 20; //火の気
                elements_earth_ = 4; //土の気
                elements_metal_ = 22; //金の気
                elements_water_ = 20; //水の気

                //陰陽の設定
                yin_yang_ = 51; //陰陽の設定
                break;

        }
    }
}