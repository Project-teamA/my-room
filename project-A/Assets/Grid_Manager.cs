using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wall
{
    public float start;
    public float end;
    public float other;
}

public class Grid_Manager : MonoBehaviour
{
    public GameObject lineObject;
    public GameObject pointsObject;
    public GameObject wall;

    public int Grid_number = 10;
    public float Grid_density = 0.5f;

    public GameObject[,] line_x;
    public GameObject[,] line_y;
    public GameObject[,] points;

    public GameObject Line_x;
    public GameObject Line_y;
    public GameObject Points;

    List<GameObject> Wall_x = new List<GameObject>();
    List<GameObject> Wall_y = new List<GameObject>();

    List<wall> wall_x = new List<wall>();
    List<wall> wall_y = new List<wall>();

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
                    line_x[j, i].transform.parent = Line_x.transform;
                    line_x[j, i].name = "line_x[" + j + "," + i + "]";
                    line_x[j, i].GetComponent<LineRenderer>().material.color = new Color(0, 0, 0);
                    LineRenderer renderer_x = line_x[j, i].GetComponent<LineRenderer>();
                    renderer_x.SetWidth(0.02f, 0.02f);
                    renderer_x.SetVertexCount(2);
                    renderer_x.SetPosition(0, new Vector3(j * Grid_density, i * Grid_density, 0));
                    renderer_x.SetPosition(1, new Vector3((j + 1) * Grid_density, i * Grid_density, 0));

                }

                if (i < Grid_number - 1)
                {
                    line_y[j, i] = Instantiate(lineObject, Vector3.zero, Quaternion.identity);
                    line_y[j, i].transform.parent = Line_y.transform;
                    line_y[j, i].name = "line_y[" + j + "," + i + "]";
                    line_y[j, i].GetComponent<LineRenderer>().material.color = new Color(0, 0, 0);
                    LineRenderer renderer_y = line_y[j, i].GetComponent<LineRenderer>();
                    renderer_y.SetWidth(0.02f, 0.02f);
                    renderer_y.SetVertexCount(2);
                    renderer_y.SetPosition(0, new Vector3(j * Grid_density, i * Grid_density, 0));
                    renderer_y.SetPosition(1, new Vector3(j * Grid_density, (i + 1) * Grid_density, 0));

                }

                points[j, i] = Instantiate(pointsObject, new Vector3(j * Grid_density, i * Grid_density, 0), Quaternion.identity);
                points[j, i].transform.parent = Points.transform;
                points[j, i].name = "points[" + j + "," + i + "]";
                points[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);
            }
        }
    }

    public void Click_Start()
    {
        for (int i = 0; i < Grid_number; i++)
        {
            for (int j = 0; j < Grid_number; j++)
            {
                if (start_point_x == points[j, i].transform.position.x || start_point_y == points[j, i].transform.position.y)
                {
                    if (!(start_point_x == points[j, i].transform.position.x && start_point_y == points[j, i].transform.position.y))
                    {
                        points[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 255);
                    }


                    if (start_point_x == points[j, i].transform.position.x)
                    {
                        if (j < Grid_number && i < Grid_number - 1)
                        {
                            line_y[j, i].GetComponent<LineRenderer>().material.color = new Color(0, 0, 255);
                        }
                    }

                    if (start_point_y == points[j, i].transform.position.y)
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
        if (points[start_point_j, start_point_i].GetComponent<MeshRenderer>().material.color == new Color(0, 255, 255))
        {
            points[start_point_j, start_point_i].GetComponent<MeshRenderer>().material.color = new Color(0, 255, 0);
        }

        for (int i = 0; i < Grid_number; i++)
        {
            for (int j = 0; j < Grid_number; j++)
            {
                if (points[j, i].GetComponent<MeshRenderer>().material.color == new Color(0, 0, 255))
                {
                    points[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);
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
                    if (points[j, i].transform.position.x == end_point_x)
                    {
                        if (start_point_y < end_point_y)
                        {
                            if (points[j, i].transform.position.y > end_point_y)
                            {
                                if (points[j, i].GetComponent<MeshRenderer>().material.color != new Color(255, 0, 0))
                                {
                                    bool line = true;                                  
                                 
                                    for (int k = i - 1; k > end_point_y / Grid_density; k--)
                                    {                                      
                                        if (line)
                                        {
                                            if (points[j, k].GetComponent<MeshRenderer>().material.color == new Color(255, 0, 0))
                                            {                                              
                                                line = false;
                                            }
                                            else if (points[j, k].GetComponent<MeshRenderer>().material.color == new Color(0, 255, 0))
                                            {                                                
                                                line = false;

                                            }
                                        }
                                    }

                                    if (line == true)
                                    {
                                        if (points[j, i].GetComponent<MeshRenderer>().material.color != new Color(0, 255, 0))
                                        {
                                            points[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 255);
                                        }
                                        else
                                        {
                                            first = true;
                                        }

                                        line_y[j, i - 1].GetComponent<LineRenderer>().material.color = new Color(0, 0, 255);
                                    }

                                }
                            }
                            else if (start_point_y < points[j, i].transform.position.y && points[j, i].transform.position.y <= end_point_y)
                            {
                                points[j, i].GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);

                                line_y[j, i - 1].GetComponent<LineRenderer>().material.color = new Color(255, 0, 0);
                            }

                        }
                        else if (start_point_y > end_point_y)
                        {
                            if (points[j, i].transform.position.y < end_point_y)
                            {
                                if (points[j, i].GetComponent<MeshRenderer>().material.color != new Color(255, 0, 0))
                                {
                                    bool line = true;

                                    for (int k = i + 1; k < end_point_y / Grid_density; k++)
                                    {
                                        if (line)
                                        {
                                            if (points[j, k].GetComponent<MeshRenderer>().material.color == new Color(255, 0, 0))
                                            {
                                                line = false;
                                            }
                                            else if (points[j, k].GetComponent<MeshRenderer>().material.color == new Color(0, 255, 0))
                                            {
                                                line = false;
                                                
                                            }
                                        }
                                    }

                                    if (line == true)
                                    {
                                        if (points[j, i].GetComponent<MeshRenderer>().material.color != new Color(0, 255, 0))
                                        {
                                            points[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 255);
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
                            else if (start_point_y > points[j, i].transform.position.y && points[j, i].transform.position.y >= end_point_y)
                            {
                                points[j, i].GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);

                                if (j < Grid_number && i < Grid_number - 1)
                                {
                                    line_y[j, i].GetComponent<LineRenderer>().material.color = new Color(255, 0, 0);
                                }
                            }
                        }
                    }

                    else if (points[j, i].transform.position.y == end_point_y)
                    {
                        if (points[j, i].transform.position.x < end_point_x)
                        {
                            if (points[j, i].GetComponent<MeshRenderer>().material.color != new Color(255, 0, 0))
                            {
                                bool line = true;

                                for (int k = j + 1; k < end_point_x / Grid_density; k++)
                                {
                                    if (line)
                                    {
                                        if (points[k, i].GetComponent<MeshRenderer>().material.color == new Color(255, 0, 0))
                                        {
                                            line = false;
                                        }
                                        else if (points[k, i].GetComponent<MeshRenderer>().material.color == new Color(0, 255, 0))
                                        {
                                            line = false;
                                          
                                        }
                                    }
                                }

                                if (line == true)
                                {
                                    if (points[j, i].GetComponent<MeshRenderer>().material.color != new Color(0, 255, 0))
                                    {
                                        points[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 255);
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
                        else if (points[j, i].transform.position.x > end_point_x)
                        {
                            if (points[j, i].GetComponent<MeshRenderer>().material.color != new Color(255, 0, 0))
                            {
                                bool line = true;

                                for (int k = j - 1; k > end_point_x / Grid_density; k--)
                                {
                                    if (line)
                                    {
                                        if (points[k, i].GetComponent<MeshRenderer>().material.color == new Color(255, 0, 0))
                                        {
                                            line = false;
                                        }
                                        else if (points[k, i].GetComponent<MeshRenderer>().material.color == new Color(0, 255, 0))
                                        {
                                            line = false;
                                            
                                        }
                                    }
                                }

                                if (line == true)
                                {
                                    if (points[j, i].GetComponent<MeshRenderer>().material.color != new Color(0, 255, 0))
                                    {
                                        points[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 255);
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
                    if (points[j, i].transform.position.y == end_point_y)
                    {
                        if (start_point_x > end_point_x)
                        {
                            if (points[j, i].transform.position.x < end_point_x)
                            {                                
                                if (points[j, i].GetComponent<MeshRenderer>().material.color != new Color(255, 0, 0))
                                {
                                    bool line = true;

                                    for (int k = j + 1; k < end_point_x / Grid_density; k++)
                                    {                                        
                                        if (line)
                                        {
                                            if (points[k, i].GetComponent<MeshRenderer>().material.color == new Color(255, 0, 0))
                                            {
                                                line = false;
                                            }
                                            else if (points[k, i].GetComponent<MeshRenderer>().material.color == new Color(0, 255, 0))
                                            {
                                                line = false;
                                            }
                                        }
                                    }

                                    if (line == true)
                                    {
                                        if (points[j, i].GetComponent<MeshRenderer>().material.color != new Color(0, 255, 0))
                                        {
                                            points[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 255);
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
                            else if (start_point_x > points[j, i].transform.position.x && points[j, i].transform.position.x >= end_point_x)
                            {
                                points[j, i].GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);

                                if (j < Grid_number - 1 && i < Grid_number)
                                {
                                    line_x[j, i].GetComponent<LineRenderer>().material.color = new Color(255, 0, 0);
                                }
                            }
                        }
                        else if (start_point_x < end_point_x)
                        {
                            if (points[j, i].transform.position.x > end_point_x)
                            {
                                if (points[j, i].GetComponent<MeshRenderer>().material.color != new Color(255, 0, 0))
                                {
                                    bool line = true;

                                    for (int k = j - 1; k > end_point_x / Grid_density; k--)
                                    {                                        
                                        if (line)
                                        {
                                            if (points[k, i].GetComponent<MeshRenderer>().material.color == new Color(255, 0, 0))
                                            {
                                                line = false;
                                            }
                                            else if (points[k, i].GetComponent<MeshRenderer>().material.color == new Color(0, 255, 0))
                                            {
                                                line = false;
                                             
                                            }
                                        }
                                    }

                                    if (line == true)
                                    {
                                        if (points[j, i].GetComponent<MeshRenderer>().material.color != new Color(0, 255, 0))
                                        {
                                            points[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 255);
                                        }
                                        else
                                        {
                                            first = true;
                                        }

                                        line_x[j - 1, i].GetComponent<LineRenderer>().material.color = new Color(0, 0, 255);
                                    }

                                }
                            }
                            else if (start_point_x < points[j, i].transform.position.x && points[j, i].transform.position.x <= end_point_x)
                            {
                                points[j, i].GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);

                                line_x[j - 1, i].GetComponent<LineRenderer>().material.color = new Color(255, 0, 0);
                            }

                        }
                        
                    }

                    else if (points[j, i].transform.position.x == end_point_x)
                    {
                        if (points[j, i].transform.position.y < end_point_y)
                        {
                            if (points[j, i].GetComponent<MeshRenderer>().material.color != new Color(255, 0, 0))
                            {
                                bool line = true;

                                for (int k = i + 1; k < end_point_y / Grid_density; k++)
                                {
                                    if (line)
                                    {
                                        if (points[j, k].GetComponent<MeshRenderer>().material.color == new Color(255, 0, 0))
                                        {
                                            line = false;
                                        }
                                        else if (points[j, k].GetComponent<MeshRenderer>().material.color == new Color(0, 255, 0))
                                        {
                                            line = false;
                                            
                                        }
                                    }
                                }

                                if (line == true)
                                {
                                    if (points[j, i].GetComponent<MeshRenderer>().material.color != new Color(0, 255, 0))
                                    {
                                        points[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 255);
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
                        else if (points[j, i].transform.position.y > end_point_y)
                        {
                            if (points[j, i].GetComponent<MeshRenderer>().material.color != new Color(255, 0, 0))
                            {
                                bool line = true;

                                for (int k = i - 1; k > end_point_y / Grid_density; k--)
                                {
                                    if (line)
                                    {
                                        if (points[j, k].GetComponent<MeshRenderer>().material.color == new Color(255, 0, 0))
                                        {
                                            line = false;
                                        }
                                        else if (points[j, k].GetComponent<MeshRenderer>().material.color == new Color(0, 255, 0))
                                        {
                                            line = false;
                                            
                                        }
                                    }
                                }

                                if (line == true)
                                {
                                    if (points[j, i].GetComponent<MeshRenderer>().material.color != new Color(0, 255, 0))
                                    {
                                        points[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 255);
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

                if (points[j, i].GetComponent<MeshRenderer>().material.color == new Color(0, 255, 0))
                {
                    start_point_j = j;
                    start_point_i = i;
                }             

            }
        }

        if (first == true)
        {
            points[start_point_j, start_point_i].GetComponent<MeshRenderer>().material.color = new Color(0, 255, 255);
            first = false;
        }
       
    }

    public void Click_End()
    {
        for (int i = 0; i < Grid_number; i++)
        {
            for (int j = 0; j < Grid_number; j++)
            {
                if (points[j, i].GetComponent<MeshRenderer>().material.color == new Color(0, 0, 255))
                {
                    points[j, i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);
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
                    if (points[j, i].transform.position.x == end_point_x)
                    {
                        if (start_point_y < end_point_y)
                        {                            
                            if (start_point_y < points[j, i].transform.position.y && points[j, i].transform.position.y <= end_point_y)
                            {
                                points[j, i].GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);

                                line_y[j, i - 1].GetComponent<LineRenderer>().material.color = new Color(255, 0, 0);
                            }

                        }
                        else if (start_point_y > end_point_y)
                        {
                            if (start_point_y > points[j, i].transform.position.y && points[j, i].transform.position.y >= end_point_y)
                            {
                                points[j, i].GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);

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
                    if (points[j, i].transform.position.y == end_point_y)
                    {
                        if (start_point_x > end_point_x)
                        {
                            if (start_point_x > points[j, i].transform.position.x && points[j, i].transform.position.x >= end_point_x)
                            {
                                points[j, i].GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);

                                if (j < Grid_number - 1 && i < Grid_number)
                                {
                                    line_x[j, i].GetComponent<LineRenderer>().material.color = new Color(255, 0, 0);
                                }
                            }
                        }
                        else if (start_point_x < end_point_x)
                        {
                            if (start_point_x < points[j, i].transform.position.x && points[j, i].transform.position.x <= end_point_x)
                            {
                                points[j, i].GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);

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

                            if(points[j, i + 1].GetComponent<MeshRenderer>().material.color != new Color(255, 0, 0))
                            {
                                points[j, i + 1].GetComponent<MeshRenderer>().material.color = new Color(255, 255, 0);
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

    public void Search_Wall_x()
    {
        bool mode = false;

        int ID = 0;

        for (int i = 0; i < Grid_number; i++)
        {
            for (int j = 0; j < Grid_number; j++)
            {
                if (j < Grid_number - 1 && i < Grid_number)
                {
                    if (mode == false)
                    {
                        if (line_x[j, i].GetComponent<LineRenderer>().material.color == new Color(255, 0, 0))
                        {                           
                            wall temp = new wall();  
                            
                            wall_x.Add(temp);

                            wall_x[ID].start = j * Grid_density;
                            wall_x[ID].other = i * Grid_density;

                            mode = true;
                        }
                    }
                    else if (mode == true)
                    {
                        if (line_x[j, i].GetComponent<LineRenderer>().material.color != new Color(255, 0, 0))
                        {                
                            wall_x[ID].end = j * Grid_density;

                            ID++;

                            mode = false;
                        }
                    }

                }
            }

            if (mode == true)
            {
                wall_x[ID].end = Grid_number - 1;

                ID++;

                mode = false;
            }

        }
    }

    public void Search_Wall_y()
    {
        bool mode = false;

        int ID = 0;

        for (int j = 0; j < Grid_number; j++)           
        {
            for (int i = 0; i < Grid_number; i++)
            {
                if (j < Grid_number && i < Grid_number - 1)
                {
                    if (mode == false)
                    {
                        if (line_y[j, i].GetComponent<LineRenderer>().material.color == new Color(255, 0, 0))
                        {
                            wall temp = new wall();

                            wall_y.Add(temp);

                            wall_y[ID].start = i * Grid_density;
                            wall_y[ID].other = j * Grid_density;

                            mode = true;
                        }
                    }
                    else if (mode == true)
                    {
                        if (line_y[j, i].GetComponent<LineRenderer>().material.color != new Color(255, 0, 0))
                        {
                            wall_y[ID].end = i * Grid_density;

                            ID++;

                            mode = false;
                        }
                    }

                }
            }

            if (mode == true)
            {
                wall_y[ID].end = Grid_number - 1;

                ID++;

                mode = false;
            }
            
        }
    }

    public void Wall_Instantiate()
    {
        Debug.Log(wall_x[0].start);
        Debug.Log(wall_x[0].end);

        for (int i = 0; i < wall_x.Count; i++)
        {
            Wall_x.Add(Instantiate(wall, new Vector3((wall_x[i].start + wall_x[i].end) / 2, wall_x[i].other, 0), Quaternion.identity));
            Wall_x[i].transform.localScale = new Vector3(wall_x[i].end - wall_x[i].start, 0.1f, 3);
            
        }

        for (int i = 0; i < wall_y.Count; i++)
        {
            Wall_y.Add(Instantiate(wall, new Vector3(wall_y[i].other, (wall_y[i].start + wall_y[i].end) / 2, 0), Quaternion.identity));
            Wall_y[i].transform.localScale = new Vector3(0.1f, wall_y[i].end - wall_y[i].start, 3);
        }        
    }

    // Use this for initialization
    void Start()
    {
        line_x = new GameObject[Grid_number, Grid_number];
        line_y = new GameObject[Grid_number, Grid_number];
        points = new GameObject[Grid_number, Grid_number];

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

                        Search_Wall_x();

                        Search_Wall_y();

                        //Wall_Instantiate();
                    }
                }
                break;
        }
    }
}
