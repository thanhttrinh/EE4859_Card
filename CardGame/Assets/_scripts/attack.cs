using UnityEngine;
using System;

public class attack : MonoBehaviour
{
    private class HoldIndividualData : MonoBehaviour
    {
        public int HP = 0;
        public int Dam = 0;
        public bool Base = false;

        public void makeData(int health, int damage)
        {
            HP = health;
            Dam = damage;
            Base = false;
        }

        public void makeData(int health, int damage, bool bse)
        {
            HP = health;
            Dam = damage;
            Base = bse;
        }
    }

    public bool ViableAttack(GameUnits[,] board, int atkx, int atky, int vicx, int vicy)
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
        return false;
    }
	//Do ViableAttack and have it == true first
	public void doAttack(Player player, GameUnits[,] board, int atkx, int atky, int vicx, int vicy)
	{
		int atkDam = 0, vicDam = 0, atkHP = 0, vicHP = 0;
        bool isBase = false;
        OneSoldierManager atkSoldier = board[atkx, atky].GetComponent<OneSoldierManager>(), vicSoldier = board[vicx, vicy].GetComponent<OneSoldierManager>();
        HoldIndividualData atkHold = board[atkx, atky].GetComponent<HoldIndividualData>(), vicHold = board[vicx, vicy].GetComponent<HoldIndividualData>();
        if (!ViableAttack(board, atkx, atky, vicx, vicy))
        {

            if (atkHold == null)
            {
                Int32.TryParse(atkSoldier.AttackText.text, out atkDam);
                Int32.TryParse(atkSoldier.HealthText.text, out atkHP);
                if(atkHP != 0)
                {
                    board[atkx, atky].gameObject.AddComponent<HoldIndividualData>();
                    atkHold = board[atkx, atky].GetComponent<HoldIndividualData>();
                    atkHold.makeData(atkHP, atkDam);
                }
            }
            else
            {
                atkDam = atkHold.Dam;
                atkHP = atkHold.HP;
            }
            if (vicHold == null)
            {
                if (vicSoldier != null)
                {
                    Int32.TryParse(vicSoldier.AttackText.text, out vicDam);
                    Int32.TryParse(vicSoldier.HealthText.text, out vicHP);
                    if (vicHP != 0)
                    {
                        board[vicx, vicy].gameObject.AddComponent<HoldIndividualData>();
                        vicHold = board[vicx, vicy].GetComponent<HoldIndividualData>();
                        //if (vicDam == 0)
                        //{
                        //    vicHold.Dam = 0;
                        //}
                        vicHold.makeData(vicHP, vicDam);
                    }
                }
                else if (board[vicx, vicy].GetComponent<Base>() != null)
                {
                    vicDam = 0;
                    vicHP = board[vicx, vicy].GetComponent<Base>().BaseHP;
                    isBase = true;
                    board[vicx, vicy].gameObject.AddComponent<HoldIndividualData>();
                    vicHold = board[vicx, vicy].GetComponent<HoldIndividualData>();
                    vicHold.makeData(vicHP, vicDam, isBase);
                }
            }
            else
            {
                vicDam = vicHold.Dam;
                vicHP = vicHold.HP;
                isBase = vicHold.Base;
            }
            if (atkDam == 0 && atkHP == 0 && vicDam == 0 && vicHP == 0)
            {
                atkDam = atkSoldier.cardAsset.Attack;
                atkHP = atkSoldier.cardAsset.MaxHealth;

                if (vicSoldier != null)
                {
                    vicDam = vicSoldier.cardAsset.Attack;
                    vicHP = vicSoldier.cardAsset.MaxHealth;
                    isBase = false;
                }
                else
                {
                    vicDam = 0;
                    vicHP = board[vicx, vicy].GetComponent<Base>().BaseHP;
                    isBase = true;
                }
                board[atkx, atky].gameObject.AddComponent<HoldIndividualData>();
                atkHold = board[atkx, atky].GetComponent<HoldIndividualData>();
                atkHold.makeData(atkHP, atkDam);
                board[vicx, vicy].gameObject.AddComponent<HoldIndividualData>();
                vicHold = board[vicx, vicy].GetComponent<HoldIndividualData>();
                vicHold.makeData(vicHP, vicDam, isBase);
            }
        }
        if (true)
        {
            Debug.Log("RED ENEMY");
            if (atkDam >= vicHP && !isBase)
            {
                Debug.Log("atkDam >= vicHP && not a base");
                DestroyObject(board[vicx, vicy].gameObject);
                board[vicx, vicy] = null;
            }
            else if (atkDam < vicHP && vicDam >= atkHP && !isBase)
            {
                Debug.Log("atkDam < vicHP && not a base");
                vicHold.HP = (vicHP - atkDam);
                DestroyObject(board[atkx, atky].gameObject);
                board[atkx, atky] = null;
            }
            else if (atkDam < vicHP && vicDam < atkHP && !isBase)
            {
                Debug.Log("atkDam < vicHP && vicDam < atkHP and not a base");
                vicHold.HP = (vicHP - atkDam);
                atkHold.HP = (atkHP - vicDam);
            }
            else if (atkDam < vicHP && isBase)
            {
                Debug.Log("atkDam < vicHP and is a base");
                vicHold.HP = vicHP - atkDam;
            }
            else if (atkDam >= vicHP && isBase)
            {
                Debug.Log("atkDam >= vicHP and is a base");
                vicHold.HP = 0;
                GameOverCommand game = new GameOverCommand(player);
            }
        }
        else
        {
            Debug.Log("BLUE ENEMY");
            if (atkDam >= vicHP && !isBase)
            {
                Debug.Log("atkDam >= vicHP && not a base");
                DestroyObject(board[vicx, vicy].gameObject);
                board[vicx, vicy] = null;
            }
            else if (atkDam < vicHP && vicDam >= atkHP && !isBase)
            {
                Debug.Log("atkDam < vicHP && not a base");
                vicHold.HP = (vicHP - atkDam);
                DestroyObject(board[atkx, atky].gameObject);
                board[atkx, atky] = null;
            }
            else if (atkDam < vicHP && vicDam < atkHP && !isBase)
            {
                Debug.Log("atkDam < vicHP && vicDam < atkHP and not a base");
                vicHold.HP = (vicHP - atkDam);
                atkHold.HP = (atkHP - vicDam);
            }
            else if (atkDam < vicHP && isBase)
            {
                Debug.Log("atkDam < vicHP and is a base");
                vicHold.HP = vicHP - atkDam;
            }
            else if (atkDam >= vicHP && isBase)
            {
                Debug.Log("atkDam >= vicHP and is a base");
                vicHold.HP = 0;
                GameOverCommand game = new GameOverCommand(player);
            }
        }
	}
}
