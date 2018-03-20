using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DraggingInBattle : DraggingActions 
{
    // script used to drag soldier to move/attack

    private Vector3 savedPos;
    private Vector3 draggingPos;
	private bool canAttack;
	private bool canMove;
	private int move;
    private RaycastHit hit;
    private RaycastHit hit2;


    private void Start()
    {
        canMove = true;
        canAttack = true;
    }

    public override void OnStartDrag()
    {
        savedPos = transform.position;
        draggingPos = savedPos + new Vector3(0, 0, 1);
        int.TryParse(this.gameObject.GetComponent<OneSoldierManager>().MovementText.text, out move);
    }

    public override void OnEndDrag()
    {
        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        //transform.DOMove(savedPos, 0.1f);
    }

    public void OnMouseDown()
    {
        //Ray ray = new Ray(draggingPos, Vector3.back);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.gameObject.name);
        }
        else
        {
            Debug.Log("nothing there");
        }
    }

    public void OnMouseUp()
    {

    }

    public override void OnDraggingInUpdate()
    {

    }

    protected override bool DragSuccessful()
    {
        return true;
    }

}
