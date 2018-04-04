using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalSettings : MonoBehaviour {

	[Header("Players")]
	public Player playerRed;
	public Player playerBlue;
	[Header("Numbers and Values")]
	public float CardPreviewTime = 1f;
	public float CardTransitionTime= 1f;
	public float CardPreviewTimeFast = 0.2f;
	public float CardTransitionTimeFast = 0.5f;
	[Header("Prefabs and Assets")]
	public GameObject NoTargetSpellCardPrefab;
	public GameObject TargetedSpellCardPrefab;
	public GameObject SoldierCardPrefab;
	public GameObject SoldierPrefab;
	public GameObject DamageEffectPrefab;
	[Header("Other")]
	public Button EndTurnButton;
	public GameObject GameOverPanel;

	public Dictionary<AreaPosition, Player> Players = new Dictionary<AreaPosition, Player>();

	public static GlobalSettings Instance;

	void Awake(){
		Instance = this;
		Players.Add (AreaPosition.red, playerRed);
		Players.Add (AreaPosition.blue, playerBlue);
	}

	public bool CanControlThisPlayer(AreaPosition owner){
		bool PlayersTurn = (TurnManager.Instance.whoseTurn == Players [owner]);
		bool NotDrawingAnyCards = !Command.CardDrawPending ();
		return Players [owner].PArea.AllowedToControlThisPlayer && Players [owner].PArea.ControlON && PlayersTurn && NotDrawingAnyCards;
	}

	public bool CanControlThisPlayer(Player ownerPlayer)
	{
		bool PlayersTurn = (TurnManager.Instance.whoseTurn == ownerPlayer);
		bool NotDrawingAnyCards = !Command.CardDrawPending();
		return ownerPlayer.PArea.AllowedToControlThisPlayer && ownerPlayer.PArea.ControlON && PlayersTurn && NotDrawingAnyCards;
	}

	public void EnableEndTurnButtonOnStart(Player P)
	{
		if (P == playerBlue && CanControlThisPlayer(AreaPosition.blue) ||
			P == playerRed && CanControlThisPlayer(AreaPosition.red))
			EndTurnButton.interactable = true;
		else
			EndTurnButton.interactable = false;

	}

	public bool IsPlayer(int id)
	{
		if (playerBlue.ID == id || playerRed.ID == id)
			return true;
		else
			return false;
	}
}
