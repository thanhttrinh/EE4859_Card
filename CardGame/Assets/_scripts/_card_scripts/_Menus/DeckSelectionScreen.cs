using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckSelectionScreen : MonoBehaviour {

	public GameObject screenContent;
	public DeckIcon[] DeckIcons;
	public GameObject title;

	public CardInfoPanel CardPanelDeckSelection;

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

		//disable all deck icons first
		foreach (DeckIcon icon in DeckIcons) {
			icon.gameObject.SetActive (false);
			icon.InstantDeselect ();
		}

		for (int j = 0; j < DeckStorage.Instance.AllDecks.Count; j++) {
			DeckIcons[j].ApplyLockToIcon(DeckStorage.Instance.AllDecks [j]);
			DeckIcons[j].gameObject.SetActive (true);
		}

	}

	public void ShowScreen(){
		screenContent.SetActive (true);
		CCScreen.Instance.title.SetActive (false);
		CCScreen.Instance.screenContent.SetActive (false);
		ShowDecks ();
	}

	public void HideScreen(){
		screenContent.SetActive (false);
		title.SetActive (true);
	}

	public void ShowBuilder(){
		CCScreen.Instance.ShowScreenForCollectionBrowsing ();
	}
}
