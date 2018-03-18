using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckSelectionScreen : MonoBehaviour {

	public GameObject screenContent;
	public GameObject DeckIcons;
	public GameObject title;

	public static DeckSelectionScreen Instance;

	void Awake()
	{
		Instance = this;
		HideScreen ();
	}

	public void ShowDecks(){
		if (DeckStorage.Instance.AllDecks.Count == 0) {
			HideScreen ();
			return;
		}

	}
	public void HideScreen(){
		screenContent.SetActive (false);
		title.SetActive (true);
	}
}
