using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColorIndicator : MonoBehaviour {
	public Text ColorText;
	public Player playerBlue;
	public Player playerRed;
	void Awake(){
		if(Player.Instance.PArea == playerRed){
			Debug.Log("red");
			ColorText.text = string.Format("PLAYER RED");
			ColorText.color = new Color(255.0f, 62.0f, 62.0f, 255.0f);
			Debug.Log("endred");
		}
		else if(Player.Instance.PArea == playerRed){
			Debug.Log("blue");
			ColorText.text = string.Format("PLAYER BLUE");
			ColorText.color = new Color(62.0f, 151.0f, 255.0f, 255.0f);
			Debug.Log("endblue");
		}
	}
}
