using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UserDatabase : MonoBehaviour {

	[Header("Database Settings")]
	public string serverName = "localhost";
	public string serverUsername = "root";
	public string serverPassword = "";
	public string serverDatabase = "users";
	public string serverKeycode = "defaultkey123";

	[Header("Database SQL Links")]
	public string url_checkValue = "_scripts/_login_registration/UserCheckValue";
	public string url_setValue = "_scripts/_login_registration/UserSetValue";


	private WWWForm StablishedServer()
	{
		WWWForm webForm = new WWWForm ();
		webForm.AddField ("serverName", serverName);
		webForm.AddField ("serverUsername", serverUsername);
		webForm.AddField ("serverPassword", serverPassword);
		webForm.AddField ("serverDatabase", serverDatabase);
		webForm.AddField ("serverKeycode", serverKeycode);
		return webForm;
	}

	//upload user inputs to the database
	//this is a registration method
	public void User_Registration()
	{
		StartCoroutine (Registration ());
	}

	[Header("Registration Field")]
	public InputField set_username = null;
	public InputField set_password = null;
	public InputField set_email = null;
	public Text set_result = null;

	//need to wait for the data to arrive first 
	private IEnumerator Registration()
	{
		set_result.text = "Acessing Database";

		WWWForm setForm = new WWWForm ();
		setForm.AddField ("set_username", set_username.text);
		setForm.AddField ("set_password", set_password.text);
		setForm.AddField ("set_email", set_email.text);

		WWW database = new WWW (url_setValue, setForm);
		yield return database;

		set_result.text = database.text;
		Debug.Log (database.text);
	}

	//check if username and password already exist in the database
	public void User_Authenticate()
	{
		StartCoroutine (Authentication ());
	}

	[Header("Authentication Field")]
	public InputField check_username = null;
	public InputField check_password = null;
	public Text check_result = null;

	private IEnumerator Authentication()
	{
		if (!check_username.text.Equals (string.Empty) && !check_password.text.Equals (string.Empty)) {
			check_result.text = "Checking Database";

			WWWForm checkForm = StablishedServer ();
			checkForm.AddField ("check_username", check_username.text);
			checkForm.AddField ("check_password", check_password.text);

			WWW database = new WWW (url_checkValue, checkForm);
			yield return database;

			check_result.text = database.text;
			Debug.Log (database.text);
		} else {
			check_result.text = "Insufficient Inpts";
		}
	}
}
