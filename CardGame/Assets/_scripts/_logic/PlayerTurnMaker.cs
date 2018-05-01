using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnMaker : TurnMaker {

	public override void OnTurnStart(){
		base.OnTurnStart ();
		if (player.PlayerColor == "blue") {
			new ShowMessageCommand ("Blue Turn", 2.0f).AddToQueue ();
			MessageManager.Instance.ShowMessage ("Blue Turn", 2.0f);
		}
		else if (player.PlayerColor == "red") {
			new ShowMessageCommand ("Red Turn", 2.0f).AddToQueue ();
			MessageManager.Instance.ShowMessage ("Red Turn", 2.0f);
		}
		player.DrawACard();
	}
}
