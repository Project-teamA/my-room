//このクラスはFurnitureGridクラスの分割部分であり，天井ランプのグリッドデータを生成するGetGridDataCeilLampメソッドが実装されている
//
//天井ランプのFurnitureTypeはCeilLamp
//
//方位の指定は特になし
//
//天井掛けである．
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
    partial void GetGridDataCeilLamp(int furniture_ID)
    {
        switch (furniture_ID)
        {
            case 1:
            default:
                {
                    //天井ランプタイプ1

                    object_type_ = ObjectType.CeilingHook;
                    children_number_ = 1;
                    center_point_ = new int[2] { 5, 5 }; //中心のグリッド座標
                    put_point_ = new int[2] { 5, 5 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 10 }; //1
                    grid_point_[2] = new int[2] { 10, 0 }; //2
                    grid_point_[3] = new int[2] { 10, 10 }; //3

                    texture_ = Resources.Load<Texture2D>("ceillamp/ceillamp_1_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.24F, 0.05F); //0
                    uv_coordinate_[1] = new Vector2(0.24F, 0.95F); //1
                    uv_coordinate_[2] = new Vector2(0.76F, 0.05F); //2
                    uv_coordinate_[3] = new Vector2(0.76F, 0.95F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //----------------------------------------------------

                    color_name_.Add(ColorName.Gold);
                    color_name_weight_.Add(3);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Metal);
                    material_type_weight_.Add(2);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Round);
                    form_type_weight_.Add(1);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Light);
                    characteristic_weight_.Add(2);

                    characteristic_.Add(Characteristic.Luxury);
                    characteristic_weight_.Add(2);

                    characteristic_.Add(Characteristic.Western);
                    characteristic_weight_.Add(2);

                    break;
                }

            case 2:
                {
                    //天井ランプタイプ2

                    object_type_ = ObjectType.CeilingHook;
                    children_number_ = 1;
                    center_point_ = new int[2] { 4, 4 }; //中心のグリッド座標
                    put_point_ = new int[2] { 4, 4 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 8 }; //1
                    grid_point_[2] = new int[2] { 8, 0 }; //2
                    grid_point_[3] = new int[2] { 8, 8 }; //3

                    texture_ = Resources.Load<Texture2D>("ceillamp/ceillamp_2_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.31F, 0.12F); //0
                    uv_coordinate_[1] = new Vector2(0.31F, 0.88F); //1
                    uv_coordinate_[2] = new Vector2(0.69F, 0.12F); //2
                    uv_coordinate_[3] = new Vector2(0.69F, 0.88F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { false, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //ダミー

                    //----------------------------------------------------

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(3);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Plastic);
                    material_type_weight_.Add(2);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Round);
                    form_type_weight_.Add(2);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Light);
                    characteristic_weight_.Add(2);

                    break;
                }

        }
    }
}