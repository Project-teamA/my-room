//風水評価クラス(多分furnitureManagementと同じように空のオブジェクトに放り込んで実装),動くかどうかのテストは行っていない
//
//風水評価に関して，以下のサイトを参考
// https://www.interior-heart.com/seven-color/fengshui/
// http://fusuiweb.com/sitemap.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evaluation : MonoBehaviour {

    public enum Direction { North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest, Center};

	private float health_luck_ = 0; //健康運(基準0)
    private float economic_luck_ = 0; //金運(基準0)
    private float love_luck_ = 0; //恋愛運(基準0)
    private float work_luck_ = 0; //仕事運(基準0)
    private float popular_luck_ = 0; //人気運(基準0)

    private float all_luck_ = 0; //総合運

    private float elements_wood_ = 0; //五行木
    private float elements_fire_ = 0; //五行火
    private float elements_earth_ = 0; //五行土
    private float elements_metal_ = 0; //五行金
    private float elements_water_ = 0; //五行水

    private float energy_strength_ = 0; //気の強さ

    private float yin_ = 0; //陰
    private float yang_ = 0; //陽

    private int house_ID_ = 0; //部屋配置ID
    private int[] floor_plan_; //間取りID

    private List<FurnitureGrid> furniture_grid_ = new List<FurnitureGrid>(); //FurnitureGrid.csで実装されているクラスのリスト(最大50)

    //引数 間取り 部屋
    public void Init(int house_ID, int[] floor_plan, List<FurnitureGrid> furniture_grid)
    {
        house_ID_ = house_ID;
        floor_plan_ = floor_plan;

        health_luck_ = 0;
        economic_luck_ = 0;
        love_luck_ = 0;
        work_luck_ = 0;
        popular_luck_ = 0;

        elements_wood_ = 0;
        elements_fire_ = 0;
        elements_earth_ = 0;
        elements_metal_ = 0;
        elements_water_ = 0;
    }

    //引数 更新された家具グリッド
    public void UpdateGrid(FurnitureGrid furniture_grid)
    {
        for(int i = 0; i < furniture_grid_.Count; ++i)
        {
            if( furniture_grid_[i].furniture_grid() == furniture_grid.furniture_grid())
            {
                furniture_grid_[i] = furniture_grid;
                break;
            }
            furniture_grid_.Add(furniture_grid);
        }
    }

    //五行評価関数
    public void EvaluationFiveElements()
    {
        float wood_displacement = 0;
        float wood_threshold = 50;

        float fire_displacement = 0;
        float fire_threshold = 50;

        float earth_displacement = 0;
        float earth_threshold = 50;

        float metal_displacement = 0;
        float metal_threshold = 50;

        float water_displacement = 0;
        float water_threshold = 50;

        if (wood_threshold > 50)
        {
            fire_displacement += wood_threshold/2; //木は火に対して相生効果
            earth_displacement -= wood_threshold/2; //木は土に対して相克効果
        }

        if (fire_threshold > 50)
        {
            earth_displacement += fire_threshold/2; //火は土に対して相生効果
            metal_displacement -= fire_threshold/2; //火は金に対して相克効果
        }

        if (earth_threshold > 50)
        {
            metal_displacement += earth_threshold/2; //土は金に対して相生効果
            water_displacement -= earth_threshold/2; //土は水に対して相克効果
        }

        if (metal_threshold > 50)
        {
            water_displacement += metal_threshold/2; //金は水に対して相生効果
            wood_displacement -= metal_threshold/2; //金は木に対して相克効果
        }

        if (water_threshold > 50)
        {
            wood_displacement += water_threshold/2; //水は木に対して相生効果
            fire_displacement -= water_threshold/2; //水は火に対して相克効果
        }

        elements_wood_ += wood_displacement;
        elements_fire_ += fire_displacement;
        elements_earth_ += earth_displacement;
        elements_metal_ += metal_displacement;
        elements_water_ += water_displacement;
    }

    //方位評価関数(部屋の)
    public void EvaluationDirection(Direction direction)
    {
        if(direction == Direction.North)
        {
            //北は水の気が強い
            if (elements_water_ > 0)
            {
                elements_water_ *= 1.5F; 
            }
        }
        else if(direction == Direction.NorthEast)
        {
            //北東は土の気を持つ(山)
            if(elements_earth_ > 0)
            {
                elements_earth_ *= 1.3F;
            }
        }
        else if(direction == Direction.East)
        {
            //東は木の気をもつ(若木)
            if (elements_wood_ > 0)
            {
                elements_wood_ *= 1.3F;
            }
        }
        else if(direction == Direction.SouthEast)
        {
            //南東は木の気をもつ(大木)
            if(elements_wood_ > 0)
            {
                elements_wood_ *= 1.5F;
            }
        }
        else if(direction == Direction.South)
        {
            //南は火の気をもつ
            if(elements_fire_ > 0)
            {
                elements_fire_ *= 1.5F;
            }
        }
        else if(direction == Direction.SouthWest)
        {
            //栄養豊富な黄色い土の気を持つ
            if (elements_wood_ > 0)
            {
                elements_wood_ *= 1.5F;
            }
        }
        else if(direction == Direction.West)
        {
            //金の気を持つ方位(金運に直接関係)
            if(elements_metal_ > 0)
            {
                elements_metal_ *= 1.5F;
            }
        }
        else if(direction == Direction.NorthWest)
        {
            //北西は主人の方位(金の気)
            if(elements_metal_ > 0)
            {
                elements_metal_ *= 1.3F;
            }
        }
        else
        {
            
        }
    }

    //陰陽評価関数
    public void EvaluationYinYang()
    {

    }

    //アイテム評価関数
    public void EvaluationItem(FurnitureGrid furniture_grid)
    {
        if(furniture_grid.furniture_type() == FurnitureGrid.FurnitureType.Bed)
        {
            //ベッドが北枕にあるかどうか評価(グリッド作成段階で北枕の方位を北とする場合)
            if (furniture_grid.up_direction() == Vector3.up)
            {
                all_luck_ += 1F;
            }
            else
            {

            }

            //シングルベッドをつなげるとダメ
            for (int i=0; i<furniture_grid_.Count; ++i)
            {
                if (furniture_grid_[i].furniture_grid().GetComponent<BoxCollider>().bounds.Contains
                    (furniture_grid_[i].grid_position() + (furniture_grid_[i].parameta(0) + 1) * 0.2F * furniture_grid_[i].right_direction() ) == true)
                {
                    all_luck_ -= 0.5F;
                }

                if (furniture_grid_[i].furniture_grid().GetComponent<BoxCollider>().bounds.Contains
                    (furniture_grid_[i].grid_position() - (furniture_grid_[i].parameta(0) + 1) * 0.2F * furniture_grid_[i].right_direction()) == true)
                {
                    all_luck_ -= 0.5F;
                }

            }

            //ベッドと壁の隙間ダメ


            //ドアベクトルがベッドの頭指すだめ(ドア側で実装)


            //鏡写しダメ(鏡側で実装)


            //窓の近くにベッドダメ(1つでもあったら)
            GameObject surrounding = furniture_grid.furniture_grid();
            surrounding.transform.localScale = new Vector3(3F, 3F, 3F);
            bool window_near_flag = false;
            for (int i= 0; i < surrounding.transform.childCount; ++i)
            {
                surrounding.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
                for(int j = 0; j < furniture_grid_.Count; ++j)
                {
                    if(furniture_grid_[j].tag == "furniture_grid_window")
                    {
                        for (int k = 0; k < furniture_grid_[j].transform.childCount; ++k)
                        {
                            if(surrounding.GetComponents<BoxCollider>()[i].bounds.Intersects(furniture_grid_[j].GetComponents<BoxCollider>()[k].bounds))
                            {
                                window_near_flag = true;
                                break;
                            }

                        } //k
                    }
                } //j
            } //i

            if(window_near_flag == true)
            {
                all_luck_ -= 1F;
            }


            //角がベッドの顔に向いたらだめ
        }
    }

    //部屋評価関数
    public void EvaluationRoom()
    {

    }

    //色評価関数
    public void EvaluationColor()
    {

    }

    //運勢評価関数
    public void Evaluationfortune(Direction direction)
    {
        if (direction == Direction.North)
        {
            //アイビー，曲線的なもの，桃色のものを配置すると恋愛運が高まる

            //北の気が強いと恋愛運が高まる
            if (energy_strength_ > 400F)
            {
                love_luck_ += energy_strength_ - 400F; 
            }

            //北の水の気が強すぎると健康運が下がる
            if (elements_water_ > 200F)
            {
                health_luck_ -= (elements_water_ - 200F);
            }
        }
        else if (direction == Direction.NorthEast)
        {
            //背の高い家具や観葉植物を置くと運気UP

            //北東は変化を司る方位なので気が強い場合，良い運はよりよく，悪い運はより悪くなる
            if(energy_strength_ > 400F)
            {
                health_luck_ *= (1F + energy_strength_/800) ;
                economic_luck_ *= (1F + energy_strength_/800);
                love_luck_ *= (1F + energy_strength_/800);
                work_luck_ *= (1F + energy_strength_/800);
                popular_luck_ *= (1F + energy_strength_ / 800);
            }
        }
        else if (direction == Direction.East)
        {
            //東に風を意識させるようなアイテムを加えると運気UP

            //東の気が強いと仕事運が高まる
            if (energy_strength_ > 400F)
            {
                work_luck_ += energy_strength_ - 400F;
            }
        }
        else if (direction == Direction.SouthEast)
        {
            //東以上に風の流れと強い関係あり
            //南東は縁を呼び込む方角
            if(energy_strength_ > 400F)
            {
                work_luck_ += (energy_strength_ - 400F) / 3;
                love_luck_ += (energy_strength_ - 400F) / 3;
                popular_luck_ += (energy_strength_ - 400F) / 3;
            }

            //水槽はこの方角に置くとよい
        }
        else if (direction == Direction.South)
        {
            //南の火の気で悪い気を燃やす
            if(elements_fire_ > 100F)
            {
                if(health_luck_ < 0)
                {
                    health_luck_ = 0;
                }
                if (economic_luck_ < 0)
                {
                    economic_luck_ = 0;
                }
                if (love_luck_ < 0)
                {
                    love_luck_ = 0;
                }
                if (work_luck_ < 0)
                {
                    work_luck_ = 0;
                }
                if (popular_luck_ < 0)
                {
                    popular_luck_ = 0;
                }
            }

            //人気運や美容を司る
            if(energy_strength_ > 400F)
            {
                health_luck_ += (energy_strength_ - 400F) / 6;
                work_luck_ += (energy_strength_ - 400F) / 6;
                love_luck_ -= (energy_strength_ - 400F) / 6;
                popular_luck_ += (energy_strength_ - 400F) / 2;
            }

            //あまり火の気が強すぎるとイライラしたり怒りっぽくなる
            if (elements_fire_ > 200F)
            {
                health_luck_ -= (energy_strength_ - 400F) / 4;
                work_luck_ -= (energy_strength_ - 400F) / 4;
                love_luck_ -= (energy_strength_ - 400F) / 4;
                popular_luck_ -= (energy_strength_ - 400F) / 4;
            }

            //原色系の色は避ける．プラスチックや化学繊維は避ける
        }
        else if (direction == Direction.SouthWest)
        {
            //南西は家庭運を司る方位
            if(energy_strength_ > 400F)
            {
                economic_luck_ += (energy_strength_ - 400F) / 4;
                love_luck_ -= (elements_fire_ - 400F) / 4;
                work_luck_ += (energy_strength_ - 400F) / 4;
                popular_luck_ += (energy_strength_ - 400F) / 4;
            }


            //北東とは違い，低い家具の方が良い
        }
        else if (direction == Direction.West)
        {
            //西は金運に直結(娯楽運や恋愛運などの楽しみにつながる運気も担当)
            if(energy_strength_ > 400F)
            {
                economic_luck_ += (energy_strength_ - 400F) / 3;
                love_luck_ += (energy_strength_ - 400F) / 3;
            }

            if(elements_metal_ > 150F)
            {
                economic_luck_ += (elements_metal_ - 150F) / 3;
            }
        }
        else if (direction == Direction.NorthWest)
        {
            //部屋全体に高級感を持たせるとよい

            //北西は主人の方位(自分の格を上げたいときはこの方位を強化)
            if (energy_strength_ > 400F)
            {
                economic_luck_ += (energy_strength_ - 400F) / 6;
                work_luck_ += (energy_strength_ - 400F) / 6;
                popular_luck_ += (energy_strength_ - 400F) / 6;
            }

            if (elements_metal_ > 150F)
            {
                economic_luck_ += (elements_metal_ - 400F) / 6;
                work_luck_ += (elements_metal_ - 400F) / 6;
                popular_luck_ += (elements_metal_ - 400F) / 6;
            }
        }
        else
        {

        }
    }
}
