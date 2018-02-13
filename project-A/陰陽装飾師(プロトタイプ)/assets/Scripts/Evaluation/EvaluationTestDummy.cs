using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Evaluation : MonoBehaviour
{

    public void EvaluationTotalTestDummy()
    {
        is_finished_game_ = true;
        advaice_mode_ = 0;
        //数値をいじる
        room_role_ = Room.Bedroom; //部屋の種類
        room_direction_ = Direction.NorthWest; //部屋の方角
        for (int i = 0; i < 5; ++i)
        {
            split_elements_[i] = new int[8];
        }

        for (int i = 0; i < 5; ++i)
        {
            elements_[i] = 0;
            sosho_stock_[i] = 0;
            sokoku_stock_[i] = 0;
            luck_[i] = 0;
            plus_luck_[i] = 0;
            minus_luck_[i] = 0;
        }

        for (int j = 0; j < 8; ++j)
        {
            split_elements_[0][j] = 5;
            split_elements_[1][j] = 0;
            split_elements_[2][j] = 0;
            split_elements_[3][j] = 0;
            split_elements_[4][j] = 0;
        }

        for (int i = 0; i < 8; ++i)
        {
            split_yin_yang_[i] = 0;
        }

        yin_yang_ = 0;
        energy_strength_ = 0;
        all_luck_ = 0;

        EvaluationItem(); //部屋のアイテムによる五行陰陽評価関数
        EvaluationRoom(); //部屋の種類による五行陰陽評価関数(部屋の)
        EvaluationDirection(); //方位五行陰陽評価関数(部屋の)
        EvaluationFiveElements(); //五行による相生効果と相克効果
        EvaluationeLast();

        comment_.Clear(); //コメントの初期化

        //ここから五行の気の強さの加算
        for (int i = 0; i < 5; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                elements_[i] += split_elements_[i][j];
            }

            if (elements_[i] > 300)
            {
                energy_strength_ += 300;
            }
            else
            {
                energy_strength_ += elements_[i];
            }
        }
        //ここまで五行の気の強さの加算

        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            //FortuneItem(furniture_grid_[i]); //アイテムによる運勢補正
        }
        FortuneRoom(); //部屋による運勢補正
        FortuneDirection(); //方位による運勢補正
        FortuneFiveElement(); //五行による運勢補正

        //陰陽による運勢補正
        if (yin_yang_ < -30)
        {
            //陰気が強すぎる
            minus_luck_[0] -= (yin_yang_ + 30);
            minus_luck_[1] -= (yin_yang_ + 30);
            minus_luck_[2] -= (yin_yang_ + 30);
            minus_luck_[4] -= (yin_yang_ + 30);

            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverYang, -(yin_yang_ + 30), 0));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverYang, -(yin_yang_ + 30), 1));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverYang, -(yin_yang_ + 30), 2));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverYang, -(yin_yang_ + 30), 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverYang, -(yin_yang_ + 30) * 4, 5));
        }
        else if (yin_yang_ > 300)
        {
            //陽気が強すぎる
            minus_luck_[0] += (yin_yang_ - 300);
            minus_luck_[2] += (yin_yang_ - 300);
            minus_luck_[3] += (yin_yang_ - 300);

            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverYang, (yin_yang_ - 300), 0));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverYang, (yin_yang_ - 300), 2));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverYang, (yin_yang_ - 300), 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverYang, (yin_yang_ - 300) * 3, 5));
        }

        FortuneLast();

        for (int i = 0; i < 5; ++i)
        {
            luck_[i] = plus_luck_[i] - minus_luck_[i];
        }

        all_luck_ = luck_[0] + luck_[1] + luck_[2] + luck_[3] + luck_[4];

        if (is_finished_game_)
        {
            Comment();
        }
        else
        {
            CommentMini();
        }

        Debug.Log(elements_[0]);
        Debug.Log(elements_[1]);
        Debug.Log(elements_[2]);
        Debug.Log(elements_[3]);
        Debug.Log(elements_[4]);
        Debug.Log(energy_strength_);

        Debug.Log(luck_[0]);
        Debug.Log(luck_[1]);
        Debug.Log(luck_[2]);
        Debug.Log(luck_[3]);
        Debug.Log(luck_[4]);
        Debug.Log(all_luck_);

        for (int i = 0; i < comment_.Count; ++i)
        {
            Debug.Log(comment_[i]);
        }
    }
}