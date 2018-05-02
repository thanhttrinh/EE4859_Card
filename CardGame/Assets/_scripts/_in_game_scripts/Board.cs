using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Board : MonoBehaviour 
{
    public static Board Instance { set; get; }
    public GameUnits[,] cards = new GameUnits[6, 6];
    public List<GameUnits> soldierList = new List<GameUnits>();
	public List<Base> baseList = new List<Base> ();

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

	public GameObject soldierCard;

	private int baseBlueX;
	private int baseBlueY;
	private int baseRedX;
	private int baseRedY;
    private bool isBlueplayer;

    public string cardPlayed;

     public GameObject deathMark;


    public Client client;
    public string msg;

    void Awake(){
        client = FindObjectOfType<Client>();
        isBlueplayer=client.isHost;
        Instance = this;
       //deathMark.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () 
	{
		if (playerBlue != null && TurnManager.Instance.whoseTurn == playerBlue) {
			PlayerInput ();
		} 
		if (playerRed != null && TurnManager.Instance.whoseTurn == playerRed) {
			PlayerInput ();
		}

		if(Input.GetKeyDown("escape")){
			Application.Quit();
		}
    }



	public void PlayerInput()
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

            if (TurnManager.Instance.whoseTurn == playerBlue && isBlueplayer == true)
            {
                if (!baseBlueCreated) { 
                GenerateBasePlayer(x, y);
                    //Networking action
                    msg = "CGBC|";
                    msg += x.ToString() + "|";
                    msg += y.ToString();
                    client.Send(msg);
                    //End networking action
                }


                //if(selectedSoldierCard == null)
                SelectSoldierBlue(x, y);
                if (soldierCard != null)
                {
                    cardPlayed = soldierCard.gameObject.GetComponent<OneCardManager>().cardAsset.name.ToString();
                    Debug.Log(cardPlayed);

                    GenerateSoldier(soldierCard, cardPlayed, x, y);
                    if (isBlueplayer == true)
                    {
                        if (((x >= baseBlueX + 1 || x <= baseBlueX - 1) && (y != baseBlueY)) || ((y >= baseBlueY + 1 || y <= baseBlueY - 1) && (x != baseBlueX)))
                            Debug.Log("Invalid Move");
                        else
                        {
                            //Networking action
                            msg = "CGSB|";
                            msg += cardPlayed + "|";
                            msg += x.ToString() + "|";
                            msg += y.ToString();
                            Debug.Log(msg);
                            client.Send(msg);
                            //End networking action
                        }
                    }

                    //GenerateClientSoldierBlue(cardPlayed, baseBlueX, baseBlueY);
                    

                }

                GenerateCrop(cropCard, cardPlayed, x, y);


            }
            if (TurnManager.Instance.whoseTurn == playerBlue && isBlueplayer == false)
            {
                if (!baseBlueCreated)
                {
                    GenerateBasePlayer(x, y);
                    //Networking action
                    msg = "CGBC|";
                    msg += x.ToString() + "|";
                    msg += y.ToString();
                    client.Send(msg);
                    //End networking action
                }
                //if(selectedSoldierCard == null)

                SelectSoldierBlue(x, y);
                if (soldierCard != null)
                {
                    cardPlayed = soldierCard.gameObject.GetComponent<OneCardManager>().cardAsset.name.ToString();
                    //GenerateClientSoldierRed(cardPlayed, baseBlueX, baseBlueY);
                    Debug.Log(cardPlayed);
                    GenerateSoldier(soldierCard, cardPlayed, x, y);
                    if (isBlueplayer == false)
                    {
                        if (((x >= baseBlueX + 1 || x <= baseBlueX - 1) && (y != baseBlueY)) || ((y >= baseBlueY + 1 || y <= baseBlueY - 1) && (x != baseBlueX)))

                            Debug.Log("Invalid Move");
                        else
                        {
                            msg = "CGSR|";
                            msg += cardPlayed + "|";
                            msg += x.ToString() + "|";
                            msg += y.ToString();
                            Debug.Log(msg);
                            client.Send(msg);
                            //End networking action
                        }
                    }
                    //Networking action

                   

                }

                GenerateCrop(cropCard, cardPlayed, x, y);



            }
				
			

		}
		if (Input.GetMouseButtonUp (0)) 
		{
            if (selectedSoldierCard != null)
            {
                //Networking action
                msg = "CMOV|";
                msg += startDrag.x.ToString() + "|";
                msg += startDrag.y.ToString() + "|";
                msg += x.ToString() + "|";
                msg += y.ToString();
                client.Send(msg);
                //End networking action
                TryMove((int)startDrag.x, (int)startDrag.y, x, y);

            }
        }

		if(baseRedCreated && baseBlueCreated)
			basesCreated = true;
	}


	void OnTriggerEnter(Collider collider)
	{
		//Debug.Log ("collision detected");
        
		if ((collider.gameObject.tag == "blueCard" || collider.gameObject.tag == "redCard") && collider.gameObject.GetComponent<OneCardManager> ().cardAsset.TypeOfCard == TypesOfCards.Soldier && collider.gameObject.GetComponent<DraggingActionsReturn> ().dragging == false) {
            if (TurnManager.Instance.whoseTurn == playerBlue && collider.gameObject.tag == "blueCard" && playerBlue.ManaLeft >= collider.gameObject.GetComponent<OneCardManager>().cardAsset.ManaCost)
            {
                //GenerateSoldierBlue(collider, baseBlueX, baseBlueY);
				isCreated = false;
				playerBlue.ManaLeft = playerBlue.ManaLeft - collider.gameObject.GetComponent<OneCardManager>().cardAsset.ManaCost;
				soldierCard = collider.gameObject;
				collider.gameObject.SetActive(false);
                /*/Networking action
                msg = "CTEC|";
                client.Send(msg);
                //End networking action*/
            }
            if(TurnManager.Instance.whoseTurn == playerBlue && collider.gameObject.tag == "redCard"){
                return;
            }
            if (TurnManager.Instance.whoseTurn == playerRed && collider.gameObject.tag == "redCard" && playerRed.ManaLeft >= collider.gameObject.GetComponent<OneCardManager>().cardAsset.ManaCost)
            {
                //GenerateSoldierRed(collider, baseRedX, baseRedY);
				isCreated = false;
				playerRed.ManaLeft = playerRed.ManaLeft - collider.gameObject.GetComponent<OneCardManager>().cardAsset.ManaCost;
				soldierCard = collider.gameObject;
				collider.gameObject.SetActive(false);
                /*/Networking action
                msg = "CTEC|";
                client.Send(msg);
                //End networking action*/
            }
        }

		if ((collider.gameObject.tag == "blueCard" || collider.gameObject.tag == "redCard") && collider.gameObject.GetComponent<OneCardManager> ().cardAsset.TypeOfCard == TypesOfCards.Crop && collider.gameObject.GetComponent<DraggingActionsReturn> ().dragging == false) {
            //GenerateCropBlue (collider, 1, 1);
            if (TurnManager.Instance.whoseTurn == playerBlue && collider.gameObject.tag == "blueCard" && playerBlue.ManaLeft >= collider.gameObject.GetComponent<OneCardManager>().cardAsset.ManaCost)
            {
                cropsBlue = collider.gameObject.GetComponent<OneCardManager>().cardAsset.CropSize;
                playerBlue.ManaLeft = playerBlue.ManaLeft - collider.gameObject.GetComponent<OneCardManager>().cardAsset.ManaCost;
                cropCard = collider.gameObject;
                collider.gameObject.SetActive(false);
            }
            if (TurnManager.Instance.whoseTurn == playerRed && collider.gameObject.tag == "redCard" && playerRed.ManaLeft >= collider.gameObject.GetComponent<OneCardManager>().cardAsset.ManaCost)
            {
                cropsRed = collider.gameObject.GetComponent<OneCardManager>().cardAsset.CropSize;
                playerRed.ManaLeft = playerRed.ManaLeft - collider.gameObject.GetComponent<OneCardManager>().cardAsset.ManaCost;
                cropCard = collider.gameObject;
                collider.gameObject.SetActive(false);
            }
            /*if (playerBlue.ManaLeft >= collider.gameObject.GetComponent<OneCardManager>().cardAsset.ManaCost || playerRed.ManaLeft >= collider.gameObject.GetComponent<OneCardManager>().cardAsset.ManaCost)
            {
                cropCard = collider.gameObject;
                collider.gameObject.SetActive(false);
            }*/
		}
        
    }

   public void OnTriggerEnterClient()
    {
        //test function
            isCreated = false;
            soldierCard = Soldier;
        CardAsset asset;
        asset=Resources.Load("Ninja") as CardAsset;
        soldierCard.GetComponent<OneSoldierManager>().cardAsset = asset;
        Debug.Log("Part 2");
        string ca = asset.name.ToString();
        Debug.Log(ca);


    }


    void OnTriggerExit(Collider collider)
	{
	}

    public void GenerateBasePlayer(int x, int y)
    {
        if (isBlueplayer == true)
        {
            if (x < 0 || x > 6 || y < 0 || y > 2)
                return;
        }
        if (isBlueplayer == false)
        {
            if (x < 0 || x > 6 || y < 3 || y > 6)
                return;
        }
        GameObject newGO = Instantiate(Base) as GameObject;
        newGO.gameObject.GetComponent<Base>().isBaseBlue = true;
        newGO.transform.position = new Vector3(x, y, 0);
        GameUnits b = newGO.gameObject.GetComponent<GameUnits>();
        baseBlueX = x;
        baseBlueY = y;
        baseList.Add(newGO.gameObject.GetComponent<Base>());
        cards[x, y] = b;
        baseBlueCreated = true;

    }

    public void GenerateBaseClient(int x, int y)
    {
        GameObject newGO = Instantiate(Base) as GameObject;
        newGO.gameObject.GetComponent<Base>().isBaseRed = true;
        newGO.transform.position = new Vector3(x, y, 0);
        GameUnits b = newGO.gameObject.GetComponent<GameUnits>();
        baseRedX = x;
        baseRedY = y;
        baseList.Add(newGO.gameObject.GetComponent<Base>());
        cards[x, y] = b;
        baseRedCreated = true;
    }

    public void GenerateBaseBlue(int x, int y)
	{
		if (x < 0 || x > 6 || y < 0 || y > 2)
			return; 
		GameObject newGO = Instantiate(Base) as GameObject;
        newGO.gameObject.GetComponent<Base>().isBaseBlue = true;
        newGO.transform.position = new Vector3(x, y, 0);
		GameUnits b = newGO.gameObject.GetComponent<GameUnits> ();
		baseBlueX = x;
		baseBlueY = y;
		baseList.Add (newGO.gameObject.GetComponent<Base> ());
		cards [x, y] = b;
		baseBlueCreated = true;
	}

	public void GenerateBaseRed(int x, int y)
	{
		if (x < 0 || x > 6 || y < 3 || y > 6)
			return; 
		GameObject newGO = Instantiate(Base) as GameObject;
        newGO.gameObject.GetComponent<Base>().isBaseRed = true;
		newGO.transform.position = new Vector3(x, y, 0);
		GameUnits b = newGO.gameObject.GetComponent<GameUnits> ();
		baseRedX = x;
		baseRedY = y;
		baseList.Add (newGO.gameObject.GetComponent<Base> ());
		cards [x, y] = b;
		baseRedCreated = true;
	}

    public void GenerateSoldier(GameObject go, string cardName, int x, int y)
    {
        if (isCreated == false)
        {

                if (cards[x, y] != null || ((x >= baseBlueX + 1 || x <= baseBlueX - 1) && (y != baseBlueY)) || ((y >= baseBlueY + 1 || y <= baseBlueY - 1) && (x != baseBlueX)))
                    return;
            

                GameObject newGO = Instantiate(Soldier) as GameObject;
            //if (cards [x, y] == null && ((x == baseBlueX + 1 || x == baseBlueX - 1) && (y == baseBlueY)) || ((y == baseBlueY + 1 || y == baseBlueY - 1) && (x == baseBlueX)))
            newGO.transform.position = new Vector3(x, y, 0);
            newGO.gameObject.GetComponent<OneSoldierManager>().cardAsset = go.gameObject.GetComponent<OneCardManager>().cardAsset;
            newGO.gameObject.GetComponent<OneSoldierManager>().ReadSoldierFromAsset();
                newGO.gameObject.GetComponent<OneSoldierManager>().isBlue = true;
            cardName = newGO.gameObject.GetComponent<OneSoldierManager>().cardAsset.name.ToString();

            newGO.gameObject.GetComponent<SoldierPreview>().PreviewUnit = go.gameObject.GetComponent<SoldierPreview>().PreviewUnit;
            newGO.gameObject.GetComponent<SoldierPreview>().PreviewText = go.gameObject.GetComponent<SoldierPreview>().PreviewText;
            //Debug.Log (collider.gameObject.GetComponent<OneCardManager> ().cardAsset.name);
            GameUnits s = newGO.GetComponent<GameUnits>();
            cards[x, y] = s;
            soldierList.Add(s);
            isCreated = true;
            soldierCard = null;
            Destroy(go.gameObject);

        }
        //if(soldiers[x,y] != null)
        //Debug.Log (soldiers[x,y].cardAsset.name);
    }

    public void GenerateSoldierBlue(GameObject go, string cardName, int x, int y)
	{
		if (isCreated == false) {
			if (cards[x,y] != null || ((x >= baseBlueX + 1 || x <= baseBlueX - 1) && (y != baseBlueY)) || ((y >= baseBlueY + 1 || y <= baseBlueY - 1) && (x != baseBlueX)))
				return;

            GameObject newGO = Instantiate (Soldier) as GameObject;
			//if (cards [x, y] == null && ((x == baseBlueX + 1 || x == baseBlueX - 1) && (y == baseBlueY)) || ((y == baseBlueY + 1 || y == baseBlueY - 1) && (x == baseBlueX)))
		    newGO.transform.position = new Vector3 (x, y, 0);
			newGO.gameObject.GetComponent<OneSoldierManager> ().cardAsset = go.gameObject.GetComponent<OneCardManager> ().cardAsset;
			newGO.gameObject.GetComponent<OneSoldierManager> ().ReadSoldierFromAsset ();
			newGO.gameObject.GetComponent<OneSoldierManager> ().isBlue = true;

            cardName = newGO.gameObject.GetComponent<OneSoldierManager>().cardAsset.name.ToString();

            newGO.gameObject.GetComponent<SoldierPreview> ().PreviewUnit = go.gameObject.GetComponent<SoldierPreview> ().PreviewUnit;
			newGO.gameObject.GetComponent<SoldierPreview> ().PreviewText = go.gameObject.GetComponent<SoldierPreview> ().PreviewText;
            //Debug.Log (collider.gameObject.GetComponent<OneCardManager> ().cardAsset.name);
            GameUnits s = newGO.GetComponent<GameUnits>();
			cards[x, y] = s;
            soldierList.Add(s);
			isCreated = true;
			soldierCard = null;
			Destroy (go.gameObject);

        }
		//if(soldiers[x,y] != null)
			//Debug.Log (soldiers[x,y].cardAsset.name);
	}


    public void GenerateClientSoldierBlue(string cardName, int x, int y)
    {
        isCreated = false;
        CardAsset ca = Resources.Load("_soldiers/" + cardName) as CardAsset;
        if (ca != null)
        {
            Debug.Log("Asset Present");
        }
        else
        {
            Debug.Log("No Asset");
        }
        GameObject goTest = Instantiate(Soldier) as GameObject;
        goTest.AddComponent<OneSoldierManager>();
        goTest.AddComponent<SoldierPreview>();
        goTest.AddComponent<OneCardManager>();
        goTest.GetComponent<OneSoldierManager>().cardAsset = ca;

        if (goTest.GetComponent<OneSoldierManager>().cardAsset != null)
        {
            goTest.GetComponent<OneSoldierManager>().ReadSoldierFromAsset();
            goTest.GetComponent<SoldierPreview>().NameText.text = goTest.GetComponent<OneSoldierManager>().cardAsset.name.ToString();
            goTest.GetComponent<SoldierPreview>().ManaText.text = goTest.GetComponent<OneSoldierManager>().cardAsset.ManaCost.ToString();
            goTest.GetComponent<SoldierPreview>().AttackText.text = goTest.GetComponent<OneSoldierManager>().cardAsset.Attack.ToString();
            goTest.GetComponent<SoldierPreview>().HealthText.text = goTest.GetComponent<OneSoldierManager>().cardAsset.MaxHealth.ToString();
            goTest.GetComponent<SoldierPreview>().RangeText.text = goTest.GetComponent<OneSoldierManager>().cardAsset.SoldierRange.ToString();
            goTest.GetComponent<SoldierPreview>().MovementText.text = goTest.GetComponent<OneSoldierManager>().cardAsset.Movement.ToString();

        }
        else Debug.Log("No Asset");
        if (isCreated == false)
        {
            if (cards[x, y] != null)
                return;

            GameObject newGO = Instantiate(Soldier) as GameObject;
            //if (cards [x, y] == null && ((x == baseRedX + 1 || x == baseRedX - 1) && (y == baseRedY)) || ((y == baseRedY + 1 || y == baseRedY - 1) && (x == baseRedX)))
            newGO.transform.position = new Vector3(x, y, 0);
            newGO.gameObject.GetComponent<OneSoldierManager>().cardAsset = goTest.gameObject.GetComponent<OneSoldierManager>().cardAsset;
            newGO.gameObject.GetComponent<OneSoldierManager>().ReadSoldierFromAsset();
        
                newGO.gameObject.GetComponent<OneSoldierManager>().isRed = true;

            cardName = newGO.gameObject.GetComponent<OneSoldierManager>().cardAsset.name.ToString();

            newGO.gameObject.GetComponent<SoldierPreview>().PreviewUnit = goTest.gameObject.GetComponent<SoldierPreview>().PreviewUnit;
            newGO.gameObject.GetComponent<SoldierPreview>().PreviewText = goTest.gameObject.GetComponent<SoldierPreview>().PreviewText;
            //Debug.Log (collider.gameObject.GetComponent<OneCardManager> ().cardAsset.name);
            GameUnits s = newGO.GetComponent<GameUnits>();
            cards[x, y] = s;
            soldierList.Add(s);
            isCreated = true;
            soldierCard = null;
            Destroy(goTest.gameObject);

        }
        //GenerateSoldierBlue(goTest, cardName, x, y);
    }

    public void GenerateSoldierRed(GameObject go, string cardName, int x, int y)
	{
		if (isCreated == false) {

            if (cards[x,y] != null || ((x >= baseRedX + 1 || x <= baseRedX - 1) && (y != baseRedY)) || ((y >= baseRedY + 1 || y <= baseRedY - 1) && (x != baseRedX)))
				return;

			GameObject newGO = Instantiate (Soldier) as GameObject;
			//if (cards [x, y] == null && ((x == baseRedX + 1 || x == baseRedX - 1) && (y == baseRedY)) || ((y == baseRedY + 1 || y == baseRedY - 1) && (x == baseRedX)))
			newGO.transform.position = new Vector3 (x, y, 0);
			newGO.gameObject.GetComponent<OneSoldierManager> ().cardAsset = go.gameObject.GetComponent<OneCardManager> ().cardAsset;
			newGO.gameObject.GetComponent<OneSoldierManager> ().ReadSoldierFromAsset ();
            newGO.gameObject.GetComponent<OneSoldierManager>().isRed = true;
            cardName = newGO.gameObject.GetComponent<OneSoldierManager>().cardAsset.name.ToString();

            newGO.gameObject.GetComponent<SoldierPreview> ().PreviewUnit = go.gameObject.GetComponent<SoldierPreview> ().PreviewUnit;
			newGO.gameObject.GetComponent<SoldierPreview> ().PreviewText = go.gameObject.GetComponent<SoldierPreview> ().PreviewText;
			//Debug.Log (collider.gameObject.GetComponent<OneCardManager> ().cardAsset.name);
			GameUnits s = newGO.GetComponent<GameUnits>();
			cards[x, y] = s;
            soldierList.Add(s);
			isCreated = true;
			soldierCard = null;
			Destroy (go.gameObject);

        }
        //if(soldiers[x,y] != null)
        //Debug.Log (soldiers[x,y].cardAsset.name);
    }

    public void GenerateClientSoldierRed(string cardName, int x, int y)
    {
        isCreated = false;
        CardAsset ca = Resources.Load("_soldiers/" + cardName) as CardAsset;
        if (ca != null)
        {
            Debug.Log("Asset Present");
        }
        else
        {
            Debug.Log("No Asset");
        }
        GameObject goTest = Instantiate(Soldier) as GameObject;
        goTest.AddComponent<OneSoldierManager>();
        goTest.AddComponent<SoldierPreview>();
        goTest.GetComponent<OneSoldierManager>().cardAsset = ca;

        if (goTest.GetComponent<OneSoldierManager>().cardAsset != null)
        {
            goTest.GetComponent<OneSoldierManager>().ReadSoldierFromAsset();
            goTest.GetComponent<SoldierPreview>().NameText.text = goTest.GetComponent<OneSoldierManager>().cardAsset.name.ToString();
            goTest.GetComponent<SoldierPreview>().ManaText.text = goTest.GetComponent<OneSoldierManager>().cardAsset.ManaCost.ToString();
            goTest.GetComponent<SoldierPreview>().AttackText.text = goTest.GetComponent<OneSoldierManager>().cardAsset.Attack.ToString();
            goTest.GetComponent<SoldierPreview>().HealthText.text = goTest.GetComponent<OneSoldierManager>().cardAsset.MaxHealth.ToString();
            goTest.GetComponent<SoldierPreview>().RangeText.text = goTest.GetComponent<OneSoldierManager>().cardAsset.SoldierRange.ToString();
            goTest.GetComponent<SoldierPreview>().MovementText.text = goTest.GetComponent<OneSoldierManager>().cardAsset.Movement.ToString();
        }
        else Debug.Log("No Asset");
        if (isCreated == false)
        {
            if (cards[x, y] != null)
                return;

            GameObject newGO = Instantiate(Soldier) as GameObject;
            //if (cards [x, y] == null && ((x == baseRedX + 1 || x == baseRedX - 1) && (y == baseRedY)) || ((y == baseRedY + 1 || y == baseRedY - 1) && (x == baseRedX)))
            newGO.transform.position = new Vector3(x, y, 0);
            newGO.gameObject.GetComponent<OneSoldierManager>().cardAsset = goTest.gameObject.GetComponent<OneSoldierManager>().cardAsset;
            newGO.gameObject.GetComponent<OneSoldierManager>().ReadSoldierFromAsset();
            newGO.gameObject.GetComponent<OneSoldierManager>().isRed = true;

            cardName = newGO.gameObject.GetComponent<OneSoldierManager>().cardAsset.name.ToString();

            newGO.gameObject.GetComponent<SoldierPreview>().PreviewUnit = goTest.gameObject.GetComponent<SoldierPreview>().PreviewUnit;
            newGO.gameObject.GetComponent<SoldierPreview>().PreviewText = goTest.gameObject.GetComponent<SoldierPreview>().PreviewText;
            //Debug.Log (collider.gameObject.GetComponent<OneCardManager> ().cardAsset.name);
            GameUnits s = newGO.GetComponent<GameUnits>();
            cards[x, y] = s;
            soldierList.Add(s);
            isCreated = true;
            soldierCard = null;
            Destroy(goTest.gameObject);

        }
        //GenerateSoldierRed(goTest, cardName, x, y);
    }

    public void GenerateCropBlue(GameObject go, string cardName, int x, int y)
	{
		if (cropsBlue != 0) {
			if (x < 0 || x > 6 || y < 0 || y > 2)
				return; 
			GameObject newGO = Instantiate (Crop) as GameObject;
			newGO.transform.position = new Vector3 (x, y, 0);
			newGO.gameObject.GetComponent<OneCropManager> ().cardAsset = go.gameObject.GetComponent<OneCardManager> ().cardAsset;
			newGO.gameObject.GetComponent<OneCropManager> ().ReadCropFromAsset ();

            cardName = newGO.gameObject.GetComponent<OneCropManager>().cardAsset.name.ToString();

            newGO.gameObject.GetComponent<CropPreview> ().PreviewUnit = go.gameObject.GetComponent<CropPreview> ().PreviewUnit;
			newGO.gameObject.GetComponent<CropPreview> ().PreviewText = go.gameObject.GetComponent<CropPreview> ().PreviewText;
            //newGO.transform.SetParent (this.gameObject.transform, false);
            //Debug.Log (collider.gameObject.GetComponent<OneCardManager> ().cardAsset.name);
            GameUnits c = newGO.GetComponent<GameUnits>();
			cards[x, y] = c;
			//isCreated = true;
			//Destroy(go.gameObject);
			cropsBlue--;
			//Debug.Log (cropsBlue);
        }
	}

    public void GenerateCrop(GameObject go, string cardName, int x, int y)
    {
        if (cropsBlue != 0)
        {
            if (isBlueplayer == true)
            {
                if (x < 0 || x > 6 || y < 0 || y > 2)
                    return;
            }
            if (isBlueplayer == false)
            {
                if (x < 0 || x > 6 || y < 3 || y > 6)
                    return;
            }
            GameObject newGO = Instantiate(Crop) as GameObject;
            newGO.transform.position = new Vector3(x, y, 0);
            newGO.gameObject.GetComponent<OneCropManager>().cardAsset = go.gameObject.GetComponent<OneCardManager>().cardAsset;
            newGO.gameObject.GetComponent<OneCropManager>().ReadCropFromAsset();

            cardName = newGO.gameObject.GetComponent<OneCropManager>().cardAsset.name.ToString();

            newGO.gameObject.GetComponent<CropPreview>().PreviewUnit = go.gameObject.GetComponent<CropPreview>().PreviewUnit;
            newGO.gameObject.GetComponent<CropPreview>().PreviewText = go.gameObject.GetComponent<CropPreview>().PreviewText;
            //newGO.transform.SetParent (this.gameObject.transform, false);
            //Debug.Log (collider.gameObject.GetComponent<OneCardManager> ().cardAsset.name);
            GameUnits c = newGO.GetComponent<GameUnits>();
            cards[x, y] = c;
            
            //isCreated = true;

            cropsBlue--;
            //Debug.Log (cropsBlue);

            //Networking action
            msg = "CGGC|";
            msg += cardName + "|";
            msg += x.ToString() + "|";
            msg += y.ToString();
            Debug.Log(msg);
            client.Send(msg);
            //End networking action
            
        }

    }

    public void GenerateCropClient(string cardName, int x, int y)
    {
        CardAsset ca = Resources.Load("_crops/" + cardName) as CardAsset;
        if (ca != null)
        {
            Debug.Log("Asset Present");
        }
        else
        {
            Debug.Log("No Asset");
        }
        GameObject goTest = Instantiate(Crop) as GameObject;
        goTest.AddComponent<OneCropManager>();
        goTest.AddComponent<CropPreview>();
        goTest.GetComponent<OneCropManager>().cardAsset = ca;

        if (goTest.GetComponent<OneCropManager>().cardAsset != null)
        {
            goTest.GetComponent<OneCropManager>().ReadCropFromAsset();
        }
        if (cards[x, y] != null)
            return;
        GameObject newGO = Instantiate(Crop) as GameObject;
        newGO.transform.position = new Vector3(x, y, 0);
        newGO.gameObject.GetComponent<OneCropManager>().cardAsset = goTest.gameObject.GetComponent<OneCropManager>().cardAsset;
        newGO.gameObject.GetComponent<OneCropManager>().ReadCropFromAsset();
        cardName = newGO.gameObject.GetComponent<OneCropManager>().cardAsset.name.ToString();
        newGO.gameObject.GetComponent<CropPreview>().PreviewUnit = goTest.gameObject.GetComponent<CropPreview>().PreviewUnit;
        newGO.gameObject.GetComponent<CropPreview>().PreviewText = goTest.gameObject.GetComponent<CropPreview>().PreviewText;
        GameUnits c = newGO.GetComponent<GameUnits>();
        cards[x, y] = c;
    }

    public void GenerateCropRed(GameObject go, string cardName, int x, int y)
	{
		if (cropsRed != 0) {
			if (x < 0 || x > 6 || y < 3 || y > 6)
				return;
			GameObject newGO = Instantiate (Crop) as GameObject;
			newGO.transform.position = new Vector3 (x, y, 0);
			newGO.gameObject.GetComponent<OneCropManager> ().cardAsset = go.gameObject.GetComponent<OneCardManager> ().cardAsset;
            newGO.gameObject.GetComponent<OneCropManager>().ReadCropFromAsset();

            cardName = newGO.gameObject.GetComponent<OneCropManager>().cardAsset.name.ToString();

            newGO.gameObject.GetComponent<CropPreview> ().PreviewUnit = go.gameObject.GetComponent<CropPreview> ().PreviewUnit;
			newGO.gameObject.GetComponent<CropPreview> ().PreviewText = go.gameObject.GetComponent<CropPreview> ().PreviewText;
			//newGO.transform.SetParent (this.gameObject.transform, false);
			//Debug.Log (collider.gameObject.GetComponent<OneCardManager> ().cardAsset.name);
			GameUnits c = newGO.GetComponent<GameUnits>();
			cards[x, y] = c;
			//isCreated = true;
			//Destroy(go.gameObject);
			cropsRed--;
			//Debug.Log (cropsRed);
		}
	}

	public void SelectSoldierBlue(int x, int y)
	{
		//out of bounds
		if (x < 0 || x > 6 || y < 0 || y > 6)
			return;

        GameUnits s = cards[x,y];
		if (s != null && s.gameObject.GetComponent<OneSoldierManager>().isBlue == true 
		&& s.gameObject.GetComponent<OneSoldierManager>().cardAsset.TypeOfCard == TypesOfCards.Soldier)
		{
			selectedSoldierCard = s;
			startDrag = mouseOver;
            Debug.Log(selectedSoldierCard.gameObject.GetComponent<OneSoldierManager>().cardAsset.name);
		}
		else
			Debug.Log("nothing there");
		
	}

	public void SelectSoldierRed(int x, int y)
	{
		//out of bounds
		if (x < 0 || x > 6 || y < 0 || y > 6)
			return;

		GameUnits s = cards[x,y];
		if (s != null && s.gameObject.GetComponent<OneSoldierManager>().isRed == true 
		&& s.gameObject.GetComponent<OneSoldierManager>().cardAsset.TypeOfCard == TypesOfCards.Soldier)
		{
			selectedSoldierCard = s;
			startDrag = mouseOver;

            Debug.Log(selectedSoldierCard.gameObject.GetComponent<OneSoldierManager>().cardAsset.name);
		}
		else
			Debug.Log("nothing there");

	}

    public void TryAttack(int x1, int y1, int x2, int y2)
    {
        selectedSoldierCard = cards[x1, y1];
        selectedSoldierCard.GetComponent<attack>().doAttack(playerRed, cards, x1, y1, x2, y2);
        selectedSoldierCard.GetComponent<attack>().doAttack(playerBlue, cards, x1, y1, x2, y2);
    }

	public void TryMove(int x1, int y1, int x2, int y2)
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
            if (selectedSoldierCard.moving == false)
            {
                MoveSoldier(selectedSoldierCard, x1, y1);
                startDrag = Vector2.zero;
                selectedSoldierCard = null;
                return;
            }


            //check if valid move
            if (selectedSoldierCard.ValidMove(cards, x1, y1, x2, y2) && selectedSoldierCard.moving == true)
            {
                cards[x2, y2] = selectedSoldierCard;
                cards[x1, y1] = null;
                MoveSoldier(selectedSoldierCard, x2, y2);
                selectedSoldierCard.moving = false;
            }
            else if (selectedSoldierCard.attacking == true)
            {
                if (selectedSoldierCard.GetComponent<attack>() == null)
                {
                    selectedSoldierCard.gameObject.AddComponent<attack>();
                }

                if (selectedSoldierCard.GetComponent<OneSoldierManager>().isBlue)
                {
                    //Networking action
                    msg = "CATK|";
                    msg += x1 + "|";
                    msg += y1.ToString() + "|";
                    msg += x2.ToString() + "|";
                    msg += y2.ToString();
                    client.Send(msg);
                    //End networking action
                    selectedSoldierCard.GetComponent<attack>().doAttack(playerBlue, cards, x1, y1, x2, y2);
                } else if(selectedSoldierCard.GetComponent<OneSoldierManager>().isRed)
                {
                    selectedSoldierCard.GetComponent<attack>().doAttack(playerRed, cards, x1, y1, x2, y2);
                }
                selectedSoldierCard.moving = false;
                selectedSoldierCard.attacking = false;
            }
        }
	}

	public void MoveSoldier(GameUnits soldierUnit, int x, int y)
	{
        if(soldierUnit.gameObject.GetComponent<OneSoldierManager>().cardAsset.TypeOfCard == TypesOfCards.Soldier)
            soldierUnit.transform.position = (Vector2.right * x) + (Vector2.up * y);
	}
}
