using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public static BallSpawner Instance;
    public GameObject ballPrefab;  // Префаб кульки (переконайтеся, що в ньому є Rigidbody і Ballistics)
    public Transform spawnPoint;   // Точка спавну кульки

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Метод для спавну кульки із заданим вектором (в якому міститься сила натягу)
    public GameObject SpawnBall(Vector3 launchVector)
    {
        GameObject ball = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
        Ballistics ballistics = ball.GetComponent<Ballistics>();
        if (ballistics != null)
        {
            ballistics.Shoot(launchVector);
        }
        else
        {
            Debug.LogError("Ballistics component is missing on the ball prefab.");
        }
        return ball;
    }
}
