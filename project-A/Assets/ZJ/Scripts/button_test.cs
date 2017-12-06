using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_test : MonoBehaviour {

    public Texture Button0;
    public Texture Button1;
    public Texture Button2;
    public Texture Button3;
    public Texture Button4;
    public Texture Button5;
    public Texture Button6;
    public Texture Button7;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private bool m_down;
    internal void OnGUI()

    {
        if (GUI.Button(new Rect(20, 60, 120, 120), Button0))

        {
            m_down = !m_down; ;
        }

        if (m_down)

        {

            if (GUI.Button(new Rect(30, 80, 120, 120), Button1))

            {
                //todo:do something

            }

            if (GUI.Button(new Rect(150, 80, 120, 120), Button2))

            {
                //todo:do something

            }

            if (GUI.Button(new Rect(270, 80, 120, 120), Button3))

            {
                //todo:do something

            }

            if (GUI.Button(new Rect(390, 80, 120, 120), Button4))

            {
                //todo:do something

            }

            if (GUI.Button(new Rect(510, 80, 120, 120), Button5))

            {
                //todo:do something

            }

            if (GUI.Button(new Rect(630, 80, 120, 120), Button6))

            {
                //todo:do something

            }

            if (GUI.Button(new Rect(750, 80, 120, 120), Button7))

            {
                //todo:do something

            }
        }

    }
}
