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
    public Text HealthText;
    public Text AttackText;
    public Text RangeText;
    public Text MovementText;
    [Header("Image References")]
    public Image CreatureGraphicImage;

    private int hp;

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
        // Change the card graphic sprite
        CreatureGraphicImage.sprite = cardAsset.CardImage;

		NameText.text = cardAsset.name.ToString ();
        AttackText.text = cardAsset.Attack.ToString();
        HealthText.text = cardAsset.MaxHealth.ToString();
        RangeText.text = cardAsset.SoldierRange.ToString();
        MovementText.text = cardAsset.Movement.ToString();

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

    /*
    public bool ValidMove(OneSoldierManager[,] sBoard, OneCropManager[,] cBoard, int x1, int y1, int x2, int y2)
    {
        int deltaMoveX = Mathf.Abs(x1 - x2);
        int deltaMoveY = Mathf.Abs(y1 - y2);

        Debug.Log("deltaMoveX = " + deltaMoveX + ", deltaMoveY = " + deltaMoveY);

        //if moving on top of another soldier/crop
        if (sBoard[x2, y2] != null || cBoard[x2, y2] != null)
        {
            Debug.Log("false");
            return false;
        }

        //if soldier moves over a crop
        
        
        //sBoard[x1,y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.TypeOfCard == TypesOfCards.Soldier
        //if (isAllySoldier)
        //{
            //if deltaMoveX and deltaMoveY is less than or equal to the soldiers movement
            //deltaMoveX <= board[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement
            if ((deltaMoveX == 0 && deltaMoveY <= sBoard[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement) || (deltaMoveX <= sBoard[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement && deltaMoveY == 0))
            {
                Debug.Log("true & soldier has "+ sBoard[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement +"movement and deltaMoveX =" + deltaMoveX + "and deltaMoveY = "+ deltaMoveY);
                return true;
                
            }
            //if player reaches to an enemy soldier at its max movement+1 and wanted to attack
            /*
            else if(deltaMoveX == sBoard[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement + 1)
            {
                deltaMoveX = sBoard[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement;
                Debug.Log("true in x movement");
                return true;
            }
            else if (deltaMoveY == sBoard[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement + 1)
            {
                deltaMoveY = sBoard[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement;
                Debug.Log("true in y movement");
                return true;
            }
            
        //}
        Debug.Log("false");
        return false;
    }
    */
}
