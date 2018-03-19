using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DraggingInBattle : MonoBehaviour 
{
	// script used to drag soldier to move/attack

	private int clickCounter = 4;
	private bool canAttack;
	private bool canMove;
	private int move;

    private void OnMouseUp()
    {

    }

    private void OnMouseDown()
	{
		canMove = true;
		canAttack = true;
		clickCounter--;
		SoldierMove ();
    }

    private void SoldierMove()
    {
        //once soldier gameobject is detected
        //soldier should move based on his movement parameter
        //soldier shouldnt move again until next turn so `canMove == false`

		if (clickCounter != 0 && canMove == true) 
		{
			int.TryParse (this.gameObject.GetComponent<OneSoldierManager> ().MovementText.text, out move);
			Vector3 positiveX = new Vector3 (this.gameObject.transform.position.x + move, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
			Vector3 negativeX = new Vector3 (this.gameObject.transform.position.x - move, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
			if (Input.mousePosition.x <= this.gameObject.transform.position.x + move || Input.mousePosition.x >= this.gameObject.transform.position.x - move) 
			{
				this.gameObject.transform.position = new Vector3((int)Input.mousePosition.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
			}
		}
    }

}
