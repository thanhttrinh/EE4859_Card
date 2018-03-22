using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SpellPreview : MonoBehaviour
{
    public GameObject PreviewUnit;

    public Text PreviewText;
    public Text NameText;
    public Text ManaText;
    public Text RangeText;
    public Text DescriptionText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (SceneManager.GetActiveScene().name == "InGame")
        {
            PreviewText.text = string.Format("Name = {0}\nMana = {1}\nRange = {2}\nDescription = {3}", NameText.text, ManaText.text, RangeText.text, DescriptionText.text);
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
            PreviewText.text = string.Format("Name = {0}\nMana = {1}\nRange = {2}\nDescription = {3}", NameText.text, ManaText.text, RangeText.text, DescriptionText.text);
            PreviewUnit.SetActive(true);
        }
    }

    public void OnMouseExit()
    {
        if (SceneManager.GetActiveScene().name == "InGame")
            PreviewUnit.SetActive(false);
    }
}
