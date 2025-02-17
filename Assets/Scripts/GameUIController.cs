using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameUIController : MonoBehaviour
{
    public GameObject backGround;
    public TextMeshPro gameOverText;
    public Button restartButton;
    public Button backButton;

    void Start()
    {
        restartButton.onClick.AddListener(RestartButton);
        backButton.onClick.AddListener(BackButtonClick);
    }
    public void BackButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        //gameOverText.IsActive;
    }

}
