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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
            Debug.Log("something there");
        else
            Debug.Log("nothing there");
        transform.DOMove(savedPos, 0.1f);
    }

    public override void OnDraggingInUpdate()
    {

    }

    protected override bool DragSuccessful()
    {
        return true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "soldier")
            Debug.Log("It's another soldier");
    }

}
