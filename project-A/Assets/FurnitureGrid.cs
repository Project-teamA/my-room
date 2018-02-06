//FurnitureGrid.cs(家具グリッド用クラス)
//
// 2018年1月17日以降変更した箇所(菅原涼太)
// FurnitureTypeにTable(テーブル追加)
// 家具の部屋ない方角位置の書き込み関数を追加
//
// 2018年1月21日以降更新した箇所(菅原涼太)
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //UnityEventを使用するため
using UnityEngine.EventSystems; //
using UnityEngine.UI;

//このクラスは始めにInitしなければならない
//また，エラーフラグはオブジェクトの色で判断すること
//オブジェクトの重なりの順番はobjectのz位置で判断(z値が小さいほど上に載っている判定)
//オブジェクトのエラー判定はオブジェクトの色で判断すること
//
//2018年1月19日更新事項    
//花関連 = flowerを追加
//色(温かみのある色を削除，その他の特性に[温かみ]があるのでそれで代用)



public partial class FurnitureGrid : MonoBehaviour
{
    //Normal = この家具グリッドは普通の家具グリッドである．
    //Rugs = この家具グリッドは敷物である
    //CeilingHook = この家具グリッドは天井掛けである
    //WallMounted = この家具グリッドは壁掛けである(派生はカーテン)
    //Door = この家具グリッドはドアである
    //Window = この家具グリッドは窓である
    public enum ObjectType { Normal, Rugs, CeilingHook, WallMounted, Door, Window };

    //家具のタイプ
    //ベッド，机，テーブル,ソファ，観葉植物，造花，水槽，カーペット，カーテン，家電(TV,PC,冷蔵庫,レンジ,洗濯機,ストーブ,エアコン,加湿器),
    //鏡，照明，電気スタンド，椅子，額縁，ぬいぐるみ，窓，ドア，タンス, その他
    public enum FurnitureType
    {
        Bed, Desk, Table, Sofa, FoliagePlant, ArtificialFlower, WaterTank, Carpet, Curtain,
        ConsumerElectronics, Dresser, CeilLamp, DeskLamp, Chair, PictureFrame, PlushDoll, Window, Door, Cabinet, Otherwise
    };

    //カラー
    //白，黒，灰，赤，ピンク，青，オレンジ，黄色，緑，ベージュ，クリーム，茶，金，銀，紫, その他
    public enum ColorName { White, Black, Gray, Red, Pink, Blue, Orange, Yellow, Green, Beige, Cream, Brown, Gold, Silver, Purple, Othrewise };

    //材質
    //木製，天然素材，化学素材，プラスチック，陶磁器，大理石，金属，鉱物, ガラス，水, その他
    public enum MaterialType { Wooden, Natural, Chemical, Plastic, Ceramic, Marble, Metal, Mineral, Glass, Water, Othrewise };

    //模様
    //ストライプ，リーフパターン，花柄，星柄，ダイヤ柄，アニマル柄，ジグザグ，ボーダー，チェック(市松)，
    //ドット(水玉)，アーチ，フルーツ，光沢，ウェーブストライプ，不規則パターン，雲柄, その他
    public enum PatternType
    {
        Stripe, Leaf, Flower, Star, Diamond, Animal, Zigzag, Border, Check,
        Dot, Arch, Fruits, Luster, Wave, Irregularity, Cloud, Othrewise
    };

    //形状
    //背が高い，背が低い，縦長, 横長，正方形，長方形，円形，楕円形，尖っている,その他
    public enum FormType { High, Low, Vertical, Oblong, Square, Rectangle, Round, Ellipse, Sharp, Othrewise };

    //その他特性
    //高級そう, 音が出る，(いい)におい, 発光，硬い，やわらかい，温かみ，冷たさ，花関連, 風関連, 西洋風, その他(特性なし)
    public enum Characteristic { Luxury, Sound, Smell, Light, Hard, Soft, Warm, Cold, Flower, Wind, Western, Otherwise };



    //部屋中から見た家具の置かれている方角
    public enum PlacedDirection { North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest, Othrewise };

