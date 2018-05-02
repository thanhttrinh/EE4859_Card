using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseVisual : MonoBehaviour {
    public static BaseVisual Instance { set; get; }
    public Text ProgressBlueText;
	public Text ProgressRedText;
    public PlayerAsset pAsset;
    public Client client;
    public string msg;
    bool isBlueplayer;
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

    void Awake()
    {
        client = FindObjectOfType<Client>();
        isBlueplayer = client.isHost;
        Instance = this;
    }

    private void Update()
    {
        if(Base.Instance != null)
        {
            if (isBlueplayer == true)
            {
                AvailableBlueHP = Base.Instance.BaseBlueHP;
                //if(Base.Instance.isBaseRed)
                AvailableRedHP = Base.Instance.BaseRedHP;
            }
            //Networking action
            msg = "CABH|";
            msg += AvailableBlueHP.ToString() + "|";
            msg += AvailableRedHP.ToString();
            Debug.Log(msg);
            client.Send(msg);
            //End networking action
        }

    }
    public void AvailableBaseHp(int bluehp, int redhp)
    {
        AvailableBlueHP = bluehp;
        //if(Base.Instance.isBaseRed)
        AvailableRedHP = redhp;
        
    }
}

