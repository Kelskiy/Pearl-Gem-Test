using UnityEngine;

[RequireComponent(typeof(TrajectoryPredictor))]
public class GameController : MonoBehaviour
{
    public Transform launchPoint;
    public float maxForce = 20f;
    public int trajectoryPoints = 50;
    public float timeToSpawnNextBall = 2f;

    public PlayerBallSpawner ballSpawner;
    public TrajectoryPredictor trajectoryPredictor;

    private LineRenderer lineRenderer;
    private Vector3 mouseStartPos;
    private bool isAiming = false;
    private float currentAngleVertical = 0f;
    private float currentAngleHorizontal = 0f;
    private float currentForce = 10f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        trajectoryPredictor = GetComponent<TrajectoryPredictor>();

        if (lineRenderer == null)
            Debug.LogError("Couldn't find LineRenderer");

        lineRenderer.positionCount = trajectoryPoints;
        lineRenderer.enabled = false;

        ballSpawner = FindObjectOfType<PlayerBallSpawner>();
        ballSpawner.SpawnBall();

        GameManager.Instance.ChangedGameState(GameState.Gameplay);
    }

    void Update()
    {
        if (GameManager.Instance.currentState == GameState.WaitingForStart || GameManager.Instance.currentState == GameState.GameOver)
        {
            trajectoryPredictor.ClearTrajectory();
            return;
        }

        if (ballSpawner.CurrentBall == null)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            isAiming = true;
            mouseStartPos = Input.mousePosition;
            lineRenderer.enabled = true;
        }

        if (isAiming)
        {
            Vector3 mouseDelta = Input.mousePosition - mouseStartPos;

            currentAngleVertical = Mathf.Lerp(-60f, 60f, Mathf.InverseLerp(-Screen.height / 2, Screen.height / 2, mouseDelta.y));
            currentAngleHorizontal = Mathf.Lerp(-45f, 45f, Mathf.InverseLerp(-Screen.width / 2, Screen.width / 2, mouseDelta.x));

            float distance = mouseDelta.magnitude / Screen.width;
            currentForce = Mathf.Lerp(5f, maxForce, distance);

            Quaternion rotation = Quaternion.Euler(-currentAngleVertical, currentAngleHorizontal, 0);
            Vector3 launchDirection = launchPoint.forward; 
            Vector3 launchVelocity = rotation * launchDirection * currentForce;

            trajectoryPredictor.PredictTrajectory(launchPoint.position, launchVelocity);
        }

        if (Input.GetMouseButtonUp(0) && isAiming)
        {
            isAiming = false;
            lineRenderer.enabled = false;

            LaunchBall(); 
        }
    }

    void LaunchBall()
    {
        if (ballSpawner != null && ballSpawner.CurrentBall != null)
        {
            Rigidbody ballRb = ballSpawner.CurrentBall.GetComponent<Rigidbody>();
            if (ballRb != null)
            {
                Quaternion rotation = Quaternion.Euler(-currentAngleVertical, currentAngleHorizontal, 0);
                Vector3 launchDirection = launchPoint.forward; 
                Vector3 launchVelocity = rotation * launchDirection * currentForce;

                ballRb.isKinematic = false;
                ballRb.useGravity = true;
                ballRb.velocity = launchVelocity;

                Destroy(ballSpawner.CurrentBall, 10f);
                ballSpawner.CurrentBall = null;

                Invoke(nameof(SpawnNewBall), timeToSpawnNextBall);

                ballSpawner.ShootBall();
            }
        }
    }

    void SpawnNewBall()
    {
        ballSpawner.SpawnBall();
    }
}