using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandVisual : MonoBehaviour {
	public AreaPosition owner;
	public bool TakeCardsOpenly = true;
	public SameDistanceChildren slots;

	[Header("Transform Ref")]
	public Transform DrawPreviewSpot;
	public Transform DeckTransform;
	public Transform OtherCardDrawSourceTransform;
	//public Transform PlayPreviewSpot;
	private List<GameObject> CardsInHand = new List<GameObject>();

	public void AddCard(GameObject card){
		CardsInHand.Insert (0, card);
		card.transform.SetParent (slots.transform);
		PlaceCardsOnNewSlots ();
	//	UpdatePlacementOfSlots ();
		//Debug.Log(CardsInHand[0].name.ToString());
	}

	public void RemoveCard(GameObject card){
		CardsInHand.Remove (card);
		PlaceCardsOnNewSlots ();
	//	UpdatePlacementOfSlots ();
		//Debug.Log(card.name.ToString());
	}

	public void RemoveCardAtIndex(int index){
		CardsInHand.RemoveAt (index);
		PlaceCardsOnNewSlots ();
		//UpdatePlacementOfSlots ();
		//Debug.Log(CardsInHand[index].name.ToString());
	}

	public GameObject GetCardAtIndex(int index){
		return CardsInHand [index];
	}
	/*
	void UpdatePlacementOfSlots(){
		float xPos;
		if (CardsInHand.Count > 0)
			xPos = (slots.Children [0].transform.localPosition.x - slots.Children [CardsInHand.Count - 1].transform.localPosition.x) / 1f;
		else
			xPos = 0f;
		Debug.Log (slots.Children [0].transform.position.ToString ());
		Debug.Log (slots.Children [CardsInHand.Count - 1].transform.position.ToString ());
		//slots.gameObject.transform.DOLocalMoveX (xPos, 0.3f);
	}*/

	public void PlaceCardsOnNewSlots(){
		foreach (GameObject g in CardsInHand) {
			if (g != null) {
				g.transform.DOLocalMoveX (slots.Children [CardsInHand.IndexOf (g)].transform.localPosition.x, 0.3f);
				WhereIsTheCardOrSoldier w = g.GetComponent<WhereIsTheCardOrSoldier> ();
				//	Debug.Log(g.GetComponent<OneCardManager>().cardAsset.ScriptName.ToString());
				w.Slot = CardsInHand.IndexOf (g);
				w.SetHandSortingOrder ();
			}
		}
	}

	GameObject CreateACardAtPosition(CardAsset c, Vector3 position, Vector3 eulerAngles)
	{
		GameObject card;

		if (c.TypeOfCard == TypesOfCards.Soldier)
		{
			card = GameObject.Instantiate(GlobalSettings.Instance.SoldierCardPrefab, position, Quaternion.Euler(eulerAngles)) as GameObject;
			//Debug.Log(card.GetComponent<OneCardManager>().cardAsset.name.ToString());
		}
		else if(c.TypeOfCard == TypesOfCards.Spell)
		{
			card = GameObject.Instantiate(GlobalSettings.Instance.SpellCardPrefab, position, Quaternion.Euler(eulerAngles)) as GameObject;
			//Debug.Log(card.GetComponent<OneCardManager>().cardAsset.name.ToString());
		}
		else if(c.TypeOfCard == TypesOfCards.Crop)
		{
			card = GameObject.Instantiate(GlobalSettings.Instance.CropCardPrefab, position, Quaternion.Euler(eulerAngles)) as GameObject;
			//Debug.Log(card.GetComponent<OneCardManager>().cardAsset.name.ToString() + "; position = " + position.ToString() + "; GS = "+ GlobalSettings.Instance.SpellCardPrefab.name.ToString());
		}
		else {
			card = null;
		}
		//Debug.Log("HV: card was instanitated");
		OneCardManager manager = card.GetComponent<OneCardManager>();
		manager.cardAsset = c;
		manager.ReadCardFromAsset();
		//Debug.Log("Access one card manager: success");
		return card;
	}

	public void GivePlayerACard(CardAsset c, int UniqueID, bool fast = false, bool fromDeck = true){
		GameObject card;
		if (fromDeck) 
			card = CreateACardAtPosition (c, DeckTransform.position, new Vector3 (0f, -179f, 0f));
		else
			card = CreateACardAtPosition (c, OtherCardDrawSourceTransform.position, new Vector3 (0f, -179f, 0f));

		// Set a tag to reflect where this card is
		foreach (Transform t in card.GetComponentsInChildren<Transform>())
			t.tag = owner.ToString () + "Card";

		// pass this card to HandVisual class
		AddCard(card);

		// Bring card to front while it travels from draw spot to hand
		WhereIsTheCardOrSoldier w = card.GetComponent<WhereIsTheCardOrSoldier>();
		w.BringToFront();
		w.Slot = 0; 
		w.VisualState = VisualStates.Transition;

		// pass a unique ID to this card.
		IDHolder id = card.AddComponent<IDHolder>();
		id.UniqueID = UniqueID;

		// move card to the hand;
		Sequence s = DOTween.Sequence();
		if (!fast)
		{
			// Debug.Log ("Not fast!!!");
			s.Append(card.transform.DOMove(DrawPreviewSpot.position, GlobalSettings.Instance.CardTransitionTime));
			if (TakeCardsOpenly)
				s.Insert(0f, card.transform.DOLocalRotate(Vector3.zero, GlobalSettings.Instance.CardTransitionTime)); 
			else 
				s.Insert(0f, card.transform.DOLocalRotate(new Vector3(0f, -179f, 0f), GlobalSettings.Instance.CardTransitionTime)); 
			s.AppendInterval(GlobalSettings.Instance.CardPreviewTime);
			// displace the card so that we can select it in the scene easier.
			s.Append(card.transform.DOLocalMove(slots.Children[0].transform.localPosition, GlobalSettings.Instance.CardTransitionTime));
		}
		else
		{
			// displace the card so that we can select it in the scene easier.
			s.Append(card.transform.DOLocalMove(slots.Children[0].transform.localPosition, GlobalSettings.Instance.CardTransitionTimeFast));
			if (TakeCardsOpenly)    
				s.Insert(0f,card.transform.DOLocalRotate(Vector3.zero, GlobalSettings.Instance.CardTransitionTimeFast)); 
			else 
				s.Insert(0f, card.transform.DOLocalRotate(new Vector3(0f, -179f, 0f), GlobalSettings.Instance.CardTransitionTimeFast)); 
		}

		s.OnComplete(()=>ChangeLastCardStatusToInHand(card, w));
	}

	void ChangeLastCardStatusToInHand(GameObject card, WhereIsTheCardOrSoldier w)
	{
//		Debug.Log("Changing state to Hand for card: " + card.gameObject.name);
		if (owner == AreaPosition.blue)
			w.VisualState = VisualStates.BlueHand;
		else
			w.VisualState = VisualStates.RedHand;

		// set correct sorting order
		w.SetHandSortingOrder();
		// end command execution for DrawACArdCommand
		Command.CommandExecutionComplete();
	}
}
