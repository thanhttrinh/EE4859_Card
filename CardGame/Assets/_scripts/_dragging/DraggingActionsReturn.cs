using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DraggingActionsReturn : DraggingActions {

	private Vector3 savedPos;
	public bool dragging;

	public override void OnStartDrag(){
		savedPos = transform.position;
		dragging = true;
	}

	public override void OnEndDrag(){
		transform.DOMove (savedPos, 0.1f);
		dragging = false;
		//transform.DOMove (savedPos, 1f).SetEase (Ease.OutBounce, 0.5f, 0.1f);
		//transform.DOMove (savedPos, 1f).SetEase (Ease.OutQuint, 0.5f, 0.1f);
	}

	public override void OnDraggingInUpdate(){
	
	}

	protected override bool DragSuccessful(){
		return true;
	}

	public override void OnCancelDrag(){

	}
}
