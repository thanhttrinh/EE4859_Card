﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {

	public Hashtable DeckSet = new Hashtable();

	void Start () 
	{
		
	}

	void Update () 
	{
		
	}

	void OnTriggerEnter2D(Collider2D obj)
	{
		if (obj.gameObject.tag == "card") {
			Debug.Log ("it's a card");
		}
	}

	void OnTriggerExit2D(Collider2D obj)
	{
		Debug.Log ("bye card");
	}
}