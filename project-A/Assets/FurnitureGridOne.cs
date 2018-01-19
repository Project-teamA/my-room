using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //UnityEventを使用するため
using UnityEngine.EventSystems; //

public partial class FurnitureGrid : MonoBehaviour
{ 
    partial void GetGridData(int grid_ID)
    {
        switch (grid_ID)
        {
            case 0:
                object_type_ = ObjectType.Normal;
                children_number_ = 2;
                center_point_ = new int[2] { 5, 2 }; //中心のグリッド座標
                                                     //使用する頂点グリッド
                vertices_number_ = 6;
                grid_point_ = new int[vertices_number_][];
                grid_point_[0] = new int[2] { 0, 0 }; //0
                grid_point_[1] = new int[2] { 0, 5 }; //1
                grid_point_[2] = new int[2] { 8, 0 }; //2
                grid_point_[3] = new int[2] { 8, 5 }; //3
                grid_point_[4] = new int[2] { 10, 0 }; //4
                grid_point_[5] = new int[2] { 10, 5 }; //5
                children_grid_ = new GameObject[children_number_];
                //頂点インデックス生成
                triangles = new int[children_number_][];
                triangles[0] = new int[4] { 0, 1, 2, 3 }; //上における
                triangles[1] = new int[4] { 2, 3, 4, 5 }; //上に置けない
                break;

            case 1:
                object_type_ = ObjectType.Normal;
                children_number_ = 5;
                center_point_ = new int[2] { 4, 3 }; //中心のグリッド座標
                //使用する頂点グリッド
                vertices_number_ = 14;
                grid_point_ = new int[vertices_number_][];
                grid_point_[0] = new int[2] { 0, 0 }; //0
                grid_point_[1] = new int[2] { 0, 5 }; //1
                grid_point_[2] = new int[2] { 2, 0 }; //2
                grid_point_[3] = new int[2] { 2, 2 }; //3
                grid_point_[4] = new int[2] { 2, 5 }; //4
                grid_point_[5] = new int[2] { 3, 2 }; //5
                grid_point_[6] = new int[2] { 3, 5 }; //6
                grid_point_[7] = new int[2] { 5, 2 }; //7
                grid_point_[8] = new int[2] { 5, 5 }; //8
                grid_point_[9] = new int[2] { 6, 0 }; //9
                grid_point_[10] = new int[2] { 6, 2 }; //10
                grid_point_[11] = new int[2] { 6, 5 }; //11
                grid_point_[12] = new int[2] { 8, 0 }; //12
                grid_point_[13] = new int[2] { 8, 5 }; //13
                children_grid_ = new GameObject[children_number_];
                //頂点インデックス生成
                triangles = new int[children_number_][];
                triangles[0] = new int[4] { 0, 1, 2, 4 }; //上における1
                triangles[1] = new int[4] { 3, 4, 5, 6 }; //上における1
                triangles[2] = new int[4] { 5, 6, 7, 8 }; //上におけない
                triangles[3] = new int[4] { 7, 8, 10, 11 }; //上における2
                triangles[4] = new int[4] { 9, 11, 12, 13 }; //上における2
                break;

            case 2:
                object_type_ = ObjectType.Normal;
                children_number_ = 1;
                center_point_ = new int[2] { 2, 1 }; //中心のグリッド座標
                //使用する頂点グリッド
                vertices_number_ = 4;
                grid_point_ = new int[vertices_number_][];
                grid_point_[0] = new int[2] { 0, 0 }; //0
                grid_point_[1] = new int[2] { 0, 2 }; //1
                grid_point_[2] = new int[2] { 4, 0 }; //2
                grid_point_[3] = new int[2] { 4, 2 }; //3
                children_grid_ = new GameObject[children_number_];
                //頂点インデックス生成
                triangles = new int[children_number_][];
                triangles[0] = new int[4] { 0, 1, 2, 3 }; //上における
                break;

            case 3:
                object_type_ = ObjectType.Normal;
                children_number_ = 2;
                center_point_ = new int[2] { 2, 1 }; //中心のグリッド座標
                //使用する頂点グリッド
                vertices_number_ = 7;
                grid_point_ = new int[vertices_number_][];
                grid_point_[0] = new int[2] { 0, 0 }; //0
                grid_point_[1] = new int[2] { 0, 2 }; //1
                grid_point_[2] = new int[2] { 0, 3 }; //2
                grid_point_[3] = new int[2] { 2, 2 }; //3
                grid_point_[4] = new int[2] { 2, 3 }; //4
                grid_point_[5] = new int[2] { 5, 0 }; //5
                grid_point_[6] = new int[2] { 5, 2 }; //6
                children_grid_ = new GameObject[children_number_];
                //頂点インデックス生成
                triangles = new int[children_number_][];
                triangles[0] = new int[4] { 0, 1, 5, 6 }; //上における
                triangles[1] = new int[4] { 1, 2, 3, 4 }; //上におけない
                break;

            case 4: //カーペット
                object_type_ = ObjectType.Rugs;
                children_number_ = 1;
                center_point_ = new int[2] { 5, 4 }; //中心のグリッド座標
                //使用する頂点グリッド
                vertices_number_ = 4;
                grid_point_ = new int[vertices_number_][];
                grid_point_[0] = new int[2] { 0, 0 }; //0
                grid_point_[1] = new int[2] { 0, 8 }; //1
                grid_point_[2] = new int[2] { 9, 0 }; //2
                grid_point_[3] = new int[2] { 9, 8 }; //3
                children_grid_ = new GameObject[children_number_];
                //頂点インデックス生成
                triangles = new int[children_number_][];
                triangles[0] = new int[4] { 0, 1, 2, 3 }; //上における
                break;

            case 5: //壁掛け
                object_type_ = ObjectType.WallMounted;
                children_number_ = 1;
                center_point_ = new int[2] { 0, 2 }; //中心のグリッド座標
                //使用する頂点グリッド
                vertices_number_ = 4;
                grid_point_ = new int[vertices_number_][];
                grid_point_[0] = new int[2] { 0, 0 }; //0
                grid_point_[1] = new int[2] { 0, 4 }; //1
                grid_point_[2] = new int[2] { 1, 0 }; //2
                grid_point_[3] = new int[2] { 1, 4 }; //3
                children_grid_ = new GameObject[children_number_];
                //頂点インデックス生成
                triangles = new int[children_number_][];
                triangles[0] = new int[4] { 0, 1, 2, 3 }; //上における
                break;

            case 6: //ドア
                object_type_ = ObjectType.Door;
                children_number_ = 1;
                center_point_ = new int[2] { 1, 2 }; //中心のグリッド座標
                //使用する頂点グリッド
                vertices_number_ = 4;
                grid_point_ = new int[vertices_number_][];
                grid_point_[0] = new int[2] { 0, 0 }; //0
                grid_point_[1] = new int[2] { 0, 4 }; //1
                grid_point_[2] = new int[2] { 1, 0 }; //2
                grid_point_[3] = new int[2] { 1, 4 }; //3
                children_grid_ = new GameObject[children_number_];
                //頂点インデックス生成
                triangles = new int[children_number_][];
                triangles[0] = new int[4] { 0, 1, 2, 3 }; //上における
                break;

            case 7: //天井掛け
                object_type_ = ObjectType.CeilingHook;
                children_number_ = 1;
                center_point_ = new int[2] { 2, 2 }; //中心のグリッド座標
                //使用する頂点グリッド
                vertices_number_ = 4;
                grid_point_ = new int[vertices_number_][];
                grid_point_[0] = new int[2] { 0, 0 }; //0
                grid_point_[1] = new int[2] { 0, 4 }; //1
                grid_point_[2] = new int[2] { 4, 0 }; //2
                grid_point_[3] = new int[2] { 4, 4 }; //3
                children_grid_ = new GameObject[children_number_];
                //頂点インデックス生成
                triangles = new int[children_number_][];
                triangles[0] = new int[4] { 0, 1, 2, 3 }; //上における
                break;

            default: //窓
                object_type_ = ObjectType.Door;
                children_number_ = 1;
                center_point_ = new int[2] { 1, 4 }; //中心のグリッド座標
                //使用する頂点グリッド
                vertices_number_ = 4;
                grid_point_ = new int[vertices_number_][];
                grid_point_[0] = new int[2] { 0, 0 }; //0
                grid_point_[1] = new int[2] { 0, 8 }; //1
                grid_point_[2] = new int[2] { 1, 0 }; //2
                grid_point_[3] = new int[2] { 1, 8 }; //3
                children_grid_ = new GameObject[children_number_];
                //頂点インデックス生成
                triangles = new int[children_number_][];
                triangles[0] = new int[4] { 0, 1, 2, 3 }; //上における
                break;
        }
    } 
}
