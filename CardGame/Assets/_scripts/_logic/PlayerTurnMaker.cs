using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnMaker : TurnMaker {

	public override void OnTurnStart(){
		base.OnTurnStart ();
		if (player.PlayerColor == "blue") {
			new ShowMessageCommand ("Your Turn", 2.0f).AddToQueue ();
			MessageManager.Instance.ShowMessage ("Your Turn", 2.0f);
		}
		else if (player.PlayerColor == "red") {
			new ShowMessageCommand ("Enemy's Turn", 2.0f).AddToQueue ();
			MessageManager.Instance.ShowMessage ("Enemy's Turn", 2.0f);
		}
		player.DrawACard();
	}
}
