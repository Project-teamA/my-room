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
