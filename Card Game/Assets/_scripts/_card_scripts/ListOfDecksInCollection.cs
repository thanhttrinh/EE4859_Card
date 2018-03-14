﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfDecksInCollection : MonoBehaviour {

	public Transform Content;

	public GameObject DeckInListPrefab;

	public GameObject NewDeckButtonPrefab;

	public void UpdateList(){
		//delete all the deck icon first
		foreach (Transform t in Content) {
			if (t != Content) {
				Destroy (t.gameObject);
			}
		}

		foreach (DeckInfo info in DeckStorage.Instance.AllDecks) {
			GameObject g = Instantiate (DeckInListPrefab, Content);
			g.transform.localScale = Vector3.one;
			DeckInScrollList deckInScrollListComponent = g.GetComponent<DeckInScrollList> ();
		}

		if(DeckStorage.Instance.AllDecks.Count < 9){
			GameObject g = Instantiate (NewDeckButtonPrefab, Content);
			g.transform.localScale = Vector3.one;
		}
	}
}
