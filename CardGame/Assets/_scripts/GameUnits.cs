using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUnits : MonoBehaviour {

    public int moving = 1;

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

        //if deltaMoveX and deltaMoveY is less than or equal to the soldiers movement
        if ((deltaMoveX == 0 && deltaMoveY <= board[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement) || (deltaMoveX <= board[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement && deltaMoveY == 0))
        {
            Debug.Log("true & soldier has " + board[x1, y1].gameObject.GetComponent<OneSoldierManager>().cardAsset.Movement + "movement and deltaMoveX =" + deltaMoveX + "and deltaMoveY = " + deltaMoveY);
            Debug.Log(moving);
            return true;

        }

        Debug.Log("false");
        return false;
    }

}
