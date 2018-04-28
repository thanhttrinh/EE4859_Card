using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseVisual : MonoBehaviour {

    public Text ProgressBlueText;
	public Text ProgressRedText;
    public PlayerAsset pAsset;
    //public int availableHealth = Base.Instance.BaseHP;

    private int totalHP;
    public int TotalHP
    {
        get { return totalHP; }
        set
        {
            if (value > 30)
                totalHP = 30;
            else if (value < 0)
                totalHP = 0;
            else
                totalHP = 30;

            //update the text
			//if(Base.Instance.isBaseBlue)
            	ProgressBlueText.text = string.Format("{0}/{1}", availableBlueHP.ToString(), totalHP.ToString());
			//if(Base.Instance.isBaseRed)
				ProgressRedText.text = string.Format("{0}/{1}", availableRedHP.ToString(), totalHP.ToString());
			
        }
    }

    private int availableBlueHP;
    public int AvailableBlueHP
    {
        get { return availableBlueHP; }
        set
        {
            if (value > totalHP)
				availableBlueHP = totalHP;
            else if (value < 0)
				availableBlueHP = 0;
            else
				availableBlueHP = value;

            ProgressBlueText.text = string.Format("{0}/{1}", availableBlueHP.ToString(), totalHP.ToString());
        }
    }

	private int availableRedHP;
	public int AvailableRedHP
	{
		get { return availableRedHP; }
		set
		{
			if (value > totalHP)
				availableRedHP = totalHP;
			else if (value < 0)
				availableRedHP = 0;
			else
				availableRedHP = value;

			ProgressRedText.text = string.Format("{0}/{1}", availableRedHP.ToString(), totalHP.ToString());
		}
	}

    private void Update()
    {
        if(Base.Instance != null)
        {
            TotalHP = pAsset.MaxHealth;
			//if(Base.Instance.isBaseBlue)
            	AvailableBlueHP = Base.Instance.BaseBlueHP;
			//if(Base.Instance.isBaseRed)
				AvailableRedHP = Base.Instance.BaseRedHP;
        }
        
    }

}
