using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionBrowser : MonoBehaviour {

	public Transform[] slots;
	public GameObject soldierMenuPrefab;
	public GameObject spellMenuPrefab;
	public GameObject cropMenuPrefab;

	public GameObject AllCardsTabs;
	public GameObject OneCardTabs;

	private CardAsset _cards;

	private List<GameObject> createdCards = new List<GameObject>();
	private int _manaCost;

	//properties for every variable that matters for filtering and selecting cards
	#region properties
	private int _pageIndex = 0;
	public int PageIndex
	{
		get 
		{
			return _pageIndex;
		}
		set
		{
			_pageIndex = value;
			UpdatePage ();
		}
	}

	private bool _includeAllCards = true;
	public bool IncludeAllCards 
	{
		get 
		{
			return _includeAllCards;
		}
		set 
		{
			_includeAllCards = value;
			//show the first page in order for cards to be instantly visible
			_pageIndex = 0;
			UpdatePage ();
		}
	}

	private CardAsset _asset = null;
	public CardAsset Asset
	{
		get
		{ 
			return _asset;
		}
		set
		{ 
			_asset = value;
			//show the first page in order for cards to be instantly visible
			_pageIndex = 0;
			UpdatePage ();
		}
	}
	#endregion

	public void ShowCollectionForBrowsing()
	{
		//_cards = buldingForThisCard;

		ShowCards (0, false, null, -1);

		
		//select all tabs by default
		//CCScreen.Instance.TabsScript.CardTab.Select(instant: true);
		//CCScreen.Instance.TabsScript.SelectTab (CCScreen.Instance.TabsScript.CardTab, instant: true);
	
	}

	public void ClearCreatedCards()
	{
		while (createdCards.Count > 0) {
			GameObject g = createdCards [0];
			createdCards.RemoveAt (0);
			Destroy (g);
		}
	}

	public void UpdateQuantitiesOnPage(){
		foreach (GameObject card in createdCards) {
			//AddCardToDeck addCardComponent = card.GetComponent<AddCardToDeck> ();
			//addCardComponent.UpdateQuantity ();
		}
	}

	//a function to display cards based on all the selected parameters
	public void UpdatePage(){
		ShowCards (_pageIndex, _includeAllCards, _asset, _manaCost);
	}

	private void ShowCards(int pageIndex = 0, bool includeAllCards = true, CardAsset asset = null, int manaCost = -1)
	{
		//saving infomation about the cards that we are showing to the players on this page
		_pageIndex = pageIndex;
		_includeAllCards = includeAllCards;
		_asset = asset;
		_manaCost = manaCost;

		List<CardAsset> CardsOnThisPage = PageSelection (pageIndex, includeAllCards, asset, manaCost);

		//clear created cards list
		ClearCreatedCards();

		if (CardsOnThisPage.Count == 0)
			return;

		for (int i = 0; i < CardsOnThisPage.Count; i++) {
			GameObject newMenuCard;
			if (CardsOnThisPage [i].TypeOfCard == TypesOfCards.Soldier) {
				//it is a soldier
				newMenuCard = Instantiate (soldierMenuPrefab, slots [i].position, Quaternion.identity) as GameObject;
			}
			else if (CardsOnThisPage [i].TypeOfCard == TypesOfCards.Spell) {
				//it is a soldier
				newMenuCard = Instantiate (spellMenuPrefab, slots [i].position, Quaternion.identity) as GameObject;
			}
			else
				newMenuCard = Instantiate (cropMenuPrefab, slots [i].position, Quaternion.identity) as GameObject;		
		
			newMenuCard.transform.SetParent (this.transform);

			createdCards.Add (newMenuCard);

			OneCardManager manager = newMenuCard.GetComponent<OneCardManager> ();
			manager.cardAsset = CardsOnThisPage [i];
			manager.ReadCardFromAsset ();

			//AddCardToDeck addCardComponent = newMenuCard.GetComponent<AddCardToDeck> ();
			//addCardComponent.SetCardAsset (CardsOnThisPage [i]);
			//addCardComponent.UpdateQuantity ();
		}
	}

	public void Next()
	{
		if (PageSelection (_pageIndex + 1, _includeAllCards, _asset, _manaCost).Count == 0)
			return;

		ShowCards (_pageIndex + 1, _includeAllCards, _asset, _manaCost);
	}

	public void Previous()
	{
		if (_pageIndex == 0)
			return;

		ShowCards (_pageIndex - 1, _includeAllCards, _asset, _manaCost);
	}

	private List<CardAsset> PageSelection(int pageIndex = 0, bool includeAllCards = true, CardAsset asset = null, int manaCost = -1)
	{
		List<CardAsset> returnList = new List<CardAsset> ();

		//obatain cards from collection that satisfy all the selected criteria
		List<CardAsset> cardsToChooseFrom = CardCollection.Instance.GetCards (includeAllCards, manaCost);

		//if there are enough cards so that we can show some cards on page with pageIndex
		//otherwise an empty list will be returned
		if (cardsToChooseFrom.Count > pageIndex * slots.Length) {
			for (int i = 0; (i < cardsToChooseFrom.Count - pageIndex * slots.Length && i < slots.Length); i++) {
				returnList.Add (cardsToChooseFrom [pageIndex * slots.Length + i]);
			}
		}

		return returnList;
	}

}
