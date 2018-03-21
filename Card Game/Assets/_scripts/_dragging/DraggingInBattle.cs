using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DraggingInBattle : MonoBehaviour 
{
    // script used to drag soldier to move/attack

	private bool canAttack;
	private bool canMove;
	private int move;
    private Vector3 newPos;
	private Vector3 boardOffset = new Vector3(-2,-1,0);
    private Vector2 mouseOver;
	private OneSoldierManager selectedSoldier;
	private OneSoldierManager[,] soldiers = new OneSoldierManager[6,6]


    void Start()
    {
        //canMove = true;
        //canAttack = true;
        newPos = transform.position;
    }

    void Update()
    {
		UpdateMouseOver();

		int x = (int)mouseOver.x;
		int y = (int)mouseOver.y;

        if (Input.GetMouseButtonDown(0))
        {
			SelectSoldier (x, y);
        }
        
        Debug.Log(mouseOver);
    }

    private void UpdateMouseOver()
    {
        RaycastHit hit;
		if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
			mouseOver.x = (int)(hit.collider.transform.position.x - boardOffset.x);
			mouseOver.y = (int)(hit.collider.transform.position.y - boardOffset.y);
        }
    }

	private void SelectSoldier(int x, int y)
	{
		//out of bounds
		if (x < 0 || x > 6 || y < 0 || y > 6)
			return;

		OneSoldierManager soldier = soldiers[x,y];
		if(soldiers != null) 
		{
			selectedSoldier = soldiers;
		}
	}


}