    //家具の挙動
    //
    //Normal = 普通
    //Fixed = 固定されている(動かせない)
    //Connected = 連動している(家具の上に置かれているので下の家具と連動して動く)
    public enum Behavior { Normal, Fixed, Connected };

    //*********************************************************************************************************************************************************************************

    //ここから持っているデータ
    private int category_ID_; //家具のカテゴリーID
    private int furniture_ID_; //家具カテゴリーの中の家具ID
    private FurnitureType furniture_type_; //家具の種類(ベッド，机など)
    //家具のタイプごとのパラメータ(要素は家具タイプによって異なる)
    private float[] parameta_;
    private ColorName[] color_name_; //色(複数指定可能)
    private MaterialType[] material_type_; //材質タイプ(複数指定可能)
    private PatternType[] pattern_type_; //模様タイプ(複数指定可能)
    private FormType[] form_type_; //形状タイプ(複数指定可能)
    private Characteristic[] characteristic_; //その他特性(複数指定可能)

    private GameObject furniture_grid_; //家具グリッド親オブジェクト
    private Vector3 grid_position_ = new Vector3(0f, 0f, 0f); //グリッド(中心)の3次元位置
    private Vector3 put_position_ = new Vector3(0f, 0f, 0f); //別のグリッドが乗るときに乗るグリッドの中心と合わせる点
    private Vector3[] vertices_all_; //頂点

    private PlacedDirection placed_direction_ = PlacedDirection.Othrewise; //(部屋の中から見た)家具が置かれている方角
    private Behavior behavior_ = Behavior.Normal; //普通の挙動

    private Vector3 up_direction_ = new Vector3(0, 1, 0); //上向きの方向(回転で変化)
    private Vector3 right_direction_ = new Vector3(1, 0, 0); //右向きの方向(回転で変化)

    private int elements_wood_ = 0; //木の気
    private int elements_fire_ = 0; //火の気
    private int elements_earth_ = 0; //土の気
    private int elements_metal_ = 0; //金の気
    private int elements_water_ = 0; //水の気

    private int yin_yang_ = 0; //陰陽(プラス=陽，マイナス=陰)

    //ここからアクセス不可能
    private float rate_ = 0.1F; //仮比率(基本ここでしか変更しない) (アクセス不可)
    private int[] center_point_;
    private int[] put_point_; //乗せるためのグリッド点
    private int[][] grid_point_;
    private Vector2[] uv_coordinate_;
    private Texture2D texture_;
    private int[][] triangles_;
    private int[] outline_index_;
    private ObjectType object_type_;
    private int children_number_; //子オブジェクトの数
    private GameObject[] children_grid_; //家具グリッド子オブジェクト
    private int vertices_number_; //頂点の数


    //家具のカテゴリーID(取得用)
    public int category_ID()
    {
        return category_ID_;
    }

    //家具の種類ID(取得用)
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

    //色(取得用)
    public ColorName[] color_name()
    {
        return color_name_;
    }

    //材質タイプ(取得用)
    public MaterialType[] material_type()
    {
        return material_type_;
    }

    //形状タイプ(取得用)
    public FormType[] form_type()
    {
        return form_type_;
    }

    //模様タイプ(取得用)
    public PatternType[] pattern_type()
    {
        return pattern_type_;
    }

