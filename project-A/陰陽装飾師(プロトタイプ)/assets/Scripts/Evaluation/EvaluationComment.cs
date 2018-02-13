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
        int comment_num = 5; //ゲーム終了時のコメント数

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
                    //if()
                    {

                    }
                    comment_.Add("部屋全体が陰気に偏っています．すべての運勢に悪影響を及ぼします.");
                    break;
                case CommentIdentifier.OverYang:
                    if (comment_support_.IndexOf(CommentSupport.ChemicalOrPlastic) >= 0)
                    {
                        comment_.Add("陽気が強いのは悪くはありませんがいくら何でも高すぎです．仕事運，健康運，恋愛運に悪影響およぼします.プラスチックや化学繊維は非常に強い陽気をもっているので風水的にはお勧めできません");
                    }
                    comment_.Add("陽気が強いのは悪くはありませんがいくら何でも高すぎです．仕事運，健康運，恋愛運に悪影響およぼします.");
                    break;
                case CommentIdentifier.EntranceYinNorthEast:
                    comment_.Add("北東は鬼門と呼ばれ，邪気が入りやすくなっています．玄関を北東に置く際は特に陰気の強さに注意しないと全ての運気が下がってしまいます．");
                    break;
                case CommentIdentifier.EntranceYinSouthWest:
                    comment_.Add("南西は裏鬼門と呼ばれ，邪気が入りやすくなっています．玄関を南西に置く際は特に陰気の強さに注意しないと全ての運気が下がってしまいます．");
                    break;
                case CommentIdentifier.EntranceNoCarpet:
                    comment_.Add("玄関マットは，外から持ち込んだ悪い気を払う効果があります．玄関には必ず玄関マットを敷きましょう．");
                    break;
                case CommentIdentifier.EntranceSmell:
                    comment_.Add("良い運気は良い香りのするところを好みます．運気の入り口である玄関に良い香りがするアイテムを置いてみてはどうでしょうか．");
                    break;
                case CommentIdentifier.EntranceMulti:
                    //優先度高め?
                    comment_.Add("運気が下がる原因となる家具配置が多いです．玄関は，運気の入り口であり，家具配置による運気の影響を非常に受けやすくなっています．より一層慎重に家具を配置しましょう");
                    break;
                case CommentIdentifier.BedroomLuckyDirection:
                    comment_.Add("寝室の吉方位は北，西，北西です．寝室に少しでも多くの運気を取り入れたい場合はいっそ寝室の方位を変えてしまうのも手かもしれません．");
                    break;
                case CommentIdentifier.BedroomNorthEast:
                    comment_.Add("北東の寝室は金運アップにつながります．寝室で少しでも多くの金運を取り入れたい場合はいっそ寝室の方位を変えてしまうのも手かもしれません．");
                    break;
                case CommentIdentifier.BedroomBeauty:
                    comment_.Add("東，南東，西，北西の寝室は美容運アップにつながります．美容と関係が深い人気運，健康運，恋愛運を少しでも多く取り入れたい場合はいっそ寝室の方位を変えてしまうのも手かもしれません．");
                    break;
                case CommentIdentifier.BedroomWoodNatural:
                    comment_.Add("天然素材の家具や木製の家具は癒し効果があり，寝室と相性が良いです．健康運を上げるためにもできるだけ木製家具を置きましょう. ");
                    break;
                case CommentIdentifier.BedroomBlue:
                    comment_.Add("青色は安眠に効果的な色です．寝室に青系の色をより多く取り入れて，健康運アップを目指しましょう．");
                    break;
                case CommentIdentifier.BedroomMulti:
                    //寝室の総括的な
                    comment_.Add("運気が下がる原因となる家具配置が多いです．寝室は寝ている間に運気を吸収する部屋と言われ，玄関ほどではありませんが，家具配置によるによる運気の影響を受けやすくなっています．より一層慎重に家具配置を心掛けましょう．");
                    break;
                case CommentIdentifier.LivingMulti:
                    comment_.Add("仕事運，人気運，健康運が下がる原因となる家具配置が多いです．リビングは家庭運を司り，それに関連する仕事運，人気運，健康運が関係する運気の影響を受けやすくなっています．より一層慎重に家具配置を心掛けましょう．");
                    break;
                case CommentIdentifier.KitchenDamage:
                    comment_.Add("西，南西は食べ物が傷みやすくなる方位であり，キッチンをその方向に配置すると，健康運を下げてしまいます．家具配置の際は下がった健康運を上げるように心がけましょう．");
                    break;
                case CommentIdentifier.KitchenAiry:
                    comment_.Add("東，南東，北西の方角は風通しが良く，水回りであるキッチンとは相性が良いです．健康運が上がるため，少しでも多く健康運を上げたい場合はいっそキッチンの方位を変えてしまうのも手かもしれません(現実的ではありませんが…)");
                    break;
                case CommentIdentifier.WorkroomNorthWest:
                    comment_.Add("北西は「主人の方位」とも言われ，仕事部屋とは相性が良いです．もちろん仕事運アップにつながるため，少しでも多く仕事運を上げたい場合はいっそ仕事部屋の方位を変えてしまうのも手かもしれません．");
                    break;
                case CommentIdentifier.WorkroomMulti:
                    comment_.Add("仕事運が下がる原因となる家具配置が多いです．当然ながら仕事部屋は仕事運と深い関係があり，家具配置による仕事運の影響を受けやすくなっています．より一層慎重に家具配置を心掛けましょう．");
                    break;
                case CommentIdentifier.BathroomAiry:
                    comment_.Add("風通しの良い東，南東は浴室と相性最高であり，健康運を上げることができます．五行の面から見ても水の気が強い浴室，木の気が強い東，南東はこの上なく良い相性なので，いっそ浴室の方位を変えてしまうのも手かもしれません(現実的ではありませんが…)");
                    break;
                case CommentIdentifier.BathroomYin:
                    comment_.Add("浴室の陰気が強いと恋愛運を下げてしまいます．元々浴室は陰気が強いですのである程度陽気が高めな家具を配置するなどしましょう．ただし浴室は水の気が持ち，火の気と相性が悪いので火の気の強い家具は置くのを控えましょう．");
                    break;
                case CommentIdentifier.WeakNorth:
                    if (elements_[4] < 50)
                    {
                        if (sokoku_stock_[4] > 100)
                        {
                            //土の気が水の気を消している
                            comment_.Add("北は金運，恋愛運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．北の力は特に水の気を高めることで高まり，" +
                                "また，北は元々水の気を持っているのですが，土の気によって消されています．水と土は相性が悪いので土の気を高めすぎないようにしましょう．");
                        }
                        else if (sokoku_stock_[1] > 70)
                        {
                            //水の気が火の気を消している
                            comment_.Add("北は金運，恋愛運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．北の力は特に水の気を高めることで高まり，" +
                             "また，北は元々水の気を持っているのですが，火の気によって消されています．水と火は相性が悪いので火の気を高めすぎないようにしましょう．");
                        }
                        else if (sosho_stock_[0] > 300)
                        {
                            //木の気が水の気を吸収してしまっている．
                            comment_.Add("北は金運，恋愛運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．北の力は特に水の気を高めることで高まり，" +
                              "また，北は元々水の気を持っているのですが，木の気によって吸収されすぎています．水と木は相性が良いのですが，調子に乗って木の気を高めすぎないようにしましょう．");
                        }
                        else
                        {
                            comment_.Add("北は金運，恋愛運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．北の力は特に水の気を高めることで高まり，" +
                             "また，北は元々水の気を持っているのですが，何らかの影響で消されています．土の気，火の気は水の気と相性が悪く，水の気を消してしまうのでそれらの気を高めすぎないようにしましょう．");
                        }
                    }
                    else
                    {
                        comment_.Add("北の方角は金運，恋愛運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．気の強い家具を配置する，または，気の相克効果を避け，相生効果を利用するなどで，部屋の気を高めましょう．");
                    }
                    break;
                case CommentIdentifier.WeakNorthEast:
                    if (elements_[2] < 50)
                    {
                        if (sokoku_stock_[2] > 100)
                        {
                            //木の気が土の気を消している
                            comment_.Add("北東は運を全体的に上げる力を持っていますが部屋の気が低く，力を発揮できていません．北東の力は特に土の気を高めることで高まり，" +
                                "また，北東は元々土の気を持っているのですが，木の気によって消されています．土と木は相性が悪いので木の気を高めすぎないようにしましょう．");
                        }
                        else if (sokoku_stock_[4] > 70)
                        {
                            //水の気が土の気を消している
                            comment_.Add("北東は運を全体的に上げる力を持っていますが部屋の気が低く，力を発揮できていません．北東の力は特に土の気を高めることで高まり，" +
                                 "また，北東は元々土の気を持っているのですが，水の気によって消されています．土と水は相性が悪いので水の気を高めすぎないようにしましょう．");
                        }
                        else if (sosho_stock_[3] > 300)
                        {
                            //金の気が土の気を吸収してしまっている．
                            comment_.Add("北東は運を全体的に上げる力を持っていますが部屋の気が低く，力を発揮できていません．北東の力は特に土の気を高めることで高まり，" +
                              "また，北東は元々土の気を持っているのですが，金の気によって吸収されすぎています．土と金は相性が良いのですが，調子に乗って金の気を高めすぎないようにしましょう．");
                        }
                        else
                        {
                            comment_.Add("北東は運を全体的に上げる力を持っていますが部屋の気が低く，力を発揮できていません．北東の力は特に土の気を高めることで高まり，" +
                             "また，北東は元々土の気を持っているのですが，何らかの影響で消されています．木の気，水の気は土の気と相性が悪く，土の気を消してしまうのでそれらの気を高めすぎないようにしましょう．");
                        }
                    }
                    else
                    {
                        comment_.Add("北東は運を全体的に上げる力を持っていますが部屋の気が低く，力を発揮できていません．気の強い家具を配置する，または，気の相克効果を避け，相生効果を利用するなどして，部屋の気を高めましょう．");
                    }
                    break;
                case CommentIdentifier.MinusNorthEast:
                    comment_.Add("北東は変化を司る方位であり，運気が変化しやすい方位でもあります．部屋の陰陽のバランスが取れていないと，せっかくの部屋の気がマイナスに作用してしまうので陰陽バランスには特に注意しましょう");
                    break;
                case CommentIdentifier.WeakEast:
                    if (elements_[0] < 50)
                    {
                        if (sokoku_stock_[0] > 100)
                        {
                            //金の気が木の気を消している
                            comment_.Add("東は仕事運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．東の力は特に木の気を高めることで高まり，" +
                                "また，東は元々木の気を持っているのですが，金の気によって消されています．木と金は相性が悪いのであまり金の気を高めないようにしましょう．");
                        }
                        else if (sokoku_stock_[2] > 70)
                        {
                            //土の気が木の気を消している
                            comment_.Add("東は仕事運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．東の力は特に木の気を高めることで高まり，" +
                               "また，東は元々木の気を持っているのですが，土の気によって消されています．木と土は相性が悪いのであまり土の気を高めすぎないようにしましょう．");
                        }
                        else if (sosho_stock_[1] > 300)
                        {
                            //火の気が木の気を吸収してしまっている．
                            comment_.Add("東は仕事運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．東の力は特に木の気を高めることで高まり，" +
                                  "また，東は元々木の気を持っているのですが，火の気によって吸収されすぎています．木と火は相性が良いのですが，調子に乗って火の気を高めすぎないようにしましょう．");
                        }
                        else
                        {
                            comment_.Add("東は仕事運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．東の力は特に木の気を高めることで高まり，" +
                            "また，東は元々木の気を持っているのですが，何らかの影響で消されています．土の気，金の気は木の気と相生が悪いので，それらの気が強い家具は置かないようにしましょう．");
                        }
                    }
                    else
                    {
                        comment_.Add("東は仕事運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．気の強い家具を配置する，または，気の相克効果を避け，相生効果を利用するなどして，部屋の気を高めましょう．");
                    }
                    break;
                case CommentIdentifier.WeakSouthEast:
                    if (elements_[0] < 50)
                    {
                        if (sokoku_stock_[0] > 100)
                        {
                            //金の気が木の気を消している
                            comment_.Add("南東は人気運，恋愛運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．南東の力は特に木の気を高めることで高まり，" +
                                "また，南東は元々木の気を持っているのですが，金の気によって消されています．木と金は相性が悪いのであまり金の気を高めないようにしましょう．");
                        }
                        else if (sokoku_stock_[2] > 70)
                        {
                            //土の気が木の気を消している
                            comment_.Add("南東は人気運，恋愛運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．南東の力は特に木の気を高めることで高まり，" +
                               "また，南東は元々木の気を持っているのですが，土の気によって消されています．木と土は相性が悪いのであまり土の気を高めすぎないようにしましょう．");
                        }
                        else if (sosho_stock_[1] > 300)
                        {
                            //火の気が木の気を吸収してしまっている．
                            comment_.Add("南東は人気運，恋愛運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．南東の力は特に木の気を高めることで高まり，" +
                                  "また，南東は元々木の気を持っているのですが，火の気によって吸収されすぎています．木と火は相性が良いのですが，調子に乗って火の気を高めすぎないようにしましょう．");
                        }
                        else
                        {
                            comment_.Add("南東は人気運，恋愛運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．南東の力は特に木の気を高めることで高まり，" +
                            "また，南東は元々木の気を持っているのですが，何らかの影響で消されています．土の気，金の気は木の気と相生が悪いので，それらの気が強い家具は置かないようにしましょう．");
                        }
                    }
                    else
                    {
                        comment_.Add("南東は人気運，恋愛運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．気の強い家具を配置する，または，気の相克効果を避け，相生効果を利用するなどして，部屋の気を高めましょう．");
                    }
                    break;
                case CommentIdentifier.WeakSouth:
                    if (elements_[1] < 50)
                    {
                        if (sokoku_stock_[1] > 100)
                        {
                            //水の気が火の気を消している
                            comment_.Add("南は人気運，健康運，恋愛運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．南の力は特に火の気を高めることで高まり，" +
                                "また，南は元々火の気を持っているのですが，水の気によって消されています．火と水は相性が悪いのであまり水の気を高めないようにしましょう．");
                        }
                        else if (sokoku_stock_[3] > 70)
                        {
                            //金の気が火の気を消している
                            comment_.Add("南は人気運，健康運，恋愛運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．南の力は特に火の気を高めることで高まり，" +
                               "また，南は元々火の気を持っているのですが，金の気によって消されています．火と金は相性が悪いのであまり金の気を高めすぎないようにしましょう．");
                        }
                        else if (sosho_stock_[2] > 300)
                        {
                            //土の気が火の気を吸収してしまっている．
                            comment_.Add("南は人気運，健康運，恋愛運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．南の力は特に火の気を高めることで高まり，" +
                                  "また，南は元々火の気を持っているのですが，土の気によって吸収されすぎています．火と土は相性が良いのですが，調子に乗って土の気を高めすぎないようにしましょう．");
                        }
                        else
                        {
                            comment_.Add("南は人気運，健康運，恋愛運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．南の力は特に火の気を高めることで高まり，" +
                            "また，南は元々火の気を持っているのですが，何らかの影響で消されています．水の気，金の気は火の気と相生が悪いので，それらの気が強い家具は置かないようにしましょう．");
                        }
                    }
                    else
                    {
                        comment_.Add("南は人気運，健康運，恋愛運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．気の強い家具を配置する，または，気の相克効果を避け，相生効果を利用するなどして，部屋の気を高めましょう．");
                    }
                    break;
                case CommentIdentifier.WeakSouthWest:
                    if (elements_[2] < 50)
                    {
                        if (sokoku_stock_[2] > 100)
                        {
                            //木の気が土の気を消している
                            comment_.Add("南西は仕事運，人気運，健康運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．南西の力は特に土の気を高めることで高まり，" +
                                "また，南西は元々土の気を持っているのですが，木の気によって消されています．土と木は相性が悪いので木の気を高めすぎないようにしましょう．");
                        }
                        else if (sokoku_stock_[4] > 70)
                        {
                            //水の気が土の気を消している
                            comment_.Add("南西は仕事運，人気運，健康運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．南西の力は特に土の気を高めることで高まり，" +
                                 "また，南西は元々土の気を持っているのですが，水の気によって消されています．土と水は相性が悪いので水の気を高めすぎないようにしましょう．");
                        }
                        else if (sosho_stock_[3] > 300)
                        {
                            //金の気が土の気を吸収してしまっている．
                            comment_.Add("南西は仕事運，人気運，健康運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．南西の力は特に土の気を高めることで高まり，" +
                              "また，南西は元々土の気を持っているのですが，金の気によって吸収されすぎています．土と金は相性が良いのですが，調子に乗って金の気を高めすぎないようにしましょう．");
                        }
                        else
                        {
                            comment_.Add("南西は仕事運，人気運，健康運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．南西の力は特に土の気を高めることで高まり，" +
                             "また，南西は元々土の気を持っているのですが，何らかの影響で消されています．木の気，水の気は土の気と相性が悪く，土の気を消してしまうのでそれらの気を高めすぎないようにしましょう．");
                        }
                    }
                    else
                    {
                        comment_.Add("南西は仕事運，人気運，健康運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．気の強い家具を配置する，または，気の相克効果を避け，相生効果を利用するなどして，部屋の気を高めましょう．");
                    }
                    break;
                case CommentIdentifier.WeakWest:
                    if (elements_[3] < 50)
                    {
                        if (sokoku_stock_[3] > 100)
                        {
                            //火の気が金の気を消している
                            comment_.Add("西は金運，恋愛運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．西の力は特に金の気を高めることで高まり，" +
                                "また，西は元々金の気を持っているのですが，火の気によって消されています．金と火は相性が悪いので火の気を高めすぎないようにしましょう．");
                        }
                        else if (sokoku_stock_[0] > 70)
                        {
                            //木の気が金の気を消している
                            comment_.Add("西は金運，恋愛運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．西の力は特に金の気を高めることで高まり，" +
                                 "また，西は元々金の気を持っているのですが，木の気によって消されています．金と木は相性が悪いので木の気を高めすぎないようにしましょう．");
                        }
                        else if (sosho_stock_[4] > 300)
                        {
                            //水の気が金の気を吸収してしまっている．
                            comment_.Add("西は金運，恋愛運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．西の力は特に金の気を高めることで高まり，" +
                              "また，西は元々金の気を持っているのですが，水の気によって吸収されすぎています．金と水は相性が良いのですが，調子に乗って水の気を高めすぎないようにしましょう．");
                        }
                        else
                        {
                            comment_.Add("西は金運，恋愛運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．西の力は特に金の気を高めることで高まり，" +
                             "また，西は元々金の気を持っているのですが，何らかの影響で消されています．木の気，火の気は土の気と相性が悪く，金の気を消してしまうのでそれらの気を高めすぎないようにしましょう．");
                        }
                    }
                    else
                    {
                        comment_.Add("西は金運, 恋愛運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．気の強い家具を配置する，または，気の相克効果を避け，相生効果を利用するなどして，部屋の気を高めましょう．");
                    }
                    break;
                case CommentIdentifier.WeakNorthWest:
                    if (elements_[3] < 50)
                    {
                        if (sokoku_stock_[3] > 100)
                        {
                            //火の気が金の気を消している
                            comment_.Add("北西は仕事運，金運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．北西の力は特に金の気を高めることで高まり，" +
                                "また，北西は元々金の気を持っているのですが，火の気によって消されています．金と火は相性が悪いので火の気を高めすぎないようにしましょう．");
                        }
                        else if (sokoku_stock_[0] > 70)
                        {
                            //木の気が金の気を消している
                            comment_.Add("北西は仕事運，金運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．北西の力は特に金の気を高めることで高まり，" +
                                 "また，北西は元々金の気を持っているのですが，木の気によって消されています．金と木は相性が悪いので木の気を高めすぎないようにしましょう．");
                        }
                        else if (sosho_stock_[4] > 300)
                        {
                            //水の気が金の気を吸収してしまっている．
                            comment_.Add("北西は仕事運，金運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．北西の力は特に金の気を高めることで高まり，" +
                              "また，北西は元々金の気を持っているのですが，水の気によって吸収されすぎています．金と水は相性が良いのですが，調子に乗って水の気を高めすぎないようにしましょう．");
                        }
                        else
                        {
                            comment_.Add("北西は仕事運，金運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．北西の力は特に金の気を高めることで高まり，" +
                             "また，北西は元々金の気を持っているのですが，何らかの影響で消されています．木の気，火の気は土の気と相性が悪く，金の気を消してしまうのでそれらの気を高めすぎないようにしましょう．");
                        }
                    }
                    else
                    {
                        comment_.Add("北西は仕事運，金運を上げる力を持っていますが部屋の気が低く，力を発揮できていません．気の強い家具を配置する，または，気の相克効果を避け，相生効果を利用するなどして，部屋の気を高めましょう．");
                    }
                    break;
                case CommentIdentifier.NorthCold:
                    comment_.Add("北は冷えやすい方位といわれ，もともと健康運，人気運を下げてしまいます．温かみのある家具を置くことでこれらのマイナス要素を抑えることができるので，なるべく温かみのある家具を置きましょう．");
                    break;
                case CommentIdentifier.NorthPink:
                    comment_.Add("北の部屋にピンクの家具を置くと恋愛運を上げることができます．恋愛運が足りないと思ったらぜひピンクの家具を多めに置いてみてください．");
                    break;
                case CommentIdentifier.NorthEastHigh:
                    comment_.Add("北東は背の高い山という意味をもっていますので，背の高い家具と相性が良いです．北東の部屋にはぜひ背の高い家具を多めに置いてみましょう．");
                    break;
                case CommentIdentifier.EastWindSound:
                    comment_.Add("東の部屋では風を連想させる家具，または音の出る家具を置くと人気運，恋愛運が上がりますので，ぜひ風を連想させる家具，音の出る家具を多めに置いてみましょう．");
                    break;
                case CommentIdentifier.SouthEastWindSound:
                    comment_.Add("南東の部屋では風を連想させる家具，または音の出る家具を置くと人気運，恋愛運が上がりますので，ぜひ風を連想させる家具，音の出る家具を多めに置いてみましょう．");
                    break;
                case CommentIdentifier.SouthEastOrange:
                    comment_.Add("南東とオレンジは非常に相性が良く，人気運，恋愛運アップにつながりますので，南東の部屋にはぜひオレンジの家具を多めに置いてみましょう．");
                    break;
                case CommentIdentifier.SouthPurification:
                    comment_.Add("南の部屋では強い火の気が悪い運気を燃やしてくれるといわれています．悪い運気が少し多めなのでいっそ木の気にによる相生効果などで火の気をさらに強めてはいかがでしょうか．");
                    break;
                case CommentIdentifier.SouthWestLow:
                    comment_.Add("北西は背の低い土壌という意味をもっていますので，背の低い家具と相性が良いです．北西の部屋にはぜひ背の低い家具を多めに置いてみましょう．");
                    break;
                case CommentIdentifier.WestWestern:
                    comment_.Add("「西」の部屋ということで西洋風の家具と相性が良いです．金運アップにつながりますので，西の部屋にはぜひ西洋風の家具を多めに置いてみましょう．");
                    break;
                case CommentIdentifier.WestLuxury:
                    comment_.Add("西の部屋に高級そうな家具を置くことで更なる金運アップが見込めます．ぜひ高級そうな家具を多めに置いてみましょう．");
                    break;
                case CommentIdentifier.NorthWestLuxury:
                    comment_.Add("主人の方位である北西の部屋に高級そうな家具を置くことで更なる仕事運，金運アップが見込めます．ぜひ高級そうな家具を多めに置いてみましょう．");
                    break;
                case CommentIdentifier.NorthWestLuxuryZero:
                    comment_.Add("主人の方位である北西の部屋に高級そうな家具を置くことで更なる仕事運，金運アップが見込めますが，そのような家具がないと逆に仕事運，金運が下がってしまいますので．北西の部屋には最低一個は高級そうな家具を置いておきましょう．");
                    break;
                case CommentIdentifier.NorthWestSilverGray:
                    comment_.Add("銀色や灰色は北西を象徴する色です．仕事運アップにつながりますので北西の部屋にはぜひ銀色，灰色の家具を多めに置いてみましょう．");
                    break;
                case CommentIdentifier.NorthWestVain:
                    comment_.Add("北西の金の気が強すぎると，独善的な思考を生み出し，仕事運と人気運に悪影響を及ぼしてしまいます．水の気で金の気を吸収してもらうなどして金の気を抑えるようにしましょう．");
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
                    //if(sokoku_stock_ > )
                    {

                    }
                    //else if(sokoku_stock_ > )
                    {

                    }
                    //else
                    {
                        comment_.Add("木の気は仕事運，健康運を上げる作用があるのですが，木の気が弱すぎてその恩恵を十分に受けれていません．仕事運，健康運を上げたい場合はぜひ木の気を上げておきましょう．");
                    }
                    break;
                case CommentIdentifier.WeakFire:
                    comment_.Add("火の気は人気運，健康運，恋愛運を上げる作用があるのですが，火の気が弱すぎてその恩恵を十分に受けれていません．人気運，健康運，恋愛運を上げたい場合はぜひ火の気を上げておきましょう．");
                    break;
                case CommentIdentifier.WeakEarth:
                    comment_.Add("土の気は全体的に運気を上げる作用があるのですが，土の気が弱すぎてその恩恵を十分に受けれていません．まんべんなく運気を上げたい場合はぜひ土の気を上げておきましょう．");
                    break;
                case CommentIdentifier.WeakMetal:
                    comment_.Add("金の気は金運を上げる作用があるのですが，金の気が弱すぎてその恩恵を十分に受けれていません．金運を上げたい場合はぜひ金の気を上げておきましょう．");
                    break;
                case CommentIdentifier.WeakWater:
                    comment_.Add("水の気は仕事運，金運，恋愛運を上げる作用があるのですが，水の気が弱すぎてその恩恵を十分に受けれていません．仕事運，金運，恋愛運を上げたい場合はぜひ水の気を上げておきましょう．");
                    break;
                case CommentIdentifier.WeakEnergy:
                    comment_.Add("全体的に気の力が弱すぎます．もしかして気が弱い家具ばかり置いていませんか？ 相克効果によって各々の気が相殺されていたりしませんか？ 部屋の気の強さは方位のパワーの強さにも影響しますので今一度部屋の家具レイアウトを見直しましょう．");
                    break;
                case CommentIdentifier.OverWood:
                    comment_.Add("いくらなんでも木の気が強すぎます．これでは仕事運に悪影響を及ぼしてしまいます．木の気の強い家具を減らす．火の気で吸収してもらうなどして木の気を抑えましょう．");
                    break;
                case CommentIdentifier.OverFire:
                    comment_.Add("いくらなんでも火の気が強すぎます．これでは仕事運，健康運，恋愛運に悪影響を及ぼしてしまいます．火の気の強い家具を減らす．土の気で吸収してもらうなどして火の気を抑えましょう．");
                    break;
                case CommentIdentifier.OverEarth:
                    comment_.Add("いくらなんでも土の気が強すぎます．これでは健康運に悪影響を及ぼしてしまいます．土の気の強い家具を減らす．金の気で吸収してもらうなどして土の気を抑えましょう．");
                    break;
                case CommentIdentifier.OverMetal:
                    comment_.Add("いくらなんでも金の気が強すぎます．これでは金運に悪影響を及ぼしてしまいます．金の気の強い家具を減らす．水の気で吸収してもらうなどして金の気を抑えましょう．");
                    break;
                case CommentIdentifier.OverWater:
                    comment_.Add("いくらなんでも水の気が強すぎます．これでは仕事運，金運，恋愛運に悪影響を及ぼしてしまいます．水の気の強い家具を減らす．木の気で吸収してもらうなどして水の気を抑えましょう．");
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