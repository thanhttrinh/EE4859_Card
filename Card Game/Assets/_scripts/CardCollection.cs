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
	//how many basic cards the player has by default
	public int DefaultNumberOfBasicCards = 20;

	public static CardCollection Instance;
	private Dictionary<string, CardAsset> AllCardsDictionary = new Dictionary<string, CardAsset>();

	public Dictionary<CardAsset, int> QuantityOfEachCard = new Dictionary<CardAsset, int>();

	private CardAsset[] allCardsArray;

	void awake(){
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

	public List<CardAsset> GetCards(bool includeAllCards = true, int manaCost = -1)
	{
		//initially select all cards
		var cards = from card in allCardsArray
		            select card;
		/*
		if (!includeAllCards) {
			cards = cards.Where (card => card.cardAssets == asset);
		}*/
		if (manaCost == 10) 
			cards = cards.Where (card => card.ManaCost >= 10);
		else if (manaCost != -1) 
			cards = cards.Where (card => card.ManaCost == manaCost);

		var returnList = cards.ToList<CardAsset> ();
		returnList.Sort ();

		return returnList;
	}

}
