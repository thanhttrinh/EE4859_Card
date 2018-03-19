using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DraggingInBattle : MonoBehaviour 
{
	// script used to drag soldier to move/attack

	private Vector3 savedPos;
	private Vector3 attackingPos;
	private bool canAttack;
	private bool canMove;
	private RaycastHit hit;

    private void OnMouseUp()
    {

    }

    private void OnMouseDown()
    {
        
    }

    private void SoldierMove()
    {
        //once soldier gameobject is detected
        //soldier should move based on his movement parameter
        //soldier shouldnt move again until next turn so `canMove == false`
    }

}
