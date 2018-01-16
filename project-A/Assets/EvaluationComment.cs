//このファイルはEvaluationの分割ファイルであり評価の際のコメントを呼び出す関数である

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Evaluation : MonoBehaviour
{

	partial void Comment(int comment_ID)
    {
        switch (comment_ID)
        {
            case 0:
                comment_ += "木の気が多すぎます\n";
                break;
            case 1:
                comment_ += "火の気が多すぎます\n";
                break;
            case 2:
                comment_ += "土の気が多すぎます\n";
                break;
            case 3:
                comment_ += "金の気が多すぎます\n";
                break;
            case 4:
                comment_ += "水の気が多すぎます\n";
                break;
            case 5:
                comment_ += "木の気が多すぎます\n";
                break;
            case 6:
                comment_ += "火の気が多すぎます\n";
                break;
            case 7:
                comment_ += "土の気が多すぎます\n";
                break;
            case 8:
                comment_ += "金の気が多すぎます\n";
                break;
            case 9:
                comment_ += "水の気が多すぎます\n";
                break;
            default:
                break;
        }
    }
}
