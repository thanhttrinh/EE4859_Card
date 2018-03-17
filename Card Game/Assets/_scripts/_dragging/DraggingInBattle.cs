using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DraggingInBattle : DraggingActions 
{
	// script used to drag soldier to move/attack

	private Vector3 savedPos;
	private bool canAttack;
	private bool canMove;

	public override void OnStartDrag(){
		savedPos = transform.position;
	}

	public override void OnEndDrag(){
		transform.DOMove (savedPos, 0.1f);
		//transform.DOMove (savedPos, 1f).SetEase (Ease.OutBounce, 0.5f, 0.1f);
		//transform.DOMove (savedPos, 1f).SetEase (Ease.OutQuint, 0.5f, 0.1f);
	}

	public override void OnDraggingInUpdate(){

	}

	protected override bool DragSuccessful(){
		return true;
	}
}
