using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	private GameObject deckBuilder;
	private bool isInDeck;

	private Vector3 offset;
	private Vector3 curPositionCard;
	private Vector3 newPositionCard;

	void start()
	{
		deckBuilder = GameObject.FindGameObjectWithTag ("cardBuilder");
	}

	//click and hold the left mouse button
	void OnMouseDown()
	{
		curPositionCard = gameObject.transform.position;
		offset = curPositionCard - Camera.main.ScreenToWorldPoint (
			new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10.0f));
	}

	//the object that is clicked on will follow the position of the mouse
	void OnMouseDrag()
	{
		Vector3 newPosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10.0f);
		transform.position = Camera.main.ScreenToWorldPoint (newPosition) + offset;
	}

	void OnMouseUp()
	{
		newPositionCard = gameObject.transform.position;

		Debug.Log ("old position  :   " + curPositionCard);
		Debug.Log ("new position  :   " + newPositionCard);
		//this gives obj reference not set to instance error
		//Debug.Log ("Deck position :   " + deckBuilder.transform.position.x);

		//if card is not within the card builder then return to original position
		if (newPositionCard.x < 5.5f) {
			Debug.Log ("card is in deck");
		}
	}


}