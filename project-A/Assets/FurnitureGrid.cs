//FurnitureGrid.cs(家具グリッド用クラス)
//
// 2017年12月12日 更新(菅原涼太)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //UnityEventを使用するため
using UnityEngine.EventSystems; //

//このクラスは始めにInitしなければならない
//また，エラーフラグはオブジェクトの色で判断すること
//オブジェクトの重なりの順番はobjectのz位置で判断(z値が小さいほど上に載っている判定)
//オブジェクトのエラー判定はオブジェクトの色で判断すること
public class FurnitureGrid : MonoBehaviour
{
    //NotPlaced = この家具グリッドは別の家具グリッドに乗れない
    //CanPlaced = この家具グリッドは別の家具グリッドに乗れる
    public enum ObjectType { NotPlaced, CanPlaced }; 

    //NotPut = 長方形の上に別の家具グリッドを乗せれない
    //CanPut = 長方形の上に別の家具グリッドを乗せれる
    public enum QuadType { NotPut, CanPut }; 

    private float rate_ = 0.2F; //仮比率(基本ここでしか変更しない) (アクセス不可)

    private int furniture_ID_; //仮家具グリッドID
    private int horizontal_; //横のグリッド数
    private int vertical_; //縦のグリッド数
    private GameObject furniture_grid_; //家具グリッド親オブジェクト
    private Vector3 grid_position_ = new Vector3(0f,0f,0f); //グリッド(中心)の3次元位置
    private Vector3[] vertices_all_; //頂点
    private float height_ = 0; //家具の高さ

    //仮家具グリッドID(取得用)
    public int furniture_ID()
    {
        return furniture_ID_;
    }

    //横のグリッド数(取得用)
    public int horizontal() 
    {
        return horizontal_;
    }

    //縦の縦のグリッド数(取得用)
    public int vertical()
    {
        return vertical_;
    }

    //オブジェクト(取得用)
    public GameObject furniture_grid()
    {
        return furniture_grid_;
    }

    //グリッド(中心)の3次元位置(取得用)
    private Vector3 grid_position()
    {
        return grid_position();
    }

    //頂点群(取得用)
    private Vector3[] vertices_all()
    {
        return vertices_all_;
    }

    //家具の高さ(取得用)(現在はALL0)
    private float height()
    {
        return height_;
    }

