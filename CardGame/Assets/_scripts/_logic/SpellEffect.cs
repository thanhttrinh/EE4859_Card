using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffect {

	public Player owner;

	public virtual void ActivateEffect(int specialAmount = 0, ICharacter target = null)
	{
		Debug.Log ("No spell effect with this anem found.");
	}
}
