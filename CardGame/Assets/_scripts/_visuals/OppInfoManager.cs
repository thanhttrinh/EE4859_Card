using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OppInfoManager : MonoBehaviour {

	public Text redHP;
	public Text redDeckAmount;
	public Text redHandAmount;

    public Text baseHP;
	public Player redPlayer;

	void Update(){
		redHP.text = baseHP.text;
		redDeckAmount.text = redPlayer.deck.cards.Count.ToString();
		redHandAmount.text = redPlayer.hand.CardsInHand.Count.ToString();
	}

}
