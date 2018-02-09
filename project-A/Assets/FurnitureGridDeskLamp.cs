//このクラスはFurnitureGridクラスの分割部分であり，天井ランプのグリッドデータを生成するGetGridDataDeskLampメソッドが実装されている
//
//机ランプのFurnitureTypeはDeskLamp
//
//方位の指定は特になし
//
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
    partial void GetGridDataDeskLamp(int furniture_ID)
    {
        switch (furniture_ID)
        {
            case 1:
            default:
                {
                    //天井掛け1
                    //カラー = 
                    //材質 =
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //
                    //五行
                    //
                    //木 = 
                    //火 = 
                    //土 = 
                    //金 = 
                    //水 = 
                    //
                    //陰陽 = 
                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 2, 2 }; //中心のグリッド座標
                    put_point_ = new int[2] { 2, 2 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 5 }; //1
                    grid_point_[2] = new int[2] { 5, 0 }; //2
                    grid_point_[3] = new int[2] { 5, 5 }; //3
                    texture_ = Resources.Load<Texture2D>("desklamp/desklamp_1_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.41F, 0.32F); //0
                    uv_coordinate_[1] = new Vector2(0.41F, 0.68F); //1
                    uv_coordinate_[2] = new Vector2(0.59F, 0.32F); //2
                    uv_coordinate_[3] = new Vector2(0.59F, 0.68F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }

            case 2:
                {
                    //天井掛け2
                    //カラー = 
                    //材質 =
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //
                    //五行
                    //
                    //木 = 
                    //火 = 
                    //土 = 
                    //金 = 
                    //水 = 
                    //
                    //陰陽 = 
                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 2, 2 }; //中心のグリッド座標
                    put_point_ = new int[2] { 2, 2 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 5 }; //1
                    grid_point_[2] = new int[2] { 5, 0 }; //2
                    grid_point_[3] = new int[2] { 5, 5 }; //3
                    texture_ = Resources.Load<Texture2D>("desklamp/desklamp_2_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.4F, 0.319F); //0
                    uv_coordinate_[1] = new Vector2(0.4F, 0.681F); //1
                    uv_coordinate_[2] = new Vector2(0.6F, 0.319F); //2
                    uv_coordinate_[3] = new Vector2(0.6F, 0.681F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }

            case 3:
                {
                    //天井掛け3
                    //カラー = 
                    //材質 =
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //
                    //五行
                    //
                    //木 = 
                    //火 = 
                    //土 = 
                    //金 = 
                    //水 = 
                    //
                    //陰陽 = 
                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 6, 1 }; //中心のグリッド座標
                    put_point_ = new int[2] { 6, 1 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 3 }; //1
                    grid_point_[2] = new int[2] { 12, 0 }; //2
                    grid_point_[3] = new int[2] { 12, 3 }; //3
                    texture_ = Resources.Load<Texture2D>("desklamp/desklamp_3_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.333F, 0.42F); //0
                    uv_coordinate_[1] = new Vector2(0.333F, 0.58F); //1
                    uv_coordinate_[2] = new Vector2(0.68F, 0.42F); //2
                    uv_coordinate_[3] = new Vector2(0.68F, 0.58F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }

            case 4:
                {
                    //天井掛け4
                    //カラー = 
                    //材質 =
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //
                    //五行
                    //
                    //木 = 
                    //火 = 
                    //土 = 
                    //金 = 
                    //水 = 
                    //
                    //陰陽 = 
                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 3, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 3, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 6 }; //1
                    grid_point_[2] = new int[2] { 6, 0 }; //2
                    grid_point_[3] = new int[2] { 6, 6 }; //3
                    texture_ = Resources.Load<Texture2D>("desklamp/desklamp_4_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.34F, 0.23F); //0
                    uv_coordinate_[1] = new Vector2(0.34F, 0.77F); //1
                    uv_coordinate_[2] = new Vector2(0.66F, 0.23F); //2
                    uv_coordinate_[3] = new Vector2(0.66F, 0.77F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }

            case 5:
                {
                    //机ランプ5
                    //カラー = 
                    //材質 =
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //
                   
                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 3, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 3, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 6 }; //1
                    grid_point_[2] = new int[2] { 6, 0 }; //2
                    grid_point_[3] = new int[2] { 6, 6 }; //3
                    texture_ = Resources.Load<Texture2D>("desklamp/desklamp_5_grid"); //テクスチャはそのうち変える
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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }

            case 6:
                {
                    //机ランプ6
                    //カラー = 
                    //材質 =
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 3, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 3, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 6 }; //1
                    grid_point_[2] = new int[2] { 6, 0 }; //2
                    grid_point_[3] = new int[2] { 6, 6 }; //3
                    texture_ = Resources.Load<Texture2D>("desklamp/desklamp_6_grid"); //テクスチャはそのうち変える
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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }

            case 7:
                {
                    //机ランプ7
                    //カラー = 
                    //材質 =
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 3, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 3, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 6 }; //1
                    grid_point_[2] = new int[2] { 6, 0 }; //2
                    grid_point_[3] = new int[2] { 6, 6 }; //3
                    texture_ = Resources.Load<Texture2D>("desklamp/desklamp_7_grid"); //テクスチャはそのうち変える
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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }

            case 8:
                {
                    //机ランプ8
                    //カラー = 
                    //材質 =
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 3, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 3, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 7 }; //1
                    grid_point_[2] = new int[2] { 7, 0 }; //2
                    grid_point_[3] = new int[2] { 7, 7 }; //3
                    texture_ = Resources.Load<Texture2D>("desklamp/desklamp_8_grid"); //テクスチャはそのうち変える
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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }

            case 9:
                {
                    //机ランプ9
                    //カラー = 
                    //材質 =
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 4, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 4, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 7 }; //1
                    grid_point_[2] = new int[2] { 8, 0 }; //2
                    grid_point_[3] = new int[2] { 8, 7 }; //3
                    texture_ = Resources.Load<Texture2D>("desklamp/desklamp_9_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.05F, 0.0F); //0
                    uv_coordinate_[1] = new Vector2(0.05F, 1.0F); //1
                    uv_coordinate_[2] = new Vector2(1.0F, 0.0F); //2
                    uv_coordinate_[3] = new Vector2(1.0F, 1.0F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }

            case 10:
                {
                    //机ランプ10
                    //カラー = 
                    //材質 =
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 3, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 3, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 7 }; //1
                    grid_point_[2] = new int[2] { 7, 0 }; //2
                    grid_point_[3] = new int[2] { 7, 7 }; //3
                    texture_ = Resources.Load<Texture2D>("desklamp/desklamp_10_grid"); //テクスチャはそのうち変える
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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }

            case 11:
                {
                    //机ランプ11
                    //カラー = 
                    //材質 =
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 3, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 3, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 6 }; //1
                    grid_point_[2] = new int[2] { 6, 0 }; //2
                    grid_point_[3] = new int[2] { 6, 6 }; //3
                    texture_ = Resources.Load<Texture2D>("desklamp/desklamp_11_grid"); //テクスチャはそのうち変える
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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }

            case 12:
                {
                    //机ランプ12
                    //カラー = 
                    //材質 =
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 3, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 3, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 6 }; //1
                    grid_point_[2] = new int[2] { 6, 0 }; //2
                    grid_point_[3] = new int[2] { 6, 6 }; //3
                    texture_ = Resources.Load<Texture2D>("desklamp/desklamp_12_grid"); //テクスチャはそのうち変える
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
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }
        }
    }
}