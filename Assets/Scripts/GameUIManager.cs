using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameUIManager : Singleton<GameUIManager>
{
    public GameObject backGround;
    public GameObject gameOverTextHolder;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button backButton;

    public TextMeshProUGUI shotsRemainingText;

    private void Start()
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

    public void UpdateUI(int currentBallCount)
    {
        if (shotsRemainingText != null)
        {
            shotsRemainingText.text = "Shots: " + currentBallCount.ToString();
        }
        else
        {
            Debug.LogError("shotsText is NULL! Assign the UI text reference.");
        }
    }

    public void GameOver()
    {
        gameOverTextHolder.SetActive(true);

        gameOverText.text = "You lose!";
        gameOverText.color = Color.red;
    }

    public void WinGame()
    {
        gameOverTextHolder.SetActive(true);

        gameOverText.text = "You win!";
        gameOverText.color = Color.green;
    }

}
