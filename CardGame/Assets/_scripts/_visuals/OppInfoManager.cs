using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OppInfoManager : MonoBehaviour {

	public Text redHP;
	public Text redDeckAmount;
	public Text redHandAmount;

	public Text BaseHP;
	public Player redPlayer;

	void Update(){
		if(Base.Instance != null)
			redHP.text = Base.Instance.BaseRedHP.ToString();
		redDeckAmount.text = redPlayer.deck.cards.Count.ToString();
		redHandAmount.text = redPlayer.hand.CardsInHand.Count.ToString();
	}

}
