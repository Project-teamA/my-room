using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public GameObject lineObject;
    public GameObject sphereObject;

    int Grid_number = 30;
    float Grid_density = 2.0f;

    public GameObject[,] line_x;
    public GameObject[,] line_y;
    public GameObject[,] sphere;

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

    public Text Finish_text;
    public Text Square_text;

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
        for (int i = 0; i < Grid_number; i++)
        {
            for (int j = 0; j < Grid_number; j++)
            {
                if (j < Grid_number - 1)
                {
                    line_x[j, i] = Instantiate(lineObject, Vector3.zero, Quaternion.identity);
                    line_x[j, i].name = "line_x[" + j + "," + i + "]";
                    line_x[j, i].GetComponent<LineRenderer>().material.color = new Color(0, 0, 0);
                    LineRenderer renderer_x = line_x[j, i].GetComponent<LineRenderer>();
                    renderer_x.SetWidth(0.02f, 0.02f);
                    renderer_x.SetVertexCount(2);
                    renderer_x.SetPosition(0, new Vector3(j / Grid_density, i / Grid_density, 0));
                    renderer_x.SetPosition(1, new Vector3((j + 1) / Grid_density, i / Grid_density, 0));

                }

                if (i < Grid_number - 1)
                {
                    line_y[j, i] = Instantiate(lineObject, Vector3.zero, Quaternion.identity);
                    line_y[j, i].name = "line_y[" + j + "," + i + "]";
                    line_y[j, i].GetComponent<LineRenderer>().material.color = new Color(0, 0, 0);
                    LineRenderer renderer_y = line_y[j, i].GetComponent<LineRenderer>();
                    renderer_y.SetWidth(0.02f, 0.02f);
                    renderer_y.SetVertexCount(2);
                    renderer_y.SetPosition(0, new Vector3(j / Grid_density, i / Grid_density, 0));
                    renderer_y.SetPosition(1, new Vector3(j / Grid_density, (i + 1) / Grid_density, 0));

                }

                sphere[j, i] = Instantiate(sphereObject, new Vector3(j / Grid_density, i / Grid_density, 0), Quaternion.identity);
                sphere[j, i].name = "sphere[" + j + "," + i + "]";
                sphere[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);
            }
        }
    }

    public void Click_Start()
    {
        for (int i = 0; i < Grid_number; i++)
        {
            for (int j = 0; j < Grid_number; j++)
            {
                if (start_point_x == sphere[j, i].transform.position.x || start_point_y == sphere[j, i].transform.position.y)
                {
                    if (!(start_point_x == sphere[j, i].transform.position.x && start_point_y == sphere[j, i].transform.position.y))
                    {
                        sphere[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 255);
                    }


                    if (start_point_x == sphere[j, i].transform.position.x)
                    {
                        if (j < Grid_number && i < Grid_number - 1)
                        {
                            line_y[j, i].GetComponent<LineRenderer>().material.color = new Color(0, 0, 255);
                        }
                    }

                    if (start_point_y == sphere[j, i].transform.position.y)
                    {
                        if (j < Grid_number - 1 && i < Grid_number)
                        {
                            line_x[j, i].GetComponent<LineRenderer>().material.color = new Color(0, 0, 255);
                        }
                    }

                }
            }
        }
    }

    public void Click()
    {
        if (sphere[start_point_j, start_point_i].GetComponent<MeshRenderer>().material.color == new Color(0, 255, 255))
        {
            sphere[start_point_j, start_point_i].GetComponent<MeshRenderer>().material.color = new Color(0, 255, 0);
        }

        for (int i = 0; i < Grid_number; i++)
        {
            for (int j = 0; j < Grid_number; j++)
            {
                if (sphere[j, i].GetComponent<MeshRenderer>().material.color == new Color(0, 0, 255))
                {
                    sphere[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);
                }
               
                if (j < Grid_number && i < Grid_number - 1)
                {
                    if (line_y[j, i].GetComponent<LineRenderer>().material.color == new Color(0, 0, 255))
                    {
                        line_y[j, i].GetComponent<LineRenderer>().material.color = new Color(0, 0, 0);
                    }
                }

                if (j < Grid_number - 1 && i < Grid_number)
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
                                if (sphere[j, i].GetComponent<MeshRenderer>().material.color != new Color(255, 0, 0))
                                {
                                    bool line = true;                                  
                                 
                                    for (int k = i - 1; k > end_point_y * Grid_density; k--)
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
                                if (sphere[j, i].GetComponent<MeshRenderer>().material.color != new Color(255, 0, 0))
                                {
                                    bool line = true;

                                    for (int k = i + 1; k < end_point_y * Grid_density; k++)
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

                                        if (j < Grid_number && i < Grid_number - 1)
                                        {
                                            line_y[j, i].GetComponent<LineRenderer>().material.color = new Color(0, 0, 255);
                                        }
                                    }
                                }
                            }
                            else if (start_point_y > sphere[j, i].transform.position.y && sphere[j, i].transform.position.y >= end_point_y)
                            {
                                sphere[j, i].GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);

                                if (j < Grid_number && i < Grid_number - 1)
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
                            if (sphere[j, i].GetComponent<MeshRenderer>().material.color != new Color(255, 0, 0))
                            {
                                bool line = true;

                                for (int k = j + 1; k < end_point_x * Grid_density; k++)
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

                                    if (j < Grid_number - 1 && i < Grid_number)
                                    {
                                        line_x[j, i].GetComponent<LineRenderer>().material.color = new Color(0, 0, 255);
                                    }
                                }
                            }
                        }
                        else if (sphere[j, i].transform.position.x > end_point_x)
                        {
                            if (sphere[j, i].GetComponent<MeshRenderer>().material.color != new Color(255, 0, 0))
                            {
                                bool line = true;

                                for (int k = j - 1; k > end_point_x * Grid_density; k--)
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
                                if (sphere[j, i].GetComponent<MeshRenderer>().material.color != new Color(255, 0, 0))
                                {
                                    bool line = true;

                                    for (int k = j + 1; k < end_point_x * Grid_density; k++)
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

                                        if (j < Grid_number - 1 && i < Grid_number)
                                        {
                                            line_x[j, i].GetComponent<LineRenderer>().material.color = new Color(0, 0, 255);
                                        }
                                    }
                                }
                            }
                            else if (start_point_x > sphere[j, i].transform.position.x && sphere[j, i].transform.position.x >= end_point_x)
                            {
                                sphere[j, i].GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);

                                if (j < Grid_number - 1 && i < Grid_number)
                                {
                                    line_x[j, i].GetComponent<LineRenderer>().material.color = new Color(255, 0, 0);
                                }
                            }
                        }
                        else if (start_point_x < end_point_x)
                        {
                            if (sphere[j, i].transform.position.x > end_point_x)
                            {
                                if (sphere[j, i].GetComponent<MeshRenderer>().material.color != new Color(255, 0, 0))
                                {
                                    bool line = true;

                                    for (int k = j - 1; k > end_point_x * Grid_density; k--)
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
                            if (sphere[j, i].GetComponent<MeshRenderer>().material.color != new Color(255, 0, 0))
                            {
                                bool line = true;

                                for (int k = i + 1; k < end_point_y * Grid_density; k++)
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

                                    if (j < Grid_number && i < Grid_number - 1)
                                    {
                                        line_y[j, i].GetComponent<LineRenderer>().material.color = new Color(0, 0, 255);
                                    }
                                }
                            }
                        }
                        else if (sphere[j, i].transform.position.y > end_point_y)
                        {
                            if (sphere[j, i].GetComponent<MeshRenderer>().material.color != new Color(255, 0, 0))
                            {
                                bool line = true;

                                for (int k = i - 1; k > end_point_y * Grid_density; k--)
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
            sphere[start_point_j, start_point_i].GetComponent<MeshRenderer>().material.color = new Color(0, 255, 255);
            first = false;
        }
       
    }

    public void Click_End()
    {
        for (int i = 0; i < Grid_number; i++)
        {
            for (int j = 0; j < Grid_number; j++)
            {
                if (sphere[j, i].GetComponent<MeshRenderer>().material.color == new Color(0, 0, 255))
                {
                    sphere[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);
                }

                if (j < Grid_number && i < Grid_number - 1)
                {
                    if (line_y[j, i].GetComponent<LineRenderer>().material.color == new Color(0, 0, 255))
                    {
                        line_y[j, i].GetComponent<LineRenderer>().material.color = new Color(0, 0, 0);
                    }
                }

                if (j < Grid_number - 1 && i < Grid_number)
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
                            if (start_point_y < sphere[j, i].transform.position.y && sphere[j, i].transform.position.y <= end_point_y)
                            {
                                sphere[j, i].GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);

                                line_y[j, i - 1].GetComponent<LineRenderer>().material.color = new Color(255, 0, 0);
                            }

                        }
                        else if (start_point_y > end_point_y)
                        {
                            if (start_point_y > sphere[j, i].transform.position.y && sphere[j, i].transform.position.y >= end_point_y)
                            {
                                sphere[j, i].GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);

                                if (j < Grid_number && i < Grid_number - 1)
                                {
                                    line_y[j, i].GetComponent<LineRenderer>().material.color = new Color(255, 0, 0);
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
                            if (start_point_x > sphere[j, i].transform.position.x && sphere[j, i].transform.position.x >= end_point_x)
                            {
                                sphere[j, i].GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);

                                if (j < Grid_number - 1 && i < Grid_number)
                                {
                                    line_x[j, i].GetComponent<LineRenderer>().material.color = new Color(255, 0, 0);
                                }
                            }
                        }
                        else if (start_point_x < end_point_x)
                        {
                            if (start_point_x < sphere[j, i].transform.position.x && sphere[j, i].transform.position.x <= end_point_x)
                            {
                                sphere[j, i].GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);

                                line_x[j - 1, i].GetComponent<LineRenderer>().material.color = new Color(255, 0, 0);
                            }
                        }
                    }                    
                }               
            }
        }
    }

    public void Square_measure()
    {
        bool mode = false;
        int Square = 0;

        for (int i = 0; i < Grid_number; i++)
        {
            for (int j = 0; j < Grid_number; j++)
            {
                if (j < Grid_number && i < Grid_number - 1)
                {
                    if (mode == false)
                    {
                        if (line_y[j, i].GetComponent<LineRenderer>().material.color == new Color(255, 0, 0))
                        {
                            mode = true;
                            Square++;
                        }
                    }
                    else if (mode == true)
                    {
                        if (line_y[j, i].GetComponent<LineRenderer>().material.color == new Color(255, 0, 0))
                        {
                            if (line_x[j - 1, i + 1].GetComponent<LineRenderer>().material.color != new Color(255, 0, 0))
                            {
                                line_x[j - 1, i + 1].GetComponent<LineRenderer>().material.color = new Color(255, 255, 0);
                            }

                            mode = false;
                        }
                        else if (line_y[j, i].GetComponent<LineRenderer>().material.color == new Color(0, 0, 0))
                        {
                            Square++;

                            if(sphere[j, i + 1].GetComponent<MeshRenderer>().material.color != new Color(255, 0, 0))
                            {
                                sphere[j, i + 1].GetComponent<MeshRenderer>().material.color = new Color(255, 255, 0);
                            }

                            if (line_x[j - 1, i + 1].GetComponent<LineRenderer>().material.color != new Color(255, 0, 0))
                            {
                                line_x[j - 1, i + 1].GetComponent<LineRenderer>().material.color = new Color(255, 255, 0);
                            }

                            line_y[j, i].GetComponent<LineRenderer>().material.color = new Color(255, 255, 0);
                        }
                    }
                   
                }
            }
        }

        Square_text.text = Square.ToString();

    }

    // Use this for initialization
    void Start()
    {
        line_x = new GameObject[Grid_number, Grid_number];
        line_y = new GameObject[Grid_number, Grid_number];
        sphere = new GameObject[Grid_number, Grid_number];

        Finish_text.enabled = false;

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
                    if (getClickObject().GetComponent<MeshRenderer>().material.color == new Color(0, 0, 255))
                    {
                        getClickObject().GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);

                        end_point_x = getClickObject().transform.position.x;
                        end_point_y = getClickObject().transform.position.y;                       

                        Click();

                        start_point_x = end_point_x;
                        start_point_y = end_point_y;

                    }
                    else if (getClickObject().GetComponent<MeshRenderer>().material.color == new Color(0, 255, 255))
                    {
                        getClickObject().GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);

                        end_point_x = getClickObject().transform.position.x;
                        end_point_y = getClickObject().transform.position.y;

                        Click_End();

                        Finish_text.enabled = true;

                        Square_measure();                        
                    }
                }
                break;
        }
    }
}
