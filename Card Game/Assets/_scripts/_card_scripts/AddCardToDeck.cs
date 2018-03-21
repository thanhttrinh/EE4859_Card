using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AddCardToDeck : MonoBehaviour {

	//public Text quantityText;
	private float initialScale;
	//private float scaleFactor = 1.1f;
	private CardAsset cardAsset;

	void Awake(){
		//initialScale = transform.localScale.x;
	}

	public void SetCardAsset(CardAsset asset){
		cardAsset = asset;
	}

	void OnMouseDown(){
		CardAsset asset = GetComponent<OneCardManager> ().cardAsset;

		if (asset == null)
			return;
		
		//check if these cards are available in collection
		//if (CardCollection.Instance.QuantityOfEachCard [cardAsset] - CCScreen.Instance.BuilderScript.NumberOfThisCardInDeck (cardAsset) > 0) {
		CCScreen.Instance.BuilderScript.AddCard (asset);
		Debug.Log ("Added " + asset.name);
		UpdateQuantity ();
		//}
	}

	void OnMouseEnter(){
		//transform.DOScale (initialScale * scaleFactor, 0.5f);
	}

	void OnMouseExit()
	{
		//transform.DOScale (initialScale * scaleFactor, 0.5f);
	}

	void Update()
	{
		if (Input.GetMouseButtonDown (1))
			OnRightClick ();
	}

	void OnRightClick(){
		CardAsset asset = GetComponent<OneCardManager> ().cardAsset;
		//cast a ray from the mouse, the cursor's position
		Ray clickPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitPoint;

		Debug.Log ("clicked on " + this.name);

		//check if the ray collided with an object
		if (Physics.Raycast (clickPoint, out hitPoint)) {
			if (hitPoint.collider == this.GetComponent<Collider> ()) {
				Debug.Log ("right clicked on " + this.name);

			}
		}
	}

	public void UpdateQuantity()
	{
		int quantity = CardCollection.Instance.QuantityOfEachCard [cardAsset];
		if (CCScreen.Instance.BuilderScript.InDeckBuildingMode && CCScreen.Instance.ShowReducedQuantitiesInDeckBuilding)
			quantity -= CCScreen.Instance.BuilderScript.NumberOfThisCardInDeck (cardAsset);

	}
}
