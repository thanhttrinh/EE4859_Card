using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDeckScript : MonoBehaviour {

	public void MakeANewDeck(){
		CCScreen.Instance.HideScreen ();
		CCScreen.Instance.BuildADeck ();
	}
}
