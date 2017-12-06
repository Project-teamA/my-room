using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

    bool selected = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            //Debug.Log(hit.collider.gameObject);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject != null && hit.collider.gameObject == gameObject)
                {
                    selected = true;
                }
            }
        }
        else if (Input.GetMouseButton(0) && selected == true)
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 10.0f;
            
            gameObject.transform.position = Camera.main.ScreenToWorldPoint(pos);         
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (selected == true)
            {
                Vector3 target_pos = new Vector3();

                float dist_min = Vector2.Distance(
                           GameObject.Find("Grid_Manager").GetComponent<Grid_Manager>().points[0, 0].transform.position,
                           transform.position);

                Grid_Manager Grid_Manager = GameObject.Find("Grid_Manager").GetComponent<Grid_Manager>();

                FurnitureGrid.Furniture furniture = GameObject.Find("Grid_Manager").GetComponent<FurnitureGrid>().
                            furniture[GameObject.Find("Grid_Manager").GetComponent<FurnitureGrid>().furniture_Count_];
                             
                for (int i = 0; i < Grid_Manager.Grid_number; i++)
                {
                    for (int j = 0; j < Grid_Manager.Grid_number; j++)
                    {
                        float dist_temp = Vector2.Distance(
                            Grid_Manager.points[j, i].transform.position,
                            transform.position);

                        if (dist_temp < dist_min)
                        {
                            dist_min = dist_temp;
                            target_pos = Grid_Manager.points[j, i].transform.position;
                        }

                    }
                }

                transform.position = target_pos;

                bool error = false;
                int count = 0;

                for (int k = 0; k < furniture.vertex_number_; k++)
                {                  
                    for (int i = 0; i < Grid_Manager.Grid_number; i++)
                    {
                        for (int j = 0; j < Grid_Manager.Grid_number; j++)
                        {
                            if (furniture.vertex_[k] + transform.position == Grid_Manager.points[j, i].transform.position)
                            {
                                count++;
                            }
                        }
                    }
                }

                if (count < furniture.vertex_number_)
                {
                    error = true;
                    Debug.Log("グリッド外");
                }

                if (error == false)
                {
                    for (int k = 0; k < furniture.vertex_number_; k++)
                    {
                        for (int i = 0; i < Grid_Manager.Grid_number; i++)
                        {
                            for (int j = 0; j < Grid_Manager.Grid_number; j++)
                            {
                                if (error == false &&
                                    furniture.vertex_[k] + transform.position == Grid_Manager.points[j, i].transform.position)
                                {
                                    if (Grid_Manager.points[j, i].GetComponent<MeshRenderer>().material.color == new Color(0, 0, 0))
                                    {
                                        error = true;
                                        Debug.Log("間取り外");
                                    }
                                    
                                }
                            }
                        }
                    }
                }

                if (error == false)
                {
                    GameObject.Find("Grid_Manager").GetComponent<FurnitureGrid>().furniture_Count_++;
                }

            }

            selected = false;
        }

    }
}
