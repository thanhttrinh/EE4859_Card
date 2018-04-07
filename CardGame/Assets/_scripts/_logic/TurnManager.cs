using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TurnManager : MonoBehaviour {

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
			if (tm is PlayerTurnMaker)
				whoseTurn.HighlightPlayableCards ();
			whoseTurn.HighlightPlayableCards (true);
		}
	}

	void Awake(){
		Players = GameObject.FindObjectsOfType<Player>();
		Instance = this;
		timer = GetComponent<TimerVisual>();
	}

	void Start(){
		Debug.Log ("Game is starting");
		OnGameStart ();
		Debug.Log ("Game already started");
	}

	public void OnGameStart(){
		Debug.Log ("OnGameStart");
		CardLogic.CardsCreatedThisGame.Clear ();
		SoldierLogic.SoldiersCreatedThisGame.Clear ();
		Debug.Log ("card logic and soldier logic cleared");
		foreach (Player p in Players) {
			p.ManaThisTurn = 0;
			p.ManaLeft = 0;
			p.LoadPlayerInfoFromAsset ();
			p.TransmitInfoAboutPlayer ();
			p.PArea.PDeck.CardsInDeck = p.deck.cards.Count;
		}
			
		int rng = Random.Range(0, 2);
		Player whoGoesFirst = Players[rng];
		Player whoGoesSecond = whoGoesFirst.otherPlayer;

		Debug.Log ("first is " + whoGoesFirst.name.ToString ());
		int initDraw = 3;
		for(int i = 0; i < initDraw; i++){
			whoGoesSecond.DrawACard(true);
			whoGoesFirst.DrawACard(true);
		}

		whoGoesSecond.DrawACard(true);
		new StartATurnCommand(whoGoesFirst).AddToQueue();
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Space))
			EndTurn ();
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
