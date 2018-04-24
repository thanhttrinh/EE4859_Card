using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayScreen : MonoBehaviour {

    public GameObject screenContent;
    public GameObject titleScreen;
    

    public static HowToPlayScreen Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowScreen()
    {
        screenContent.SetActive(true);
        titleScreen.SetActive(false);
        CCScreen.Instance.screenContent.SetActive(false);
        DeckSelectionScreen.Instance.screenContent.SetActive(false);
    }

    public void HideScreen()
    {
        screenContent.SetActive(false);
        titleScreen.SetActive(true);
    }

    public void ShowCCInstructions()
    {

    }

    public void ShowGamePlayInstructions()
    {

    }
}
