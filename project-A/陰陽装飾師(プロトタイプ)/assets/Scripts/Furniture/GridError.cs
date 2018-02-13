//GridError.cs(家具グリッド同士が衝突したら赤く表示, 家具グリッドオブジェクト一つ一つにアタッチされる)
//この仕様は変更する可能性大
//
//8個ほど同じ位置に重なると処理がかなり重い
//
// 2018年1月17日 仕様変更の検討(菅原涼太)
// 家具を乗せれる家具は家具右クリック→プロパティで実現(乗せれる家具はあらかじめ確定)
// 消したいときも乗せている家具グリッドを右クリック→プロパティで削除
// 乗せた家具乗せられた家具は一つの家具とみなされる．(家具を数えるときは乗っている家具をすっ飛ばす．運勢評価のときは乗っている家具も数える)
// 乗せる家具と乗る家具の移動は完全同期
//
// 窓の家具グリッド，ドアの家具グリッドは(家具を数えるときはすっ飛ばす, 運勢評価のときは数える)
//
// 新たに追加する家具グリッド
// 床・・・床全体の模様が変わる効果．一番下，そもそも動かせないしグリッドももたない(もつのはテクスチャのみ)，運勢評価時には数えるが，家具の数を数えるときは飛ばす．(どう森と一緒)
// 壁紙・・・壁全体の模様が変わる効果, そもそも動かせないしグリッドももたない(もつのはテクスチャのみ)，運勢評価時には数えるが，家具の数を数えるときは飛ばす．(どう森と一緒)
//
// エラー判定
// 家具グリッドがはみ出してしまう
// カーペット系，天井につるす照明系の家具以外は基本全て重なってはダメ．(上に乗せれる乗せれない判定は大体削除対象)

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridError : MonoBehaviour
{

    private Bounds[] bounds_;
    private List<GameObject> collision_objects_ = new List<GameObject>(); //衝突しているオブジェクト
    private List<GameObject> error_objects_ = new List<GameObject>(); //エラーの元になっているオブジェクト 
    private bool errored_ = false; //エラーかどうか判定

    //エラーかどうか判定
    public bool errored()
    {
        return errored_;
    }

    //他の家具グリッドにぶつかっているとき
    void OnTriggerStay(Collider collider)
    {
        if (((((collider.gameObject.tag == "furniture_grid"
            || collider.gameObject.tag == "furniture_grid_rugs")
            || collider.gameObject.tag == "furniture_grid_wall")
            || collider.gameObject.tag == "furniture_grid_door")
            || collider.gameObject.tag == "furniture_grid_ceil")
            || collider.gameObject.tag == "furniture_grid_window")
        {
            bool collider_add_flag = true;

            for (int i = 0; i < collision_objects_.Count; ++i)
            {
                if (collision_objects_[i] == collider.gameObject)
                {
                    collider_add_flag = false;
                }
            }

            if (collider_add_flag == true)
            {
                collision_objects_.Add(collider.gameObject);
            }

            int my_child_number = transform.childCount; //自分の子オブジェクトの数
            int opponent_child_number = collider.gameObject.transform.childCount; //相手の子オブジェクトの数



            //true = エラー, false = ok
            bool error_flag = false;

            //0 = どちらでもない, 1 = (自分が)ダウン，2 = (自分が)アップ
            int up_down = 0;


            if (transform.tag == "furniture_grid_door"
               || transform.tag == "furniture_grid_window")
            {
                //壁掛け，ドア，窓は詳しく決めていない
                //詳しく決めていない

            }
            else if (transform.tag == "furniture_grid_wall") //自分が壁掛けの場合
            {
                if (collider.transform.tag == "furniture_grid_wall")
                {
                    //相手が壁掛けならばエラー
                    error_flag = true;
                }
                else if (collider.transform.tag == "furniture_grid_ceil")
                {
                    //相手が天井掛けなら自分が下になる
                    error_flag = false;
                    up_down = 1;
                }
                else
                {
                    //他は自分が上になる
                    error_flag = false;
                    up_down = 2;
                }
            }
            else if (transform.tag == "furniture_grid_rugs") //自分が敷物の場合
            {
                if (collider.transform.tag == "furniture_grid_rugs")
                {
                    //相手が敷物ならばエラー
                    error_flag = true;
                }
                else
                {
                    //相手が敷物でなければOK(自分が下になる)
                    error_flag = false;
                    up_down = 1;
                }
            }
            else if (transform.tag == "furniture_grid_ceil") //自分が天井なら
            {
                if (collider.transform.tag == "furniture_grid_ceil")
                {
                    //相手が天井ならばエラー
                    error_flag = true;
                }
                else
                {
                    //相手が天井以外ならOK(自分が上になる)
                    error_flag = false;
                    up_down = 2;
                }
            }
            else if (transform.tag == "furniture_grid") //自分が普通の家具なら
            {
                if (collider.transform.tag == "furniture_grid")
                {
                    //あいてが普通の家具ならエラー
                    error_flag = true;
                }
                else if (collider.transform.tag == "furniture_grid_rugs")
                {
                    //あいてが敷物ならOK(自分が上になる)
                    error_flag = false;
                    up_down = 2;
                }
                else if (collider.transform.tag == "furniture_grid_rugs")
                {
                    //相手が天井掛けならOK(自分が下になる)
                    error_flag = false;
                    up_down = 1;
                }
            }



            if (error_flag == false)
            {
                for (int i = 0; i < error_objects_.Count; ++i)
                {
                    if (error_objects_[i] == collider.gameObject)
                    {
                        error_objects_.RemoveAt(i);
                        break;
                    }
                }

                if (error_objects_.Count == 0)
                {
                    for (int i = 0; i < transform.childCount; ++i)
                    {
                        transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(10, 10, 10, 1);
                    } //i
                    errored_ = false;
                }

                if (up_down == 1)
                {
                    if (collider.gameObject.transform.position.z > transform.position.z)
                    {
                        Vector3 buffer_my_position = transform.position;
                        Vector3 buffer_opponent_position = collider.gameObject.transform.position;
                        float buffer_z = buffer_my_position.z;
                        buffer_my_position.z = buffer_opponent_position.z;
                        buffer_opponent_position.z = buffer_z;
                        transform.position = buffer_my_position;
                        collider.gameObject.transform.position = buffer_opponent_position;
                    } //collider.gameObject.transform.position.z > transform.position.z
                    else if (transform.position.z == collider.gameObject.transform.position.z)
                    {
                        Vector3 buffer_position = collider.gameObject.transform.position;
                        buffer_position.z -= 0.01F;
                        collider.gameObject.transform.position = buffer_position;
                    } //transform.position.z == collider.gameObject.transform.position.z
                }
                else if (up_down == 2)
                {
                    if (transform.position.z > collider.gameObject.transform.position.z)
                    {
                        Vector3 buffer_my_position = transform.position;
                        Vector3 buffer_opponent_position = collider.gameObject.transform.position;
                        float buffer_z = buffer_my_position.z;
                        buffer_my_position.z = buffer_opponent_position.z;
                        buffer_opponent_position.z = buffer_z;
                        transform.position = buffer_my_position;
                        collider.gameObject.transform.position = buffer_opponent_position;
                    } //transform.position.z > collider.gameObject.transform.position.
                    else if (transform.position.z == collider.gameObject.transform.position.z)
                    {
                        Vector3 buffer_position = transform.position;
                        buffer_position.z -= 0.01F;
                        transform.position = buffer_position;
                    } //transform.position.z == collider.gameObject.transform.position.z
                }
            }
            else
            {
                bool error_add_flag = true;

                for (int i = 0; i < error_objects_.Count; ++i)
                {
                    if (error_objects_[i] == collider.gameObject)
                    {
                        error_add_flag = false;
                    }
                }

                if (error_add_flag == true)
                {
                    error_objects_.Add(collider.gameObject);
                }

                for (int i = 0; i < my_child_number; ++i)
                {
                    transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0, 1);
                } //i
                errored_ = true;
            }

        } //gameObject.tag == "furniture_grid_base" || gameObject.tag == "furniture_grid"

    } //void OnTriggerStay(Collider collider)

    //************************************************************************************************************************************************************************************************

    void OnTriggerExit(Collider collider)
    {
        if (((((collider.gameObject.tag == "furniture_grid"
           || collider.gameObject.tag == "furniture_grid_rugs")
           || collider.gameObject.tag == "furniture_grid_wall")
           || collider.gameObject.tag == "furniture_grid_door")
           || collider.gameObject.tag == "furniture_grid_ceil")
           || collider.gameObject.tag == "furniture_grid_window")
        {
            for (int i = 0; i < collision_objects_.Count; ++i)
            {
                if (collision_objects_[i] == collider.gameObject)
                {
                    collision_objects_.RemoveAt(i);
                    break;
                }
            }

            for (int i = 0; i < error_objects_.Count; ++i)
            {
                if (error_objects_[i] == collider.gameObject)
                {
                    error_objects_.RemoveAt(i);
                    break;
                }
            }
        }
    }

    //************************************************************************************************************************************************************************************************

    void Start()
    {
        bounds_ = new Bounds[transform.childCount];
    } //Start()

    //************************************************************************************************************************************************************************************************

    //他の家具から離れているとき
    void Update()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            bounds_[i] = GetComponents<BoxCollider>()[i].bounds;
        } //i

        for (int i = 0; i < collision_objects_.Count; ++i)
        {
            if (collision_objects_[i] == null)
            {
                collision_objects_.RemoveAt(i);
                break;
            }
        }

        for (int i = 0; i < error_objects_.Count; ++i)
        {
            if (error_objects_[i] == null)
            {
                error_objects_.RemoveAt(i);
                break;
            }
        }

        if (collision_objects_.Count == 0)
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(10, 10, 10, 1);
            } //i
            errored_ = false;
            Vector3 buffer_position = transform.position;
            buffer_position.z = 0F;
            transform.position = buffer_position;

        } //off_trigger_stay_ == true

    } //Update()

    //*************************************************************************************************************************************************************************************************

    //デバッグ用(バウンディングボックス表示)
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < transform.childCount; ++i)
        {
            Gizmos.DrawWireCube(bounds_[i].center, bounds_[i].size);
        } //i

    } //OnDrawGizmos()
}