//GridError.cs(家具グリッド同士が衝突したら赤く表示, 家具グリッドオブジェクト一つ一つにアタッチされる)
//この仕様は変更する可能性大
//
//8個ほど同じ位置に重なると処理がかなり重い
//
// 2017年12月12日 更新(菅原涼太)

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridError : MonoBehaviour {

    private Bounds[] bounds_;
    private List<GameObject> collision_objects_ = new List<GameObject>(); //衝突しているオブジェクト
    private List<GameObject> error_objects_ = new List<GameObject>(); //エラーの元になっているオブジェクト 

    //他の家具グリッドにぶつかっているとき
    void OnTriggerStay(Collider collider)
    {
        if ( (((((collider.gameObject.tag == "furniture_grid_base" || collider.gameObject.tag == "furniture_grid")
            || collider.gameObject.tag == "furniture_grid_rugs")
            || collider.gameObject.tag == "furniture_grid_wall")
            || collider.gameObject.tag == "furniture_grid_door")
            || collider.gameObject.tag == "furniture_grid_ceil")
            || collider.gameObject.tag == "furniture_grid_window")
        {
            bool collider_add_flag = true;

            for(int i=0; i<collision_objects_.Count; ++i)
            {
              if(collision_objects_[i] == collider.gameObject)
                {
                    collider_add_flag = false;
                }
            }

            if(collider_add_flag == true)
            {
                collision_objects_.Add(collider.gameObject);
            }
           

            int my_child_number = transform.childCount; //自分の子オブジェクトの数
            int opponent_child_number = collider.gameObject.transform.childCount; //相手の子オブジェクトの数
            Vector3[] my_vertices = new Vector3[my_child_number * 4]; //自分の頂点
            Vector3[] opponent_vertices = new Vector3[opponent_child_number * 4]; //相手の頂点
            bool[] put_flag = Enumerable.Repeat<bool>(false, my_vertices.Length).ToArray(); //自分が乗っているか
            bool[] placed_flag = Enumerable.Repeat<bool>(false, opponent_vertices.Length).ToArray(); //自分が乗せているか
            for (int j = 0; j < my_child_number; ++j )
            {
                for(int i = 0; i< 4; ++i)
                {
                    int in_count = 0;
                    my_vertices[j * 4 + i] = transform.rotation * transform.GetChild(j).GetComponent<MeshFilter>().mesh.vertices[i] + transform.position;
                    for(int k = 0; k < opponent_child_number; ++k)
                    {
                      if (collider.gameObject.transform.GetChild(k).tag == "canput_quad" &&
                      collider.gameObject.GetComponents<BoxCollider>()[k].bounds.Contains(my_vertices[j * 4 + i]) == true)
                        {
                            ++in_count;
                        } // collider.gameObject.GetComponents<BoxCollider>()[k].bounds.Contains(my_vertices[j * 4 + i]) == true
                    } //k

                    if(in_count > 0)
                    {
                        put_flag[j * 4 + i] = true;
                    } //in_count > 0
                    else
                    {
                        put_flag[j * 4 + i] = false;
                    } //in_count < 0

                } //j

            } //i

            for (int j = 0; j < opponent_child_number; ++j)
            {
                for (int i = 0; i < 4; ++i)
                {
                    int in_count = 0;
                    opponent_vertices[j * 4 + i] = collider.gameObject.transform.rotation * collider.gameObject.transform.GetChild(j).GetComponent<MeshFilter>().mesh.vertices[i] + collider.gameObject.transform.position;
                    for (int k = 0; k < my_child_number; ++k)
                    {
                     if(transform.GetChild(k).tag == "canput_quad" &&
                      GetComponents<BoxCollider>()[k].bounds.Contains(opponent_vertices[j * 4 + i]) == true)
                        {
                            ++in_count;
                        } //GetComponents<BoxCollider>()[k].bounds.Contains(opponent_vertices[j * 4 + i]) == true

                    } //k

                    if (in_count > 0)
                    {
                        placed_flag[j * 4 + i] = true;
                    } //in_count > 0
                    else
                    {
                        placed_flag[j * 4 + i] = false;
                    } //in_count < 0

                } //j

            } //i

            //true = エラー, false = ok
            bool error_flag = false;

            //0 = どちらでもない, 1 = (自分が)ダウン，2 = (自分が)アップ
            int up_down = 0;


            if( (transform.tag == "furniture_grid_wall" 
               || transform.tag == "furniture_grid_door" )
               || transform.tag == "furniture_grid_window")
            {

            }
            else if ((collider.transform.tag == "furniture_grid_wall"
               || collider.transform.tag == "furniture_grid_door")
               || collider.transform.tag == "furniture_grid_window")
            {

            }
            else if(((put_flag.All(i => i == true) == false && placed_flag.All(i => i == true) == false))) //どっちもずれている場合
            {
                if((transform.tag == "furniture_grid_rugs" ^ collider.transform.tag == "furniture_grid_rugs"))
                {
                    error_flag = false;
                    if (transform.tag == "furniture_grid_rugs")
                    {
                        up_down = 1;
                    }
                    else
                    {
                        up_down = 2;
                    }
                }
                else if ((transform.tag == "furniture_grid_ceil" ^ collider.transform.tag == "furniture_grid_ceil"))
                {
                    error_flag = false;
                    if (transform.tag == "furniture_grid_ceil")
                    {
                        up_down = 2;
                    }
                    else
                    {
                        up_down = 1;
                    }
                }
                else if((transform.tag == "furniture_grid_wall" ^ collider.transform.tag == "furniture_grid_wall"))
                {
                    error_flag = false;
                    if(transform.tag == "furniture_grid_wall")
                    {
                        if (collider.transform.tag == "furniture_grid_door")
                        {
                            error_flag = false; //壁掛けドアとは重なってもよい
                        }
                        else if(collider.transform.tag == "furniture_grid_window")
                        {
                            error_flag = false; //壁掛け窓は高さによる
                        }
                        else
                        {
                            error_flag = false; //壁掛けと家具との重なりは高さによる
                        }
                        up_down = 2; //これも高さによる
                    }
                    else
                    {
                        if (transform.tag == "furniture_grid_door")
                        {
                            error_flag = false; //壁掛けドアとは重なってもよい
                        }
                        else if(transform.tag == "furniture_grid_window")
                        {
                            error_flag = false; //壁掛け窓は高さによる
                        }
                        else
                        {
                            error_flag = false; //高さによる
                        }
                        up_down = 1; //これも高さによる
                    }
                }
                else if((transform.tag == "furniture_grid_wall" && collider.transform.tag == "furniture_grid_wall"))
                {
                    error_flag = false; //壁掛け同士高さで評価して完全にずれていればfalse
                    up_down = 1;//(高さによる)
                }
                else
                {
                    error_flag = true; //エラー
                }
            }
            else if (put_flag.All(i => i == true) == true) //自分が乗っているかどうか
            {
                if(transform.tag == "furniture_grid_rugs")
                {
                    if(collider.transform.tag == "furniture_grid_rugs")
                    {
                        error_flag = true; //敷物が敷物に乗っているのはエラー
                    }
                    else
                    {
                        error_flag = false; //敷物と敷物以外はOK(敷物下)
                        up_down = 1;
                    }
                }
                else if(transform.tag == "furniture_grid_ceil")
                {
                    if (collider.transform.tag == "furniture_grid_ceil")
                    {
                        error_flag = true; //天井掛けと天井掛けが重なっているのはエラー
                    }
                    else
                    {
                        error_flag = false; //天井掛けと天井掛け以外はOK
                        up_down = 2;
                    }
                }
                else if (transform.tag == "furniture_grid_base")
                {
                    if(collider.transform.tag == "furniture_grid_rugs")
                    {
                        error_flag = false; //下にしか置けない家具グリッドと敷物はOK(敷物が下)
                        up_down = 2;
                    }
                    else if(collider.transform.tag == "furniture_grid_ceil")
                    {
                        error_flag = false; //下にしか置けない家具グリッドと天井掛けは高さによる(家具グリッドが下)
                        up_down = 1;
                    }
                    else
                    {
                        error_flag = true; //下にしか置けない家具を家具の上に置けない
                    } 
                }
                else
                {
                    error_flag = false; //その他の家具は高さによる(自分は上に行く)
                    up_down = 2; 
                }

            } //put_flag.All(i => i == true) == true && transform.tag == "furniture_grid"
            else if (placed_flag.All(i => i == true) == true) //自分が乗せているかどうか
            {
                if (transform.tag == "furniture_grid_rugs")
                {
                    if (collider.transform.tag == "furniture_grid_rugs")
                    {
                        error_flag = true; //敷物が敷物に乗っているのはエラー
                    }
                    else
                    {
                        error_flag = false; //敷物と敷物以外はOK(敷物下)
                        up_down = 1;
                    }
                }
                else if (transform.tag == "furniture_grid_ceil")
                {
                    if (collider.transform.tag == "furniture_grid_ceil")
                    {
                        error_flag = true; //天井掛けと天井掛けが重なっているのはエラー
                    }
                    else
                    {
                        error_flag = false; //天井掛けと天井掛け以外はOK
                        up_down = 2;
                    }
                }
                else if(collider.transform.tag == "furniture_grid_base")
                {
                    if(transform.tag == "furniture_grid_rugs")
                    {
                        error_flag = false; //下にしか置けない家具グリッドと敷物はOK(敷物が下)
                        up_down = 1;
                    }
                    else if(transform.tag == "furniture_grid_ceil")
                    {
                        error_flag = false; //下にしか置けない家具グリッドと天井掛けは高さによる(家具グリッドが下)
                        up_down = 2;
                    }
                    else
                    {
                        error_flag = true; //下にしか置けない家具グリッドが上にあるのはダメ
                    }
                }
                else
                {
                    error_flag = false; //その他の家具は高さによる(自分は下に行く)
                    up_down = 1;
                }
            } //placed_flag.All(i => i == true) == true

            if(error_flag == false)
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

                        if (transform.GetChild(i).tag == "canput_quad")
                        {
                            transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(0, 255, 255);
                        } //transform.GetChild(i).tag == "canput_quad"
                        else
                        {
                            transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(255, 255, 255);
                        } //transform.GetChild(i).tag != "canput_quad"

                    } //i
                }

                if(up_down == 1)
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
                else if(up_down == 2)
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
                    transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);
                } //i
            }

        } //gameObject.tag == "furniture_grid_base" || gameObject.tag == "furniture_grid"

    } //void OnTriggerStay(Collider collider)

    //************************************************************************************************************************************************************************************************

    void OnTriggerExit(Collider collider)
    {
        if ((((((collider.gameObject.tag == "furniture_grid_base" || collider.gameObject.tag == "furniture_grid")
           || collider.gameObject.tag == "furniture_grid_rugs")
           || collider.gameObject.tag == "furniture_grid_wall")
           || collider.gameObject.tag == "furniture_grid_door")
           || collider.gameObject.tag == "furniture_grid_ceil")
           || collider.gameObject.tag == "furniture_grid_window")
        {
            for(int i = 0; i < collision_objects_.Count; ++i)
            {
                if(collision_objects_[i] == collider.gameObject)
                {
                    collision_objects_.RemoveAt(i);
                    break;
                }
            }

            for (int i =0; i < error_objects_.Count; ++i)
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
        for(int i = 0; i< transform.childCount; ++i)
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

        Debug.Log(error_objects_.Count);
        if (collision_objects_.Count == 0)
        {
            for (int i = 0; i < transform.childCount; ++i)
            {

                if (transform.GetChild(i).tag == "canput_quad")
                {
                    transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(0, 255, 255);
                } //transform.GetChild(i).tag == "canput_quad"
                else
                {
                    transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(255, 255, 255);
                } //transform.GetChild(i).tag != "canput_quad"

            } //i

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
