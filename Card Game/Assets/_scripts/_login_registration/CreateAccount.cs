using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAccount : MonoBehaviour {

	[HideInInspector]
	public static string email = "";
	public static string pwd = "";
	public static string username = "";
	public static string clear = "";

	public string currentMenu = "login";

	private string registerURL = "http://tinycivs.000webhostapp.com/CreateAccountT.php";
	private string loginURL = "http://tinycivs.000webhostapp.com/LoginAccountT.php";
	private string confirmPwd = "";
	private string createPwd = "";
	private string createEmail = "";
	private string createUser = "";

	private float screenW = Screen.width;
	private float screenH = Screen.height;

	void Start () {
		
	}

	#region Custom funcitons
	//GUI function
	void OnGUI(){
		if (currentMenu == "login") {
			//if current menu is login, calls the login function
			loginGUI ();
		}else if(currentMenu == "register"){
			//if current menu is registration, calls the register function
			regGUI ();
		}
	}

	void loginGUI(){
		//create register and login buttons
		//open register window
		if (GUI.Button (new Rect ((screenW / 2) - 120, (screenH / 2), 110, 30), "REGISTER")) {
			//clear input fields
			email = "";
			pwd = "";
			//switch to register menu
			currentMenu = "register";
		}
		//log user in
		if (GUI.Button (new Rect ((screenW / 2) , (screenH / 2), 110, 30), "LOGIN")) {
			StartCoroutine (LoginAcc ());
		}

		GUI.Label (new Rect ((screenW / 3.5f), (screenH / 3) + 5, 110, 30), "Email");
		email = GUI.TextField (new Rect ((screenW / 2) - 120, (screenH / 3), 230, 30), email, 30);

		GUI.Label (new Rect ((screenW / 3.5f), (screenH / 2.5f) + 5, 110, 30), "Password");
		pwd = GUI.PasswordField (new Rect ((screenW / 2) - 120, (screenH / 2.5f), 230, 30), pwd, "*"[0], 15);
	}

	void regGUI(){
		if (GUI.Button (new Rect ((screenW / 2) - 120, (screenH / 2), 110, 30), "BACK")) {
			//clear input fields
			createEmail = "";
			createUser = "";
			createPwd = "";
			confirmPwd = "";
			//switch to login menu
			currentMenu = "login";
		}
		if (GUI.Button (new Rect ((screenW / 2) , (screenH / 2), 110, 30), "REGISTER")) {
			//register the user then go back to the login screen
			if (confirmPwd == createPwd) {
				StartCoroutine (registerAcc());
			} else {
			//	StartCoroutine ();
			}
		}

		GUI.Label (new Rect ((screenW / 4.5f), (screenH / 5.0f) + 4, 110, 30), "Email");
		createEmail = GUI.TextField (new Rect ((screenW / 2) - 120, (screenH / 5f) - 2, 230, 30), createEmail, 30);

		GUI.Label (new Rect ((screenW / 4.5f), (screenH / 4.0f) + 10, 110, 30), "Username");
		createUser = GUI.TextField (new Rect ((screenW / 2) - 120, (screenH / 4.0f) + 8, 230, 30), createUser, 15);

		GUI.Label (new Rect ((screenW / 4.5f), (screenH / 3.0f) + 5, 110, 30), "Password");
		createPwd = GUI.PasswordField (new Rect ((screenW / 2) - 120, (screenH / 3.0f), 230, 30), createPwd, "*"[0], 15);

		GUI.Label (new Rect ((screenW / 4.5f), (screenH / 2.5f) + 5, 110, 30), "Confirm Password");
		confirmPwd = GUI.PasswordField (new Rect ((screenW / 2) - 120, (screenH / 2.5f), 230, 30), confirmPwd, "*"[0], 15);
	}
	#endregion

	#region Coroutine
	//actually create the account
	IEnumerator registerAcc()
	{
		//this is what sends info to the php script
		WWWForm form = new WWWForm ();
		//these are variables that are being sent
		form.AddField ("email", createEmail);
		form.AddField ("username", createUser);
		form.AddField ("password", createPwd);

		WWW registerWWW = new WWW (registerURL, form);
		//wait for the php script to send info back
		yield return registerWWW;

		if (registerWWW.error != null) {
			Debug.LogError ("Cannot connect to Database");
		} 
		else {
			string registerReturn = registerWWW.text;
			Debug.Log (registerReturn);
			if (registerReturn == "success") 
			{
				Debug.Log ("Success: Registered");
				currentMenu = "login";
			} else if (registerReturn == "exist") 
			{
				Debug.Log ("User exists");
			} else {
				Debug.Log ("unsuccessful");
			}
		}

	}

	//actually logging in
	IEnumerator LoginAcc(){
		Debug.Log ("Attempting to log in");

		//add values into the php script
		WWWForm form = new WWWForm ();
		form.AddField ("email", email);
		form.AddField ("password", pwd);
		//connect to url and put it the form
		WWW loginAccWWW = new WWW (loginURL, form);
		//make the info is returning before continuing 
		yield return loginAccWWW;
		if (loginAccWWW.error != null) 
		{
			Debug.Log ("Cannot connect to Login");
		} 
		else 
		{
			string logText = loginAccWWW.text;
			Debug.Log (logText);
			if (logText == "success") 
			{
				Debug.Log ("4");
				Application.LoadLevel ("Title");
			}
		}
		Debug.Log ("5");
	}
	#endregion
}
