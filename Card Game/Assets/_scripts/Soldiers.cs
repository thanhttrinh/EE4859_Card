using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldiers : Card {

	public int hp;
	public int atk;
	public int movement;
	public int range;
	public cardType type = cardType.isSoldier;

	public Soldiers() {
		this.name = "";
		this.mana = 0;
		this.hp = 0;
		this.atk = 0;
		this.movement = 0;
		this.range = 0;
	}

	public Soldiers(string n, int m, int h, int a, int move, int r) {
		this.name = n;
		this.mana = m;
		this.hp = h;
		this.atk = a;
		this.movement = move;
		this.range = r;
	}

	public string getName() {
		return name;
	}

	public int getMana() {
		return mana;
	}

	public int getHP() {
		return hp;
	}

	public int getAttack() {
		return atk;
	}

	public int getMovement() {
		return movement;
	}

	public int getRange() {
		return range;
	}

	public int wasAttacked() {
		hp = hp - ; //health subtracted by attack damage of enemy
	}

	public void isDead() {
	
	}
}
