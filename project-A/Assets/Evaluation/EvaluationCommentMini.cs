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
                    comment_.Add("陰気を下げましょう");
                    break;
                case CommentIdentifier.OverYang:
                    comment_.Add("陽気を下げましょう");
                    break;
                case CommentIdentifier.EntranceYinNorthEast:
                    // comment_.Add("");
                    break;
                case CommentIdentifier.EntranceYinSouthWest:
                    //  comment_.Add("");
                    break;
                case CommentIdentifier.EntranceNoCarpet:
                    // comment_.Add("");
                    break;
                case CommentIdentifier.EntranceSmell:
                    //   comment_.Add("");
                    break;
                case CommentIdentifier.EntranceMulti:
                    //  comment_.Add("");
                    break;
                case CommentIdentifier.BedroomWoodNatural:
                    comment_.Add("木製家具，天然繊維の家具を置きましょう");
                    break;
                case CommentIdentifier.BedroomBlue:
                    comment_.Add("青色の家具を置きましょう");
                    break;
                case CommentIdentifier.BedroomMulti:
                    //comment_.Add("");
                    break;
                case CommentIdentifier.LivingMulti:
                    //comment_.Add("");
                    break;
                case CommentIdentifier.WorkroomMulti:
                    // comment_.Add("");
                    break;


                    //部屋の方位から受けるパワー関連
                case CommentIdentifier.NorthWeak:
                    comment_.Add("水の気を中心に部屋の気を上げましょう");
                    break;
                case CommentIdentifier.NorthEastWeak:
                    comment_.Add("土の気を中心に部屋の気を上げましょう");
                    break;
                case CommentIdentifier.NorthEastMinus:
                    comment_.Add("陰気を下げましょう");
                    break;
                case CommentIdentifier.EastWeak:
                    comment_.Add("木の気を中心に部屋の気を上げましょう");
                    break;
                case CommentIdentifier.SouthEastWeak:
                    comment_.Add("木の気を中心に部屋の気を上げましょう");
                    break;
                case CommentIdentifier.SouthWeak:
                    comment_.Add("火の気を中心に部屋の気を上げましょう");
                    break;
                case CommentIdentifier.SouthWestWeak:
                    comment_.Add("土の気を中心に部屋の気を上げましょう");
                    break;
                case CommentIdentifier.WestWeak:
                    comment_.Add("金の気を中心に部屋の気を上げましょう");
                    break;
                case CommentIdentifier.NorthWestWeak:
                    comment_.Add("金の気を中心に部屋の気を上げましょう");
                    break;


                //部屋の小方位(部屋の中の方位)から受けるパワー関連
                case CommentIdentifier.SplitNorthWeak:
                    comment_.Add("部屋の北側の水の気を高めましょう");
                    break;
                case CommentIdentifier.SplitNorthEastWeak:
                    comment_.Add("部屋の北西側の土の気を高めましょう");
                    break;
                case CommentIdentifier.SplitNorthEastMinus:
                    comment_.Add("陰気を下げましょう");
                    break;
                case CommentIdentifier.SplitEastWeak:
                    comment_.Add("部屋の東側の木の気を高めましょう");
                    break;
                case CommentIdentifier.SplitSouthEastWeak:
                    comment_.Add("部屋の南東側の木の気を高めましょう");
                    break;
                case CommentIdentifier.SplitSouthWeak:
                    comment_.Add("部屋の南側の火の気を高めましょう");
                    break;
                case CommentIdentifier.SplitSouthWestWeak:
                    comment_.Add("部屋の南西側の土の気を高めましょう");
                    break;
                case CommentIdentifier.SplitWestWeak:
                    comment_.Add("部屋の西側の金の気を高めましょう");
                    break;
                case CommentIdentifier.SplitNorthWestWeak:
                    comment_.Add("部屋の北西側の金の気を高めましょう");
                    break;



                case CommentIdentifier.NorthCold:
                    comment_.Add("あたたかみのある家具を置きましょう");
                    break;
                case CommentIdentifier.NorthPink:
                    comment_.Add("ピンクの家具を置きましょう");
                    break;
                case CommentIdentifier.NorthEastHigh:
                    comment_.Add("背の高い家具を置きましょう");
                    break;
                case CommentIdentifier.EastWindSound:
                    comment_.Add("風を連想させる家具を置きましょう");
                    break;
                case CommentIdentifier.SouthEastWindSound:
                    comment_.Add("風を連想させる家具を置きましょう");
                    break;
                case CommentIdentifier.SouthEastOrange:
                    comment_.Add("オレンジの家具を置きましょう");
                    break;
                case CommentIdentifier.SouthPurification:
                    comment_.Add("火の気を高める，または陰気を下げましょう");
                    break;
                case CommentIdentifier.SouthWestLow:
                    comment_.Add("背の低い家具を置きましょう");
                    break;
                case CommentIdentifier.WestWestern:
                    comment_.Add("西洋風の家具を置きましょう");
                    break;
                case CommentIdentifier.WestLuxury:
                    comment_.Add("高級そうな家具を置きましょう");
                    break;
                case CommentIdentifier.NorthWestLuxury:
                    comment_.Add("高級そうな家具を置きましょう");
                    break;
                case CommentIdentifier.NorthWestLuxuryZero:
                    comment_.Add("高級そうな家具を最低1個は置きましょう");
                    break;
                case CommentIdentifier.NorthWestSilverGray:
                    comment_.Add("灰色の家具を置きましょう");
                    break;
                case CommentIdentifier.NorthWestVain:
                    comment_.Add("金の気が強すぎます");
                    break;
                case CommentIdentifier.WhiteResetYinYang:
                    comment_.Add("白い家具を置きましょう");
                    break;
                case CommentIdentifier.WhitePurification:
                    comment_.Add("白い家具を置きましょう");
                    break;
                case CommentIdentifier.BlackStrengthening:
                    comment_.Add("黒い家具を置きましょう");
                    break;
                case CommentIdentifier.RedOne:
                    comment_.Add("赤い家具を置きましょう");
                    break;
                case CommentIdentifier.PinkOne:
                    comment_.Add("ピンクの家具を置きましょう");
                    break;
                case CommentIdentifier.PinkNoOrange:
                    comment_.Add("ピンクの家具とオレンジの家具を合わせましょう");
                    break;
                case CommentIdentifier.BlueOne:
                    comment_.Add("青い家具を置きましょう");
                    break;
                case CommentIdentifier.BlueNoOrange:
                    comment_.Add("青い家具とオレンジの家具を合わせましょう");
                    break;
                case CommentIdentifier.OrangeNoPink:
                    comment_.Add("ピンクの家具とオレンジの家具を合わせましょう");
                    break;
                case CommentIdentifier.OrangeNoBlue:
                    comment_.Add("青い家具とオレンジの家具を合わせましょう");
                    break;
                case CommentIdentifier.YellowBrownOne:
                    comment_.Add("");
                    break;
                case CommentIdentifier.GreenPurification:
                    comment_.Add("");
                    break;
                case CommentIdentifier.BeigeFew:
                    comment_.Add("ベージュの家具を置きましょう");
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

                    //ここから部屋の気の関係(気が弱い)
                case CommentIdentifier.WoodWeak:
                    comment_.Add("木の気を高めましょう");
                    break;
                case CommentIdentifier.FireWeak:
                    comment_.Add("火の気を高めましょう");
                    break;
                case CommentIdentifier.EarthWeak:
                    comment_.Add("土の気を高めましょう");
                    break;
                case CommentIdentifier.MetalWeak:
                    comment_.Add("金の気を高めましょう");
                    break;
                case CommentIdentifier.WaterWeak:
                    comment_.Add("水の気を高めましょう");
                    break;
                case CommentIdentifier.WeakEnergy:
                    comment_.Add("部屋の気が弱すぎます");
                    break;

                //相生効果によって気が消された
                case CommentIdentifier.WoodWeakSosho:
                    comment_.Add("木の気が火の気に吸収されすぎています");
                    break;
                case CommentIdentifier.FireWeakSosho:
                    comment_.Add("火の気が土の気に吸収されすぎています");
                    break;
                case CommentIdentifier.EarthWeakSosho:
                    comment_.Add("土の気が金の気に吸収されすぎています");
                    break;
                case CommentIdentifier.MetalWeakSosho:
                    comment_.Add("金の気が水の気に吸収されすぎています");
                    break;
                case CommentIdentifier.WaterWeakSosho:
                    comment_.Add("水の気が木の気に吸収されすぎています");
                    break;

                //相克効果によって気が消された
                case CommentIdentifier.WoodWeakSokoku:
                    comment_.Add("木の気が金の気，または土の気によって消されています");
                    break;
                case CommentIdentifier.FireWeakSokoku:
                    comment_.Add("火の気が水の気，または金の気によって消されています");
                    break;
                case CommentIdentifier.EarthWeakSokoku:
                    comment_.Add("土の気が木の気，または水の気によって消されています");
                    break;
                case CommentIdentifier.MetalWeakSokoku:
                    comment_.Add("金の気が火の気，または木の気によって消されています");
                    break;
                case CommentIdentifier.WaterWeakSokoku:
                    comment_.Add("水の気が土の気，または火の気によって消されています");
                    break;

                //気が強すぎる
                case CommentIdentifier.WoodOver:
                    comment_.Add("木の気が強すぎます");
                    break;
                case CommentIdentifier.FireOver:
                    comment_.Add("火の気が強すぎます");
                    break;
                case CommentIdentifier.EarthOver:
                    comment_.Add("土の気が強すぎます");
                    break;
                case CommentIdentifier.MetalOver:
                    comment_.Add("金の気が強すぎます");
                    break;
                case CommentIdentifier.WaterOver:
                    comment_.Add("水の気が強すぎます");
                    break;


                //ここからベッドの内容
                case CommentIdentifier.BedLiving:
                    comment_.Add("ベッドを置かないようにしましょう");
                    break;
                case CommentIdentifier.BedWorkroom:
                    comment_.Add("ベッドを置かないようにしましょう");
                    break;
                case CommentIdentifier.BedNoBedroom:
                    comment_.Add("最低限ベッドは置きましょう");
                    break;
                case CommentIdentifier.BedPillowDirection:
                    comment_.Add("ベッドの向きを変えましょう");
                    break;
                case CommentIdentifier.BedGapWall:
                    comment_.Add("ベッドと壁の隙間を開けないようにしましょう");
                    break;
                case CommentIdentifier.BedToDoor:
                    comment_.Add("ベッドの枕をドアの正面に向けないようにしましょう");
                    break;
                case CommentIdentifier.BedNearWindow:
                    comment_.Add("ベッドを窓の近くに置かないようにしましょう");
                    break;
                case CommentIdentifier.BedOver:
                    comment_.Add("ベッドが多すぎます");
                    break;


                //ここからタンスの内容
                case CommentIdentifier.CabinetOver:
                    comment_.Add("タンスが多すぎます");
                    break;


                //ここからカーペットの内容
                case CommentIdentifier.CarpetOver:
                    comment_.Add("カーペットが多すぎます");
                    break;


                //ここから机の内容
                case CommentIdentifier.DeskBedroom:
                    comment_.Add("机を置かないようにしましょう");
                    break;
                case CommentIdentifier.DeskNoWorkRoom:
                    comment_.Add("最低限机は置きましょう");
                    break;
                case CommentIdentifier.DeskNorthEast:
                    comment_.Add("机の向きを北，または東向きにしてみましょう");
                    break;
                case CommentIdentifier.DeskSouth:
                    comment_.Add("机の向きを南向きにしてみましょう");
                    break;
                case CommentIdentifier.DeskWest:
                    comment_.Add("机の向きを西向きにしてみましょう");
                    break;
                case CommentIdentifier.DeskOver:
                    comment_.Add("机が多すぎます");
                    break;


                //ここから観葉植物の内容
                case CommentIdentifier.FoliagePlantOver:
                    comment_.Add("観葉植物が多すぎます");
                    break;


                //ここからランプの内容
                case CommentIdentifier.LampOver:
                    comment_.Add("ランプが多すぎます");
                    break;


                //ここからソファーの内容
                case CommentIdentifier.SofaSplitWest:
                    comment_.Add("ソファーは西側に置き, 座席を東に向けましょう");
                    break;
                case CommentIdentifier.SofaToTV:
                    comment_.Add("ソファーの東にテレビを置いて向い合せましょう");
                    break;
                case CommentIdentifier.SofaToDoor:
                    comment_.Add("悪い運気を下げるか，ソファーの座席をドアに向けないようにしましょう");
                    break;
                case CommentIdentifier.SofaOver:
                    comment_.Add("ソファーが多すぎます");
                    break;


                //ここから家電の内容
                case CommentIdentifier.ElectronicsSouth:
                    comment_.Add("南側に家電を置かないようにしましょう");
                    break;
                case CommentIdentifier.ElectronicsNoEast:
                    comment_.Add("東側に家電を置きましょう");
                    break;
                case CommentIdentifier.ElectronicsYang:
                    comment_.Add("家電を置くときは陽気に注意しましょう");
                    break;
                case CommentIdentifier.ElectronicsOver:
                    comment_.Add("家電が多すぎます");
                    break;


                //ここから家具の数量の内容
                case CommentIdentifier.FurnitureFew:
                    comment_.Add("家具が少なすぎます");
                    break;
                case CommentIdentifier.FurnitureOver:
                    comment_.Add("家具が多すぎます");
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
