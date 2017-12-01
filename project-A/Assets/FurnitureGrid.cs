using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureGrid : MonoBehaviour
{
    float rate = 0.2F; //仮比率
    public int furniture_ID_ = 0; //仮ID
    private bool error_flag_ = false; //エラーフラグ

    private int horizontal_; //横のグリッド数
    private int vertical_; //縦のグリッド数
    private int[][] grid_point_; //家具頂点(時計周り)グリッド基準
    private int vertex_number_; //頂点の個数
    private Vector3[] vertex_; //頂点
    private Vector3[] normal_; //法線
    private int[] triangles_; //頂点インデックス
    private Mesh mesh_; //メッシュ
    private GameObject furniture_grid_; //家具グリッドオブジェクト
   

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

    //家具頂点
    public int[][] vertex()
    {
        return grid_point_;
    }

    //データ初期化
    public void Init (int grid_ID, string object_name)
    {
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


        vertex_ = new Vector3[vertex_number_];
        normal_ = new Vector3[vertex_number_];
        triangles_ = new int[(vertex_number_ -1) * 3];

        for (int i = 0; i < vertex_number_; ++i)
        {
            vertex_[i] = new Vector3(
                 (grid_point_[i][0] - grid_point_[0][0]) * rate,
                 (grid_point_[i][1] - grid_point_[0][1]) * rate,
                 0);

            normal_[i] = new Vector3(0, 0, 1);
        }

        for(int i=0; i< vertex_number_ -1; ++i)
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

        mesh_ = new Mesh();
        mesh_.vertices = vertex_;
        mesh_.triangles = triangles_;
        mesh_.normals = normal_;

        mesh_.RecalculateBounds();

        furniture_grid_ = new GameObject();
        furniture_grid_.name = object_name;
        furniture_grid_.AddComponent<MeshFilter>();
        furniture_grid_.AddComponent<MeshRenderer>();
        furniture_grid_.AddComponent<MeshCollider>();
        furniture_grid_.GetComponent<MeshFilter>().sharedMesh = mesh_;
        furniture_grid_.GetComponent<MeshFilter>().sharedMesh.name = object_name;
        furniture_grid_.GetComponent<MeshCollider>().sharedMesh = mesh_;
        furniture_grid_.GetComponent<MeshRenderer>().material.color = new Color(255, 255, 255);

        furniture_grid_.SetActive(true);
    }



    private void Start()
    {   
        Init(furniture_ID_,"aaa");
       
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            furniture_grid_.GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);
        }
        else if(Input.GetKey(KeyCode.N))
        {
            furniture_grid_.GetComponent<MeshRenderer>().material.color = new Color(255, 255, 255);
        }
    }
}
