using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler  {

	//vectors to track the card's movement on screen
	private Vector3 offset;
	private Vector3 curPositionCard;
	private Vector3 newPositionCard;

	[HideInInspector]
	public Transform parentToReturnTo = null;

	public void OnBeginDrag(PointerEventData eventData){
		parentToReturnTo = this.transform.parent;
		this.transform.SetParent (this.transform.parent.parent);

		GetComponent<CanvasGroup> ().blocksRaycasts = false;

	}

	public void OnDrag (PointerEventData eventData){
		this.transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData){
		this.transform.SetParent (parentToReturnTo);

		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		Debug.Log (gameObject.GetComponent<CardManager>().cardName);
	}

}