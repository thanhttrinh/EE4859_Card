using System.Collections;
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

		Debug.Log ("before obtaining info from deck info");
		//load info about decks from DeckStorage
		foreach (DeckInfo info in DeckStorage.Instance.AllDecks) {
			//create a new deckInListPrefab
			GameObject g = Instantiate (DeckInListPrefab, Content);
			g.transform.localScale = Vector3.one;
			DeckInScrollList deckInScrollListComponent = g.GetComponent<DeckInScrollList> ();
			deckInScrollListComponent.ApplyInfo (info);
		}

		//if there is room to create more decks, create a newDeckButton
		if(DeckStorage.Instance.AllDecks.Count < 9){
			GameObject g = Instantiate (NewDeckButtonPrefab, Content);
			g.transform.localScale = Vector3.one;
		}
	}
}
