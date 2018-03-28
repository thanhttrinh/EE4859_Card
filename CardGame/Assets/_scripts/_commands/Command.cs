using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Commands are used to collect everything that happens instantly in game logic
// and show it in specific order in the visuals
public abstract class Command {

	//this is true if we are showing osme command in the visual part
	//and false if our CommandQueue is empty
	public static bool playingQueue{get; set;}

	//a collection of Commands (FIFO)
	static Queue<Command> CommandQueue = new Queue<Command>();

	public void AddToQueue(){
		CommandQueue.Enqueue (this);
		if (!playingQueue)
			PlayFirstCommandFromQueue ();
	}

	public abstract void StartCommandExecution();

	public static bool CardDrawPending(){
		foreach (Command c in CommandQueue) {
			if (c is DrawACardCommand)
				return true;
		}
		return false;
	}

	//function to move to the next command in CommandQueue
	public static void CommandExecutionComplete(){
		if (CommandQueue.Count > 0)
			PlayFirstCommandFromQueue ();
		else
			playingQueue = false;

		//TODO: After every turn, visually let the player know what is their next avaliable moves
	}

	//plays command that has the index 0 in command queue
	static void PlayFirstCommandFromQueue(){
		playingQueue = true;
		CommandQueue.Dequeue ().StartCommandExecution ();
	}

	public static void OnSceneReload(){
		CommandQueue.Clear ();
		CommandExecutionComplete ();
	}
}
