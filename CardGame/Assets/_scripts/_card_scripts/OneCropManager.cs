using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneCropManager : MonoBehaviour
{
    public CardAsset cardAsset;
    public OneCardManager PreviewManager;
    [Header("Text Component References")]
	public Text NameText;
    public Text HealthText;
    public Text CropSizeText;
    [Header("Image References")]
    public Image CreatureGraphicImage;

    private int hp;

    void Awake()
    {
        if (cardAsset != null)
            ReadCreatureFromAsset();
    }

    public void ReadCreatureFromAsset()
    {
        // Change the card graphic sprite
        CreatureGraphicImage.sprite = cardAsset.CardImage;

		NameText.text = cardAsset.name.ToString ();
        HealthText.text = cardAsset.CropHealth.ToString();
        CropSizeText.text = cardAsset.CropSize.ToString();

        if (PreviewManager != null)
        {
            PreviewManager.cardAsset = cardAsset;
            PreviewManager.ReadCardFromAsset();
        }

    }

    public void TakeDamage(int amount)
    {
        int hpAfter;
        if (amount > 0)
        {
            int.TryParse(HealthText.text, out hp);
            hpAfter = hp - amount;
            HealthText.text = hpAfter.ToString();
        }
    }
}
