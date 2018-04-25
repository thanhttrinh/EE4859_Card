using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {
	public static Deck Instance;
	public List<CardAsset> cards = new List<CardAsset>();

	void Awake(){
		Instance = this;
	}
	public void ShuffleDeck()
	{
		cards.Shuffle ();
	}
}
