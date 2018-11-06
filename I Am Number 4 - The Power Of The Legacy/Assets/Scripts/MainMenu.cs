using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("Game");
    }
    public void OnRulesClick()
    {
        SceneManager.LoadScene("Rules");
    }
}
