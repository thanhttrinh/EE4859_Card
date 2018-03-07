using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverPreview : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public GameObject PreviewUnit;

	public Text PreviewText;

	public Text NameText;
	public Text ManaText;
	public Text AttackText;
	public Text HealthText;
	public Text RangeText;
	public Text MovementText;
	public Text DescriptionText;
	public Text CropSizeText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerEnter(PointerEventData eventData) {
		//if (eventData.pointerEnter.GetComponent<OneCardManager> ().cardAsset.TypeOfCard == TypesOfCards.Soldier) 
		//{
		PreviewText.text = string.Format ("Name = {0}", NameText.text);
		//}
		PreviewUnit.SetActive(true);
	}

	public void OnPointerExit(PointerEventData eventData) {
		PreviewUnit.SetActive(false);
	}

}
