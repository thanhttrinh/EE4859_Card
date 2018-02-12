using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {
	//give the name of the desired card
	public string soldier;
	public string crop;
	public string spell;

	//public Hashtable DeckSet = new Hashtable();

	void Start () 
	{
		
	}

	void Update () 
	{

	}

	void OnCollisionEnter2D(Collision2D obj)
	{
		if (obj.gameObject.tag == "card") {
			Debug.Log ("it's a card ");
			Debug.Log (obj.gameObject.tag);
		}
	}
}
