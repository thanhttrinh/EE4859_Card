using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnMaker : MonoBehaviour {

	protected Player player;
	void Awake(){
		player = GetComponent<Player> ();
	}

	public virtual void OnTurnStart(){
		//add one mana to the pool
		player.OnTurnStart();
	}
		
}
