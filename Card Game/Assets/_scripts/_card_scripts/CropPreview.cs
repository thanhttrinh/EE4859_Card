using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CropPreview : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public GameObject PreviewUnit;

	public Text PreviewText;

	public Text NameText;
	public Text ManaText;
	public Text HealthText;
	public Text CropSizeText;
	public Text DescriptionText;



	public void OnPointerEnter(PointerEventData eventData)
    {
        if (SceneManager.GetActiveScene().name == "InGame")
        {
            PreviewText.text = string.Format("Name = {0}\nMana = {1}\nHealth = {2}\nCrop Size = {3}", NameText.text, ManaText.text, HealthText.text, CropSizeText.text);
            PreviewUnit.SetActive(true);
        }
	}

	public void OnPointerExit(PointerEventData eventData)
    {
        if (SceneManager.GetActiveScene().name == "InGame")
            PreviewUnit.SetActive(false);
	}

	public void OnMouseEnter()
	{
        if (SceneManager.GetActiveScene().name == "InGame")
        {
            PreviewText.text = string.Format("Name = {0}\nMana = {1}\nHealth = {2}\nCrop Size = {3}", NameText.text, ManaText.text, HealthText.text, CropSizeText.text);
            PreviewUnit.SetActive(true);
        }
	}

	public void OnMouseExit()
	{
        if (SceneManager.GetActiveScene().name == "InGame")
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
	private static SoldierPreview currentlyViewing = null;
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
