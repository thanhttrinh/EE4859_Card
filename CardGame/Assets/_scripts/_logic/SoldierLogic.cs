using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoldierLogic : ICharacter 
{
	public Player owner;
	public CardAsset ca;
	public int UniqueSoldierID;

	public int ID{
		get { return UniqueSoldierID; }
	}

	//the basic health that we have in CardAsset
	private int baseHealth;
	public int MaxHealth{
		get{ return baseHealth; }
	}

	//current health of this soldier
	private int health;
	public int Health
	{
		get{ return health; }
		set{
			if (value > MaxHealth)
				health = MaxHealth;
			else if (value <= 0)
				Die ();
			else
				health = value;
		}
	}

	private int baseAttack;
	public int Attack{
		get{ return baseAttack; }
	}

	public bool CanAttack{
		get{
			bool ownersTurn = (TurnManager.Instance.whoseTurn == owner);
			return (ownersTurn && (AttacksLeftThisTurn > 0));
		}
	}

	private int attacksForOneTurn = 1;
	public int AttacksLeftThisTurn {
		get;
		set;
	}

	//constructor
	public SoldierLogic(Player owner, CardAsset ca){
		this.ca = ca;
		baseHealth = ca.MaxHealth;
		health = ca.MaxHealth;
		baseAttack = ca.Attack;
		attacksForOneTurn = ca.AttacksForOneTurn;
		if (ca.Charge)
			AttacksLeftThisTurn = attacksForOneTurn;
		this.owner = owner;
		UniqueSoldierID = IDFactory.GetUniqueID ();

		SoldiersCreatedThisGame.Add (UniqueSoldierID, this);
	}

	public void OnTurnStart(){
		AttacksLeftThisTurn = attacksForOneTurn;
	}

	public void Die(){
		owner.grid.SoldiersOnGrid.Remove (this);
		//new SoldierDieCommand (UniqueSoldierID, owner).AddToQueue ();
	}

	public void GoFace(){
		AttacksLeftThisTurn--;
		int targetHealthAfter = owner.otherPlayer.Health - Attack;
		//new SoldierAttackCommand (owner.otherPlayer.PlayerID, UniqueSoldierID, 0, Attack, Health, targetHealthAfter).AddToQueue ();
		owner.otherPlayer.Health -= Attack;
	}

	public void AttackSoldier(SoldierLogic target){
		AttacksLeftThisTurn--;
		int targetHealthAfter = target.Health - Attack;
		int attackerHealthAfter = Health - target.Attack;
		//new SoldierAttackCommand(target.UniqueSoldierID, UniqueSoldierID, target.Attack, Attack, targetHealthAfter, targetHealthAfter).AddToQueue ();

		target.Health -= Attack;
		Health -= target.Attack;
	}

	public void AttackSoldierWithID(int uniqueSoldierID){
		SoldierLogic target = SoldierLogic.SoldiersCreatedThisGame [uniqueSoldierID];
		AttackSoldier (target);
	}

	//static for managing IDs
	public static Dictionary<int, SoldierLogic> SoldiersCreatedThisGame = new Dictionary<int, SoldierLogic>();
}
