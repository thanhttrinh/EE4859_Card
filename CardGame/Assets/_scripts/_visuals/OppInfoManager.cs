using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OppInfoManager : MonoBehaviour {

	public Text redHP;
	public Text redDeckAmount;
	public Text redHandAmount;

    public Text baseHP;

	void Awake(){
		redHP.text = baseHP.text;
		redDeckAmount.text = "20";
		redHandAmount.text = "4";
	}

}
