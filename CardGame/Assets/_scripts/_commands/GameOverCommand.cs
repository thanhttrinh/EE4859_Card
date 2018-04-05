using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCommand : Command {

	public Player loser;

	public GameOverCommand(Player loser){
		this.loser = loser;
	}

	public override void StartCommandExecution(){
		Debug.Log ("DEFEAT");
	}

}
