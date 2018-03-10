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

	#region Preview Card Object
	private static bool _PreviewsAllowed = true;
	public static bool PreviewsAllowed{
		get{ return _PreviewsAllowed; }
		set{
			_PreviewsAllowed = value;
			if (!_PreviewsAllowed) {
				StopAllPreviews ();
			}
		}
	}

	public GameObject TurnOffWhenPreviewing;
	public GameObject previewGameObject;
	private static HoverPreview currentlyViewing = null;
	private static void StopAllPreviews(){
		if (currentlyViewing != null) {
			currentlyViewing.previewGameObject.SetActive (false);
			currentlyViewing.previewGameObject.transform.localScale = Vector3.one;
			currentlyViewing.previewGameObject.transform.localPosition = Vector3.zero;
			if (currentlyViewing.TurnOffWhenPreviewing != null)
				currentlyViewing.TurnOffWhenPreviewing.SetActive (true);
		}
	}
	#endregion
}
