//このクラスは家具グリッドの特徴を元に五行陰陽を自動生成するCreateEnergyメソッドが実装されている

//特性によってに加算される気の式
//
// デフォルトの補正気 + デフォルトの補正気/2 * weight 


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //UnityEventを使用するため
using UnityEngine.EventSystems; //

public partial class FurnitureGrid : MonoBehaviour
{

    partial void CreateEnergy()
    {
        int furniture_strength = 1;

        if (color_name_.Count != color_name_weight_.Count)
        {
            Debug.Log("色設定ででプログラムに不具合があります．");
            return;
        }
        else if (material_type_.Count != material_type_weight_.Count)
        {
            Debug.Log("材質設定でプログラムに不具合があります．");
            return;
        }
        else if (pattern_type_.Count != pattern_type_weight_.Count)
        {
            Debug.Log("模様設定でプログラムに不具合があります．");
            return;
        }
        else if (form_type_.Count != form_type_weight_.Count)
        {
            Debug.Log("形状設定でプログラムに不具合があります．");
            return;
        }
        else if (characteristic_.Count != characteristic_.Count)
        {
            Debug.Log("その他特性設定でプログラムに不具合があります．");
            return;
        }

        switch (furniture_type_)
        {
            case FurnitureType.Bed: //ベッド
                {
                    elements_wood_ += 0;
                    elements_fire_ += 0;
                    elements_earth_ += 0;
                    elements_metal_ += 0;
                    elements_water_ += 0;
                    yin_yang_ += 0;
                    break;
                }
            case FurnitureType.Cabinet: //タンス
                {
                    elements_wood_ += 0;
                    elements_fire_ += 0;
                    elements_earth_ += 0;
                    elements_metal_ += 0;
                    elements_water_ += 0;
                    yin_yang_ += 0;
                    break;
                }
            case FurnitureType.Carpet: //カーペット
                {
                    elements_wood_ += 0;
                    elements_fire_ += 0;
                    elements_earth_ += 0;
                    elements_metal_ += 0;
                    elements_water_ += 0;
                    yin_yang_ += 0;
                    break;
                }
            case FurnitureType.Desk: //机
                {
                    elements_wood_ += 0;
                    elements_fire_ += 0;
                    elements_earth_ += 0;
                    elements_metal_ += 0;
                    elements_water_ += 0;
                    yin_yang_ += 0;
                    break;
                }
            case FurnitureType.FoliagePlant: //観葉植物
                {
                    elements_wood_ += 30;
                    elements_fire_ += 0;
                    elements_earth_ += 0;
                    elements_metal_ += 0;
                    elements_water_ += 0;
                    yin_yang_ += 0;
                    break;
                }
            case FurnitureType.CeilLamp: //天井ランプ
                {
                    elements_wood_ += 0;
                    elements_fire_ += 0;
                    elements_earth_ += 0;
                    elements_metal_ += 0;
                    elements_water_ += 0;
                    yin_yang_ += 0;
                    break;
                }
            case FurnitureType.DeskLamp: //机ランプ
                {
                    elements_wood_ += 0;
                    elements_fire_ += 0;
                    elements_earth_ += 0;
                    elements_metal_ += 0;
                    elements_water_ += 0;
                    yin_yang_ += 0;
                    break;
                }
            case FurnitureType.Sofa: //ソファー
                {
                    elements_wood_ += 0;
                    elements_fire_ += 0;
                    elements_earth_ += 0;
                    elements_metal_ += 0;
                    elements_water_ += 0;
                    yin_yang_ += 0;
                    break;
                }
            case FurnitureType.Table: //テーブル
                {
                    elements_wood_ += 0;
                    elements_fire_ += 0;
                    elements_earth_ += 0;
                    elements_metal_ += 0;
                    elements_water_ += 0;
                    yin_yang_ += 0;
                    break;
                }
            case FurnitureType.ConsumerElectronics: //家電
                {
                    elements_wood_ += 20;
                    elements_fire_ += 0;
                    elements_earth_ += 0;
                    elements_metal_ += 0;
                    elements_water_ += 0;
                    yin_yang_ += 50;
                    break;
                }
            case FurnitureType.Curtain: //カーテン
                {
                    elements_wood_ += 0;
                    elements_fire_ += 0;
                    elements_earth_ += 0;
                    elements_metal_ += 0;
                    elements_water_ += 0;
                    yin_yang_ += 0;
                    break;
                }
            case FurnitureType.Dresser: //鏡
                {
                    elements_wood_ += 0;
                    elements_fire_ += 0;
                    elements_earth_ += 0;
                    elements_metal_ += 0;
                    elements_water_ += 0;
                    yin_yang_ += 0;
                    break;
                }
            default: //その他
                {
                    Debug.Log("この家具はその他です．");
                    break;
                }
        }

        for (int i = 0; i < furniture_subtype_.Count; ++i)
        {
            switch (furniture_subtype_[i])
            {
                case FurnitureType.Bed: //ベッド
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case FurnitureType.Cabinet: //タンス
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case FurnitureType.Carpet: //カーペット
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case FurnitureType.Desk: //机
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case FurnitureType.FoliagePlant: //観葉植物
                    {
                        elements_wood_ += 30;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case FurnitureType.CeilLamp: //天井ランプ
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case FurnitureType.DeskLamp: //机ランプ
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case FurnitureType.Sofa: //ソファー
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case FurnitureType.Table: //テーブル
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case FurnitureType.ConsumerElectronics: //家電
                    {
                        elements_wood_ += 20;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 50;
                        break;
                    }
                case FurnitureType.Curtain: //カーテン
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case FurnitureType.Dresser: //鏡
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                default: //その他
                    {
                        Debug.Log("この家具はその他です．");
                        break;
                    }
            }
        }

        for (int i = 0; i < color_name_.Count; ++i)
        {
            switch (color_name_[i])
            {

                case ColorName.White: //白
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 3 * (1 + color_name_weight_[i]);
                        elements_water_ += 0;
                        yin_yang_ += 4 * (1 + color_name_weight_[i]);
                        break;
                    }
                case ColorName.Black: //黒
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 4 * (1 + color_name_weight_[i]);
                        yin_yang_ += -4 * (1 + color_name_weight_[i]);
                        break;
                    }
                case ColorName.Gray: //灰
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 2 * (1 + color_name_weight_[i]);
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case ColorName.DarkGray: //濃い灰色
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 2 * (1 + color_name_weight_[i]);
                        elements_water_ += 1 * (1 + color_name_weight_[i]);
                        yin_yang_ += -1 * (1 + color_name_weight_[i]);
                        break;
                    }
                case ColorName.Red: //赤
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 4 * (1 + color_name_weight_[i]);
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 5 * (1 + color_name_weight_[i]);
                        break;
                    }
                case ColorName.Pink: //ピンク
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 2 * (1 + color_name_weight_[i]);
                        elements_earth_ += 0;
                        elements_metal_ += 2 * (1 + color_name_weight_[i]);
                        elements_water_ += 0;
                        yin_yang_ += 2 * (1 + color_name_weight_[i]);
                        break;
                    }
                case ColorName.Blue: //青
                    {
                        elements_wood_ += 2 * (1 + color_name_weight_[i]);
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 3 * (1 + color_name_weight_[i]);
                        yin_yang_ += -2 * (1 + color_name_weight_[i]);
                        break;
                    }
                case ColorName.LightBlue: //水色
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 3 * (1 + color_name_weight_[i]);
                        yin_yang_ += 2 * (1 + color_name_weight_[i]);
                        break;
                    }
                case ColorName.Orange: //オレンジ
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 3 * (1 + color_name_weight_[i]);
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 3 * (1 + color_name_weight_[i]);
                        break;
                    }
                case ColorName.Yellow: //黄
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 3 * (1 + color_name_weight_[i]);
                        elements_metal_ += 1 * (1 + color_name_weight_[i]);
                        elements_water_ += 0;
                        yin_yang_ += 3 * (1 + color_name_weight_[i]);
                        break;
                    }
                case ColorName.Green: //緑
                    {
                        elements_wood_ += 4 * (1 + color_name_weight_[i]);
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case ColorName.LightGreen: //黄緑
                    {
                        elements_wood_ += 3 * (1 + color_name_weight_[i]);
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 2 * (1 + color_name_weight_[i]);
                        break;
                    }
                case ColorName.Beige: //ベージュ
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 2 * (1 + color_name_weight_[i]);
                        elements_metal_ += 2 * (1 + color_name_weight_[i]);
                        elements_water_ += 0;
                        yin_yang_ += 2 * (1 + color_name_weight_[i]);
                        break;
                    }
                case ColorName.Cream: //クリーム色
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 2 * (1 + color_name_weight_[i]);
                        elements_water_ += 2 * (1 + color_name_weight_[i]);
                        yin_yang_ += 2 * (1 + color_name_weight_[i]);
                        break;
                    }
                case ColorName.Brown: //茶
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 4 * (1 + color_name_weight_[i]);
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += -1 * (1 + color_name_weight_[i]);
                        break;
                    }
                case ColorName.Ocher: //黄土色
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 3 * (1 + color_name_weight_[i]);
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case ColorName.Gold: //金
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 5 * (1 + color_name_weight_[i]);
                        elements_water_ += 0;
                        yin_yang_ += 5 * (1 + color_name_weight_[i]);
                        break;
                    }
                case ColorName.Silver: //銀
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 4 * (1 + color_name_weight_[i]);
                        elements_water_ += 0;
                        yin_yang_ += -1 * (1 + color_name_weight_[i]);
                        break;
                    }
                case ColorName.Purple: //紫
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 2 * (1 + color_name_weight_[i]);
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += -2 * (1 + color_name_weight_[i]);
                        break;
                    }
                default:
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
            }

        }


        for (int i = 0; i < material_type_.Count; ++i)
        {
            switch (material_type_[i])
            {
                case MaterialType.ArtificialFoliage: //人工観葉植物
                    {
                        elements_wood_ += 2 * (1 + material_type_weight_[i]);
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += -25 * (1 + material_type_weight_[i]);
                        break;
                    }
                case MaterialType.Wooden: //木材
                    {
                        elements_wood_ += 4 * (1 + material_type_weight_[i]);
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case MaterialType.Paper: //紙
                    {
                        elements_wood_ += 3 * (1 + material_type_weight_[i]);
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case MaterialType.Leather: //革
                    {
                        elements_wood_ += 2 * (1 + material_type_weight_[i]);
                        elements_fire_ += 0;
                        elements_earth_ += 2 * (1 + material_type_weight_[i]);
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 2 * (1 + material_type_weight_[i]);
                        break;
                    }
                case MaterialType.NaturalFibre: //天然繊維
                    {
                        elements_wood_ += 1 * (1 + material_type_weight_[i]);
                        elements_fire_ += 0;
                        elements_earth_ += 3 * (1 + material_type_weight_[i]);
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case MaterialType.ChemicalFibre: //化学繊維
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 10 * (1 + material_type_weight_[i]);
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 10 * (1 + material_type_weight_[i]);
                        break;
                    }
                case MaterialType.Cotton: //綿
                    {
                        elements_wood_ += 1 * (1 + material_type_weight_[i]);
                        elements_fire_ += 0;
                        elements_earth_ += 3 * (1 + material_type_weight_[i]);
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case MaterialType.Plastic: //プラスチック
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 10 * (1 + material_type_weight_[i]);
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 10 * (1 + material_type_weight_[i]);
                        break;
                    }
                case MaterialType.Ceramic: //陶磁器
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 4 * (1 + material_type_weight_[i]);
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case MaterialType.Marble: //大理石
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 4 * (1 + material_type_weight_[i]);
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case MaterialType.Metal: //金属
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 4 * (1 + material_type_weight_[i]);
                        elements_water_ += 0;
                        yin_yang_ += 1 * (1 + material_type_weight_[i]);
                        break;
                    }
                case MaterialType.Mineral: //鉱物
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 4 * (1 + material_type_weight_[i]);
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case MaterialType.Glass: //ガラス
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 4 * (1 + material_type_weight_[i]);
                        yin_yang_ += 2 * (1 + material_type_weight_[i]);
                        break;
                    }
                case MaterialType.Water: //水
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 4 * (1 + material_type_weight_[i]);
                        yin_yang_ += -1 * (1 + material_type_weight_[i]);
                        break;
                    }
                default: //その他
                    {
                        break;
                    }
            }
        }

        for (int i = 0; i < pattern_type_.Count; ++i)
        {

            switch (pattern_type_[i])
            {
                case PatternType.Stripe: //ストライプ
                    {
                        elements_wood_ += 2 * (1 + pattern_type_weight_[i]);
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case PatternType.Leaf: //リーフ
                    {
                        elements_wood_ += 3 * (1 + pattern_type_weight_[i]);
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case PatternType.Flower: //花柄
                    {
                        elements_wood_ += 3 * (1 + pattern_type_weight_[i]);
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case PatternType.Star: //星柄
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 2 * (1 + pattern_type_weight_[i]);
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case PatternType.Diamond: //ダイヤ柄
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 2 * (1 + pattern_type_weight_[i]);
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case PatternType.Animal: //アニマル柄
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 2 * (1 + pattern_type_weight_[i]);
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case PatternType.Zigzag: //ジグザグ柄
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 2 * (1 + pattern_type_weight_[i]);
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case PatternType.Novel: //奇抜
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 3 * (1 + pattern_type_weight_[i]);
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 4 * (1 + pattern_type_weight_[i]);
                        break;
                    }
                case PatternType.Border: //ボーダー
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 2 * (1 + pattern_type_weight_[i]);
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case PatternType.Check: //チェック
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 2 * (1 + pattern_type_weight_[i]);
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case PatternType.Tile: //タイル
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 2 * (1 + pattern_type_weight_[i]);
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case PatternType.Dot: //ドット
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 2 * (1 + pattern_type_weight_[i]);
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case PatternType.Round: //●柄
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 2 * (1 + pattern_type_weight_[i]);
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case PatternType.Arch: //アーチ
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 2 * (1 + pattern_type_weight_[i]);
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case PatternType.Fruits: //フルーツ
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 2 * (1 + pattern_type_weight_[i]);
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case PatternType.Luster: //光沢
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 3 * (1 + pattern_type_weight_[i]);
                        elements_water_ += 0;
                        yin_yang_ += 4 * (1 + pattern_type_weight_[i]);
                        break;
                    }
                case PatternType.Wave: //ウェーブストライプ
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 2 * (1 + pattern_type_weight_[i]);
                        yin_yang_ += 0;
                        break;
                    }
                case PatternType.Irregularity: //不規則パターン
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 2 * (1 + pattern_type_weight_[i]);
                        yin_yang_ += 0;
                        break;
                    }
                case PatternType.Cloud: //雲柄
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 2 * (1 + pattern_type_weight_[i]);
                        yin_yang_ += 0;
                        break;
                    }
                default:
                    {

                        break;
                    }
            }
        }

        for (int i = 0; i < form_type_.Count; ++i)
        {
            switch (form_type_[i])
            {
                case FormType.High: //背が高い
                    {
                        elements_wood_ += 2 * (1 + form_type_weight_[i]);
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case FormType.Low: //背が低い
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 2 * (1 + form_type_weight_[i]);
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case FormType.Vertical: //縦長
                    {
                        elements_wood_ += 2 * (1 + form_type_weight_[i]);
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case FormType.Oblong: //横長
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 2 * (1 + form_type_weight_[i]);
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case FormType.Square: //正方形
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 3 * (1 + form_type_weight_[i]);
                        break;
                    }
                case FormType.Rectangle: //長方形
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += -3 * (1 + form_type_weight_[i]);
                        break;
                    }
                case FormType.Round: //円形
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 4 * (1 + form_type_weight_[i]);
                        break;
                    }
                case FormType.Ellipse: //楕円形
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case FormType.Triangle: //三角形
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 2 * (1 + form_type_weight_[i]);
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 3 * (1 + form_type_weight_[i]);
                        break;
                    }
                case FormType.Sharp: //尖っている
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 2 * (1 + form_type_weight_[i]);
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 3 * (1 + form_type_weight_[i]);
                        break;
                    }
                case FormType.Novel: //奇抜
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 3 * (1 + form_type_weight_[i]);
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 4 * (1 + form_type_weight_[i]);
                        break;
                    }
                default: //その他
                    {

                        break;
                    }
            }
        }

        for (int i = 0; i < characteristic_.Count; ++i)
        {
            switch (characteristic_[i])
            {
                case Characteristic.Luxury: //高級そう
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 3 * (1 + characteristic_weight_[i]);
                        break;
                    }
                case Characteristic.Sound: //音が出る
                    {
                        elements_wood_ += 2 * (1 + characteristic_weight_[i]);
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 2 * (1 + characteristic_weight_[i]);
                        break;
                    }
                case Characteristic.Smell: //いいにおい
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 2 * (1 + characteristic_weight_[i]);
                        break;
                    }
                case Characteristic.Light: //発光
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 3 * (1 + characteristic_weight_[i]);
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 4 * (1 + characteristic_weight_[i]);
                        break;
                    }
                case Characteristic.Hard: //硬い
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 2 * (1 + characteristic_weight_[i]);
                        break;
                    }
                case Characteristic.Soft: //やわらかい
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += -2 * (1 + characteristic_weight_[i]);
                        break;
                    }
                case Characteristic.Warm: //温かみ
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 2 * (1 + characteristic_weight_[i]);
                        break;
                    }
                case Characteristic.Cold: //冷たさ
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += -2 * (1 + characteristic_weight_[i]);
                        break;
                    }
                case Characteristic.Flower: //花関連
                    {
                        elements_wood_ += 2 * (1 + characteristic_weight_[i]);
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case Characteristic.Wind: //風関連
                    {
                        elements_wood_ += 2 * (1 + characteristic_weight_[i]);
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case Characteristic.Western: //西洋風
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 2 * (1 + characteristic_weight_[i]);
                        elements_water_ += 0;
                        yin_yang_ += 0;
                        break;
                    }
                case Characteristic.Clutter: //乱雑
                    {
                        elements_wood_ += 0;
                        elements_fire_ += 0;
                        elements_earth_ += 0;
                        elements_metal_ += 0;
                        elements_water_ += 0;
                        yin_yang_ += -90 * (1 + characteristic_weight_[i]);
                        break;
                    }
                default: //その他
                    {
                        break;
                    }
            }
        }
    }
}