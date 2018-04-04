using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Draggable : MonoBehaviour {

	/* This class enables Drag and Drop behabiour for the cards
	 * It uses DraggingActions.cs to determine whether we can drag this game object now or not
	 */

	public bool UserPointerDisplacement = true;
	private bool dragging = false;

	//distance from the center of this card to the point where we clicked to start dragging
	private Vector3 pointerDisplacement = Vector3.zero;

	//distance of the camera to the mouse on the z axis
	private float zDisplacement;

	//reference DraggingActions.cs
	private DraggingActions da;

	//return the instance of draggable that is currently being dragged
	private static Draggable _draggingThis;
	public static Draggable DraggingThis{
		get{ return _draggingThis; }
	}

	void Awake(){
		da = GetComponent<DraggingActions> ();
	}

	void OnMouseDown(){
		//if (da != null && da.canDrag) {
		dragging = true;
		//when we drag, all preview should be off
		//HoverPreview.PreviewsAllowed = false;
		_draggingThis = this;
		da.OnStartDrag ();
		zDisplacement = -Camera.main.transform.position.z + transform.position.z;
		if (UserPointerDisplacement)
			pointerDisplacement = -transform.position + MouseInWorldCoords ();
		else
			pointerDisplacement = Vector3.zero;
		//}
	}

	void Update(){
		if (dragging) {
			Vector3 mousePos = MouseInWorldCoords ();
			transform.position = new Vector3 (mousePos.x - pointerDisplacement.x, mousePos.y - pointerDisplacement.y, transform.position.z);
			da.OnDraggingInUpdate ();
		}
	}

	void OnMouseUp(){
		if (dragging) {
			dragging = false;
			//turn all previews back on
			//HoverPreview.PreviewsAllowed = true;
			_draggingThis = null;
			da.OnEndDrag ();
		}
	}

	public void CancelDrag(){
		if (dragging) {
			dragging = false;
			_draggingThis = null;
			da.OnCancelDrag ();
		}
	}

	//returns mouse position in world coordinates for our cards to follow
	private Vector3 MouseInWorldCoords(){
		var screenMousePos = Input.mousePosition;
		screenMousePos.z = zDisplacement;
		return Camera.main.ScreenToWorldPoint (screenMousePos);
	}
}
