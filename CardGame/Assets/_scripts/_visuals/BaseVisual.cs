using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseVisual : MonoBehaviour {

    public Text ProgressText;
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
            ProgressText.text = string.Format("{0}/{1}", availableHP.ToString(), totalHP.ToString());
        }
    }

    private int availableHP;
    public int AvailableHP
    {
        get { return availableHP; }
        set
        {
            if (value > totalHP)
                availableHP = totalHP;
            else if (value < 0)
                availableHP = 0;
            else
                availableHP = value;

            ProgressText.text = string.Format("{0}/{1}", availableHP.ToString(), totalHP.ToString());
        }
    }

    private void Update()
    {
        if(Base.Instance != null)
        {
            TotalHP = pAsset.MaxHealth;
            AvailableHP = Base.Instance.BaseHP;
        }
        
    }

}
