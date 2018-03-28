using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour {

	public void ReloadScene(){
		//reset all card IDs
		IDFactory.ResetIDs ();
		IDHolder.ClearIDHoldersList ();
		Command.OnSceneReload ();
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void LoadScene(string SceneName){
		SceneManager.LoadScene (SceneName);
	}

	public void Quit(){
		Application.Quit ();
	}
}
