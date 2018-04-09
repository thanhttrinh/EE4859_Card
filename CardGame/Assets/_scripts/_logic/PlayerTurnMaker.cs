using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnMaker : TurnMaker {
	public Board instance;

	void Update()
	{
		if(player.PlayerColor == "blue")
			instance.PlayerInput ();
		else if(player.PlayerColor == "red")
			instance.PlayerInput ();
		
	}

	public override void OnTurnStart(){
		base.OnTurnStart ();
		if (player.PlayerColor == "blue") {
			new ShowMessageCommand ("Your Turn", 2.0f).AddToQueue ();
			MessageManager.Instance.ShowMessage ("Your Turn", 2.0f);
			//instance.PlayerInput ();
		}
		else if (player.PlayerColor == "red") {
			new ShowMessageCommand ("Enemy Turn", 2.0f).AddToQueue ();
			MessageManager.Instance.ShowMessage ("Enemy Turn", 2.0f);
			//instance.PlayerInput ();
		}
		player.DrawACard();
	}
}
