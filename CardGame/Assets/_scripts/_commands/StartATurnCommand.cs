using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartATurnCommand : Command {

	private Player player;

	public StartATurnCommand(Player player){
		this.player = player;
	}

	public override void StartCommandExecution(){
		TurnManager.Instance.whoseTurn = player;
		Command.CommandExecutionComplete ();
	}
}
