//このクラスはFurnitureGridクラスの分割部分であり，家電のグリッドデータを生成するGetGridDataConsumerElectronicsメソッドが実装されている
//
//机のFurnitureTypeはConsumerElectronics
//
//TVの場合，画面が北向きになるように設定
//エアコンの場合空気孔が南向きになるように設定
//
//
//parametaの長さは1
//parameta_[0] = 0 エアコン
//parameta_[0] = 1 空気清浄機
//parameta_[0] = 2 コンロ
//
//エアコンは壁掛け
//(ここからは自分の勝手な判断)
//



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //UnityEventを使用するため
using UnityEngine.EventSystems; //

public partial class FurnitureGrid : MonoBehaviour
{
    partial void GetGridDataConsumerElectronics(int furniture_ID)
    {
        switch (furniture_ID)
        {
            case 1:
            default:
                {
                    //家電1(エアコン)

                    object_type_ = ObjectType.WallMounted;
                    children_number_ = 1;
                    center_point_ = new int[2] { 4, 1 }; //中心のグリッド座標
                    put_point_ = new int[2] { 4, 1 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 2 }; //1
                    grid_point_[2] = new int[2] { 9, 0 }; //2
                    grid_point_[3] = new int[2] { 9, 2 }; //3

                    texture_ = Resources.Load<Texture2D>("electronics/electronics_1_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.14F, 0.35F); //0
                    uv_coordinate_[1] = new Vector2(0.14F, 0.65F); //1
                    uv_coordinate_[2] = new Vector2(0.86F, 0.35F); //2
                    uv_coordinate_[3] = new Vector2(0.86F, 0.65F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { true, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //エアコン

                    //----------------------------------------------------

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(3);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Plastic);
                    material_type_weight_.Add(2);

                    material_type_.Add(MaterialType.Metal);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    pattern_type_.Add(PatternType.Border);
                    pattern_type_weight_.Add(2);

                    //------------------------------------------------------

                    form_type_.Add(FormType.Oblong);
                    form_type_weight_.Add(2);

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(2);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    characteristic_.Add(Characteristic.Wind);
                    characteristic_weight_.Add(2);

                    characteristic_.Add(Characteristic.Sound);
                    characteristic_weight_.Add(1);

                    break;
                }

            case 2:
                {
                    //家電2(エアコン)

                    object_type_ = ObjectType.WallMounted;
                    children_number_ = 1;
                    center_point_ = new int[2] { 4, 1 }; //中心のグリッド座標
                    put_point_ = new int[2] { 4, 1 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 2 }; //1
                    grid_point_[2] = new int[2] { 9, 0 }; //2
                    grid_point_[3] = new int[2] { 9, 2 }; //3

                    texture_ = Resources.Load<Texture2D>("electronics/electronics_2_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.27F, 0.41F); //0
                    uv_coordinate_[1] = new Vector2(0.27F, 0.59F); //1
                    uv_coordinate_[2] = new Vector2(0.73F, 0.41F); //2
                    uv_coordinate_[3] = new Vector2(0.73F, 0.59F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { true, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 0; //エアコン

                    //----------------------------------------------------

                    color_name_.Add(ColorName.Red);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(2);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Plastic);
                    material_type_weight_.Add(2);

                    material_type_.Add(MaterialType.Metal);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Oblong);
                    form_type_weight_.Add(2);

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(2);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Wind);
                    characteristic_weight_.Add(4);

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    characteristic_.Add(Characteristic.Sound);
                    characteristic_weight_.Add(1);

                    break;
                }

            case 3:
                {
                    //家電2(空気清浄機)

                    object_type_ = ObjectType.WallMounted;
                    children_number_ = 1;
                    center_point_ = new int[2] { 1, 1 }; //中心のグリッド座標
                    put_point_ = new int[2] { 1, 1 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 2 }; //1
                    grid_point_[2] = new int[2] { 3, 0 }; //2
                    grid_point_[3] = new int[2] { 3, 2 }; //3

                    texture_ = Resources.Load<Texture2D>("electronics/electronics_3_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.46F, 0.43F); //0
                    uv_coordinate_[1] = new Vector2(0.46F, 0.57F); //1
                    uv_coordinate_[2] = new Vector2(0.54F, 0.43F); //2
                    uv_coordinate_[3] = new Vector2(0.54F, 0.57F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { true, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 1; //空気清浄機

                    //----------------------------------------------------

                    color_name_.Add(ColorName.Black);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.DarkGray);
                    color_name_weight_.Add(2);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Plastic);
                    material_type_weight_.Add(2);

                    material_type_.Add(MaterialType.Metal);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    pattern_type_.Add(PatternType.Flower);
                    pattern_type_weight_.Add(1);

                    //------------------------------------------------------

                    form_type_.Add(FormType.Oblong);
                    form_type_weight_.Add(2);

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(2);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    characteristic_.Add(Characteristic.Sound);
                    characteristic_weight_.Add(1);

                    break;
                }

            case 4:
                {
                    //家電15(テレビ)

                    object_type_ = ObjectType.WallMounted;
                    children_number_ = 1;
                    center_point_ = new int[2] { 4, 1 }; //中心のグリッド座標
                    put_point_ = new int[2] { 4, 1 }; //上に乗る家具の中心が合わせる座標
                                                      //使用する頂点グリッド
                    vertices_number_ = 4;
                    grid_point_ = new int[vertices_number_][];
                    grid_point_[0] = new int[2] { 0, 0 }; //0
                    grid_point_[1] = new int[2] { 0, 1 }; //1
                    grid_point_[2] = new int[2] { 9, 0 }; //2
                    grid_point_[3] = new int[2] { 9, 1 }; //3

                    texture_ = Resources.Load<Texture2D>("electronics/electronics_15_grid"); //テクスチャはそのうち変える
                    Debug.Log(texture_);
                    uv_coordinate_ = new Vector2[vertices_number_];
                    uv_coordinate_[0] = new Vector2(0.34F, 0.45F); //0
                    uv_coordinate_[1] = new Vector2(0.34F, 0.55F); //1
                    uv_coordinate_[2] = new Vector2(0.66F, 0.45F); //2
                    uv_coordinate_[3] = new Vector2(0.66F, 0.55F); //3

                    children_grid_ = new GameObject[children_number_];
                    //頂点インデックス生成
                    triangles_ = new int[children_number_][];
                    triangles_[0] = new int[4] { 0, 1, 2, 3 };

                    //枠線
                    outline_index_ = new int[8] { 0, 2, 2, 3, 3, 1, 1, 0 };
                    blueflag_index_ = new bool[4] { true, false, false, false };

                    //パラメータの設定
                    parameta_[0] = 2; //テレビ

                    //----------------------------------------------------

                    color_name_.Add(ColorName.Black);
                    color_name_weight_.Add(2);

                    color_name_.Add(ColorName.White);
                    color_name_weight_.Add(2);

                    //----------------------------------------------------

                    material_type_.Add(MaterialType.Glass);
                    material_type_weight_.Add(2);

                    material_type_.Add(MaterialType.Plastic);
                    material_type_weight_.Add(2);

                    material_type_.Add(MaterialType.Metal);
                    material_type_weight_.Add(1);

                    //----------------------------------------------------

                    //------------------------------------------------------

                    form_type_.Add(FormType.Rectangle);
                    form_type_weight_.Add(2);

                    //-----------------------------------------------------

                    characteristic_.Add(Characteristic.Sound);
                    characteristic_weight_.Add(4);

                    characteristic_.Add(Characteristic.Sound);
                    characteristic_weight_.Add(3);

                    characteristic_.Add(Characteristic.Hard);
                    characteristic_weight_.Add(2);

                    break;
                }
        }
    }
}