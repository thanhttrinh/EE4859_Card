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
    //private RaycastHit hit;
    private Vector3 newPos;


    void Start()
    {
        canMove = true;
        canAttack = true;
        newPos = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                newPos = hit.collider.transform.position;
                transform.position = newPos;
            }
        }

    }

    public void OnMouseDown()
    {
      


        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        /*
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag != "tile" && hit.collider.gameObject.GetComponent<OneSoldierManager>().cardAsset.TypeOfCard == TypesOfCards.Soldier)
        {
            int.TryParse(this.gameObject.GetComponent<OneSoldierManager>().MovementText.text, out move);
            Debug.Log("its a soldier with " + move + " movement");
        }

        if (Physics.Raycast(ray, out hit2) && hit2.collider.gameObject.tag == "tile")
        {
            Debug.Log(hit2.collider.transform.position);
                
        }
        
        if(canMove == true && hit.collider.gameObject.GetComponent<OneSoldierManager>().cardAsset.TypeOfCard == TypesOfCards.Soldier)
        {
            if(hit2.collider.transform.position.x >= hit.collider.transform.position.x + move && hit2.collider.transform.position.x <= hit.collider.transform.position.x - move)
            {
                hit.collider.transform.position = new Vector3(4,0,0);
            }
        }
        */

    }

    public void OnMouseUp()
    {
        
    }


}
