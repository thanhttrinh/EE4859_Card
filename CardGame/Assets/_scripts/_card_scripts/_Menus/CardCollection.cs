using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
using UnityEngine.UI;
using UnityEngine.EventSystems;
*/
using System.Linq;

public class CardCollection : MonoBehaviour
{
	public static CardCollection Instance;
	private Dictionary<string, CardAsset> AllCardsDictionary = new Dictionary<string, CardAsset>();

	public Dictionary<CardAsset, int> QuantityOfEachCard = new Dictionary<CardAsset, int>();

	private CardAsset[] allCardsArray;

	void Awake(){
		Instance = this;
		//search everywhere for all the card assets
		allCardsArray = Resources.LoadAll<CardAsset> ("");

		foreach (CardAsset ca in allCardsArray) {
			if (!AllCardsDictionary.ContainsKey (ca.name)) 
				AllCardsDictionary.Add (ca.name, ca);
		}
	
		LoadQuantityOfCardsFromPlayerPrefs ();
	}

	private void LoadQuantityOfCardsFromPlayerPrefs()
	{
		foreach (CardAsset ca in allCardsArray) {
			if (PlayerPrefs.HasKey ("NumberOf" + ca.name)) {
				QuantityOfEachCard.Add (ca, PlayerPrefs.GetInt ("NumberOf" + ca.name));
			} else
				QuantityOfEachCard.Add (ca, 0);
		}
	}

	private void SaveQuantityOfCardsIntoPlayerPrefs(){
		foreach (CardAsset ca in allCardsArray)
			PlayerPrefs.SetInt ("NumberOf" + ca.name, QuantityOfEachCard [ca]);
	}

	void OnApplicationQuit(){
		SaveQuantityOfCardsIntoPlayerPrefs ();
	}

	public CardAsset GetCardAssetByName(string name){
		if (AllCardsDictionary.ContainsKey (name))
			return AllCardsDictionary [name];
		else
			return null;
	}

	//get a list of cards filtered by type (soldiers, crops, spells)
	public List<CardAsset> GetCardOfType(CardTypeAsset asset){
		//return GetCards (false, asset);
		var cards = from card in allCardsArray where card.cardTypeAsset == asset select card;
		var returnList = cards.ToList<CardAsset> ();
		returnList.Sort ();

		return returnList;
	}

	//the general function that will filters the cards
	public List<CardAsset> GetCards(bool includeAllCards = true, CardTypeAsset asset = null)
	{
		//initially select all cards
		var cards = from card in allCardsArray
		            select card;
		
		if (!includeAllCards)
			cards = cards.Where (card => card.cardTypeAsset == asset);
		
		var returnList = cards.ToList<CardAsset> ();
		returnList.Sort ();

		return returnList;
	}

}
