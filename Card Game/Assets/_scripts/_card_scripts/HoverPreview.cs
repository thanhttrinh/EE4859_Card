using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverPreview : MonoBehaviour {

	public Text PreviewText;

	public Text NameText;
	public Text ManaText;
	public Text DescriptionText;
	public Text HealthText;
	public Text AttackText;
	public Text RangeText;
	public Text MovementText;
	public Text CropSize;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseEnter() 
	{
        //if () 
        //{
        PreviewText.text = string.Format("Name: {gameObject.GetComponent<OneCardManager>().NameText.text}");
                /*Mana: {1}\nHealth: {2}\nAttack:{3}\nRange: {4}\nMovement: {5}", 
				NameText.ToString(), ManaText.ToString(), HealthText.ToString(), AttackText.ToString(), RangeText.ToString(), MovementText.ToString());*/
			
		//}
	}

	void OnMouseExit()
	{
		
	}
}
