using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour 
{
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider)
	{
		//Debug.Log ("collision detected");
		if (collider.gameObject.tag == "card") 
		{
			Destroy (collider.gameObject);
		}
	}
}
