using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class GridVisual : MonoBehaviour 
{
	public AreaPosition owner;

	public SameDistanceChildren slots;

	private List<GameObject> SoldiersOnGrid = new List<GameObject>();

	private bool cursorOverThisTable = false;

	private BoxCollider col;

	public static bool CursorOverSomeTable
	{
		get
		{
			GridVisual[] bothTables = GameObject.FindObjectsOfType<GridVisual>();
			return (bothTables[0].CursorOverThisTable || bothTables[1].CursorOverThisTable);
		}
	}


	public bool CursorOverThisTable
	{
		get{ return cursorOverThisTable; }
	}
		
	void Awake()
	{
		col = GetComponent<BoxCollider>();
	}
		
	void Update()
	{
		// we need to Raycast because OnMouseEnter, etc reacts to colliders on cards and cards "cover" the table
		// create an array of RaycastHits
		RaycastHit[] hits;
		// raycst to mousePosition and store all the hits in the array
		hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), 30f);

		bool passedThroughTableCollider = false;
		foreach (RaycastHit h in hits)
		{
			// check if the collider that we hit is the collider on this GameObject
			if (h.collider == col)
				passedThroughTableCollider = true;
		}
		cursorOverThisTable = passedThroughTableCollider;
	}

	public void AddSoldierAtIndex(CardAsset ca, int UniqueID ,int index)
	{
		GameObject Soldier = GameObject.Instantiate(GlobalSettings.Instance.SoldierPrefab, slots.Children[index].transform.position, Quaternion.identity) as GameObject;

		// apply the look from CardAsset
		OneSoldierManager manager = Soldier.GetComponent<OneSoldierManager>();
		manager.cardAsset = ca;
		manager.ReadSoldierFromAsset();

		// add tag according to owner
		foreach (Transform t in Soldier.GetComponentsInChildren<Transform>())
			t.tag = owner.ToString()+"Soldier";

		// parent a new soldier gameObject to table slots
		Soldier.transform.SetParent(slots.transform);

		SoldiersOnGrid.Insert(index, Soldier);

		// let this Soldier know about its position
		WhereIsTheCardOrSoldier w = Soldier.GetComponent<WhereIsTheCardOrSoldier>();
		w.Slot = index;
		if (owner == AreaPosition.blue)
			w.VisualState = VisualStates.BlueGrid;
		else
			w.VisualState = VisualStates.RedGrid;

		// add our unique ID to this soldier
		IDHolder id = Soldier.AddComponent<IDHolder>();
		id.UniqueID = UniqueID;

		// after a new soldier is added update placing of all the other soldier
		PlaceSoldiersOnNewSlots();

		// end command execution
		Command.CommandExecutionComplete();
	}


	// returns an index for a new soldier based on mousePosition
	// included for placing a new soldier to any positon on the table
	public int TablePosForNewSoldier(float MouseX)
	{
		// if there are no soldier or if we are pointing to the right of all soldier with a mouse.
		// right - because the table slots are flipped and 0 is on the right side.
		if (SoldiersOnGrid.Count == 0 || MouseX > slots.Children[0].transform.position.x)
			return 0;
		else if (MouseX < slots.Children[SoldiersOnGrid.Count - 1].transform.position.x) // cursor on the left relative to all soldiers on the table
			return SoldiersOnGrid.Count;
		for (int i = 0; i < SoldiersOnGrid.Count; i++)
		{
			if (MouseX < slots.Children[i].transform.position.x && MouseX > slots.Children[i + 1].transform.position.x)
				return i + 1;
		}
		Debug.Log("Suspicious behavior. Reached end of TablePosForNewSoldier method. Returning 0");
		return 0;
	}

	// Destroy a Soldier
	public void RemoveSoldierWithID(int IDToRemove)
	{
		GameObject SoldierToRemove = IDHolder.GetGameObjectWithID(IDToRemove);
		SoldiersOnGrid.Remove(SoldierToRemove);
		Destroy(SoldierToRemove);

		PlaceSoldiersOnNewSlots();
		Command.CommandExecutionComplete();
	}

	void PlaceSoldiersOnNewSlots()
	{
		foreach (GameObject g in SoldiersOnGrid)
		{
			g.transform.DOLocalMoveX(slots.Children[SoldiersOnGrid.IndexOf(g)].transform.localPosition.x, 0.3f);
		}
	}

}
