using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	float targetWidth = 1080.0f;
	float targetHeight = 720.0f;
	int pixels = 30; //1:1 ratio of pixels to unit


	void Start () {
		float targetSize = targetWidth / targetHeight;
		float curSize = (float)Screen.width / (float) Screen.height;

		Debug.Log (Screen.currentResolution);
		Debug.Log ("current size: " + curSize);
		Debug.Log ("target size: " + targetSize);

		if (curSize > targetSize) {
			Camera.main.orthographicSize = targetHeight / 4 / pixels;
		} else {
			float differenceInSize = targetSize / curSize;
			Camera.main.orthographicSize = targetHeight / 4 / pixels * differenceInSize;

		}

	}
	
	// Update is called once per frame
	void Update () {
	}
}
