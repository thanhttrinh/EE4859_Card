using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICharacter {
	
	//int id from iD factory
	public int PlayerID;

	public PlayerAsset pAsset;
	// references to all the visual game objects for this player
	public PlayerArea PArea;
	public string PlayerColor;

	//references to logical properties belonging to this player
	public Deck deck;
	public Hand hand;
	//public GridBoard grid;

	//an array to store both players
	//always have 2 
	public static Player[] Players;

	public static Player Instance;

	private int bonusManaThisTurn = 0;

	//property of the player
	public int ID{
		get{ return PlayerID; }
	}

	//opponent player
	public Player otherPlayer{
		get{
			if (Players [0] == this)
				return Players [1];
			else 
				return Players [0];
		}
	}

	//total mana that this player has this turn
	private int manaThisTurn;
	public int ManaThisTurn
	{
		get{ return manaThisTurn;}
		set
		{
			if (value < 0)
				manaThisTurn = 0;
		//	else if (value > PArea.ManaBar.Crystals.Length)
			//	manaThisTurn = PArea.ManaBar.Crystals.Length;
			else
				manaThisTurn = value;
			new UpdateManaCommand(this, manaThisTurn, manaLeft).AddToQueue();
		}
	}

	private int manaLeft;
	public int ManaLeft
	{
		get
		{ return manaLeft;}
		set
		{
			if (value < 0)
				manaLeft = 0;
			//else if (value > PArea.ManaBar.Crystals.Length)
				//manaLeft = PArea.ManaBar.Crystals.Length;
			else
				manaLeft = value;

			PArea.ManaBar.AvailableCrystals = manaLeft;
			new UpdateManaCommand(this, ManaThisTurn, manaLeft).AddToQueue();
		//	if (TurnManager.Instance.whoseTurn == this)
			//	HighlightPlayableCards();
		}
	}

	private int health;
	public int Health
	{
		get { return health;}
		set
		{
			if (value > pAsset.MaxHealth)
				health = pAsset.MaxHealth;
			else
				health = value;
		//	if (value <= 0)
				//Die(); 
		}
	}

	//code for events to let soldiers know when to cause effects
	public delegate void VoidWithNoArguments();
	public event VoidWithNoArguments EndTurnEvent;

	void Awake(){
		Instance = this;
		Players = GameObject.FindObjectsOfType<Player> ();
		PlayerID = IDFactory.GetUniqueID ();
	}

	public virtual void OnTurnStart(){
		//add one mana to the pool
		manaThisTurn++;
        this.PArea.ManaBar.AvailableCrystals = ManaThisTurn;
		manaLeft = manaThisTurn;
		GameUnits.Instance.moving = true;
		
		//foreach (SoldierLogic sl in grid.SoldiersOnGrid)
			//sl.OnTurnStart (); 
	}

	public void OnTurnEnd(){
		if (EndTurnEvent != null)
			EndTurnEvent.Invoke ();
		manaThisTurn -= bonusManaThisTurn;
		bonusManaThisTurn = 0;
		GetComponent<TurnMaker> ().StopAllCoroutines ();
		if (GameUnits.Instance.moving == false)
			GameUnits.Instance.moving = true;
	}

	public void GetBonusMana(int amount){
		bonusManaThisTurn += amount;
		manaThisTurn += amount;
		manaLeft += amount;
	}

	//draw a card
	public void DrawACard(bool fast = false){
		if (deck.cards.Count > 0) {
           // if(PlayerColor == "red")
			   // Debug.Log ("Cards currently in red deck: " + (deck.cards.Count - 1).ToString());
           // if (PlayerColor == "blue")
                //Debug.Log("Cards currently in blue deck: " + (deck.cards.Count - 1).ToString());
			if (hand.CardsInHand.Count < PArea.handVisual.slots.Children.Length) {
				//Debug.Log ("adding card to hand");
				// add card to hand
				CardLogic newCard = new CardLogic (deck.cards [0], this);
				hand.CardsInHand.Insert (0, newCard);
				//remove the card from the deck
				deck.cards.RemoveAt (0);
				//create a command
			   // new DrawACardCommand (hand.CardsInHand [0], this, fast, fromDeck: true).AddToQueue ();
			   PArea.PDeck.CardsInDeck--;
			   PArea.handVisual.GivePlayerACard(newCard.ca, newCard.UniqueCardID, fast, true);
			   
				//Debug.Log ("DrawACard Success");
			}
		} else {
			//TODO: what to do when run out of cards
		}
	}
	
	//start game
	public void LoadPlayerInfoFromAsset(){
		Health = pAsset.MaxHealth;
	}

	public void TransmitInfoAboutPlayer(){
		PArea.AllowedToControlThisPlayer = true;
	}


}
