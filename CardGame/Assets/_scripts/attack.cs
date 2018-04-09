using UnityEngine;
using System;

public class attack : MonoBehaviour
{
    public Board board = new Board();
    public GameUnits[,] manager;

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
    public void doAttack(GameUnits[,] board, int atkx, int atky, int vicx, int vicy)
    {
        int atkDam = 0, vicDam = 0, atkHP = 0, vicHP = 0;
        Int32.TryParse(board[atkx, atky].GetComponent<OneCardManager>().AttackText.text, out atkDam);
        Int32.TryParse(board[vicx, vicy].GetComponent<OneCardManager>().AttackText.text, out vicDam);
        Int32.TryParse(board[atkx, atky].GetComponent<OneCardManager>().HealthText.text, out atkHP);
        Int32.TryParse(board[vicx, vicy].GetComponent<OneCardManager>().HealthText.text, out vicHP);

        if (atkDam >= vicHP)
        {
            board[vicx, vicy] = null;
        }
        else if (atkDam < vicHP && vicDam >= atkHP)
        {
            board[vicx, vicy].GetComponent<OneCardManager>().HealthText.text = (vicHP - atkDam) + "";
            board[atkx, atky] = null;
        }
        else if (atkDam < vicHP && vicDam < atkHP)
        {
            board[vicx, vicy].GetComponent<OneCardManager>().HealthText.text = "" + (vicHP - atkDam);
            board[vicx, vicy].GetComponent<OneCardManager>().HealthText.text = "" + (atkHP - vicDam);
        }
    }

    public attack(Board bd)
    {
        board = bd;
        manager = board.cards;
    }
}
