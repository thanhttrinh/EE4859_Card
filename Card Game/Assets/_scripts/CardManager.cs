using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {
	//give the name of the desired card
	public string card;

	//public Hashtable DeckSet = new Hashtable();
	public enum cardType{
		Soldiers,
		Crops,
		Spells
	}

	public cardType type;

	public void checkCard()
	{
		switch (type)
		{
			case cardType.Soldiers:
				break;
			case cardType.Crops:
				break;
			case cardType.Spells:
				break;
		}
	}

	void Start () 
	{
		
	}

	void Update () 
	{

	}

	void OnCollisionEnter2D(Collision2D obj)
	{
		if (obj.gameObject.tag == "card") {
			Debug.Log ("it's a card ");
			Debug.Log (obj.gameObject.tag);
		}
	}
}
