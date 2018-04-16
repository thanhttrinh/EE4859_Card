using UnityEngine;
using System;

public class attack : MonoBehaviour
{
	public bool ViableAttack(GameUnits[,] board, int atkx, int atky, int vicx, int vicy)
	{
		if(!board[atkx, atky].ValidMove(board, atkx, atky, vicx, vicy))
		{
			if (board[atkx, atky].GetComponent<OneCardManager>().AttackText.text != "0")
			{
				return true;
			}
		}
		return false;
	}
	//Do ViableAttack and have it == true first
	public void doAttack(Player player, GameUnits[,] board, int atkx, int atky, int vicx, int vicy)
	{
		int atkDam = 0, vicDam = 0, atkHP = 0, vicHP = 0;
        bool isBase = false;
		Int32.TryParse(board[atkx, atky].GetComponent<OneCardManager>().AttackText.text, out atkDam);
        Int32.TryParse(board[atkx, atky].GetComponent<OneCardManager>().HealthText.text, out atkHP);

        if (board[vicx, vicy].GetComponent<OneCardManager>() != null)
        {
            Int32.TryParse(board[vicx, vicy].GetComponent<OneCardManager>().AttackText.text, out vicDam);
            Int32.TryParse(board[vicx, vicy].GetComponent<OneCardManager>().HealthText.text, out vicHP);
        }
        else
        {
            vicHP = board[vicx, vicy].GetComponent<Base>().BaseHP;
            isBase = true;
        }

		if (atkDam >= vicHP && !isBase)
		{
			board[vicx, vicy] = null;
		}
		else if (atkDam < vicHP && vicDam >= atkHP && !isBase)
		{
			board[vicx, vicy].GetComponent<OneCardManager>().HealthText.text = (vicHP - atkDam) + "";
			board[atkx, atky] = null;
		}
		else if (atkDam < vicHP && vicDam < atkHP && !isBase)
		{
			board[vicx, vicy].GetComponent<OneCardManager>().HealthText.text = "" + (vicHP - atkDam);
			board[vicx, vicy].GetComponent<OneCardManager>().HealthText.text = "" + (atkHP - vicDam);
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
