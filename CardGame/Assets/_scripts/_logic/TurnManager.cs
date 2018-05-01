using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour {
	public Text ColorText;
	public static TurnManager Instance;

	public static Player[] Players;

	private TimerVisual timer;

	private Player _whoseTurn;
	public Player whoseTurn{
		get{ return _whoseTurn; }
		set{
			_whoseTurn = value;
			timer.StartTimer ();

			GlobalSettings.Instance.EnableEndTurnButtonOnStart (_whoseTurn);
			TurnMaker tm = whoseTurn.GetComponent<TurnMaker> ();
			tm.OnTurnStart ();
			//if (tm is PlayerTurnMaker)
			//	whoseTurn.HighlightPlayableCards ();
			//whoseTurn.HighlightPlayableCards (true);
		}
	}

	void Awake(){
		Players = GameObject.FindObjectsOfType<Player>();
		Instance = this;
		timer = GetComponent<TimerVisual>();
	}

	void Start(){
		OnGameStart ();
	}

	public void OnGameStart(){
		Deck.Instance.ShuffleDeck();
		CardLogic.CardsCreatedThisGame.Clear ();
		SoldierLogic.SoldiersCreatedThisGame.Clear ();
		foreach (Player p in Players) {
			p.ManaThisTurn = 0;
			p.ManaLeft = 0;
			p.LoadPlayerInfoFromAsset ();
			p.TransmitInfoAboutPlayer ();
			p.PArea.PDeck.CardsInDeck = p.deck.cards.Count;
			if(p.PlayerColor == "red"){
				ColorText.text = string.Format("PLAYER RED");
				ColorText.color = new Color(255.0f, 62.0f, 62.0f, 255.0f);
			}
			else if(p.PlayerColor == "blue"){
				ColorText.text = string.Format("PLAYER BLUE");
				ColorText.color = new Color(62.0f, 151.0f, 255.0f, 255.0f);
			}
		}
			
		int rng = Random.Range(0, 1);
		Player whoGoesFirst = Players[rng];
		Player whoGoesSecond = whoGoesFirst.otherPlayer;

		Debug.Log ("first is " + whoGoesFirst.name.ToString ());
		int initDraw = 3;
		for(int i = 0; i < initDraw; i++){
			whoGoesSecond.DrawACard(true);
		//	Debug.Log (whoGoesSecond.name.ToString () + " drew a card");
			whoGoesFirst.DrawACard(true);
//			Debug.Log (whoGoesFirst.name.ToString () + " drew a card");
		}

		whoGoesSecond.DrawACard(true);
		new StartATurnCommand(whoGoesFirst).AddToQueue();
	}

	public void EndTurn(){
		Draggable[] AllDraggableObjects = GameObject.FindObjectsOfType<Draggable> ();
		foreach (Draggable d in AllDraggableObjects)
			d.CancelDrag ();
		timer.StopTimer ();
		whoseTurn.OnTurnEnd ();

		new StartATurnCommand(whoseTurn.otherPlayer).AddToQueue();
	}
			
	public void StopTheTimer(){
		timer.StopTimer ();
	}
}
