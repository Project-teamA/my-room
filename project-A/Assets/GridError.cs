//GridError.cs(家具グリッド同士が衝突したら赤く表示, 家具グリッドオブジェクト一つ一つにアタッチされる)
//この仕様は変更する可能性大
//
// 2017年12月12日 更新(菅原涼太)

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridError : MonoBehaviour {

    private bool off_trigger_stay_ = true; //true = 他のオブジェクトと衝突していない，false = 他のオブジェクトと衝突している
    private Bounds[] bounds_;
   
    //他の家具グリッドにぶつかっているとき
    void OnTriggerStay(Collider collider)
    {
        off_trigger_stay_ = false;
        int aaa;
        int bbb;

        if (collider.gameObject.tag == "furniture_grid_base" || collider.gameObject.tag == "furniture_grid")
        {

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

            if (put_flag.All(i => i == true) == true && transform.tag == "furniture_grid") //put_flagの中身がすべてtrueかどうか
            {
                for (int i = 0; i < my_child_number; ++i)
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
                    else if(transform.position.z == collider.gameObject.transform.position.z)
                    {
                        Vector3 buffer_position = transform.position;
                        buffer_position.z -= 0.1F;
                        transform.position = buffer_position;
                    } //transform.position.z == collider.gameObject.transform.position.z

                    if(transform.position.z < collider.gameObject.transform.position.z)
                    {
                        transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(0, 255, 0);
                    } //transform.position.z < collider.gameObject.transform.position.z
                    else
                    {
                        if (transform.GetChild(i).tag == "canput_quad")
                        {
                            transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(0, 255, 255);
                        } //transform.GetChild(i).tag == "canput_quad"
                        else
                        {
                            transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(255, 255, 255);
                        } //transform.GetChild(i).tag != "canput_quad"

                    } //transform.position.z > collider.gameObject.transform.position.z

                } //i

            } //put_flag.All(i => i == true) == true && transform.tag == "furniture_grid"
            else if(placed_flag.All(i => i == true) == true) //placed_flagの中身がすべてtrueかどうか
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
                    buffer_position.z -= 0.1F;
                    collider.gameObject.transform.position = buffer_position;
                } //transform.position.z == collider.gameObject.transform.position.z

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

            } //placed_flag.All(i => i == true) == true
            else
            {
                for (int i = 0; i < my_child_number; ++i)
                {
                    transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);
                } //i

            } //else

        } //gameObject.tag == "furniture_grid_base" || gameObject.tag == "furniture_grid"

    } //void OnTriggerStay(Collider collider)

    //************************************************************************************************************************************************************************************************

    //他の家具グリッドから離れたとき
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "furniture_grid_base" || collider.gameObject.tag == "furniture_grid")
        {
            off_trigger_stay_ = true;
        } //collider.gameObject.tag == "furniture_grid_base" || collider.gameObject.tag == "furniture_grid"

    } //void OnTriggerExit(Collider collider)

    //***********************************************************************************************************************************************************************************************

    void Start()
    {
       bounds_ = new Bounds[transform.childCount];
    } //Start()

    //************************************************************************************************************************************************************************************************

    //他の家具から離れているとき
    void Update()
    {
        
        Debug.Log(off_trigger_stay_);
        for(int i = 0; i< transform.childCount; ++i)
        {
            bounds_[i] = GetComponents<BoxCollider>()[i].bounds;
        } //i

        if (off_trigger_stay_ == true)
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
