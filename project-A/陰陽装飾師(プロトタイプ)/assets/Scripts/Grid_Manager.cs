using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    public Vector3 pos = new Vector3(0,0,0);
    public bool inside = false;
    public bool wall = false;
}

public class Grid_Manager : MonoBehaviour
{
    public int Grid_x_min;
    public int Grid_y_min;
    public int Grid_x_max;
    public int Grid_y_max;

    public int To_center_x;
    public int To_center_y;

    public float Grid_density;

    private Point[,] point_;

    public Point point(int i, int j)
    {
        return point_[i, j];
    }

    private Vector3[] line_start = new Vector3[4];
    private Vector3[] line_end = new Vector3[4];

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
        for (int i = Grid_y_min; i < Grid_y_max; i++)
        {
            for (int j = Grid_x_min; j < Grid_x_max; j++)
            {
                point_[j, i] = new Point();
                
                point_[j, i].pos.x = (j - (Grid_x_max / 2) + To_center_x) * Grid_density;
                point_[j, i].pos.y = (i - (Grid_y_max / 2) + To_center_y) * Grid_density;

                //Grid_all(j, i);
            }
        }

        Line(point_[Grid_x_min, Grid_y_min].pos, point_[Grid_x_max - 1, Grid_y_min].pos);
        Line(point_[Grid_x_min, Grid_y_max - 1].pos, point_[Grid_x_max - 1, Grid_y_max - 1].pos);

        Line(point_[Grid_x_min, Grid_y_min].pos, point_[Grid_x_min, Grid_y_max - 1].pos);
        Line(point_[Grid_x_max - 1, Grid_y_min].pos, point_[Grid_x_max - 1, Grid_y_max - 1].pos);

    }
    
    public void Square_measure()
    {
        bool mode = false;
        int start = 0;
        int end = 0;
    
        for (int i = Grid_y_min; i < Grid_y_max; i++)
        {
            for (int j = Grid_x_min; j < Grid_x_max; j++)
            {
                if (mode == false)
                {
                    if (point_[j, i].inside == true)
                    {
                        mode = true;
                        start = j;
                    }
                }
                else if (mode == true)
                {
                    if (point_[j, i].inside == true)
                    {
                        mode = false;
                        end = j;
    
                        for (int k = start; k < end; k++)
                        {
                            point_[k, i].inside = true;

                            //Test_sphere(k, i);
                        }

                        j--;
                    }
                }
            }
    
            if (mode == true)
            {
                mode = false;

                point_[end, i].inside = true;

                //Test_sphere(end, i);              
            }            
        }
    }

    public void Grid_all(int x, int y)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.2f,0.2f,0.2f);
        sphere.GetComponent<MeshRenderer>().material.color = Color.black;
        sphere.transform.position = point_[x, y].pos;
    }

    public void Line(Vector3 start, Vector3 end)
    {
        GameObject Line = new GameObject();
        LineRenderer renderer = Line.AddComponent<LineRenderer>();
        renderer.SetWidth(0.2f, 0.2f);
        renderer.SetVertexCount(2);
        renderer.SetPosition(0, start);
        renderer.SetPosition(1, end);
    }

    public void Test_sphere(int x, int y, float scale, Color color)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(scale, scale, scale);
        sphere.GetComponent<MeshRenderer>().material.color = color;
        sphere.transform.position = point_[x, y].pos;
    }

    // Use this for initialization
    void Start()
    {
        point_ = new Point[Grid_x_max, Grid_y_max];

        Instantiate();

        //Square_measure();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
