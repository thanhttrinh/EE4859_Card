using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OppInfoManager : MonoBehaviour {

	public Text redHP;
	public Text redDeckAmount;
	public Text redHandAmount;

	void Awake(){
		redHP.text = "30";
		redDeckAmount.text = "20";
		redHandAmount.text = "4";
	}

}
