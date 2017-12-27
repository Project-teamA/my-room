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
public partial class FurnitureGrid : MonoBehaviour
{
    //NotPlaced = この家具グリッドは別の家具グリッドに乗れない
    //CanPlaced = この家具グリッドは別の家具グリッドに乗れる
    //Rugs = この家具グリッドは敷物である
    //CeilingHook = この家具グリッドは天井掛けである
    //WallMounted = この家具グリッドは壁掛けである
    //Door = この家具グリッドはドアである
    //Window = この家具グリッドは窓である
    public enum ObjectType { NotPlaced, CanPlaced, Rugs, CeilingHook, WallMounted, Door, Window };

    //NotPut = 長方形の上に別の家具グリッドを乗せれない
    //CanPut = 長方形の上に別の家具グリッドを乗せれる
    public enum QuadType { NotPut, CanPut };

    //五行の構造体
    public struct FiveElements
    {
        float wood; //木
        float fire; //火
        float earth; //土
        float metal; //金
        float water; //水
    }

    //陰陽の構造体
    public struct YinYang
    {
        float yin; //陰
        float yang; //陽
    }

    //色の構造体(使うかどうかわからん)
    public struct ColorType
    {

    }

    //家具のタイプ
    public enum FurnitureType { Bed, Desk };

    private int furniture_ID_; //仮家具グリッドID
    private FurnitureType furniture_type_; //家具の種類(ベッド，机など)
    //家具のタイプごとのパラメータ(要素は家具タイプによって異なる)
    private float[] parameta_;
    private GameObject furniture_grid_; //家具グリッド親オブジェクト
    private Vector3 grid_position_ = new Vector3(0f, 0f, 0f); //グリッド(中心)の3次元位置
    private Vector3[] vertices_all_; //頂点
    private float height_ = 0; //家具の高さ
    private Vector3 up_direction_ = new Vector3(0, 1, 0); //上向きの方向(回転で変化)
    private Vector3 right_direction_ = new Vector3(1, 0, 0); //右向きの方向(回転で変化)
    private FiveElements five_elements_; //五行
    private YinYang yin_yang_; //陰陽
    private Color furniture_color_; //色属性
    private ColorType color_type_; //(使うかどうかわからない)色の構造体



    //ここからアクセス不可能
    private float rate_ = 0.2F; //仮比率(基本ここでしか変更しない) (アクセス不可)
    private int[] center_point_;
    private int[][] grid_point_;
    private Vector2[] uv_coordinate_;
    private Texture2D texture_;
    private int[][] triangles;
    private ObjectType object_type_;
    private QuadType[] quad_type_;
    private int children_number_; //子オブジェクトの数
    private GameObject[] children_grid_; //家具グリッド子オブジェクト
    private int vertices_number_; //頂点の数


    //仮家具グリッドID(取得用)
    public int furniture_ID()
    {
        return furniture_ID_;
    }

    //家具の種類(ベッド，机など)
    public FurnitureType furniture_type()
    {
        return furniture_type_;
    }

    //パラメータ(取得用):内容は各グリッドデータ生成関数が実装されているファイルを参照
    public float parameta(int parameta_number)
    {
        return parameta_[parameta_number];
    }  

    //オブジェクト(取得用)
    public GameObject furniture_grid()
    {
        return furniture_grid_;
    }

    //グリッド(中心)の3次元位置(取得用)
    public Vector3 grid_position()
    {
        return grid_position();
    }

    //頂点群(取得用)
    public Vector3[] vertices_all()
    {
        return vertices_all_;
    }

    //頂点(指定)
    public Vector3 vertices(int index)
    {
        if (index >= 0 && index < vertices_all_.Length)
        {
            return vertices_all_[index];
        }
       else
        {
            return vertices_all_[0];
        }
    }

    //家具の高さ(取得用)(現在はALL0)
    public float height()
    {
        return height_;
    }

    //上向きの方向(取得用)
    public Vector3 up_direction()
    {
        return up_direction_;
    }

    //右向きの方向(取得用)
    public Vector3 right_direction()
    {
        return right_direction_;
    }

    //五行(取得用)
    public FiveElements five_elements()
    {
        return five_elements_;
    }

    //陰陽
    private YinYang yin_yang()
    {
        return yin_yang_;
    }

    //色属性(取得用)
    public Color furniture_color()
    {
        return furniture_color_;
    }

