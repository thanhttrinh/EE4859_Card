﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour {

    public static Base Instance;

    public PlayerAsset pAssetBlue;
    public PlayerAsset pAssetRed;
    public int BaseBlueHP;
	public int BaseRedHP;
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
        //if(isBaseBlue)
        //{
            BaseBlueHP = pAssetBlue.AvailableHealth;
        //}
            
        //if (isBaseRed)
        //{
            BaseRedHP = pAssetRed.AvailableHealth;
        //}
            
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetKeyDown("a"))
        {

                BaseBlueHP--;
                //pAssetBlue.AvailableHealth--;
                Debug.Log("BaseBlue HP: "+ BaseBlueHP);
            
        }

        if (Input.GetKeyDown("q"))
        {
                BaseRedHP--;
                //pAssetBlue.AvailableHealth--;
                Debug.Log("BaseRed HP: " + BaseRedHP);
            
        }

		foreach (Base b in Board.Instance.baseList) {
			if (GameUnits.Instance.attacking == false && isBaseBlue) {
				b.BaseBlueHP = BaseBlueHP;
			}
			if (GameUnits.Instance.attacking == false && isBaseRed) {
				
				b.BaseRedHP = BaseRedHP;
			}
		}
	
    }
}
