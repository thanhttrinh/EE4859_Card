using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum TargetingOptions
{
    NoTarget,
    AllUnits,
	AllCrops,
	AllSoldiers,
    EnemyUnits,
    EnemySoldiers,
    EnemyCrops,
    YourUnits, 
    YourSoldiers,
    YourCrops
}

/*
public enum RarityOptions
{
    Basic, Common, Rare, Epic, Legendary
}
*/

public enum TypesOfCards
{
    Soldier, Crop, Spell
}

public class CardAsset : ScriptableObject , IComparable<CardAsset>
{
    // this object will hold the info about the most general card
    [Header("General Info")]
	public CardTypeAsset cardTypeAsset; //decide what type of card this is

	public string ScriptName; //the displayed name on the card itself
    [TextArea(2,3)]
    public string Description;  // Description for spell or character
    [TextArea(2,3)]
    public string Tags;  // tags that can be searched as keywords
    
    //RarityOptions not needed for Tiny Civs
    //public RarityOptions Rarity;

	//[PreviewSprite]
    public Sprite CardImage;
	public Sprite CardSprite;
    public int ManaCost;
    
    //TokenCard not needed for Tiny Civs
    //public bool TokenCard = false; // token cards can not be seen in collection

    public int OverrideLimitOfThisCardInDeck = -1;

    public TypesOfCards TypeOfCard;


    [Header("Soldier Info")]
    [Range(1, 30)]
    public int MaxHealth;   // if =0, is a spell card  
    [Range(1, 30)]
    public int Attack;
    [Range(1, 4)]
    public int SoldierRange;
    public int Movement;
    public int AttacksForOneTurn = 1;
    public bool Charge;
    //public bool Taunt;
    //public string CreatureScriptName;
    public int SpecialCreatureAmount;

    [Header("Crop Info")]
    public int CropHealth;
    public int CropSize;
    //public string CropScriptName;
    public int SpecialCropAmount;

    [Header("Spell Info")]
    public int SpellRange;
    //public string SpellScriptName;
    public int SpecialSpellAmount;
    public TargetingOptions Targets;


    public int CompareTo (CardAsset other) 
    {
        if (other.ManaCost < this.ManaCost)
        {
            return 1;
        }
        else if (other.ManaCost > this.ManaCost)
        {
            return -1;
        }
        else
        {
            // if mana costs are equal sort in alphabetical order
            return name.CompareTo(other.name);
        }
    }

    // Define the is greater than operator.
    public static bool operator >  (CardAsset operand1, CardAsset operand2)
    {
        return operand1.CompareTo(operand2) == 1;
    }

    // Define the is less than operator.
    public static bool operator <  (CardAsset operand1, CardAsset operand2)
    {
        return operand1.CompareTo(operand2) == -1;
    }

    // Define the is greater than or equal to operator.
    public static bool operator >=  (CardAsset operand1, CardAsset operand2)
    {
        return operand1.CompareTo(operand2) >= 0;
    }

    // Define the is less than or equal to operator.
    public static bool operator <=  (CardAsset operand1, CardAsset operand2)
    {
        return operand1.CompareTo(operand2) <= 0;
    }

}
