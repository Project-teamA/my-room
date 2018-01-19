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
    partial void GetGridDataBed(int grid_ID)
    {
        switch (grid_ID)
        {
            //ベッドタイプ1
            //カラー = 青, 1
            //材質 = 天然素材, 1
            //模様 = なし(その他), 1
            //形状 = 長方形，背が低い, 2
            //その他 = なし, 1
            //
            //五行
            //
            //木 = 20
            //火 = 0
            //土 = 15
            //金 = 0
            //水 = 15
            //
            //陰陽 = -20
            case 9:
                object_type_ = ObjectType.Normal;
                children_number_ = 2;
                center_point_ = new int[2] { 3, 6 }; //中心のグリッド座標
                put_point_ = new int[2] { 3, 6 }; //上に乗る家具の中心が合わせる座標
                //使用する頂点グリッド
                vertices_number_ = 6;
                grid_point_ = new int[vertices_number_][];
                grid_point_[0] = new int[2] { 0, 0 }; //0
                grid_point_[1] = new int[2] { 0, 9 }; //1
                grid_point_[2] = new int[2] { 0, 12 }; //2
                grid_point_[3] = new int[2] { 6, 0 }; //3
                grid_point_[4] = new int[2] { 6, 9 }; //4
                grid_point_[5] = new int[2] { 6, 12 }; //5

                texture_ = Resources.Load<Texture2D>("プロトベッド2");
                Debug.Log(texture_);
                uv_coordinate_ = new Vector2[vertices_number_];
                uv_coordinate_[0] = new Vector2(0, 0); //0
                uv_coordinate_[1] = new Vector2(0, 3F / 4); //1
                uv_coordinate_[2] = new Vector2(0, 1); //2
                uv_coordinate_[3] = new Vector2(1, 0); //3
                uv_coordinate_[4] = new Vector2(1, 3F / 4); //4
                uv_coordinate_[5] = new Vector2(1, 1); //5

                children_grid_ = new GameObject[children_number_];
                //頂点インデックス生成
                triangles = new int[children_number_][];
                triangles[0] = new int[4] { 0, 1, 3, 4 }; //上における
                triangles[1] = new int[4] { 1, 2, 4, 5 }; //上に置けない

                //パラメータの設定
                parameta_[0] = 3; //横の長さの半分
                parameta_[1] = 6; //縦の長さの半分
                parameta_[2] = 0; //左下の頂点番号
                parameta_[3] = 2; //左上の頂点番号
                parameta_[4] = 3; //右下の頂点番号
                parameta_[5] = 5; //右上の頂点番号

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