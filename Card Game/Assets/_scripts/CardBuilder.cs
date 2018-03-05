using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class CardBuilder : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	Transform parentToReturnTo = null;

	[SerializeField]
	public CardAsset cardPrefab;
	public List<CardAsset> cards = new List<CardAsset>();

	void Awake(){
		if (cards.Count == 20) {
			cards.Shuffle ();
		}
	}

	public void OnPointerEnter(PointerEventData eventData){

	}

	public void OnPointerExit(PointerEventData eventData){

	}

	public void OnDrop(PointerEventData eventData){
		PlayerController pcontrol = eventData.pointerDrag.GetComponent<PlayerController> ();
		if (pcontrol != null) {
			//grab the card in the custom deck build area
			pcontrol.parentToReturnTo = this.transform;

			string cardName = eventData.pointerDrag.name;

			//assign the cardprefab
			cardPrefab = eventData.pointerDrag.GetComponent<OneCardManager> ().cardAsset;

			if (cards.Count <= 20) {
				//if the list of cards is less than 20
				//add the card into the decks
				cards.Add (this.cardPrefab);

			}
			if (cards.Count == 2) {
				//save the card into CardDeckSaved script



			}
		}

	}

}
