using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

public class LancerScript : MonoBehaviour {

	private GameObject soldier;
	private DatabaseManager db;

	IDataReader reader;

	string name;
	int mana;
	int atk_dmg;
	int hp;
	int movement;
	int range;
	string description;

	// Use this for initialization
	void Start () {
		db = soldier.GetComponent<DatabaseManager> ();
		reader.Read ();

		name = reader.GetString (0);
		mana = reader.GetInt32 (1);
		atk_dmg = reader.GetInt32 (2);
		hp = reader.GetInt32 (3);
		movement = reader.GetInt32 (4);
		range = reader.GetInt32 (5);
		description = reader.GetString (6);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void getHP() {
		Debug.Log ("HP = "+ hp);
	}
}
