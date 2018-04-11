using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShowMessageCommand : Command{

	string message;
	float duration;

	public ShowMessageCommand(string message, float duration){
		this.message = message;
		this.duration = duration;
	}

	public override void StartCommandExecution(){
		MessageManager.Instance.ShowMessage (message, duration);
		Command.CommandExecutionComplete ();
	}
}
