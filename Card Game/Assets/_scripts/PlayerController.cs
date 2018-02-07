using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	private GameObject cardBuilder;

	private Vector3 offset;
	private Vector3 curPositionCard;
	private Vector3 newPositionCard;

	void start()
	{
		//object of where cards will be drag to in order to create a deck
		//currently unused
		cardBuilder = GameObject.FindGameObjectWithTag ("cardBuilder");
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

		//test position of the card if it's within the card builder
		if (newPositionCard.x > 5.5f) {
			Debug.Log ("card IS in deck");
		} 
		//if it is NOT within card builder, go back to original position
		else {
			Debug.Log ("card IS NOT in deck");
			transform.position = curPositionCard;
		}

	}


}