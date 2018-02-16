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
            case 1:
                {
                    //鏡タイプ1(長方形)
                    //カラー = 黒, 1
                    //材質 = ガラス，木製, 2
                    //模様 = なし(その他), 1
                    //形状 = 長方形, 背が高い，2
                    //その他 = なし, 1
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
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }
        }
    }
}