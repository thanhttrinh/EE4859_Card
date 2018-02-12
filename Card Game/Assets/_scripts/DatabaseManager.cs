using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class DatabaseManager : MonoBehaviour {
	//variables on the cards
	string name;
	int mana;
	int atk_dmg;
	int hp;
	int movement;
	int range;
	int cropSize;
	string description;

	//database connections and command variables
	string conn;
	IDataReader reader;
	IDbConnection dbconn;
	IDbCommand dbcmd;

	private GameObject dbManager;
	CardManager cm;

	void Start () 
	{
		dbManager = GameObject.FindGameObjectWithTag ("database");
		cm = dbManager.GetComponent<DatabaseManager> ();

		dbStart ();
	}

	//start up the database
	public void dbStart(){
		//path to database
		//DO NOT CHANGE
		conn = "URI=file:" + Application.dataPath + "/_plugins/cards.s3db"; 

		dbconn = (IDbConnection)new SqliteConnection (conn);
		//open connection to the database
		dbconn.Open ();
		dbcmd = dbconn.CreateCommand ();

		//give an SQL command
		dbcmd.CommandText = getSoldier("imp");

		reader = dbcmd.ExecuteReader ();
		readSoldiers ();

		//close all connections to the database
		reader.Close ();
		reader = null;
		dbcmd.Dispose ();
		dbcmd = null;
		dbconn.Close ();
		dbconn = null;
	}


	public String getSoldier(string name)
	{
		string sqlQry = "SELECT * FROM soldiers WHERE name = '" + name +"'";
		return sqlQry;
	}

	public String getCrop(string name)
	{
		string sqlQry = "SELECT * FROM crops WHERE name = '" + name +"'";
		return sqlQry;
	}

	public String getSpell(string name)
	{
		string sqlQry = "SELECT * FROM spells WHERE name = '" + name +"'";
		return sqlQry;
	}

	//print out info on the soldier selected from getSoldier()
	public void readSoldiers()
	{
		reader.Read ();

		name = reader.GetString (0);
		mana = reader.GetInt32 (1);
		atk_dmg = reader.GetInt32 (2);
		hp = reader.GetInt32 (3);
		movement = reader.GetInt32 (4);
		range = reader.GetInt32 (5);
		description = reader.GetString (6);

		Debug.Log ("name = " + name + " mana = " + mana + " damage = "+ atk_dmg +
			" hp = " + hp + " movement = "+ movement + " range = "+ range +" description = " + description);
	}

	//print out info on the crop from getCrop()
	public void readCrops()
	{
		reader.Read ();

		name = reader.GetString (0);
		mana = reader.GetInt32 (1);
		cropSize = reader.GetInt32 (2);
		hp = reader.GetInt32 (3);
		description = reader.GetString (4);

		Debug.Log ("name = " + name + " mana = " + mana + " size = " + cropSize + 
			" hp = " + hp + " description = " + description);
	}

	//print out info on the spell from getSpell()
	public void readSpells()
	{
		reader.Read ();

		name = reader.GetString (0);
		mana = reader.GetInt32 (1);
		range = reader.GetInt32 (2);
		description = reader.GetString (3);

		Debug.Log ("name = " + name + " mana = " + mana +" range = "+ range +" description = " + description);
	}
}
