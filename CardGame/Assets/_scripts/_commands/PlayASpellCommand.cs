using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayASpellCommand : Command{

	private CardLogic card;
	private Player p;

	public PlayASpellCommand(Player p, CardLogic card){
		this.card = card;
		this.p = p;
	}

	public override void StartCommandExecution(){
		Command.CommandExecutionComplete ();
		//move this card to the spot
		p.PArea.handVisual.PlayASpellFromHand(card.UniqueCardID);
	}
}
