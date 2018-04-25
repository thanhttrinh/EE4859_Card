using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour {

    public GameObject ConfirmQuitCanvas;
    public Button QuitButton;

    private void Start()
    {
        Button quit = QuitButton.GetComponent<Button>();
        quit.onClick.AddListener(AreYouSurePrompt);
    }
    public void AreYouSurePrompt()
    {
        ConfirmQuitCanvas.SetActive(true);
    }
}
