﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnMaker : TurnMaker {

	public override void OnTurnStart(){
		base.OnTurnStart ();
		//new ShowMessageCommand("Your Turn", 2.0f).AddToQueue();
		player.DrawACard();
	}
}
