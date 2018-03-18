using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionBrowser : MonoBehaviour {

	public Transform[] Slots;
	public GameObject soldierMenuPrefab;
	public GameObject spellMenuPrefab;
	public GameObject cropMenuPrefab;

	public GameObject AllCardsTabs;

	private List<GameObject> createdCards = new List<GameObject>();

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

	private CardTypeAsset _asset = null;
	public CardTypeAsset Asset
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
		ShowCards (0, true, null);

		//select all tabs by default
		CCScreen.Instance.TabsScript.Default.Select(instant:true);
		CCScreen.Instance.TabsScript.SelectTab (CCScreen.Instance.TabsScript.Default, instant: true);
	}

	public void ShowCollectionForDeckBuilding()
	{
		ShowCards (0, true, null);

		CCScreen.Instance.TabsScript.Default.Select(instant:true);
		CCScreen.Instance.TabsScript.SelectTab (CCScreen.Instance.TabsScript.Default, instant: true);
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
			AddCardToDeck addCardComponent = card.GetComponent<AddCardToDeck> ();
			addCardComponent.UpdateQuantity ();
		}
	}

	//a function to display cards based on all the selected parameters
	public void UpdatePage(){
		ShowCards (_pageIndex, _includeAllCards, _asset);
	}

	private void ShowCards(int pageIndex = 0, bool includeAllCards = true, CardTypeAsset asset = null)
	{
		//saving infomation about the cards that we are showing to the players on this page
		_pageIndex = pageIndex;
		_includeAllCards = includeAllCards;
		_asset = asset;

		List<CardAsset> CardsOnThisPage = PageSelection (pageIndex, includeAllCards, asset);

		//clear created cards list
		ClearCreatedCards();

		if (CardsOnThisPage.Count == 0)
			return;

		for (int i = 0; i < CardsOnThisPage.Count; i++) {
			GameObject newMenuCard;
			if (CardsOnThisPage [i].TypeOfCard == TypesOfCards.Soldier) {
				//it is a soldier
				newMenuCard = Instantiate (soldierMenuPrefab, Slots [i].position, Quaternion.identity) as GameObject;
			} else if (CardsOnThisPage [i].TypeOfCard == TypesOfCards.Spell) {
				//it is a spell
				newMenuCard = Instantiate (spellMenuPrefab, Slots [i].position, Quaternion.identity) as GameObject;
			} else {
				//it is a crop
				newMenuCard = Instantiate (cropMenuPrefab, Slots [i].position, Quaternion.identity) as GameObject;		
			}
			newMenuCard.transform.SetParent (this.transform);

			createdCards.Add (newMenuCard);

			OneCardManager manager = newMenuCard.GetComponent<OneCardManager> ();
			manager.cardAsset = CardsOnThisPage [i];
			manager.ReadCardFromAsset ();

			AddCardToDeck addCardComponent = newMenuCard.GetComponent<AddCardToDeck> ();
			addCardComponent.SetCardAsset (CardsOnThisPage [i]);
			addCardComponent.UpdateQuantity ();
		}
	}

	public void Next()
	{
		if (PageSelection (_pageIndex + 1, _includeAllCards, _asset).Count == 0)
			return;

		ShowCards (_pageIndex + 1, _includeAllCards, _asset);
	}

	public void Previous()
	{
		if (_pageIndex == 0)
			return;

		ShowCards (_pageIndex - 1, _includeAllCards, _asset);
	}

	//return a list with assets of cards that we have to show on page with pageIndex
	// selects cards that satisfy all parameters
	private List<CardAsset> PageSelection(int pageIndex = 0, bool includeAllCards = true, CardTypeAsset asset = null)
	{
		List<CardAsset> returnList = new List<CardAsset> ();

		//obatain cards from collection that satisfy all the selected criteria
		List<CardAsset> cardsToChooseFrom = CardCollection.Instance.GetCards (includeAllCards, asset);
		//if there are enough cards so that we can show some cards on page with pageIndex
		//otherwise an empty list will be returned
		if (cardsToChooseFrom.Count > pageIndex * Slots.Length) {
			//check for 2 conditions
			//i < cardsToChooseFrom.Count - pageIndex * Slots.Length checks that we did not run out of cards on the last page
			//i < Slots.Length checks that we have reached the limit of cards to display on one page 
			for (int i = 0; (i < cardsToChooseFrom.Count - pageIndex * Slots.Length && i < Slots.Length); i++) {
				returnList.Add (cardsToChooseFrom [pageIndex * Slots.Length + i]);
			}
		}

		return returnList;
	}

}
