using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SameDistanceChildren : MonoBehaviour {

	public Transform[] Children;

	void Awake(){
		Vector3 firstElementPos = Children [0].transform.position;
		Vector3 lastElementPos = Children [Children.Length - 1].transform.position;

		float xDis = (lastElementPos.x - firstElementPos.x) / (float)(Children.Length - 1);
		float yDis = (lastElementPos.y - firstElementPos.y) / (float)(Children.Length - 1);
		float zDis = (lastElementPos.z - firstElementPos.z) / (float)(Children.Length - 1);

		Vector3 Dist = new Vector3 (xDis, yDis, zDis);

		for (int i = 1; i < Children.Length; i++) {
			Children [i].transform.position = Children [i - 1].transform.position + Dist;
		}
	}
}