    //HSV色属性(取得用)
    public void furniture_color_HSV(out float h, out float s, out float v)
    {
        Color.RGBToHSV(furniture_color_, out h, out s, out v);
    }

    //(使うかどうかわからない)色の構造体
    public ColorType color_type()
    {
        return color_type_;
    }

   

    //データ初期化(FurnitureGridをインスタンス化したとき，このメソッドを使って初期化する)
    public void Init(int grid_ID, string object_name)
    {
        furniture_ID_ = grid_ID;
        furniture_grid_ = new GameObject();

        if (grid_ID >= 0 && grid_ID <= 8)
        {
           
            GetGridData(grid_ID);
        }
        else if(grid_ID == 9) //該当するのはベッド
        {
            GetGridDataBed(grid_ID);
        }
       
        vertices_all_ = new Vector3[vertices_number_];
        for (int i = 0; i < vertices_number_; ++i)
        {
            vertices_all_[i] = new Vector3(
                 (grid_point_[i][0] - center_point_[0]) * rate_ * 0.99f,
                 (grid_point_[i][1] - center_point_[1]) * rate_ * 0.99f,
                 0);
        }

        Vector3[] normals_ = new Vector3[4];
        for (int i = 0; i < 4; ++i)
        {
            normals_[i] = new Vector3(0F, 0F, 1F);
        }

        int[] triangles_static = new int[6] { 0, 1, 2, 2, 1, 3 };
        for (int i = 0; i < children_number_; ++i)
        {
            Mesh mesh = new Mesh();
            Vector3[] vertices = new Vector3[4];
            for (int j = 0; j < 4; ++j)
            {
                vertices[j] = vertices_all_[triangles[i][j]];
            }
            mesh.vertices = vertices;
            mesh.triangles = triangles_static;
            mesh.normals = normals_;
            if (grid_ID == 9)
            {
                Vector2[] uv_modified = new Vector2[4];
                for (int j = 0; j < 4; ++j)
                {
                    uv_modified[j] = uv_coordinate_[triangles[i][j]];
                }
                mesh.uv = uv_modified;
            }
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
                if (grid_ID == 9)
                {
                    children_grid_[i].GetComponent<MeshRenderer>().material.mainTexture = texture_;
                    children_grid_[i].GetComponent<MeshRenderer>().material.color = new Color(255, 255, 255);
                }
                else
                {
                    children_grid_[i].GetComponent<MeshRenderer>().material.color = new Color(255, 255, 255);

                }
            }
            else if (quad_type_[i] == QuadType.CanPut)
            {
                children_grid_[i].tag = "canput_quad"; //上にオブジェクトおける
                if (grid_ID == 9)
                {
                    children_grid_[i].GetComponent<MeshRenderer>().material.mainTexture = texture_;
                    children_grid_[i].GetComponent<MeshRenderer>().material.color = new Color(255, 255, 255);
                }
                else
                {
                    children_grid_[i].GetComponent<MeshRenderer>().material.color = new Color(0, 255, 255);

                }
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
        else if (object_type_ == ObjectType.Rugs)
        {
            furniture_grid_.tag = "furniture_grid_rugs"; //カーペット
        }
        else if (object_type_ == ObjectType.WallMounted)
        {
            furniture_grid_.tag = "furniture_grid_wall"; //壁掛け
        }
        else if (object_type_ == ObjectType.Door)
        {
            furniture_grid_.tag = "furniture_grid_door"; //ドア(窓)
        }
        else if (object_type_ == ObjectType.CeilingHook)
        {
            furniture_grid_.tag = "furniture_grid_ceil"; //天井
        }

        furniture_grid_.AddComponent<MeshRenderer>();
       
        furniture_grid_.AddComponent<GridError>(); //家具オブジェクトにGridError.cs(家具グリッド同士の衝突判定)をアタッチ
        furniture_grid_.GetComponent<GridError>().SetHeight(height_); //GridErrorに高さ情報を渡す
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
        Quaternion quaternion = Quaternion.AngleAxis(90,Vector3.forward);
        up_direction_ = quaternion * up_direction_;
        right_direction_ = quaternion * right_direction_;
    }

    partial void GetGridData(int grid_ID); //初期実験用
    partial void GetGridDataBed(int grid_ID); //ベッド実験
}
