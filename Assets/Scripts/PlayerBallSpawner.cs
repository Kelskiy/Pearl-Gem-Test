using UnityEngine;
using TMPro;

public class PlayerBallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform spawnPoint;
    public int maxBalls = 5;
    private int currentBallCount;
    private GameObject currentBall;

    private Rigidbody currentBallRb;

    public GameObject CurrentBall
    {
        get => currentBall;
        set => currentBall = value;
    }

    private void Start()
    {
        currentBallCount = maxBalls;
        GameUIManager.Instance.UpdateUI(currentBallCount);
        SpawnBall();
    }

    public void SpawnBall()
    {
        //Player lost
        if (currentBallCount == 0 && SpawnManager.Instance.currentBallColors.Count > 0)
        {
            GameManager.Instance.ChangedGameState(GameState.GameOver);
            GameUIManager.Instance.GameOver();
            return;
        }

        //Player won
        if (SpawnManager.Instance.currentBallColors.Count == 0)
        {
            GameManager.Instance.ChangedGameState(GameState.GameOver);
            GameUIManager.Instance.WinGame();
            return;
        }

        if (currentBall == null && currentBallCount > 0)
        {
            currentBall = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
            currentBallRb = currentBall.GetComponent<Rigidbody>();

            if (currentBallRb)
            {
                currentBallRb.isKinematic = true;
            }

            PlayerBall ballScript = currentBall.GetComponent<PlayerBall>();
            BallColor randomColor = SpawnManager.Instance.GetRandomColor();
            ballScript.SetBallColor(randomColor);
        }
    }

    public void ShootBall()
    {
        currentBallCount--;
        GameUIManager.Instance.UpdateUI(currentBallCount);
    }
}

