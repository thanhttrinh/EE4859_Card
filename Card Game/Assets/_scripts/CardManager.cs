using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {
	//give the name of the desired card
	public string cardName;

	[HideInInspector]
	public bool isSr, isC, isSp;

	//public Hashtable DeckSet = new Hashtable();
	//create a new enum of the type desired for the drop down menu
	public enum cardType{
		Soldiers,
		Crops,
		Spells
	}

	public cardType type;

	//drop down menu
	public void checkCard()
	{
		//for some reason the bool for crops is flipped
		switch (type)
		{
		case cardType.Soldiers:
			{
				isSr = true;
			}
				break;
		case cardType.Crops:
			{
				isC = true;
			}
				break;
		case cardType.Spells:
			{
				isSp = true;
			}
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
