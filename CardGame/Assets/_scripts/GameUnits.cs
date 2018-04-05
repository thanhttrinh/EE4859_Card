using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUnits : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool ValidMove(GameUnits[,] board, int x1, int y1, int x2, int y2)
    {
        int deltaMoveX = Mathf.Abs(x1 - x2);
        int deltaMoveY = Mathf.Abs(y1 - y2);

        Debug.Log("deltaMoveX = " + deltaMoveX + ", deltaMoveY = " + deltaMoveY);

        //if moving on top of another soldier/crop
        if (board[x2, y2] != null)
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
        if ((deltaMoveX == 0 && deltaMoveY <= board[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement) || (deltaMoveX <= board[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement && deltaMoveY == 0))
        {
            Debug.Log("true & soldier has " + board[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement + "movement and deltaMoveX =" + deltaMoveX + "and deltaMoveY = " + deltaMoveY);
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
        */
        //}
        Debug.Log("false");
        return false;
    }
}
