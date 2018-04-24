using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoQuitButton : MonoBehaviour {

    public GameObject ConfirmQuitCanvas;
    public Button NoButton;

    private void Start()
    {
        Button no = NoButton.GetComponent<Button>();
        no.onClick.AddListener(Resume);
    }
    public void Resume()
    {
        ConfirmQuitCanvas.SetActive(false);
    }
}
