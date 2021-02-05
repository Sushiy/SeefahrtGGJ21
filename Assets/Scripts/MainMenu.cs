using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelName = "MainMap_v1";
    [Space]
    [Header ( "Buttons" )]
    public UnityEngine.UI.Button startGameButton;
    public UnityEngine.UI.Button audioButton;
    public UnityEngine.UI.Button exitButton;

    public void StartGame()
    {
        SceneManager.LoadScene(levelName,LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
