using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardNameRibbon : MonoBehaviour {

	public Text NameText;
	public Text QuantityText;
	public Text ManaCost;

	public CardAsset Asset{get; set;}
	public int Quantity{get; set;}

	public void ApplyAsset(CardAsset ca, int quantity){
		Asset = ca;
		NameText.text = ca.name;
		ManaCost.text = ca.ManaCost.ToString();
		SetQuantity (quantity);
	}

	public void SetQuantity(int quantity){
		if (quantity == 0)
			return;

		QuantityText.text = "x" + quantity.ToString ();
		Quantity = quantity;
	}

	public void ReduceQuantity(){
		Debug.Log ("Removing " + Asset.name);
		CCScreen.Instance.BuilderScript.RemoveCard (Asset);
	}
}
