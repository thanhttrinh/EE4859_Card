using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManaCommand : Command {

	public Player player;
	private int totalMana;
	private int avaliableMana;

	public UpdateManaCommand(Player player, int totalMana, int avaliableMana){
		this.player = player;
		this.totalMana = totalMana;
		this.avaliableMana = avaliableMana;
	}

	public override void StartCommandExecution ()
	{
		player.PArea.ManaBar.TotalCrystals = totalMana;
		player.PArea.ManaBar.AvailableCrystals = avaliableMana;
		Command.CommandExecutionComplete ();
	}
}
