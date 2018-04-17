using UnityEngine;
using System;

public class attack : MonoBehaviour
{
	public bool ViableAttack(GameUnits[,] board, int atkx, int atky, int vicx, int vicy)
	{
		if(!board[atkx, atky].ValidMove(board, atkx, atky, vicx, vicy))
		{
            if (board[atkx, atky] != null && board[vicx, vicy] != null)
            {
                if (board[atkx, atky].GetComponent<OneCardManager>() != null)
                {
                    if (board[atkx, atky].GetComponent<OneCardManager>().AttackText.text != null)
                    {
                        return true;
                    }
                }
            }
		}
		return false;
	}
	//Do ViableAttack and have it == true first
	public void doAttack(Player player, GameUnits[,] board, int atkx, int atky, int vicx, int vicy)
	{
		int atkDam = 0, vicDam = 0, atkHP = 0, vicHP = 0;
        bool isBase = false;
        Debug.Log("AtkDamVar: " + atkDam + ", cardAsset.Attack: " + board[atkx, atky].GetComponent<OneSoldierManager>().cardAsset.Attack);
        if (!ViableAttack(board, atkx, atky, vicx, vicy))
        {
            Int32.TryParse(board[atkx, atky].GetComponent<OneSoldierManager>().AttackText.text, out atkDam);
            Int32.TryParse(board[atkx, atky].GetComponent<OneSoldierManager>().HealthText.text, out atkHP);
            Int32.TryParse(board[vicx, vicy].GetComponent<OneSoldierManager>().AttackText.text, out vicDam);
            Int32.TryParse(board[atkx, atky].GetComponent<OneSoldierManager>().HealthText.text, out vicHP);
            if (atkDam == 0 && atkHP == 0 && vicDam == 0 && vicHP == 0)
            {
                atkDam = board[atkx, atky].GetComponent<OneSoldierManager>().cardAsset.Attack;
                Debug.Log("AtkDamVar: " + atkDam + ", cardAsset.Attack: " + board[atkx, atky].GetComponent<OneSoldierManager>().cardAsset.Attack);
                atkHP = board[atkx, atky].GetComponent<OneSoldierManager>().cardAsset.MaxHealth;

                if (board[vicx, vicy] != null)
                {
                    vicDam = board[vicx, vicy].GetComponent<OneSoldierManager>().cardAsset.Attack;
                    vicHP = board[vicx, vicy].GetComponent<OneSoldierManager>().cardAsset.MaxHealth;
                }
                else
                {
                    vicHP = board[vicx, vicy].GetComponent<Base>().BaseHP;
                    isBase = true;
                }
            }
        }

		if (atkDam >= vicHP && !isBase)
		{
            Debug.Log("Vic " + vicHP + " Atk " + atkDam);
            DestroyObject(board[vicx, vicy].gameObject);
			board[vicx, vicy] = null;
		}
		else if (atkDam < vicHP && vicDam >= atkHP && !isBase)
		{
            Debug.Log("Vic " + vicHP + " Atk " + atkDam);
            Debug.Log("Atk " + atkHP + " Vic " + vicDam);
            board[vicx, vicy].GetComponent<OneSoldierManager>().HealthText.text = "" + (vicHP - atkDam);
            DestroyObject(board[atkx, atky].gameObject);
			board[atkx, atky] = null;
		}
		else if (atkDam < vicHP && vicDam < atkHP && !isBase)
		{
			board[vicx, vicy].GetComponent<OneSoldierManager>().HealthText.text = "" + (vicHP - atkDam);
			board[atkx, atky].GetComponent<OneSoldierManager>().HealthText.text = "" + (atkHP - vicDam);
		}
        else if(atkDam < vicHP && isBase)
        {
            board[vicx, vicy].GetComponent<Base>().BaseHP = vicHP - atkDam;
        }
        else if(atkDam >= vicHP && isBase)
        {
            board[vicx, vicy].GetComponent<Base>().BaseHP = 0;
            GameOverCommand game = new GameOverCommand(player);
        }
	}
}
