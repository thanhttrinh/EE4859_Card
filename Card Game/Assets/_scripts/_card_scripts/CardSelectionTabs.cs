using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectionTabs : MonoBehaviour {

	public List<CardTypeFilter> tabs = new List<CardTypeFilter> ();
	public CardTypeFilter CardTab;

	private int currentIndex = 0;

	public void SelectTab(CardTypeFilter tab, bool instant)
	{
		int newIndex = tabs.IndexOf (tab);

		if (newIndex == currentIndex)
			return;

		currentIndex = newIndex;

		//we have selected a new tab
		foreach (CardTypeFilter t in tabs) {
			if (t != tab)
				t.Deselect (instant);
		}

		tab.Select (instant);
		//CCScreen.Instance.CollectionBrowserScript.Asset = tab.Asset;
		//CCScreen.Instance.CollectionBrowserScript.IncludeAllCharacters = tab.showAllCharacters;
	}

}
