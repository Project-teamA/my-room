﻿//このクラスはFurnitureGridクラスの分割部分であり，ソファーのグリッドデータを生成するGetGridDataSofaメソッドが実装されている
//
//ソファーのFurnitureTypeはSofa
//
//背もたれが北になるように形状設定
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
    partial void GetGridDataSofa(int furniture_ID)
    {
        switch (furniture_ID)
        {
            case 1:
            default:
                {
                    // ソファータイプ1
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
                    center_point_ = new int[2] { 11, 5 }; //中心のグリッド座標
                    put_point_ = new int[2] { 11, 5 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 11 }; //1
                    grid_point_[2] = new int[2] { 21, 0 }; //2
                    grid_point_[3] = new int[2] { 21, 11 }; //3

                    texture_ = Resources.Load<Texture2D>("sofa/sofa_1_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.3F, 0.31F); //0
                    uv_coordinate_[1] = new Vector2(0.3F, 0.71F); //1
                    uv_coordinate_[2] = new Vector2(0.7F, 0.31F); //2
                    uv_coordinate_[3] = new Vector2(0.7F, 0.71F); //3

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
                    // ソファータイプ2
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
                    center_point_ = new int[2] { 11, 5 }; //中心のグリッド座標
                    put_point_ = new int[2] { 11, 5 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 10 }; //1
                    grid_point_[2] = new int[2] { 21, 0 }; //2
                    grid_point_[3] = new int[2] { 21, 10 }; //3

                    texture_ = Resources.Load<Texture2D>("sofa/sofa_2_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.265F, 0.31F); //0
                    uv_coordinate_[1] = new Vector2(0.265F, 0.7F); //1
                    uv_coordinate_[2] = new Vector2(0.735F, 0.31F); //2
                    uv_coordinate_[3] = new Vector2(0.735F, 0.7F); //3

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
                    // ソファータイプ3
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
                    center_point_ = new int[2] { 5, 5 }; //中心のグリッド座標
                    put_point_ = new int[2] { 5, 5 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 11 }; //1
                    grid_point_[2] = new int[2] { 11, 0 }; //2
                    grid_point_[3] = new int[2] { 11, 11 }; //3

                    texture_ = Resources.Load<Texture2D>("sofa/sofa_3_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.39F, 0.3F); //0
                    uv_coordinate_[1] = new Vector2(0.39F, 0.74F); //1
                    uv_coordinate_[2] = new Vector2(0.61F, 0.3F); //2
                    uv_coordinate_[3] = new Vector2(0.61F, 0.74F); //3

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
                    // ソファータイプ4
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
                    center_point_ = new int[2] { 11, 5 }; //中心のグリッド座標
                    put_point_ = new int[2] { 11, 5 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 11 }; //1
                    grid_point_[2] = new int[2] { 21, 0 }; //2
                    grid_point_[3] = new int[2] { 21, 11 }; //3

                    texture_ = Resources.Load<Texture2D>("sofa/sofa_4_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.285F, 0.31F); //0
                    uv_coordinate_[1] = new Vector2(0.285F, 0.71F); //1
                    uv_coordinate_[2] = new Vector2(0.715F, 0.31F); //2
                    uv_coordinate_[3] = new Vector2(0.715F, 0.71F); //3

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
                    // ソファータイプ5
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
                    center_point_ = new int[2] { 10, 5 }; //中心のグリッド座標
                    put_point_ = new int[2] { 10, 5 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 11 }; //1
                    grid_point_[2] = new int[2] { 20, 0 }; //2
                    grid_point_[3] = new int[2] { 20, 11 }; //3

                    texture_ = Resources.Load<Texture2D>("sofa/sofa_5_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.31F, 0.3F); //0
                    uv_coordinate_[1] = new Vector2(0.31F, 0.7F); //1
                    uv_coordinate_[2] = new Vector2(0.69F, 0.3F); //2
                    uv_coordinate_[3] = new Vector2(0.69F, 0.7F); //3

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
                    // ソファータイプ6
                    //カラー =
                    //材質 = 
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //
                   
                    object_type_ = ObjectType.Normal;
                    children_number_ = 2;
                    center_point_ = new int[2] { 22, 20 }; //中心のグリッド座標
                    put_point_ = new int[2] { 22, 20 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 8;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 17, 0 }; //0
                    grid_point_[1] = new int[2] { 17, 15 }; //1
                    grid_point_[2] = new int[2] { 29, 0 }; //2
                    grid_point_[3] = new int[2] { 29, 15 }; //3

                    grid_point_[4] = new int[2] { 0, 15 }; //4
                    grid_point_[5] = new int[2] { 0, 27 }; //5
                    grid_point_[6] = new int[2] { 29, 15 }; //6
                    grid_point_[7] = new int[2] { 29, 27 }; //7

                    texture_ = Resources.Load<Texture2D>("sofa/sofa_6_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.586F, 0.0F); //0
                    uv_coordinate_[1] = new Vector2(0.586F, 0.556F); //1
                    uv_coordinate_[2] = new Vector2(1.0F, 0.0F); //2
                    uv_coordinate_[3] = new Vector2(1.0F, 0.556F); //3

                    uv_coordinate_[4] = new Vector2(0.0F, 0.556F); //4
                    uv_coordinate_[5] = new Vector2(0.0F, 1.0F); //5
                    uv_coordinate_[6] = new Vector2(1.0F, 0.556F); //6
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

            case 7:
                {
                    // ソファータイプ7
                    //カラー =
                    //材質 = 
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //

                    object_type_ = ObjectType.Normal;
                    children_number_ = 2;
                    center_point_ = new int[2] { 21, 26 }; //中心のグリッド座標
                    put_point_ = new int[2] { 21, 26 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 8;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 15, 0 }; //0
                    grid_point_[1] = new int[2] { 15, 20 }; //1
                    grid_point_[2] = new int[2] { 30, 0 }; //2
                    grid_point_[3] = new int[2] { 30, 20 }; //3

                    grid_point_[4] = new int[2] { 0, 20 }; //4
                    grid_point_[5] = new int[2] { 0, 35 }; //5
                    grid_point_[6] = new int[2] { 30, 20 }; //6
                    grid_point_[7] = new int[2] { 30, 35 }; //7

                    texture_ = Resources.Load<Texture2D>("sofa/sofa_7_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.5F, 0.0F); //0
                    uv_coordinate_[1] = new Vector2(0.5F, 0.571F); //1
                    uv_coordinate_[2] = new Vector2(1.0F, 0.0F); //2
                    uv_coordinate_[3] = new Vector2(1.0F, 0.571F); //3

                    uv_coordinate_[4] = new Vector2(0.0F, 0.571F); //4
                    uv_coordinate_[5] = new Vector2(0.0F, 1.0F); //5
                    uv_coordinate_[6] = new Vector2(1.0F, 0.571F); //6
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

            case 8:
                {
                    // ソファータイプ8
                    //カラー =
                    //材質 = 
                    //模様 = 
                    //形状 = 
                    //その他 = 
                   
                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 8, 6 }; //中心のグリッド座標
                    put_point_ = new int[2] { 8, 6 }; //上に乗る家具の中心が合わせる座標
                                                       //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 12 }; //1
                    grid_point_[2] = new int[2] { 16, 0 }; //2
                    grid_point_[3] = new int[2] { 16, 12 }; //3

                    texture_ = Resources.Load<Texture2D>("sofa/sofa_8_grid"); //テクスチャはそのうち変える
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
                    // ソファータイプ9
                    //カラー =
                    //材質 = 
                    //模様 = 
                    //形状 = 
                    //その他 = 
                    //

                    object_type_ = ObjectType.Normal;
                    children_number_ = 2;
                    center_point_ = new int[2] { 23, 19 }; //中心のグリッド座標
                    put_point_ = new int[2] { 23, 19 }; //上に乗る家具の中心が合わせる座標
                                                        //使用する頂点グリッド
                    vertices_number_ = 8;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 18, 0 }; //0
                    grid_point_[1] = new int[2] { 18, 14 }; //1
                    grid_point_[2] = new int[2] { 30, 0 }; //2
                    grid_point_[3] = new int[2] { 30, 14 }; //3

                    grid_point_[4] = new int[2] { 0, 14 }; //4
                    grid_point_[5] = new int[2] { 0, 25 }; //5
                    grid_point_[6] = new int[2] { 30, 14 }; //6
                    grid_point_[7] = new int[2] { 30, 25 }; //7

                    texture_ = Resources.Load<Texture2D>("sofa/sofa_9_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.6F, 0.0F); //0
                    uv_coordinate_[1] = new Vector2(0.6F, 0.56F); //1
                    uv_coordinate_[2] = new Vector2(1.0F, 0.0F); //2
                    uv_coordinate_[3] = new Vector2(1.0F, 0.56F); //3

                    uv_coordinate_[4] = new Vector2(0.0F, 0.56F); //4
                    uv_coordinate_[5] = new Vector2(0.0F, 1.0F); //5
                    uv_coordinate_[6] = new Vector2(1.0F, 0.56F); //6
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

            case 10:
                {
                    // ソファータイプ10
                    //カラー =
                    //材質 = 
                    //模様 = 
                    //形状 = 
                    //その他 = 

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 6, 9 }; //中心のグリッド座標
                    put_point_ = new int[2] { 6, 9 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 18 }; //1
                    grid_point_[2] = new int[2] { 12, 0 }; //2
                    grid_point_[3] = new int[2] { 12, 18 }; //3

                    texture_ = Resources.Load<Texture2D>("sofa/sofa_10_grid"); //テクスチャはそのうち変える
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
                    // ソファータイプ11
                    //カラー =
                    //材質 = 
                    //模様 = 
                    //形状 = 
                    //その他 = 

                    object_type_ = ObjectType.Normal;
                    children_number_ = 1;
                    center_point_ = new int[2] { 8, 7 }; //中心のグリッド座標
                    put_point_ = new int[2] { 8, 7 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 14 }; //1
                    grid_point_[2] = new int[2] { 17, 0 }; //2
                    grid_point_[3] = new int[2] { 17, 14 }; //3

                    texture_ = Resources.Load<Texture2D>("sofa/sofa_11_grid"); //テクスチャはそのうち変える
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