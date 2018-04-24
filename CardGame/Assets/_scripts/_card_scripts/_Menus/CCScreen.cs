using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCScreen : MonoBehaviour {

	public GameObject screenContent;
	public GameObject readyDecksList;
	public GameObject cardsInDeckList;
	public GameObject title;
	public DeckBuilder BuilderScript;

	public ListOfDecksInCollection ListOfReadyMadeDecksScript;
	public CollectionBrowser CollectionBrowserScript;
	public CardSelectionTabs TabsScript;
	public bool ShowReducedQuantitiesInDeckBuilding = true;

	public static CCScreen Instance;

	void Awake()
	{
		Instance = this;
		HideScreen ();
	}

	public void ShowScreenForCollectionBrowsing(){
		screenContent.SetActive (true);
		readyDecksList.SetActive (true);
		cardsInDeckList.SetActive (false);
		DeckSelectionScreen.Instance.screenContent.SetActive (false);
		title.SetActive (false);
		BuilderScript.InDeckBuildingMode = false;
		//if there is already decks made, show them
		ListOfReadyMadeDecksScript.UpdateList ();

		CollectionBrowserScript.AllCardsTabs.gameObject.SetActive (true);
		Canvas.ForceUpdateCanvases ();

		CollectionBrowserScript.ShowCollectionForBrowsing ();
	}

	public void ShowScreenForDeckBuilding(){
		screenContent.SetActive (true);
		readyDecksList.SetActive (false);
		cardsInDeckList.SetActive (true);
		title.SetActive (false);
        HowToPlayScreen.Instance.screenContent.SetActive(false);

        CollectionBrowserScript.AllCardsTabs.gameObject.SetActive (true);
		Canvas.ForceUpdateCanvases ();
	}
		
	public void BuildADeck(){
		ShowScreenForDeckBuilding ();
		CollectionBrowserScript.ShowCollectionForDeckBuilding ();
		BuilderScript.BuildADeck();
	}

	public void HideScreen(){
		screenContent.SetActive (false);
		title.SetActive (true);
		DeckSelectionScreen.Instance.screenContent.SetActive (false);
		CollectionBrowserScript.ClearCreatedCards ();
	}
}
