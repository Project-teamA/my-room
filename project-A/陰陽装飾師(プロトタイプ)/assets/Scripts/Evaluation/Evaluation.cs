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
using UnityEngine.UI;

public partial class Evaluation : MonoBehaviour
{
    public enum Room { Entrance, Living, Kitchen, Workroom, Bedroom, Bathroom, Toilet };
    public enum Direction { North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest };

    //コメントの識別子(列挙型)
    //基本的にボーナス点，ペナルティ点が呼び出されたら必ずコメント識別子が呼び出される．
    public enum CommentIdentifier
    {
        OverYin, OverYang, //陰陽の強すぎ

        EntranceYinNorthEast, EntranceYinSouthWest, EntranceNoCarpet, EntranceSmell, EntranceMulti, //玄関関連
        BedroomLuckyDirection, BedroomNorthEast, BedroomBeauty, BedroomWoodNatural, BedroomBlue, BedroomMulti, //寝室関連
        LivingMulti, //リビング関連
        KitchenDamage, KitchenAiry, //キッチン関連
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
        NorthWestLuxury, NorthWestLuxuryZero, NorthWestSilverGray, NorthWestVain, //北西の運勢

        WhiteResetYinYang, WhitePurification, BlackStrengthening, RedOne, PinkOne, PinkNoOrange, BlueOne, BlueNoOrange, OrangeNoPink, OrangeNoBlue, //色関連
        YellowBrownOne, GreenPurification, BeigeIneger, Cream, GoldMulti,  //色関連
                                                                           //素材関連
                                                                           //模様関連
        SquareFix, CircleGoodRelation, CircleCirculation, SharpBadRelation, //形状関連
        SweetSmell, Luminescence, FlowerAssociative, //その他特性
        ExcessFurniture, ShortageFurniture,  //家具の数関連

        WeakWood, WeakFire, WeakEarth, WeakMetal, WeakWater, WeakEnergy, //気が弱い
        OverWood, OverFire, OverEarth, OverMetal, OverWater, //気が強すぎ
    };

    //------------------------------------------------------------------------------------------------------------------------------------------------

    //コメント識別認識補助(Comment関数内でif文でコメントを分けるために使用する)
    public enum CommentSupport
    {
        WoodToFireSosho, FireToEarthSosho, EarthToMetalSosho, MetalToWaterSosho, WaterToWoodSosho, //相生効果により上がった気
        WoodToEarthSokoku, FireToMetalSokoku, EarthToWaterSokoku, MetalToWoodSokoku, WaterToFireSokoku, //相克効果により下がった気
        OverYin, OverYang, //陰陽が強すぎ
        OverWood, OverFire, OverEarth, OverMetal, OverWater, //気が強すぎ
        ChemicalOrPlastic,
    }


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
    private class CommentFlag
    {
        public CommentIdentifier comment_identifier_; //コメント識別子
        public int flag_weight_; //コメントの重要度(数値がおおきいほど重要度が高い)
        public int luck_identifier_;  //影響を受けた運気の識別子 [0] = 仕事運，[1] = 人気運, [2] = 健康運, [3] = 金運, [4] = 恋愛運

        public CommentFlag(CommentIdentifier comment_identifier, int flag_weight, int luck_identifier)
        {
            comment_identifier_ = comment_identifier;
            flag_weight_ = flag_weight;
            luck_identifier_ = luck_identifier;
        }

        public void WeightAdd(int addend)
        {
            flag_weight_ += addend;
        }
    }

    //コメント選択補助用構造体(部屋の条件によってコメント変更)
    //
    //もしかしたらcomment_identifierみたいに構造体にするかもしれない
    private List<CommentSupport> comment_support_ = new List<CommentSupport>();

    private List<CommentFlag> comment_flag_ = new List<CommentFlag>();
    private List<string> comment_ = new List<string>(); //コメント ( コメントフラグに応じていくつかのコメントを出力 )

    private bool is_finished_game_; //ゲームが終わったかどうかのフラグ (ture = ゲーム終了 false = ゲーム終了せず)
    private int advaice_mode_; //アドバイスモード(0 = 仕事運重視，1 = 人気運重視，2 = 健康運重視，3 = 金運重視，4 = 恋愛運重視, 5 = デフォルト(ノルマ重視))

