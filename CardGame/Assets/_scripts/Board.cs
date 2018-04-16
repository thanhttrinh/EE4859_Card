using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour 
{
    public GameUnits[,] cards = new GameUnits[6, 6];

	private GameUnits selectedSoldierCard;

	private Vector2 mouseOver;
	private Vector2 startDrag;
	private Vector2 endDrag;
	public Vector2 BoardOffset = new Vector2(0.5f, 0.5f);

	public Player playerBlue;
	public Player playerRed;

    public GameObject Base;
	public GameObject Soldier;
	public GameObject Crop;

	private bool isCreated;
	private bool baseBlueCreated;
	private bool baseRedCreated;
	public bool basesCreated;
	private GameObject cropCard;
	private int cropsBlue = 0;
	private int cropsRed = 0;

	private int baseBlueX;
	private int baseBlueY;
	private int baseRedX;
	private int baseRedY;

	public static Board Instance;

	void Awake(){
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//PlayerInput ();

        /*
        if(selectedSoldier != null)
        {
            UpdateSoldierDrag(selectedSoldier);
        }
        */
		if (playerBlue != null && TurnManager.Instance.whoseTurn == playerBlue) {
			PlayerInput ();
			//Debug.Log ("player Blue");
		} 
		if (playerRed != null && TurnManager.Instance.whoseTurn == playerRed) {
			//Debug.Log ("player Red");
			PlayerInput ();
		}

	}



	private void PlayerInput()
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

		int x = (int) (mouseOver.x);
		int y = (int) (mouseOver.y);

		if (Input.GetMouseButtonDown(0))
		{
			//Debug.Log (x + ", " + y);

			if (TurnManager.Instance.whoseTurn == playerBlue) {
				if(!baseBlueCreated)
					GenerateBaseBlue (x, y);
				SelectSoldierBlue (x, y);

			}
			if (TurnManager.Instance.whoseTurn == playerRed) {
				if(!baseRedCreated)
					GenerateBaseRed (x, y);
				SelectSoldierRed (x, y);

			}
			//SelectSoldier (x, y);

			GenerateCropBlue (cropCard, x, y);
			GenerateCropRed (cropCard, x, y);

			//Debug.Log ("base generated + num of base : " + NumOfBase.ToString());

		}
		if (Input.GetMouseButtonUp (0)) 
		{
			TryMove ((int)startDrag.x, (int)startDrag.y, x, y);
		}
		if(baseRedCreated && baseBlueCreated)
			basesCreated = true;
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


	void OnTriggerEnter(Collider collider)
	{
		//Debug.Log ("collision detected");

		if (collider.gameObject.tag == "card" && collider.gameObject.GetComponent<OneCardManager> ().cardAsset.TypeOfCard == TypesOfCards.Soldier && collider.gameObject.GetComponent<DraggingActionsReturn> ().dragging == false) {
			if(TurnManager.Instance.whoseTurn == playerBlue)
				GenerateSoldierBlue (collider, baseBlueX, baseBlueY);
			if(TurnManager.Instance.whoseTurn == playerRed)
				GenerateSoldierRed (collider, baseRedX, baseRedY);
		}

		if (collider.gameObject.tag == "card" && collider.gameObject.GetComponent<OneCardManager> ().cardAsset.TypeOfCard == TypesOfCards.Crop && collider.gameObject.GetComponent<DraggingActionsReturn> ().dragging == false) {
				//GenerateCropBlue (collider, 1, 1);
			if (TurnManager.Instance.whoseTurn == playerBlue)
				cropsBlue = collider.gameObject.GetComponent<OneCardManager> ().cardAsset.CropSize;
			if(TurnManager.Instance.whoseTurn == playerRed)
				cropsRed = collider.gameObject.GetComponent<OneCardManager> ().cardAsset.CropSize;
			cropCard = collider.gameObject;
			collider.gameObject.SetActive (false);
		}
		
	}

	void OnTriggerExit(Collider collider)
	{
		isCreated = false;
	}

	private void GenerateBaseBlue(int x, int y)
	{
		if (x < 0 || x > 6 || y < 0 || y > 2)
			return; 
		GameObject newGO = Instantiate(Base) as GameObject;
		newGO.transform.position = new Vector3(x, y, 0);
		GameUnits b = newGO.gameObject.GetComponent<GameUnits> ();
		baseBlueX = x;
		baseBlueY = y;
		cards [x, y] = b;
		baseBlueCreated = true;
	}

	private void GenerateBaseRed(int x, int y)
	{
		if (x < 0 || x > 6 || y < 3 || y > 6)
			return; 
		GameObject newGO = Instantiate(Base) as GameObject;
		newGO.transform.position = new Vector3(x, y, 0);
		GameUnits b = newGO.gameObject.GetComponent<GameUnits> ();
		baseRedX = x;
		baseRedY = y;
		cards [x, y] = b;
		baseRedCreated = true;
	}

	private void GenerateSoldierBlue(Collider collider, int x, int y)
	{
		if (!isCreated) {
			GameObject newGO = Instantiate (Soldier) as GameObject;
			newGO.transform.position = new Vector3 (x, y, 0);
			newGO.gameObject.GetComponent<OneSoldierManager> ().cardAsset = collider.gameObject.GetComponent<OneCardManager> ().cardAsset;
			newGO.gameObject.GetComponent<OneSoldierManager> ().ReadSoldierFromAsset ();
			newGO.gameObject.GetComponent<OneSoldierManager> ().isBlue = true;

            newGO.gameObject.GetComponent<SoldierPreview> ().PreviewUnit = collider.gameObject.GetComponent<SoldierPreview> ().PreviewUnit;
			newGO.gameObject.GetComponent<SoldierPreview> ().PreviewText = collider.gameObject.GetComponent<SoldierPreview> ().PreviewText;
            //newGO.transform.SetParent (this.gameObject.transform, false);
            //Debug.Log (collider.gameObject.GetComponent<OneCardManager> ().cardAsset.name);
            GameUnits s = newGO.GetComponent<GameUnits>();
			cards[x, y] = s;
			isCreated = true;
			Destroy (collider.gameObject);
		}
		//if(soldiers[x,y] != null)
			//Debug.Log (soldiers[x,y].cardAsset.name);
	}

	private void GenerateSoldierRed(Collider collider, int x, int y)
	{
		if (!isCreated) {
			GameObject newGO = Instantiate (Soldier) as GameObject;
			newGO.transform.position = new Vector3 (x, y, 0);
			newGO.gameObject.GetComponent<OneSoldierManager> ().cardAsset = collider.gameObject.GetComponent<OneCardManager> ().cardAsset;
			newGO.gameObject.GetComponent<OneSoldierManager> ().ReadSoldierFromAsset ();
			newGO.gameObject.GetComponent<OneSoldierManager> ().isRed = true;

			newGO.gameObject.GetComponent<SoldierPreview> ().PreviewUnit = collider.gameObject.GetComponent<SoldierPreview> ().PreviewUnit;
			newGO.gameObject.GetComponent<SoldierPreview> ().PreviewText = collider.gameObject.GetComponent<SoldierPreview> ().PreviewText;
			//newGO.transform.SetParent (this.gameObject.transform, false);
			//Debug.Log (collider.gameObject.GetComponent<OneCardManager> ().cardAsset.name);
			GameUnits s = newGO.GetComponent<GameUnits>();
			cards[x, y] = s;
			isCreated = true;
			Destroy (collider.gameObject);
		}
		//if(soldiers[x,y] != null)
		//Debug.Log (soldiers[x,y].cardAsset.name);
	}

	private void GenerateCropBlue(GameObject go, int x, int y)
	{
		if (cropsBlue != 0) {
			if (x < 0 || x > 6 || y < 0 || y > 2)
				return; 
			GameObject newGO = Instantiate (Crop) as GameObject;
			newGO.transform.position = new Vector3 (x, y, 0);
			newGO.gameObject.GetComponent<OneCropManager> ().cardAsset = go.gameObject.GetComponent<OneCardManager> ().cardAsset;
			newGO.gameObject.GetComponent<OneCropManager> ().ReadCropFromAsset ();

            newGO.gameObject.GetComponent<CropPreview> ().PreviewUnit = go.gameObject.GetComponent<CropPreview> ().PreviewUnit;
			newGO.gameObject.GetComponent<CropPreview> ().PreviewText = go.gameObject.GetComponent<CropPreview> ().PreviewText;
            //newGO.transform.SetParent (this.gameObject.transform, false);
            //Debug.Log (collider.gameObject.GetComponent<OneCardManager> ().cardAsset.name);
            GameUnits c = newGO.GetComponent<GameUnits>();
			cards[x, y] = c;
			//isCreated = true;
			//Destroy(go.gameObject);
			cropsBlue--;
			Debug.Log (cropsBlue);
        }
	}

	private void GenerateCropRed(GameObject go, int x, int y)
	{
		if (cropsRed != 0) {
			if (x < 0 || x > 6 || y < 3 || y > 6)
				return;
			GameObject newGO = Instantiate (Crop) as GameObject;
			newGO.transform.position = new Vector3 (x, y, 0);
			newGO.gameObject.GetComponent<OneCropManager> ().cardAsset = go.gameObject.GetComponent<OneCardManager> ().cardAsset;
			newGO.gameObject.GetComponent<CropPreview> ().PreviewUnit = go.gameObject.GetComponent<CropPreview> ().PreviewUnit;
			newGO.gameObject.GetComponent<CropPreview> ().PreviewText = go.gameObject.GetComponent<CropPreview> ().PreviewText;
			//newGO.transform.SetParent (this.gameObject.transform, false);
			//Debug.Log (collider.gameObject.GetComponent<OneCardManager> ().cardAsset.name);
			GameUnits c = newGO.GetComponent<GameUnits>();
			cards[x, y] = c;
			//isCreated = true;
			//Destroy(go.gameObject);
			cropsRed--;
			Debug.Log (cropsRed);
		}
	}

	private void SelectSoldierBlue(int x, int y)
	{
		//out of bounds
		if (x < 0 || x > 6 || y < 0 || y > 6)
			return;

        GameUnits s = cards[x,y];
		if (s != null && s.gameObject.GetComponent<OneSoldierManager>().isBlue == true && s.gameObject.GetComponent<OneSoldierManager>().cardAsset.TypeOfCard == TypesOfCards.Soldier)
		{
			selectedSoldierCard = s;
			startDrag = mouseOver;
			Debug.Log(selectedSoldierCard.gameObject.GetComponent<OneSoldierManager>().cardAsset.name);
		}
		else
			Debug.Log("nothing there");
		
	}

	private void SelectSoldierRed(int x, int y)
	{
		//out of bounds
		if (x < 0 || x > 6 || y < 0 || y > 6)
			return;

		GameUnits s = cards[x,y];
		if (s != null && s.gameObject.GetComponent<OneSoldierManager>().isRed == true && s.gameObject.GetComponent<OneSoldierManager>().cardAsset.TypeOfCard == TypesOfCards.Soldier)
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

		//if out of bounds
		if (x2 < 0 || x2 >= cards.Length || y2 < 0 || y2 >= cards.Length) 
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

	private void MoveSoldier(GameUnits soldierUnit, int x, int y)
	{
        if(soldierUnit.gameObject.GetComponent<OneSoldierManager>().cardAsset.TypeOfCard == TypesOfCards.Soldier)
            soldierUnit.transform.position = (Vector2.right * x) + (Vector2.up * y);
	}

}
