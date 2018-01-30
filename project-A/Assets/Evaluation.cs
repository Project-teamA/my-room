//風水評価クラス(多分furnitureManagementと同じように空のオブジェクトに放り込んで実装),動くかどうかのテストは行っていない
//
//使い方
//①先ずInitで初期化(この初期化関数は最初に1回だけ実行すること)
//②次にUpdateGrid,またはDeleteGridで家具グリッドの更新
//③その後EvaluationTotalで運勢評価
//②に戻る
//
//
//更新点
//2018年1月21日
//五行と運気に関連する変数名を大きく変更(具体的には配列になった, forによるループ処理のため)
//基本的には
//[0] = 木, [1] = 火, [2] = 土, [3] = 金, [4] = 水
//[0] = 仕事運, [1] = 人気運, [2] = 健康運, [3] = 金運, [4] = 恋愛運
//運気を旺気成分(プラスの運気)と邪気成分(マイナスの運気)に分離(最終的に足し合わせる)
//五行と陰陽を部屋の中の八方位に分割(最終的に足し合わせる)
//
//ノルマ変数を追加
//ノルマ入力関数を追加
//
//
//とりあえずコメントはこのようにして出力させますという方針をプログラム内に記述(1月28日更新)
//コメント出力フラグのための構造体作成
//要素
//comment_identifier_ = コメント識別子，flag_weight_ = コメントの重要度，luck_identifier_ = 影響を受けた運気の識別子
//FortuneItem関数改装案，いままでは引数に家具の番号を取っていたが，それを消して，関数内でベッドなり他の家具なり探索することにしたい(評価の実装のためにその方が都合よい)
//ソースファイル内に似た操作の部分が結構あるので似た操作部分を関数化するか検討中

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public partial class Evaluation : MonoBehaviour
{
    public enum Room { Entrance, Living, Kitchen, Workroom, Bedroom, Bathroom, Toilet };
    public enum Direction { North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest };

    //コメントの識別子(列挙型)
    //基本的にボーナス点，ペナルティ点が呼び出されたら必ずコメント識別子が呼び出される．
    public enum CommentIdentifier
    {
        WoodSosho, FireSosho, EarthSosho, MetalSosho, WaterSosho, //相生効果により上がった気
        WoodSokoku, FireSokoku, EarthSokoku, MetalSokoku, WaterSokoku, //相克効果により下がった気
        OverYin, OverYang, //陰陽の強すぎ
        WeakWork, WeakPopular, WeakHealth, WeakEconomic, WeakLove, WeakAllLuck, //運気が低い(ノルマ未達成)

        EntranceYinNorthEast, EntranceYinSouthWest, EntranceNoCarpet, EntranceSmell, EntranceMulti, //玄関関連
        BedroomLuckyDirection, BedroomNorthEast, BedroomBeauty, BedroomWood, BedroomBlue, BedroomMulti, //寝室関連
        LivingMulti, //リビング関連
        KitchenDamage, KitchenAiry, KitchenNorthWest, //キッチン関連
        WorkroomNorthWest, WorkroomMulti, //仕事部屋関連
        BathroomAiry, BathroomYin, //風呂関連
        //便所関連

        WeakNorth, WeakNorthEast, MinusNorthEast, WeakEast, WeakSouthEast, WeakSouth, WeakSouthWest, WeakWest, WeakNorthWest, //方角基本性能
        NorthCold, NorthPink, //北の運勢
        NorthEastHigh, //北東の運勢
        EastWindSound, //東の運勢
        SouthEastWindSound, SouthEastOrange, //南東の運勢
        SouthPurification, //南の運勢
        SouthWestLow, //南西の運勢
        WestWestern, WestLuxury, //西の運勢
        NorthWestLuxury, NorthWestLuxuryZero, NorthWestSilver, NorthWestGray, NorthWestVain, //北西の運勢

        WhiteResetYinYang, WhitePurification, BlackStrengthening, RedOne, PinkOne, PinkNoOrange, BlueOne, BlueNoOrange, OrangeNoPink, OrangeNoBlue,
        YellowBrownOne, GreenPurification, BeigeIneger, Cream, GoldMulti,  //色関連
        //素材関連
       //模様関連
        SquareFix, CircleGoodRelation, CircleCirculation, SharpBadRelation, //形状関連
        SweetSmell, Luminescence, FlowerAssociative, //その他特性
        ExcessFurniture,  ShortageFurniture,  //家具の数関連

        WeakWood, WeakFire, WeakEarth, WeakMetal, WeakWater, WeakEnergy, //気が弱い
        OverWood, OverFire, OverEarth, OverMetal, OverWater, //気が強すぎ
    };

    //elements_の各要素について
    //[0] = 木，[1] = 火, [2] = 土，[3] = 金, [4] = 水
    private int[] elements_ = new int[5] { 0, 0, 0, 0, 0 };
    private int yin_yang_ = 0; //陰陽(プラスで陽，マイナスで陰)

    private int energy_strength_ = 0; //気の強さ(max1000 min0, ここは確定するように調整)

    //部屋の中の方位ごとの五行
    //[0] = 木, [1] = 火, [2] = 土, [3] = 金, [4] = 水
    // [][0] = 北，[][1] = 北東, [][2] = 東，[][3] = 南東, [][4] = 南, [][5] = 南西, [][6] = 西，[][7] = 北西
    private int[][] split_elements_ = new int[5][];

    //部屋の中の方位ごとの陰陽
    private int[] split_yin_yang_ = new int[8];

    //相生効果によって変化した気の量
    //[0] = 木，[1] = 火, [2] = 土, [3] = 金, [4] = 水
    private int[] sosho_stock_ = new int[5];

    //相克効果によって変化した気の量
    //[0] = 木，[1] = 火, [2] = 土, [3] = 金, [4] = 水
    private int[] sokoku_stock_ = new int[5];

    //---------------------------------------------------------------------------------------------------------------------------------

    //運気(旺気と邪気を合わせた最終結果)
    //[0] = 仕事運，[1] = 人気運, [2] = 健康運, [3] = 金運, [4] = 恋愛運
    private int[] luck_ = new int[5] { 0, 0, 0, 0, 0 };

    private int all_luck_ = 0; //総合運

    //(運気の)ノルマ変数
    //[0] = 仕事運，[1] = 人気運, [2] = 健康運, [3] = 金運, [4] = 恋愛運
    private int[] norma_luck_ = new int[5];

    private int all_norma_ = 0; //総合運のノルマ

    //運気の変化(プラスの運気成分(旺気))
    //[0] = 仕事運，[1] = 人気運, [2] = 健康運, [3] = 金運, [4] = 恋愛運
    private int[] plus_luck_ = new int[5];

    //運気の変化(マイナスの運気成分(邪気))
    //[0] = 仕事運，[1] = 人気運, [2] = 健康運, [3] = 金運, [4] = 恋愛運
    private int[] minus_luck_ = new int[5];

    private Room room_role_; //部屋の種類
    private Direction room_direction_; //部屋の方角

    private List<FurnitureGrid> furniture_grid_ = new List<FurnitureGrid>(); //FurnitureGrid.csで実装されているクラスのリスト(最大50)

    //コメント選択用構造体(あとで優先順位準にソートする)
    private struct CommentFlag
    {
        public CommentIdentifier comment_identifier_; //コメント識別子
        public int flag_weight_; //コメントの重要度(数値がおおきいほど重要度が高い)
        private int luck_identifier_;  //影響を受けた運気の識別子 [0] = 仕事運，[1] = 人気運, [2] = 健康運, [3] = 金運, [4] = 恋愛運

        public CommentFlag(CommentIdentifier comment_identifier, int flag_weight, int luck_identifier)
        {
            comment_identifier_ = comment_identifier;
            flag_weight_ = flag_weight;
            luck_identifier_ = luck_identifier;
        }
    }

    private List<CommentFlag> comment_flag_ = new List<CommentFlag>();
    private List<string> comment_ = new List<string>(); //コメント ( コメントフラグに応じていくつかのコメントを出力 )

    private bool is_finished_game_; //ゲームが終わったかどうかのフラグ (ture = ゲーム終了 false = ゲーム終了せず)

    //*******************************************************************************************************************************************************************************************

    //五行木取得用
    public int elements_wood()
    {
        return elements_[0];
    }

    //五行火取得用
    public int elements_fire()
    {
        return elements_[1];
    }

    //五行土取得用
    public int elements_earth()
    {
        return elements_[2];
    }

    //五行金取得用
    public int elements_metal()
    {
        return elements_[3];
    }

    //五行水取得用
    public int elements_water()
    {
        return elements_[4];
    }

    //気の強さ取得用
    public int energy_strength()
    {
        return energy_strength_;
    }




    //仕事運(取得用)
    public int work_luck()
    {
        return luck_[0];
    }

    //人気運(取得用)
    public int popular_luck()
    {
        return luck_[1];
    }

    //健康運(取得用)
    public int health_luck()
    {
        return luck_[2];
    }

    //金運(取得用)
    public int economic_luck()
    {
        return luck_[3];
    }

    //恋愛運(取得用)
    public int love_luck()
    {
        return luck_[4];
    }

    //総合運(取得用)
    public int all_luck()
    {
        return all_luck_;
    }



    //仕事運(取得用)
    public int work_norma()
    {
        return norma_luck_[0];
    }

    //人気運(取得用)
    public int popular_norma()
    {
        return norma_luck_[1];
    }

    //健康運ノルマ(取得用)
    public int health_norma()
    {
        return norma_luck_[2];
    }

    //金運(取得用)
    public int economic_norma()
    {
        return norma_luck_[4];
    }

    //恋愛運(取得用)
    public int love_norma()
    {
        return norma_luck_[5];
    }



    //総合運(取得用)
    public int all_norma()
    {
        return all_norma_;
    }



    //コメント(取得用)
    public List<string> comment()
    {
        return comment_;
    }

    //*******************************************************************************************************************************************************************************************

    //初期化関数(この関数は最初に1回だけ実行すること)
    // 
    // room_role_ = 部屋の種類
    // room_direction_ = 部屋の方角
    // furniture_grid_ = 家具の更新
    public void Init(Room room_role, Direction room_direction, List<FurnitureGrid> furniture_grid)
    {
        room_role_ = room_role;
        room_direction_ = room_direction;
        furniture_grid_ = furniture_grid;
        is_finished_game_ = false;

        for (int i = 0; i < 5; ++i)
        {
            split_elements_[i] = new int[8];
        }
    }

    //*******************************************************************************************************************************************************************************************

    //ノルマセット関数
    //
    //引数
    //work_norma = 仕事運ノルマ, popular_norma = 人気運ノルマ, health_norma = 健康運ノルマ, economic_norma = 金運ノルマ, love_norma = 人気運ノルマ
    public void SetNorma(int work_norma, int popular_norma, int health_norma, int economic_norma, int love_norma, int all_norma)
    {
        norma_luck_[0] = work_norma;
        norma_luck_[1] = popular_norma;
        norma_luck_[2] = health_norma;
        norma_luck_[3] = economic_norma;
        norma_luck_[4] = love_norma;
        all_norma_ = all_norma;
    }

    //*******************************************************************************************************************************************************************************************

    //家具の更新関数(変更する可能性がかなり高い)
    //
    //furniture_grid = 変更，更新する家具グリッド
    public void UpdateGrid(FurnitureGrid furniture_grid)
    {
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            if (furniture_grid_[i].furniture_grid() == furniture_grid.furniture_grid())
            {
                furniture_grid_[i] = furniture_grid;
                break;
            }
            furniture_grid_.Add(furniture_grid);
        }
    }

    //*******************************************************************************************************************************************************************************************

    //家具の削除関数(変更する可能性がかなり高い)
    //
    //delete_number = 削除する家具グリッドの添字ナンバー
    public void DeleteGrid(int delete_number)
    {
        if (-1 < delete_number && furniture_grid_.Count > delete_number)
        {
            furniture_grid_.RemoveAt(delete_number);
        }
    }

    //*******************************************************************************************************************************************************************************************

    //is_finised_gameに値をセット
    public void SetIsFinishedGame(bool is_finished_game)
    {
        is_finished_game_ = is_finished_game;
    }

    //*******************************************************************************************************************************************************************************************

    //総合評価関数(評価の一連の流れ)
    //
    //
    public void EvaluationTotal()
    {
        for (int i = 0; i < 5; ++i)
        {
            elements_[i] = 0;
            sosho_stock_[i] = 0;
            sokoku_stock_[i] = 0;
            luck_[i] = 0;
            plus_luck_[i] = 0;
            minus_luck_[i] = 0;

            for (int j = 0; j < 8; ++j)
            {
                split_elements_[i][j] = 0;
            }

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

        //for (int i = 0; i < furniture_grid_.Count; ++i)
        //{
        //    FortuneItem(furniture_grid_[i]); //アイテムによる運勢補正
        //}
        FortuneItem(); //アイテムによる運勢補正(要素は関数の中で検索した方がコメントを付けるときやりやすい)

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
        Comment();
    }

    //*******************************************************************************************************************************************************************************************

    //アイテム評価関数(アイテムごとの五行と陰陽を加算)
    private void EvaluationItem()
    {
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.North)
            {
                split_elements_[0][i] += furniture_grid_[i].elements_wood();
                split_elements_[1][i] += furniture_grid_[i].elements_fire();
                split_elements_[2][i] += furniture_grid_[i].elements_earth();
                split_elements_[3][i] += furniture_grid_[i].elements_metal();
                split_elements_[4][i] += furniture_grid_[i].elements_water();
                split_yin_yang_[i] += furniture_grid_[i].yin_yang();
            }
            else if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.NorthEast)
            {
                split_elements_[0][i] += furniture_grid_[i].elements_wood();
                split_elements_[1][i] += furniture_grid_[i].elements_fire();
                split_elements_[2][i] += furniture_grid_[i].elements_earth();
                split_elements_[3][i] += furniture_grid_[i].elements_metal();
                split_elements_[4][i] += furniture_grid_[i].elements_water();
                split_yin_yang_[i] += furniture_grid_[i].yin_yang();
            }
            else if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.East)
            {
                split_elements_[0][i] += furniture_grid_[i].elements_wood();
                split_elements_[1][i] += furniture_grid_[i].elements_fire();
                split_elements_[2][i] += furniture_grid_[i].elements_earth();
                split_elements_[3][i] += furniture_grid_[i].elements_metal();
                split_elements_[4][i] += furniture_grid_[i].elements_water();
                split_yin_yang_[i] += furniture_grid_[i].yin_yang();
            }
            else if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.SouthEast)
            {
                split_elements_[0][i] += furniture_grid_[i].elements_wood();
                split_elements_[1][i] += furniture_grid_[i].elements_fire();
                split_elements_[2][i] += furniture_grid_[i].elements_earth();
                split_elements_[3][i] += furniture_grid_[i].elements_metal();
                split_elements_[4][i] += furniture_grid_[i].elements_water();
                split_yin_yang_[i] += furniture_grid_[i].yin_yang();
            }
            else if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.South)
            {
                split_elements_[0][i] += furniture_grid_[i].elements_wood();
                split_elements_[1][i] += furniture_grid_[i].elements_fire();
                split_elements_[2][i] += furniture_grid_[i].elements_earth();
                split_elements_[3][i] += furniture_grid_[i].elements_metal();
                split_elements_[4][i] += furniture_grid_[i].elements_water();
                split_yin_yang_[i] += furniture_grid_[i].yin_yang();
            }
            else if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.SouthWest)
            {
                split_elements_[0][i] += furniture_grid_[i].elements_wood();
                split_elements_[1][i] += furniture_grid_[i].elements_fire();
                split_elements_[2][i] += furniture_grid_[i].elements_earth();
                split_elements_[3][i] += furniture_grid_[i].elements_metal();
                split_elements_[4][i] += furniture_grid_[i].elements_water();
                split_yin_yang_[i] += furniture_grid_[i].yin_yang();
            }
            else if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.West)
            {
                split_elements_[0][i] += furniture_grid_[i].elements_wood();
                split_elements_[1][i] += furniture_grid_[i].elements_fire();
                split_elements_[2][i] += furniture_grid_[i].elements_earth();
                split_elements_[3][i] += furniture_grid_[i].elements_metal();
                split_elements_[4][i] += furniture_grid_[i].elements_water();
                split_yin_yang_[i] += furniture_grid_[i].yin_yang();
            }
            else if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.NorthWest)
            {
                split_elements_[0][i] += furniture_grid_[i].elements_wood();
                split_elements_[1][i] += furniture_grid_[i].elements_fire();
                split_elements_[2][i] += furniture_grid_[i].elements_earth();
                split_elements_[3][i] += furniture_grid_[i].elements_metal();
                split_elements_[4][i] += furniture_grid_[i].elements_water();
                split_yin_yang_[i] += furniture_grid_[i].yin_yang();
            }
        }
    }

    //*****************************************************************************************************************************************************************************************

    //部屋評価関数(基本五行と陰陽のみ)
    //
    //
    private void EvaluationRoom()
    {
        if (room_role_ == Room.Entrance)
        {
            //良い運気取り込む部屋作りを(重要)
        }
        else if (room_role_ == Room.Bedroom)
        {
            //良い運気取り込む部屋作りを
        }
        else if (room_role_ == Room.Living)
        {
            //土の気をもつ
            for (int i = 0; i < 8; ++i)
            {
                split_elements_[2][i] += 8;
            }
            //良い運気取り込む部屋作りを
        }
        else if (room_role_ == Room.Kitchen)
        {
            //火と水の気，弱い金の気をもつ
            for (int i = 0; i < 8; ++i)
            {
                split_elements_[1][i] += 16;
                split_elements_[3][i] += 4;
                split_elements_[4][i] += 16;
            }
            //悪い運気を払う部屋作りを
        }
        else if (room_role_ == Room.Workroom)
        {
            //木の気をもつ
            for (int i = 0; i < 8; ++i)
            {
                split_elements_[0][i] += 8;
            }
            //良い運気取り込む部屋作りを
        }
        else if (room_role_ == Room.Bathroom)
        {
            //強い水の気をもつ
            for (int i = 0; i < 8; ++i)
            {
                split_elements_[4][i] += 16;
                split_yin_yang_[i] += 12;
            }
            //悪い運気を払う部屋作りを
        }
        else if (room_role_ == Room.Toilet)
        {
            //強い水の気をもつ
            for (int i = 0; i < 8; ++i)
            {
                split_elements_[4][i] += 16;
                split_yin_yang_[i] += 16;
            }
            //悪い運気を払う部屋作りを(重要)
        }
        else
        {
            Debug.Log("そのような部屋は存在しない");
        }
    }

    //**************************************************************************************************************************************************************************************************

    //方位評価関数(部屋の)
    //
    //内容はこの方位がもともと持っている気を倍加
    private void EvaluationDirection()
    {
        if (room_direction_ == Direction.North)
        {
            //北は水の気が強い
            for (int i = 0; i < 8; ++i)
            {
                split_elements_[4][i] += 20;
            }
        }
        else if (room_direction_ == Direction.NorthEast)
        {
            //北東は土の気が強い(山)
            for (int i = 0; i < 8; ++i)
            {
                split_elements_[2][i] += 20;
            }
        }
        else if (room_direction_ == Direction.East)
        {
            //東は木の気が強い(若木)
            for (int i = 0; i < 8; ++i)
            {
                split_elements_[0][i] += 20;
            }
        }
        else if (room_direction_ == Direction.SouthEast)
        {
            //南東は木の気が強い(大木)
            for (int i = 0; i < 8; ++i)
            {
                split_elements_[0][i] += 20;
            }
        }
        else if (room_direction_ == Direction.South)
        {
            //南は火の気が強い
            for (int i = 0; i < 8; ++i)
            {
                split_elements_[1][i] += 20;
            }
        }
        else if (room_direction_ == Direction.SouthWest)
        {
            //南西は土の気が強い
            for (int i = 0; i < 8; ++i)
            {
                split_elements_[2][i] += 20;
            }
        }
        else if (room_direction_ == Direction.West)
        {
            //西は金の気が強い
            for (int i = 0; i < 8; ++i)
            {
                split_elements_[3][i] += 20;
            }
        }
        else if (room_direction_ == Direction.NorthWest)
        {
            //北西は金の気が強い
            for (int i = 0; i < 8; ++i)
            {
                split_elements_[3][i] += 20;
            }
        }
        else
        {
            Debug.Log("そのような方位は存在しない");
        }
    }

    //**************************************************************************************************************************************************************************************************


    //五行評価関数(内部的に陰陽も変化)
    private void EvaluationFiveElements()
    {
        //相生の処理
        for (int j = 0; j < 8; ++j)
        {
            int[] sosho = new int[5] { 0, 0, 0, 0, 0 };

            for (int i = 0; i < 5; ++i)
            {
                if (split_elements_[i][j] / 2 <= split_elements_[(i + 1) % 5][j])
                {
                    sosho[(i + 1) % 5] += split_elements_[i][j] / 2;
                    sosho[i] -= split_elements_[i][j] / 4;
                }
                else
                {
                    sosho[(i + 1) % 5] += split_elements_[(i + 1) % 5][j];
                    sosho[i] -= split_elements_[(i + 1) % 5][j] / 2;
                }
            }

            for (int i = 0; i < 5; ++i)
            {
                //相生した量をストック(コメントのウェイトに利用)
                sosho_stock_[i] += sosho[i];

                //相生でマイナスになるようなことはまずない.のでマイナスの処理はしなくてよい
                split_elements_[i][j] += sosho[i];
            }
        }


        //相克の処理
        for (int j = 0; j < 8; ++j)
        {
            int[] sokoku = new int[5] { 0, 0, 0, 0, 0 };

            for (int i = 0; i < 5; ++i)
            {
                if (split_elements_[i][j] / 2 <= split_elements_[(i + 2) % 5][j])
                {
                    sokoku[(i + 2) % 5] += split_elements_[i][j] / 2;
                    sokoku[i] += split_elements_[i][j] / 4;
                }
                else
                {
                    sokoku[(i + 2) % 5] += split_elements_[(i + 2) % 5][j];
                    sokoku[i] += split_elements_[(i + 2) % 5][j] / 2;
                }
            }

            for (int i = 0; i < 5; ++i)
            {
                //相克した量をストック(コメントのウェイトに利用)
                sokoku_stock_[i] += sokoku[i];

                split_elements_[i][j] += sokoku[i];
                if (split_elements_[i][j] < 0)
                {
                    split_elements_[i][j] = 0;
                }
            }
        }

        //コメント用ここまで----------------------------------------------------------------

        for (int j = 0; j < 8; ++j)
        {
            //五行による陰陽補正
            split_yin_yang_[j] += split_elements_[1][j];
            split_yin_yang_[j] -= split_elements_[2][j];
            split_yin_yang_[j] += split_elements_[3][j];
            split_yin_yang_[j] -= split_elements_[4][j];
        }
    }

    //**************************************************************************************************************************************************************************************************

    //仕上げの五行陰陽補正(観葉植物による陰陽中和など)
    //
    //ここでは五行陰陽に対し加算ではなく乗算による補正が行われる
    private void EvaluationeLast()
    {
        int[] displacement_elements_stock = new int[5] { 0, 0, 0, 0, 0 };
        int displacement_yin_yang_stock = 0; //陰陽の気変化量

        //最後に元の五行陰陽に補正を加算
        for (int i = 0; i < 0; ++i)
        {
            elements_[i] = displacement_elements_stock[i];
        }
        yin_yang_ += displacement_yin_yang_stock;
    }

    //**************************************************************************************************************************************************************************************************

    //アイテムによる運勢補正
    private void FortuneItem(FurnitureGrid furniture_grid)
    {
        if (furniture_grid.furniture_type() == FurnitureGrid.FurnitureType.Bed)
        {
            //ベッドの枕の位置(安眠できるかどうかで，健康運，美容運にかかわる)
            if (furniture_grid.up_direction() == Vector3.up)
            {
                //ベッドが北枕(1番良い, 安眠できる)
                plus_luck_[2] += 30;
            }
            else if (furniture_grid.up_direction() == Vector3.down)
            {
                //ベッドが南枕(1番ダメ, 安眠出来ない)
                minus_luck_[2] += 30;
            }
            else if (furniture_grid.up_direction() == Vector3.right)
            {
                //ベッドが東枕(2番目に良い)
                plus_luck_[2] += 20;
            }
            else if (furniture_grid.up_direction() == Vector3.left)
            {
                //ベッドが西枕(2番目に良くない)
                minus_luck_[2] -= 20;
            }

            //シングルベッドをつなげるとダメ
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                if (furniture_grid_[i].furniture_grid().GetComponent<BoxCollider>().bounds.Contains
                    (furniture_grid_[i].grid_position() + (furniture_grid_[i].parameta(0) + 1) * 0.2F * furniture_grid_[i].right_direction()) == true)
                {

                }

                if (furniture_grid_[i].furniture_grid().GetComponent<BoxCollider>().bounds.Contains
                    (furniture_grid_[i].grid_position() - (furniture_grid_[i].parameta(0) + 1) * 0.2F * furniture_grid_[i].right_direction()) == true)
                {

                }

            }

            //ベッドと壁の隙間ダメ


            //ドアベクトルがベッドの頭指すだめ(ドア側で実装)


            //鏡写しダメ(鏡側で実装)


            //窓の近くにベッドダメ(1つでもあったら)
            GameObject surrounding = furniture_grid.furniture_grid();
            surrounding.transform.localScale = new Vector3(3F, 3F, 3F);
            bool window_near_flag = false;
            for (int i = 0; i < surrounding.transform.childCount; ++i)
            {
                surrounding.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
                for (int j = 0; j < furniture_grid_.Count; ++j)
                {
                    if (furniture_grid_[j].tag == "furniture_grid_window")
                    {
                        for (int k = 0; k < furniture_grid_[j].transform.childCount; ++k)
                        {
                            if (surrounding.GetComponents<BoxCollider>()[i].bounds.Intersects(furniture_grid_[j].GetComponents<BoxCollider>()[k].bounds))
                            {
                                window_near_flag = true;
                                break;
                            }

                        } //k
                    }
                } //j
            } //i

            if (window_near_flag == true)
            {

            }


            //角がベッドの顔に向いたらだめ
        }
        else if (furniture_grid.furniture_type() == FurnitureGrid.FurnitureType.Desk)
        {

        }
        else if (furniture_grid.furniture_type() == FurnitureGrid.FurnitureType.Sofa)
        {

        }
        else if (furniture_grid.furniture_type() == FurnitureGrid.FurnitureType.FoliagePlant)
        {

        }
        else if (furniture_grid.furniture_type() == FurnitureGrid.FurnitureType.ArtificialFlower)
        {

        }
        else if (furniture_grid.furniture_type() == FurnitureGrid.FurnitureType.WaterTank)
        {

        }
        else if (furniture_grid.furniture_type() == FurnitureGrid.FurnitureType.Carpet)
        {

        }
        else if (furniture_grid.furniture_type() == FurnitureGrid.FurnitureType.Curtain)
        {

        }
        else if (furniture_grid.furniture_type() == FurnitureGrid.FurnitureType.ConsumerElectronics)
        {

        }
        else if (furniture_grid.furniture_type() == FurnitureGrid.FurnitureType.Dresser)
        {

        }
        else if (furniture_grid.furniture_type() == FurnitureGrid.FurnitureType.Illumination)
        {

        }
        else if (furniture_grid.furniture_type() == FurnitureGrid.FurnitureType.DeskLamp)
        {

        }
        else if (furniture_grid.furniture_type() == FurnitureGrid.FurnitureType.Chair)
        {

        }
        else if (furniture_grid.furniture_type() == FurnitureGrid.FurnitureType.PictureFrame)
        {

        }
        else if (furniture_grid.furniture_type() == FurnitureGrid.FurnitureType.PlushDoll)
        {

        }
        else if (furniture_grid.furniture_type() == FurnitureGrid.FurnitureType.Window)
        {

        }
        else if (furniture_grid.furniture_type() == FurnitureGrid.FurnitureType.Door)
        {

        }
        else if (furniture_grid.furniture_type() == FurnitureGrid.FurnitureType.Bureau)
        {

        }
        else
        {
            Debug.Log("ありえん");
        }
    }

    //*************************************************************************************************************************************************************************************************

    //アイテムによる運勢補正
    private void FortuneItem()
    {
        int[] color_luck_plus_stock = new int[5] { 0, 0, 0, 0, 0 };
        int[] color_luck_minus_stock = new int[5] { 0, 0, 0, 0, 0 };

        //赤い家具1つでも置くと仕事，人気，健康，恋愛アップ
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            if (Array.IndexOf(furniture_grid_[i].color_name(), FurnitureGrid.ColorName.Red) >= 0)
            {
                color_luck_plus_stock [0] += 10;
                color_luck_plus_stock[1] += 30;
                color_luck_plus_stock[2] += 10;
                color_luck_plus_stock[4] += 10;
                break;
            }

            if (i == furniture_grid_.Count)
            {
                //赤い家具一つもないので一つ置きましょう
                comment_flag_.Add(new CommentFlag(CommentIdentifier.RedOne, 10, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.RedOne, 20, 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.RedOne, 10, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.RedOne, 10, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.RedOne, 50, 5));
            }
        }

        //ピンクの家具1つでも置くと恋愛アップ
        bool pink_at_least = false; 
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            if (Array.IndexOf(furniture_grid_[i].color_name(), FurnitureGrid.ColorName.Pink) >= 0)
            {
                color_luck_plus_stock[4] += 50;
                pink_at_least = true;
                break;
            }

            if (i == furniture_grid_.Count)
            {
                //ピンク家具一つもないので一つ置きましょう
                comment_flag_.Add(new CommentFlag(CommentIdentifier.PinkOne, 50, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.PinkOne, 50, 5));
            }
        }

        //青の家具1つでも置くと仕事運アップ
        bool blue_at_least = false;
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            if (Array.IndexOf(furniture_grid_[i].color_name(), FurnitureGrid.ColorName.Blue) >= 0)
            {
                color_luck_plus_stock[0] += 30;
                blue_at_least = true;
                break;
            }

            if (i == furniture_grid_.Count)
            {
                //青家具一つもないので一つ置きましょう
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BlueOne, 30, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BlueOne, 30, 5));
            }
        }

        //オレンジの家具一つでも置くと…
        bool orange_at_least = false;
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            if (Array.IndexOf(furniture_grid_[i].color_name(), FurnitureGrid.ColorName.Blue) >= 0)
            {
                orange_at_least = true;
                break;
            }
        }

        if(orange_at_least)
        {
            if(pink_at_least)
            {
                //オレンジとピンクで恋愛運アップ
                color_luck_plus_stock[4] += 30;
            }
            else
            {
                //オレンジがあるのでピンクと合わせましょう
                comment_flag_.Add(new CommentFlag(CommentIdentifier.OrangeNoPink, 30, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.OrangeNoPink, 30, 5));
            }

            if(blue_at_least)
            {
                //オレンジと黄色で健康運アップ
                color_luck_plus_stock[0] += 30;
            }
            else
            {
                //オレンジがあるので青と合わせましょう
                comment_flag_.Add(new CommentFlag(CommentIdentifier.OrangeNoBlue, 30, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.OrangeNoBlue, 30, 5));
            }
        }
        else if(pink_at_least)
        {
            //ピンクがあるのでオレンジと合わせましょう
            comment_flag_.Add(new CommentFlag(CommentIdentifier.PinkNoOrange, 30, 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.PinkNoOrange, 30, 5));
        }
        else if(blue_at_least)
        {
            //青があるのでオレンジと合わせましょう
            comment_flag_.Add(new CommentFlag(CommentIdentifier.BlueNoOrange, 30, 0));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.BlueNoOrange, 30, 5));
        }

        //ベージュの家具1つにつき仕事運，恋愛運アップ
        int beige_item = 0;
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            if (Array.IndexOf(furniture_grid_[i].color_name(), FurnitureGrid.ColorName.Beige) >= 0)
            {
                ++beige_item;
            }
        }
        color_luck_plus_stock[0] += beige_item * 10;
        color_luck_plus_stock[4] += beige_item * 10;

        if (beige_item < 3)
        {
            //ベージュの家具をさらに置けば運気アップにつながる
            comment_flag_.Add(new CommentFlag(CommentIdentifier.BeigeIneger, 30 - beige_item * 10, 0));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.BeigeIneger, 30 - beige_item * 10, 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.BeigeIneger, 60 - beige_item * 20, 5));
        }

        //黒が1つでもあればカラーによる運気補正がさらに高まる(実装がかなり面倒)

        for(int i = 0; i< 5; ++i )
        {
            plus_luck_[i] += color_luck_plus_stock[i];
            minus_luck_[i] += color_luck_minus_stock[i];
        }
    }

    //*************************************************************************************************************************************************************************************************

    //部屋による運勢補正
    private void FortuneRoom()
    {
        if (room_role_ == Room.Entrance)
        {
            if (room_direction_ == Direction.NorthEast)
            {
                if (yin_yang_ < -30)
                {
                    //鬼門(北東)の場合，陰気が強いと運気ダウン
                    for (int i = 0; i < 5; ++i)
                    {
                        minus_luck_[i] += 40;
                        comment_flag_.Add(new CommentFlag(CommentIdentifier.EntranceYinNorthEast, 40, i));
                    }
                    comment_flag_.Add(new CommentFlag(CommentIdentifier.EntranceYinNorthEast, 200, 5));
                }
            }
            else if (room_direction_ == Direction.SouthWest)
            {
                if (yin_yang_ < -30)
                {
                    //裏鬼門(北東)の場合，陰気が強いと運気ダウン
                    for (int i = 0; i < 5; ++i)
                    {
                        minus_luck_[i] += 40;
                        comment_flag_.Add(new CommentFlag(CommentIdentifier.EntranceYinSouthWest, 40, i));
                    }
                    comment_flag_.Add(new CommentFlag(CommentIdentifier.EntranceYinSouthWest, 200, 5));
                }
            }

            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.Carpet)
                {
                    break;
                }

                if (i == furniture_grid_.Count - 1)
                {
                    //玄関にカーペットなしは運気ダウン
                    for (int j = 0; j < 5; ++j)
                    {
                        minus_luck_[j] += 50;
                        comment_flag_.Add(new CommentFlag(CommentIdentifier.EntranceNoCarpet, 50, j));
                    }
                    comment_flag_.Add(new CommentFlag(CommentIdentifier.EntranceNoCarpet, 250, 5));
                }
            }

            //良い香りのするものを一つでも玄関に置くと運気アップ
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                if (Array.IndexOf(furniture_grid_[i].characteristic(), FurnitureGrid.Characteristic.Smell) >= 0)
                {
                    for (int j = 0; j < 5; ++j)
                    {
                        plus_luck_[j] += 20;
                    }
                    break;
                }

                if (i == furniture_grid_.Count)
                {
                    for (int j = 0; j < 5; ++j)
                    {
                        comment_flag_.Add(new CommentFlag(CommentIdentifier.EntranceSmell, 20, j));
                    }
                    comment_flag_.Add(new CommentFlag(CommentIdentifier.EntranceSmell, 100, 5));
                }
            }

        }
        else if (room_role_ == Room.Bedroom)
        {
            //吉方位で運気アップ
            if ((room_direction_ == Direction.North || room_direction_ == Direction.East) || room_direction_ == Direction.SouthWest)
            {
                plus_luck_[0] += 5;
                plus_luck_[1] += 5;
                plus_luck_[2] += 10;
                plus_luck_[3] += 5;
                plus_luck_[4] += 10;
            }
            else
            {
                //寝室を吉方位にすれば運気上がる(方向は定まっているので要らないコメント？)
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomLuckyDirection, 5, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomLuckyDirection, 5, 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomLuckyDirection, 10, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomLuckyDirection, 5, 3));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomLuckyDirection, 10, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomLuckyDirection, 35, 5));
            }

            //北東の寝室は金運アップにつながる
            if (room_direction_ == Direction.NorthEast)
            {
                plus_luck_[3] += 20;
            }
            else
            {
                //寝室を北東にすれば運気上がる(方向は定まっているので要らないコメント？)
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomNorthEast, 20, 3));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomNorthEast, 20, 5));
            }

            //東，南東，西，北西の寝室は美容運アップ(人気運，健康運，恋愛運アップ)につながる
            if (((room_direction_ == Direction.East || room_direction_ == Direction.SouthEast)
                || room_direction_ == Direction.West)
                || room_direction_ == Direction.NorthWest)
            {
                plus_luck_[1] += 10;
                plus_luck_[2] += 5;
                plus_luck_[4] += 5;
            }
            else
            {
                //寝室を東，南東，西，北西にすれば美容運アップ(人気運，健康運，恋愛運アップ)(方向は定まって…)
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomBeauty, 10, 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomBeauty, 5, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomBeauty, 5, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomBeauty, 20, 5));
            }

            //木材家具1個につき健康運アップ
            int wooden_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                if (Array.IndexOf(furniture_grid_[i].material_type(), FurnitureGrid.MaterialType.Wooden) >= 0)
                {
                    ++wooden_item;
                }
            }
            plus_luck_[2] += wooden_item * 10;

            if (wooden_item < 3)
            {
                //木材家具さらに置けば健康運アップにつながる
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomWood, 30 - wooden_item * 10, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomWood, 30 - wooden_item * 10, 5));
            }

            //青い家具一個につき安眠で健康運アップ
            int blue_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                if (Array.IndexOf(furniture_grid_[i].color_name(), FurnitureGrid.ColorName.Blue) >= 0)
                {
                    ++blue_item;
                }
            }
            plus_luck_[2] += blue_item * 10;

            if (blue_item < 3)
            {
                //青い家具さらに置けば健康運アップにつながる
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomBlue, 30 - blue_item * 10, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomBlue, 30 - blue_item * 10, 5));
            }

        }
        else if (room_role_ == Room.Living)
        {

        }
        else if (room_role_ == Room.Kitchen)
        {
            //西，南西のキッチンは食べ物が傷みやすく健康運を損なう
            if (room_direction_ == Direction.SouthWest || room_direction_ == Direction.West)
            {
                minus_luck_[2] += 30;
            }
            else
            {
                //西，南西のキッチンは健康運に悪影響があるので健康運を上げるように心がける
                comment_flag_.Add(new CommentFlag(CommentIdentifier.KitchenDamage, 30, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.KitchenDamage, 30, 5));
            }

            //東，南東，北西のキッチンは風通しよく健康運上がる
            if ((room_direction_ == Direction.East || room_direction_ == Direction.SouthEast) || room_direction_ == Direction.NorthWest )
            {
                plus_luck_[2] += 20;
            }
            else
            {
                //キッチンを東，南東，北西にすれば風通しよく健康運上がる(いらんコメント？)
                comment_flag_.Add(new CommentFlag(CommentIdentifier.KitchenAiry, 20, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.KitchenAiry, 20, 5));
            }

        }
        else if (room_role_ == Room.Workroom)
        {
            //北西の仕事部屋は仕事運上がる
            if (room_direction_ == Direction.NorthWest)
            {
                plus_luck_[0] += 30;
            }
            else
            {
                //仕事部屋を北西にすれば仕事運上がる(いらんコメント？)
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WorkroomNorthWest, 30, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WorkroomNorthWest, 30, 5));
            }

        }
        else if (room_role_ == Room.Bathroom)
        {
            //東，南東の風呂は健康運上がる
            if (room_direction_ == Direction.East || room_direction_ == Direction.SouthEast )
            {
                plus_luck_[2] += 30;
            }
            else
            {
                //風呂を東，南東にすれば健康運上がる(いらんコメント？)
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BathroomAiry, 30, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BathroomAiry, 30, 5));
            }

            //陰気が強い風呂は恋愛運ダウン
            if( yin_yang_ < -30 )
            {
                minus_luck_[4] += 30;

                comment_flag_.Add(new CommentFlag(CommentIdentifier.BathroomYin, 30, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BathroomYin, 30, 5));
            }

        }
        else if (room_role_ == Room.Toilet)
        {

        }
    }

    //**************************************************************************************************************************************************************************************************

    //部屋の方位による運勢補正
    private void FortuneDirection()
    {
        if (room_direction_ == Direction.North)
        {
            //北は水の気でパワーアップ
            int direction_power;

            if (elements_[4] <= 300)
            {
                direction_power = (energy_strength_ / 5 + elements_[4]) / 2;
            }
            else
            {
                direction_power = (energy_strength_ / 5 + 300) / 2;
            }

            plus_luck_[3] += direction_power / 2;
            plus_luck_[4] += direction_power / 2;

            if (direction_power < 100)
            {
                //北の部屋のパワー弱すぎて金運，恋愛運が上がらない
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakNorth, (100 - direction_power) / 2, 3));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakNorth, (100 - direction_power) / 2, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakNorth, 100 - direction_power, 5));
            }

            //元々健康運に悪影響
            minus_luck_[2] += 50;

            //温かみのある家具一つにつき健康運アップ
            int warm_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                if (Array.IndexOf(furniture_grid_[i].characteristic(), FurnitureGrid.Characteristic.Warm) >= 0)
                {
                    ++warm_item;
                }
            }
            plus_luck_[2] += warm_item * 10;

            if (warm_item < 5)
            {
                //温かみのある家具が少なすぎて健康運に悪影響
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthCold, 50 - warm_item * 10, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthCold, 50 - warm_item * 10, 5));
            }

            //ピンクの家具一つにつき恋愛運がアップ
            int pink_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                if (Array.IndexOf(furniture_grid_[i].color_name(), FurnitureGrid.ColorName.Pink) >= 0)
                {
                    ++pink_item;
                }
            }
            plus_luck_[4] += pink_item * 10;

            if (pink_item < 3)
            {
                //ピンクの家具をもっと多くすれば恋愛運アップにつながる
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthPink, 30 - pink_item * 10, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthPink, 30 - pink_item * 10, 5));
            }

        }
        else if (room_direction_ == Direction.NorthEast)
        {
            //北東は木の気でパワーアップ
            int direction_power;

            if (elements_[2] <= 300)
            {
                direction_power = (energy_strength_ / 5 + elements_[2]) / 2;
            }
            else
            {
                direction_power = (energy_strength_ / 5 + 300) / 2;
            }

            if (yin_yang_ > 30 && yin_yang_ <= 300)
            {
                plus_luck_[0] += direction_power / 5;
                plus_luck_[1] += direction_power / 5;
                plus_luck_[2] += direction_power / 5;
                plus_luck_[3] += direction_power / 5;
                plus_luck_[4] += direction_power / 5;

                if (direction_power < 100)
                {
                    //北東の部屋のパワー弱すぎて全ての運気が上がらない
                    comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakNorthEast, (100 - direction_power) / 5, 0));
                    comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakNorthEast, (100 - direction_power) / 5, 1));
                    comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakNorthEast, (100 - direction_power) / 5, 2));
                    comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakNorthEast, (100 - direction_power) / 5, 3));
                    comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakNorthEast, (100 - direction_power) / 5, 4));
                    comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakNorthEast, 100 - direction_power, 5));
                }
            }
            else
            {
                minus_luck_[0] += direction_power / 10;
                minus_luck_[1] += direction_power / 10;
                minus_luck_[2] += direction_power / 10;
                minus_luck_[3] += direction_power / 10;
                minus_luck_[4] += direction_power / 10;

                //北東の陰陽バランスが悪いので全ての運気に悪影響
                comment_flag_.Add(new CommentFlag(CommentIdentifier.MinusNorthEast, direction_power / 10, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.MinusNorthEast, direction_power / 10, 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.MinusNorthEast, direction_power / 10, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.MinusNorthEast, direction_power / 10, 3));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.MinusNorthEast, direction_power / 10, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.MinusNorthEast, direction_power / 2, 5));
            }

            //背の高い家具一つにつき運気アップ
            int high_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                if (Array.IndexOf(furniture_grid_[i].form_type(), FurnitureGrid.FormType.High) >= 0)
                {
                    ++high_item;
                }
            }
            plus_luck_[0] += high_item * 5;
            plus_luck_[1] += high_item * 5;
            plus_luck_[2] += high_item * 5;
            plus_luck_[3] += high_item * 5;
            plus_luck_[4] += high_item * 5;

            if (high_item < 3)
            {
                //背の高い家具をさらに置けば運気アップにつながる
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthEastHigh, 15 - high_item * 5, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthEastHigh, 15 - high_item * 5, 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthEastHigh, 15 - high_item * 5, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthEastHigh, 15 - high_item * 5, 3));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthEastHigh, 15 - high_item * 5, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthEastHigh, 75 - high_item * 25, 5));
            }

        }
        else if (room_direction_ == Direction.East)
        {
            //東は木の気でパワーアップ
            int direction_power;

            if (elements_[0] <= 300)
            {
                direction_power = (energy_strength_ / 5 + elements_[0]) / 2;
            }
            else
            {
                direction_power = (energy_strength_ / 5 + 300) / 2;
            }

            plus_luck_[0] += direction_power;

            if (direction_power < 100)
            {
                //東の部屋のパワー弱すぎて仕事運が上がらない
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakEast, 100 - direction_power, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakEast, 100 - direction_power, 5));
            }

            //風関連，または音の出る家具で人気運，恋愛運アップ
            int wind_sound_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                if (Array.IndexOf(furniture_grid_[i].characteristic(), FurnitureGrid.Characteristic.Wind) >= 0
                    || Array.IndexOf(furniture_grid_[i].characteristic(), FurnitureGrid.Characteristic.Wind) >= 0)
                {
                    ++wind_sound_item;
                }
            }
            plus_luck_[1] += wind_sound_item * 5;
            plus_luck_[4] += wind_sound_item * 5;

            if (wind_sound_item < 3)
            {
                //風を連想させる家具，音の出る家具を置けば，人気運，恋愛運アップにつながる
                comment_flag_.Add(new CommentFlag(CommentIdentifier.EastWindSound, 15 - wind_sound_item * 5, 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.EastWindSound, 15 - wind_sound_item * 5, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.EastWindSound, 30 - wind_sound_item * 10, 5));
            }

        }
        else if (room_direction_ == Direction.SouthEast)
        {
            //南東は木の気でパワーアップ
            int direction_power;

            if (elements_[0] <= 300)
            {
                direction_power = (energy_strength_ / 5 + elements_[0]) / 2;
            }
            else
            {
                direction_power = (energy_strength_ / 5 + 300) / 2;
            }

            plus_luck_[1] += direction_power * 2 / 5;
            plus_luck_[4] += direction_power * 3 / 5;

            if (direction_power < 100)
            {
                //南東の部屋のパワー弱すぎて人気運，恋愛運が上がらない
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakSouth, (100 - direction_power) * 2 / 5, 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakSouth, (100 - direction_power) * 3 / 5, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakSouth, 100 - direction_power, 5));
            }

            //風関連，または音の出る家具で人気運，恋愛運アップ
            int wind_sound_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                if (Array.IndexOf(furniture_grid_[i].characteristic(), FurnitureGrid.Characteristic.Wind) >= 0
                    || Array.IndexOf(furniture_grid_[i].characteristic(), FurnitureGrid.Characteristic.Wind) >= 0)
                {
                    ++wind_sound_item;
                }
            }
            plus_luck_[1] += wind_sound_item * 5;
            plus_luck_[4] += wind_sound_item * 5;

            if (wind_sound_item < 3)
            {
                //風を連想させる家具，音の出る家具を置けば，人気運，恋愛運アップにつながる
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SouthEastWindSound, 15 - wind_sound_item * 5, 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SouthEastWindSound, 15 - wind_sound_item * 5, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SouthEastWindSound, 30 - wind_sound_item * 10, 5));
            }

            //オレンジの家具で人気運，恋愛運アップ
            int orange_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                if (Array.IndexOf(furniture_grid_[i].color_name(), FurnitureGrid.ColorName.Orange) >= 0)
                {
                    ++orange_item;
                }
            }
            plus_luck_[1] += orange_item * 5;
            plus_luck_[4] += orange_item * 10;

            if (wind_sound_item < 3)
            {
                //オレンジの家具を置けば，人気運，恋愛運アップにつながる
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SouthEastOrange, 15 - wind_sound_item * 5, 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SouthEastOrange, 30 - wind_sound_item * 10, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SouthEastOrange, 45 - wind_sound_item * 15, 5));
            }

        }
        else if (room_direction_ == Direction.South)
        {
            //南は火の気でパワーアップ
            int direction_power;

            if (elements_[1] <= 300)
            {
                direction_power = (energy_strength_ / 5 + elements_[1]) / 2;
            }
            else
            {
                direction_power = (energy_strength_ / 5 + 300) / 2;
            }

            plus_luck_[1] += direction_power * 3 / 5;
            plus_luck_[2] += direction_power / 5;
            plus_luck_[4] += direction_power / 5;

            if (direction_power < 100)
            {
                //南の部屋のパワー弱すぎて人気運，健康運，恋愛運が上がらない
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakSouth, (100 - direction_power) * 3 / 5, 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakSouth, (100 - direction_power) / 5, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakSouth, (100 - direction_power) / 5, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakSouth, 100 - direction_power, 5));
            }
        }
        else if (room_direction_ == Direction.SouthWest)
        {
            //南西は土の気でパワーアップ
            int direction_power;

            if (elements_[2] <= 300)
            {
                direction_power = (energy_strength_ / 5 + elements_[2]) / 2;
            }
            else
            {
                direction_power = (energy_strength_ / 5 + 300) / 2;
            }

            plus_luck_[0] += direction_power / 3;
            plus_luck_[1] += direction_power / 3;
            plus_luck_[2] += direction_power / 3;

            if (direction_power < 100)
            {
                //南西の部屋のパワー弱すぎて仕事運，人気運，健康運が上がらない
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakSouthWest, (100 - direction_power) / 3, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakSouthWest, (100 - direction_power) / 3, 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakSouthWest, (100 - direction_power) / 3, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakSouthWest, 100 - direction_power, 5));
            }

            //背の低い家具一つにつき運気アップ
            int low_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                if (Array.IndexOf(furniture_grid_[i].form_type(), FurnitureGrid.FormType.Low) >= 0)
                {
                    ++low_item;
                }
            }
            plus_luck_[0] += low_item * 5;
            plus_luck_[1] += low_item * 5;
            plus_luck_[2] += low_item * 5;
            plus_luck_[3] += low_item * 5;
            plus_luck_[4] += low_item * 5;

            if (low_item < 3)
            {
                //背の低い家具をさらに置けば運気アップにつながる
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SouthWestLow, 15 - low_item * 5, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SouthWestLow, 15 - low_item * 5, 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SouthWestLow, 15 - low_item * 5, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SouthWestLow, 15 - low_item * 5, 3));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SouthWestLow, 15 - low_item * 5, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SouthWestLow, 75 - low_item * 25, 5));
            }

        }
        else if (room_direction_ == Direction.West)
        {
            //西は金の気でパワーアップ
            int direction_power;

            if (elements_[3] <= 300)
            {
                direction_power = (energy_strength_ / 5 + elements_[3]) / 2;
            }
            else
            {
                direction_power = (energy_strength_ / 5 + 300) / 2;
            }

            plus_luck_[3] += direction_power / 2;
            plus_luck_[4] += direction_power / 2;

            if (direction_power < 100)
            {
                //西の部屋のパワー弱すぎて金運，恋愛運が上がらない
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakWest, (100 - direction_power) / 2, 3));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakWest, (100 - direction_power) / 2, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakWest, 100 - direction_power, 5));
            }

            //西洋風家具一つにつき金運アップ
            int western_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                if (Array.IndexOf(furniture_grid_[i].characteristic(), FurnitureGrid.Characteristic.Western) >= 0)
                {
                    ++western_item;
                }
            }
            plus_luck_[3] += western_item * 10;

            if (western_item < 3)
            {
                //西洋風家具をさらに置けば金運アップにつながる
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WestWestern, 30 - western_item * 10, 3));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WestWestern, 30 - western_item * 10, 5));
            }

            //高級家具一つにつき金運アップ
            int luxury_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                if (Array.IndexOf(furniture_grid_[i].characteristic(), FurnitureGrid.Characteristic.Luxury) >= 0)
                {
                    ++luxury_item;
                }
            }
            plus_luck_[3] += luxury_item * 10;

            if (luxury_item < 3)
            {
                //高級家具をさらに置けば金運アップにつながる
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WestLuxury, 30 - luxury_item * 10, 3));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WestLuxury, 30 - luxury_item * 10, 5));
            }

        }
        else if (room_direction_ == Direction.NorthWest)
        {
            //北西は金の気でパワーアップ
            int direction_power;

            if (elements_[3] <= 300)
            {
                direction_power = (energy_strength_ / 5 + elements_[3]) / 2;
            }
            else
            {
                direction_power = (energy_strength_ / 5 + 300) / 2;
            }

            plus_luck_[0] += direction_power / 2;
            plus_luck_[3] += direction_power / 2;

            if (direction_power < 100)
            {
                //北西の部屋のパワー弱すぎて仕事運，金運が上がらない
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakSouth, (100 - direction_power) / 2, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakSouth, (100 - direction_power) / 2, 3));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakSouth, 100 - direction_power, 5));
            }

            //高級家具一つにつき仕事運，金運アップ
            int luxury_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                if (Array.IndexOf(furniture_grid_[i].characteristic(), FurnitureGrid.Characteristic.Luxury) >= 0)
                {
                    ++luxury_item;
                }
            }

            if (luxury_item > 0)
            {
                plus_luck_[0] += luxury_item * 10;
                plus_luck_[3] += luxury_item * 5;

                if (luxury_item < 3)
                {
                    //高級家具をもっと置けば仕事運，金運アップ
                    comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthWestLuxury, 30 - luxury_item * 10, 0));
                    comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthWestLuxury, 15 - luxury_item * 5, 3));
                    comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthWestLuxury, 45 - luxury_item * 15, 5));
                }
            }
            else
            {
                minus_luck_[0] += 40;
                minus_luck_[3] += 20;

                //高級家具を置かなければ仕事運，金運ダウン
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthWestLuxuryZero, 40, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthWestLuxuryZero, 20, 3));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthWestLuxuryZero, 60, 5));
            }


        }
        else
        {
            Debug.Log("そのような方位は存在しない");
        }
    }

    //*********************************************************************************************************************************************************************************************

    //五行による補正
    private void FortuneFiveElement()
    {
        //木の気の影響
        if (elements_[0] <= 300)
        {
            plus_luck_[0] += elements_[0] * 4 / 5;
            plus_luck_[2] += elements_[0] / 5;

            if (elements_[0] < 50)
            {
                //木の気が弱すぎて仕事運と健康運が上がらない
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakWood, (50 - elements_[0]) * 4 / 5, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakWood, (50 - elements_[0]) / 5, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakWood, 50 - elements_[0], 5));
            }
        }
        else
        {
            plus_luck_[0] += 240;
            plus_luck_[2] += 60;

            //木の気が強すぎて仕事運に悪影響
            minus_luck_[0] += (elements_[0] - 300);
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverWood, elements_[0] - 300, 0));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverWood, elements_[0] - 300, 5));
        }

        //火の気の影響
        if (elements_[1] <= 300)
        {
            plus_luck_[1] += elements_[1] * 3 / 5;
            plus_luck_[2] += elements_[1] / 5;
            plus_luck_[4] += elements_[1] / 5;

            if (elements_[1] < 50)
            {
                //火の気が弱すぎて人気運，健康運，恋愛運が上がらない
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakFire, (50 - elements_[1]) * 3 / 5, 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakFire, (50 - elements_[1]) / 5, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakFire, (50 - elements_[1]) / 5, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakFire, 50 - elements_[1], 5));
            }
        }
        else
        {
            plus_luck_[1] += 180;
            plus_luck_[2] += 60;
            plus_luck_[4] += 60;

            //火の気が強すぎて仕事運，健康運，恋愛運に悪影響
            minus_luck_[0] += (elements_[1] - 300) / 3;
            minus_luck_[2] += (elements_[1] - 300) / 3;
            minus_luck_[4] += (elements_[1] - 300) / 3;
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverFire, (elements_[1] - 300) / 3, 0));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverFire, (elements_[1] - 300) / 3, 2));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverFire, (elements_[1] - 300) / 3, 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverFire, elements_[1] - 300, 5));
        }

        //土の気の影響
        if (elements_[2] <= 300)
        {
            plus_luck_[0] += elements_[0] / 5;
            plus_luck_[1] += elements_[1] / 5;
            plus_luck_[2] += elements_[2] / 5;
            plus_luck_[3] += elements_[3] / 5;
            plus_luck_[4] += elements_[4] / 5;

            if (elements_[2] < 50)
            {
                //土の気が弱すぎて仕事運，人気運，健康運，金運，恋愛運が上がらない
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakEarth, (50 - elements_[2]) / 5, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakEarth, (50 - elements_[2]) / 5, 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakEarth, (50 - elements_[2]) / 5, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakEarth, (50 - elements_[2]) / 5, 3));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakEarth, (50 - elements_[2]) / 5, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakEarth, 50 - elements_[2], 5));
            }
        }
        else
        {
            plus_luck_[0] += 60;
            plus_luck_[1] += 60;
            plus_luck_[2] += 60;
            plus_luck_[3] += 60;
            plus_luck_[4] += 60;

            //土の気が強すぎて健康運に悪影響
            minus_luck_[2] += (elements_[2] - 300);
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverEarth, elements_[2] - 300, 2));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverEarth, elements_[2] - 300, 5));
        }

        //金の気の影響
        if (elements_[3] <= 300)
        {
            plus_luck_[3] += elements_[3];

            if (elements_[3] < 50)
            {
                //金の気が弱すぎて金運が上がらない
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakMetal, 50 - elements_[3], 3));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakMetal, 50 - elements_[3], 5));
            }
        }
        else
        {
            plus_luck_[3] += 300;

            //金の気が強すぎて金運に悪影響
            minus_luck_[3] += (elements_[3] - 300);
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverMetal, elements_[3] - 300, 3));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverMetal, elements_[3] - 300, 5));
        }

        //水の気の影響
        if (elements_[4] <= 300)
        {
            plus_luck_[0] += elements_[4] / 5;
            plus_luck_[3] += elements_[4] / 5;
            plus_luck_[4] += elements_[4] * 3 / 5;

            if (elements_[4] < 50)
            {
                //水の気が弱すぎて仕事運，金運，恋愛運が上がらない
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakWater, (50 - elements_[4]) / 5, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakWater, (50 - elements_[4]) / 5, 3));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakWater, (50 - elements_[4]) * 3 / 5, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WeakWater, 50 - elements_[4], 5));
            }
        }
        else
        {
            plus_luck_[0] += 60;
            plus_luck_[3] += 60;
            plus_luck_[4] += 180;

            //水の気が強すぎて健康運，金運, 恋愛運に悪影響
            minus_luck_[2] += (elements_[4] - 300) / 5;
            minus_luck_[3] += (elements_[4] - 300) * 2 / 5;
            minus_luck_[4] += (elements_[4] - 300) * 2 / 5;
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverWater, (elements_[4] - 300) / 5, 2));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverWater, (elements_[4] - 300) * 2 / 5, 3));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverWater, (elements_[4] - 300) * 2 / 5, 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverWater, elements_[4] - 300, 5));
        }
    }

    //***********************************************************************************************************************************************************************************************

    //仕上げの運勢補正(鏡による運勢増減など)
    //
    //ここでは運勢に対し加算ではなく乗算による補正が行われる
    private void FortuneLast()
    {
        int[] displacement_plus_stock = new int[5] { 0, 0, 0, 0, 0 };

        int[] displacement_minus_stock = new int[5] { 0, 0, 0, 0, 0 };
        


        if (room_role_ == Room.Entrance)
        {
            int[] displacement = new int[5] { 0, 0, 0, 0, 0 };

            //玄関でプラスの運気，邪気共に1.5倍
            for (int i = 0; i < 5; ++i)
            {
                displacement_plus_stock[i] += plus_luck_[i] / 2;
                displacement_minus_stock[i] += minus_luck_[i] / 2;
                displacement[i] = minus_luck_[i] / 2 - plus_luck_[i] / 2;
                if (displacement[i] > 0)
                {
                    //玄関の邪気が高く，悪い運気を取り込みやすくなっている．
                    comment_flag_.Add(new CommentFlag(CommentIdentifier.EntranceMulti, displacement[i], i));
                }
            }

            int displacement_all = displacement[0] + displacement[1] + displacement[2] + displacement[3] + displacement[4];
            if (displacement_all > 0)
            {
                //玄関の邪気が高く，悪い運気を取り込みやすくなっている．
                comment_flag_.Add(new CommentFlag(CommentIdentifier.EntranceMulti, displacement_all, 5));
            }
        }
        else if (room_role_ == Room.Bedroom)
        {
            int[] displacement = new int[5] { 0, 0, 0, 0, 0 };

            //寝室でプラスの運気，邪気共に1.3倍
            for (int i = 0; i < 5; ++i)
            {
                displacement_plus_stock[i] += plus_luck_[i] * 3 / 10;
                displacement_minus_stock[i] += minus_luck_[i] * 3 / 10;
                displacement[i] = minus_luck_[i] * 3 / 10 - plus_luck_[i] * 3 / 10;
                if (displacement[i] > 0)
                {
                    //寝室の邪気が高く，悪い運気を取り込みやすくなっている．
                    comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomMulti, displacement[i], i));
                }
            }

            int displacement_all = displacement[0] + displacement[1] + displacement[2] + displacement[3] + displacement[4];
            if (displacement_all > 0)
            {
                //寝室の邪気が高く，悪い運気を取り込みやすくなっている．
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomMulti, displacement_all, 5));
            }

        }
        else if (room_role_ == Room.Living)
        {
            int[] displacement = new int[3] { 0, 0, 0 };

            //リビングで家庭運(仕事運, 人気運, 健康運)の良い運気，悪い運気共に1.1倍
            for (int i = 0; i < 3; ++i)
            {
                displacement_plus_stock[i] += plus_luck_[i] / 10;
                displacement_minus_stock[i] += minus_luck_[i] / 10;
                displacement[i] = minus_luck_[i] / 10 - plus_luck_[i] / 10;
                if (displacement[i] > 0)
                {
                    //リビングの邪気が高く，悪い仕事運，人気運，健康運を取り込みやすくなっている．
                    comment_flag_.Add(new CommentFlag(CommentIdentifier.LivingMulti, displacement[i], i));
                }

            }

            int displacement_all = displacement[0] + displacement[1] + displacement[2];
            if (displacement_all > 0)
            {
                //リビングの邪気が高く，悪い仕事運，人気運，健康運を取り込みやすくなっている．
                comment_flag_.Add(new CommentFlag(CommentIdentifier.LivingMulti, displacement_all, 5));
            }

        }
        else if (room_role_ == Room.Kitchen)
        {

        }
        else if (room_role_ == Room.Workroom)
        {
            int displacement = 0;

            //仕事部屋で仕事運のプラスの運気，邪気共に1.3倍
            displacement_plus_stock[0] += plus_luck_[0] * 3 / 10;
            displacement_minus_stock[0] += minus_luck_[0] * 3 / 10;
            displacement = minus_luck_[0] * 3 / 10 - plus_luck_[0] * 3 / 10;
            if (displacement > 0)
            {
                //寝室の邪気が高く，悪い運気を取り込みやすくなっている．
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WorkroomMulti, displacement, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WorkroomMulti, displacement, 5));
            }

        }
        else if (room_role_ == Room.Bathroom)
        {

        }
        else if (room_role_ == Room.Toilet)
        {

        }



        if (room_direction_ == Direction.North)
        {

        }
        else if (room_direction_ == Direction.NorthEast)
        {

        }
        else if (room_direction_ == Direction.East)
        {

        }
        else if (room_direction_ == Direction.SouthEast)
        {

        }
        else if (room_direction_ == Direction.South)
        {
            int[] displacement = new int[5] { 0, 0, 0, 0, 0 };

            //邪気(悪い運気)0.9倍
            for (int i = 0; i < 5; ++i)
            {
                if (elements_[1] > 250)
                {
                    displacement_minus_stock[i] -= minus_luck_[i] / 10;
                }
                else
                {
                    //南の火の気をある程度強めることで悪い運気を浄化することができます
                    displacement[i] = minus_luck_[i] / 10;
                    comment_flag_.Add(new CommentFlag(CommentIdentifier.SouthPurification, displacement[i], i));
                }
            }

            if (elements_[1] <= 250)
            {
                //南の火の気をある程度強めることで悪い運気を浄化することができます
                int displacement_all = displacement[0] + displacement[1] + displacement[2] + displacement[3] + displacement[4];
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SouthPurification, displacement_all, 5));
            }

        }
        else if (room_direction_ == Direction.SouthWest)
        {

        }
        else if (room_direction_ == Direction.West)
        {

        }
        else if (room_direction_ == Direction.NorthWest)
        {

        }

        //最後に元の運勢に補正を加算
        for (int i = 0; i < 5; ++i)
        {
            plus_luck_[i] += displacement_plus_stock[i];
            minus_luck_[i] += displacement_minus_stock[i];
        }
    }

    partial void EvaluationTotaTestDummy();
    partial void Comment();
}