    //その他特性(取得用)
    public Characteristic[] characteristic()
    {
        return characteristic_;
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

    //別のグリッドが乗るときに乗るグリッドの中心と合わせる点(取得用)
    public Vector3 put_position()
    {
        return put_position_;
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

    //(部屋の中から見た)家具が置かれている方角(取得用)
    public PlacedDirection placed_direction()
    {
        return placed_direction_;
    }

    //(部屋の中から見た)家具が置かれている方角(セット用)
    public void set_direction(PlacedDirection placed_direction)
    {
        placed_direction_ = placed_direction;
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




    //木の気
    public int elements_wood()
    {
        return elements_wood_;
    }

    //火の気
    public int elements_fire()
    {
        return elements_wood_;
    }

    //土の気
    public int elements_earth()
    {
        return elements_wood_;
    }

    //金の気
    public int elements_metal()
    {
        return elements_wood_;
    }

    //水の気
    public int elements_water()
    {
        return elements_wood_;
    }




    //陰陽
    public int yin_yang()
    {
        return yin_yang_;
    }




    //データ初期化(FurnitureGridをインスタンス化したとき，このメソッドを使って初期化する)
    public void Init(int category_ID, int furniture_ID, string object_name)
    {
        category_ID_ = category_ID;
        furniture_ID_ = furniture_ID;
        furniture_grid_ = new GameObject();

        if (category_ID_ == 0)
        {
            //ベッド
            furniture_type_ = FurnitureType.Bed;
            parameta_ = new float[6];
            GetGridDataBed(furniture_ID_);
        }
        else if (category_ID_ == 1)
        {
            //タンス
            furniture_type_ = FurnitureType.Cabinet;
            parameta_ = new float[1];
            GetGridDataCabinet(furniture_ID_);
        }
        else if (category_ID_ == 2)
        {
            //カーペット
            furniture_type_ = FurnitureType.Carpet;
            parameta_ = new float[1];
            GetGridDataCarpet(furniture_ID_);
        }
        else if (category_ID_ == 3)
        {
            //机
            furniture_type_ = FurnitureType.Desk;
            parameta_ = new float[1];
            GetGridDataDesk(furniture_ID_);
        }
        else if (category_ID_ == 4)
        {
            //観葉植物(現在未実装)
            furniture_type_ = FurnitureType.FoliagePlant;
            parameta_ = new float[1];
            GetGridDataFoliagePlant(furniture_ID_);
        }
        else if (category_ID_ == 5)
        {
            //天井ランプ
            furniture_type_ = FurnitureType.CeilLamp;
            parameta_ = new float[1];
            GetGridDataCeilLamp(furniture_ID_);
        }
        else if (category_ID_ == 6)
        {
            //机ランプ
            furniture_type_ = FurnitureType.DeskLamp;
            parameta_ = new float[1];
            GetGridDataDeskLamp(furniture_ID_);
        }
        else if (category_ID_ == 7)
        {
            //ソファー
            furniture_type_ = FurnitureType.Sofa;
            parameta_ = new float[1];
            GetGridDataSofa(furniture_ID_);
        }
        else if (category_ID_ == 8)
        {
            //テーブル
            furniture_type_ = FurnitureType.Table;
            parameta_ = new float[1];
            GetGridDataTable(furniture_ID_);
        }
        else if (category_ID_ == 9)
        {
            //家電
            furniture_type_ = FurnitureType.ConsumerElectronics;
            parameta_ = new float[1];
            GetGridDataConsumerElectronics(furniture_ID_);
        }
        else if (category_ID_ == 10)
        {
            //カーテン
            furniture_type_ = FurnitureType.Curtain;
            parameta_ = new float[1];
            GetGridDataCurtain(furniture_ID_);
        }
        //ここまで個別に家具グリッドを取得

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
                vertices[j] = vertices_all_[triangles_[i][j]];
            }
            mesh.vertices = vertices;
            mesh.triangles = triangles_static;
            mesh.normals = normals_;

            Vector2[] uv_modified = new Vector2[4];
            for (int j = 0; j < 4; ++j)
            {
                uv_modified[j] = uv_coordinate_[triangles_[i][j]];
            }
            mesh.uv = uv_modified;

            mesh.RecalculateBounds();
            string children_name = "_children_" + i.ToString();
            children_grid_[i] = new GameObject();
            children_grid_[i].name = object_name + children_name;
            children_grid_[i].AddComponent<MeshFilter>();
            children_grid_[i].GetComponent<MeshFilter>().sharedMesh = mesh;
            children_grid_[i].GetComponent<MeshFilter>().sharedMesh.name = object_name;
            children_grid_[i].AddComponent<MeshRenderer>();


            children_grid_[i].GetComponent<MeshRenderer>().material.mainTexture = texture_;
            children_grid_[i].GetComponent<MeshRenderer>().material.color = new Color(2, 2, 2, 1);

            //ここからグリッドの透明化処理(シェーダモードをcutout化)
            children_grid_[i].GetComponent<MeshRenderer>().material.SetOverrideTag("RenderType", "TransparentCutout");
            children_grid_[i].GetComponent<MeshRenderer>().material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            children_grid_[i].GetComponent<MeshRenderer>().material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            children_grid_[i].GetComponent<MeshRenderer>().material.SetInt("_ZWrite", 1);
            children_grid_[i].GetComponent<MeshRenderer>().material.EnableKeyword("_ALPHATEST_ON");
            children_grid_[i].GetComponent<MeshRenderer>().material.DisableKeyword("_ALPHABLEND_ON");
            children_grid_[i].GetComponent<MeshRenderer>().material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            children_grid_[i].GetComponent<MeshRenderer>().material.renderQueue = 2450;
            //ここまでグリッドの透明化処理

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

        if (object_type_ == ObjectType.Normal)
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
        furniture_grid_.GetComponent<BoxCollider>().isTrigger = true;
        furniture_grid_.AddComponent<Rigidbody>();
        furniture_grid_.GetComponent<Rigidbody>().isKinematic = true;
        furniture_grid_.SetActive(true);

        furniture_grid_.AddComponent<LineRenderer>();
        furniture_grid_.GetComponent<LineRenderer>().SetWidth(0.1f, 0.1f);
        furniture_grid_.GetComponent<LineRenderer>().SetVertexCount(outline_index_.Length);

        Vector3[] outline_vertices = new Vector3[outline_index_.Length];
        for (int i = 0; i < outline_index_.Length; ++i)
        {
            outline_vertices[i] = vertices_all_[outline_index_[i]];
        }
        furniture_grid_.GetComponent<LineRenderer>().SetPositions(outline_vertices);
        furniture_grid_.GetComponent<LineRenderer>().enabled = false;
    }

    //マウス移動
    public void Translate(Vector3 new_position)
    {
        furniture_grid_.transform.position = new_position;

        for (int i = 0; i < vertices_all_.Length; ++i)
        {
            vertices_all_[i] = (new_position - grid_position_) + vertices_all_[i];
        }

        Vector3[] outline_vertices = new Vector3[outline_index_.Length];
        for (int i = 0; i < outline_index_.Length; ++i)
        {
            outline_vertices[i] = vertices_all_[outline_index_[i]];
        }
        furniture_grid_.GetComponent<LineRenderer>().SetPositions(outline_vertices);

        grid_position_ = new_position;
    }

    //マウス回転
    public void Rotate()
    {
        furniture_grid_.transform.Rotate(0, 0, 90);
        Quaternion quaternion = Quaternion.AngleAxis(90, Vector3.forward);

        for (int i= 0; i < vertices_all_.Length; ++i)
        {
            vertices_all_[i] = quaternion * (vertices_all_[i] - grid_position_) + grid_position_;
        }

        Vector3[] outline_vertices = new Vector3[outline_index_.Length];
        for (int i = 0; i < outline_index_.Length; ++i)
        {
            outline_vertices[i] = vertices_all_[outline_index_[i]];
        }
        furniture_grid_.GetComponent<LineRenderer>().SetPositions(outline_vertices);

      
        up_direction_ = quaternion * up_direction_;
        right_direction_ = quaternion * right_direction_;
    }

    partial void GetGridDataBed(int furniture_ID); // 0 ---ベッド
    partial void GetGridDataCabinet(int furniture_ID); // 1 ---キャビネット
    partial void GetGridDataCarpet(int furniture_ID); // 2 ---カーペット
    partial void GetGridDataDesk(int furniture_ID); // 3 ---机
    partial void GetGridDataFoliagePlant(int furniture_ID); // 4 ---観葉植物
    partial void GetGridDataCeilLamp(int furniture_ID); // 5 ---天井ランプ
    partial void GetGridDataDeskLamp(int furniture_ID); // 6 ---机ランプ
    partial void GetGridDataSofa(int furniture_ID); // 7 ---ソファー
    partial void GetGridDataTable(int furniture_ID); // 8 ---テーブル
    partial void GetGridDataConsumerElectronics(int furniture_ID); // 9 ---家電
    partial void GetGridDataCurtain(int furniture_ID); // 10 ---カーテン

    //ここから未実装
    partial void GetGridDataDresser(int grid_ID); //鏡(ドレッサー)
}