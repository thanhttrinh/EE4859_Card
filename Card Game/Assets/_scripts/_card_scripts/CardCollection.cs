using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
using UnityEngine.UI;
using UnityEngine.EventSystems;
*/
using System.Linq;

public class CardCollection : MonoBehaviour
//, IDropHandler, IPointerEnterHandler, IPointerExitHandler 
{
	#region Old Code
	/*
	Transform parentToReturnTo = null;

	public void OnPointerEnter(PointerEventData eventData){

	}

	public void OnPointerExit(PointerEventData eventData){
		
	}

	public void OnDrop(PointerEventData eventData){
		PlayerController pcontrol = eventData.pointerDrag.GetComponent<PlayerController> ();
		if (pcontrol != null) {
			pcontrol.parentToReturnTo = this.transform;
		}
	}*/
	#endregion
	//how many cards the player has by default
	public int DefaultNumberOfBasicCards = 43;

	public static CardCollection Instance;
	private Dictionary<string, CardAsset> AllCardsDictionary = new Dictionary<string, CardAsset>();

	public Dictionary<CardAsset, int> QuantityOfEachCard = new Dictionary<CardAsset, int>();

	private CardAsset[] allCardsArray;

	void Awake(){
		Instance = this;

		//search everywhere for all the card assets
		allCardsArray = Resources.LoadAll<CardAsset> ("");
		Debug.Log (allCardsArray.Length);

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
	public List<CardAsset> GetCardOfType(TypesOfCards asset){
		return GetCards (false, asset);
	}

	//the general function that will filters the cards
	public List<CardAsset> GetCards(bool includeAllCards = true, TypesOfCards asset = 0/*, CardAsset asset = null, int manaCost = -1*/)
	{
		//initially select all cards
		var cards = from card in allCardsArray
		            select card;
		
		if (!includeAllCards)
			cards = cards.Where (card => card.TypeOfCard == asset);
		/*
		if (manaCost == 10) 
			cards = cards.Where (card => card.ManaCost >= 10);
		else if (manaCost != -1) 
			cards = cards.Where (card => card.ManaCost == manaCost);
*/
		var returnList = cards.ToList<CardAsset> ();
		returnList.Sort ();

		return returnList;
	}

}
