using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;

    private void Start()
    {
        startButton.onClick.AddListener(StartButtonClick);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void StartButtonClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void QuitGame()
    {
        Application.Quit();
    }

}
