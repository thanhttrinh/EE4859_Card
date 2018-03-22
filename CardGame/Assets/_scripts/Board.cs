using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour 
{
	public GameObject Soldier;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider)
	{
		//Debug.Log ("collision detected");
		if (collider.gameObject.tag == "card" && collider.gameObject.GetComponent<DraggingActionsReturn>().dragging == false) 
		{
			GenerateSoldier (collider);
			
		}
	}

	private void GenerateSoldier(Collider collider)
	{
		GameObject newGO = Instantiate (Soldier) as GameObject;
		newGO.transform.position = new Vector3 (0,0,0);
		newGO.gameObject.GetComponent<OneSoldierManager> ().cardAsset = collider.gameObject.GetComponent<OneCardManager> ().cardAsset;
		//newGO.transform.SetParent (this.gameObject.transform, false);
		Destroy (collider.gameObject);
		Debug.Log (collider.gameObject.GetComponent<OneCardManager>().cardAsset.name);
	}

}
