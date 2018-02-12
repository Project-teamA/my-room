using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

	Animator animator ;
	
	void Start () 
	{
		animator = GetComponent<Animator>();
	}


	void OnGUI()
	{
		if(GUILayout.Button("金式神攻撃"))
		{
			animator.Play("金式神1");
		}
    }
}
