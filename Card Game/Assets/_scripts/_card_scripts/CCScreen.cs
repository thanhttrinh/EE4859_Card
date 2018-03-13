﻿using System.Collections;
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
		title.SetActive (false);
		BuilderScript.InDeckBuildingMode = false;
		ListOfReadyMadeDecksScript.UpdateList ();

		CollectionBrowserScript.ShowCollectionForBrowsing ();
	}

	public void ShowScreenForDeckBuilding(){
		screenContent.SetActive (true);
		readyDecksList.SetActive (false);
		cardsInDeckList.SetActive (true);
		title.SetActive (false);
	}
		
	public void BuildADeckFor(CardAsset asset){
		ShowScreenForDeckBuilding ();
		CollectionBrowserScript.ShowCollectionForBrowsing ();
		//BuilderScript.BuildADeckFor(asset);
	}

	public void HideScreen(){
		screenContent.SetActive (false);
		title.SetActive (true);
		CollectionBrowserScript.ClearCreatedCards ();
	}
}
