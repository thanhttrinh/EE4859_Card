using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneSoldierManager : MonoBehaviour
{
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

    void Awake()
    {
        if (cardAsset != null)
            ReadSoldierFromAsset();
    }

	private bool canAttackNow = false;
	public bool CanAttackNow{
		get{ return canAttackNow; }
		set{
			canAttackNow = value;

		}
	}

    public void ReadSoldierFromAsset()
    {
		NameText.text = cardAsset.name.ToString ();
		ManaText.text = cardAsset.ManaCost.ToString ();

        AttackText.text = cardAsset.Attack.ToString();
        HealthText.text = cardAsset.MaxHealth.ToString();
        RangeText.text = cardAsset.SoldierRange.ToString();
        MovementText.text = cardAsset.Movement.ToString();

		// Change the card graphic sprite
		SoldierInPlayImage.sprite = cardAsset.CardSprite;

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
