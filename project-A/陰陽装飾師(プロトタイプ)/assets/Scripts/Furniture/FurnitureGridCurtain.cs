//このクラスはFurnitureGridクラスの分割部分であり，家電のグリッドデータを生成するGetGridDataCurtainメソッドが実装されている
//
//机のFurnitureTypeはCurtain
//
//窓側が北向きになるように設定
//
//parametaの長さは1
//
//カーテンは壁掛け
//(ここからは自分の勝手な判断)
//


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //UnityEventを使用するため
using UnityEngine.EventSystems; //

public partial class FurnitureGrid : MonoBehaviour
{
    partial void GetGridDataCurtain(int furniture_ID)
    {
        switch (furniture_ID)
        {
            case 1:
            default:
                {
                    //カーテン1

                    object_type_ = ObjectType.WallMounted;
                    children_number_ = 1;
                    center_point_ = new int[2] { 12, 1 }; //中心のグリッド座標
                    put_point_ = new int[2] { 12, 1 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 2 }; //1
                    grid_point_[2] = new int[2] { 25, 0 }; //2
                    grid_point_[3] = new int[2] { 25, 2 }; //3

                    texture_ = Resources.Load<Texture2D>("curtain/curtain_1_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.26F, 0.45F); //0
                    uv_coordinate_[1] = new Vector2(0.26F, 0.55F); //1
                    uv_coordinate_[2] = new Vector2(0.74F, 0.45F); //2
                    uv_coordinate_[3] = new Vector2(0.74F, 0.55F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }

            case 2:
                {
                    //カーテン2

                    object_type_ = ObjectType.WallMounted;
                    children_number_ = 1;
                    center_point_ = new int[2] { 12, 1 }; //中心のグリッド座標
                    put_point_ = new int[2] { 12, 1 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 2 }; //1
                    grid_point_[2] = new int[2] { 25, 0 }; //2
                    grid_point_[3] = new int[2] { 25, 2 }; //3

                    texture_ = Resources.Load<Texture2D>("curtain/curtain_2_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.29F, 0.46F); //0
                    uv_coordinate_[1] = new Vector2(0.29F, 0.54F); //1
                    uv_coordinate_[2] = new Vector2(0.66F, 0.46F); //2
                    uv_coordinate_[3] = new Vector2(0.66F, 0.54F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }

            case 3:
                {
                    //カーテン3

                    object_type_ = ObjectType.WallMounted;
                    children_number_ = 1;
                    center_point_ = new int[2] { 12, 1 }; //中心のグリッド座標
                    put_point_ = new int[2] { 12, 1 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 2 }; //1
                    grid_point_[2] = new int[2] { 25, 0 }; //2
                    grid_point_[3] = new int[2] { 25, 2 }; //3

                    texture_ = Resources.Load<Texture2D>("curtain/curtain_3_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.19F, 0.45F); //0
                    uv_coordinate_[1] = new Vector2(0.19F, 0.51F); //1
                    uv_coordinate_[2] = new Vector2(0.81F, 0.45F); //2
                    uv_coordinate_[3] = new Vector2(0.81F, 0.51F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }

            case 4:
                {
                    //カーテン4

                    object_type_ = ObjectType.WallMounted;
                    children_number_ = 1;
                    center_point_ = new int[2] { 12, 1 }; //中心のグリッド座標
                    put_point_ = new int[2] { 12, 1 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 2 }; //1
                    grid_point_[2] = new int[2] { 25, 0 }; //2
                    grid_point_[3] = new int[2] { 25, 2 }; //3

                    texture_ = Resources.Load<Texture2D>("curtain/curtain_4_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.14F, 0.51F); //0
                    uv_coordinate_[1] = new Vector2(0.14F, 0.54F); //1
                    uv_coordinate_[2] = new Vector2(0.86F, 0.51F); //2
                    uv_coordinate_[3] = new Vector2(0.86F, 0.54F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }

            case 5:
                {
                    //カーテン5

                    object_type_ = ObjectType.WallMounted;
                    children_number_ = 1;
                    center_point_ = new int[2] { 12, 1 }; //中心のグリッド座標
                    put_point_ = new int[2] { 12, 1 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 2 }; //1
                    grid_point_[2] = new int[2] { 25, 0 }; //2
                    grid_point_[3] = new int[2] { 25, 2 }; //3

                    texture_ = Resources.Load<Texture2D>("curtain/curtain_5_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.3F, 0.45F); //0
                    uv_coordinate_[1] = new Vector2(0.3F, 0.51F); //1
                    uv_coordinate_[2] = new Vector2(0.7F, 0.45F); //2
                    uv_coordinate_[3] = new Vector2(0.7F, 0.51F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }

            case 6:
                {
                    //カーテン6

                    object_type_ = ObjectType.WallMounted;
                    children_number_ = 1;
                    center_point_ = new int[2] { 12, 1 }; //中心のグリッド座標
                    put_point_ = new int[2] { 12, 1 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 2 }; //1
                    grid_point_[2] = new int[2] { 25, 0 }; //2
                    grid_point_[3] = new int[2] { 25, 2 }; //3

                    texture_ = Resources.Load<Texture2D>("curtain/curtain_6_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.285F, 0.465F); //0
                    uv_coordinate_[1] = new Vector2(0.285F, 0.531F); //1
                    uv_coordinate_[2] = new Vector2(0.715F, 0.465F); //2
                    uv_coordinate_[3] = new Vector2(0.715F, 0.531F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }

            case 7:
                {
                    //カーテン7

                    object_type_ = ObjectType.WallMounted;
                    children_number_ = 1;
                    center_point_ = new int[2] { 12, 1 }; //中心のグリッド座標
                    put_point_ = new int[2] { 12, 1 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 2 }; //1
                    grid_point_[2] = new int[2] { 25, 0 }; //2
                    grid_point_[3] = new int[2] { 25, 2 }; //3

                    texture_ = Resources.Load<Texture2D>("curtain/curtain_7_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.0F, 0.0F); //0
                    uv_coordinate_[1] = new Vector2(0.0F, 1.0F); //1
                    uv_coordinate_[2] = new Vector2(1.0F, 0.0F); //2
                    uv_coordinate_[3] = new Vector2(1.0F, 1.0F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }

            case 8:
                {
                    //カーテン8

                    object_type_ = ObjectType.WallMounted;
                    children_number_ = 1;
                    center_point_ = new int[2] { 12, 1 }; //中心のグリッド座標
                    put_point_ = new int[2] { 12, 1 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 2 }; //1
                    grid_point_[2] = new int[2] { 25, 0 }; //2
                    grid_point_[3] = new int[2] { 25, 2 }; //3

                    texture_ = Resources.Load<Texture2D>("curtain/curtain_8_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.0F, 0.0F); //0
                    uv_coordinate_[1] = new Vector2(0.0F, 1.0F); //1
                    uv_coordinate_[2] = new Vector2(1.0F, 0.0F); //2
                    uv_coordinate_[3] = new Vector2(1.0F, 1.0F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }

            case 9:
                {
                    //カーテン9

                    object_type_ = ObjectType.WallMounted;
                    children_number_ = 1;
                    center_point_ = new int[2] { 12, 1 }; //中心のグリッド座標
                    put_point_ = new int[2] { 12, 1 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 2 }; //1
                    grid_point_[2] = new int[2] { 25, 0 }; //2
                    grid_point_[3] = new int[2] { 25, 2 }; //3

                    texture_ = Resources.Load<Texture2D>("curtain/curtain_9_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.0F, 0.0F); //0
                    uv_coordinate_[1] = new Vector2(0.0F, 1.0F); //1
                    uv_coordinate_[2] = new Vector2(1.0F, 0.0F); //2
                    uv_coordinate_[3] = new Vector2(1.0F, 1.0F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }
        }
    }
}