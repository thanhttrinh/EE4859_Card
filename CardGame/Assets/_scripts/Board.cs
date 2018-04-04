using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour 
{
	public OneSoldierManager[,] soldiers = new OneSoldierManager[6,6];
	public OneCropManager[,] crops = new OneCropManager[6,6];

    public OneCardManager[,] cards = new OneCardManager[6, 6];

	private Vector2 Tiles;
	public GameObject BlueTile;
	public GameObject BlueTile2;
	public GameObject RedTile;
	public GameObject RedTile2;
	public Vector2 BoardOffset = new Vector2(0.5f, 0.5f);

    public GameObject Base;
	public GameObject Soldier;
	public GameObject Crop;
	private bool isCreated;

	private Vector2 mouseOver;
	private Vector2 startDrag;
	private Vector2 endDrag;

	private OneCardManager selectedSoldierCard;

    private bool isBlueTurn;
    private bool isBlue;

	// Use this for initialization
	void Start ()
    {
        isBlueTurn = true;
        GenerateBase(Random.Range(0, 5), 0);
    }
	
	// Update is called once per frame
	void Update () 
	{
		UpdateMouseOver ();
		//Debug.Log (mouseOver);

		int x = (int) (mouseOver.x);
		int y = (int) (mouseOver.y);

        /*
        if(selectedSoldier != null)
        {
            UpdateSoldierDrag(selectedSoldier);
        }
        */
		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log (x + ", " + y);
			SelectSoldier (x, y);
		}
		if (Input.GetMouseButtonUp (0)) 
		{
			TryMove ((int)startDrag.x, (int)startDrag.y, x, y);
		}
	}

	void UpdateMouseOver() 
	{
		if (!Camera.main)
		{
			Debug.Log ("cannot find main camera");
			return;
		}

		RaycastHit hit;
		if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
		{
			mouseOver.x = (int)(hit.collider.transform.position.x);
			mouseOver.y = (int)(hit.collider.transform.position.y);
		}
	}

    /*
    private void UpdateSoldierDrag(OneSoldierManager s)
    {
        if (!Camera.main)
        {
            Debug.Log("cannot find main camera");
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            s.transform.position = hit.point + Vector3.forward;
        }
    }
    */

    private void GenerateBase(int x, int y)
    {
        GameObject newGO = Instantiate(Base) as GameObject;
        newGO.transform.position = new Vector3(x, y, 0);
		OneCardManager b = newGO.gameObject.GetComponent<OneCardManager> ();
		cards [x, y] = b;
    }

	void OnTriggerEnter(Collider collider)
	{
		//Debug.Log ("collision detected");
		if (Base.gameObject.GetComponent<OneCardManager> () != null) 
		{
			if (collider.gameObject.tag == "card" && collider.gameObject.GetComponent<OneCardManager> ().cardAsset.TypeOfCard == TypesOfCards.Soldier && collider.gameObject.GetComponent<DraggingActionsReturn> ().dragging == false) {
				GenerateSoldier (collider, (int)Base.gameObject.transform.position.x, (int)Base.gameObject.transform.position.y);
			}

			if (collider.gameObject.tag == "card" && collider.gameObject.GetComponent<OneCardManager> ().cardAsset.TypeOfCard == TypesOfCards.Crop && collider.gameObject.GetComponent<DraggingActionsReturn> ().dragging == false) {
				GenerateCrop (collider, 1, 1);
			}
		}
	}

	void OnTriggerExit(Collider collider)
	{
		isCreated = false;
	}
		

	private void GenerateSoldier(Collider collider, int x, int y)
	{
		if (!isCreated) {
			GameObject newGO = Instantiate (Soldier) as GameObject;
			newGO.transform.position = new Vector3 (x, y, 0);
			newGO.gameObject.GetComponent<OneSoldierManager> ().cardAsset = collider.gameObject.GetComponent<OneCardManager> ().cardAsset;
            newGO.gameObject.GetComponent<OneCardManager>().cardAsset = collider.gameObject.GetComponent<OneCardManager>().cardAsset;
            newGO.gameObject.GetComponent<SoldierPreview> ().PreviewUnit = collider.gameObject.GetComponent<SoldierPreview> ().PreviewUnit;
			newGO.gameObject.GetComponent<SoldierPreview> ().PreviewText = collider.gameObject.GetComponent<SoldierPreview> ().PreviewText;
			//newGO.transform.SetParent (this.gameObject.transform, false);
			//Debug.Log (collider.gameObject.GetComponent<OneCardManager> ().cardAsset.name);
			OneCardManager s = newGO.GetComponent<OneCardManager>();
			cards[x, y] = s;
			isCreated = true;
			Destroy (collider.gameObject);
		}
		//if(soldiers[x,y] != null)
			//Debug.Log (soldiers[x,y].cardAsset.name);
	}

	private void GenerateCrop(Collider collider, int x, int y)
	{
		if (!isCreated) {
			GameObject newGO = Instantiate (Crop) as GameObject;
			newGO.transform.position = new Vector3 (x, y, 0);
			newGO.gameObject.GetComponent<OneCropManager> ().cardAsset = collider.gameObject.GetComponent<OneCardManager> ().cardAsset;
            newGO.gameObject.GetComponent<OneCardManager>().cardAsset = collider.gameObject.GetComponent<OneCardManager>().cardAsset;
            newGO.gameObject.GetComponent<CropPreview> ().PreviewUnit = collider.gameObject.GetComponent<CropPreview> ().PreviewUnit;
			newGO.gameObject.GetComponent<CropPreview> ().PreviewText = collider.gameObject.GetComponent<CropPreview> ().PreviewText;
			//newGO.transform.SetParent (this.gameObject.transform, false);
			//Debug.Log (collider.gameObject.GetComponent<OneCardManager> ().cardAsset.name);
			OneCardManager c = newGO.GetComponent<OneCardManager>();
			cards[x, y] = c;
			isCreated = true;
            Destroy(collider.gameObject);
        }
	}

	private void SelectSoldier(int x, int y)
	{
		//out of bounds
		if (x < 0 || x > 6 || y < 0 || y > 6)
			return;

		OneCardManager s = cards[x,y];
		if (s != null && s.gameObject.GetComponent<OneCardManager>().cardAsset.TypeOfCard == TypesOfCards.Soldier)
		{
			selectedSoldierCard = s;
			startDrag = mouseOver;
			Debug.Log(selectedSoldierCard.gameObject.GetComponent<OneSoldierManager>().cardAsset.name);
		}
		else
			Debug.Log("nothing there");
		
	}

	private void TryMove(int x1, int y1, int x2, int y2)
	{
		startDrag = new Vector2 (x1, y1);
		endDrag = new Vector2 (x2, y2);
		selectedSoldierCard = cards [x1, y1];

		//MoveSoldier (selectedSoldier, x2, y2);

		if (x2 < 0 || x2 >= soldiers.Length || y2 < 0 || y2 >= soldiers.Length) 
		{
			if (selectedSoldierCard != null) 
			{
				MoveSoldier (selectedSoldierCard, x1, y1);
			}
			startDrag = Vector2.zero;
			selectedSoldierCard = null;
			return;
		}

        if(selectedSoldierCard != null)
        {
            //soldier not moved
            if(endDrag == startDrag)
            {
                MoveSoldier(selectedSoldierCard, x1, y1);
                startDrag = Vector2.zero;
                selectedSoldierCard = null;
                return;
            }

            //check if valid move
            if(selectedSoldierCard.ValidMove(cards, x1, y1, x2, y2))
            {
                cards[x2, y2] = selectedSoldierCard;
                cards[x1, y1] = null;
                MoveSoldier(selectedSoldierCard, x2, y2);
            }
        }


	}

	private void MoveSoldier(OneCardManager soldierCard, int x, int y)
	{
        if(soldierCard.gameObject.GetComponent<OneCardManager>().cardAsset.TypeOfCard == TypesOfCards.Soldier)
		    soldierCard.transform.position = (Vector2.right * x) + (Vector2.up * y);
	}

    private void EndTurn()
    {
        selectedSoldierCard = null;
        startDrag = Vector2.zero;

        isBlueTurn = !isBlueTurn;
    }

}
