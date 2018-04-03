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
            ReadCreatureFromAsset();
    }

    public void ReadCreatureFromAsset()
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

    public bool ValidMove(OneSoldierManager[,] board, int x1, int y1, int x2, int y2)
    {
        //if moving on top of another soldier/crop
        if (board[x2, y2] != null)
            return false;

        int deltaMoveX = Mathf.Abs(x1 - x2);
        int deltaMoveY = Mathf.Abs(y1 - y2);
        if(board[x1,y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.TypeOfCard == TypesOfCards.Soldier)
        {
            //if deltaMoveX and deltaMoveY is less than or equal to the soldiers movement
            //deltaMoveX <= board[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement
            if ((deltaMoveX == 0 && deltaMoveY <= board[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement) || (deltaMoveX <= board[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement && deltaMoveY == 0))
            {
                Debug.Log("true 1 & soldier has "+ board[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement +"movement and deltaMoveX =" + deltaMoveX + "and deltaMoveY = "+ deltaMoveY);
                return true;
                
            }
            //if player reaches to an enemy soldier at its max movement and wanted to attack
            /*
            else if(deltaMoveX == board[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement + 1)
            {
                deltaMoveX = board[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement;
                Debug.Log("true 2");
                return true;
            }
            else if (deltaMoveY == board[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement + 1)
            {
                deltaMoveY = board[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement;
                Debug.Log("true 3");
                return true;
            }
            */
        }
        Debug.Log("false");
        return false;
    }
}
