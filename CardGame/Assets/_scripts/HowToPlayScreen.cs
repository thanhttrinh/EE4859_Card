using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayScreen : MonoBehaviour {

    public GameObject screenContent;
    public GameObject titleScreen;

    public GameObject GetStartedText;
    public GameObject CCText;
    public GameObject DSText;
    public GameObject GPText;
    

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
        CCText.SetActive(true);
        GetStartedText.SetActive(false);
        DSText.SetActive(false);
        GPText.SetActive(false);
    }

    public void ShowDSInstructions()
    {
        DSText.SetActive(true);
        CCText.SetActive(false);
        GetStartedText.SetActive(false);
        GPText.SetActive(false);
    }

    public void ShowGamePlayInstructions()
    {
        GPText.SetActive(true);
        CCText.SetActive(false);
        GetStartedText.SetActive(false);
        DSText.SetActive(false);
    }
}