    //データ初期化(FurnitureGridをインスタンス化したとき，このメソッドを使って初期化する)
    public void Init (int grid_ID, string object_name)
    {
        furniture_ID_ = grid_ID;
        furniture_grid_ = new GameObject();

        int[] center_point_;
        int[][] grid_point_;
        int[][] triangles;
        ObjectType object_type_;
        QuadType[] quad_type_;
        int children_number_; //子オブジェクトの数
        GameObject[] children_grid_; //家具グリッド子オブジェクト
        int vertices_number; //頂点の数

        switch (grid_ID)
        {
            case 0:
                object_type_ = ObjectType.NotPlaced;
                horizontal_ = 10;
                vertical_ = 5;
                children_number_ = 2;
                center_point_ = new int[2] {5, 2}; //中心のグリッド座標
                //使用する頂点グリッド
                vertices_number = 6;
                grid_point_ = new int[vertices_number][];
                grid_point_[0] = new int[2] { 0, 0 }; //0
                grid_point_[1] = new int[2] { 0, 5 }; //1
                grid_point_[2] = new int[2] { 8, 0 }; //2
                grid_point_[3] = new int[2] { 8, 5 }; //3
                grid_point_[4] = new int[2] { 10, 0 }; //4
                grid_point_[5] = new int[2] { 10, 5 }; //5
                children_grid_ = new GameObject[children_number_];
                //頂点インデックス生成
                triangles = new int[children_number_][];
                quad_type_ = new QuadType[2] {QuadType.CanPut, QuadType.NotPut };
                triangles[0] = new int[4] { 0, 1, 2, 3 }; //上における
                triangles[1] = new int[4] { 2, 3, 4, 5 }; //上に置けない
                break;

            case 1:
                object_type_ = ObjectType.NotPlaced;
                horizontal_ = 8;
                vertical_ = 5;
                children_number_ = 5;
                center_point_ = new int[2] { 4, 3 }; //中心のグリッド座標
                //使用する頂点グリッド
                vertices_number = 14;
                grid_point_ = new int[vertices_number][];
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
                quad_type_ = new QuadType[5] { QuadType.CanPut, QuadType.CanPut, QuadType.NotPut, QuadType.CanPut, QuadType.CanPut };
                triangles[0] = new int[4] { 0, 1, 2, 4 }; //上における1
                triangles[1] = new int[4] { 3, 4, 5, 6 }; //上における1
                triangles[2] = new int[4] { 5, 6, 7, 8 }; //上におけない
                triangles[3] = new int[4] { 7, 8, 10, 11 }; //上における2
                triangles[4] = new int[4] { 9, 11, 12, 13 }; //上における2
                break;

            case 2:
                object_type_ = ObjectType.CanPlaced;
                horizontal_ = 4;
                vertical_ = 2;
                children_number_ = 1;
                center_point_ = new int[2] { 2, 1 }; //中心のグリッド座標
                //使用する頂点グリッド
                vertices_number = 4;
                grid_point_ = new int[vertices_number][];
                grid_point_[0] = new int[2] { 0, 0 }; //0
                grid_point_[1] = new int[2] { 0, 2 }; //1
                grid_point_[2] = new int[2] { 4, 0 }; //2
                grid_point_[3] = new int[2] { 4, 2 }; //3
                children_grid_ = new GameObject[children_number_];
                //頂点インデックス生成
                triangles = new int[children_number_][];
                quad_type_ = new QuadType[1] { QuadType.CanPut };
                triangles[0] = new int[4] { 0, 1, 2, 3 }; //上における
                break;

            default:
                object_type_ = ObjectType.CanPlaced;
                horizontal_ = 5;
                vertical_ = 3;
                children_number_ = 2;
                center_point_ = new int[2] { 2, 1 }; //中心のグリッド座標
                //使用する頂点グリッド
                vertices_number = 7;
                grid_point_ = new int[vertices_number][];
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
                quad_type_ = new QuadType[2] { QuadType.CanPut, QuadType.NotPut };
                triangles[0] = new int[4] { 0, 1, 5, 6 }; //上における
                triangles[1] = new int[4] { 1, 2, 3, 4 }; //上におけない
                break;
        }
        vertices_all_ = new Vector3[vertices_number];
        for (int i = 0; i < vertices_number; ++i)
        {
            vertices_all_[i] = new Vector3(
                 (grid_point_[i][0] - center_point_[0]) * rate_ * 0.99f,
                 (grid_point_[i][1] - center_point_[1]) * rate_ * 0.99f,
                 0);
        }

        Vector3[] normals_ = new Vector3[4];
        for(int i = 0; i < 4; ++i)
        {
            normals_[i] = new Vector3(0F, 0F, 1F );
        }

        int[] triangles_static = new int[6] { 0, 1, 2, 2, 1, 3};
        for(int i = 0; i < children_number_; ++i)
        {
            Mesh mesh = new Mesh();
            Vector3[] vertices = new Vector3[4];
            for(int j = 0; j < 4; ++j)
            {
                vertices[j] = vertices_all_[triangles[i][j]];
            }
            mesh.vertices = vertices;
            mesh.triangles = triangles_static;
            mesh.normals = normals_;
            mesh.RecalculateBounds();
            string children_name = "_children_" + i.ToString();
            children_grid_[i] = new GameObject();
            children_grid_[i].name = object_name + children_name;
            children_grid_[i].AddComponent<MeshFilter>();
            children_grid_[i].GetComponent<MeshFilter>().sharedMesh = mesh;
            children_grid_[i].GetComponent<MeshFilter>().sharedMesh.name = object_name;
            children_grid_[i].AddComponent<MeshRenderer>();
            
            if (quad_type_[i] == QuadType.NotPut)
            {
                children_grid_[i].tag = "notput_quad"; //上にオブジェクトおけない
                children_grid_[i].GetComponent<MeshRenderer>().material.color = new Color(255, 255, 255);
            }
            else if (quad_type_[i] == QuadType.CanPut)
            {
                children_grid_[i].tag = "canput_quad"; //上にオブジェクトおける
                children_grid_[i].GetComponent<MeshRenderer>().material.color = new Color(0, 255, 255);
            }

            float quad_horizontal = (vertices[2] - vertices[0]).magnitude; //四角形の横の長さ
            float quad_vertical = (vertices[1] - vertices[0]).magnitude; //四角形の縦の長さ
                                                                       
            furniture_grid_.AddComponent<BoxCollider>();
            furniture_grid_.GetComponents<BoxCollider>()[i].center = vertices[0] + new Vector3(quad_horizontal / 2, quad_vertical / 2, 0);
            furniture_grid_.GetComponents<BoxCollider>()[i].size = new Vector3(quad_horizontal, quad_vertical, 1);
            children_grid_[i].AddComponent<Rigidbody>();
            children_grid_[i].GetComponent<Rigidbody>().isKinematic = true;
            children_grid_[i].SetActive(true);
            children_grid_[i].transform.parent = furniture_grid_.transform; //親オブジェクトに登録
        }
        furniture_grid_.name = object_name;
        if (object_type_ == ObjectType.NotPlaced)
        {
            furniture_grid_.tag = "furniture_grid_base"; //必ず地面に接する
        }
        else if (object_type_ == ObjectType.CanPlaced)
        {
            furniture_grid_.tag = "furniture_grid"; //家具の上においてもよい
        }
      
        furniture_grid_.AddComponent<MeshRenderer>();
        furniture_grid_.GetComponent<MeshRenderer>().material.color = new Color(255, 255, 255);
        furniture_grid_.AddComponent<GridError>(); //家具オブジェクトにGridError.cs(家具グリッド同士の衝突判定)をアタッチ
        furniture_grid_.GetComponent<BoxCollider>().isTrigger = true;
        furniture_grid_.AddComponent<Rigidbody>();
        furniture_grid_.GetComponent<Rigidbody>().isKinematic = true;
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
