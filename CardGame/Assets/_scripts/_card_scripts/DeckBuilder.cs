using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuilder : MonoBehaviour {
	public GameObject cardNamePrefab;
	public Transform content;
	public InputField DeckName;

	public int SameCardLimit = 3;
	public int AmountOfCardsInDeck = 20;


	private List<CardAsset> deckList = new List<CardAsset>();
	private Dictionary<CardAsset, CardNameRibbon> ribbons = new Dictionary<CardAsset, CardNameRibbon>();

	public bool InDeckBuildingMode{ get; set; }

	public void AddCard(CardAsset asset){
		//if we are browsing the collection
		if (!InDeckBuildingMode)
			return;

		//if the deck is already full
		if (deckList.Count == AmountOfCardsInDeck)
			return;

		int count = NumberOfThisCardInDeck (asset);

		int limitOfThisCardInDeck = SameCardLimit;

		//if something else if specified in the CardAsset, we use that
		if (asset.OverrideLimitOfThisCardInDeck > 0)
			limitOfThisCardInDeck = asset.OverrideLimitOfThisCardInDeck;

		if (count < limitOfThisCardInDeck) {
			deckList.Add (asset);

			//added one to count if we are adding this card
			count++;

			//do all the graphical stuff
			if (ribbons.ContainsKey (asset)) {
				//update quantity
				ribbons [asset].SetQuantity (count);
			} else {
				// add card's name to the list
				GameObject cardName = Instantiate(cardNamePrefab, content) as GameObject;
				cardName.transform.SetAsLastSibling ();
				cardName.transform.localScale = Vector3.one;
				CardNameRibbon ribbon = cardName.GetComponent<CardNameRibbon> ();
				ribbon.ApplyAsset (asset, count);
				ribbons.Add (asset, ribbon);
			}
		}
	}
		

	public int NumberOfThisCardInDeck(CardAsset asset)
	{
		int count = 0;
		foreach (CardAsset ca in deckList) {
			if (ca == asset)
				count++;
		}
		return count;
	}

	public void RemoveCard(CardAsset asset){
		Debug.Log ("RemoveCard");
		CardNameRibbon ribbonToRemove = ribbons [asset];
		ribbonToRemove.SetQuantity (ribbonToRemove.Quantity - 1);

		if (NumberOfThisCardInDeck (asset) == 1) {
			ribbons.Remove (asset);
			Destroy (ribbonToRemove.gameObject);
		}
		deckList.Remove (asset);

		//update quantities of all cards taht we currently show in the collection
		//this is after deckList.RemoveCard(asset)
		CCScreen.Instance.CollectionBrowserScript.UpdateQuantitiesOnPage();
	}

	public void BuildADeck(){
		InDeckBuildingMode = true;
		while (deckList.Count > 0) {
			RemoveCard (deckList [0]);
		}
		CCScreen.Instance.CollectionBrowserScript.ShowCollectionForDeckBuilding ();

		//reset the InputField text to be empty
		DeckName.text = "";
	}

	public void DoneButtonHandler(){
		//save current deck list into DeckStorage
		DeckInfo deckToSave = new DeckInfo(deckList, DeckName.text);
		Debug.Log ("deck to save: " + deckToSave.DeckName);
		DeckStorage.Instance.AllDecks.Add (deckToSave);
		Debug.Log ("added " + deckToSave.DeckName +" deck into storage");
		DeckStorage.Instance.SaveDecksIntoPlayerPrefs ();
		//the screen with the collection and pre-made decks is loaded by calling
		//other functions on this button
		CCScreen.Instance.ShowScreenForCollectionBrowsing();
	}

	void OnApplicationQuit(){
		//if the player exit the app while editing a deck, we want to save it
		DoneButtonHandler ();
	}
}