    //*******************************************************************************************************************************************************************************************

    public Text work_luck_text_;
    public Text popular_luck_text_;
    public Text health_luck_text_;
    public Text economic_luck_text_;
    public Text love_luck_text_;

    public Text hint_text_;

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

    //仕事運ノルマ(取得用)
    public int work_norma()
    {
        return norma_luck_[0];
    }

    //人気運ノルマ(取得用)
    public int popular_norma()
    {
        return norma_luck_[1];
    }

    //健康運ノルマ(取得用)
    public int health_norma()
    {
        return norma_luck_[2];
    }

    //金運ノルマ(取得用)
    public int economic_norma()
    {
        return norma_luck_[4];
    }

    //恋愛運ノルマ(取得用)
    public int love_norma()
    {
        return norma_luck_[5];
    }

    //総合運ノルマ(取得用)
    public int all_norma()
    {
        return all_norma_;
    }

    //コメント(取得用)
    public List<string> comment()
    {
        return comment_;
    }

    public void Comment_Text()
    {
        if (is_finished_game_ == true)
        {
            DataManager.set_comment(comment_);
        }
        else
        {
            hint_text_.text = comment_[0];
        }        
    }

    //*******************************************************************************************************************************************************************************************

    private Grid_Manager Grid_Manager_;
    private DataManager DataManager;

    //初期化関数(この関数は最初に1回だけ実行すること)
    public void Init(Room room_role, Direction room_direction, int[] norma_luck, int advaice_mode, List<FurnitureGrid> furniture_grid, Grid_Manager grid_manager)
    {
        Grid_Manager_ = grid_manager;
        DataManager = GameObject.Find("DataManager").GetComponent<DataManager>();

        room_role_ = room_role;
        room_direction_ = room_direction;
        norma_luck_ = norma_luck;
        advaice_mode_ = advaice_mode;
        furniture_grid_ = furniture_grid;
        is_finished_game_ = false;
        
        for (int i = 0; i < 5; ++i)
        {
            split_elements_[i] = new int[8];
        }

        EvaluationTotal();
        UpdateLuckText();
        Comment_Text();
    }

    //ノルマセット関数
    public void set_norma(int work_norma, int popular_norma, int health_norma, int economic_norma, int love_norma, int all_norma)
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
    public void UpdateGrid(List<FurnitureGrid> furniture_grid)
    {
        furniture_grid_ = furniture_grid;
    }

    //家具の削除関数(変更する可能性がかなり高い)
    public void DeleteGrid(int delete_number)
    {
        if (-1 < delete_number && furniture_grid_.Count > delete_number)
        {
            furniture_grid_.RemoveAt(delete_number);
        }
    }

    //is_finised_gameに値をセット
    public void set_is_finishedGame(bool is_finished_game)
    {
        is_finished_game_ = is_finished_game;
    }

    //advaice_mode_に値をセット
    public void set_advaice_mode(int advaice_mode)
    {
        if (advaice_mode < 0 || advaice_mode > 5)
        {
            Debug.Log("アドバイスモードの設定がおかしいのでデフォルトにします．");
        }
        advaice_mode_ = advaice_mode;
    }

    //*******************************************************************************************************************************************************************************************


    public void UpdateLuckText()
    {
        work_luck_text_.text = luck_[0].ToString();
        popular_luck_text_.text = luck_[1].ToString();
        health_luck_text_.text = luck_[2].ToString();
        economic_luck_text_.text = luck_[3].ToString();
        love_luck_text_.text = luck_[4].ToString();      
    }

    //総合評価関数(評価の一連の流れ)
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

        EvaluationItem();
        EvaluationRoom();
        EvaluationDirection();
        EvaluationFiveElements();
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

        FortuneRoom();
        FortuneDirection();
        FortuneFiveElement();

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

            comment_support_.Add(CommentSupport.OverYin);
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

            comment_support_.Add(CommentSupport.OverYang);
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
    private void FortuneItem()
    {
        //プラスチック, または化学繊維の家具があるかどうか(CommentSupport用).
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            if (furniture_grid_[i].material_type().IndexOf(FurnitureGrid.MaterialType.ChemicalFibre) >= 0
                || furniture_grid_[i].material_type().IndexOf(FurnitureGrid.MaterialType.Plastic) >= 0)
            {
                comment_support_.Add(CommentSupport.ChemicalOrPlastic);
                break;
            }
        }

