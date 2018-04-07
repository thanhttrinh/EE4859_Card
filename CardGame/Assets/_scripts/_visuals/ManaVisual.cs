using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ManaVisual : MonoBehaviour {
    public Text ProgressText;

    private int totalCrystals;
    public int TotalCrystals
    {
        get { return totalCrystals; }
        set
        {
            if (value > 10)
                totalCrystals = 10;
            else if (value < 0)
                totalCrystals = 0;
            else
                totalCrystals = 10;

            //update the text
            ProgressText.text = string.Format("{0}/{1}", availableCrystals.ToString(), totalCrystals.ToString());
        }
    }

	private int availableCrystals;
	public int AvailableCrystals{
		get { return availableCrystals; }
		set{
			if (value > totalCrystals)
				availableCrystals = totalCrystals; 
			else if (value < 0)
				availableCrystals = 0;
			else
				availableCrystals = value;

			ProgressText.text = string.Format ("{0}/{1}", availableCrystals.ToString (), totalCrystals.ToString ());
		}
	}

}
