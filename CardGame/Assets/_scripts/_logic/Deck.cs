﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

	public List<CardAsset> cards = new List<CardAsset>();

	void awake()
	{
		cards.Shuffle ();
	}

	void Start(){
		cards.Shuffle ();
	}
}
