using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnQuitButton : MonoBehaviour {

	public void OnApplicationQuit(){
		Debug.Log ("aha quiting");
	}

	public void OnQuit(){
		Application.Quit ();
	}
}
