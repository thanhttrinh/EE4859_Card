using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayASoldierCommand : Command {

	private CardLogic cl;
	private int gridPos;
	private Player p;
	private int soldierID;

	public PlayASoldierCommand(CardLogic card, Player p, int gridPos, int soldierID){
		this.p = p;
		this.cl = card;
		this.gridPos = gridPos;
		this.soldierID = soldierID;
	}

	public override void StartCommandExecution(){
		//remove and destroy the card in hand
		HandVisual PlayerHand = p.PArea.handVisual;
		GameObject card = IDHolder.GetGameObjectWithID(cl.UniqueCardID);
		PlayerHand.RemoveCard (card);
		GameObject.Destroy (card);
		SoldierPreview.PreviewsAllowed = true;
		p.PArea.gridVisual.AddSoldierAtIndex(card.GetComponent<OneCardManager>().cardAsset, soldierID, gridPos);
		//TODO: create a soldier
	}
}
