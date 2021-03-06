﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SoldierPreview : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public GameObject PreviewUnit;

	public Text PreviewText;

	public Text NameText;
	public Text ManaText;
	public Text AttackText;
	public Text HealthText;
	public Text RangeText;
	public Text MovementText;


	public void OnPointerEnter(PointerEventData eventData)
    {
		if(SceneManager.GetActiveScene().name == "InGame" && PreviewUnit != null)
        {
            PreviewText.text = string.Format("Name = {0}\nMana = {1}\nAttack = {2}\nHealth = {3}\nRange = {4}\nMovement = {5}", 
			NameText.text, ManaText.text, AttackText.text, HealthText.text, RangeText.text, MovementText.text);
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
		if (SceneManager.GetActiveScene().name == "InGame" && PreviewUnit != null)
        {
            PreviewText.text = string.Format("Name = {0}\nMana = {1}\nAttack = {2}\nHealth = {3}\nRange = {4}\nMovement = {5}", 
			NameText.text, ManaText.text, AttackText.text, HealthText.text, RangeText.text, MovementText.text);
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
