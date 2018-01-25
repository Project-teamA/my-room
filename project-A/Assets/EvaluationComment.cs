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
using UnityEngine;

public partial class Evaluation : MonoBehaviour
{
    //ほんの一例だけ(中途半端)
	partial void Comment()
    {
        if(comment_flag_.Contains(CommentFlag.WeakWork))
        {
           if(comment_flag_.Contains(CommentFlag.WeakWood))
            {
                comment_.Add("木の気が弱すぎて仕事運が上がっていません");

                //flag_weight_利用してcomment_weightを設定
                comment_weight_.Add(1);
            }

            if (comment_flag_.Contains(CommentFlag.WeakFire))
            {
                comment_.Add("火の気が弱すぎて仕事運が上がっていません");

                //flag_weight_利用してcomment_weightを設定
                comment_weight_.Add(1);
            }

            if (comment_flag_.Contains(CommentFlag.WeakEarth))
            {
                comment_.Add("土の気が弱すぎて仕事運が上がっていません");

                //flag_weight_利用してcomment_weightを設定
                comment_weight_.Add(1);
            }

            if (comment_flag_.Contains(CommentFlag.WeakWater))
            {
                comment_.Add("水の気が弱すぎて仕事運が上がっていません");

                //flag_weight_利用してcomment_weightを設定
                comment_weight_.Add(1);
            }

            if (comment_flag_.Contains(CommentFlag.OverWood))
            {
                comment_.Add("木の気が強すぎて仕事運に悪影響を及ぼしています");

                //flag_weight_利用してcomment_weightを設定
                comment_weight_.Add(1);
            }

            if (comment_flag_.Contains(CommentFlag.OverFire))
            {
                comment_.Add("火の気が強すぎて仕事運に悪影響を及ぼしています");

                //flag_weight_利用してcomment_weightを設定
                comment_weight_.Add(1);
            }

            if (comment_flag_.Contains(CommentFlag.OverYang))
            {
                comment_.Add("陽気がつよすぎて仕事運に悪影響を及ぼしています");

                //flag_weight_利用してcomment_weightを設定
                comment_weight_.Add(1);
            }

        }

        if (comment_flag_.Contains(CommentFlag.WeakPopular))
        {
          
        }

        if (comment_flag_.Contains(CommentFlag.WeakHealth))
        {

        }

        if (comment_flag_.Contains(CommentFlag.WeakEconomic))
        {

        }

        if (comment_flag_.Contains(CommentFlag.WeakWork))
        {

        }
    }
}
