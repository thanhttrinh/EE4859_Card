using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeckVisual : MonoBehaviour {

	public AreaPosition owner;
	public float HeightOfOneCard = 0.012f;
	private int cardsInDeck = 0;

	void Start(){
		cardsInDeck = GlobalSettings.Instance.Players [owner].deck.cards.Count;
	}
		
	public int CardsInDeck{
		get{return cardsInDeck;}
		set{
			cardsInDeck = value;
			transform.localPosition = new Vector3(0, 0, -HeightOfOneCard * value);
		}
	}
}
