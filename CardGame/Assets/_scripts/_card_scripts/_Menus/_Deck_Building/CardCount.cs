using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardCount : MonoBehaviour {

	public int count;
	public Text countText;

	public static CardCount Instance;

	void Awake(){
		Instance = this;
	}

	void Start(){
		count = 0;
		SetCountText ();
	}

	public void SetCountText(){
		countText.text = count.ToString () + "/20";
	}
}
