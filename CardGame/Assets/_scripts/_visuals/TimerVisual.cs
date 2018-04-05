using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TimerVisual : MonoBehaviour {
	public float TimeForOneTurn;
	public Text TimerText;
	private float TimerTillZero;
	private bool counting = false;

	[SerializeField]
	public UnityEvent TimerExpired = new UnityEvent();

	public void StartTime(){
		TimerTillZero = TimeForOneTurn;
		counting = true;
	}

	public void StopTimer(){
		counting = false;
	}

	void Update(){
		if (counting) {
			TimerTillZero -= Time.deltaTime;
			if (TimerText != null)
				TimerText.text = ToString ();

			//check for TimeExpired
			if (TimerTillZero <= 0) {
				counting = false;
				TimerExpired.Invoke ();
			}
		}
	}

	public override string ToString ()
	{
		int inSeconds = Mathf.RoundToInt (TimerTillZero);
		string justSeconds = (inSeconds % 60).ToString ();
		if (justSeconds.Length == 1)
			justSeconds = "0" + justSeconds;
		string justMinutes = (inSeconds / 60).ToString ();
		if (justMinutes.Length == 1)
			justMinutes = "0" + justMinutes;
		return string.Format ("{0}:{1}", justMinutes, justSeconds);
	}
}
