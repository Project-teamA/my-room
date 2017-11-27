using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    public GameObject lineObject;
    public GameObject[,] line_x = new GameObject[9, 9];
    public GameObject[,] line_y = new GameObject[9, 9];

    public GameObject sphereObject;
    public GameObject[,] sphere = new GameObject[9, 9];

    float first_point_x;
    float first_point_y;

    float start_point_x;
    float start_point_y;

    int start_point_j;
    int start_point_i;

    float end_point_x;
    float end_point_y;

    int se = 0;
    bool first = false;

    // 左クリックしたオブジェクトを取得する関数(3D)
    public GameObject getClickObject()
    {
        GameObject result = null;

        // 左クリックされた場所のオブジェクトを取得
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit))
            {
                result = hit.collider.gameObject;
            }
        }
        return result;
    }

    public void Instantiate()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (j < 8)
                {
                    line_x[j, i] = Instantiate(lineObject, Vector3.zero, Quaternion.identity);
                    line_x[j, i].name = "line_x[" + j + "," + i + "]";
                    line_x[j, i].GetComponent<LineRenderer>().material.color = new Color(0, 0, 0);
                    LineRenderer renderer_x = line_x[j, i].GetComponent<LineRenderer>();
                    renderer_x.SetWidth(0.02f, 0.02f);
                    renderer_x.SetVertexCount(2);
                    renderer_x.SetPosition(0, new Vector3(j, i, 0));
                    renderer_x.SetPosition(1, new Vector3(j + 1, i, 0));

                }

                if (i < 8)
                {

                    line_y[j, i] = Instantiate(lineObject, Vector3.zero, Quaternion.identity);
                    line_y[j, i].name = "line_y[" + j + "," + i + "]";
                    line_y[j, i].GetComponent<LineRenderer>().material.color = new Color(0, 0, 0);
                    LineRenderer renderer_y = line_y[j, i].GetComponent<LineRenderer>();
                    renderer_y.SetWidth(0.02f, 0.02f);
                    renderer_y.SetVertexCount(2);
                    renderer_y.SetPosition(0, new Vector3(j, i, 0));
                    renderer_y.SetPosition(1, new Vector3(j, i + 1, 0));

                }

                sphere[j, i] = Instantiate(sphereObject, new Vector3(j, i, 0), Quaternion.identity);
                sphere[j, i].name = "sphere[" + j + "," + i + "]";
                sphere[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);
            }

        }
    }

    public void Click_Start()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (start_point_x == sphere[j, i].transform.position.x || start_point_y == sphere[j, i].transform.position.y)
                {
                    if (!(start_point_x == sphere[j, i].transform.position.x && start_point_y == sphere[j, i].transform.position.y))
                    {
                        sphere[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 255);
                    }


                    if (start_point_x == sphere[j, i].transform.position.x)
                    {
                        if (j < 9 && i < 8)
                        {
                            line_y[j, i].GetComponent<LineRenderer>().material.color = new Color(0, 0, 255);
                        }
                    }

                    if (start_point_y == sphere[j, i].transform.position.y)
                    {
                        if (j < 8 && i < 9)
                        {
                            line_x[j, i].GetComponent<LineRenderer>().material.color = new Color(0, 0, 255);
                        }
                    }

                }
            }
        }
    }

    public void Click_End()
    {
        if (sphere[start_point_j, start_point_i].GetComponent<MeshRenderer>().material.color == new Color(0, 255, 255))
        {
            sphere[start_point_j, start_point_i].GetComponent<MeshRenderer>().material.color = new Color(0, 255, 0);
        }

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (sphere[j, i].GetComponent<MeshRenderer>().material.color == new Color(0, 0, 255))
                {
                    sphere[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);
                }
               
                if (j < 9 && i < 8)
                {
                    if (line_y[j, i].GetComponent<LineRenderer>().material.color == new Color(0, 0, 255))
                    {
                        line_y[j, i].GetComponent<LineRenderer>().material.color = new Color(0, 0, 0);
                    }
                }

                if (j < 8 && i < 9)
                {
                    if (line_x[j, i].GetComponent<LineRenderer>().material.color == new Color(0, 0, 255))
                    {
                        line_x[j, i].GetComponent<LineRenderer>().material.color = new Color(0, 0, 0);
                    }
                }

                //xが同じ場合
                if (start_point_x == end_point_x)
                {
                    if (sphere[j, i].transform.position.x == end_point_x)
                    {
                        if (start_point_y < end_point_y)
                        {
                            if (sphere[j, i].transform.position.y > end_point_y)
                            {
                                //if (sphere[j, i].GetComponent<MeshRenderer>().material.color != new Color(255, 0, 0))
                                {
                                    bool line = true;

                                    for (int k = i - 1; k > end_point_y; k--)
                                    {
                                        if (line)
                                        {
                                            if (sphere[j, k].GetComponent<MeshRenderer>().material.color == new Color(255, 0, 0))
                                            {
                                                line = false;
                                            }
                                            else if (sphere[j, k].GetComponent<MeshRenderer>().material.color == new Color(0, 255, 0))
                                            {
                                                line = false;
                                               
                                            }
                                        }
                                    }

                                    if (line == true)
                                    {
                                        if (sphere[j, i].GetComponent<MeshRenderer>().material.color != new Color(0, 255, 0))
                                        {
                                            sphere[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 255);
                                        }
                                        else
                                        {
                                            first = true;
                                        }

                                        line_y[j, i - 1].GetComponent<LineRenderer>().material.color = new Color(0, 0, 255);
                                    }

                                }
                            }
                            else if (start_point_y < sphere[j, i].transform.position.y && sphere[j, i].transform.position.y <= end_point_y)
                            {
                                sphere[j, i].GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);

                                line_y[j, i - 1].GetComponent<LineRenderer>().material.color = new Color(255, 0, 0);
                            }

                        }
                        else if (start_point_y > end_point_y)
                        {
                            if (sphere[j, i].transform.position.y < end_point_y)
                            {
                                //if (sphere[j, i].GetComponent<MeshRenderer>().material.color != new Color(255, 0, 0))
                                {
                                    bool line = true;

                                    for (int k = i + 1; k < end_point_y; k++)
                                    {
                                        if (line)
                                        {
                                            if (sphere[j, k].GetComponent<MeshRenderer>().material.color == new Color(255, 0, 0))
                                            {
                                                line = false;
                                            }
                                            else if (sphere[j, k].GetComponent<MeshRenderer>().material.color == new Color(0, 255, 0))
                                            {
                                                line = false;
                                                
                                            }
                                        }
                                    }

                                    if (line == true)
                                    {
                                        if (sphere[j, i].GetComponent<MeshRenderer>().material.color != new Color(0, 255, 0))
                                        {
                                            sphere[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 255);
                                        }
                                        else
                                        {
                                            first = true;
                                        }

                                        if (j < 9 && i < 8)
                                        {
                                            line_y[j, i].GetComponent<LineRenderer>().material.color = new Color(0, 0, 255);
                                        }
                                    }
                                }
                            }
                            else if (start_point_y > sphere[j, i].transform.position.y && sphere[j, i].transform.position.y >= end_point_y)
                            {
                                sphere[j, i].GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);

                                if (j < 9 && i < 8)
                                {
                                    line_y[j, i].GetComponent<LineRenderer>().material.color = new Color(255, 0, 0);
                                }
                            }
                        }
                    }

                    else if (sphere[j, i].transform.position.y == end_point_y)
                    {
                        if (sphere[j, i].transform.position.x < end_point_x)
                        {
                            //if (sphere[j, i].GetComponent<MeshRenderer>().material.color != new Color(255, 0, 0))
                            {
                                bool line = true;

                                for (int k = j + 1; k < end_point_x; k++)
                                {
                                    if (line)
                                    {
                                        if (sphere[k, i].GetComponent<MeshRenderer>().material.color == new Color(255, 0, 0))
                                        {
                                            line = false;
                                        }
                                        else if (sphere[k, i].GetComponent<MeshRenderer>().material.color == new Color(0, 255, 0))
                                        {
                                            line = false;
                                          
                                        }
                                    }
                                }

                                if (line == true)
                                {
                                    if (sphere[j, i].GetComponent<MeshRenderer>().material.color != new Color(0, 255, 0))
                                    {
                                        sphere[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 255);
                                    }
                                    else
                                    {
                                        first = true;
                                    }

                                    if (j < 8 && i < 9)
                                    {
                                        line_x[j, i].GetComponent<LineRenderer>().material.color = new Color(0, 0, 255);
                                    }
                                }
                            }
                        }
                        else if (sphere[j, i].transform.position.x > end_point_x)
                        {
                            //if (sphere[j, i].GetComponent<MeshRenderer>().material.color != new Color(255, 0, 0))
                            {
                                bool line = true;

                                for (int k = j - 1; k > end_point_x; k--)
                                {
                                    if (line)
                                    {
                                        if (sphere[k, i].GetComponent<MeshRenderer>().material.color == new Color(255, 0, 0))
                                        {
                                            line = false;
                                        }
                                        else if (sphere[k, i].GetComponent<MeshRenderer>().material.color == new Color(0, 255, 0))
                                        {
                                            line = false;
                                            
                                        }
                                    }
                                }

                                if (line == true)
                                {
                                    if (sphere[j, i].GetComponent<MeshRenderer>().material.color != new Color(0, 255, 0))
                                    {
                                        sphere[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 255);
                                    }
                                    else
                                    {
                                        first = true;
                                    }

                                    line_x[j - 1, i].GetComponent<LineRenderer>().material.color = new Color(0, 0, 255);
                                }
                            }
                        }
                    }
                }

                //yが同じ場合
                else if (start_point_y == end_point_y)
                {
                    if (sphere[j, i].transform.position.y == end_point_y)
                    {
                        if (start_point_x > end_point_x)
                        {
                            if (sphere[j, i].transform.position.x < end_point_x)
                            {
                                

                                //if (sphere[j, i].GetComponent<MeshRenderer>().material.color != new Color(255, 0, 0))
                                {
                                    bool line = true;

                                    for (int k = j + 1; k < end_point_x; k++)
                                    {
                                        Debug.Log(sphere[k, i].GetComponent<MeshRenderer>().material.color);

                                        if (line)
                                        {
                                            if (sphere[k, i].GetComponent<MeshRenderer>().material.color == new Color(255, 0, 0))
                                            {
                                                line = false;
                                                Debug.Log("new Color(255, 0, 0)");
                                            }
                                            else if (sphere[k, i].GetComponent<MeshRenderer>().material.color == new Color(0, 255, 0))
                                            {
                                                line = false;
                                                Debug.Log("new Color(0, 255, 0)");
                                            }
                                        }
                                    }

                                    if (line == true)
                                    {
                                        if (sphere[j, i].GetComponent<MeshRenderer>().material.color != new Color(0, 255, 0))
                                        {
                                            sphere[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 255);
                                        }
                                        else
                                        {
                                            first = true;
                                        }

                                        if (j < 8 && i < 9)
                                        {
                                            line_x[j, i].GetComponent<LineRenderer>().material.color = new Color(0, 0, 255);
                                        }
                                    }
                                }
                            }
                            else if (start_point_x > sphere[j, i].transform.position.x && sphere[j, i].transform.position.x >= end_point_x)
                            {
                                sphere[j, i].GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);

                                if (j < 8 && i < 9)
                                {
                                    line_x[j, i].GetComponent<LineRenderer>().material.color = new Color(255, 0, 0);
                                }
                            }
                        }
                        else if (start_point_x < end_point_x)
                        {
                            if (sphere[j, i].transform.position.x > end_point_x)
                            {
                                //if (sphere[j, i].GetComponent<MeshRenderer>().material.color != new Color(255, 0, 0))
                                {
                                    bool line = true;

                                    for (int k = j - 1; k > end_point_x; k--)
                                    {
                                        

                                        if (line)
                                        {
                                            if (sphere[k, i].GetComponent<MeshRenderer>().material.color == new Color(255, 0, 0))
                                            {
                                                line = false;
                                            }
                                            else if (sphere[k, i].GetComponent<MeshRenderer>().material.color == new Color(0, 255, 0))
                                            {
                                                line = false;
                                             
                                            }
                                        }
                                    }

                                    if (line == true)
                                    {
                                        if (sphere[j, i].GetComponent<MeshRenderer>().material.color != new Color(0, 255, 0))
                                        {
                                            sphere[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 255);
                                        }
                                        else
                                        {
                                            first = true;
                                        }

                                        line_x[j - 1, i].GetComponent<LineRenderer>().material.color = new Color(0, 0, 255);
                                    }

                                }
                            }
                            else if (start_point_x < sphere[j, i].transform.position.x && sphere[j, i].transform.position.x <= end_point_x)
                            {
                                sphere[j, i].GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);

                                line_x[j - 1, i].GetComponent<LineRenderer>().material.color = new Color(255, 0, 0);
                            }

                        }
                        
                    }

                    else if (sphere[j, i].transform.position.x == end_point_x)
                    {
                        if (sphere[j, i].transform.position.y < end_point_y)
                        {
                            //if (sphere[j, i].GetComponent<MeshRenderer>().material.color != new Color(255, 0, 0))
                            {
                                bool line = true;

                                for (int k = i + 1; k < end_point_y; k++)
                                {
                                    if (line)
                                    {
                                        if (sphere[j, k].GetComponent<MeshRenderer>().material.color == new Color(255, 0, 0))
                                        {
                                            line = false;
                                        }
                                        else if (sphere[j, k].GetComponent<MeshRenderer>().material.color == new Color(0, 255, 0))
                                        {
                                            line = false;
                                            
                                        }
                                    }
                                }

                                if (line == true)
                                {
                                    if (sphere[j, i].GetComponent<MeshRenderer>().material.color != new Color(0, 255, 0))
                                    {
                                        sphere[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 255);
                                    }
                                    else
                                    {
                                        first = true;
                                    }

                                    if (j < 9 && i < 8)
                                    {
                                        line_y[j, i].GetComponent<LineRenderer>().material.color = new Color(0, 0, 255);
                                    }
                                }
                            }
                        }
                        else if (sphere[j, i].transform.position.y > end_point_y)
                        {
                            //if (sphere[j, i].GetComponent<MeshRenderer>().material.color != new Color(255, 0, 0))
                            {
                                bool line = true;

                                for (int k = i - 1; k > end_point_y; k--)
                                {
                                    if (line)
                                    {
                                        if (sphere[j, k].GetComponent<MeshRenderer>().material.color == new Color(255, 0, 0))
                                        {
                                            line = false;
                                        }
                                        else if (sphere[j, k].GetComponent<MeshRenderer>().material.color == new Color(0, 255, 0))
                                        {
                                            line = false;
                                            
                                        }
                                    }
                                }

                                if (line == true)
                                {
                                    if (sphere[j, i].GetComponent<MeshRenderer>().material.color != new Color(0, 255, 0))
                                    {
                                        sphere[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 255);
                                    }
                                    else
                                    {
                                        first = true;
                                    }

                                    line_y[j, i - 1].GetComponent<LineRenderer>().material.color = new Color(0, 0, 255);
                                }
                            }
                        }
                    }
                }

                if (sphere[j, i].GetComponent<MeshRenderer>().material.color == new Color(0, 255, 0))
                {
                    start_point_j = j;
                    start_point_i = i;
                }             

            }
        }

        if (first == true)
        {
            Debug.Log("first == true");
            sphere[start_point_j, start_point_i].GetComponent<MeshRenderer>().material.color = new Color(0, 255, 255);
            first = false;
        }
       
    }

    // Use this for initialization
    void Start()
    {
        Instantiate();
    }

    // Update is called once per frame
    void Update()
    {
        switch (se)
        {
            case 0:

                if (getClickObject() != null)
                {
                    first_point_x = getClickObject().transform.position.x;
                    first_point_y = getClickObject().transform.position.y;

                    start_point_x = first_point_x;
                    start_point_y = first_point_y;

                    getClickObject().GetComponent<MeshRenderer>().material.color = new Color(0, 255, 0);

                    Click_Start();

                    se = 1;
                }

                break;

            case 1:

                if (getClickObject() != null)
                {
                    if (getClickObject().GetComponent<MeshRenderer>().material.color == new Color(0, 0, 255) ||
                        getClickObject().GetComponent<MeshRenderer>().material.color == new Color(0, 255, 255))
                    {
                        getClickObject().GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);

                        end_point_x = getClickObject().transform.position.x;
                        end_point_y = getClickObject().transform.position.y;

                        Click_End();

                        start_point_x = end_point_x;
                        start_point_y = end_point_y;

                    }
                }
                break;
        }
    }
}
