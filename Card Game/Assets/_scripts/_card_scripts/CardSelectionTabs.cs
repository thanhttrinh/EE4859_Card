using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelectionTabs : MonoBehaviour {

	public List<CardTypeFilter> Tabs = new List<CardTypeFilter> ();
	public CardTypeFilter CardTab;
	public CardTypeFilter NeutralTabWhenCollectionBrowsing;
	private int currentIndex = 0;

	public void SelectTab(CardTypeFilter tab, bool instant)
	{
		int newIndex = Tabs.IndexOf (tab);

		if (newIndex == currentIndex)
			return;

		currentIndex = newIndex;
		//we have selected a new tab
		foreach (CardTypeFilter t in Tabs) {
			if (t != tab)
				t.Deselect (instant);
		}

		tab.Select (instant);
		CCScreen.Instance.CollectionBrowserScript.Asset = tab.asset;
		CCScreen.Instance.CollectionBrowserScript.IncludeAllCards = tab.showAllCards;
	}

	public void SetClassOnClassTab(CardTypeAsset asset){
		CardTab.asset = asset;
		CardTab.GetComponentInChildren<Text> ().text = asset.name;
	}

}
