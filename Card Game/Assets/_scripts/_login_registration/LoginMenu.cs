﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginMenu : MonoBehaviour {

	public GameObject loginObject;
	public GameObject registerObject;

	[HideInInspector]
	//login input field
	public UnityEngine.UI.InputField input_login_email;
	public UnityEngine.UI.InputField input_login_password;
	public UnityEngine.UI.InputField input_register_username;
	public UnityEngine.UI.InputField input_register_email;
	public UnityEngine.UI.InputField input_register_password;

	//the part of the UI currently being shown
	//0 = login, 1 = register, 2 = login

	int part = 0;

	bool isDBsetup = true;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (isDBsetup == true) {
			if (part == 0) {
				loginObject.gameObject.SetActive (true);
				registerObject.gameObject.SetActive (false);
			}
			if (part == 1) {
				loginObject.gameObject.SetActive (false);
				registerObject.gameObject.SetActive (true);
			}
			if(part == 2){
			}
		}
	}

	public void login_reg_button(){
		part = 1;
	}

	public void reg_back_button(){
		part = 0;
	}

}
