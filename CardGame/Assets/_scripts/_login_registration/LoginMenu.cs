using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginMenu : MonoBehaviour {

	[Header("Login Menu Inputs")]
	public InputField email;
	public InputField pwd;
	public string confirmLogin;

	[Header("Register Menu Inputs")]
	public InputField confirmPwd;
	public InputField createPwd;
	public InputField createEmail;
	public InputField createUser;

	[Header("Menu Contents")]
	public GameObject LoginContent;
	public GameObject RegisterContent;

	private string registerURL = "http://tinycivs.000webhostapp.com/CreateAccountT.php";
	private string loginURL = "http://tinycivs.000webhostapp.com/LoginAccountT.php";


	public static LoginMenu Instance;


	void Awake(){
		Instance = this;
		LoginGUI ();
	}

	#region UI Menus

	public void LoginGUI(){
		RegisterContent.SetActive (false);
		LoginContent.SetActive (true);

		//clear register input fields
		confirmPwd.text = "";
		createPwd.text = "";
		createEmail.text = "";
		createUser.text = "";

	}

	public void RegGUI(){
		LoginContent.SetActive (false);
		RegisterContent.SetActive (true);

		//clear login input fields
		email.text = "";
		pwd.text = "";

	}

	public void BackButtonHandler(){
		LoginGUI ();
	}

	public void RegisterButtonHandler(){
		StartCoroutine (registerAcc ());
	}

	public void LoginButtonHandler(){
		StartCoroutine (LoginAcc ());
	}

	#endregion

	#region Coroutine
	//actually create the account
	IEnumerator registerAcc()
	{
		//this is what sends info to the php script
		WWWForm form = new WWWForm ();
		//these are variables that are being sent
		form.AddField ("email", createEmail.text);
		form.AddField ("username", createUser.text);
		form.AddField ("password", createPwd.text);

		WWW registerWWW = new WWW (registerURL, form);
		//wait for the php script to send info back
		yield return registerWWW;

		if (registerWWW.error != null) {
			//display the debugged message
			Debug.Log(registerWWW.error);
			Debug.LogError ("Cannot connect to Database");
		} 
		else {
			string registerReturn = registerWWW.text;
			Debug.Log (registerReturn);
			if (registerReturn == "success") 
			{
				Debug.Log ("Success: Registered");
				LoginGUI ();
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
		form.AddField ("email", email.text);
		form.AddField ("password", pwd.text);
		//connect to url and put it the form
		WWW loginAccWWW = new WWW (loginURL, form);
		//make sure the info is returning before continuing 
		yield return loginAccWWW;
		if (loginAccWWW.error != null) 
		{
			//display the debugged message
			Debug.Log (loginAccWWW.error);
			Debug.Log ("Cannot connect to Login");
		} 
		else 
		{
			string logText = loginAccWWW.text;
			Debug.Log (logText);
			if (logText == "success") 
			{
				confirmLogin = email.text;
				SceneManager.LoadScene ("MenuScene");
			}
			if (logText == "noExistEmail") {
				Debug.Log ("Email does not exist");
			}
			if (logText == "noMatchPassword") {
				Debug.Log ("Password does not match");
			}
		}
	}
	#endregion
}