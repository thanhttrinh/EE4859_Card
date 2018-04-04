using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DeckIcon : MonoBehaviour {

	public Text DeckNameText;
	public GameObject DeckNoCompleteObject;

	public Image DeckBG;

	//private float initialScale;
	//private float targetScale = 1.2f;
	private bool selected = false;

	public DeckInfo DeckInfomation{ get; set;}

	void Awake(){
		//initialScale = transform.localScale.x;
	}

	public void ApplyLockToIcon(DeckInfo info){
		DeckInfomation = info;

		//check if deck is complete
		DeckNoCompleteObject.SetActive(!info.IsComplete());

		DeckNameText.text = info.DeckName;
	}

	void OnMouseDown(){
		//show the animation
		if (!selected) {
			selected = true;
			//zoom in on the deck only if it's complete (full deck)
			if (DeckInfomation.IsComplete ()) {
				//transform.DOScale(targetScale, 0.5f);
				DeckBG.color = Color.cyan;
			}

			DeckSelectionScreen.Instance.CardPanelDeckSelection.SelectDeck (this);
			//deselect all the other portrait menu buttons
			DeckIcon[] allPortraitButtons = GameObject.FindObjectsOfType<DeckIcon> ();
			foreach (DeckIcon m in allPortraitButtons)
				if (m != this)
					m.Deselect ();
				
		} else {
			Deselect ();
			DeckSelectionScreen.Instance.CardPanelDeckSelection.SelectDeck(null);
		}
	}

	public void Deselect(){
		//transform.DOScale (initialScale, 0.5f);
		DeckBG.color = Color.white;
		selected = false;
	}

	public void InstantDeselect(){
		//transform.localScale = new Vector3 (initialScale, initialScale, initialScale);
		DeckBG.color = Color.white;
		selected = false;
	}
}
