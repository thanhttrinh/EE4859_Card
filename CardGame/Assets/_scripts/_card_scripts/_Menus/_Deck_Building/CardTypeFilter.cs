﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardTypeFilter : MonoBehaviour 
{
	//if null, this tab shows all cards
	public CardTypeAsset asset;
	public bool showAllCards = true;

	private CardSelectionTabs TabsScript;
	private float selectionTransitionTime = 0.5f;
	private Vector3 initialScale = Vector3.one;
	private float scaleMultiplier = 1f;

	public void TabButtonHandler(){
		CCScreen.Instance.TabsScript.SelectTab (this, false);
	}

	public void Select(bool instant = false){
		if (instant)
			transform.localScale = initialScale * scaleMultiplier;
		else
			transform.DOScale (initialScale.x * scaleMultiplier, selectionTransitionTime);
	}

	public void Deselect(bool instant = false)
	{
		if (instant)
			transform.localScale = initialScale;
		else
			transform.DOScale (initialScale, selectionTransitionTime);
	}
}
