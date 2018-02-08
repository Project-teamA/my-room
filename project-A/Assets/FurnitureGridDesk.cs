//このクラスはFurnitureGridクラスの分割部分であり，机のグリッドデータを生成するGetGridDataDeskメソッドが実装されている
//
//机のFurnitureTypeはDesk
//
//椅子の位置が南になるように形状設定
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
    partial void GetGridDataDesk(int furniture_ID)
    {
        switch (furniture_ID)
        {
            case 1:
            default:
                {
                    //机タイプ1
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
                    children_number_ = 2;
                    center_point_ = new int[2] { 10, 9 }; //中心のグリッド座標
                    put_point_ = new int[2] { 10, 9 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 8;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 21 }; //1
                    grid_point_[2] = new int[2] { 10, 0 }; //2
                    grid_point_[3] = new int[2] { 10, 21 }; //3

                    grid_point_[4] = new int[2] { 10, 7 }; //4
                    grid_point_[5] = new int[2] { 10, 14 }; //5
                    grid_point_[6] = new int[2] { 17, 7 }; //6
                    grid_point_[7] = new int[2] { 17, 14 }; //7

                    texture_ = Resources.Load<Texture2D>("desk/desk_1_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.12F, 0.3F); //0
                    uv_coordinate_[1] = new Vector2(0.12F, 0.7F); //1
                    uv_coordinate_[2] = new Vector2(0.5F, 0.3F); //2
                    uv_coordinate_[3] = new Vector2(0.5F, 0.7F); //3

                    uv_coordinate_[4] = new Vector2(0.5F, 0.432F); //0
                    uv_coordinate_[5] = new Vector2(0.5F, 0.568F); //1
                    uv_coordinate_[6] = new Vector2(0.805F, 0.432F); //2
                    uv_coordinate_[7] = new Vector2(0.805F, 0.568F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };
                    triangles_[1] = new int[4] { 4, 5, 6, 7 };

                    //枠線
                    outline_index_ = new int[9] { 0, 2, 4, 6, 7, 5, 3, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }

            case 2:
                {
                    //机タイプ2
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
                    center_point_ = new int[2] { 8, 4 }; //中心のグリッド座標
                    put_point_ = new int[2] { 8, 4 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 8 }; //1
                    grid_point_[2] = new int[2] { 16, 0 }; //2
                    grid_point_[3] = new int[2] { 16, 8 }; //3

                    texture_ = Resources.Load<Texture2D>("desk/desk_2_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.31F, 0.31F); //0
                    uv_coordinate_[1] = new Vector2(0.31F, 0.69F); //1
                    uv_coordinate_[2] = new Vector2(0.69F, 0.31F); //2
                    uv_coordinate_[3] = new Vector2(0.69F, 0.69F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[5] { 0, 2, 3, 1, 0};

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }

            case 3:
                {
                    //机タイプ3
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
                    children_number_ = 2;
                    center_point_ = new int[2] { 8, 9 }; //中心のグリッド座標
                    put_point_ = new int[2] { 8, 9 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 8;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 5 }; //1
                    grid_point_[2] = new int[2] { 8, 0 }; //2
                    grid_point_[3] = new int[2] { 8, 5 }; //3

                    grid_point_[4] = new int[2] { 0, 5 }; //4
                    grid_point_[5] = new int[2] { 0, 13 }; //5
                    grid_point_[6] = new int[2] { 16, 5 }; //6
                    grid_point_[7] = new int[2] { 16, 13 }; //7

                    texture_ = Resources.Load<Texture2D>("desk/desk_3_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.35F, 0.29F); //0
                    uv_coordinate_[1] = new Vector2(0.35F, 0.45F); //1
                    uv_coordinate_[2] = new Vector2(0.5F, 0.29F); //2
                    uv_coordinate_[3] = new Vector2(0.5F, 0.45F); //3

                    uv_coordinate_[4] = new Vector2(0.35F, 0.45F); //4
                    uv_coordinate_[5] = new Vector2(0.35F, 0.73F); //5
                    uv_coordinate_[6] = new Vector2(0.65F, 0.45F); //6
                    uv_coordinate_[7] = new Vector2(0.65F, 0.73F); //7

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };
                    triangles_[1] = new int[4] { 4, 5, 6, 7 };

                    //枠線
                    outline_index_ = new int[9] { 0, 2, 3, 6, 7, 5, 4, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }

            case 4:
                {
                    //机タイプ4
                    //カラー = 
                    //材質 = 
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //
                  
                    object_type_ = ObjectType.Normal;
                    children_number_ = 2;
                    center_point_ = new int[2] { 8, 9 }; //中心のグリッド座標
                    put_point_ = new int[2] { 8, 9 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 8;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 14 }; //1
                    grid_point_[2] = new int[2] { 14, 0 }; //2
                    grid_point_[3] = new int[2] { 14, 14 }; //3

                    grid_point_[4] = new int[2] { 0, 14 }; //4
                    grid_point_[5] = new int[2] { 0, 28 }; //5
                    grid_point_[6] = new int[2] { 35, 14 }; //6
                    grid_point_[7] = new int[2] { 35, 28 }; //7

                    texture_ = Resources.Load<Texture2D>("desk/desk_4_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.0F, 0.0F); //0
                    uv_coordinate_[1] = new Vector2(0.0F, 0.5F); //1
                    uv_coordinate_[2] = new Vector2(0.4F, 0.0F); //2
                    uv_coordinate_[3] = new Vector2(0.4F, 0.5F); //3

                    uv_coordinate_[4] = new Vector2(0.0F, 0.5F); //4
                    uv_coordinate_[5] = new Vector2(0.0F, 1.0F); //5
                    uv_coordinate_[6] = new Vector2(1.0F, 0.5F); //6
                    uv_coordinate_[7] = new Vector2(1.0F, 1.0F); //7

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };
                    triangles_[1] = new int[4] { 4, 5, 6, 7 };

                    //枠線
                    outline_index_ = new int[9] { 0, 2, 3, 6, 7, 5, 4, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }

            case 5:
                {
                    //机タイプ5
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
                    center_point_ = new int[2] { 8, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 8, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 7 }; //1
                    grid_point_[2] = new int[2] { 16, 0 }; //2
                    grid_point_[3] = new int[2] { 16, 7 }; //3

                    texture_ = Resources.Load<Texture2D>("desk/desk_5_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.31F, 0.335F); //0
                    uv_coordinate_[1] = new Vector2(0.31F, 0.665F); //1
                    uv_coordinate_[2] = new Vector2(0.69F, 0.335F); //2
                    uv_coordinate_[3] = new Vector2(0.69F, 0.665F); //3

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
                    //机タイプ6
                    //カラー = 
                    //材質 = 
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //
                   
                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 8, 4 }; //中心のグリッド座標
                    put_point_ = new int[2] { 8, 4 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 8 }; //1
                    grid_point_[2] = new int[2] { 17, 0 }; //2
                    grid_point_[3] = new int[2] { 17, 8 }; //3

                    texture_ = Resources.Load<Texture2D>("desk/desk_6_grid"); //テクスチャはそのうち変える
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
                    //机タイプ7
                    //カラー = 
                    //材質 = 
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 8, 4 }; //中心のグリッド座標
                    put_point_ = new int[2] { 8, 4 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 8 }; //1
                    grid_point_[2] = new int[2] { 17, 0 }; //2
                    grid_point_[3] = new int[2] { 17, 8 }; //3

                    texture_ = Resources.Load<Texture2D>("desk/desk_7_grid"); //テクスチャはそのうち変える
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
                    //机タイプ8
                    //カラー = 
                    //材質 = 
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 8, 4 }; //中心のグリッド座標
                    put_point_ = new int[2] { 8, 4 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 9 }; //1
                    grid_point_[2] = new int[2] { 24, 0 }; //2
                    grid_point_[3] = new int[2] { 24, 9 }; //3

                    texture_ = Resources.Load<Texture2D>("desk/desk_8_grid"); //テクスチャはそのうち変える
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
                    //机タイプ9
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
                    center_point_ = new int[2] { 9, 3 }; //中心のグリッド座標
                    put_point_ = new int[2] { 9, 3 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 7 }; //1
                    grid_point_[2] = new int[2] { 19, 0 }; //2
                    grid_point_[3] = new int[2] { 19, 7 }; //3

                    texture_ = Resources.Load<Texture2D>("desk/desk_9_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.12F, 0.425F); //0
                    uv_coordinate_[1] = new Vector2(0.12F, 0.575F); //1
                    uv_coordinate_[2] = new Vector2(0.88F, 0.425F); //2
                    uv_coordinate_[3] = new Vector2(0.88F, 0.575F); //3

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
                    //机タイプ10
                    //カラー = 
                    //材質 = 
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    
                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 8, 5 }; //中心のグリッド座標
                    put_point_ = new int[2] { 8, 5 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 11 }; //1
                    grid_point_[2] = new int[2] { 17, 0 }; //2
                    grid_point_[3] = new int[2] { 17, 11 }; //3

                    texture_ = Resources.Load<Texture2D>("desk/desk_10_grid"); //テクスチャはそのうち変える
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
                    //机タイプ11
                    //カラー = 
                    //材質 = 
                    //模様 = 
                    //形状 = 
                    //その他 = 

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 15, 24 }; //中心のグリッド座標
                    put_point_ = new int[2] { 15, 24 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 49 }; //1
                    grid_point_[2] = new int[2] { 31, 0 }; //2
                    grid_point_[3] = new int[2] { 31, 49 }; //3

                    texture_ = Resources.Load<Texture2D>("desk/desk_11_grid"); //テクスチャはそのうち変える
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
                    //机タイプ12
                    //カラー = 
                    //材質 = 
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //

                    object_type_ = ObjectType.Normal;
                    children_number_ = 2;
                    center_point_ = new int[2] { 8, 9 }; //中心のグリッド座標
                    put_point_ = new int[2] { 8, 9 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 8;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 12 }; //1
                    grid_point_[2] = new int[2] { 11, 0 }; //2
                    grid_point_[3] = new int[2] { 11, 12}; //3

                    grid_point_[4] = new int[2] { 0, 12 }; //4
                    grid_point_[5] = new int[2] { 0, 22 }; //5
                    grid_point_[6] = new int[2] { 24, 12 }; //6
                    grid_point_[7] = new int[2] { 24, 22 }; //7

                    texture_ = Resources.Load<Texture2D>("desk/desk_12_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.0F, 0.0F); //0
                    uv_coordinate_[1] = new Vector2(0.0F, 0.544F); //1
                    uv_coordinate_[2] = new Vector2(0.458F, 0.0F); //2
                    uv_coordinate_[3] = new Vector2(0.458F, 0.544F); //3

                    uv_coordinate_[4] = new Vector2(0.0F, 0.544F); //4
                    uv_coordinate_[5] = new Vector2(0.0F, 1.0F); //5
                    uv_coordinate_[6] = new Vector2(1.0F, 0.544F); //6
                    uv_coordinate_[7] = new Vector2(1.0F, 1.0F); //7

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };
                    triangles_[1] = new int[4] { 4, 5, 6, 7 };

                    //枠線
                    outline_index_ = new int[9] { 0, 2, 3, 6, 7, 5, 4, 1, 0 };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    break;
                }

        }
    }
}