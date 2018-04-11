﻿using System.Collections;
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
	public Transform PlayPreviewSpot;
	private List<GameObject> CardsInHand = new List<GameObject>();

	public void AddCard(GameObject card){
		CardsInHand.Insert (0, card);
		card.transform.SetParent (slots.transform);
		PlaceCardsOnNewSlots ();
		UpdatePlacementOfSlots ();
	}

	public void RemoveCard(GameObject card){
		CardsInHand.Remove (card);
		PlaceCardsOnNewSlots ();
		UpdatePlacementOfSlots ();
	}

	public void RemoveCardAtIndex(int index){
		CardsInHand.RemoveAt (index);
		PlaceCardsOnNewSlots ();
		UpdatePlacementOfSlots ();
	}

	public GameObject GetCardAtIndex(int index){
		return CardsInHand [index];
	}

	void UpdatePlacementOfSlots(){
		float xPos;
		if (CardsInHand.Count > 0)
			xPos = (slots.Children [0].transform.localPosition.x - slots.Children [CardsInHand.Count - 1].transform.localPosition.x) / 2f;
		else
			xPos = 0f;
		Debug.Log (slots.Children [0].transform.position.ToString ());
		Debug.Log (slots.Children [CardsInHand.Count - 1].transform.position.ToString ());
		slots.gameObject.transform.DOLocalMoveX (xPos, 0.3f);
	}

	public void PlaceCardsOnNewSlots(){
		foreach (GameObject g in CardsInHand) {
			Debug.Log (g.name.ToString ());
			g.transform.position = slots.Children [CardsInHand.IndexOf (g)].transform.position;
			WhereIsTheCardOrSoldier w = g.GetComponent<WhereIsTheCardOrSoldier> ();
			w.Slot = CardsInHand.IndexOf (g);
			w.SetHandSortingOrder ();
		}
	}

	GameObject CreateACardAtPosition(CardAsset c, Vector3 position, Vector3 eulerAngles)
	{
		GameObject card;
		if (c.TypeOfCard == TypesOfCards.Soldier)
		{
			card = GameObject.Instantiate(GlobalSettings.Instance.SoldierCardPrefab, position, Quaternion.Euler(eulerAngles)) as GameObject;
		}
		else 
		{
			if (c.Targets == TargetingOptions.NoTarget)
			{
				card = GameObject.Instantiate(GlobalSettings.Instance.NoTargetSpellCardPrefab, position, Quaternion.Euler(eulerAngles)) as GameObject;
			}
			else
			{
				card = GameObject.Instantiate(GlobalSettings.Instance.TargetedSpellCardPrefab, position, Quaternion.Euler(eulerAngles)) as GameObject;
			// pass targeting options to DraggingActions
			//	DragSpellOnTarget dragSpell = card.GetComponentInChildren<DragSpellOnTarget>();
			//	dragSpell.Targets = c.Targets;
			}
		}

		OneCardManager manager = card.GetComponent<OneCardManager>();
		manager.cardAsset = c;
		manager.ReadCardFromAsset();

		return card;
	}

	public void GivePlayerACard(CardAsset c, int UniqueID, bool fast = false, bool fromDeck = true){
		GameObject card;
		if (fromDeck)
			card = CreateACardAtPosition(c, DeckTransform.position, new Vector3(0f, -179f, 0f));
		else
			card = CreateACardAtPosition(c, OtherCardDrawSourceTransform.position, new Vector3(0f, -179f, 0f));

		// Set a tag to reflect where this card is
		foreach (Transform t in card.GetComponentsInChildren<Transform>())
			t.tag = owner.ToString()+"Card";
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
		Debug.Log("Changing state to Hand for card: " + card.gameObject.name);
		if (owner == AreaPosition.blue)
			w.VisualState = VisualStates.BlueHand;
		else
			w.VisualState = VisualStates.RedHand;

		// set correct sorting order
		w.SetHandSortingOrder();
		// end command execution for DrawACArdCommand
		Command.CommandExecutionComplete();
	}

	public void PlayASpellFromHand(int CardID)
	{
		GameObject card = IDHolder.GetGameObjectWithID(CardID);
		PlayASpellFromHand(card);
	}

	public void PlayASpellFromHand(GameObject CardVisual)
	{
		Command.CommandExecutionComplete();
		CardVisual.GetComponent<WhereIsTheCardOrSoldier>().VisualState = VisualStates.Transition;
		RemoveCard(CardVisual);

		CardVisual.transform.SetParent(null);

		Sequence s = DOTween.Sequence();
		s.Append(CardVisual.transform.DOMove(PlayPreviewSpot.position, 1f));
		s.Insert(0f, CardVisual.transform.DORotate(Vector3.zero, 1f));
		s.AppendInterval(2f);
		s.OnComplete(()=>
			{
				Command.CommandExecutionComplete();
				Destroy(CardVisual);
			});
	}
}
