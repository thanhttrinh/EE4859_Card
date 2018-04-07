using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridForCards : MonoBehaviour {

	public Transform[,] Children = new Transform[6, 3];

	void Awake(){
		Vector3 firstElementPos = Children [0, 0].transform.position;
		Vector3 lastElementPos = Children [Children.Length - 1, Children.Length - 1].transform.position;

		float xDis = (lastElementPos.x - firstElementPos.x) / (float)(Children.Length - 1);
		float yDis = (lastElementPos.y - firstElementPos.y) / (float)(Children.Length - 1);
		float zDis = (lastElementPos.z - firstElementPos.z) / (float)(Children.Length - 1);

		Vector3 Dist = new Vector3 (xDis, yDis, zDis);

		for (int i = 1; i < Children.Length; i++) {
			Children [i, i].transform.position = Children [i - 1, i - 1].transform.position + Dist;
		}
	}
}
