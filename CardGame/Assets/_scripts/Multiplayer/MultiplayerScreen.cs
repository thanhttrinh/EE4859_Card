using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerScreen : MonoBehaviour {

    public void StartButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
