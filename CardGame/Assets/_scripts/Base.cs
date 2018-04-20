using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour {

    public PlayerAsset pAssetBlue;
    public PlayerAsset pAssetRed;
    public int BaseHP;
    public bool isBaseBlue;
    public bool isBaseRed;

	// Use this for initialization
	void Start ()
    {
        if(isBaseBlue)
            BaseHP = pAssetBlue.AvailableHealth;
        if (isBaseRed)
            BaseHP = pAssetRed.AvailableHealth;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("a"))
        {
            if (isBaseBlue)
            {
                BaseHP--;
                Debug.Log("BaseBlue HP: "+ BaseHP);
            }
        }
    }
}
