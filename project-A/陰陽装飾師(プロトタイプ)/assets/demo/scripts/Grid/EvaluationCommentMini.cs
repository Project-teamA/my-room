using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//ゲーム途中のワンポイントアドバイス
public partial class Evaluation : MonoBehaviour
{

    partial void CommentMini()
    {
        int comment_num = 3; //ゲーム終了時のコメント数

        //アドバイスのモードによりコメントの重み変更．
        if (advaice_mode_ == 0)
        {
            //仕事運重視
            for (int i = 0; i < comment_flag_.Count; ++i)
            {
                if (comment_flag_[i].luck_identifier_ == 0)
                {
                    comment_flag_[i].WeightAdd(100000 + 100 * (norma_luck_[0] - luck_[0]));
                }
                else if (comment_flag_[i].luck_identifier_ == 1)
                {
                    comment_flag_[i].WeightAdd(100 * (norma_luck_[1] - luck_[1]));
                }
                else if (comment_flag_[i].luck_identifier_ == 2)
                {
                    comment_flag_[i].WeightAdd(100 * (norma_luck_[2] - luck_[2]));
                }
                else if (comment_flag_[i].luck_identifier_ == 3)
                {
                    comment_flag_[i].WeightAdd(100 * (norma_luck_[3] - luck_[3]));
                }
                else if (comment_flag_[i].luck_identifier_ == 4)
                {
                    comment_flag_[i].WeightAdd(100 * (norma_luck_[4] - luck_[4]));
                }
                else
                {
                    comment_flag_[i].WeightAdd(100 * (all_norma_ - all_luck_));
                }
            }
        }
        else if (advaice_mode_ == 1)
        {
            //人気運重視
            //デフォルト
            for (int i = 0; i < comment_flag_.Count; ++i)
            {
                if (comment_flag_[i].luck_identifier_ == 0)
                {
                    comment_flag_[i].WeightAdd(100 * (norma_luck_[0] - luck_[0]));
                }
                else if (comment_flag_[i].luck_identifier_ == 1)
                {
                    comment_flag_[i].WeightAdd(100000 + 100 * (norma_luck_[1] - luck_[1]));
                }
                else if (comment_flag_[i].luck_identifier_ == 2)
                {
                    comment_flag_[i].WeightAdd(100 * (norma_luck_[2] - luck_[2]));
                }
                else if (comment_flag_[i].luck_identifier_ == 3)
                {
                    comment_flag_[i].WeightAdd(100 * (norma_luck_[3] - luck_[3]));
                }
                else if (comment_flag_[i].luck_identifier_ == 4)
                {
                    comment_flag_[i].WeightAdd(100 * (norma_luck_[4] - luck_[4]));
                }
                else
                {
                    comment_flag_[i].WeightAdd(100 * (all_norma_ - all_luck_));
                }
            }

        }
        else if (advaice_mode_ == 2)
        {
            //健康運重視
            //デフォルト
            for (int i = 0; i < comment_flag_.Count; ++i)
            {
                if (comment_flag_[i].luck_identifier_ == 0)
                {
                    comment_flag_[i].WeightAdd(100 * (norma_luck_[0] - luck_[0]));
                }
                else if (comment_flag_[i].luck_identifier_ == 1)
                {
                    comment_flag_[i].WeightAdd(100 * (norma_luck_[1] - luck_[1]));
                }
                else if (comment_flag_[i].luck_identifier_ == 2)
                {
                    comment_flag_[i].WeightAdd(100000 + 100 * (norma_luck_[2] - luck_[2]));
                }
                else if (comment_flag_[i].luck_identifier_ == 3)
                {
                    comment_flag_[i].WeightAdd(100 * (norma_luck_[3] - luck_[3]));
                }
                else if (comment_flag_[i].luck_identifier_ == 4)
                {
                    comment_flag_[i].WeightAdd(100 * (norma_luck_[4] - luck_[4]));
                }
                else
                {
                    comment_flag_[i].WeightAdd(100 * (all_norma_ - all_luck_));
                }
            }
        }
        else if (advaice_mode_ == 3)
        {
            //金運重視
            //デフォルト
            for (int i = 0; i < comment_flag_.Count; ++i)
            {
                if (comment_flag_[i].luck_identifier_ == 0)
                {
                    comment_flag_[i].WeightAdd(100 * (norma_luck_[0] - luck_[0]));
                }
                else if (comment_flag_[i].luck_identifier_ == 1)
                {
                    comment_flag_[i].WeightAdd(100 * (norma_luck_[1] - luck_[1]));
                }
                else if (comment_flag_[i].luck_identifier_ == 2)
                {
                    comment_flag_[i].WeightAdd(100 * (norma_luck_[2] - luck_[2]));
                }
                else if (comment_flag_[i].luck_identifier_ == 3)
                {
                    comment_flag_[i].WeightAdd(100000 + 100 * (norma_luck_[3] - luck_[3]));
                }
                else if (comment_flag_[i].luck_identifier_ == 4)
                {
                    comment_flag_[i].WeightAdd(100 * (norma_luck_[4] - luck_[4]));
                }
                else
                {
                    comment_flag_[i].WeightAdd(100 * (all_norma_ - all_luck_));
                }
            }
        }
        else if (advaice_mode_ == 4)
        {
            //恋愛運重視
            //デフォルト
            for (int i = 0; i < comment_flag_.Count; ++i)
            {
                if (comment_flag_[i].luck_identifier_ == 0)
                {
                    comment_flag_[i].WeightAdd(100 * (norma_luck_[0] - luck_[0]));
                }
                else if (comment_flag_[i].luck_identifier_ == 1)
                {
                    comment_flag_[i].WeightAdd(100 * (norma_luck_[1] - luck_[1]));
                }
                else if (comment_flag_[i].luck_identifier_ == 2)
                {
                    comment_flag_[i].WeightAdd(100 * (norma_luck_[2] - luck_[2]));
                }
                else if (comment_flag_[i].luck_identifier_ == 3)
                {
                    comment_flag_[i].WeightAdd(100 * (norma_luck_[3] - luck_[3]));
                }
                else if (comment_flag_[i].luck_identifier_ == 4)
                {
                    comment_flag_[i].WeightAdd(100000 + 100 * (norma_luck_[4] - luck_[4]));
                }
                else
                {
                    comment_flag_[i].WeightAdd(100 * (all_norma_ - all_luck_));
                }
            }
        }
        else
        {
            //デフォルト
            for (int i = 0; i < comment_flag_.Count; ++i)
            {
                if (comment_flag_[i].luck_identifier_ == 0)
                {
                    comment_flag_[i].WeightAdd(100 * (norma_luck_[0] - luck_[0]));
                }
                else if (comment_flag_[i].luck_identifier_ == 1)
                {
                    comment_flag_[i].WeightAdd(100 * (norma_luck_[1] - luck_[1]));
                }
                else if (comment_flag_[i].luck_identifier_ == 2)
                {
                    comment_flag_[i].WeightAdd(100 * (norma_luck_[2] - luck_[2]));
                }
                else if (comment_flag_[i].luck_identifier_ == 3)
                {
                    comment_flag_[i].WeightAdd(100 * (norma_luck_[3] - luck_[3]));
                }
                else if (comment_flag_[i].luck_identifier_ == 4)
                {
                    comment_flag_[i].WeightAdd(100 * (norma_luck_[4] - luck_[4]));
                }
                else
                {
                    comment_flag_[i].WeightAdd(100 * (all_norma_ - all_luck_));
                }
            }
        }

        //ソート処理(ラムダ式を使うらしい)
        comment_flag_.Sort((a, b) => b.flag_weight_ - a.flag_weight_);

        for (int i = 0; i < comment_flag_.Count; ++i)
        {
            if (comment_.Count >= comment_num)
            {
                break;
            }

            //ところどころコメントがかぶっています．
            switch (comment_flag_[i].comment_identifier_)
            {
                case CommentIdentifier.OverYin:
                    //if()
                    {

                    }
                    comment_.Add("陰気が強すぎます．");
                    break;
                case CommentIdentifier.OverYang:
                    comment_.Add("陽気が強すぎます．");
                    break;
                case CommentIdentifier.EntranceYinNorthEast:
                    comment_.Add("陰気が強すぎます．");
                    break;
                case CommentIdentifier.EntranceYinSouthWest:
                    comment_.Add("陰気が強すぎます. ");
                    break;
                case CommentIdentifier.EntranceNoCarpet:
                    comment_.Add("玄関マットを敷きましょう．");
                    break;
                case CommentIdentifier.EntranceSmell:
                    comment_.Add("良い香りがする家具を置きましょう．");
                    break;
                case CommentIdentifier.EntranceMulti:
                    //優先度高め?
                    comment_.Add("運気が下がる原因となる家具配置が多いです．");
                    break;
                case CommentIdentifier.BedroomLuckyDirection:
                    //優先度低く
                    comment_.Add("北，西，北西の寝室にしてみては？");
                    break;
                case CommentIdentifier.BedroomNorthEast:
                    comment_.Add("北東の寝室にしてみては？");
                    break;
                case CommentIdentifier.BedroomBeauty:
                    comment_.Add("東，南東，西，北西の寝室にしてみては？");
                    break;
                case CommentIdentifier.BedroomWoodNatural:
                    comment_.Add("木製家具を置きましょう. ");
                    break;
                case CommentIdentifier.BedroomBlue:
                    comment_.Add("青色の家具を置きましょう．");
                    break;
                case CommentIdentifier.BedroomMulti:
                    //寝室の総括的な
                    comment_.Add("運気が下がる原因となる家具配置が多いです．");
                    break;
                case CommentIdentifier.LivingMulti:
                    comment_.Add("仕事運，人気運，健康運が下がる原因となる家具配置が多いです. ");
                    break;
                case CommentIdentifier.KitchenDamage:
                    comment_.Add("健康運を上げるように心がけましょう．");
                    break;
                case CommentIdentifier.KitchenAiry:
                    comment_.Add("東，南東，北西のキッチンにしてみては?");
                    break;
                case CommentIdentifier.WorkroomNorthWest:
                    comment_.Add("北西の仕事部屋にしてみては？");
                    break;
                case CommentIdentifier.WorkroomMulti:
                    comment_.Add("仕事運が下がる原因となる家具配置が多いです．");
                    break;
                case CommentIdentifier.BathroomAiry:
                    comment_.Add("東，南東の浴室にしてみては？");
                    break;
                case CommentIdentifier.BathroomYin:
                    comment_.Add("陰気を減らしましょう．");
                    break;
                case CommentIdentifier.WeakNorth:
                    if (elements_[4] < 50)
                    {
                        if (sokoku_stock_[4] > 100)
                        {
                            //土の気が水の気を消している
                            comment_.Add("北に土の気は相性が悪いです．");
                        }
                        else if (sokoku_stock_[1] > 70)
                        {
                            //水の気が火の気を消している
                            comment_.Add("北に火の気は相性が悪いです．");
                        }
                        else if (sosho_stock_[0] > 300)
                        {
                            //木の気が水の気を吸収してしまっている．
                            comment_.Add("木の気が強すぎます．");
                        }
                        else
                        {
                            comment_.Add("水の気が消されすぎています．");
                        }
                    }
                    else
                    {
                        comment_.Add("部屋の気が弱いです．");
                    }
                    break;
                case CommentIdentifier.WeakNorthEast:
                    if (elements_[2] < 50)
                    {
                        if (sokoku_stock_[2] > 100)
                        {
                            //木の気が土の気を消している
                            comment_.Add("北東に木の気は相性が悪いです．");
                        }
                        else if (sokoku_stock_[4] > 70)
                        {
                            //水の気が土の気を消している
                            comment_.Add("北東に水の気は相性が悪いです．");
                        }
                        else if (sosho_stock_[3] > 300)
                        {
                            //金の気が土の気を吸収してしまっている．
                            comment_.Add("金の気が強すぎます．");
                        }
                        else
                        {
                            comment_.Add("土の気が消されすぎています. ");
                        }
                    }
                    else
                    {
                        comment_.Add("部屋の気が弱いです．");
                    }
                    break;
                case CommentIdentifier.MinusNorthEast:
                    comment_.Add("陰気が強すぎます．");
                    break;
                case CommentIdentifier.WeakEast:
                    if (elements_[0] < 50)
                    {
                        if (sokoku_stock_[0] > 100)
                        {
                            //金の気が木の気を消している
                            comment_.Add("東に金の気は相性が悪いです．");

                        }
                        else if (sokoku_stock_[2] > 70)
                        {
                            //土の気が木の気を消している
                            comment_.Add("東に土の気は相性が悪いです．");
                        }
                        else if (sosho_stock_[1] > 300)
                        {
                            //火の気が木の気を吸収してしまっている．
                            comment_.Add("火の気が強すぎます．");

                        }
                        else
                        {
                            comment_.Add("木の気が消されすぎています．");
                        }
                    }
                    else
                    {
                        comment_.Add("部屋の気が弱いです．");
                    }
                    break;
                case CommentIdentifier.WeakSouthEast:
                    if (elements_[0] < 50)
                    {
                        if (sokoku_stock_[0] > 100)
                        {
                            //金の気が木の気を消している
                            comment_.Add("南東に金の気は相性が悪いです．");
                        }
                        else if (sokoku_stock_[2] > 70)
                        {
                            //土の気が木の気を消している
                            comment_.Add("南東に土の気は相性が悪いです．");
                        }
                        else if (sosho_stock_[1] > 300)
                        {
                            //火の気が木の気を吸収してしまっている．
                            comment_.Add("火の気が強すぎます．");
                        }
                        else
                        {
                            comment_.Add("木の気が消されすぎています．");
                        }
                    }
                    else
                    {
                        comment_.Add("部屋の気が弱いです．");
                    }
                    break;
                case CommentIdentifier.WeakSouth:
                    if (elements_[1] < 50)
                    {
                        if (sokoku_stock_[1] > 100)
                        {
                            //水の気が火の気を消している
                            comment_.Add("南は水の気と相性が悪いです．");
                        }
                        else if (sokoku_stock_[3] > 70)
                        {
                            //金の気が火の気を消している
                            comment_.Add("南は金の気と相性が悪いです．");

                        }
                        else if (sosho_stock_[2] > 300)
                        {
                            //土の気が火の気を吸収してしまっている．
                            comment_.Add("土の気が強すぎます．");
                        }
                        else
                        {
                            comment_.Add("土の気が消されすぎています．");
                        }
                    }
                    else
                    {
                        comment_.Add("部屋の気が弱いです．");
                    }
                    break;
                case CommentIdentifier.WeakSouthWest:
                    if (elements_[2] < 50)
                    {
                        if (sokoku_stock_[2] > 100)
                        {
                            //木の気が土の気を消している
                            comment_.Add("南西に木の気は相性が悪いです．");

                        }
                        else if (sokoku_stock_[4] > 70)
                        {
                            //水の気が土の気を消している
                            comment_.Add("南西に土の気は相性が悪いです．");
                        }
                        else if (sosho_stock_[3] > 300)
                        {
                            //金の気が土の気を吸収してしまっている．
                            comment_.Add("金の気が強すぎます．");
                        }
                        else
                        {
                            comment_.Add("土の気が消されすぎています．");
                        }
                    }
                    else
                    {
                        comment_.Add("部屋の気が弱いです．");
                    }
                    break;
                case CommentIdentifier.WeakWest:
                    if (elements_[3] < 50)
                    {
                        if (sokoku_stock_[3] > 100)
                        {
                            //火の気が金の気を消している
                            comment_.Add("西に火の気は相性が悪いです．");

                        }
                        else if (sokoku_stock_[0] > 70)
                        {
                            //木の気が金の気を消している
                            comment_.Add("西に木の気は相性が悪いです．");
                        }
                        else if (sosho_stock_[4] > 300)
                        {
                            //水の気が金の気を吸収してしまっている．
                            comment_.Add("水の気が強すぎます．");
                        }
                        else
                        {
                            comment_.Add("金の気が消されすぎています．");
                        }
                    }
                    else
                    {
                        comment_.Add("部屋の気が弱いです．");
                    }
                    break;
                case CommentIdentifier.WeakNorthWest:
                    if (elements_[3] < 50)
                    {
                        if (sokoku_stock_[3] > 100)
                        {
                            //火の気が金の気を消している
                            comment_.Add("北西に火の気は相性が悪いです．");

                        }
                        else if (sokoku_stock_[0] > 70)
                        {
                            //木の気が金の気を消している
                            comment_.Add("北西に木の気は相性が悪いです．");

                        }
                        else if (sosho_stock_[4] > 300)
                        {
                            //水の気が金の気を吸収してしまっている．
                            comment_.Add("水の気が強すぎます．");
                        }
                        else
                        {
                            comment_.Add("金の気が消されすぎています．");
                        }
                    }
                    else
                    {
                        comment_.Add("部屋の気が弱いです．");
                    }
                    break;
                case CommentIdentifier.NorthCold:
                    comment_.Add("温かみのある家具を置きましょう．");
                    break;
                case CommentIdentifier.NorthPink:
                    comment_.Add("ピンクの家具を置きましょう．");
                    break;
                case CommentIdentifier.NorthEastHigh:
                    comment_.Add("背の高い家具を置きましょう．");
                    break;
                case CommentIdentifier.EastWindSound:
                    comment_.Add("音の出る家具を置きましょう．");
                    break;
                case CommentIdentifier.SouthEastWindSound:
                    comment_.Add("音の出る家具を置きましょう．");
                    break;
                case CommentIdentifier.SouthEastOrange:
                    comment_.Add("オレンジの家具を置きましょう．");
                    break;
                case CommentIdentifier.SouthPurification:
                    comment_.Add("火の気を強めましょう．");
                    break;
                case CommentIdentifier.SouthWestLow:
                    comment_.Add("背の低い家具を置きましょう．");
                    break;
                case CommentIdentifier.WestWestern:
                    comment_.Add("西洋風の家具を置きましょう．");
                    break;
                case CommentIdentifier.WestLuxury:
                    comment_.Add("高級そうな家具を多めに置きましょう．");
                    break;
                case CommentIdentifier.NorthWestLuxury:
                    comment_.Add("高級そうな家具を多めに置きましょう．");
                    break;
                case CommentIdentifier.NorthWestLuxuryZero:
                    comment_.Add("高級そうな家具を多めに置きましょう．");
                    break;
                case CommentIdentifier.NorthWestSilverGray:
                    comment_.Add("銀色や灰色の家具を置きましょう．");
                    break;
                case CommentIdentifier.NorthWestVain:
                    comment_.Add("金の気が強すぎます．");
                    break;
                case CommentIdentifier.WhiteResetYinYang:
                    comment_.Add("");
                    break;
                case CommentIdentifier.WhitePurification:
                    comment_.Add("");
                    break;
                case CommentIdentifier.BlackStrengthening:
                    comment_.Add("");
                    break;
                case CommentIdentifier.RedOne:
                    if (room_direction_ == Direction.South)
                    {
                        comment_.Add("赤い家具を置きましょう．");
                    }
                    else
                    {
                        comment_.Add("赤い家具を置きましょう．");
                    }
                    break;
                case CommentIdentifier.PinkOne:
                    comment_.Add("ピンクの家具を置きましょう．");
                    break;
                case CommentIdentifier.PinkNoOrange:
                    comment_.Add("ピンクと家具を置きましょう．");
                    break;
                case CommentIdentifier.BlueOne:
                    if (room_direction_ == Direction.North)
                    {
                        comment_.Add("青い家具を置きましょう．");
                    }
                    else
                    {
                        comment_.Add("青い家具を置きましょう．");
                    }
                    break;
                case CommentIdentifier.BlueNoOrange:
                    comment_.Add("オレンジの家具を置きましょう．");
                    break;
                case CommentIdentifier.OrangeNoPink:
                    comment_.Add("ピンクの家具を置きましょう．");
                    break;
                case CommentIdentifier.OrangeNoBlue:
                    comment_.Add("青の家具を置きましょう．");
                    break;
                case CommentIdentifier.YellowBrownOne:
                    comment_.Add("");
                    break;
                case CommentIdentifier.GreenPurification:
                    comment_.Add("");
                    break;
                case CommentIdentifier.BeigeIneger:
                    comment_.Add("ベージュの家具を置きましょう．");
                    break;
                case CommentIdentifier.Cream:
                    comment_.Add("");
                    break;
                case CommentIdentifier.GoldMulti:
                    comment_.Add("");
                    break;
                case CommentIdentifier.SquareFix:
                    comment_.Add("");
                    break;
                case CommentIdentifier.CircleGoodRelation:
                    comment_.Add("");
                    break;
                case CommentIdentifier.CircleCirculation:
                    comment_.Add("");
                    break;
                case CommentIdentifier.SharpBadRelation:
                    comment_.Add("");
                    break;
                case CommentIdentifier.SweetSmell:
                    comment_.Add("");
                    break;
                case CommentIdentifier.Luminescence:
                    comment_.Add("");
                    break;
                case CommentIdentifier.FlowerAssociative:
                    comment_.Add("");
                    break;
                case CommentIdentifier.ExcessFurniture:
                    comment_.Add("");
                    break;
                case CommentIdentifier.ShortageFurniture:
                    comment_.Add("");
                    break;
                case CommentIdentifier.WeakWood:
                    //if(sokoku_stock_ > )
                    {

                    }
                    //else if(sokoku_stock_ > )
                    {

                    }
                    //else
                    {
                        comment_.Add("木の気が低いです．");
                    }
                    break;
                case CommentIdentifier.WeakFire:
                    comment_.Add("火の気が低いです．");
                    break;
                case CommentIdentifier.WeakEarth:
                    comment_.Add("土の気が低いです．");
                    break;
                case CommentIdentifier.WeakMetal:
                    comment_.Add("金の気が低いです．");
                    break;
                case CommentIdentifier.WeakWater:
                    comment_.Add("水の気が低いです．");
                    break;
                case CommentIdentifier.WeakEnergy:
                    comment_.Add("全体的に気の力が弱すぎます．");
                    break;
                case CommentIdentifier.OverWood:
                    comment_.Add("木の気が強すぎます．");
                    break;
                case CommentIdentifier.OverFire:
                    comment_.Add("火の気が強すぎます．");
                    break;
                case CommentIdentifier.OverEarth:
                    comment_.Add("土の気が強すぎます．");
                    break;
                case CommentIdentifier.OverMetal:
                    comment_.Add("金の気が強すぎます．");
                    break;
                case CommentIdentifier.OverWater:
                    comment_.Add("水の気が強すぎます．");
                    break;
                default:
                    comment_.Add("良い家具配置になっているとおもいます．");
                    break;
            }

            for (int j = 0; j < comment_.Count - 1; ++j)
            {
                if (comment_[comment_.Count - 1] == comment_[j])
                {
                    comment_.RemoveAt(comment_.Count - 1);
                    break;
                }
            }

        }

        comment_flag_.Clear();

    }
}