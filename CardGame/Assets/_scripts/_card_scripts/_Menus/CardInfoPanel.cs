using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInfoPanel : MonoBehaviour {
	public Button PlayButton;
	public Button BuildDeckButton;

	public DeckIcon selectedDeck{ get; set; }

	void Awake(){
		OnOpen ();
	}

	public void OnOpen(){
		SelectDeck(null);
	}

	public void SelectDeck(DeckIcon deck){
		if (deck == null || selectedDeck == deck || !deck.DeckInfomation.IsComplete ()) {
			selectedDeck = null;
			if (PlayButton != null)
				PlayButton.interactable = false;

			Debug.Log ("No deck was selected");
		} 
		else 
		{
			selectedDeck = deck;
			Debug.Log ("deck selected: " + selectedDeck.DeckNameText.ToString ());
			//load this information to BattleStartInfo
			BattleStartInfo.SelectedDeck = selectedDeck.DeckInfomation;

			if (PlayButton != null)
				PlayButton.interactable = true;
		}
	}

	public void GoToDeckBuilding(){
		CCScreen.Instance.BuildADeck ();
	}
}
