//FurnitureGrid.cs(家具グリッド用クラス)
//
// 2017年12月4日 更新(菅原涼太)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //UnityEventを使用するため
using UnityEngine.EventSystems; //

//このクラスは始めにInitしなければならない
//また，エラーフラグはオブジェクトの色で判断すること
public class FurnitureGrid : MonoBehaviour
{
    private float rate_ = 0.2F; //仮比率(基本ここでしか変更しない) (アクセス不可)

    private int furniture_ID_; //仮家具グリッドID
    private int horizontal_; //横のグリッド数
    private int vertical_; //縦のグリッド数
    private int[][] grid_point_; //家具頂点(時計周り)グリッド基準
    private int vertex_number_; //頂点の個数
    private Vector3 grid_position_; //グリッド(中心)の3次元位置
    private GameObject furniture_grid_; //家具グリッドオブジェクト
  
    //仮家具グリッドID(取得用)
    public int furniture_ID()
    {
        return furniture_ID_;
    }

    //横の頂点数(取得用)
    public int horizontal() 
    {
        return horizontal_;
    }

    //縦の頂点数(取得用)
    public int vertical()
    {
        return vertical_;
    }

    //グリッド(中心)の3次元位置(取得用)
    private Vector3 grid_position()
    {
        return grid_position();
    }

    //オブジェクト取得用
    public GameObject furniture_grid()
    {
        return furniture_grid_;
    }

    //データ初期化(FurnitureGridをインスタンス化したとき，このメソッドを使って初期化する)
    public void Init (int grid_ID, string object_name)
    {
        furniture_ID_ = grid_ID;
        switch (grid_ID)
        {
            case 0:
                horizontal_ = 4;
                vertical_ = 4;
                vertex_number_ = 5;
                grid_point_ = new int[5][];
                grid_point_[0] = new int[2] { 2, 2 }; //0(中心)
                grid_point_[1] = new int[2] { 0, 0 }; //1
                grid_point_[2] = new int[2] { 0, 4 }; //2
                grid_point_[3] = new int[2] { 4, 4 }; //3
                grid_point_[4] = new int[2] { 4, 0 }; //4
                break;
            case 1:
                horizontal_ = 5;
                vertical_ = 3;
                vertex_number_ = 7;
                grid_point_ = new int[7][];
                grid_point_[0] = new int[2] { 2, 1 }; //0(中心)
                grid_point_[1] = new int[2] { 0, 0 }; //1
                grid_point_[2] = new int[2] { 0, 3 }; //2
                grid_point_[3] = new int[2] { 2, 3 }; //3
                grid_point_[4] = new int[2] { 2, 1 }; //4
                grid_point_[5] = new int[2] { 5, 1 }; //5
                grid_point_[6] = new int[2] { 5, 0 }; //6
                break;
            default:
                horizontal_ = 4;
                vertical_ = 4;
                vertex_number_ = 13;
                grid_point_ = new int[13][];
                grid_point_[0] = new int[2] { 2, 2 }; //0(中心)
                grid_point_[1] = new int[2] { 1, 0 }; //1
                grid_point_[2] = new int[2] { 1, 1 }; //2
                grid_point_[3] = new int[2] { 0, 1 }; //3
                grid_point_[4] = new int[2] { 0, 3 }; //4
                grid_point_[5] = new int[2] { 1, 3 }; //5
                grid_point_[6] = new int[2] { 1, 4 }; //6
                grid_point_[7] = new int[2] { 3, 4 }; //6
                grid_point_[8] = new int[2] { 3, 3 }; //8
                grid_point_[9] = new int[2] { 4, 3 }; //9
                grid_point_[10] = new int[2] { 4, 1 }; //10
                grid_point_[11] = new int[2] { 3, 1 }; //11
                grid_point_[12] = new int[2] { 3, 0 }; //12
                break;
        }

        Vector3[] vertex_ = new Vector3[vertex_number_];
        Vector3[] normal_ = new Vector3[vertex_number_];
        int[] triangles_ = new int[(vertex_number_ -1) * 3];

        for (int i = 0; i < vertex_number_ - 1; ++i)
        {
            if (i == vertex_number_ - 2)
            {
                triangles_[i * 3] = 0;
                triangles_[i * 3 + 1] = i + 1;
                triangles_[i * 3 + 2] = 1;
            }
            else
            {
                triangles_[i * 3] = 0;
                triangles_[i * 3 + 1] = i + 1;
                triangles_[i * 3 + 2] = i + 2;
            }
        }

        for (int i = 0; i < vertex_number_; ++i)
        {
            vertex_[i] = new Vector3(
                 (grid_point_[i][0] - grid_point_[0][0]) * rate_ * 0.99f,
                 (grid_point_[i][1] - grid_point_[0][1]) * rate_ * 0.99f,
                 0);

            normal_[i] = new Vector3(0, 0, 1);
        }
        grid_position_ = vertex_[0];

        Mesh mesh = new Mesh();
        mesh.vertices = vertex_;
        mesh.triangles = triangles_;
        mesh.normals = normal_;
        mesh.RecalculateBounds();

        furniture_grid_ = new GameObject();
        furniture_grid_.name = object_name;
        furniture_grid_.tag = "furniture_grid";
        furniture_grid_.AddComponent<MeshFilter>();
        furniture_grid_.GetComponent<MeshFilter>().sharedMesh = mesh;
        furniture_grid_.GetComponent<MeshFilter>().sharedMesh.name = object_name;
        furniture_grid_.AddComponent<MeshRenderer>();
        furniture_grid_.GetComponent<MeshRenderer>().material.color = new Color(255, 255, 255);
        furniture_grid_.AddComponent<MeshCollider>();
        furniture_grid_.GetComponent<MeshCollider>().sharedMesh = mesh;
        furniture_grid_.GetComponent<MeshCollider>().convex = true; //これがtrueでないと衝突判定されない
        furniture_grid_.GetComponent<MeshCollider>().isTrigger = true;
        furniture_grid_.AddComponent<Rigidbody>();
        furniture_grid_.GetComponent<Rigidbody>().isKinematic = true;
        furniture_grid_.AddComponent<GridError>(); //家具オブジェクトにGridError.cs(家具グリッド同士の衝突判定)をアタッチ
        furniture_grid_.SetActive(true);
    }

    //マウス移動
    public void Translate(Vector3 new_position)
    {
        furniture_grid_.transform.position = new_position;
        grid_position_ = new_position;
    }

    //マウス回転
    public void Rotate()
    {
        furniture_grid_.transform.Rotate(0,0,90);
    }
}
