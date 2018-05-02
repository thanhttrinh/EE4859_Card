using UnityEngine;
using System;
using System.Collections;

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
        OneSoldierManager atkSoldier = board[atkx, atky].GetComponent<OneSoldierManager>(), vicSoldier;
        try
        {
            vicSoldier = board[vicx, vicy].GetComponent<OneSoldierManager>();
        }
        catch(NullReferenceException nl)
        {
            vicSoldier = null;
            Debug.Log(nl.Message);
        }
        HoldIndividualData atkHold = board[atkx, atky].GetComponent<HoldIndividualData>(), vicHold; Base vicBase; OneCropManager vicCrop;
        try
        {
            vicHold = board[vicx, vicy].GetComponent<HoldIndividualData>();
        }
        catch(NullReferenceException nl)
        {
            vicHold = null;
            Debug.Log(nl.Message);
        }
        try
        {
            vicBase = board[vicx, vicy].GetComponent<Base>(); 
        }
        catch(NullReferenceException nl)
        {
            vicBase = null;
            Debug.Log(nl.Message);
        }
        try
        {
            vicCrop = board[vicx, vicy].GetComponent<OneCropManager>();
        }
        catch(NullReferenceException nl)
        {
            vicCrop = null;
            Debug.Log(nl.Message);
        }


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
            if ((vicBase.isBaseRed && atkSoldier.isRed) || (vicBase.isBaseBlue && atkSoldier.isBlue))
            {

            }
            else
            {
                if (vicHold == null)
                {
                    if(vicBase.isBaseBlue)
                    {
                        vicHP = vicBase.BaseBlueHP;
                    }
                    else
                    {
                        vicHP = vicBase.BaseRedHP;
                    }
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
                    atkDam = atkSoldier.cardAsset.Attack;
                    atkHP = atkSoldier.cardAsset.MaxHealth;
                    atkSoldier.gameObject.AddComponent<HoldIndividualData>();
                    atkHold = atkSoldier.GetComponent<HoldIndividualData>();
                    atkHold.makeData(atkHP, atkDam);
                }
                else
                {
                    atkDam = atkHold.Dam;
                }

                vicHP -= atkDam;
                vicHold.makeData(vicHP, vicDam);
                if(vicBase.isBaseBlue)
                {
                    vicBase.BaseBlueHP = vicHP;
                }
                else
                {
                    vicBase.BaseRedHP = vicHP;
                }
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
                //put death code here
                StartCoroutine(Death());
                Destroy(board[atkx, atky].gameObject);
                board[atkx, atky] = null;
            }
            else
            {
                atkHold.makeData(atkHP, atkDam);
            }
        }
        
        

	}

    public IEnumerator Death(){
        Board.Instance.deathMark.SetActive(true);
        yield return new WaitForSeconds(0.5f);
    }
}
