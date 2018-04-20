using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour {

    public static Base Instance;

    public PlayerAsset pAssetBlue;
    public PlayerAsset pAssetRed;
    public int BaseHP;
    public bool isBaseBlue;
    public bool isBaseRed;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        //Debug.Log(BaseHP);
        if(isBaseBlue)
        {
            BaseHP = pAssetBlue.AvailableHealth;
        }
            
        if (isBaseRed)
        {
            BaseHP = pAssetRed.AvailableHealth;
        }
            
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("a"))
        {
            if (isBaseBlue)
            {
                BaseHP--;
                //pAssetBlue.AvailableHealth--;
                Debug.Log("BaseBlue HP: "+ BaseHP);
            }
        }
    }
}
