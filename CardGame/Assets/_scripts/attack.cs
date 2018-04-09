using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    public Board board = new Board();
    public GameUnits[,] manager;

    public bool ViableAttack(GameUnits[,] board, int atkx, int atky, int vicx, int vicy)
    {
        if(board[atkx, atky] != null && board[vicx, vicy] != null)
        {
        }
        return false;
    }
    //Do ViableAttack and have it == true first
    public void doAttack(GameUnits[,] board, int atkx, int atky, int vicx, int vicy)
    {
        int atkDam = 0, vicDam = 0, atkHP = 0, vicHP = 0;
        if (atkDam >= vicHP)
        {
            board[vicx, vicy] = null;
        }
        else if (atkDam < vicHP && vicDam >= atkHP)
        {
            vicHP -= atkDam;
            board[atkx, atky] = null;
        }
        else if (atkDam < vicHP && vicDam < atkHP)
        {
            vicHP -= atkDam;
            atkHP -= vicDam;
        }
    }

    public attack(Board bd)
    {
        board = bd;
        manager = board.cards;
    }
}
