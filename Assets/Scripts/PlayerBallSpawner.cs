using UnityEngine;
using TMPro;

public class PlayerBallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform spawnPoint;
    public int maxBalls = 5;
    private int currentBallCount;
    private GameObject currentBall;

    public BallColor ballColor;

    public TextMeshProUGUI shootsText;
    public float ballLifeTime = 5f;

    private Rigidbody currentBallRb;

    public GameObject CurrentBall
    {
        get => currentBall;
        set => currentBall = value;
    }

    private void Start()
    {
        currentBallCount = maxBalls;
        UpdateUI();
        SpawnBall();
    }

    public void SpawnBall()
    {
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

            currentBallCount--;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (shootsText != null)
        {
            shootsText.text = "Shots: " + currentBallCount.ToString();
        }
        else
        {
            Debug.LogError("shotsText is NULL! Assign the UI text reference.");
        }
    }
}
