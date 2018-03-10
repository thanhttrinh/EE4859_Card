using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCScreen : MonoBehaviour {

	public GameObject screenContent;
	public GameObject readyDecksList;
	public GameObject cardsInDeckList;
	public DeckBuilder BuilderScript;

	public ListOfDecksInCollection ListOfReadyMadeDecksScript;
	public CollectionBrowser collectionBrowserScript;
	public CardSelectionTabs TabsScript;
	public bool ShowReducedQuantitiesInDeckBuilding = true;

	public static CCScreen Instance;

	void Awake()
	{
		Instance = this;
		HideScreen ();
	}

	public void ShowScreenForDeckBuilding(){
		screenContent.SetActive (true);
		//readyDecksList.SetActive (false);
		//cardsInDeckList.SetActive (true);

		//collectionBrowserScript.AllCardsTabs.gameObject.SetActive (false);
		Canvas.ForceUpdateCanvases ();
	}

	public void BuildADeckFor(CardAsset asset){
		ShowScreenForDeckBuilding ();
		collectionBrowserScript.ShowCollectionForBrowsing ();
//		BuilderScript.BuildADeckFor (asset);
	}

	public void HideScreen(){
		screenContent.SetActive (false);
		collectionBrowserScript.ClearCreatedCards ();
	}
}
