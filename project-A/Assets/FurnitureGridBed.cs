//このクラスはFurnitureGridクラスの分割部分であり，ベッドのグリッドデータを生成するGetGridDataBedメソッドが実装されている
//
//ベッドのFurnitureTypeはBed
//
//parametaの長さは2
//[0] = 横の長さの半分, [1] = 縦の長さの半分
//
//必ず横の長さは偶数でなければならない

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //UnityEventを使用するため
using UnityEngine.EventSystems; //

public partial class FurnitureGrid : MonoBehaviour
{
    partial void GetGridDataBed(int grid_ID)
    {
        parameta_ = new float[2]; //パラメータ2個
        switch (grid_ID)
        {
            case 9:
                object_type_ = ObjectType.NotPlaced;
                children_number_ = 2;
                center_point_ = new int[2] { 3, 6 }; //中心のグリッド座標
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
                uv_coordinate_[0] = new Vector2 ( 0, 0 ); //0
                uv_coordinate_[1] = new Vector2(0, 3F/4); //1
                uv_coordinate_[2] = new Vector2(0, 1); //2
                uv_coordinate_[3] = new Vector2(1, 0); //3
                uv_coordinate_[4] = new Vector2(1, 3F/4); //4
                uv_coordinate_[5] = new Vector2(1, 1); //5

                children_grid_ = new GameObject[children_number_];
                //頂点インデックス生成
                triangles = new int[children_number_][];
                quad_type_ = new QuadType[2] { QuadType.CanPut, QuadType.NotPut };
                triangles[0] = new int[4] { 0, 1, 3, 4 }; //上における
                triangles[1] = new int[4] { 1, 2, 4, 5 }; //上に置けない

                parameta_[0] = 3; //横の長さの半分
                parameta_[1] = 6; //縦の長さの半分
                break;
        }
    }

}
