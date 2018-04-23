using UnityEngine;
using System;

public class attack : MonoBehaviour
{
    public GameObject source;

    private void Start()
    {
        source = GameObject.Find("SoundEffects");
    }

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
        OneSoldierManager atkSoldier = board[atkx, atky].GetComponent<OneSoldierManager>(), vicSoldier = board[vicx, vicy].GetComponent<OneSoldierManager>();
        HoldIndividualData atkHold = board[atkx, atky].GetComponent<HoldIndividualData>(), vicHold = board[vicx, vicy].GetComponent<HoldIndividualData>();
        Base vicBase = board[vicx, vicy].GetComponent<Base>(); OneCropManager vicCrop = board[vicx, vicy].GetComponent<OneCropManager>();
        if (vicSoldier != null)
        {
            if (atkHold == null)
            {
                atkDam = atkSoldier.cardAsset.Attack;
                atkHP = atkSoldier.cardAsset.MaxHealth;
                atkSoldier.gameObject.AddComponent<HoldIndividualData>();
                atkHold = atkSoldier.GetComponent<HoldIndividualData>();
                atkHold.makeData(atkHP, atkDam);
            }
            else
            {
                atkDam = atkHold.Dam;
                atkHP = atkHold.HP;
            }
            if (vicHold == null)
            {
                vicDam = vicSoldier.cardAsset.Attack;
                vicHP = vicSoldier.cardAsset.MaxHealth;
                vicSoldier.gameObject.AddComponent<HoldIndividualData>();
                vicHold = vicSoldier.GetComponent<HoldIndividualData>();
                vicHold.makeData(vicHP, vicDam);
            }
            else
            {
                vicDam = vicHold.Dam;
                vicHP = vicHold.HP;
            }

            if ((atkSoldier.isBlue && vicSoldier.isRed) || (atkSoldier.isRed && vicSoldier.isBlue))
            {
                source.GetComponent<AudioSource>().Play();
                Debug.Log("RED|BLUE ENEMY");
                if (atkDam >= vicHP)
                {
                    DestroyObject(vicSoldier.gameObject);
                    board[vicx, vicy] = null;
                }
                else if (atkDam < vicHP && vicDam >= atkHP)
                {
                    vicHold.makeData(vicHP - atkDam, vicDam);
                    DestroyObject(atkSoldier.gameObject);
                    board[atkx, atky] = null;
                }
                else if (atkDam < vicHP && vicDam < atkHP)
                {
                    vicHold.makeData(vicHP - atkDam, vicDam);
                    atkHold.makeData(atkHP - vicDam, atkDam);
                }
            }
        }
        else if (vicBase != null)
        {
            if ((vicBase.isBaseRed && vicSoldier.isRed) || (vicBase.isBaseBlue && vicSoldier.isBlue))
            {

            }
            else
            {
                if (vicHold == null)
                {
                    vicHP = vicBase.BaseHP;
                    vicDam = 0;
                    vicBase.gameObject.AddComponent<HoldIndividualData>();
                    vicHold = vicBase.GetComponent<HoldIndividualData>();
                    vicHold.makeData(vicHP, vicDam);
                }
                else
                {
                    vicHP = vicHold.HP;
                    vicDam = 0;
                }
                if (atkHold == null)
                {
                    atkDam = vicSoldier.cardAsset.Attack;
                    atkHP = vicSoldier.cardAsset.MaxHealth;
                    atkSoldier.gameObject.AddComponent<HoldIndividualData>();
                    atkHold = atkSoldier.GetComponent<HoldIndividualData>();
                    atkHold.makeData(atkHP, atkDam);
                }
                vicHP -= atkDam;
                vicHold.makeData(vicHP, vicDam);
                vicBase.BaseHP = vicHP;
                if (vicHP <= 0)
                {
                    GameOverCommand gameOver = new GameOverCommand(player.otherPlayer);
                }
            }
        }
        else if (vicCrop != null)
        {
            if (vicHold == null)
            {
                vicHP = vicCrop.cardAsset.CropHealth;
                vicDam = vicCrop.cardAsset.Attack;
                vicCrop.gameObject.AddComponent<HoldIndividualData>();
                vicHold = vicCrop.GetComponent<HoldIndividualData>();
                vicHold.makeData(vicHP, vicDam);
            }
            else
            {
                vicHP = vicHold.HP;
                vicDam = vicHold.Dam;
            }
            if (atkHold != null)
            {
                atkDam = atkHold.Dam;
            }
            else
            {
                atkDam = atkSoldier.cardAsset.Attack;
                atkHP = atkSoldier.cardAsset.MaxHealth;
                atkSoldier.gameObject.AddComponent<HoldIndividualData>();
                atkHold = atkSoldier.GetComponent<HoldIndividualData>();
                atkHold.makeData(atkHP, atkDam);
            }

            vicHP -= atkDam;
            atkHP -= vicDam;
            if (vicHP <= 0)
            {
                Destroy(board[vicx, vicy].gameObject);
                board[vicx, vicy] = null;
            }
            else
            {
                vicHold.makeData(vicHP, vicDam);
            }
            if (atkHP <= 0)
            {
                Destroy(board[atkx, atky].gameObject);
                board[atkx, atky] = null;
            }
            else
            {
                atkHold.makeData(atkHP, atkDam);
            }
        }
        
        

	}
}
