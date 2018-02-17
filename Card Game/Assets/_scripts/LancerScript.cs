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

	// Use this for initialization
	void Start () {
		db = GetComponent<DatabaseManager> ();
		db.getHP();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
}
