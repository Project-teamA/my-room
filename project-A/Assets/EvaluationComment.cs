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

public partial class Evaluation : MonoBehaviour
{
	partial void Comment()
    {
        int comment_num_finished = 5; //ゲーム終了時のコメント数
        int comment_num = 3; //ゲーム中のコメント数
     
        //実験用にAllLuck関連にプラス1000をしているだけ(そのうち消す)
        for(int i = 0; i < comment_flag_.Count; ++i)
        {
            if(comment_flag_[i].luck_identifier_ == 5)
            {
                comment_flag_[i].WeightAdd(1000);
            }
        }
        //そのうち消す ここまで

        //ソート処理(ラムダ式を使うらしい)
        comment_flag_.Sort( (a, b) =>  a.flag_weight_ - b.flag_weight_);

        if(is_finished_game_)
        {
            for(int i = 0; i < comment_num_finished; ++i)
            {
                switch (comment_flag_[i].comment_identifier_)
                {
                    case CommentIdentifier.WoodSosho:
                        comment_.Add("木の気の相生");
                        break;
                    case CommentIdentifier.FireSosho:
                        comment_.Add("火の気の相生");
                        break;
                    case CommentIdentifier.EarthSosho:
                        comment_.Add("土の気の相生");
                        break;
                    case CommentIdentifier.MetalSosho:
                        comment_.Add("金の気の相生");
                        break;
                    case CommentIdentifier.WaterSosho:
                        comment_.Add("水の気の相生");
                        break;
                    case CommentIdentifier.WoodSokoku:
                        comment_.Add("木相克");
                        break;
                    case CommentIdentifier.FireSokoku:
                        comment_.Add("火相克");
                        break;
                    case CommentIdentifier.EarthSokoku:
                        comment_.Add("土相克");
                        break;
                    case CommentIdentifier.MetalSokoku:
                        comment_.Add("金相克");
                        break;
                    case CommentIdentifier.WaterSokoku:
                        comment_.Add("水相克");
                        break;
                    case CommentIdentifier.OverYin:
                        //if()
                        {

                        }
                        comment_.Add("部屋全体が陰気に偏っています．すべての運勢に悪影響を及ぼします.");
                        break;
                    case CommentIdentifier.OverYang:
                        //if()
                        {

                        }
                        comment_.Add("陽気が強いのは悪くはありませんがいくら何でも高すぎです．仕事運，健康運，恋愛運に悪影響およぼします.");
                        break;
                    case CommentIdentifier.WeakWork:
                        //if()
                        {
                            //他の評価の補助用?
                        }
                        comment_.Add("仕事運だめ");
                        break;
                    case CommentIdentifier.WeakPopular:
                        //if()
                        {
                            //他の評価の補助用?
                        }
                        comment_.Add("人気運だめ");
                        break;
                    case CommentIdentifier.WeakHealth:
                        //if()
                        {
                            //他の評価の補助用?
                        }
                        comment_.Add("健康運だめ");
                        break;
                    case CommentIdentifier.WeakEconomic:
                        //if()
                        {
                            //他の評価の補助用?
                        }
                        comment_.Add("金運だめ");
                        break;
                    case CommentIdentifier.WeakLove:
                        //if()
                        {
                            //他の評価の補助用?
                        }
                        comment_.Add("恋愛運だめ");
                        break;
                    case CommentIdentifier.WeakAllLuck:
                        comment_.Add("総合運だめ");
                        break;
                    case CommentIdentifier.EntranceYinNorthEast:
                        comment_.Add("北東玄関は");
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
                        comment_.Add("");
                        break;
                    case CommentIdentifier.BedroomLuckyDirection:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.BedroomNorthEast:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.BedroomBeauty:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.BedroomWood:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.BedroomBlue:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.BedroomMulti:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.LivingMulti:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.KitchenDamage:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.KitchenAiry:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.KitchenNorthWest:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.WorkroomNorthWest:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.WorkroomMulti:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.BathroomAiry:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.BathroomYin:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.WeakNorth:
                        //if(Array.IndexOf(comment_support_, CommentSupport.) >= WaterSokoku)
                        {

                        }
                        //else
                        {

                        }
                        comment_.Add("北の方角は金運，恋愛運を上げる力を持っていますがそのパワーが弱っています．北のパワーを上げるには特に水の気が必要です");
                        break;
                    case CommentIdentifier.WeakNorthEast:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.WeakEast:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.WeakSouthEast:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.WeakSouth:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.WeakSouthWest:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.WeakWest:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.WeakNorthWest:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.NorthCold:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.NorthPink:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.NorthEastHigh:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.EastWindSound:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.SouthEastWindSound:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.SouthEastOrange:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.SouthPurification:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.SouthWestLow:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.WestWestern:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.WestLuxury:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.NorthWestLuxury:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.NorthWestLuxuryZero:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.NorthWestSilver:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.NorthWestGray:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.NorthWestVain:
                        comment_.Add("");
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
                        if(room_direction_ == Direction.South)
                        {
                            comment_.Add("赤い家具を一つでも置くと，仕事運，人気運，健康運，恋愛運を上げることができます．ただし原色系の赤は火の気と陽気が非常に強く，南の方角自体も火の気が強いので置きすぎには十分注意しましょう");
                        }
                        else
                        {
                            comment_.Add("赤い家具を一つでも置くと，仕事運，人気運，健康運，恋愛運を上げることができます．ただし原色系の赤は火の気と陽気が非常に強いため置きすぎに注意しましょう．");
                        }
                        break;
                    case CommentIdentifier.PinkOne:
                        comment_.Add("ピンクの家具を置くと，恋愛運が大きく上げることができます．一つでもあれば十分ですので是非置いてみましょう．");
                        break;
                    case CommentIdentifier.PinkNoOrange:
                        comment_.Add("ピンクとオレンジを合わせると，恋愛運を上げることができます．せっかくピンクの家具がありますのでオレンジの家具を置いてみましょう．");
                        break;
                    case CommentIdentifier.BlueOne:
                        if (room_direction_ == Direction.North)
                        {
                            comment_.Add("青い家具を一つでも置くと，仕事運を上げることができます．ただし，北の方角は水の気が強く，青も水の気が強めですので置きすぎには十分注意しましょう");
                        }
                        else
                        {
                            comment_.Add("青い家具を一つでも置くと，仕事運を上げることができます．");
                        }
                        break;
                    case CommentIdentifier.BlueNoOrange:
                        comment_.Add("青とオレンジを合わせると，仕事運を上げることができます．せっかく青の家具がありますのでオレンジの家具を置いてみましょう");
                        break;
                    case CommentIdentifier.OrangeNoPink:
                        comment_.Add("ピンクとオレンジを合わせると，恋愛運を上げることができます．せっかくオレンジの家具がありますのでピンクの家具を置いてみましょう．");
                        break;
                    case CommentIdentifier.OrangeNoBlue:
                        comment_.Add("青とオレンジを合わせると，仕事運を上げることができます．せっかくオレンジの家具がありますので青の家具を置いてみましょう");
                        break;
                    case CommentIdentifier.YellowBrownOne:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.GreenPurification:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.BeigeIneger:
                        comment_.Add("ベージュの家具は置けば置くほど仕事運，恋愛運を上げることができます．");
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
                        comment_.Add("");
                        break;
                    case CommentIdentifier.WeakFire:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.WeakEarth:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.WeakMetal:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.WeakWater:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.WeakEnergy:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.OverWood:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.OverFire:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.OverEarth:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.OverMetal:
                        comment_.Add("");
                        break;
                    case CommentIdentifier.OverWater:
                        comment_.Add("");
                        break;
                }
            }
        }
        else
        {
            for (int i = 0; i < comment_num; ++i)
            {

            }
        }
    }
}
