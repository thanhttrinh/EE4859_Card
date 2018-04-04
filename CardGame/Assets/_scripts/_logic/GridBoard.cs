using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBoard : MonoBehaviour {

	public List<SoldierLogic> SoldiersOnGrid = new List<SoldierLogic>();

	public void PlaceCreatureAt(int index, SoldierLogic soldier)
	{
		SoldiersOnGrid.Insert(index, soldier);
	}
}
