//風水評価クラス(多分furnitureManagementと同じように空のオブジェクトに放り込んで実装),動くかどうかのテストは行っていない
//
//使い方
//①先ずInitで初期化
//②次にUpdateGrid,またはDeleteGridで家具グリッドの更新
//③その後EvaluationTotalで運勢評価
//②に戻る
//
//2018年1月21日
//五行陰陽評価ラスト関数 EvaluationLast() 追加
//運勢評価ラスト関数 FortuneLast() 追加

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Evaluation : MonoBehaviour
{
    public enum Room { Entrance, Living, kitchen, Workroom, Bedroom, Bathroom, Toilet };
    public enum Direction { North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest };

    //五行(200以上で超過 ここは確定するように調整) 0未満にならない
    private int elements_wood_ = 0; //五行木
    private int elements_fire_ = 0; //五行火
    private int elements_earth_ = 0; //五行土
    private int elements_metal_ = 0; //五行金
    private int elements_water_ = 0; //五行水

    private int energy_strength_ = 0; //気の強さ(max1000 min0, ここは確定するように調整)

    private int yin_yang_ = 0; //陰陽(プラスで陽，マイナスで陰)

    //今のところ無制限(あとでバランス調整)
    private int health_luck_ = 0; //健康運(マイナスあり)
    private int economic_luck_ = 0; //金運(マイナスあり)
    private int love_luck_ = 0; //恋愛運(マイナスあり)
    private int work_luck_ = 0; //仕事運(マイナスあり)
    private int popular_luck_ = 0; //人気運(マイナスあり)

    private int all_luck_ = 0; //総合運

    private string comment_ = "あなたの部屋の評価\n"; //コメント

    private Room room_role_; //部屋の種類
    private Direction room_direction_; //部屋の方角

    private List<FurnitureGrid> furniture_grid_ = new List<FurnitureGrid>(); //FurnitureGrid.csで実装されているクラスのリスト(最大50)

    //*******************************************************************************************************************************************************************************************

    //五行木取得用
    public int elements_wood()
    {
        return elements_wood_;
    }

    //五行火取得用
    public int elements_fire()
    {
        return elements_fire_;
    }

    //五行土取得用
    public int elements_earth()
    {
        return elements_earth_;
    }

    //五行金取得用
    public int elements_metal()
    {
        return elements_metal_;
    }

    //五行水取得用
    public int elements_water()
    {
        return elements_water_;
    }

    //気の強さ取得用
    public int energy_strength()
    {
        return energy_strength_;
    }




    //健康運(取得用)
    public int health_luck()
    {
        return health_luck_;
    }

    //金運(取得用)
    public int economic_luck()
    {
        return economic_luck_;
    }

    //恋愛運(取得用)
    public int love_luck()
    {
        return love_luck_;
    }

    //仕事運(取得用)
    public int work_luck()
    {
        return work_luck_;
    }

    //人気運(取得用)
    public int popular_luck()
    {
        return popular_luck_;
    }

    //総合運(取得用)
    private int all_luck()
    {
        return all_luck_;
    }




    //コメント(取得用)
    public string comment()
    {
        return comment_;
    }

    //*******************************************************************************************************************************************************************************************

    //初期化関数
    // 
    // room_role_ = 部屋の種類
    // room_direction_ = 部屋の方角
    // furniture_grid_ = 家具の更新
    public void Init(Room room_role, Direction room_direction, List<FurnitureGrid> furniture_grid)
    {
        room_role_ = room_role;
        room_direction_ = room_direction;
        furniture_grid_ = furniture_grid;
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

    //総合評価関数(評価の一連の流れ)
    //
    //
    public void EvaluationTotal()
    {
        //再評価の都合上全て0に戻す
        health_luck_ = 0; //健康運(基準0)
        economic_luck_ = 0; //金運(基準0)
        love_luck_ = 0; //恋愛運(基準0)
        work_luck_ = 0; //仕事運(基準0)
        popular_luck_ = 0; //人気運(基準0)

        all_luck_ = 0; //総合運(全ての運の加算)

        //五行はプラスオンリー
        elements_wood_ = 0; //五行木
        elements_fire_ = 0; //五行火
        elements_earth_ = 0; //五行土
        elements_metal_ = 0; //五行金
        elements_water_ = 0; //五行水

        energy_strength_ = 0; //気の強さ(全ての気の加算)

        yin_yang_ = 0; //陰陽

        EvaluationItem(); //部屋のアイテムによる五行陰陽評価関数
        EvaluationRoom(); //部屋の種類による五行陰陽評価関数(部屋の)
        EvaluationDirection(); //方位五行陰陽評価関数(部屋の)
        EvaluationFiveElements(); //五行による相生効果と相克効果
        EvaluationeLast();

        //ここから五行の気の強さの加算
        if(elements_wood_ > 200)
        {
            energy_strength_ += 200; 
        }
        else
        {
            energy_strength_ += elements_wood_;
        }

        if (elements_fire_ > 200)
        {
            energy_strength_ += 200;
        }
        else
        {
            energy_strength_ += elements_fire_;
        }

        if (elements_earth_ > 200)
        {
            energy_strength_ += 200;
        }
        else
        {
            energy_strength_ += elements_earth_;
        }

        if (elements_metal_ > 200)
        {
            energy_strength_ += 200;
        }
        else
        {
            energy_strength_ += elements_metal_;
        }

        if (elements_water_ > 200)
        {
            energy_strength_ += 200;
        }
        else
        {
            energy_strength_ += elements_water_;
        }
        //ここまで五行の気の強さの加算



        for(int i = 0; i < furniture_grid_.Count; ++i)
        {
            FortuneItem(furniture_grid_[i]); //アイテムによる運勢補正
        }
        FortuneRoom(); //部屋による運勢補正
        FortuneDirection(); //方位による運勢補正
        FortuneFiveElement(); //五行による運勢補正

        //陰陽による運勢補正
        if(yin_yang_ < -20)
        {
            //陰の気が強すぎて運気ダウン
            health_luck_ += (yin_yang_ + 20);
            love_luck_ += (yin_yang_ + 20);
            popular_luck_ += (yin_yang_ + 20); 
        }
        else if(yin_yang_ > 200)
        {
            //陽の気が強すぎて運気ダウン
            love_luck_ -= (yin_yang_ - 200);
            work_luck_ -= (yin_yang_ - 200);
        }

        FortuneLast();

        all_luck_ = health_luck_ + economic_luck_ + love_luck_ + work_luck_ + popular_luck_;
    }

    //*******************************************************************************************************************************************************************************************

    //アイテム評価関数(アイテムごとの五行と陰陽を加算)
    private void EvaluationItem()
    {
        for (int i = 0; i < furniture_grid_.Count; ++i)
        {
            elements_wood_ += furniture_grid_[i].elements_wood();
            elements_fire_ += furniture_grid_[i].elements_fire();
            elements_earth_ += furniture_grid_[i].elements_earth();
            elements_metal_ += furniture_grid_[i].elements_metal();
            elements_water_ += furniture_grid_[i].elements_water();

            yin_yang_ += furniture_grid_[i].yin_yang();
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
            //陽気(重要)
        }
        else if (room_role_ == Room.Living)
        {
            //土の気をもつ
            elements_earth_ += 40;
            //陽気 
        }
        else if (room_role_ == Room.kitchen)
        {
            //火と水の気，弱い金の気をもつ
            elements_fire_ += 50;
            elements_metal_ += 10;
            elements_water_ += 50;
            //陰気
        }
        else if (room_role_ == Room.Workroom)
        {
            //木の気をもつ
            elements_wood_ += 20;
            //陽気
        }
        else if (room_role_ == Room.Bedroom)
        {
            //陽気
        }
        else if (room_role_ == Room.Bathroom)
        {
            //強い水の気をもつ
            elements_water_ += 60;
            //陰気
            yin_yang_ -= 40;
        }
        else if (room_role_ == Room.Toilet)
        {
            //強い水の気をもつ
            elements_water_ += 60;
            //陰気(重要)
            yin_yang_ -= 50;
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
            elements_water_ += 100;
        }
        else if (room_direction_ == Direction.NorthEast)
        {
            //北東は土の気が強い(山)
            elements_earth_ += 100;
        }
        else if (room_direction_ == Direction.East)
        {
            //東は木の気が強い(若木)
            elements_wood_ += 100;
        }
        else if (room_direction_ == Direction.SouthEast)
        {
            //南東は木の気が強い(大木)
            elements_wood_ += 100;
        }
        else if (room_direction_ == Direction.South)
        {
            //南は火の気が強い
            elements_fire_ += 100;
        }
        else if (room_direction_ == Direction.SouthWest)
        {
            //南西は土の気が強い
            elements_earth_ += 100;
        }
        else if (room_direction_ == Direction.West)
        {
            //西は金の気が強い
            elements_metal_ += 100;
        }
        else if (room_direction_ == Direction.NorthWest)
        {
            //北西は金の気が強い
            elements_metal_ += 100;
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
        int wood_displacement = 0;
        int fire_displacement = 0;
        int earth_displacement = 0;
        int metal_displacement = 0;
        int water_displacement = 0;


        //相生効果(木は火を生む)
        if(elements_wood_ > elements_fire_)
        {
            fire_displacement += elements_fire_/2;
            wood_displacement -= elements_fire_/4;
        }
        else
        {
            fire_displacement += elements_wood_/2;
            wood_displacement -= elements_wood_/4;
        }

        //相生効果(火は土を生む)
        if (elements_fire_ > elements_earth_)
        {
            earth_displacement += elements_earth_/2;
            fire_displacement -= elements_earth_/4;
        }
        else
        {
            earth_displacement += elements_fire_/2;
            fire_displacement -= elements_fire_/4;
        }

        //相生効果(土は金を生む)
        if (elements_earth_ > elements_metal_)
        {
            metal_displacement += elements_metal_/2;
            earth_displacement -= elements_metal_/4;
        }
        else
        {
            metal_displacement += elements_earth_/2;
            earth_displacement -= elements_earth_/4;
        }

        //相生効果(金は水を生む)
        if (elements_metal_ > elements_water_)
        {
            water_displacement += elements_water_/2;
            metal_displacement -= elements_water_/4;
        }
        else
        {
            water_displacement += elements_metal_/2;
            metal_displacement -= elements_metal_/4;
        }

        //相生効果(水は木を生む)
        if (elements_water_ > elements_wood_)
        {
            wood_displacement += elements_wood_/2;
            water_displacement -= elements_wood_/4;
        }
        else
        {
            wood_displacement += elements_water_/2;
            water_displacement -= elements_water_/4;
        }

        

        //相克効果(木は土を消す)
        if (elements_wood_ > elements_earth_)
        {
            earth_displacement -= elements_earth_ /2;
            wood_displacement -= elements_earth_ / 4; 
        }
        else
        {
            earth_displacement -= elements_wood_ /2;
            wood_displacement -= elements_wood_ / 4;
        }

        //相克効果(火は金を消す)
        if (elements_fire_ > elements_metal_)
        {
            metal_displacement -= elements_metal_ /2;
            fire_displacement -= elements_metal_ / 4;
        }
        else
        {
            metal_displacement -= elements_fire_ /2;
            fire_displacement -= elements_fire_ / 4;
        }

        //相克効果(土は水を消す)
        if (elements_earth_ > elements_water_)
        {
            water_displacement -= elements_water_ /2;
            earth_displacement -= elements_water_ / 4;
        }
        else
        {
            water_displacement -= elements_earth_/2;
            earth_displacement -= elements_earth_ / 4;
        }

        //相克効果(金は木を消す)
        if (elements_metal_ > elements_wood_)
        {
            wood_displacement -= elements_wood_ /2;
            metal_displacement -= elements_wood_ / 4;
        }
        else
        {
            wood_displacement -= elements_metal_ /2;
            metal_displacement -= elements_metal_ / 4;
        }

        //相克効果(水は火を消す)
        if (elements_water_ > elements_fire_)
        {
            fire_displacement -= elements_fire_/2;
            water_displacement -= elements_fire_ / 4;
        }
        else
        {
            fire_displacement -= elements_water_/2;
            water_displacement -= elements_water_ / 4;
        }

        elements_wood_ += wood_displacement;
        elements_fire_ += fire_displacement;
        elements_earth_ += earth_displacement;
        elements_metal_ += metal_displacement;
        elements_water_ += water_displacement;

        if(elements_wood_ < 0)
        {
            elements_wood_ = 0;
        }

        if (elements_fire_ < 0)
        {
            elements_fire_ = 0;
        }

        if (elements_earth_ < 0)
        {
            elements_earth_ = 0;
        }

        if (elements_metal_ < 0)
        {
            elements_metal_ = 0;
        }

        if (elements_water_ < 0)
        {
            elements_water_ = 0;
        }

        //五行による陰陽補正
        yin_yang_ += elements_fire_;
        yin_yang_ -= elements_earth_ / 2;
        yin_yang_ += elements_metal_ / 2;
        yin_yang_ -= elements_water_;
    }

    //**************************************************************************************************************************************************************************************************

    //仕上げの五行陰陽補正(観葉植物による陰陽中和など)
    //
    //ここでは五行陰陽に対し加算ではなく乗算による補正が行われる
    private void EvaluationeLast()
    {
        int wood_displacement = 0; //木の気変化量
        int fire_displacement = 0; //火の気変化量
        int earth_displacement = 0; //土の気変化量
        int metal_displacement = 0; //金の気変化量
        int water_displacement = 0; //水の気変化量
        int yin_yang_displacement = 0; //陰陽の気変化量

        //ここから使用例(それぞれ1.2倍の場合)
        //wood_displacement += elements_wood_ /5;
        //fire_displacement += elements_fire_ / 5;
        //earth_displacement += elements_earth_ / 5;
        //metal_displacement += elements_metal_ / 5;
        //water_displacement += elements_water_ / 5;
        //yin_yang_displacement += yin_yang_ / 5;

        //最後に元の五行陰陽に補正を加算
        elements_wood_ += wood_displacement;
        elements_fire_ += fire_displacement;
        elements_earth_ += earth_displacement;
        elements_metal_ += metal_displacement;
        elements_water_ += water_displacement;
        yin_yang_ += yin_yang_displacement;
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
                health_luck_ += 30;
            }
            else if (furniture_grid.up_direction() == Vector3.down)
            {
                //ベッドが南枕(1番ダメ, 安眠出来ない)
                health_luck_ -= 30;
            }
            else if (furniture_grid.up_direction() == Vector3.right)
            {
                //ベッドが東枕(2番目に良い)
                health_luck_ += 20;
            }
            else if (furniture_grid.up_direction() == Vector3.left)
            {
                //ベッドが西枕(2番目に良くない)
                health_luck_ -= 20;
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

    //部屋による運勢補正
    private void FortuneRoom()
    {
        if (room_role_ == Room.Bedroom)
        {

        }
    }

    //**************************************************************************************************************************************************************************************************

    //部屋の方位による運勢補正
    private void FortuneDirection()
    {
        if (room_direction_ == Direction.North)
        {
            //北は金運と恋愛運
            int buffer_water = elements_water_;
            if(buffer_water > 200)
            {
                buffer_water = 200;
            }
            economic_luck_ += ( energy_strength_ + buffer_water )/ 10;
            love_luck_ += ( energy_strength_ + buffer_water )/ 10;
            
        }
        else if (room_direction_ == Direction.NorthEast)
        {
            int buffer_earth = elements_earth_;
            if(buffer_earth > 200)
            {
                buffer_earth = 200;
            }
            //変化を司る方位なので陰陽バランスが悪いと，運気が悪くなる
            if(yin_yang_ >= -20 && yin_yang_ <= 200)
            {
                //まんべんなく運気上がる
                health_luck_ += ( energy_strength_ + buffer_earth ) / 20;
                economic_luck_ += ( energy_strength_ + buffer_earth )/ 20;
                love_luck_ += ( energy_strength_ + buffer_earth )/ 20;
                work_luck_ += ( energy_strength_ + buffer_earth )/ 20;
                popular_luck_ += ( energy_strength_ + buffer_earth )/ 20;
            }
            else
            {
                //まんべんなく運気が下がる
                health_luck_ -= energy_strength_ + buffer_earth / 40;
                economic_luck_ -= energy_strength_ + buffer_earth / 40;
                love_luck_ -= energy_strength_ + buffer_earth/ 40;
                work_luck_ -= energy_strength_ + buffer_earth/ 40;
                popular_luck_ -= energy_strength_ + buffer_earth/ 40;
            }
        }
        else if (room_direction_ == Direction.East)
        {
            //東は仕事運
            int buffer_wood = elements_wood_;
            if(buffer_wood > 200)
            {
                buffer_wood = 200;
            }
            work_luck_ += (energy_strength_ + buffer_wood)/ 5;
        }
        else if (room_direction_ == Direction.SouthEast)
        {
            //南東は恋愛運
            int buffer_wood = elements_wood_;
            if(buffer_wood > 200)
            {
                buffer_wood = 200;
            }
            love_luck_ += (energy_strength_ + buffer_wood) / 5;
        }
        else if (room_direction_ == Direction.South)
        {
            //南は人気運と健康運
            int buffer_fire = elements_fire_;
            if(buffer_fire > 200)
            {
                buffer_fire = 200;
            }
            health_luck_ += ( energy_strength_ + buffer_fire ) / 10;
            popular_luck_ += ( energy_strength_ + buffer_fire )/ 10;
        }
        else if (room_direction_ == Direction.SouthWest)
        {
            //南西は健康運と仕事運
            int buffer_earth = elements_earth_;
            if(buffer_earth > 200)
            {
                buffer_earth = 200;
            }
            health_luck_ += ( energy_strength_ + buffer_earth ) / 10;
            work_luck_ += ( energy_strength_ + buffer_earth ) / 10;
        }
        else if (room_direction_ == Direction.West)
        {
            //西は金運と恋愛運
            int buffer_metal = elements_metal_;
            if(buffer_metal > 200)
            {
                buffer_metal = 200;
            }
            economic_luck_ += ( energy_strength_ + buffer_metal )/ 10;
            love_luck_ += ( energy_strength_ + buffer_metal )/ 10;
        }
        else if (room_direction_ == Direction.NorthWest)
        {
            //北西は金運と仕事運
            int buffer_metal = elements_metal_;
            if (buffer_metal > 200)
            {
                buffer_metal = 200;
            }
            economic_luck_ += (energy_strength_ + 200) / 10;
            work_luck_ += (energy_strength_ + 200) / 10;
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
        work_luck_ += elements_wood_ * 4 / 5;
        health_luck_ += elements_wood_ / 5;
        if(elements_wood_ > 200)
        {
            work_luck_ -= (elements_wood_ - 200) * 2;
        }

        //火の気の影響
        popular_luck_ += elements_fire_ * 3 / 5;
        health_luck_ += elements_fire_ / 5;
        work_luck_ += elements_fire_ / 5;
        if(elements_fire_ > 200)
        {
            work_luck_ -= elements_fire_ - 200;
            love_luck_ -= elements_fire_ - 200;
        }

        //土の気の影響
        health_luck_ += elements_earth_ / 5;
        economic_luck_ += elements_earth_ / 5; 
        love_luck_ += elements_earth_ / 5; 
        work_luck_ += elements_earth_ / 5;
        popular_luck_ += elements_earth_ / 5;
        if(elements_earth_ > 200)
        {
            health_luck_ -= (elements_earth_ - 200) * 2;
        }

        //金の気の影響
        economic_luck_ += elements_metal_;
        if(elements_metal_ > 200)
        {
            economic_luck_ -= (elements_metal_ - 200) * 2;
        }

        //水の気の影響
        love_luck_ += elements_water_ * 4 / 5;
        work_luck_ -= elements_water_ / 5;
        if(elements_water_ > 200)
        {
            health_luck_ -= (elements_water_ - 200);
            economic_luck_ -= (elements_water_ - 200);
        }
    }

    //***********************************************************************************************************************************************************************************************

    //仕上げの運勢補正(鏡による運勢増減など)
    //
    //ここでは運勢に対し加算ではなく乗算による補正が行われる
    private void FortuneLast()
    {
        int work_displacement = 0;
        int popular_displacement = 0;
        int health_displacement = 0;
        int economic_displacement = 0;
        int love_displacement = 0;

        //ここから使用例(それぞれ0.8倍の場合)
        //work_displacement -= work_luck_/5;
        //popular_displacement -= popular_luck_/5;
        //health_displacement -= health_luck_/5;
        //economic_displacement -= economic_luck_/5;
        //love_displacement -= love_luck_/5;

        //最後に元の運勢に補正を加算
        work_luck_ += work_displacement;
        popular_luck_ += popular_displacement;
        health_luck_ += health_displacement;
        economic_luck_ += economic_displacement;
        love_luck_ += love_displacement;
    }

    partial void EvaluationTotaTestDummy();
    partial void Comment(int comment_ID);
}