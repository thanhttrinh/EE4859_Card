using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeckInfo
{
	public string DeckName;
	public List<CardAsset> Cards;

	public DeckInfo(List<CardAsset> cards, string deckName)
	{
		//copy a list, not just use the card list
		Cards = new List<CardAsset>(cards);
		DeckName = deckName;
	}

	public bool IsComplete()
	{
		return CCScreen.Instance.BuilderScript.AmountOfCardsInDeck == Cards.Count;
	}

	public int NumberOfThisCardInDeck(CardAsset asset)
	{
		int count = 0;
		foreach (CardAsset ca in Cards) 
		{
			if (ca == asset)
				count++;
		}
		return count;
	}

}

public class DeckStorage : MonoBehaviour 
{
	public static DeckStorage Instance;
	public List<DeckInfo> AllDecks{ get; set; }
	private bool alreadyLoadedDecks = false;

	void Awake()
	{
		//AllDecks = new List<DeckInfo> ();
		//Debug.Log ("all deck instantiated");
		Instance = this;
	}

	void Start()
	{
		AllDecks = new List<DeckInfo> ();
		//load in decks that is already made, if there is any
		if (!alreadyLoadedDecks) 
		{
			LoadDecksFromPlayerPrefs ();
			alreadyLoadedDecks = true;
		}
	}

	void LoadDecksFromPlayerPrefs()
	{
		List<DeckInfo> DecksFound = new List<DeckInfo> ();
		//load the information about decks from PlayerPrefsX
		for (int i = 0; i < 9; i++) {
			string deckListKey = "Deck" + i.ToString ();
			string deckNameKey = "DeckName" + i.ToString ();
			string[] DeckAsCardNames = PlayerPrefsX.GetStringArray (deckListKey);

//			Debug.Log ("Has DeckName key: " + PlayerPrefs.HasKey (deckNameKey) + " ... " + PlayerPrefs.GetString(deckNameKey));
		//	Debug.Log ("Length of DeckAsCardNames: " + DeckAsCardNames.Length);

			if (DeckAsCardNames.Length > 0 && PlayerPrefs.HasKey (deckNameKey)) {
				string deckName = PlayerPrefs.GetString (deckNameKey);

				//make a CardAsset list from an array of strings
				List<CardAsset> deckList = new List<CardAsset>();
				foreach (string name in DeckAsCardNames) {
					deckList.Add (CardCollection.Instance.GetCardAssetByName (name));
				}
				DecksFound.Add (new DeckInfo (deckList, deckName));
			}
		}
		AllDecks = DecksFound;
	}

	public void SaveDecksIntoPlayerPrefs()
	{
		for (int i = 0; i < 9; i++) {
			string deckNameKey = "DeckName" + i.ToString ();

			if (PlayerPrefs.HasKey (deckNameKey))
				PlayerPrefs.DeleteKey (deckNameKey);
		}

		for (int i = 0; i < AllDecks.Count; i++) {
			string deckListKey = "Deck" + i.ToString ();
			string deckNameKey = "DeckName" + i.ToString ();

			List<string> cardNamesList = new List<string> ();
			foreach (CardAsset a in AllDecks[i].Cards)
				cardNamesList.Add (a.name);

			string[] cardNamesArray = cardNamesList.ToArray ();

			PlayerPrefsX.SetStringArray (deckListKey, cardNamesArray);
			PlayerPrefs.SetString (deckNameKey, AllDecks[i].DeckName);
		}

	}

	void OnApplicationQuit()
	{
		SaveDecksIntoPlayerPrefs ();
	}

}