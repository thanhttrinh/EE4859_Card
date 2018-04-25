using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DeckInScrollList : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public Text NameText;
	public GameObject DeleteDeckButton;
	public DeckInfo SavedDeckInfo;

	public void Awake(){
		DeleteDeckButton.SetActive (false);
	}

	public void EditThisDeck(){
		Debug.Log ("Editing Deck: " + SavedDeckInfo.DeckName);
		//switch collection to edit mode
		//display deck list
		CCScreen.Instance.HideScreen();
		//make sure it's the same deck
		CCScreen.Instance.BuilderScript.BuildADeck();
		CCScreen.Instance.BuilderScript.DeckName.text = SavedDeckInfo.DeckName;
		CardCount.Instance.count = SavedDeckInfo.Cards.Count;
		//populate it with the same cards taht were in this deck
		foreach (CardAsset asset in SavedDeckInfo.Cards)
			CCScreen.Instance.BuilderScript.AddCard (asset);
		//delete the deck that we are editing from decks storage
		DeckStorage.Instance.AllDecks.Remove(SavedDeckInfo);

		CCScreen.Instance.CollectionBrowserScript.ShowCollectionForDeckBuilding ();
		CCScreen.Instance.ShowScreenForDeckBuilding ();
	
		CardCount.Instance.SetCountText ();
	}

	public void DeleteThisDeck(){
		DeckStorage.Instance.AllDecks.Remove (SavedDeckInfo);
		Destroy (gameObject);
	}

	public void ApplyInfo(DeckInfo deckInfo){
		NameText.text = deckInfo.DeckName;
		SavedDeckInfo = deckInfo;
	}

	public void OnPointerEnter(PointerEventData data){
		//show delete deck button
		DeleteDeckButton.SetActive(true);
	}

	public void OnPointerExit(PointerEventData data){
		//hide delete deck button
		DeleteDeckButton.SetActive(false);
	}

}
