using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {
	//give the name of the desired card
	public string cardName;

	[HideInInspector]
	public bool cmSoldier, cmCrop, cmSpell;

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
				cmSoldier = true;
			}
				break;
		case cardType.Crops:
			{
				cmCrop = true;
			}
				break;
		case cardType.Spells:
			{
				cmSpell = true;
			}
				break;
		}
	}

	void Start () 
	{
		checkCard ();
	}

}
