using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ManaVisual : MonoBehaviour {

    public int TestFullCrystals;
    public int TestTotalCrystalsThisTurn;

    //public Image[] Crystals;
    public Text ProgressText;

    private int totalCrystals;
	private int availableCrystals;

    public int TotalCrystals
    {
        get { return totalCrystals; }

        set
        {
            //Debug.Log("Changed total mana to: " + value);

            if (value > totalCrystals)
                totalCrystals = 10; //mana cap is 10
            else if (value < 0)
                totalCrystals = 0;
            else
                totalCrystals = value;

            // update the text
            ProgressText.text = string.Format("{0}/{1}", availableCrystals.ToString(), totalCrystals.ToString());
        }
    }

    
    public int AvailableCrystals
    {
        get { return availableCrystals; }

        set
        {
            //Debug.Log("Changed mana this turn to: " + value);

            if (value > totalCrystals)
                availableCrystals = totalCrystals;
            else if (value < 0)
                availableCrystals = 0;
            else
                availableCrystals = value;

            // update the text
            ProgressText.text = string.Format("{0}/{1}", availableCrystals.ToString(), totalCrystals.ToString());

        }
    }

    void Update()
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            TotalCrystals = TestTotalCrystalsThisTurn;
            AvailableCrystals = TestFullCrystals;
        }
    }

}
