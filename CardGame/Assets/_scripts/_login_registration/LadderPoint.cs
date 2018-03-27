using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LadderPoint : MonoBehaviour {

	[Header("User's Info")]
	public Text Username;
	public Text LadderPoints;
	private int lp;

	private string ladderURL = "http://tinycivs.000webhostapp.com/LadderPointManager.php";

	public static LadderPoint Instance;

	void Awake(){
		Instance = this;
	}

	void Start(){
		Debug.Log ("LadderPoints.cs");
		StartCoroutine (GetLadderPoints ());
		Debug.Log ("LP.cs ends");
	}

	#region Coroutine
	IEnumerator GetLadderPoints(){
		Debug.Log ("Attemping to retrieve LP");

		//add values into the php script
		WWWForm form = new WWWForm ();
		form.AddField ("email", LoginMenu.Instance.confirmLogin);
		Debug.Log (LoginMenu.Instance.confirmLogin);
		form.AddField ("Username", Username.text);
		form.AddField ("LadderPoints", LadderPoints.text);

		//connect to url and put it in the form
		WWW LadderPointsWWW = new WWW (ladderURL, form);

		//make the info sure the info is returning before continuing
		yield return LadderPointsWWW;

		//put the retrieving info form php into an array
		//the php will send info in the format
		//success, [Username], [LadderPoints]
		string[] UsernameAndLP = LadderPointsWWW.text.Split ("," [0]);
		string successResult = UsernameAndLP [0].ToString();
		Debug.Log (LadderPointsWWW.text);
		Debug.Log (successResult);


		if (LadderPointsWWW.error != null) {
			//display the debugged message
			Debug.Log (LadderPointsWWW.error);
			Debug.Log ("Cannot connect to LP database");
		} 
		else 
		{
			Debug.Log ("LP inside else statement");
			if (successResult == "success") {
				Debug.Log ("LP success");
				Username.text = UsernameAndLP[1].ToString();
				//LadderPoints.text = UsernameAndLP [2];
				Debug.Log(Username.text);

				lp = int.Parse (UsernameAndLP [2]);
				if (lp >= 0 && lp <= 150) {
					LadderPoints.text = "Rank: E " + UsernameAndLP[2].ToString() +"LP";
				}
			}
			if (successResult == "noExistEmail") {
				Debug.Log ("Email does not exist");
			}
			Debug.Log ("LP end of else statement");
		}

	}
	#endregion
}
