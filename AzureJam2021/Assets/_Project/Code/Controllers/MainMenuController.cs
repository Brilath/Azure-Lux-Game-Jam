using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{

    public void StartGame()
    {
        SceneController.LoadScene("Story");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Logout()
    {
        PlayfabController.Instance.Logout();
        SceneController.LoadScene("Login");
    }
}
