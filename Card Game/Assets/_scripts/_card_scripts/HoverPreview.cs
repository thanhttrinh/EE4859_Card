using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverPreview : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public GameObject previewUnit;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerEnter(PointerEventData eventData) {
		previewUnit.SetActive(true);
	}

	public void OnPointerExit(PointerEventData eventData) {
		previewUnit.SetActive(false);
	}

}
