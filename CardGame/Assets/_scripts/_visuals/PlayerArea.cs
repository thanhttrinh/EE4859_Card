﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AreaPosition{red, blue};

public class PlayerArea : MonoBehaviour {

	public AreaPosition owner;
	public bool ControlON = true;
	public PlayerDeckVisual PDeck;
	public ManaPoolVisual ManaBar;
	public HandVisual handVisual;
	public GridBoard gridVisual;

	public bool AllowedToControlThisPlayer {
		get;
		set;
	}

}
