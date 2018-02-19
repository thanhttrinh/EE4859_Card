using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

public class LancerScript : MonoBehaviour {

	private DatabaseManager db;

	string name;
	int mana;
	int atk_dmg;
	int hp;
	int movement;
	int range;
	string description;

	//components
	private GameObject dbObject;
	private GameObject cmObject;
	private CardManager cm;

	void Start () {
		dbObject = GameObject.FindGameObjectWithTag ("database");
		cmObject = GameObject.FindGameObjectWithTag ("card");

		cm = cmObject.GetComponent<CardManager> ();

		db = dbObject.GetComponent<DatabaseManager> ();
		//db.getHP(cm.cardName);

		this.name = cm.cardName;
		this.hp = db.getHP;
		Debug.Log (hp);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
}
