using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MessageManager : MonoBehaviour {

	public Text MessageText;
	public Text MessageText2;
	public GameObject MessagePanel;

	public static MessageManager Instance;

	void Awake(){
		Instance = this;
		MessagePanel.SetActive(false);
	}

	public void ShowMessage(string message, float duration){
		StartCoroutine (ShowMessageCoroutine (message, duration));
	}

	IEnumerator ShowMessageCoroutine(string message, float duration){
		MessageText.text = message;
		MessagePanel.SetActive (true);
		if(Board.Instance.basesCreated == true)
			MessageText2.text = "";
		yield return new WaitForSeconds (duration);
		MessagePanel.SetActive (false);
	}
}
