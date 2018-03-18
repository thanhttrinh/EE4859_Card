using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DraggingInBattle : DraggingActions 
{
	// script used to drag soldier to move/attack

	private Vector3 savedPos;
	private Vector3 attackingPos;
	private bool canAttack;
	private bool canMove;
	private RaycastHit hit;

	void Start() {
		canAttack = true;
		canMove = true;
	}

	public override void OnStartDrag(){
		savedPos = transform.position;

	}

	public override void OnEndDrag(){
		attackingPos = transform.position + new Vector3 (0, 0, 0.5f);
		Ray attacking = new Ray (attackingPos, Vector3.back);
		if(Physics.Raycast(attacking, out hit, 2))
			Debug.Log ("Something is in front of me.");
		transform.DOMove (savedPos, 0.1f);
	}

	public override void OnDraggingInUpdate(){

	}

	protected override bool DragSuccessful(){
		return true;
	}
}
