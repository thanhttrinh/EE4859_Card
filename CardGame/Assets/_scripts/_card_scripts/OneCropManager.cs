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
    public Image CropInPlayImage;

    private int hp;

    void Awake()
    {
        if (cardAsset != null)
            ReadCropFromAsset();
    }

    public void ReadCropFromAsset()
    {
        // Change the card graphic sprite
        CropInPlayImage.sprite = cardAsset.CardSprite;

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
