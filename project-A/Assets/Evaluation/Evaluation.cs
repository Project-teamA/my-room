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
using System.Linq; //(2018年 2月15日追加)
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
        BedroomWoodNatural, BedroomBlue, BedroomMulti, //寝室関連
        LivingMulti, //リビング関連
        WorkroomMulti, //仕事部屋関連

        NorthWeak, NorthEastWeak, NorthEastMinus, EastWeak, SouthEastWeak, SouthWeak, SouthWestWeak, WestWeak, NorthWestWeak, //方角基本性能
        SplitNorthWeak, SplitNorthEastWeak, SplitNorthEastMinus, SplitEastWeak, SplitSouthEastWeak, SplitSouthWeak, SplitSouthWestWeak, SplitWestWeak, SplitNorthWestWeak, //方角基本性能(小方位)

        NorthCold, NorthPink, //北の運勢
        NorthEastHigh, //北東の運勢
        EastWindSound, //東の運勢
        SouthEastWindSound, SouthEastOrange, //南東の運勢
        SouthPurification, //南の運勢
        SouthWestLow, //南西の運勢
        WestWestern, WestLuxury, //西の運勢
        NorthWestLuxury, NorthWestLuxuryZero, NorthWestSilverGray, NorthWestVain, //北西の運勢



        WhiteResetYinYang, WhitePurification, BlackStrengthening, RedOne, PinkOne, PinkNoOrange, BlueOne, BlueNoOrange, OrangeNoPink, OrangeNoBlue, //色関連
        YellowBrownOne, GreenPurification, BeigeFew, Cream, GoldMulti,  //色関連

        SquareFix, CircleGoodRelation, CircleCirculation, SharpBadRelation, //形状関連
        SweetSmell, Luminescence, FlowerAssociative, //その他特性
        ExcessFurniture, ShortageFurniture,  //家具の数関連

        WoodWeak, FireWeak, EarthWeak, MetalWeak, WaterWeak, WeakEnergy, //気が弱い
        WoodOver, FireOver, EarthOver, MetalOver, WaterOver, //気が強すぎ

        WoodWeakSosho, FireWeakSosho, EarthWeakSosho, MetalWeakSosho, WaterWeakSosho, //相生
        WoodWeakSokoku, FireWeakSokoku, EarthWeakSokoku, MetalWeakSokoku, WaterWeakSokoku,  //相克

        BedLiving, BedWorkroom, BedNoBedroom, BedPillowDirection, BedConnected, BedGapWall, BedToDoor, BedNearWindow, BedOver, //ベッド関連 (2個まで)
        CabinetOver, //タンス関連( 5個まで )
        CarpetOver,//カーペット関連 (2個まで)
        DeskBedroom, DeskNoWorkRoom, DeskNorthEast, DeskSouth, DeskWest, DeskFrontWindow, DeskSeatNearWall, DeskOver, //机関連 (2個まで)
        FoliagePlantOver,//観葉植物関連 (5個まで)
        //天井ランプ関連 
        LampOver,//机ランプ (天井机ランプ合わせて4個まで)
        SofaSplitWest, SofaToTV, SofaToDoor, SofaOver,//ソファー関連 (3個まで)
        TableNoLiving, TableOver,//テーブル関連 (2個まで)
        ElectronicsSouth, ElectronicsNoEast, ElectronicsYang, ElectronicsToWindowDoor, ElectronicsToBed, ElectronicsOver,//家電関連 ( 4個まで )
                                                                                                                         //カーテン関連
        FurnitureFew, FurnitureOver, //家具多すぎと少なすぎ
    };

    //------------------------------------------------------------------------------------------------------------------------------------------------

    //家具の特性リスト(特性の数，ウェイトを数えるために実装 2018年 2月15日)
    public enum CharacteristicIdentifier
    {
        //家具のタイプ
        BedFurniture, DeskFurniture, TableFurniture, SofaFurniture, FoliagePlantFurniture, CarpetFurniture, CurtainFurniture,
        ConsumerElectronicsFurniture, DresserFurniture, CeilLampFurniture, DeskLampFurniture, WindowFurniture, DoorFurniture, CabinetFurniture,

        //カラー
        WhiteColor, BlackColor, GrayColor, DarkGrayColor, RedColor, PinkColor, BlueColor, LightBlueColor, OrangeColor,
        YellowColor, GreenColor, LightGreenColor, BeigeColor, CreamColor, BrownColor, OcherColor, GoldColor, SilverColor, PurpleColor,

        //材質
        ArtificialFoliageMaterial, WoodenMaterial, PaperMaterial, LeatherMaterial, NaturalFibreMaterial, ChemicalFibreMaterial,
        CottonMaterial, PlasticMaterial, CeramicMaterial, MarbleMaterial, MetalMaterial, MineralMaterial, GlassMaterial, WaterMaterial,

        //模様
        StripePattern, LeafPattern, FlowerPattern, StarPattern, DiamondPattern, AnimalPattern, ZigzagPattern, NovelPattern, BorderPattern, CheckPattern, TilePattern,
        DotPattern, RoundPattern, ArchPattern, FruitsPattern, LusterPattern, WavePattern, IrregularityPattern, CloudPattern,

        //形状
        HighForm, LowForm, VerticalForm, OblongForm, SquareForm, RectangleForm, RoundForm, EllipseForm, TriangleForm, SharpForm, NovelForm,

        //その他の特性
        Luxury, Sound, Smell, Light, Hard, Soft, Warm, Cold, Flower, Wind, Western, Clutter
    }

    private int[] elements_ = new int[5] { 0, 0, 0, 0, 0 };  //elements_の各要素について
    private int yin_yang_ = 0; //陰陽(プラスで陽，マイナスで陰)

    private int energy_strength_ = 0; //気の強さ(max1000 min0, ここは確定するように調整)

    //部屋の中の方位ごとの五行
    //[0] = 木, [1] = 火, [2] = 土, [3] = 金, [4] = 水
    // [][0] = 北，[][1] = 北東, [][2] = 東，[][3] = 南東, [][4] = 南, [][5] = 南西, [][6] = 西，[][7] = 北西
    private int[][] split_elements_ = new int[5][];
    private int[] split_yin_yang_ = new int[8];  //部屋の中の方位ごとの陰陽

    //ここから評価用のバッファ(これらはすべて気の変化を保存する)----------------------------------------------------------------------------------------------------
    private int[] sosho_buffer_ = new int[5]; //相生効果によって変化した気の量
    private int[] sokoku_buffer_ = new int[5];  //相克効果によって変化した気の量
    //---------------------------------------------------------------------------------------------------------------------------------


    private int[] luck_ = new int[5] { 0, 0, 0, 0, 0 };  //運気(旺気と邪気を合わせた最終結果)
    private int all_luck_ = 0; //総合運
    private int[] norma_luck_ = new int[5];  //(運気の)ノルマ変数
    private int all_norma_ = 0; //総合運のノルマ
    private int[] plus_luck_ = new int[5]; //運気の変化(プラスの運気成分(旺気))
    private int[] minus_luck_ = new int[5];  //運気の変化(マイナスの運気成分(邪気))

    private Room room_role_; //部屋の種類
    private Direction room_direction_; //部屋の方角

    private List<FurnitureGrid> furniture_grid_ = new List<FurnitureGrid>(); //FurnitureGrid.csで実装されているクラスのリスト(最大50)
    private List<int> ignore_index_ = new List<int>(); //この中に入った家具グリッドは評価から除外される (2018年 2月15日)

    //家具の特性の数のクラス(2018年 2月15日追加, 家具の特性の数，ウェイトの合計数を数えるためのクラス)
    private class CharacteristicNumber
    {
        public CharacteristicIdentifier characteristic_identifier_; //特徴の種類
        public int weight_total_; //特徴のウェイト合計
        public int count_; //特徴を持っている家具の数

        public CharacteristicNumber(CharacteristicIdentifier characteristic_identifier, int weight_total, int count)
        {
            characteristic_identifier_ = characteristic_identifier;
            weight_total_ = weight_total;
        }

        public void WeightPlus(int add_weight)
        {
            weight_total_ += add_weight;
        }

        public void CountPlus(int add_count)
        {
            count_ += add_count;
        }
    }

    private List<CharacteristicNumber> characteristic_number_ = new List<CharacteristicNumber>();

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

    private List<CommentFlag> comment_flag_ = new List<CommentFlag>();
    private List<string> comment_ = new List<string>(); //コメント ( コメントフラグに応じていくつかのコメントを出力 )

    private bool is_finished_game_; //ゲームが終わったかどうかのフラグ (ture = ゲーム終了 false = ゲーム終了せず)
    private int advaice_mode_; //アドバイスモード(0 = 仕事運重視，1 = 人気運重視，2 = 健康運重視，3 = 金運重視，4 = 恋愛運重視, 5 = デフォルト(ノルマ重視))

    private const int limit_elements_ = 500;
    private const int limit_yin_ = -300;
    private const int limit_yang_ = 300;

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

    //陰陽取得用(そういえば実装忘れてましたね…  2018年2月15日実装)
    public int yin_yang()
    {
        return yin_yang_;
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

    //部屋の種類取得用 (そういえば実装忘れてましたね…  2018年2月15日実装)
    public Room room_role()
    {
        return room_role_;
    }

    //部屋の方向取得用 (そういえば実装忘れてましたね…  2018年2月15日実装)
    public Direction room_direction()
    {
        return room_direction_;
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

    //***************************************************************************************************************
    //UI表示

    public Text[] elements_text_ = new Text[5];

    public GameObject Gage_Value_yin;
    public GameObject Gage_Value_yang;
    private int yin_yang_max_ = 300;
    private int yin_yang_min_ = -300;

    public GameObject[] Gage_Value_ = new GameObject[5];
    private int all_luck_min_ = -500;

    public void UpdateElementsText()
    {
        for (int i = 0; i < elements_.Length; i++)
        {
            elements_text_[i].text = elements_[i].ToString();
        }

        Debug.Log("陰陽(前) " + yin_yang_);

        float yin_yang_temp_ = yin_yang_;

        yin_yang_temp_ += (-yin_yang_min_);
        yin_yang_temp_ /= yin_yang_max_ + (-yin_yang_min_);

        Debug.Log("陰陽(後) " + yin_yang_temp_);

        Gage_Value_yin.GetComponent<RectTransform>().localScale = new Vector3(yin_yang_temp_, 1, 1);
        Gage_Value_yang.GetComponent<RectTransform>().localScale = new Vector3(1 - Gage_Value_yin.GetComponent<RectTransform>().localScale.x, 1, 1);

        int count = 0;

        for (int i = 0; i < luck_.Length; i++)
        {
            Debug.Log("運勢" + i + "(前) " + luck_[i]);

            float luck_temp_ = luck_[i];

            luck_temp_ += (-all_luck_min_);
            luck_temp_ /= norma_luck_[i] + (-all_luck_min_);

            Debug.Log("運勢" + i + "(後) " + luck_temp_);

            Gage_Value_[i].GetComponent<RectTransform>().localScale = new Vector3(luck_temp_, 1, 1);

            if (luck_temp_ >= 1)
            {
                count++;
                Gage_Value_[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
            else if (luck_temp_ <= 0)
            {
                Gage_Value_[i].GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
            }
        }

        if (count == luck_.Length)
        {
            GameObject.Find("LevelManager").GetComponent<LevelManager>().FinishGame(true);
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

        //エラーグリッド無視
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            if (furniture_grid_[i].furniture_grid().GetComponent<GridError>().errored())
            {
                ignore_index_.Add(i);
            }
        }

        //特性を数えるため実装(2018年 2月15日実装)
        for (int i = 0; i < furniture_grid.Count; ++i)
        {
            //エラー家具を無視する処理
            if (IsIgnored(i))
            {
                continue;
            }
            CountCharacteristic(furniture_grid_[i]);
        }

        is_finished_game_ = false;

        for (int i = 0; i < 5; ++i)
        {
            split_elements_[i] = new int[8];
        }

        EvaluationTotal();
        UpdateElementsText();
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

    //家具の更新関数
    //furniture_grid = 変更，更新する家具グリッド
    public void UpdateGrid(List<FurnitureGrid> furniture_grid)
    {
        furniture_grid_ = furniture_grid;

        //エラーグリッド無視
        ignore_index_.Clear();
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            if (furniture_grid_[i].furniture_grid().GetComponent<GridError>().errored())
            {
                ignore_index_.Add(i);
            }
        }

        //特性を数えるため実装(2018年 2月15日実装)
        characteristic_number_.Clear();
        for (int i = 0; i < furniture_grid.Count; ++i)
        {
            //エラー家具を無視する処理
            if (IsIgnored(i))
            {
                continue;
            }

            CountCharacteristic(furniture_grid_[i]);
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

    //総合評価関数(評価の一連の流れ)
    public void EvaluationTotal()
    {
        for (int i = 0; i < 5; ++i)
        {
            elements_[i] = 0;
            luck_[i] = 0;
            plus_luck_[i] = 0;
            minus_luck_[i] = 0;

            sosho_buffer_[i] = 0;
            sokoku_buffer_[i] = 0;

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
        comment_.Clear(); //コメントの初期化

        EvaluationItem();
        EvaluationRoom();
        EvaluationDirection();
        EvaluationSoshoSokoku();
        EvaluationFiveElementsMultiply();
        for (int i = 0; i < 8; ++i)
        {
            split_yin_yang_[i] += split_elements_[1][i];
            split_yin_yang_[i] += split_elements_[2][i] / 2;
            split_yin_yang_[i] -= split_elements_[3][i] / 2;
            split_yin_yang_[i] -= split_elements_[4][i];
        }
        EvaluationYinYangMultiply();


        //ここから五行の気の強さ, 陰陽加算
        for (int i = 0; i < 5; ++i)
        {
            elements_[i] += split_elements_[i].Sum();

            if (elements_[i] > limit_elements_)
            {
                energy_strength_ += limit_elements_;
            }
            else
            {
                energy_strength_ += elements_[i];
            }
        }
        yin_yang_ += split_yin_yang_.Sum();
        //ここまで五行の気の強さ, 陰陽加算

        FortuneItem();
        FortuneRoom();
        FortuneDirection();
        FortuneSplitDirection();
        FortuneFiveElement();

        //陰陽による運勢補正
        if (yin_yang_ < limit_yin_)
        {
            minus_luck_[0] -= (yin_yang_ + limit_yin_);
            minus_luck_[1] -= (yin_yang_ + limit_yin_);
            minus_luck_[2] -= (yin_yang_ + limit_yin_);
            minus_luck_[4] -= (yin_yang_ + limit_yin_);

            //陰気が強すぎて, 仕事，人気，健康，金，下がる
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverYin, -(yin_yang_ - limit_yin_), 0));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverYin, -(yin_yang_ - limit_yin_), 1));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverYin, -(yin_yang_ - limit_yin_), 2));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverYin, -(yin_yang_ - limit_yin_), 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverYin, -(yin_yang_ + limit_yin_) * 4, 5));
            Debug.Log("OverYin");
        }
        else if (yin_yang_ > limit_yang_)
        {
            minus_luck_[0] += (yin_yang_ - limit_yang_);
            minus_luck_[2] += (yin_yang_ - limit_yang_);
            minus_luck_[3] += (yin_yang_ - limit_yang_);

            //陽気が強すぎて，仕事，健康，恋愛下がる
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverYang, (yin_yang_ - limit_yang_), 0));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverYang, (yin_yang_ - limit_yang_), 2));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverYang, (yin_yang_ - limit_yang_), 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.OverYang, (yin_yang_ - limit_yang_) * 3, 5));
            Debug.Log("OverYang");
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
    //
    //この関数に重大な欠陥があったので直しました．すみません．
    private void EvaluationItem()
    {
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            //エラー家具を無視する処理
            if (IsIgnored(i))
            {
                continue;
            }

            if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.North)
            {
                split_elements_[0][0] += furniture_grid_[i].elements_wood();
                split_elements_[1][0] += furniture_grid_[i].elements_fire();
                split_elements_[2][0] += furniture_grid_[i].elements_earth();
                split_elements_[3][0] += furniture_grid_[i].elements_metal();
                split_elements_[4][0] += furniture_grid_[i].elements_water();
                split_yin_yang_[0] += furniture_grid_[i].yin_yang();
            }
            else if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.NorthEast)
            {
                split_elements_[0][1] += furniture_grid_[i].elements_wood();
                split_elements_[1][1] += furniture_grid_[i].elements_fire();
                split_elements_[2][1] += furniture_grid_[i].elements_earth();
                split_elements_[3][1] += furniture_grid_[i].elements_metal();
                split_elements_[4][1] += furniture_grid_[i].elements_water();
                split_yin_yang_[1] += furniture_grid_[i].yin_yang();
            }
            else if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.East)
            {
                split_elements_[0][2] += furniture_grid_[i].elements_wood();
                split_elements_[1][2] += furniture_grid_[i].elements_fire();
                split_elements_[2][2] += furniture_grid_[i].elements_earth();
                split_elements_[3][2] += furniture_grid_[i].elements_metal();
                split_elements_[4][2] += furniture_grid_[i].elements_water();
                split_yin_yang_[2] += furniture_grid_[i].yin_yang();
            }
            else if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.SouthEast)
            {
                split_elements_[0][3] += furniture_grid_[i].elements_wood();
                split_elements_[1][3] += furniture_grid_[i].elements_fire();
                split_elements_[2][3] += furniture_grid_[i].elements_earth();
                split_elements_[3][3] += furniture_grid_[i].elements_metal();
                split_elements_[4][3] += furniture_grid_[i].elements_water();
                split_yin_yang_[3] += furniture_grid_[i].yin_yang();
            }
            else if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.South)
            {
                split_elements_[0][4] += furniture_grid_[i].elements_wood();
                split_elements_[1][4] += furniture_grid_[i].elements_fire();
                split_elements_[2][4] += furniture_grid_[i].elements_earth();
                split_elements_[3][4] += furniture_grid_[i].elements_metal();
                split_elements_[4][4] += furniture_grid_[i].elements_water();
                split_yin_yang_[4] += furniture_grid_[i].yin_yang();
            }
            else if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.SouthWest)
            {
                split_elements_[0][5] += furniture_grid_[i].elements_wood();
                split_elements_[1][5] += furniture_grid_[i].elements_fire();
                split_elements_[2][5] += furniture_grid_[i].elements_earth();
                split_elements_[3][5] += furniture_grid_[i].elements_metal();
                split_elements_[4][5] += furniture_grid_[i].elements_water();
                split_yin_yang_[5] += furniture_grid_[i].yin_yang();
            }
            else if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.West)
            {
                split_elements_[0][6] += furniture_grid_[i].elements_wood();
                split_elements_[1][6] += furniture_grid_[i].elements_fire();
                split_elements_[2][6] += furniture_grid_[i].elements_earth();
                split_elements_[3][6] += furniture_grid_[i].elements_metal();
                split_elements_[4][6] += furniture_grid_[i].elements_water();
                split_yin_yang_[6] += furniture_grid_[i].yin_yang();
            }
            else if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.NorthWest)
            {
                split_elements_[0][7] += furniture_grid_[i].elements_wood();
                split_elements_[1][7] += furniture_grid_[i].elements_fire();
                split_elements_[2][7] += furniture_grid_[i].elements_earth();
                split_elements_[3][7] += furniture_grid_[i].elements_metal();
                split_elements_[4][7] += furniture_grid_[i].elements_water();
                split_yin_yang_[7] += furniture_grid_[i].yin_yang();
            }
        }
    }

    //部屋評価関数(基本五行と陰陽のみ)
    private void EvaluationRoom()
    {
        if (room_role_ == Room.Entrance)
        {

        }
        else if (room_role_ == Room.Bedroom)
        {

        }
        else if (room_role_ == Room.Living)
        {
            //土の気をもつ
            for (int i = 0; i < 8; ++i)
            {
                split_elements_[2][i] += 12;
            }
        }
        else if (room_role_ == Room.Kitchen)
        {
            //火と水の気，弱い金の気をもつ
            for (int i = 0; i < 8; ++i)
            {
                split_elements_[1][i] += 20;
                split_elements_[3][i] += 8;
                split_elements_[4][i] += 20;
            }
        }
        else if (room_role_ == Room.Workroom)
        {
            //木の気をもつ
            for (int i = 0; i < 8; ++i)
            {
                split_elements_[0][i] += 12;
            }
        }
        else if (room_role_ == Room.Bathroom)
        {
            //強い水の気をもつ
            for (int i = 0; i < 8; ++i)
            {
                split_elements_[4][i] += 20;
                split_yin_yang_[i] += 16;
            }

        }
        else if (room_role_ == Room.Toilet)
        {
            //強い水の気をもつ
            for (int i = 0; i < 8; ++i)
            {
                split_elements_[4][i] += 20;
                split_yin_yang_[i] += 20;
            }
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
                split_elements_[4][i] += 25;
            }
        }
        else if (room_direction_ == Direction.NorthEast)
        {
            //北東は土の気が強い(山)
            for (int i = 0; i < 8; ++i)
            {
                split_elements_[2][i] += 25;
            }
        }
        else if (room_direction_ == Direction.East)
        {
            //東は木の気が強い(若木)
            for (int i = 0; i < 8; ++i)
            {
                split_elements_[0][i] += 25;
            }
        }
        else if (room_direction_ == Direction.SouthEast)
        {
            //南東は木の気が強い(大木)
            for (int i = 0; i < 8; ++i)
            {
                split_elements_[0][i] += 25;
            }
        }
        else if (room_direction_ == Direction.South)
        {
            //南は火の気が強い
            for (int i = 0; i < 8; ++i)
            {
                split_elements_[1][i] += 25;
            }
        }
        else if (room_direction_ == Direction.SouthWest)
        {
            //南西は土の気が強い
            for (int i = 0; i < 8; ++i)
            {
                split_elements_[2][i] += 25;
            }
        }
        else if (room_direction_ == Direction.West)
        {
            //西は金の気が強い
            for (int i = 0; i < 8; ++i)
            {
                split_elements_[3][i] += 25;
            }
        }
        else if (room_direction_ == Direction.NorthWest)
        {
            //北西は金の気が強い
            for (int i = 0; i < 8; ++i)
            {
                split_elements_[3][i] += 25;
            }
        }
        else
        {
            Debug.Log("そのような方位は存在しない");
        }
    }

    //五行評価関数(内部的に陰陽も変化)
    private void EvaluationSoshoSokoku()
    {
        //ここから相生の処理
        int[][] sosho_stock = new int[5][];
        for (int i = 0; i < 5; ++i)
        {
            sosho_stock[i] = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        for (int j = 0; j < 8; ++j)
        {
            for (int i = 0; i < 5; ++i)
            {
                //同じ方位
                if (split_elements_[i][j] / 2 <= split_elements_[(i + 1) % 5][j])
                {
                    sosho_stock[(i + 1) % 5][j] += split_elements_[i][j] / 2;
                    sosho_stock[i][j] -= split_elements_[i][j] / 4;
                }
                else
                {
                    sosho_stock[(i + 1) % 5][j] += split_elements_[(i + 1) % 5][j];
                    sosho_stock[i][j] -= split_elements_[(i + 1) % 5][j] / 2;
                }

                //時計隣
                if (split_elements_[i][j] / 4 <= split_elements_[(i + 1) % 5][(j + 1) % 8])
                {
                    sosho_stock[(i + 1) % 5][(j + 1) % 8] += split_elements_[i][j] / 4;
                    sosho_stock[i][j] -= split_elements_[i][j] / 8;
                }
                else
                {
                    sosho_stock[(i + 1) % 5][(j + 1) % 8] += split_elements_[(i + 1) % 5][(j + 1) % 8];
                    sosho_stock[i][j] -= split_elements_[(i + 1) % 5][(j + 1) % 8] / 2;
                }

                //反時計隣
                if (split_elements_[i][j] / 4 <= split_elements_[(i + 1) % 5][(j + 7) % 8])
                {
                    sosho_stock[(i + 1) % 5][(j + 7) % 8] += split_elements_[i][j] / 4;
                    sosho_stock[i][j] -= split_elements_[i][j] / 8;
                }
                else
                {
                    sosho_stock[(i + 1) % 5][(j + 7) % 8] += split_elements_[(i + 1) % 5][(j + 7) % 8];
                    sosho_stock[i][j] -= split_elements_[(i + 1) % 5][(j + 7) % 8] / 2;
                }
            }
        }

        for (int j = 0; j < 8; ++j)
        {
            for (int i = 0; i < 5; ++i)
            {
                if ((split_elements_[i][j] + sosho_stock[i][j]) < 0)
                {
                    split_elements_[i][j] = 0;
                    sosho_buffer_[i] -= split_elements_[i][j];
                }
                else
                {
                    split_elements_[i][j] += sosho_stock[i][j];
                    sosho_buffer_[i] += sosho_stock[i][j];
                }
            }
        }

        //ここから相克の処理
        int[][] sokoku_stock = new int[5][];
        for (int i = 0; i < 5; ++i)
        {
            sokoku_stock[i] = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        for (int j = 0; j < 8; ++j)
        {
            for (int i = 0; i < 5; ++i)
            {
                //同じ方位
                if (split_elements_[i][j] / 2 <= split_elements_[(i + 2) % 5][j])
                {
                    sokoku_stock[(i + 2) % 5][j] -= split_elements_[i][j] / 2;
                    sokoku_stock[i][j] -= split_elements_[i][j] / 4;
                }
                else
                {
                    sokoku_stock[(i + 2) % 5][j] -= split_elements_[(i + 2) % 5][j];
                    sokoku_stock[i][j] -= split_elements_[(i + 2) % 5][j] / 2;
                }

                //時計隣
                if (split_elements_[i][j] / 4 <= split_elements_[(i + 2) % 5][(j + 1) % 8])
                {
                    sokoku_stock[(i + 2) % 5][(j + 1) % 8] -= split_elements_[i][j] / 4;
                    sokoku_stock[i][j] -= split_elements_[i][j] / 8;
                }
                else
                {
                    sokoku_stock[(i + 2) % 5][(j + 1) % 8] -= split_elements_[(i + 2) % 5][(j + 1) % 8];
                    sokoku_stock[i][j] -= split_elements_[(i + 2) % 5][(j + 1) % 8] / 2;
                }

                //反時計隣
                if (split_elements_[i][j] / 4 <= split_elements_[(i + 2) % 5][(j + 7) % 8])
                {
                    sokoku_stock[(i + 2) % 5][(j + 7) % 8] -= split_elements_[i][j] / 4;
                    sokoku_stock[i][j] -= split_elements_[i][j] / 8;
                }
                else
                {
                    sokoku_stock[(i + 2) % 5][(j + 7) % 8] -= split_elements_[(i + 2) % 5][(j + 7) % 8];
                    sokoku_stock[i][j] -= split_elements_[(i + 2) % 5][(j + 7) % 8] / 2;
                }

            }

        }


        for (int j = 0; j < 8; ++j)
        {
            for (int i = 0; i < 5; ++i)
            {
                if ((split_elements_[i][j] + sokoku_stock[i][j]) < 0)
                {
                    split_elements_[i][j] = 0;
                    sokoku_buffer_[i] -= split_elements_[i][j];
                }
                else
                {
                    split_elements_[i][j] += sokoku_stock[i][j];
                    sokoku_buffer_[i] += sokoku_stock[i][j];
                }
            }
        }

    }

    //五行の掛け算補正(最初に五行，後に陰陽)(方角などはこちらに移動)
    private void EvaluationFiveElementsMultiply()
    {
        int[][] split_elements_stock = new int[5][];
        for (int i = 0; i < 5; ++i)
        {
            split_elements_stock[i] = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        }


        for (int i = 0; i < 5; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                split_elements_[i][j] += split_elements_stock[i][j];
                if (split_elements_[i][j] < 0)
                {
                    split_elements_[i][j] = 0;
                }

            }
        }
    }

    //陰陽の掛け算補正
    private void EvaluationYinYangMultiply()
    {
        int[] split_yin_yang_stock = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 }; //陰陽の気変化量

        //観葉植物による陰陽緩和
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            //エラー家具を無視する処理
            if (IsIgnored(i))
            {
                continue;
            }

            if (furniture_grid_[i].furniture_subtype().IndexOf(FurnitureGrid.FurnitureType.FoliagePlant) >= 0
                || furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.FoliagePlant)
            {
                if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.North)
                {
                    split_yin_yang_stock[0] += split_yin_yang_[0] / 5;
                }
                else if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.NorthEast)
                {
                    split_yin_yang_stock[1] += split_yin_yang_[1] / 5;
                }
                else if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.East)
                {
                    split_yin_yang_stock[2] += split_yin_yang_[2] / 5;
                }
                else if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.SouthEast)
                {
                    split_yin_yang_stock[3] += split_yin_yang_[3] / 5;
                }
                else if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.South)
                {
                    split_yin_yang_stock[4] += split_yin_yang_[4] / 5;
                }
                else if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.SouthWest)
                {
                    split_yin_yang_stock[5] += split_yin_yang_[5] / 5;
                }
                else if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.West)
                {
                    split_yin_yang_stock[6] += split_yin_yang_[6] / 5;
                }
                else if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.NorthWest)
                {
                    split_yin_yang_stock[7] += split_yin_yang_[7] / 5;
                }
            }
        }

        for (int j = 0; j < 8; ++j)
        {
            split_yin_yang_[j] += split_yin_yang_stock[j];
        }
    }

    //**************************************************************************************************************************************************************************************************

    //アイテムによる運勢補正
    private void FortuneItem()
    {
        int[] color_luck_plus_stock = new int[5] { 0, 0, 0, 0, 0 };
        int[] color_luck_minus_stock = new int[5] { 0, 0, 0, 0, 0 };

        //赤い家具1つでも置くと仕事，人気，健康，恋愛アップ
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            //エラー家具を無視する処理
            if (IsIgnored(i))
            {
                continue;
            }

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
                Debug.Log("RedOne");
            }
        }

        //ピンクの家具1つでも置くと恋愛アップ
        bool pink_at_least = false;
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            //エラー家具を無視する処理
            if (IsIgnored(i))
            {
                continue;
            }

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
                Debug.Log("PinkOne");
            }
        }

        //青の家具1つでも置くと仕事運アップ
        bool blue_at_least = false;
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            //エラー家具を無視する処理
            if (IsIgnored(i))
            {
                continue;
            }

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
                Debug.Log("BlueOne");
            }
        }

        //オレンジの家具一つでも置くと…
        bool orange_at_least = false;
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            //エラー家具を無視する処理
            if (IsIgnored(i))
            {
                continue;
            }

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
                Debug.Log("OrangeNoPink");
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
                Debug.Log("OrangeNoBlue");
            }
        }
        else if (pink_at_least)
        {
            //ピンクがあるのでオレンジと合わせましょう
            comment_flag_.Add(new CommentFlag(CommentIdentifier.PinkNoOrange, 30, 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.PinkNoOrange, 30, 5));
            Debug.Log("PinkNoOrange");
        }
        else if (blue_at_least)
        {
            //青があるのでオレンジと合わせましょう
            comment_flag_.Add(new CommentFlag(CommentIdentifier.BlueNoOrange, 30, 0));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.BlueNoOrange, 30, 5));
            Debug.Log("BlueNoOrange");
        }

        //ベージュの家具1つにつき仕事運，恋愛運アップ
        int beige_item = 0;
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            //エラー家具を無視する処理
            if (IsIgnored(i))
            {
                continue;
            }

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
            comment_flag_.Add(new CommentFlag(CommentIdentifier.BeigeFew, 30 - beige_item * 10, 0));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.BeigeFew, 30 - beige_item * 10, 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.BeigeFew, 60 - beige_item * 20, 5));
            Debug.Log("BeigeFew");
        }

        //黒が1つでもあればカラーによる運気補正がさらに高まる(実装がかなり面倒)

        for (int i = 0; i < 5; ++i)
        {
            plus_luck_[i] += color_luck_plus_stock[i];
            minus_luck_[i] += color_luck_minus_stock[i];
        }



        //家具の特性による評価

        //ベッド関連
        int bed_item = 0; //ベッドの数
        int[] bed_direction_south = new int[2] { 0, 0 }; //ベッドが南枕 仕事運, 健康運
        int[] bed_direction_west = new int[2] { 0, 0 }; //ベッドが西枕 仕事運, 健康運
        int bed_connected = 0; //ベッド同士がつながっている 恋愛運
        int bed_gap_wall = 0; //健康運下がる
        int bed_to_door = 0; //健康運下がる
        int bed_near_window = 0; //健康運下がる
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            //エラー家具を無視する処理
            if (IsIgnored(i))
            {
                continue;
            }

            if ((furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.Bed)
                || (furniture_grid_[i].furniture_subtype().IndexOf(FurnitureGrid.FurnitureType.Bed) >= 0))
            {
                RaycastHit hit;

                //ベッドの枕の位置(安眠できるかどうかで，健康運，美容運にかかわる)
                if (furniture_grid_[i].up_direction() == Vector3.up)
                {
                    //ベッドが北枕だと安眠(仕事運，健康運が上がる)
                    plus_luck_[0] += 30;
                    plus_luck_[2] += 60;
                }
                else if (furniture_grid_[i].up_direction() == Vector3.down)
                {
                    //ベッドが南枕だと仕事運，健康運下がる
                    minus_luck_[0] += 40;
                    minus_luck_[2] += 60;

                    bed_direction_south[0] += 70;
                    bed_direction_south[1] += 90;
                }
                else if (furniture_grid_[i].up_direction() == Vector3.right)
                {
                    //ベッドが東枕だと(仕事運，健康運上がる)
                    plus_luck_[0] += 40;
                    plus_luck_[2] += 30;
                }
                else if (furniture_grid_[i].up_direction() == Vector3.left)
                {
                    //ベッドが西枕だと(仕事運，健康運下がる)
                    minus_luck_[0] += 20;
                    minus_luck_[2] += 30;

                    bed_direction_west[0] += 50;
                    bed_direction_west[1] += 60;
                }

                ////(シングル)ベッドをつなげるとダメ
                //for (int j = 0; j < furniture_grid_.Count; ++j)
                //{
                //    //ベッド
                //    if (furniture_grid_[i] != furniture_grid_[j])
                //    {
                //        if (furniture_grid_[j].furniture_type() == FurnitureGrid.FurnitureType.Bed)
                //        {
                //            Vector3 left_down_source = furniture_grid_[i].vertices((int)furniture_grid_[i].parameta(2)) + furniture_grid_[i].grid_position();
                //            Vector3 left_up_source = furniture_grid_[i].vertices((int)furniture_grid_[i].parameta(3)) + furniture_grid_[i].grid_position();
                //            Vector3 right_down_source = furniture_grid_[i].vertices((int)furniture_grid_[i].parameta(4)) + furniture_grid_[i].grid_position();
                //            Vector3 right_up_source = furniture_grid_[i].vertices((int)furniture_grid_[i].parameta(5)) + furniture_grid_[i].grid_position();

                //            Vector3 left_down_target = furniture_grid_[j].vertices((int)furniture_grid_[j].parameta(2)) + furniture_grid_[j].grid_position();
                //            Vector3 left_up_target = furniture_grid_[j].vertices((int)furniture_grid_[j].parameta(3)) + furniture_grid_[j].grid_position();
                //            Vector3 right_down_target = furniture_grid_[j].vertices((int)furniture_grid_[j].parameta(4)) + furniture_grid_[j].grid_position();
                //            Vector3 right_up_target = furniture_grid_[j].vertices((int)furniture_grid_[j].parameta(5)) + furniture_grid_[j].grid_position();

                //            if (((left_down_source == right_down_target) &&
                //                (left_up_source == right_up_target)) ||
                //                ((right_down_source == left_down_target) &&
                //                (right_up_source == left_up_target)))
                //            {
                //                //つながっていると恋愛運下がる
                //                minus_luck_[4] += 25;
                //                bed_connected += 25;
                //                Debug.Log("つながっている");
                //            }
                //        }
                //    }
                //}

                //ベッドと壁の隙間ダメ
                bool left_down = false;
                bool left_up = false;
                bool right_down = false;
                bool right_up = false;

                //for (int l = 0; l < furniture_grid_[i].vertices_number(); l++)
                //{
                //    for (int j = Grid_Manager_.Grid_y_min; j < Grid_Manager_.Grid_y_max; j++)
                //    {
                //        for (int k = Grid_Manager_.Grid_x_min; k < Grid_Manager_.Grid_x_max; k++)
                //        {
                //            if (Grid_Manager_.point(k, j).wall == true)
                //            {
                //                float verticesx = furniture_grid_[i].vertices(l).x + furniture_grid_[i].grid_position().x;
                //                float verticesy = furniture_grid_[i].vertices(l).y + furniture_grid_[i].grid_position().y;

                //                float grid_x_min = Grid_Manager_.point(k, j).pos.x - (Grid_Manager_.Grid_density / 2.0f);
                //                float grid_y_min = Grid_Manager_.point(k, j).pos.y - (Grid_Manager_.Grid_density / 2.0f);
                //                float grid_x_max = Grid_Manager_.point(k, j).pos.x + (Grid_Manager_.Grid_density / 2.0f);
                //                float grid_y_max = Grid_Manager_.point(k, j).pos.y + (Grid_Manager_.Grid_density / 2.0f);

                //                if (grid_x_min < verticesx && verticesx < grid_x_max &&
                //                    grid_y_min < verticesy && verticesy < grid_y_max)
                //                {
                //                    if (furniture_grid_[i].vertices(l) == furniture_grid_[i].vertices(furniture_grid_[i].parameta(2)))
                //                    {
                //                        left_down = true;
                //                    }
                //                    else if (furniture_grid_[i].vertices(l) == furniture_grid_[i].vertices(furniture_grid_[i].parameta(3)))
                //                    {
                //                        left_up = true;
                //                    }
                //                    else if (furniture_grid_[i].vertices(l) == furniture_grid_[i].vertices(furniture_grid_[i].parameta(4)))
                //                    {
                //                        right_down = true;
                //                    }
                //                    else if (furniture_grid_[i].vertices(l) == furniture_grid_[i].vertices(furniture_grid_[i].parameta(5)))
                //                    {
                //                        right_up = true;
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}

                //if ((left_down == true && left_up == true) || (right_down == true && right_up == true))
                //{
                //    //隙間なし
                //    Debug.Log("隙間なし");
                //}
                //else
                //{
                //    //隙間ありだと健康運下がる
                //    minus_luck_[2] += 40;
                //    bed_connected += 40;
                //    Debug.Log("隙間あり");
                //}

                //ドアが正面            
                for (int j = 0; j < furniture_grid_.Count; ++j)
                {
                    //エラー家具を無視する処理
                    if (IsIgnored(i))
                    {
                        continue;
                    }

                    //ドア
                    if (furniture_grid_[j].furniture_type() == FurnitureGrid.FurnitureType.Door)
                    {
                        if (Physics.Raycast(furniture_grid_[i].furniture_grid().transform.position, furniture_grid_[i].up_direction(), out hit))
                        {
                            if (hit.collider.gameObject == furniture_grid_[j].furniture_grid())
                            {
                                //健康運下がる
                                minus_luck_[2] += 40;
                                bed_to_door += 40;
                            }
                        }
                    }
                }

                //窓の近くにベッドダメ
                for (int j = 0; j < furniture_grid_.Count; ++j)
                {
                    //エラー家具を無視する処理
                    if (IsIgnored(i))
                    {
                        continue;
                    }

                    //窓
                    if (furniture_grid_[j].furniture_type() == FurnitureGrid.FurnitureType.Window)
                    {
                        //近く
                        if (2.0f > Vector3.Distance(furniture_grid_[i].furniture_grid().transform.position, furniture_grid_[j].furniture_grid().transform.position))
                        {
                            //健康運下がる
                            minus_luck_[2] += 40;
                            bed_near_window += 40;
                        }

                    }
                }

                if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.Bed)
                {
                    ++bed_item;
                }
            }
        }

        if (bed_item != 0)
        {
            if (room_role_ == Room.Living)
            {
                //リビングにベッドは置くな(仕事，人気, 健康ダウン)
                minus_luck_[0] += 100;
                minus_luck_[1] += 100;
                minus_luck_[2] += 100;
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedLiving, 100, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedLiving, 100, 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedLiving, 100, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedLiving, 300, 5));
                Debug.Log("BedLiving");
            }
            else if (room_role_ == Room.Workroom)
            {
                //仕事部屋にベッドは置くな(仕事，健康大幅ダウン)
                minus_luck_[0] += 150;
                minus_luck_[2] += 150;
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedWorkroom, 150, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedWorkroom, 150, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedWorkroom, 300, 5));
                Debug.Log("BedWorkroom");
            }

            //南向きのベッド，西向きのベッドが一つでもある場合
            if ((bed_direction_south[0] > 0) || (bed_direction_west[0] > 0))
            {
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedPillowDirection, bed_direction_south[0] + bed_direction_west[0], 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedPillowDirection, bed_direction_south[1] + bed_direction_west[1], 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedPillowDirection, bed_direction_south.Sum() + bed_direction_west.Sum(), 5));
                Debug.Log("BedPillowDirection");
            }

            //ベッド同士がくっついている場合(恋愛運下がる)
            if (bed_connected > 0)
            {
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedConnected, bed_connected, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedConnected, bed_connected, 5));
                Debug.Log("BedConnected");
            }

            //ベッドと壁の間に隙間ある場合
            if (bed_gap_wall > 0)
            {
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedGapWall, bed_gap_wall, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedGapWall, bed_gap_wall, 5));
                Debug.Log("BedGapWall");
            }

            //ドアがベッドに向けている場合
            if (bed_to_door > 0)
            {
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedToDoor, bed_to_door, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedToDoor, bed_to_door, 5));
                Debug.Log("BedToDoor");
            }

            //窓がベッドの近くの場合
            if (bed_near_window > 0)
            {
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedNearWindow, bed_near_window, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedNearWindow, bed_near_window, 5));
                Debug.Log("BedNearWindow");
            }

            //ベッドが多すぎる．
            if (bed_item > 2)
            {
                OverFurniture(bed_item, 2, 100, 100, 100, 100, 100, CommentIdentifier.BedOver, "BedOver");
            }
        }
        else
        {
            if (room_role_ == Room.Bedroom)
            {
                //寝室にベッド置かないのは論外(健康運中心に大幅下がる)
                minus_luck_[0] += 50;
                minus_luck_[1] += 50;
                minus_luck_[2] += 300;
                minus_luck_[3] += 50;
                minus_luck_[4] += 50;
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedNoBedroom, 50, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedNoBedroom, 50, 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedNoBedroom, 300, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedNoBedroom, 50, 3));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedNoBedroom, 50, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedNoBedroom, 500, 5));
                Debug.Log("BedNoBedroom");
            }
        }



        //タンス関連
        int cabinet_item = 0; //タンスの数
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            //エラー家具を無視する処理
            if (IsIgnored(i))
            {
                continue;
            }

            if ((furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.Cabinet)
                || (furniture_grid_[i].furniture_subtype().IndexOf(FurnitureGrid.FurnitureType.Cabinet) >= 0))
            {
                if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.Cabinet)
                {
                    ++cabinet_item;
                }
            }
        }

        if (cabinet_item != 0)
        {
            //タンス置きすぎ
            if (cabinet_item > 5)
            {
                OverFurniture(cabinet_item, 5, 100, 100, 100, 100, 100, CommentIdentifier.CabinetOver, "CabinetOver");
            }
        }
        else
        {

        }



        //カーペット関連
        int carpet_item = 0; //カーペットの数
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            //エラー家具を無視する処理
            if (IsIgnored(i))
            {
                continue;
            }

            if ((furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.Carpet)
                || (furniture_grid_[i].furniture_subtype().IndexOf(FurnitureGrid.FurnitureType.Carpet) >= 0))
            {
                if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.Carpet)
                {
                    ++carpet_item;
                }
            }

        }

        if (carpet_item != 0)
        {
            //机置きすぎ
            if (carpet_item > 2)
            {
                OverFurniture(carpet_item, 2, 100, 100, 100, 100, 100, CommentIdentifier.CarpetOver, "CarpetOver");
            }
        }
        else
        {

        }




        //机関連
        int desk_item = 0; //机の数
        int desk_north_east = 0; //仕事運上げる
        int desk_south = 0; //人気運上げる
        int desk_west = 0; //金運上げる
        int desk_front_window = 0; //仕事運下げる
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            //エラー家具を無視する処理
            if (IsIgnored(i))
            {
                continue;
            }

            if ((furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.Desk)
                || (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.Desk))
            {

                //北向き, または東向き
                if ((furniture_grid_[i].up_direction() == Vector3.up)
                    || (furniture_grid_[i].up_direction() == Vector3.right))
                {
                    //仕事運アップ
                    plus_luck_[0] += 50;
                    desk_south += 50;
                    desk_west += 50;
                }
                //西向き
                else if (furniture_grid_[i].up_direction() == Vector3.left)
                {
                    //金運アップ
                    plus_luck_[3] += 50;
                    desk_north_east += 50;
                    desk_south += 50;
                }
                //南向き
                else if (furniture_grid_[i].up_direction() == Vector3.down)
                {
                    //人気運アップ
                    plus_luck_[1] += 50;
                    desk_north_east += 50;
                    desk_west += 50;
                }

                //窓の正面
                RaycastHit hit;
                for (int j = 0; j < furniture_grid_.Count; ++j)
                {
                    //エラー家具を無視する処理
                    if (IsIgnored(j))
                    {
                        continue;
                    }

                    //窓
                    if (furniture_grid_[j].furniture_type() == FurnitureGrid.FurnitureType.Window)
                    {
                        if (Physics.Raycast(furniture_grid_[i].furniture_grid().transform.position, furniture_grid_[i].up_direction(), out hit))
                        {
                            if (hit.collider.gameObject == furniture_grid_[j].furniture_grid())
                            {
                                //机の座席が窓に対面すると仕事運下がる
                                minus_luck_[0] += 30;
                                desk_front_window += 30;
                            }
                        }
                    }
                }

                if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.Desk)
                {
                    ++desk_item;
                }

            }
        }

        if (desk_item != 0)
        {
            if (room_role_ == Room.Bedroom)
            {
                //寝室に机は置くな(仕事，健康運ダウン)
                minus_luck_[0] += 150;
                minus_luck_[2] += 150;
                comment_flag_.Add(new CommentFlag(CommentIdentifier.DeskBedroom, 150, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.DeskBedroom, 150, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.DeskBedroom, 300, 5));
                Debug.Log("DeskBedroom");
            }

            //机が北, または東向きでない
            if (desk_north_east > 0)
            {
                comment_flag_.Add(new CommentFlag(CommentIdentifier.DeskNorthEast, desk_north_east, 0));
                Debug.Log("DeskNorthEast");
            }

            //机が南向きでない
            if (desk_south > 0)
            {
                comment_flag_.Add(new CommentFlag(CommentIdentifier.DeskSouth, desk_south, 1));
                Debug.Log("DeskSouth");
            }

            //机が西向きでない
            if (desk_west > 0)
            {
                comment_flag_.Add(new CommentFlag(CommentIdentifier.DeskWest, desk_west, 3));
                Debug.Log("DeskWest");
            }

            //窓と机が正面衝突
            if (desk_front_window > 0)
            {
                comment_flag_.Add(new CommentFlag(CommentIdentifier.DeskFrontWindow, desk_front_window, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.DeskFrontWindow, desk_front_window, 5));
                Debug.Log("DeskFrontWindow");
            }

            //机置きすぎ
            if (desk_item > 2)
            {
                OverFurniture(desk_item, 2, 100, 100, 100, 100, 100, CommentIdentifier.DeskOver, "DeskOver");
            }
        }
        else
        {
            if (room_role_ == Room.Workroom)
            {
                //仕事部屋に机置かないのは論外(仕事運に大幅下がる)
                minus_luck_[0] += 300;
                comment_flag_.Add(new CommentFlag(CommentIdentifier.DeskNoWorkRoom, 300, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.DeskNoWorkRoom, 300, 5));
                Debug.Log("DeskNoWorkRoom");
            }
        }



        //観葉植物関連
        int foliage_item = 0; //観葉植物関連
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            //エラー家具を無視する処理
            if (IsIgnored(i))
            {
                continue;
            }

            if ((furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.FoliagePlant)
                || (furniture_grid_[i].furniture_subtype().IndexOf(FurnitureGrid.FurnitureType.FoliagePlant) >= 0))
            {
                if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.FoliagePlant)
                {
                    ++foliage_item;
                }
            }

        }

        if (foliage_item != 0)
        {
            //観葉植物置きすぎ
            if (foliage_item > 5)
            {
                OverFurniture(foliage_item, 5, 100, 100, 100, 100, 100, CommentIdentifier.FoliagePlantOver, "FoliagePlantOver");
            }
        }
        else
        {

        }



        //ランプ関連
        int lamp_item = 0; //ランプの数
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            //エラー家具を無視する処理
            if (IsIgnored(i))
            {
                continue;
            }

            if ((((furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.CeilLamp)
                || (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.DeskLamp))
                || (furniture_grid_[i].furniture_subtype().IndexOf(FurnitureGrid.FurnitureType.CeilLamp) >= 0))
                || furniture_grid_[i].furniture_subtype().IndexOf(FurnitureGrid.FurnitureType.DeskLamp) >= 0)
            {
                if ((furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.CeilLamp)
               || (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.DeskLamp))
                {
                    ++lamp_item;
                }

            }
        }

        if (foliage_item != 0)
        {
            //ランプ置きすぎ
            if (lamp_item > 4)
            {
                OverFurniture(lamp_item, 4, 100, 100, 100, 100, 100, CommentIdentifier.LampOver, "LampOver");
            }
        }
        else
        {

        }


        //ソファー関連
        int sofa_item = 0; //ソファーの数
        int[] sofa_split_west = new int[3] { 0, 0, 0 }; //仕事運，人気運，健康運上がる
        int sofa_to_TV = 0; //健康運上がる
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            //エラー家具を無視する処理
            if (IsIgnored(i))
            {
                continue;
            }

            if ((furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.Sofa)
                || (furniture_grid_[i].furniture_subtype().IndexOf(FurnitureGrid.FurnitureType.Sofa) >= 0))
            {
                RaycastHit hit;

                //西に配置
                if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.West)
                {
                    //東向き
                    if (furniture_grid_[i].up_direction() == new Vector3(-1, 0, 0))
                    {
                        //西にソファーをおき，座席を東に向けると仕事運，人気運，健康運アップ
                        plus_luck_[0] += 30;
                        plus_luck_[1] += 30;
                        plus_luck_[2] += 30;

                        for (int j = 0; j < furniture_grid_.Count; ++j)
                        {
                            //エラー家具を無視する処理
                            if (IsIgnored(i))
                            {
                                continue;
                            }

                            //テレビ
                            if ((furniture_grid_[j].furniture_type() == FurnitureGrid.FurnitureType.ConsumerElectronics) && (furniture_grid_[j].parameta(0) == 2))
                            {
                                //西向き
                                if (furniture_grid_[j].up_direction() == new Vector3(1, 0, 0))
                                {
                                    if (Physics.Raycast(furniture_grid_[i].furniture_grid().transform.position, furniture_grid_[i].up_direction(), out hit))
                                    {
                                        if (hit.collider.gameObject == furniture_grid_[j].furniture_grid())
                                        {
                                            //ソファーに対してテレビが東側にあれば仕事運アップ
                                            plus_luck_[0] += 50;

                                        }
                                        else
                                        {
                                            //ソファーに対してテレビが東側以外
                                            sofa_to_TV += 50;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //ソファー西で東に向く以外
                        sofa_split_west[0] += 30;
                        sofa_split_west[1] += 30;
                        sofa_split_west[2] += 30;
                    }
                }
                ++sofa_item;
            }
        }

        if (sofa_item != 0)
        {
            //西以外のソファーが一つでもある場合
            if (sofa_split_west[0] > 0)
            {
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SofaSplitWest, sofa_split_west[0], 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SofaSplitWest, sofa_split_west[1], 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SofaSplitWest, sofa_split_west[2], 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SofaSplitWest, sofa_split_west.Sum(), 5));
                Debug.Log("SofaSplitWest");
            }

            //西のソファーと東のテレビが向かい合っていない場合
            if (sofa_to_TV > 0)
            {
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SofaSplitWest, sofa_to_TV, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SofaSplitWest, sofa_to_TV, 5));
                Debug.Log("SofaSplitWest");
            }

            //ソファ置きすぎ
            if (sofa_item > 3)
            {
                OverFurniture(sofa_item, 3, 100, 100, 100, 100, 100, CommentIdentifier.SofaOver, "SofaOver");
            }

        }
        else
        {
        }


        //テーブル関連
        int table_item = 0; //テーブルの数
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            //エラー家具を無視する処理
            if (IsIgnored(i))
            {
                continue;
            }

            if ((furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.Table)
                || (furniture_grid_[i].furniture_subtype().IndexOf(FurnitureGrid.FurnitureType.Table) >= 0))
            {

                if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.Table)
                {
                    ++table_item;
                }
            }

        }

        if (table_item != 0)
        {
            //テーブル置きすぎ
            if (table_item > 2)
            {
                OverFurniture(table_item, 2, 100, 100, 100, 100, 100, CommentIdentifier.TableOver, "TableOver");
            }
        }
        else
        {

        }

        //家電関連
        int electronics_item = 0;
        int[] electronics_south = new int[3] { 0, 0, 0 };
        int[] electronics_no_east = new int[3] { 0, 0, 0 };
        int electronics_yang = 0;
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            //エラー家具を無視する処理
            if (IsIgnored(i))
            {
                continue;
            }

            if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.ConsumerElectronics)
            {
                //南に配置
                if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.South)
                {
                    //仕事運 健康運 恋愛運下がる
                    minus_luck_[0] += 30;
                    minus_luck_[2] += 30;
                    minus_luck_[4] += 30;

                    electronics_south[0] += 30;
                    electronics_south[1] += 30;
                    electronics_south[2] += 30;
                }
                //東に配置
                else if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.East)
                {
                    //仕事運 人気運 健康運あがる
                    plus_luck_[0] += 60;
                    plus_luck_[1] += 30;
                    plus_luck_[2] += 30;
                }
                else
                {
                    electronics_no_east[0] += 60;
                    electronics_no_east[1] += 30;
                    electronics_no_east[2] += 30;
                }

                //陽気がある程度強いと
                if (yin_yang_ > limit_yang_)
                {
                    minus_luck_[2] += 60;

                    //健康運下がる
                    electronics_yang += 60;
                }

                ++electronics_item;
            }
        }

        if (electronics_item != 0)
        {
            //南に配置されている場合
            if (electronics_south[0] > 0)
            {
                comment_flag_.Add(new CommentFlag(CommentIdentifier.ElectronicsSouth, electronics_south[0], 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.ElectronicsSouth, electronics_south[1], 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.ElectronicsSouth, electronics_south[2], 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.ElectronicsSouth, electronics_south.Sum(), 5));
                Debug.Log("ElectronicsSouth");
            }

            //東に配置されている場合
            if (electronics_no_east[0] > 0)
            {
                comment_flag_.Add(new CommentFlag(CommentIdentifier.ElectronicsNoEast, electronics_no_east[0], 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.ElectronicsNoEast, electronics_no_east[1], 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.ElectronicsNoEast, electronics_no_east[2], 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.ElectronicsNoEast, electronics_no_east.Sum(), 5));
                Debug.Log("ElectronicsNoEast");
            }

            //陽気が強い場合
            if (electronics_yang > 0)
            {
                comment_flag_.Add(new CommentFlag(CommentIdentifier.ElectronicsYang, electronics_yang, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.ElectronicsYang, electronics_yang, 5));
                Debug.Log("ElectronicsYang");
            }

            if (electronics_item > 4)
            {
                OverFurniture(electronics_item, 4, 100, 100, 100, 100, 100, CommentIdentifier.ElectronicsOver, "ElectronicsOver");
            }

        }
        else
        {

        }


        //鏡関連
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            //エラー家具を無視する処理
            if (IsIgnored(i))
            {
                continue;
            }

            if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.Dresser)
            {
                //RaycastHit hit;

                ////南に配置
                //if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.South)
                //{
                //    luck_[1] += 10;
                //    luck_[4] += 5;
                //    luck_[2] += 5;
                //}

                ////丸い鏡
                //if (furniture_grid_[i].form_type()[0] == FurnitureGrid.FormType.Round)
                //{
                //    //全ての運気が1.2倍
                //    luck_[2] = (int)(luck_[2] * 1.2f);
                //    luck_[3] = (int)(luck_[3] * 1.2f);
                //    luck_[4] = (int)(luck_[4] * 1.2f);
                //    luck_[0] = (int)(luck_[0] * 1.2f);
                //    luck_[1] = (int)(luck_[1] * 1.2f);

                //    //リビング
                //    if (room_role_ == Room.Living)
                //    {
                //        //北の方角に置く
                //        if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.North)
                //        {
                //            //全体的に運気が上がる（特に金運）
                //        }
                //    }
                //}
                ////四角い鏡
                //if (furniture_grid_[i].form_type()[0] == FurnitureGrid.FormType.Square)
                //{
                //    //全ての運気が0.8倍
                //    luck_[2] = (int)(luck_[2] * 0.8f);
                //    luck_[3] = (int)(luck_[3] * 0.8f);
                //    luck_[4] = (int)(luck_[4] * 0.8f);
                //    luck_[0] = (int)(luck_[0] * 0.8f);
                //    luck_[1] = (int)(luck_[1] * 0.8f);

                //    //北の方角に置く
                //    if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.North)
                //    {
                //        //ダメ
                //    }

                //    //北東の方角に置く
                //    if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.NorthEast)
                //    {
                //        //良い
                //    }

                //    //窓の正面
                //    for (int j = 0; j < furniture_grid_.Count; ++j)
                //    {
                //        //窓
                //        if (furniture_grid_[j].furniture_type() == FurnitureGrid.FurnitureType.Window)
                //        {
                //            if (Physics.Raycast(furniture_grid_[i].furniture_grid().transform.position, furniture_grid_[i].up_direction(), out hit))
                //            {
                //                if (hit.collider.gameObject == furniture_grid_[j].furniture_grid())
                //                {
                //                    //ダメ
                //                }
                //            }
                //        }
                //    }
                //}
                ////長方形の鏡
                //if (furniture_grid_[i].form_type()[0] == FurnitureGrid.FormType.Rectangle)
                //{
                //    //東の方角に置く
                //    if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.East)
                //    {
                //        //良い
                //    }
                //    //南東の方角に置く
                //    if (furniture_grid_[i].placed_direction() == FurnitureGrid.PlacedDirection.SouthEast)
                //    {
                //        //良い
                //    }
                //}
                ////
                //for (int j = 0; j < furniture_grid_.Count; ++j)
                //{
                //    //ベッドが正面(写っている)
                //    if (furniture_grid_[j].furniture_type() == FurnitureGrid.FurnitureType.bed)
                //    {
                //        if (Physics.Raycast(furniture_grid_[i].furniture_grid().transform.position, furniture_grid_[i].up_direction(), out hit))
                //        {
                //            if (hit.collider.gameObject == furniture_grid_[j].furniture_grid())
                //            {
                //                luck_[2] -= 20;
                //                luck_[4] -= 20;
                //                luck_[0] -= 5;
                //                luck_[1] -= 5;
                //                luck_[3] -= 5;
                //            }
                //        }
                //    }
                //    //鏡が正面(合わせ鏡)
                //    if (furniture_grid_[j].furniture_type() == FurnitureGrid.FurnitureType.dresser)
                //    {
                //        if (Physics.Raycast(furniture_grid_[i].furniture_grid().transform.position, furniture_grid_[i].up_direction(), out hit))
                //        {
                //            if (hit.collider.gameObject == furniture_grid_[j].furniture_grid())
                //            {
                //                luck_[2] -= 40;
                //                luck_[4] -= 40;
                //                luck_[0] -= 40;
                //                luck_[1] -= 40;
                //                luck_[3] -= 60;
                //            }
                //        }
                //    }
                //}

            }
        }

        if ((furniture_grid_.Count - ignore_index_.Count) < 3)
        {
            //家具少なすぎ
            OverFurniture(3, (furniture_grid_.Count - ignore_index_.Count), 100, 100, 100, 100, 100, CommentIdentifier.FurnitureFew, "FurnitureFew");
        }
        else if ((furniture_grid_.Count - ignore_index_.Count) > 10)
        {
            //家具多すぎ
            OverFurniture((furniture_grid_.Count - ignore_index_.Count), 10, 100, 100, 100, 100, 100, CommentIdentifier.FurnitureOver, "FurnitureOver");
        }

    }


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
            //木材家具, 天然素材1個につき健康運アップ(両方の特性を持っている場合は1個にカウント)
            int wood_natural_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                //エラー家具を無視する処理
                if (IsIgnored(i))
                {
                    continue;
                }

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
                Debug.Log("BedroomWoodNatural");
            }

            //青い家具一個につき安眠で健康運アップ
            int blue_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                //エラー家具を無視する処理
                if (IsIgnored(i))
                {
                    continue;
                }

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
                Debug.Log("BedroomBlue");
            }

        }
        else if (room_role_ == Room.Living)
        {

        }
        else if (room_role_ == Room.Kitchen)
        {

        }
        else if (room_role_ == Room.Workroom)
        {

        }
        else if (room_role_ == Room.Bathroom)
        {

        }
        else if (room_role_ == Room.Toilet)
        {

        }
    }


    //部屋の方位による運勢補正
    private void FortuneDirection()
    {
        if (room_direction_ == Direction.North)
        {
            //北は水の気でパワーアップ
            int direction_power;
            if (elements_[4] <= limit_elements_)
            {
                direction_power = (energy_strength_ / 5 + elements_[4] - 200) / 2;
                if (direction_power < 0)
                {
                    direction_power = 0;
                }
            }
            else
            {
                direction_power = (energy_strength_ / 5 + limit_elements_ - 200) / 2;
            }

            plus_luck_[3] += direction_power / 2;
            plus_luck_[4] += direction_power / 2;

            if (direction_power < 400)
            {
                //北の部屋のパワー弱すぎて金運，恋愛運が上がらない
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthWeak, (400 - direction_power) / 4, 3));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthWeak, (400 - direction_power) / 4, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthWeak, (400 - direction_power) / 2, 5));
                Debug.Log("WeakNorth");
            }

            //元々健康運，人気運に悪影響
            minus_luck_[1] += 50;
            minus_luck_[2] += 50;

            //温かみのある家具一つにつき健康運, 人気運アップ
            int warm_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                //エラー家具を無視する処理
                if (IsIgnored(i))
                {
                    continue;
                }

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
                Debug.Log("NorthCold");
            }

            //ピンクの家具一つにつき恋愛運がアップ
            int pink_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                //エラー家具を無視する処理
                if (IsIgnored(i))
                {
                    continue;
                }

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
                Debug.Log("NorthPink");
            }

        }
        else if (room_direction_ == Direction.NorthEast)
        {
            //北東は木の気でパワーアップ
            int direction_power;
            if (elements_[2] <= limit_elements_)
            {
                direction_power = (energy_strength_ / 5 + elements_[2] - 200) / 2;
                if (direction_power < 0)
                {
                    direction_power = 0;
                }
            }
            else
            {
                direction_power = (energy_strength_ / 5 + limit_elements_ - 200) / 2;
            }

            if ((yin_yang_ >= limit_yin_) && (yin_yang_ <= limit_yang_))
            {
                plus_luck_[0] += direction_power / 5;
                plus_luck_[1] += direction_power / 5;
                plus_luck_[2] += direction_power / 5;
                plus_luck_[3] += direction_power / 5;
                plus_luck_[4] += direction_power / 5;

                if (direction_power < 400)
                {
                    //北東の部屋のパワー弱すぎて全ての運気が上がらない
                    comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthEastWeak, (400 - direction_power) / 10, 0));
                    comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthEastWeak, (400 - direction_power) / 10, 1));
                    comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthEastWeak, (400 - direction_power) / 10, 2));
                    comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthEastWeak, (400 - direction_power) / 10, 3));
                    comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthEastWeak, (400 - direction_power) / 10, 4));
                    comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthEastWeak, (400 - direction_power) / 2, 5));
                    Debug.Log("WeakNorthEast");
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
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthEastMinus, direction_power / 10, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthEastMinus, direction_power / 10, 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthEastMinus, direction_power / 10, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthEastMinus, direction_power / 10, 3));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthEastMinus, direction_power / 10, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthEastMinus, direction_power / 2, 5));
                Debug.Log("MinusNorthEast");
            }

            //背の高い家具一つにつき運気アップ
            int high_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                //エラー家具を無視する処理
                if (IsIgnored(i))
                {
                    continue;
                }

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
                Debug.Log("NorthEastHigh");
            }

        }
        else if (room_direction_ == Direction.East)
        {
            //東は木の気でパワーアップ
            int direction_power;
            if (elements_[0] <= limit_elements_)
            {
                direction_power = (energy_strength_ / 5 + elements_[0] - 200) / 2;
                if (direction_power < 0)
                {
                    direction_power = 0;
                }
            }
            else
            {
                direction_power = (energy_strength_ / 5 + limit_elements_ - 200) / 2;
            }

            plus_luck_[0] += direction_power;

            if (direction_power < 400)
            {
                //東の部屋のパワー弱すぎて仕事運が上がらない
                comment_flag_.Add(new CommentFlag(CommentIdentifier.EastWeak, (400 - direction_power) / 2, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.EastWeak, (400 - direction_power) / 2, 5));
                Debug.Log("WeakEast");
            }

            //風関連，または音の出る家具で人気運，恋愛運アップ
            int wind_sound_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                //エラー家具を無視する処理
                if (IsIgnored(i))
                {
                    continue;
                }

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
                Debug.Log("EastWindSound");
            }

        }
        else if (room_direction_ == Direction.SouthEast)
        {
            //南東は木の気でパワーアップ
            int direction_power;
            if (elements_[0] <= limit_elements_)
            {
                direction_power = (energy_strength_ / 5 + elements_[0] - 200) / 2;
                if (direction_power < 0)
                {
                    direction_power = 0;
                }
            }
            else
            {
                direction_power = (energy_strength_ / 5 + limit_elements_ - 200) / 2;
            }

            plus_luck_[1] += direction_power * 2 / 5;
            plus_luck_[4] += direction_power * 3 / 5;

            if (direction_power < 400)
            {
                //南東の部屋のパワー弱すぎて人気運，恋愛運が上がらない
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SouthEastWeak, (400 - direction_power) / 5, 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SouthEastWeak, (400 - direction_power) * 3 / 10, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SouthEastWeak, (400 - direction_power) / 2, 5));
                Debug.Log("WeakSouthEast");
            }

            //風関連，または音の出る家具で人気運，恋愛運アップ
            int wind_sound_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                //エラー家具を無視する処理
                if (IsIgnored(i))
                {
                    continue;
                }

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
                Debug.Log("SouthEastWindSound");
            }

            //オレンジの家具で人気運，恋愛運アップ
            int orange_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                //エラー家具を無視する処理
                if (IsIgnored(i))
                {
                    continue;
                }

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
                Debug.Log("SouthEastOrange");
            }

        }
        else if (room_direction_ == Direction.South)
        {
            //南は火の気でパワーアップ
            int direction_power;
            if (elements_[1] <= limit_elements_)
            {
                direction_power = (energy_strength_ / 5 + elements_[1] - 200) / 2;
                if (direction_power < 0)
                {
                    direction_power = 0;
                }
            }
            else
            {
                direction_power = (energy_strength_ / 5 + limit_elements_ - 300) / 2;
            }

            plus_luck_[1] += direction_power * 3 / 5;
            plus_luck_[2] += direction_power / 5;
            plus_luck_[4] += direction_power / 5;

            if (direction_power < 400)
            {
                //南の部屋のパワー弱すぎて人気運，健康運，恋愛運が上がらない
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SouthWeak, (400 - direction_power) * 3 / 10, 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SouthWeak, (400 - direction_power) / 10, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SouthWeak, (400 - direction_power) / 10, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SouthWeak, (400 - direction_power) / 2, 5));
                Debug.Log("WeakSouth");
            }
        }
        else if (room_direction_ == Direction.SouthWest)
        {
            //南西は土の気でパワーアップ
            int direction_power = energy_strength_ / 5;
            if (elements_[2] <= limit_elements_)
            {
                direction_power = (energy_strength_ / 5 + elements_[2] - 200) / 2;
                if (direction_power < 0)
                {
                    direction_power = 0;
                }
            }
            else
            {
                direction_power = (energy_strength_ / 5 + limit_elements_ - 200) / 2;
            }

            plus_luck_[0] += direction_power / 3;
            plus_luck_[1] += direction_power / 3;
            plus_luck_[2] += direction_power / 3;

            if (direction_power < 400)
            {
                //南西の部屋のパワー弱すぎて仕事運，人気運，健康運が上がらない
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SouthWestWeak, (400 - direction_power) / 6, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SouthWestWeak, (400 - direction_power) / 6, 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SouthWestWeak, (400 - direction_power) / 6, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SouthWestWeak, (400 - direction_power) / 2, 5));
                Debug.Log("WeakSouthWest");
            }

            //背の低い家具一つにつき運気アップ
            int low_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                //エラー家具を無視する処理
                if (IsIgnored(i))
                {
                    continue;
                }

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
                Debug.Log("SouthWestLow");
            }

        }
        else if (room_direction_ == Direction.West)
        {
            //西は金の気でパワーアップ
            int direction_power;
            if (elements_[3] <= limit_elements_)
            {
                direction_power = (energy_strength_ / 5 + elements_[3] - 200) / 2;
                if (direction_power < 0)
                {
                    direction_power = 0;
                }
            }
            else
            {
                direction_power = (energy_strength_ / 5 + limit_elements_ - 200) / 2;
            }

            plus_luck_[3] += direction_power / 2;
            plus_luck_[4] += direction_power / 2;

            if (direction_power < 400)
            {
                //西の部屋のパワー弱すぎて金運，恋愛運が上がらない
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WestWeak, (400 - direction_power) / 4, 3));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WestWeak, (400 - direction_power) / 4, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.WestWeak, (400 - direction_power) / 2, 5));
                Debug.Log("WeakWest");
            }

            //西洋風家具一つにつき金運アップ
            int western_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                //エラー家具を無視する処理
                if (IsIgnored(i))
                {
                    continue;
                }

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
                Debug.Log("WestWestern");
            }

            //高級家具一つにつき金運アップ
            int luxury_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                //エラー家具を無視する処理
                if (IsIgnored(i))
                {
                    continue;
                }

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
                Debug.Log("WestLuxury");
            }

        }
        else if (room_direction_ == Direction.NorthWest)
        {
            //北西は金の気でパワーアップ
            int direction_power;
            if (elements_[3] <= limit_elements_)
            {
                direction_power = (energy_strength_ / 5 + elements_[3] - 200) / 2;
                if (direction_power < 0)
                {
                    direction_power = 0;
                }
            }
            else
            {
                direction_power = (energy_strength_ / 5 + limit_elements_ - 200) / 2;
            }

            plus_luck_[0] += direction_power / 2;
            plus_luck_[3] += direction_power / 2;

            if (direction_power < 400)
            {
                //北西の部屋のパワー弱すぎて仕事運，金運が上がらない
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthWestWeak, (400 - direction_power) / 4, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthWestWeak, (400 - direction_power) / 4, 3));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthWestWeak, (400 - direction_power) / 2, 5));
                Debug.Log("WeakNorthWest");
            }

            //高級家具一つにつき仕事運，金運アップ
            int luxury_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                //エラー家具を無視する処理
                if (IsIgnored(i))
                {
                    continue;
                }

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
                    Debug.Log("NorthWestLuxury");
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
                Debug.Log("NorthWestLuxuryZero");
            }

            //銀 灰の家具一つにつき仕事運アップ
            int silver_gray_item = 0;
            for (int i = 0; i < furniture_grid_.Count; ++i)
            {
                //エラー家具を無視する処理
                if (IsIgnored(i))
                {
                    continue;
                }

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
                    Debug.Log("NorthWestSilverGray");
                }
            }

            //北西の金の気が強すぎると仕事運，人気運下がる
            if (elements_[3] > limit_elements_)
            {
                minus_luck_[0] = elements_[3] - limit_elements_;
                minus_luck_[1] = elements_[3] - limit_elements_;

                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthWestVain, elements_[3] - limit_elements_, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthWestVain, elements_[3] - limit_elements_, 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.NorthWestVain, (elements_[3] - limit_elements_) * 2, 5));
                Debug.Log("NorthWestVain");
            }
        }
        else
        {
            Debug.Log("そのような方位は存在しない");
        }
    }


    //部屋の小方位による運勢補正(部屋の中の方位)
    private void FortuneSplitDirection()
    {
        int direction_power;

        //北は水の気でパワーアップ
        if (split_elements_[4][0] <= 125)
        {
            direction_power = split_elements_[4][0] - 25;
            if (direction_power < 0)
            {
                direction_power = 0;
            }
        }
        else
        {
            direction_power = 100;
        }

        plus_luck_[3] += direction_power / 2;
        plus_luck_[4] += direction_power / 2;

        if (direction_power < 100)
        {
            //北の部屋のパワー弱すぎて金運，恋愛運が上がらない
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitNorthWeak, (100 - direction_power) / 2, 3));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitNorthWeak, (100 - direction_power) / 2, 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitNorthWeak, (100 - direction_power), 5));
            Debug.Log("SpritNorthWeak");
        }

        //北東は土の気でパワーアップ
        if (split_elements_[2][1] <= 125)
        {
            direction_power = split_elements_[2][1] - 25;
            if (direction_power < 0)
            {
                direction_power = 0;
            }
        }
        else
        {
            direction_power = 100;
        }

        if (yin_yang_ >= limit_yin_ && yin_yang_ <= limit_yang_)
        {
            plus_luck_[0] += direction_power / 5;
            plus_luck_[1] += direction_power / 5;
            plus_luck_[2] += direction_power / 5;
            plus_luck_[3] += direction_power / 5;
            plus_luck_[4] += direction_power / 5;

            if (direction_power < 500)
            {
                //北東の部屋のパワー弱すぎて全ての運気が上がらない
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitNorthEastWeak, (100 - direction_power) / 5, 0));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitNorthEastWeak, (100 - direction_power) / 5, 1));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitNorthEastWeak, (100 - direction_power) / 5, 2));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitNorthEastWeak, (100 - direction_power) / 5, 3));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitNorthEastWeak, (100 - direction_power) / 5, 4));
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitNorthEastWeak, 100 - direction_power, 5));
                Debug.Log("SplitNorthEastWeak");
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
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitNorthEastMinus, direction_power / 10, 0));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitNorthEastMinus, direction_power / 10, 1));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitNorthEastMinus, direction_power / 10, 2));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitNorthEastMinus, direction_power / 10, 3));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitNorthEastMinus, direction_power / 10, 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitNorthEastMinus, direction_power / 2, 5));
            Debug.Log("SplitNorthEastMinus");
        }

        //東は木の気でパワーアップ
        if (split_elements_[0][2] <= 125)
        {
            direction_power = split_elements_[0][2] - 25;
            if (direction_power < 0)
            {
                direction_power = 0;
            }
        }
        else
        {
            direction_power = 100;
        }

        plus_luck_[0] += direction_power;

        if (direction_power < 100)
        {
            //東の部屋のパワー弱すぎて仕事運が上がらない
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitEastWeak, 100 - direction_power, 0));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitEastWeak, 100 - direction_power, 5));
            Debug.Log("SplitEastWeak");
        }

        //南東は木の気でパワーアップ
        if (split_elements_[0][3] <= 125)
        {
            direction_power = split_elements_[0][3] - 25;
            if (direction_power < 0)
            {
                direction_power = 0;
            }
        }
        else
        {
            direction_power = 100;
        }

        plus_luck_[1] += direction_power * 2 / 5;
        plus_luck_[4] += direction_power * 3 / 5;

        if (direction_power < 100)
        {
            //南東の部屋のパワー弱すぎて人気運，恋愛運が上がらない
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitSouthEastWeak, (100 - direction_power) * 2 / 5, 1));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitSouthEastWeak, (100 - direction_power) * 3 / 5, 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitSouthEastWeak, 100 - direction_power, 5));
            Debug.Log("SplitSouthEastWeak");
        }

        //南は火の気でパワーアップ
        if (split_elements_[1][4] <= 125)
        {
            direction_power = split_elements_[1][4] - 25;
            if (direction_power < 0)
            {
                direction_power = 0;
            }
        }
        else
        {
            direction_power = 100;
        }

        plus_luck_[1] += direction_power * 3 / 5;
        plus_luck_[2] += direction_power / 5;
        plus_luck_[4] += direction_power / 5;

        if (direction_power < 100)
        {
            //南の部屋のパワー弱すぎて人気運，健康運，恋愛運が上がらない
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitSouthWeak, (100 - direction_power) * 3 / 5, 1));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitSouthWeak, (100 - direction_power) / 5, 2));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitSouthWeak, (100 - direction_power) / 5, 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitSouthWeak, 100 - direction_power, 5));
            Debug.Log("SplitSouthWeak");
        }

        //南西は土の気でパワーアップ
        if (split_elements_[2][5] <= 125)
        {
            direction_power = split_elements_[2][5] - 25;
            if (direction_power < 0)
            {
                direction_power = 0;
            }
        }
        else
        {
            direction_power = 100;
        }

        plus_luck_[0] += direction_power / 3;
        plus_luck_[1] += direction_power / 3;
        plus_luck_[2] += direction_power / 3;

        if (direction_power < 100)
        {
            //南西の部屋のパワー弱すぎて仕事運，人気運，健康運が上がらない
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitSouthWestWeak, (100 - direction_power) / 3, 0));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitSouthWestWeak, (100 - direction_power) / 3, 1));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitSouthWestWeak, (100 - direction_power) / 3, 2));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitSouthWestWeak, 100 - direction_power, 5));
            Debug.Log("SplitSouthWest");
        }

        //西は金の気でパワーアップ
        if (split_elements_[3][6] <= 125)
        {
            direction_power = split_elements_[3][6] - 25;
            if (direction_power < 0)
            {
                direction_power = 0;
            }
        }
        else
        {
            direction_power = 100;
        }

        plus_luck_[3] += direction_power / 2;
        plus_luck_[4] += direction_power / 2;

        if (direction_power < 100)
        {
            //西の部屋のパワー弱すぎて金運，恋愛運が上がらない
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitWestWeak, (100 - direction_power) / 2, 3));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitWestWeak, (100 - direction_power) / 2, 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitWestWeak, 100 - direction_power, 5));
            Debug.Log("SplitWestWeak");
        }

        //北西は金の気でパワーアップ
        if (split_elements_[3][7] <= 125)
        {
            direction_power = split_elements_[3][7] - 25;
            if (direction_power < 0)
            {
                direction_power = 0;
            }
        }
        else
        {
            direction_power = 100;
        }

        plus_luck_[0] += direction_power / 2;
        plus_luck_[3] += direction_power / 2;

        if (direction_power < 100)
        {
            //北西の部屋のパワー弱すぎて仕事運，金運が上がらない
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitNorthWestWeak, (100 - direction_power) / 2, 0));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitNorthWestWeak, (100 - direction_power) / 2, 3));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.SplitNorthWestWeak, 100 - direction_power, 5));
            Debug.Log("SplitNorthWeatWeak");
        }

    }

    //五行による補正
    private void FortuneFiveElement()
    {
        int lost_buffer;
        //木の気の影響
        if (elements_[0] <= limit_elements_)
        {
            plus_luck_[0] += elements_[0] * 4 / 5;
            plus_luck_[2] += elements_[0] / 5;


            //木の気が弱すぎて仕事運と健康運が上がらない
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WoodWeak, (limit_elements_ - elements_[0]) * 2 / 5, 0));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WoodWeak, (limit_elements_ - elements_[0]) / 10, 2));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WoodWeak, (limit_elements_ - elements_[0]) / 2, 5));
            Debug.Log("WeakWood");

            //相生によるコメント
            if ((limit_elements_ - elements_[0]) > -sosho_buffer_[0])
            {
                lost_buffer = -sosho_buffer_[0];
            }
            else
            {
                lost_buffer = limit_elements_ - elements_[0];
            }
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WoodWeakSosho, lost_buffer * 4 / 5, 0));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WoodWeakSosho, lost_buffer / 5, 2));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WoodWeakSosho, lost_buffer, 5));
            Debug.Log("WoodWeakSosho");

            //相克によるコメント
            if ((limit_elements_ - elements_[0]) > -sokoku_buffer_[0])
            {
                lost_buffer = -sokoku_buffer_[0];
            }
            else
            {
                lost_buffer = limit_elements_ - elements_[0];
            }
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WoodWeakSokoku, lost_buffer * 4 / 5, 0));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WoodWeakSokoku, lost_buffer / 5, 2));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WoodWeakSokoku, lost_buffer, 5));
            Debug.Log("WoodWeakSokoku");
        }
        else
        {
            plus_luck_[0] += limit_elements_ * 4 / 5;
            plus_luck_[2] += limit_elements_ / 5;

            //木の気が強すぎて仕事運に悪影響
            minus_luck_[0] += (elements_[0] - limit_elements_);
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WoodOver, elements_[0] - limit_elements_, 0));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WoodOver, elements_[0] - limit_elements_, 5));
            Debug.Log("WoodOver");
        }



        //火の気の影響
        if (elements_[1] <= limit_elements_)
        {
            plus_luck_[1] += elements_[1] * 3 / 5;
            plus_luck_[2] += elements_[1] / 5;
            plus_luck_[4] += elements_[1] / 5;

            //火の気が弱すぎて人気運，健康運，恋愛運が上がらない
            comment_flag_.Add(new CommentFlag(CommentIdentifier.FireWeak, (limit_elements_ - elements_[1]) * 3 / 10, 1));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.FireWeak, (limit_elements_ - elements_[1]) / 10, 2));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.FireWeak, (limit_elements_ - elements_[1]) / 10, 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.FireWeak, (limit_elements_ - elements_[1]) / 2, 5));
            Debug.Log("FireWeak");


            //相生によるコメント
            if ((limit_elements_ - elements_[1]) > -sosho_buffer_[1])
            {
                lost_buffer = -sosho_buffer_[1];
            }
            else
            {
                lost_buffer = limit_elements_ - elements_[1];
            }
            comment_flag_.Add(new CommentFlag(CommentIdentifier.FireWeakSosho, lost_buffer * 3 / 5, 1));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.FireWeakSosho, lost_buffer / 5, 2));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.FireWeakSosho, lost_buffer / 5, 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.FireWeakSosho, lost_buffer, 5));
            Debug.Log("FireWeakSosho");

            //相克によるコメント
            if ((limit_elements_ - elements_[1]) > -sokoku_buffer_[1])
            {
                lost_buffer = -sokoku_buffer_[1];
            }
            else
            {
                lost_buffer = limit_elements_ - elements_[1];
            }
            comment_flag_.Add(new CommentFlag(CommentIdentifier.FireWeakSokoku, lost_buffer * 3 / 5, 1));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.FireWeakSokoku, lost_buffer / 5, 2));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.FireWeakSokoku, lost_buffer / 5, 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.FireWeakSokoku, lost_buffer, 5));
            Debug.Log("FireWeakSokoku");

        }
        else
        {
            plus_luck_[1] += limit_elements_ * 3 / 5;
            plus_luck_[2] += limit_elements_ / 5;
            plus_luck_[4] += limit_elements_ / 5;

            //火の気が強すぎて仕事運，健康運，恋愛運に悪影響
            minus_luck_[0] += (elements_[1] - limit_elements_) / 3;
            minus_luck_[2] += (elements_[1] - limit_elements_) / 3;
            minus_luck_[4] += (elements_[1] - limit_elements_) / 3;
            comment_flag_.Add(new CommentFlag(CommentIdentifier.FireOver, (elements_[1] - limit_elements_) / 3, 0));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.FireOver, (elements_[1] - limit_elements_) / 3, 2));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.FireOver, (elements_[1] - limit_elements_) / 3, 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.FireOver, elements_[1] - limit_elements_, 5));
            Debug.Log("FireOver");
        }

        //土の気の影響
        if (elements_[2] <= limit_elements_)
        {
            plus_luck_[0] += elements_[0] / 5;
            plus_luck_[1] += elements_[1] / 5;
            plus_luck_[2] += elements_[2] / 5;
            plus_luck_[3] += elements_[3] / 5;
            plus_luck_[4] += elements_[4] / 5;

            //土の気が弱すぎて仕事運，人気運，健康運，金運，恋愛運が上がらない
            comment_flag_.Add(new CommentFlag(CommentIdentifier.EarthWeak, (limit_elements_ - elements_[2]) / 10, 0));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.EarthWeak, (limit_elements_ - elements_[2]) / 10, 1));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.EarthWeak, (limit_elements_ - elements_[2]) / 10, 2));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.EarthWeak, (limit_elements_ - elements_[2]) / 10, 3));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.EarthWeak, (limit_elements_ - elements_[2]) / 10, 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.EarthWeak, (limit_elements_ - elements_[2]) / 2, 5));
            Debug.Log("EarthWeak");

            //相生によるコメント
            if ((limit_elements_ - elements_[2]) > -sosho_buffer_[2])
            {
                lost_buffer = -sosho_buffer_[2];
            }
            else
            {
                lost_buffer = limit_elements_ - elements_[2];
            }
            comment_flag_.Add(new CommentFlag(CommentIdentifier.EarthWeakSosho, lost_buffer/ 5, 0));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.EarthWeakSosho, lost_buffer / 5, 1));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.EarthWeakSosho, lost_buffer / 5, 2));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.EarthWeakSosho, lost_buffer / 5, 3));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.EarthWeakSosho, lost_buffer / 5, 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.EarthWeakSosho, lost_buffer, 5));
            Debug.Log("EarthWeakSosho");

            //相克によるコメント
            if ((limit_elements_ - elements_[2]) > -sokoku_buffer_[2])
            {
                lost_buffer = -sokoku_buffer_[2];
            }
            else
            {
                lost_buffer = limit_elements_ - elements_[2];
            }
            comment_flag_.Add(new CommentFlag(CommentIdentifier.EarthWeakSokoku, lost_buffer / 5, 0));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.EarthWeakSokoku, lost_buffer / 5, 1));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.EarthWeakSokoku, lost_buffer / 5, 2));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.EarthWeakSokoku, lost_buffer / 5, 3));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.EarthWeakSokoku, lost_buffer / 5, 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.EarthWeakSokoku, lost_buffer, 5));
            Debug.Log("EarthWeakSokoku");

        }
        else
        {
            plus_luck_[0] += limit_elements_ / 5;
            plus_luck_[1] += limit_elements_ / 5;
            plus_luck_[2] += limit_elements_ / 5;
            plus_luck_[3] += limit_elements_ / 5;
            plus_luck_[4] += limit_elements_ / 5;

            //土の気が強すぎて健康運に悪影響
            minus_luck_[2] += (elements_[2] - limit_elements_);
            comment_flag_.Add(new CommentFlag(CommentIdentifier.EarthOver, elements_[2] - limit_elements_, 2));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.EarthOver, elements_[2] - limit_elements_, 5));
            Debug.Log("EarthOver");
        }

        //金の気の影響
        if (elements_[3] <= limit_elements_)
        {
            plus_luck_[3] += elements_[3];

            //金の気が弱すぎて金運が上がらない
            comment_flag_.Add(new CommentFlag(CommentIdentifier.MetalWeak, (limit_elements_ - elements_[3]) / 2, 3));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.MetalWeak, (limit_elements_ - elements_[3]) / 2, 5));
            Debug.Log("MetalWeak");

            //相生によるコメント
            if ((limit_elements_ - elements_[3]) > -sosho_buffer_[3])
            {
                lost_buffer = -sosho_buffer_[3];
            }
            else
            {
                lost_buffer = limit_elements_ - elements_[3];
            }
            comment_flag_.Add(new CommentFlag(CommentIdentifier.MetalWeakSosho, lost_buffer, 3));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.MetalWeakSosho, lost_buffer, 5));
            Debug.Log("MetalWeakSosho");

            //相克によるコメント
            if ((limit_elements_ - elements_[3]) > -sokoku_buffer_[3])
            {
                lost_buffer = -sokoku_buffer_[3];
            }
            else
            {
                lost_buffer = limit_elements_ - elements_[3];
            }
            comment_flag_.Add(new CommentFlag(CommentIdentifier.MetalWeakSokoku, lost_buffer, 3));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.MetalWeakSokoku, lost_buffer, 5));
            Debug.Log("MetalWeakSokoku");

        }
        else
        {
            plus_luck_[3] += limit_elements_;

            //金の気が強すぎて金運に悪影響
            minus_luck_[3] += (elements_[3] - limit_elements_);
            comment_flag_.Add(new CommentFlag(CommentIdentifier.MetalOver, elements_[3] - limit_elements_, 3));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.MetalOver, elements_[3] - limit_elements_, 5));
            Debug.Log("MetalOver");
        }

        //水の気の影響
        if (elements_[4] <= limit_elements_)
        {
            plus_luck_[0] += elements_[4] / 5;
            plus_luck_[3] += elements_[4] / 5;
            plus_luck_[4] += elements_[4] * 3 / 5;

            //水の気が弱すぎて仕事運，金運，恋愛運が上がらない
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WaterWeak, (limit_elements_ - elements_[4]) / 10, 0));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WaterWeak, (limit_elements_ - elements_[4]) / 10, 3));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WaterWeak, (limit_elements_ - elements_[4]) * 3 / 10, 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WaterWeak, (limit_elements_ - elements_[4]) / 2, 5));
            Debug.Log("WaterWeak");

            //相生によるコメント
            if ((limit_elements_ - elements_[4]) > -sosho_buffer_[4])
            {
                lost_buffer = -sosho_buffer_[4];
            }
            else
            {
                lost_buffer = limit_elements_ - elements_[4];
            }
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WaterWeakSosho, lost_buffer / 5, 0));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WaterWeakSosho, lost_buffer / 5, 3));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WaterWeakSosho, lost_buffer * 3 / 5, 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WaterWeakSosho, lost_buffer, 5));
            Debug.Log("WaterWeakSosho");

            //相克によるコメント
            if ((limit_elements_ - elements_[4]) > -sokoku_buffer_[4])
            {
                lost_buffer = -sokoku_buffer_[4];
            }
            else
            {
                lost_buffer = limit_elements_ - elements_[4];
            }
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WaterWeakSokoku, lost_buffer / 5, 0));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WaterWeakSokoku, lost_buffer / 5, 3));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WaterWeakSokoku, lost_buffer * 3 / 5, 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WaterWeakSokoku, lost_buffer, 5));
            Debug.Log("WaterWeakSokoku");

        }
        else
        {
            plus_luck_[0] += limit_elements_ / 5;
            plus_luck_[3] += limit_elements_ / 5;
            plus_luck_[4] += limit_elements_ * 3 / 5;

            //水の気が強すぎて健康運，金運, 恋愛運に悪影響
            minus_luck_[2] += (elements_[4] - limit_elements_) / 5;
            minus_luck_[3] += (elements_[4] - limit_elements_) * 2 / 5;
            minus_luck_[4] += (elements_[4] - limit_elements_) * 2 / 5;
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WaterOver, (elements_[4] - limit_elements_) / 5, 2));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WaterOver, (elements_[4] - limit_elements_) * 2 / 5, 3));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WaterOver, (elements_[4] - limit_elements_) * 2 / 5, 4));
            comment_flag_.Add(new CommentFlag(CommentIdentifier.WaterOver, elements_[4] - limit_elements_, 5));
            Debug.Log("OverWater");
        }
    }


    //仕上げの運勢補正(鏡による運勢増減など)
    private void FortuneLast()
    {
        int[] displacement_plus_stock = new int[5] { 0, 0, 0, 0, 0 };

        int[] displacement_minus_stock = new int[5] { 0, 0, 0, 0, 0 };



        //ソファー関連
        int sofa_item = 0;
        int[] sofa_to_door = new int[5] { 0, 0, 0, 0, 0 }; //ドアの真正面にソファーがあった場合の運勢変化
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            //エラー家具を無視する処理
            if (IsIgnored(i))
            {
                continue;
            }

            if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.Sofa)
            {
                RaycastHit hit;

                //ドアの真正面
                for (int j = 0; j < furniture_grid_.Count; ++j)
                {
                    //エラー家具を無視する処理
                    if (IsIgnored(j))
                    {
                        continue;
                    }

                    //ドア
                    if (furniture_grid_[j].furniture_type() == FurnitureGrid.FurnitureType.Door)
                    {
                        if (Physics.Raycast(furniture_grid_[i].furniture_grid().transform.position, furniture_grid_[i].up_direction(), out hit))
                        {
                            if (hit.collider.gameObject == furniture_grid_[j].furniture_grid())
                            {
                                for (int k = 0; k < 5; ++k)
                                {
                                    //旺気1.2倍, 邪気1.5倍
                                    displacement_plus_stock[k] += plus_luck_[k] / 5;
                                    displacement_minus_stock[k] += minus_luck_[k] / 2;
                                    sofa_to_door[k] += minus_luck_[k] / 2 - plus_luck_[k] / 2;
                                }
                            }
                        }
                    }
                }
                ++sofa_item;
            }
        }

        if (sofa_item != 0)
        {
            for (int i = 0; i < 5; ++i)
            {
                if (sofa_to_door[i] > 0)
                {
                    //寝室の邪気が高く，悪い運気を取り込みやすくなっている．
                    comment_flag_.Add(new CommentFlag(CommentIdentifier.SofaToDoor, sofa_to_door[i], i));
                }
            }

            int sofa_to_door_sum = sofa_to_door.Sum();
            if (sofa_to_door_sum > 0)
            {
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SofaToDoor, sofa_to_door_sum, 5));
                Debug.Log("SofaToDoor");
            }
        }


        //家電関連
        int electronics_item = 0;

        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            //エラー家具を無視する処理
            if (IsIgnored(i))
            {
                continue;
            }

            if (furniture_grid_[i].furniture_type() == FurnitureGrid.FurnitureType.ConsumerElectronics)
            {
                //窓やドアの真正面
                RaycastHit hit;

                for (int j = 0; j < furniture_grid_.Count; ++j)
                {
                    //エラー家具を無視する処理
                    if (IsIgnored(i))
                    {
                        continue;
                    }

                    //窓，ドア
                    if ((furniture_grid_[j].furniture_type() == FurnitureGrid.FurnitureType.Door)
                        || furniture_grid_[j].furniture_type() == FurnitureGrid.FurnitureType.Window)
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
                    if (furniture_grid_[j].furniture_type() == FurnitureGrid.FurnitureType.Window)
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
                ++electronics_item;
            }
        }



        //方角関連
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

            int displacement_all = displacement.Sum();
            if (displacement_all > 0)
            {
                //寝室の邪気が高く，悪い運気を取り込みやすくなっている．
                comment_flag_.Add(new CommentFlag(CommentIdentifier.BedroomMulti, displacement_all, 5));
                Debug.Log("BedroomMulti");
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

            int displacement_all = displacement.Sum();
            if (displacement_all > 0)
            {
                //リビングの邪気が高く，悪い仕事運，人気運，健康運を取り込みやすくなっている．
                comment_flag_.Add(new CommentFlag(CommentIdentifier.LivingMulti, displacement_all, 5));
                Debug.Log("LivingMulti");
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
                Debug.Log("WorkroomMulti");
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
                int displacement_all = displacement.Sum();
                comment_flag_.Add(new CommentFlag(CommentIdentifier.SouthPurification, displacement_all, 5));
                Debug.Log("SouthPurification");
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

    //**********************************************************************************************************************************************************************************************

    //家具の特徴をカウントする関数 (2018年 2月15日追加)
    private void CountCharacteristic(FurnitureGrid furniture_grid)
    {
        switch (furniture_grid.furniture_type())
        {
            case FurnitureGrid.FurnitureType.Bed: //ベッド
                {
                    SubCountCharacteristic(CharacteristicIdentifier.BedFurniture, 1);
                    break;
                }
            case FurnitureGrid.FurnitureType.Cabinet: //タンス
                {
                    SubCountCharacteristic(CharacteristicIdentifier.CabinetFurniture, 1);
                    break;
                }
            case FurnitureGrid.FurnitureType.Carpet: //カーペット
                {
                    SubCountCharacteristic(CharacteristicIdentifier.CarpetFurniture, 1);
                    break;
                }
            case FurnitureGrid.FurnitureType.Desk: //机
                {
                    SubCountCharacteristic(CharacteristicIdentifier.DeskFurniture, 1);
                    break;
                }
            case FurnitureGrid.FurnitureType.FoliagePlant: //観葉植物
                {
                    SubCountCharacteristic(CharacteristicIdentifier.FoliagePlantFurniture, 1);
                    break;
                }
            case FurnitureGrid.FurnitureType.CeilLamp: //天井ランプ
                {
                    SubCountCharacteristic(CharacteristicIdentifier.CeilLampFurniture, 1);
                    break;
                }
            case FurnitureGrid.FurnitureType.DeskLamp: //机ランプ
                {
                    SubCountCharacteristic(CharacteristicIdentifier.DeskLampFurniture, 1);
                    break;
                }
            case FurnitureGrid.FurnitureType.Sofa: //ソファー
                {
                    SubCountCharacteristic(CharacteristicIdentifier.SofaFurniture, 1);
                    break;
                }
            case FurnitureGrid.FurnitureType.Table: //テーブル
                {
                    SubCountCharacteristic(CharacteristicIdentifier.TableFurniture, 1);
                    break;
                }
            case FurnitureGrid.FurnitureType.ConsumerElectronics: //家電
                {
                    SubCountCharacteristic(CharacteristicIdentifier.ConsumerElectronicsFurniture, 1);
                    break;
                }
            case FurnitureGrid.FurnitureType.Curtain: //カーテン
                {
                    SubCountCharacteristic(CharacteristicIdentifier.CurtainFurniture, 1);
                    break;
                }
            case FurnitureGrid.FurnitureType.Dresser: //鏡
                {
                    SubCountCharacteristic(CharacteristicIdentifier.DresserFurniture, 1);
                    break;
                }
            default: //その他
                {
                    Debug.Log("この家具はその他です．");
                    break;
                }
        }

        for (int i = 0; i < furniture_grid.furniture_subtype().Count; ++i)
        {
            switch (furniture_grid.furniture_subtype()[i])
            {
                case FurnitureGrid.FurnitureType.Bed: //ベッド
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.BedFurniture, 1);
                        break;
                    }
                case FurnitureGrid.FurnitureType.Cabinet: //タンス
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.CabinetFurniture, 1);
                        break;
                    }
                case FurnitureGrid.FurnitureType.Carpet: //カーペット
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.CarpetFurniture, 1);
                        break;
                    }
                case FurnitureGrid.FurnitureType.Desk: //机
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.DeskFurniture, 1);
                        break;
                    }
                case FurnitureGrid.FurnitureType.FoliagePlant: //観葉植物
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.FoliagePlantFurniture, 1);
                        break;
                    }
                case FurnitureGrid.FurnitureType.CeilLamp: //天井ランプ
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.CeilLampFurniture, 1);
                        break;
                    }
                case FurnitureGrid.FurnitureType.DeskLamp: //机ランプ
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.DeskLampFurniture, 1);
                        break;
                    }
                case FurnitureGrid.FurnitureType.Sofa: //ソファー
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.SofaFurniture, 1);
                        break;
                    }
                case FurnitureGrid.FurnitureType.Table: //テーブル
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.TableFurniture, 1);
                        break;
                    }
                case FurnitureGrid.FurnitureType.ConsumerElectronics: //家電
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.ConsumerElectronicsFurniture, 1);
                        break;
                    }
                case FurnitureGrid.FurnitureType.Curtain: //カーテン
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.CurtainFurniture, 1);
                        break;
                    }
                case FurnitureGrid.FurnitureType.Dresser: //鏡
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.DresserFurniture, 1);
                        break;
                    }
                default: //その他
                    {
                        break;
                    }
            }
        }

        for (int i = 0; i < furniture_grid.color_name().Count; ++i)
        {
            switch (furniture_grid.color_name()[i])
            {

                case FurnitureGrid.ColorName.White: //白
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.WhiteColor, furniture_grid.color_name_weight()[i]);
                        break;
                    }
                case FurnitureGrid.ColorName.Black: //黒
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.BlackColor, furniture_grid.color_name_weight()[i]);
                        break;
                    }
                case FurnitureGrid.ColorName.Gray: //灰
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.GrayColor, furniture_grid.color_name_weight()[i]);
                        break;
                    }
                case FurnitureGrid.ColorName.DarkGray: //濃い灰色
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.DarkGrayColor, furniture_grid.color_name_weight()[i]);
                        break;
                    }
                case FurnitureGrid.ColorName.Red: //赤
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.RedColor, furniture_grid.color_name_weight()[i]);
                        break;
                    }
                case FurnitureGrid.ColorName.Pink: //ピンク
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.PinkColor, furniture_grid.color_name_weight()[i]);
                        break;
                    }
                case FurnitureGrid.ColorName.Blue: //青
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.BlueColor, furniture_grid.color_name_weight()[i]);
                        break;
                    }
                case FurnitureGrid.ColorName.LightBlue: //水色
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.LightBlueColor, furniture_grid.color_name_weight()[i]);
                        break;
                    }
                case FurnitureGrid.ColorName.Orange: //オレンジ
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.OrangeColor, furniture_grid.color_name_weight()[i]);
                        break;
                    }
                case FurnitureGrid.ColorName.Yellow: //黄
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.YellowColor, furniture_grid.color_name_weight()[i]);
                        break;
                    }
                case FurnitureGrid.ColorName.Green: //緑
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.GreenColor, furniture_grid.color_name_weight()[i]);
                        break;
                    }
                case FurnitureGrid.ColorName.LightGreen: //黄緑
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.LightGreenColor, furniture_grid.color_name_weight()[i]);
                        break;
                    }
                case FurnitureGrid.ColorName.Beige: //ベージュ
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.BeigeColor, furniture_grid.color_name_weight()[i]);
                        break;
                    }
                case FurnitureGrid.ColorName.Cream: //クリーム色
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.CreamColor, furniture_grid.color_name_weight()[i]);
                        break;
                    }
                case FurnitureGrid.ColorName.Brown: //茶
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.BrownColor, furniture_grid.color_name_weight()[i]);
                        break;
                    }
                case FurnitureGrid.ColorName.Ocher: //黄土色
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.OcherColor, furniture_grid.color_name_weight()[i]);
                        break;
                    }
                case FurnitureGrid.ColorName.Gold: //金
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.GoldColor, furniture_grid.color_name_weight()[i]);
                        break;
                    }
                case FurnitureGrid.ColorName.Silver: //銀
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.SilverColor, furniture_grid.color_name_weight()[i]);
                        break;
                    }
                case FurnitureGrid.ColorName.Purple: //紫
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.PurpleColor, furniture_grid.color_name_weight()[i]);
                        break;
                    }
                default:
                    {

                        break;
                    }
            }

        }

        for (int i = 0; i < furniture_grid.material_type().Count; ++i)
        {
            switch (furniture_grid.material_type()[i])
            {
                case FurnitureGrid.MaterialType.ArtificialFoliage: //人工観葉植物
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.ArtificialFoliageMaterial, furniture_grid.material_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.MaterialType.Wooden: //木材
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.WoodenMaterial, furniture_grid.material_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.MaterialType.Paper: //紙
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.PaperMaterial, furniture_grid.material_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.MaterialType.Leather: //革
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.PaperMaterial, furniture_grid.material_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.MaterialType.NaturalFibre: //天然素材
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.NaturalFibreMaterial, furniture_grid.material_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.MaterialType.ChemicalFibre: //化学素材
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.ChemicalFibreMaterial, furniture_grid.material_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.MaterialType.Cotton: //綿
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.CottonMaterial, furniture_grid.material_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.MaterialType.Plastic: //プラスチック
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.PlasticMaterial, furniture_grid.material_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.MaterialType.Ceramic: //陶磁器
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.CeramicMaterial, furniture_grid.material_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.MaterialType.Marble: //大理石
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.MarbleMaterial, furniture_grid.material_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.MaterialType.Metal: //金属
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.MetalMaterial, furniture_grid.material_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.MaterialType.Mineral: //鉱物
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.MineralMaterial, furniture_grid.material_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.MaterialType.Glass: //ガラス
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.GlassMaterial, furniture_grid.material_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.MaterialType.Water: //水
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.WaterMaterial, furniture_grid.material_type_weight()[i]);
                        break;
                    }
                default: //その他
                    {

                        break;
                    }
            }
        }

        for (int i = 0; i < furniture_grid.pattern_type().Count; ++i)
        {

            switch (furniture_grid.pattern_type()[i])
            {
                case FurnitureGrid.PatternType.Stripe: //ストライプ
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.StripePattern, furniture_grid.pattern_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.PatternType.Leaf: //リーフ
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.LeafPattern, furniture_grid.pattern_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.PatternType.Flower: //花柄
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.FlowerPattern, furniture_grid.pattern_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.PatternType.Star: //星柄
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.StarPattern, furniture_grid.pattern_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.PatternType.Diamond: //ダイヤ柄
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.DiamondPattern, furniture_grid.pattern_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.PatternType.Animal: //アニマル柄
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.AnimalPattern, furniture_grid.pattern_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.PatternType.Zigzag: //ジグザグ柄
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.ZigzagPattern, furniture_grid.pattern_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.PatternType.Novel: //奇抜
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.NovelPattern, furniture_grid.pattern_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.PatternType.Border: //ボーダー
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.BorderPattern, furniture_grid.pattern_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.PatternType.Check: //チェック
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.CheckPattern, furniture_grid.pattern_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.PatternType.Tile: //タイル
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.TilePattern, furniture_grid.pattern_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.PatternType.Dot: //ドット
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.DotPattern, furniture_grid.pattern_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.PatternType.Round: //●柄
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.RoundPattern, furniture_grid.pattern_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.PatternType.Arch: //アーチ
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.ArchPattern, furniture_grid.pattern_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.PatternType.Fruits: //フルーツ
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.FruitsPattern, furniture_grid.pattern_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.PatternType.Luster: //光沢
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.LusterPattern, furniture_grid.pattern_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.PatternType.Wave: //ウェーブストライプ
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.WavePattern, furniture_grid.pattern_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.PatternType.Irregularity: //不規則パターン
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.IrregularityPattern, furniture_grid.pattern_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.PatternType.Cloud: //雲柄
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.CloudPattern, furniture_grid.pattern_type_weight()[i]);
                        break;
                    }
                default:
                    {

                        break;
                    }
            }
        }

        for (int i = 0; i < furniture_grid.form_type().Count; ++i)
        {
            switch (furniture_grid.form_type()[i])
            {
                case FurnitureGrid.FormType.High: //背が高い
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.HighForm, furniture_grid.form_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.FormType.Low: //背が低い
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.LowForm, furniture_grid.form_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.FormType.Vertical: //縦長
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.VerticalForm, furniture_grid.form_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.FormType.Oblong: //横長
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.OblongForm, furniture_grid.form_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.FormType.Square: //正方形
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.SquareForm, furniture_grid.form_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.FormType.Rectangle: //長方形
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.RectangleForm, furniture_grid.form_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.FormType.Round: //円形
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.RoundForm, furniture_grid.form_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.FormType.Ellipse: //楕円形
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.EllipseForm, furniture_grid.form_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.FormType.Triangle: //三角形
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.TriangleForm, furniture_grid.form_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.FormType.Sharp: //尖っている
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.SharpForm, furniture_grid.form_type_weight()[i]);
                        break;
                    }
                case FurnitureGrid.FormType.Novel: //奇抜
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.NovelForm, furniture_grid.form_type_weight()[i]);
                        break;
                    }
                default: //その他
                    {

                        break;
                    }
            }
        }

        for (int i = 0; i < furniture_grid.characteristic().Count; ++i)
        {
            switch (furniture_grid.characteristic()[i])
            {
                case FurnitureGrid.Characteristic.Luxury: //高級そう
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.Luxury, furniture_grid.characteristic_weight()[i]);
                        break;
                    }
                case FurnitureGrid.Characteristic.Sound: //音が出る
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.Sound, furniture_grid.characteristic_weight()[i]);
                        break;
                    }
                case FurnitureGrid.Characteristic.Smell: //いいにおい
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.Smell, furniture_grid.characteristic_weight()[i]);
                        break;
                    }
                case FurnitureGrid.Characteristic.Light: //発光
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.Light, furniture_grid.characteristic_weight()[i]);
                        break;
                    }
                case FurnitureGrid.Characteristic.Hard: //硬い
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.Hard, furniture_grid.characteristic_weight()[i]);
                        break;
                    }
                case FurnitureGrid.Characteristic.Soft: //やわらかい
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.Soft, furniture_grid.characteristic_weight()[i]);
                        break;
                    }
                case FurnitureGrid.Characteristic.Warm: //温かみ
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.Warm, furniture_grid.characteristic_weight()[i]);
                        break;
                    }
                case FurnitureGrid.Characteristic.Cold: //冷たさ
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.Cold, furniture_grid.characteristic_weight()[i]);
                        break;
                    }
                case FurnitureGrid.Characteristic.Flower: //花関連
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.Flower, furniture_grid.characteristic_weight()[i]);
                        break;
                    }
                case FurnitureGrid.Characteristic.Wind: //風関連
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.Wind, furniture_grid.characteristic_weight()[i]);
                        break;
                    }
                case FurnitureGrid.Characteristic.Western: //西洋風
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.Western, furniture_grid.characteristic_weight()[i]);
                        break;
                    }
                case FurnitureGrid.Characteristic.Clutter: //乱雑
                    {
                        SubCountCharacteristic(CharacteristicIdentifier.Clutter, furniture_grid.characteristic_weight()[i]);
                        break;
                    }
                default: //その他
                    {

                        break;
                    }
            }
        }
    }

    //家具の特徴をカウントする関数の補助  (2018年 2月15日追加)
    private void SubCountCharacteristic(CharacteristicIdentifier characteristic_identifier, int weight)
    {
        for (int i = 0; i < characteristic_number_.Count; ++i)
        {
            if (characteristic_number_[i].characteristic_identifier_ == characteristic_identifier)
            {
                characteristic_number_[i].WeightPlus(weight);
                characteristic_number_[i].CountPlus(1);
                break;
            }

            if (i == characteristic_number_.Count - 1)
            {
                characteristic_number_.Add(new CharacteristicNumber(characteristic_identifier, weight, 1));
            }
        }

    }

    //家具を無視するかどうか
    private bool IsIgnored(int index)
    {
        for (int i = 0; i < ignore_index_.Count; ++i)
        {
            if (index == ignore_index_[i])
            {
                return true;
            }
        }
        return false;
    }

    //家具置きすぎ関数
    private void OverFurniture(int furniture_count, int tolerance, int work_weight, int popular_weight, int health_weight, int economic_weight, int love_weight,
    CommentIdentifier comment_identifier, string identifier_name)
    {
        int[] weight = new int[5] { work_weight, popular_weight, health_weight, economic_weight, love_weight };

        for (int i = 0; i < 5; ++i)
        {
            weight[i] *= (furniture_count - tolerance);
            if (weight[i] <= 0)
            {
                continue;
            }
            minus_luck_[i] += weight[i];
            comment_flag_.Add(new CommentFlag(comment_identifier, weight[i], i));
        }
        comment_flag_.Add(new CommentFlag(comment_identifier, weight.Sum(), 5));
        Debug.Log(identifier_name);
    }

    partial void EvaluationTotaTestDummy();
    partial void CommentMini();
    partial void Comment();
}