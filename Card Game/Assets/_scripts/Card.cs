using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

	public string name;
	public int mana;

	public enum cardType {
		isSoldier,
		isCrop,
		isSpell
	}

	public Card() {
		name = "";
		mana = 0;
	}

	public Card(string n, int m) {
		name = n;
		mana = m;
	}

	public string getName() {
		return name;
	}

	public int getMana() {
		return mana;
	}
}
