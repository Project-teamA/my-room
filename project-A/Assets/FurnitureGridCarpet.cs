//このクラスはFurnitureGridクラスの分割部分であり，カーペットのグリッドデータを生成するGetGridDataCarpetメソッドが実装されている
//
//カーペットのFurnitureTypeはCarpet
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
    partial void GetGridDataCarpet(int grid_ID)
    {
        switch (grid_ID)
        {
            //カーペットタイプ1(長方形)
            //カラー = 青,緑,白 3
            //材質 = 天然素材, 1
            //模様 = 水玉柄, 1
            //形状 = 長方形, 1
            //その他 = やわらかい, 1
            //
            //五行
            //
            //木 = 18
            //火 = 0
            //土 = 18
            //金 = 10
            //水 = 26
            //
            //陰陽 = -12
            case 15:
                object_type_ = ObjectType.Rugs;
                children_number_ = 1;
                center_point_ = new int[2] { 8, 5 }; //中心のグリッド座標
                put_point_ = new int[2] { 8, 5 }; //上に乗る家具の中心が合わせる座標
                //使用する頂点グリッド
                vertices_number_ = 4;
                grid_point_ = new int[vertices_number_][];
                grid_point_[0] = new int[2] { 0, 0 }; //0
                grid_point_[1] = new int[2] { 0, 10 }; //1
                grid_point_[2] = new int[2] { 16, 0 }; //2
                grid_point_[3] = new int[2] { 16, 10 }; //3

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

            //*********************************************************************************************************************************************************************************************

            //カーペットタイプ2(円形)
            //カラー = オレンジ,ピンク,白 3
            //材質 = 天然素材, 1
            //模様 = 花柄, 1
            //形状 = 円形, 1
            //その他 = やわらかい, 高級 2
            //
            //五行
            //
            //木 = 21
            //火 = 30
            //土 = 13
            //金 = 13
            //水 = 0
            //
            //陰陽 = 65
            case 16:
                object_type_ = ObjectType.Rugs;
                children_number_ = 1;
                center_point_ = new int[2] { 6, 6 }; //中心のグリッド座標
                put_point_ = new int[2] { 6, 6 }; //上に乗る家具の中心が合わせる座標
                //使用する頂点グリッド
                vertices_number_ = 4;
                grid_point_ = new int[vertices_number_][];
                grid_point_[0] = new int[2] { 0, 0 }; //0
                grid_point_[1] = new int[2] { 0, 12 }; //1
                grid_point_[2] = new int[2] { 12, 0 }; //2
                grid_point_[3] = new int[2] { 12, 12 }; //3

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
                color_name_ = new ColorName[3]; //色
                color_name_[0] = ColorName.Orange;
                color_name_[1] = ColorName.Pink;
                color_name_[2] = ColorName.White;

                //材質の指定
                material_type_ = new MaterialType[1]; //マテリアルタイプ
                material_type_[0] = MaterialType.Natural;

                //模様の指定
                pattern_type_ = new PatternType[1]; //模様タイプ
                pattern_type_[0] = PatternType.Flower;

                //形状指定
                form_type_ = new FormType[1]; //形状タイプ
                form_type_[0] = FormType.Round;

                //その他特性の指定
                characteristic_ = new Characteristic[2]; //その他特性
                characteristic_[0] = Characteristic.Soft;
                characteristic_[1] = Characteristic.Luxury;

                //五行の設定
                elements_wood_ = 21; //木の気
                elements_fire_ = 30; //火の気
                elements_earth_ = 13; //土の気
                elements_metal_ = 13; //金の気
                elements_water_ = 0; //水の気

                //陰陽の設定
                yin_yang_ = 65; //陰陽の設定
                break;

            //*********************************************************************************************************************************************************************************************

            //カーペットタイプ3(正方形)
            //カラー = 茶色,灰色,クリーム色 3
            //材質 = 天然素材, 1
            //模様 = ダイヤ柄, 1
            //形状 = 正方形, 1
            //その他 = やわらかい, 高級 2
            //
            //五行
            //
            //木 = 5
            //火 = 12
            //土 = 23
            //金 = 23
            //水 = 2
            //
            //陰陽 = 10
            case 17:
                object_type_ = ObjectType.Rugs;
                children_number_ = 1;
                center_point_ = new int[2] { 6, 6 }; //中心のグリッド座標
                put_point_ = new int[2] { 6, 6 }; //上に乗る家具の中心が合わせる座標
                //使用する頂点グリッド
                vertices_number_ = 4;
                grid_point_ = new int[vertices_number_][];
                grid_point_[0] = new int[2] { 0, 0 }; //0
                grid_point_[1] = new int[2] { 0, 12 }; //1
                grid_point_[2] = new int[2] { 12, 0 }; //2
                grid_point_[3] = new int[2] { 12, 12 }; //3

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
                color_name_ = new ColorName[3]; //色
                color_name_[0] = ColorName.Brown;
                color_name_[1] = ColorName.Gray;
                color_name_[2] = ColorName.Cream;

                //材質の指定
                material_type_ = new MaterialType[1]; //マテリアルタイプ
                material_type_[0] = MaterialType.Natural;

                //模様の指定
                pattern_type_ = new PatternType[1]; //模様タイプ
                pattern_type_[0] = PatternType.Diamond;

                //形状指定
                form_type_ = new FormType[1]; //形状タイプ
                form_type_[0] = FormType.Square;

                //その他特性の指定
                characteristic_ = new Characteristic[2]; //その他特性
                characteristic_[0] = Characteristic.Soft;
                characteristic_[1] = Characteristic.Luxury;

                //五行の設定
                elements_wood_ = 5; //木の気
                elements_fire_ = 12; //火の気
                elements_earth_ = 23; //土の気
                elements_metal_ = 23; //金の気
                elements_water_ = 2; //水の気

                //陰陽の設定
                yin_yang_ = 10; //陰陽の設定
                break;
        }
    }
}