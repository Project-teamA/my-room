//このクラスはFurnitureGridクラスの分割部分であり，ソファーのグリッドデータを生成するGetGridDataSofaメソッドが実装されている
//
//ソファーのFurnitureTypeはSofa
//
//背もたれが北になるように形状設定
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
    partial void GetGridDataSofa(int grid_ID)
    {
        switch (grid_ID)
        {
            //ソファータイプ1
            //カラー = ベージュ, 1
            //材質 = 化学素材, 1
            //模様 = なし(その他), 1
            //形状 = 長方形，横長, 2
            //その他 = やわらかい, 1
            //
            //五行
            //
            //木 = 0
            //火 = 10
            //土 = 10
            //金 = 30
            //水 = 0
            //
            //陰陽 = 90
            case 10:
                object_type_ = ObjectType.Normal;
                children_number_ = 1;
                center_point_ = new int[2] { 4, 1 }; //中心のグリッド座標
                put_point_ = new int[2] { 4, 1 }; //上に乗る家具の中心が合わせる座標
                //使用する頂点グリッド
                vertices_number_ = 4;
                grid_point_ = new int[vertices_number_][];
                grid_point_[0] = new int[2] { 0, 0 }; //0
                grid_point_[1] = new int[2] { 0, 3 }; //1
                grid_point_[2] = new int[2] { 8, 0 }; //2
                grid_point_[3] = new int[2] { 8, 3 }; //3

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
                color_name_ = new ColorName[1]; //色1個
                color_name_[0] = ColorName.Beige;

                //材質の指定
                material_type_ = new MaterialType[1]; //マテリアルタイプ1個
                material_type_[0] = MaterialType.Chemical;

                //模様の指定
                pattern_type_ = new PatternType[1]; //模様タイプ
                pattern_type_[0] = PatternType.Othrewise;

                //形状指定
                form_type_ = new FormType[2]; //形状タイプ
                form_type_[0] = FormType.Rectangle;
                form_type_[1] = FormType.Oblong;

                //その他特性の指定
                characteristic_ = new Characteristic[1]; //その他特性
                characteristic_[0] = Characteristic.Soft;

                //五行の設定
                elements_wood_ = 0; //木の気
                elements_fire_ = 10; //火の気
                elements_earth_ = 10; //土の気
                elements_metal_ = 30; //金の気
                elements_water_ = 0; //水の気

                //陰陽の設定
                yin_yang_ = 90; //陰陽の設定
                break;
        }
    }
}