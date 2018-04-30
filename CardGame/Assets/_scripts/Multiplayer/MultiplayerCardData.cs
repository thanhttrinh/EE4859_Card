using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class MultiplayerCardData : MonoBehaviour {
    GameObject go;
    public CardAsset cardAsset;
    public OneCardManager PreviewManager;
    [Header("Text Component References")]
    public Text NameText;
    public Text ManaText;
    public Text HealthText;
    public Text AttackText;
    public Text RangeText;
    public Text MovementText;
    [Header("Image References")]
    //public Image SoldierGraphicImage;
    public Image SoldierInPlayImage;

    private int hp;
    public bool isRed;
    public bool isBlue;

    //public int moving;

    public Sprite Lancer;

    public void ReadSoldierFromClientAsset(string cardName)
    {

        if (cardName == "Lancer")
        {
            NameText.text = "Lancer";

            ManaText.text = "4";

            AttackText.text = "4";
            HealthText.text = "4";
            RangeText.text = "2";
            MovementText.text = "";
            SoldierInPlayImage.sprite = Lancer;

        }




        // Change the card graphic sprite
        SoldierInPlayImage.sprite = cardAsset.CardSprite;

        if (PreviewManager != null)
        {
            PreviewManager.cardAsset = cardAsset;
            PreviewManager.ReadCardFromAsset();
        }

    }

    public void soldiercall(string cardName, GameObject go)
    {

    }

}