        int[] color_luck_plus_stock = new int[5] { 0, 0, 0, 0, 0 };
        int[] color_luck_minus_stock = new int[5] { 0, 0, 0, 0, 0 };

        //赤い家具1つでも置くと仕事，人気，健康，恋愛アップ
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            if (furniture_grid_[i].color_name().IndexOf(FurnitureGrid.ColorName.Red) >= 0)
            {
                color_luck_plus_stock[0] += 10;
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
            if (furniture_grid_[i].color_name().IndexOf(FurnitureGrid.ColorName.Pink) >= 0)
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
            if (furniture_grid_[i].color_name().IndexOf(FurnitureGrid.ColorName.Blue) >= 0)
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
            if (furniture_grid_[i].color_name().IndexOf(FurnitureGrid.ColorName.Blue) >= 0)
            {
                orange_at_least = true;
                break;
            }
        }

        if (orange_at_least)
        {
            if (pink_at_least)
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

            if (blue_at_least)
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
        else if (pink_at_least)
        {
            //ピンクがあるのでオレンジと合わせましょう
            comment_flag_.Add(new CommentFlag(CommentIdentifier.PinkNoOrange, 30, 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.PinkNoOrange, 30, 5));
        }
        else if (blue_at_least)
        {
            //青があるのでオレンジと合わせましょう
            comment_flag_.Add(new CommentFlag(CommentIdentifier.BlueNoOrange, 30, 0));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.BlueNoOrange, 30, 5));
        }

        //ベージュの家具1つにつき仕事運，恋愛運アップ
        int beige_item = 0;
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            if (furniture_grid_[i].color_name().IndexOf(FurnitureGrid.ColorName.Beige) >= 0)
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

        for (int i = 0; i < 5; ++i)
        {
            plus_luck_[i] += color_luck_plus_stock[i];
            minus_luck_[i] += color_luck_minus_stock[i];
        }

        //家具の特性による評価
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            //ベッド
            if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.bed)
            {
                //寝室のとき(事前に減らしているため)
                if (room_role_ == Room.Bedroom)
                {
                    luck_[2] += 30;
                    luck_[3] += 30;
                    luck_[4] += 30;
                    luck_[0] += 30;
                    luck_[1] += 30;
                }
                //寝室以外はダメ
                else
                {
                    luck_[2] -= 10;
                    luck_[3] -= 10;
                    luck_[4] -= 10;
                    luck_[0] -= 10;
                    luck_[1] -= 10;
                }

                //天然素材
                for (int j = 0; j < furniture_grid_[i].material_type().Count; j++)
                {
                    if (furniture_grid_[i].material_type()[j] == FurnitureGrid.MaterialType.NaturalFibre)
                    {
                        luck_[2] += 10;
                    }
                }

                RaycastHit hit;

                //ベッドの枕の位置(安眠できるかどうかで，健康運，美容運にかかわる)
                if (furniture_grid_[i].up_direction() == Vector3.up)
                {
                    //ベッドが北枕(1番良い, 安眠できる)
                    luck_[2] += 30;
                    luck_[0] += 10;
                }
                else if (furniture_grid_[i].up_direction() == Vector3.down)
                {
                    //ベッドが南枕(1番ダメ, 安眠出来ない)
                    luck_[2] -= 20;
                    luck_[0] -= 10;
                }
                else if (furniture_grid_[i].up_direction() == Vector3.right)
                {
                    //ベッドが東枕(2番目に良い)
                    luck_[2] += 20;
                    luck_[0] += 10;
                }
                else if (furniture_grid_[i].up_direction() == Vector3.left)
                {
                    //ベッドが西枕(2番目に良くない)
                    luck_[2] -= 10;
                }

                //シングルベッドをつなげるとダメ
                for (int j = 0; j < furniture_grid_.Count; ++j)
                {
                    //ベッド
                    if (furniture_grid_[i] != furniture_grid_[j])
                    {
                        if (furniture_grid_[j].furniture_type() == FurnitureGrid.FurnitureType.bed)
                        {
                            Vector3 left_down_source = furniture_grid_[i].vertices(furniture_grid_[i].parameta(2)) + furniture_grid_[i].grid_position();
                            Vector3 left_up_source = furniture_grid_[i].vertices(furniture_grid_[i].parameta(3)) + furniture_grid_[i].grid_position();
                            Vector3 right_down_source = furniture_grid_[i].vertices(furniture_grid_[i].parameta(4)) + furniture_grid_[i].grid_position();
                            Vector3 right_up_source = furniture_grid_[i].vertices(furniture_grid_[i].parameta(5)) + furniture_grid_[i].grid_position();

                            Vector3 left_down_target = furniture_grid_[j].vertices(furniture_grid_[j].parameta(2)) + furniture_grid_[j].grid_position();
                            Vector3 left_up_target = furniture_grid_[j].vertices(furniture_grid_[j].parameta(3)) + furniture_grid_[j].grid_position();
                            Vector3 right_down_target = furniture_grid_[j].vertices(furniture_grid_[j].parameta(4)) + furniture_grid_[j].grid_position();
                            Vector3 right_up_target = furniture_grid_[j].vertices(furniture_grid_[j].parameta(5)) + furniture_grid_[j].grid_position();

                            if (((left_down_source == right_down_target) &&
                                (left_up_source == right_up_target)) ||
                                ((right_down_source == left_down_target) &&
                                (right_up_source == left_up_target)))
                            {
                                //つながっている
                                Debug.Log("つながっている");
                            }
                        }
                    }
                }

                //ベッドと壁の隙間ダメ
                bool left_down = false;
                bool left_up = false;
                bool right_down = false;
                bool right_up = false;

                for (int l = 0; l < furniture_grid_[i].vertices_number(); l++)
                {
                    for (int j = Grid_Manager_.Grid_y_min; j < Grid_Manager_.Grid_y_max; j++)
                    {
                        for (int k = Grid_Manager_.Grid_x_min; k < Grid_Manager_.Grid_x_max; k++)
                        {
                            if (Grid_Manager_.point(k, j).wall == true)
                            {
                                float verticesx = furniture_grid_[i].vertices(l).x + furniture_grid_[i].grid_position().x;
                                float verticesy = furniture_grid_[i].vertices(l).y + furniture_grid_[i].grid_position().y;

                                float grid_x_min = Grid_Manager_.point(k, j).pos.x - (Grid_Manager_.Grid_density / 2.0f);
                                float grid_y_min = Grid_Manager_.point(k, j).pos.y - (Grid_Manager_.Grid_density / 2.0f);
                                float grid_x_max = Grid_Manager_.point(k, j).pos.x + (Grid_Manager_.Grid_density / 2.0f);
                                float grid_y_max = Grid_Manager_.point(k, j).pos.y + (Grid_Manager_.Grid_density / 2.0f);

                                if (grid_x_min < verticesx && verticesx < grid_x_max &&
                                    grid_y_min < verticesy && verticesy < grid_y_max)
                                {
                                    if (furniture_grid_[i].vertices(l) == furniture_grid_[i].vertices(furniture_grid_[i].parameta(2)))
                                    {
                                        left_down = true;
                                    }
                                    else if (furniture_grid_[i].vertices(l) == furniture_grid_[i].vertices(furniture_grid_[i].parameta(3)))
                                    {
                                        left_up = true;
                                    }
                                    else if (furniture_grid_[i].vertices(l) == furniture_grid_[i].vertices(furniture_grid_[i].parameta(4)))
                                    {
                                        right_down = true;
                                    }
                                    else if (furniture_grid_[i].vertices(l) == furniture_grid_[i].vertices(furniture_grid_[i].parameta(5)))
                                    {
                                        right_up = true;
                                    }
                                }
                            }
                        }
                    }
                }

                if ((left_down == true && left_up == true) || (right_down == true && right_up == true))
                {
                    //隙間なし
                    Debug.Log("隙間なし");
                }
                else
                {
                    //隙間あり
                    Debug.Log("隙間あり");
                }

                //ドアが正面            
                for (int j = 0; j < furniture_grid_.Count; ++j)
                {
                    //ドア
                    if (furniture_grid_[j].furniture_type() == FurnitureGrid.FurnitureType.Door)
                    {
                        if (Physics.Raycast(furniture_grid_[i].furniture_grid().transform.position, furniture_grid_[i].up_direction(), out hit))
                        {
                            if (hit.collider.gameObject == furniture_grid_[j].furniture_grid())
                            {
                                //ダメ
                            }
                        }
                    }
                }

                //窓の近くにベッドダメ
                for (int j = 0; j < furniture_grid_.Count; ++j)
                {
                    //窓
                    if (furniture_grid_[j].furniture_type() == FurnitureGrid.FurnitureType.Window)
                    {
                        //近く
                        if (2.0f > Vector3.Distance(furniture_grid_[i].furniture_grid().transform.position, furniture_grid_[j].furniture_grid().transform.position))
                        {
                            //ダメ
                        }

                    }
                }

                //角がベッドの顔に向いたらだめ
            }
            //ソファ
            else if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.sofa)
            {
                RaycastHit hit;

                //西に配置
                if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.West)
                {
                    //東向き
                    if (furniture_grid_[i].up_direction() == new Vector3(1, 0, 0))
                    {
                        for (int j = 0; j < furniture_grid_.Count; ++j)
                        {
                            //テレビ
                            if (furniture_grid_[j].furniture_type() == FurnitureGrid.FurnitureType.electronics)
                            {
                                //西向き
                                if (furniture_grid_[j].up_direction() == new Vector3(-1, 0, 0))
                                {
                                    if (Physics.Raycast(furniture_grid_[i].furniture_grid().transform.position, furniture_grid_[i].up_direction(), out hit))
                                    {
                                        if (hit.collider.gameObject == furniture_grid_[j].furniture_grid())
                                        {
                                            //良い
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //ドアの真正面
                for (int j = 0; j < furniture_grid_.Count; ++j)
                {
                    //ドア
                    if (furniture_grid_[j].furniture_type() == FurnitureGrid.FurnitureType.Door)
                    {
                        if (Physics.Raycast(furniture_grid_[i].furniture_grid().transform.position, furniture_grid_[i].up_direction(), out hit))
                        {
                            if (hit.collider.gameObject == furniture_grid_[j].furniture_grid())
                            {
                                //ダメ
                            }
                        }
                    }
                }
            }
            //机
            else if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.desk)
            {
                //寝室
                if (room_role_ == Room.Bedroom)
                {
                    luck_[2] -= 20;
                    luck_[0] -= 20;
                }

                //北向き
                if (furniture_grid_[i].up_direction() == Vector3.up)
                {
                    //仕事運小アップ
                    luck_[0] += 10;
                }
                //東向き
                else if (furniture_grid_[i].up_direction() == Vector3.right)
                {
                    //仕事運アップ
                    luck_[0] += 30;
                }
                //西向き
                else if (furniture_grid_[i].up_direction() == Vector3.left)
                {
                    //金運アップ
                    luck_[3] += 30;
                }
                //南向き
                else if (furniture_grid_[i].up_direction() == Vector3.down)
                {
                    //人気運アップ
                    luck_[1] += 30;
                }

                //窓の正面
                RaycastHit hit;
                for (int j = 0; j < furniture_grid_.Count; ++j)
                {
                    //窓
                    if (furniture_grid_[j].furniture_type() == FurnitureGrid.FurnitureType.Window)
                    {
                        if (Physics.Raycast(furniture_grid_[i].furniture_grid().transform.position, furniture_grid_[i].up_direction(), out hit))
                        {
                            if (hit.collider.gameObject == furniture_grid_[j].furniture_grid())
                            {
                                luck_[0] -= 20;
                            }
                        }
                    }
                }

                //机から見て東側に観葉植物を置くとよい
            }

            else if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.table)
            {

            }

            else if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.carpet)
            {

            }
            //鏡
            else if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.dresser)
            {
                RaycastHit hit;

                //南に配置
                if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.South)
                {
                    luck_[1] += 10;
                    luck_[4] += 5;
                    luck_[2] += 5;
                }

                //丸い鏡
                if (furniture_grid_[i].form_type()[0] == FurnitureGrid.FormType.Round)
                {
                    //全ての運気が1.2倍
                    luck_[2] = (int)(luck_[2] * 1.2f);
                    luck_[3] = (int)(luck_[3] * 1.2f);
                    luck_[4] = (int)(luck_[4] * 1.2f);
                    luck_[0] = (int)(luck_[0] * 1.2f);
                    luck_[1] = (int)(luck_[1] * 1.2f);

                    //リビング
                    if (room_role_ == Room.Living)
                    {
                        //北の方角に置く
                        if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.North)
                        {
                            //全体的に運気が上がる（特に金運）
                        }
                    }
                }
                //四角い鏡
                if (furniture_grid_[i].form_type()[0] == FurnitureGrid.FormType.Square)
                {
                    //全ての運気が0.8倍
                    luck_[2] = (int)(luck_[2] * 0.8f);
                    luck_[3] = (int)(luck_[3] * 0.8f);
                    luck_[4] = (int)(luck_[4] * 0.8f);
                    luck_[0] = (int)(luck_[0] * 0.8f);
                    luck_[1] = (int)(luck_[1] * 0.8f);

                    //北の方角に置く
                    if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.North)
                    {
                        //ダメ
                    }

                    //北東の方角に置く
                    if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.NorthEast)
                    {
                        //良い
                    }

                    //窓の正面
                    for (int j = 0; j < furniture_grid_.Count; ++j)
                    {
                        //窓
                        if (furniture_grid_[j].furniture_type() == FurnitureGrid.FurnitureType.Window)
                        {
                            if (Physics.Raycast(furniture_grid_[i].furniture_grid().transform.position, furniture_grid_[i].up_direction(), out hit))
                            {
                                if (hit.collider.gameObject == furniture_grid_[j].furniture_grid())
                                {
                                    //ダメ
                                }
                            }
                        }
                    }
                }
                //長方形の鏡
                if (furniture_grid_[i].form_type()[0] == FurnitureGrid.FormType.Rectangle)
                {
                    //東の方角に置く
                    if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.East)
                    {
                        //良い
                    }
                    //南東の方角に置く
                    if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.SouthEast)
                    {
                        //良い
                    }
                }
                //
                for (int j = 0; j < furniture_grid_.Count; ++j)
                {
                    //ベッドが正面(写っている)
                    if (furniture_grid_[j].furniture_type() == FurnitureGrid.FurnitureType.bed)
                    {
                        if (Physics.Raycast(furniture_grid_[i].furniture_grid().transform.position, furniture_grid_[i].up_direction(), out hit))
                        {
                            if (hit.collider.gameObject == furniture_grid_[j].furniture_grid())
                            {
                                luck_[2] -= 20;
                                luck_[4] -= 20;
                                luck_[0] -= 5;
                                luck_[1] -= 5;
                                luck_[3] -= 5;
                            }
                        }
                    }
                    //鏡が正面(合わせ鏡)
                    if (furniture_grid_[j].furniture_type() == FurnitureGrid.FurnitureType.dresser)
                    {
                        if (Physics.Raycast(furniture_grid_[i].furniture_grid().transform.position, furniture_grid_[i].up_direction(), out hit))
                        {
                            if (hit.collider.gameObject == furniture_grid_[j].furniture_grid())
                            {
                                luck_[2] -= 40;
                                luck_[4] -= 40;
                                luck_[0] -= 40;
                                luck_[1] -= 40;
                                luck_[3] -= 60;
                            }
                        }
                    }
                }

            }
            //家電(TV)
            else if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.electronics)
            {
                //南に配置
                if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.South)
                {
                    //ダメ
                }
                //東に配置
                if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.East)
                {
                    //陽気が特大アップ
                }

                //窓やドアの真正面
                RaycastHit hit;

                for (int j = 0; j < furniture_grid_.Count; ++j)
                {
                    //ドア
                    if (furniture_grid_[j].furniture_type() == FurnitureGrid.FurnitureType.Door)
                    {
                        if (Physics.Raycast(furniture_grid_[i].furniture_grid().transform.position, furniture_grid_[i].up_direction(), out hit))
                        {
                            if (hit.collider.gameObject == furniture_grid_[j].furniture_grid())
                            {
                                //ダメ
                            }
                        }
                    }
                    //窓
                    else if (furniture_grid_[j].furniture_type() == FurnitureGrid.FurnitureType.Window)
                    {
                        if (Physics.Raycast(furniture_grid_[i].furniture_grid().transform.position, furniture_grid_[i].up_direction(), out hit))
                        {
                            if (hit.collider.gameObject == furniture_grid_[j].furniture_grid())
                            {
                                //ダメ
                            }
                        }
                    }
                    //ベッド
                    else if (furniture_grid_[j].furniture_type() == FurnitureGrid.FurnitureType.Window)
                    {
                        if (Physics.Raycast(furniture_grid_[i].furniture_grid().transform.position, furniture_grid_[i].up_direction(), out hit))
                        {
                            if (hit.collider.gameObject == furniture_grid_[j].furniture_grid())
                            {
                                luck_[2] -= 10;
                                luck_[4] -= 5;
                                luck_[0] -= 5;
                                luck_[1] -= 5;
                                luck_[3] -= 5;
                            }
                        }
                    }
                }
            }

            else if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.Window)
            {

            }
            else if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.Door)
            {

            }

            //***************************************************************************************************************

            else if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.foliage)
            {

            }
            else if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.artificialflower)
            {

            }
            else if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.watertank)
            {

            }
            else if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.curtain)
            {

            }
            else if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.desklamp)
            {

            }
            else if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.chair)
            {

            }
            else if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.pictureframe)
            {

            }
            else if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.plushdoll)
            {
                //寝室にたくさんぬいぐるみダメ
                //寝室では恋愛運下がる
            }
            else if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.cabinet)
            {
                //南向き
                //背の低いタンスがよい
            }
            //招き猫
            else if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.cat)
            {
                //右手を上げているのは金運アップ
                //左手を上げているのは恋愛運アップ
                //両手は0
            }
            else
            {
                Debug.Log("ありえん");
            }
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
                if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.carpet)
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
                if (furniture_grid_[i].characteristic().IndexOf(FurnitureGrid.Characteristic.Smell) >= 0)
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
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomLuckyDirection, 5 / 2, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomLuckyDirection, 5 / 2, 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomLuckyDirection, 10 / 2, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomLuckyDirection, 5 / 2, 3));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomLuckyDirection, 10 / 2, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomLuckyDirection, 35 / 2, 5));
            }

            //北東の寝室は金運アップにつながる
            if (room_direction_ == Direction.NorthEast)
            {
                plus_luck_[3] += 20;
            }
            else
            {
                //寝室を北東にすれば運気上がる(方向は定まっているので要らないコメント？)
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomNorthEast, 20 / 2, 3));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomNorthEast, 20 / 2, 5));
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
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomBeauty, 10 / 2, 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomBeauty, 5 / 2, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomBeauty, 5 / 2, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomBeauty, 20 / 2, 5));
            }

            //木材家具, 天然素材1個につき健康運アップ(両方の特性を持っている場合は1個にカウント)
            int wood_natural_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                if (furniture_grid_[i].material_type().IndexOf(FurnitureGrid.MaterialType.Wooden) >= 0
                    || furniture_grid_[i].material_type().IndexOf(FurnitureGrid.MaterialType.NaturalFibre) >= 0)
                {
                    ++wood_natural_item;
                }

            }
            plus_luck_[2] += wood_natural_item * 10;

            if (wood_natural_item < 3)
            {
                //木材家具，天然素材さらに置けば健康運アップにつながる
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomWoodNatural, 30 - wood_natural_item * 10, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomWoodNatural, 30 - wood_natural_item * 10, 5));
            }

            //青い家具一個につき安眠で健康運アップ
            int blue_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                if (furniture_grid_[i].color_name().IndexOf(FurnitureGrid.ColorName.Blue) >= 0)
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
                comment_flag_.Add(new CommentFlag(CommentIdentifier.KitchenDamage, 30 / 2, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.KitchenDamage, 30 / 2, 5));
            }

            //東，南東，北西のキッチンは風通しよく健康運上がる
            if ((room_direction_ == Direction.East || room_direction_ == Direction.SouthEast) || room_direction_ == Direction.NorthWest)
            {
                plus_luck_[2] += 20;
            }
            else
            {
                //キッチンを東，南東，北西にすれば風通しよく健康運上がる(いらんコメント？)
                comment_flag_.Add(new CommentFlag(CommentIdentifier.KitchenAiry, 20 / 2, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.KitchenAiry, 20 / 2, 5));
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
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WorkroomNorthWest, 30 / 2, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WorkroomNorthWest, 30 / 2, 5));
            }

        }
        else if (room_role_ == Room.Bathroom)
        {
            //東，南東の風呂は健康運上がる
            if (room_direction_ == Direction.East || room_direction_ == Direction.SouthEast)
            {
                plus_luck_[2] += 30;
            }
            else
            {
                //風呂を東，南東にすれば健康運上がる(いらんコメント？)
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BathroomAiry, 30 / 2, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BathroomAiry, 30 / 2, 5));
            }

            //陰気が強い風呂は恋愛運ダウン
            if (yin_yang_ < -30)
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

            //元々健康運，人気運に悪影響
            minus_luck_[1] += 50;
            minus_luck_[2] += 50;

            //温かみのある家具一つにつき健康運, 人気運アップ
            int warm_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                if (furniture_grid_[i].characteristic().IndexOf(FurnitureGrid.Characteristic.Warm) >= 0)
                {
                    ++warm_item;
                }
            }
            plus_luck_[1] += warm_item * 10;
            plus_luck_[2] += warm_item * 10;

            if (warm_item < 5)
            {
                //温かみのある家具が少なすぎて健康運，人気運に悪影響
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthCold, 50 - warm_item * 10, 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthCold, 50 - warm_item * 10, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthCold, 100 - warm_item * 20, 5));
            }

            //ピンクの家具一つにつき恋愛運がアップ
            int pink_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                if (furniture_grid_[i].color_name().IndexOf(FurnitureGrid.ColorName.Pink) >= 0)
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
                if (furniture_grid_[i].form_type().IndexOf(FurnitureGrid.FormType.High) >= 0)
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
                if (furniture_grid_[i].characteristic().IndexOf(FurnitureGrid.Characteristic.Wind) >= 0
                    || furniture_grid_[i].characteristic().IndexOf(FurnitureGrid.Characteristic.Sound) >= 0)
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
                if (furniture_grid_[i].characteristic().IndexOf(FurnitureGrid.Characteristic.Wind) >= 0
                    || furniture_grid_[i].characteristic().IndexOf(FurnitureGrid.Characteristic.Sound) >= 0)
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
                if (furniture_grid_[i].color_name().IndexOf(FurnitureGrid.ColorName.Orange) >= 0)
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
                if (furniture_grid_[i].form_type().IndexOf(FurnitureGrid.FormType.Low) >= 0)
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
                if (furniture_grid_[i].characteristic().IndexOf(FurnitureGrid.Characteristic.Western) >= 0)
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
                if (furniture_grid_[i].characteristic().IndexOf(FurnitureGrid.Characteristic.Luxury) >= 0)
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
                if (furniture_grid_[i].characteristic().IndexOf(FurnitureGrid.Characteristic.Luxury) >= 0)
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

            //銀 灰の家具一つにつき仕事運アップ
            int silver_gray_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                if (furniture_grid_[i].color_name().IndexOf(FurnitureGrid.ColorName.Silver) >= 0
                    || furniture_grid_[i].color_name().IndexOf(FurnitureGrid.ColorName.Gray) >= 0)
                {
                    ++silver_gray_item;
                }
            }

            if (silver_gray_item > 0)
            {
                plus_luck_[0] += silver_gray_item * 10;

                if (silver_gray_item < 3)
                {
                    //銀 灰の家具をもっと置けば仕事運アップ
                    comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthWestSilverGray, 30 - silver_gray_item * 10, 0));
                    comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthWestSilverGray, 30 - silver_gray_item * 10, 5));
                }
            }

            //北西の金の気が強すぎると仕事運，人気運下がる
            if (elements_[3] > 300)
            {
                minus_luck_[0] = elements_[3] - 300;
                minus_luck_[1] = elements_[3] - 300;

                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthWestVain, elements_[3] - 300, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthWestVain, elements_[3] - 300, 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthWestVain, (elements_[3] - 300) * 2, 5));
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
    partial void CommentMini();
    partial void Comment();
}