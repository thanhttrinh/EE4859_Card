using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CardType
{
	Soldier, Crop, Spell
}

public class CardTypeAsset : ScriptableObject{

	public CardType cardType;
	public string TypeName;
}
