using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayASoldierCommand : Command {

	private CardLogic card;
	private int gridPos;
	private Player p;
	private int soldierID;

	public PlayASoldierCommand(CardLogic card, Player p, int gridPos, int soldierID){
		this.p = p;
		this.card = card;
		this.gridPos = gridPos;
		this.soldierID = soldierID;
	}


	public override void StartCommandExecution(){
		/*
		//remove and destroy the card in hand
		HandVisual PlayerHand = p.PArea.handVisual;
		GameObject card = IDHolder.GetGameObjectWithID (card.UniqueCardID); //this is unity gameobject not from IDholder
		PlayerHand.RemoveCard (card);
		GameObject.Destroy (card);
		//HoverPreview.PrevewsAllowed = true;
		//p.PArea.gridVisual.AddSoldierAtIndex(card.GetComponent<OneCardManager>().cardAsset, soldierID, gridPos);
		//TODO: create a soldier
		*/
	}

}
