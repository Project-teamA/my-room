//このファイルはEvaluationの分割ファイルであり評価の際のコメントを呼び出す関数である
//
//
// comment_flag_とflag_weight_を参照にして
// comment_とcomment_weight_を設定する
// comment_weight_がより高い5つの文が表示される. 
//
// 基本的には風水コメントその2の条件をもとにコメントを出力する


using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//ゲーム終了時のワンポイントアドバイス
public partial class Evaluation : MonoBehaviour
{
    partial void Comment()
    {
        int comment_num = 6; //ゲーム終了時のコメント数(コメントの実装の仕方の上で偶数にする方が望ましい)

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

            switch (comment_flag_[i].comment_identifier_)
            {
                case CommentIdentifier.OverYin:
                    comment_.Add("部屋全体が陰気に偏っていて，仕事運，人気運，健康運，恋愛運が下がっています.");
                    break;
                case CommentIdentifier.OverYang:
                    comment_.Add("部屋全体が陽気に偏っていて．仕事運，健康運，恋愛運が下がっています.");
                    break;
                case CommentIdentifier.EntranceYinNorthEast:
                    comment_.Add("");
                    break;
                case CommentIdentifier.EntranceYinSouthWest:
                    comment_.Add("");
                    break;
                case CommentIdentifier.EntranceNoCarpet:
                    comment_.Add("");
                    break;
                case CommentIdentifier.EntranceSmell:
                    comment_.Add("");
                    break;
                case CommentIdentifier.EntranceMulti:
                    //優先度高め?
                    comment_.Add("");
                    break;
                case CommentIdentifier.BedroomWoodNatural:
                    comment_.Add("寝室に木製家具，天然繊維の家具を置くことで健康運が上がります. ");
                    break;
                case CommentIdentifier.BedroomBlue:
                    comment_.Add("寝室に青色の家具を多めに置くことで健康運が上がります. ");
                    break;
                case CommentIdentifier.BedroomMulti:
                    //寝室の総括的な
                    comment_.Add("寝室は運気を増幅させる効果がありますが，悪い運気の元となっている家具配置が多く，悪い運気が増幅されています．");
                    break;
                case CommentIdentifier.LivingMulti:
                    comment_.Add("リビングは仕事運，人気運，健康運を増幅させる効果がありますが，悪い運気の元となっている家具配置が多く，悪い運気が増幅されています．");
                    break;

                case CommentIdentifier.WorkroomMulti:
                    comment_.Add("仕事部屋は仕事運を増幅させる効果がありますが，悪い運気の元となっている家具配置が多く，悪い運気が増幅されています．");
                    break;

                case CommentIdentifier.NorthWeak:
                    comment_.Add("部屋の気が弱く，北の部屋が与える金運，恋愛運のパワーが受けられていません");
                    break;
                case CommentIdentifier.NorthEastWeak:
                    comment_.Add("部屋の気が弱く，北東の部屋がもつ運勢のパワーが受けられていません");
                    break;
                case CommentIdentifier.NorthEastMinus:
                    comment_.Add("北東の部屋では，陰陽のバランスが悪いと運勢が大きく下がってしまいます．");
                    break;
                case CommentIdentifier.EastWeak:
                    comment_.Add("部屋の気が弱く，東の部屋が与える仕事運のパワーが受けられていません．");
                    break;
                case CommentIdentifier.SouthEastWeak:
                    comment_.Add("部屋の気が弱く，南東の部屋が与える人気運，恋愛運のパワーが受けられていません．");
                    break;
                case CommentIdentifier.SouthWeak:
                    comment_.Add("部屋の気が弱く，南の部屋が与える人気運，健康運, 恋愛運のパワーが受けられていません．");
                    break;
                case CommentIdentifier.SouthWestWeak:
                    comment_.Add("部屋の気が弱く，南西の部屋が与える仕事運，人気運，健康運のパワーが受けられていません．");
                    break;
                case CommentIdentifier.WestWeak:
                    comment_.Add("部屋の気が弱く，西の部屋が与える金運, 恋愛運のパワーが受けられていません．");
                    break;
                case CommentIdentifier.NorthWestWeak:
                    comment_.Add("部屋の気が弱く，北西の部屋が与える仕事運, 金運のパワーが受けられていません．");
                    break;
                case CommentIdentifier.NorthCold:
                    comment_.Add("北の部屋は健康運, 人気運を下げてしまいます．あたたかみのある家具を多く置くことでそれを回避することができます．");
                    break;
                case CommentIdentifier.NorthPink:
                    comment_.Add("北の部屋にピンクの家具を多めに置くことで恋愛運が上がります. ");
                    break;
                case CommentIdentifier.NorthEastHigh:
                    comment_.Add("北東の部屋に背の高い家具を多めに置くことで運が全体的にが上がります. ");
                    break;
                case CommentIdentifier.EastWindSound:
                    comment_.Add("東の部屋に風を連想させる家具を多めに置くことで人気運，恋愛運が上がります．");
                    break;
                case CommentIdentifier.SouthEastWindSound:
                    comment_.Add("南東の部屋に風を連想させる家具を多めに置くことで人気運，恋愛運が上がります．");
                    break;
                case CommentIdentifier.SouthEastOrange:
                    comment_.Add("南東の部屋にオレンジの家具を多めに置くことで人気運，恋愛運が上がります");
                    break;
                case CommentIdentifier.SouthPurification:
                    comment_.Add("悪い運気が少し強い場合，南の部屋の火の気を高めることで悪い運気をある程度抑えられます．");
                    break;
                case CommentIdentifier.SouthWestLow:
                    comment_.Add("南西の部屋に背の低い家具を多めに置くことで運が全体的に上がります．");
                    break;
                case CommentIdentifier.WestWestern:
                    comment_.Add("西の部屋に西洋風の家具を多めに置くことで金運が上がります．");
                    break;
                case CommentIdentifier.WestLuxury:
                    comment_.Add("西の部屋に高級そうな家具を多めに置くことで金運が上がります．");
                    break;
                case CommentIdentifier.NorthWestLuxury:
                    comment_.Add("北西の部屋に高級そうな家具を多めに置くことで仕事運，金運が上がります．");
                    break;
                case CommentIdentifier.NorthWestLuxuryZero:
                    comment_.Add("北西の部屋に高級そうな家具が一つもないと，仕事運，金運が下がります．");
                    break;
                case CommentIdentifier.NorthWestSilverGray:
                    comment_.Add("北西の部屋に銀色，灰色の家具を多めに置くことで仕事運が上がります．");
                    break;
                case CommentIdentifier.NorthWestVain:
                    comment_.Add("北西の部屋の金の気が強すぎて仕事運，人気運が下がっています．");
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
                    comment_.Add("赤い家具を1つでも置けば，仕事運，人気運，健康運，恋愛運を上げることができます．");
                    break;
                case CommentIdentifier.PinkOne:
                    comment_.Add("ピンクの家具を1つでも置けば，恋愛運を大きく上げることができます．");
                    break;
                case CommentIdentifier.PinkNoOrange:
                    comment_.Add("せっかくピンクの家具がありますのでオレンジの家具を置いてみましょう．恋愛運を上げることができます．");
                    break;
                case CommentIdentifier.BlueOne:
                    comment_.Add("青い家具を一つでも置くと，仕事運を上げることができます．");
                    break;
                case CommentIdentifier.BlueNoOrange:
                    comment_.Add("せっかく青の家具がありますのでオレンジの家具を置いてみましょう. 健康運を上げることができます．");
                    break;
                case CommentIdentifier.OrangeNoPink:
                    comment_.Add("せっかくオレンジの家具がありますのでピンクの家具を置いてみましょう．恋愛運を上げることができます．");
                    break;
                case CommentIdentifier.OrangeNoBlue:
                    comment_.Add("せっかくオレンジの家具がありますので青の家具を置いてみましょう. 健康運を上げることができます．");
                    break;
                case CommentIdentifier.YellowBrownOne:
                    comment_.Add("");
                    break;
                case CommentIdentifier.GreenPurification:
                    comment_.Add("");
                    break;
                case CommentIdentifier.BeigeFew:
                    comment_.Add("ベージュの家具を多く置けば,  仕事運，恋愛運を上げることができます．");
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
                case CommentIdentifier.WoodWeak:
                    comment_.Add("木の気が弱すぎて仕事運，健康運があまり上がっていません．");
                    break;
                case CommentIdentifier.FireWeak:
                    comment_.Add("火の気が弱すぎて人気運，健康運，恋愛運があまり上がっていません. ");
                    break;
                case CommentIdentifier.EarthWeak:
                    comment_.Add("土の気が弱すぎて運気が全体的にあまり上がっていません．");
                    break;
                case CommentIdentifier.MetalWeak:
                    comment_.Add("金の気が弱すぎて金運があまり上がっていません．");
                    break;
                case CommentIdentifier.WaterWeak:
                    comment_.Add("水の気が弱すぎて仕事運，金運，恋愛運があまり上がっていません．");
                    break;
                case CommentIdentifier.WeakEnergy:
                    comment_.Add("全体的に部屋の気が弱すぎます．");
                    break;
                case CommentIdentifier.WoodOver:
                    comment_.Add("木の気が強すぎて仕事運に悪影響を及ぼしています．");
                    break;
                case CommentIdentifier.FireOver:
                    comment_.Add("火の気が強すぎて仕事運，健康運，恋愛運に悪影響を及ぼしています．");
                    break;
                case CommentIdentifier.EarthOver:
                    comment_.Add("土の気が強すぎて健康運に悪影響を及ぼしています．");
                    break;
                case CommentIdentifier.MetalOver:
                    comment_.Add("金の気が強すぎて金運に悪影響を及ぼしています．");
                    break;
                case CommentIdentifier.WaterOver:
                    comment_.Add("水の気が強すぎて人気運，健康運，恋愛運に悪影響を及ぼしています．");
                    break;
                default:
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
