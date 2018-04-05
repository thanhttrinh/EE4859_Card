using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeckVisual : MonoBehaviour {

	public AreaPosition owner;
	public float HeightOfOneCard = 0.012f;

	void Start(){
		cardsInDeck = GlobalSettings.Instance.Players[owner].deck.cards.Count;
		Debug.Log (cardsInDeck.ToString ());
	}

	private int cardsInDeck = 0;
	public int CardsInDeck{
		get{return cardsInDeck;}
		set{
			cardsInDeck = value;
			transform.localPosition = new Vector3(0, 0, -HeightOfOneCard * value);
		}
	}
}
