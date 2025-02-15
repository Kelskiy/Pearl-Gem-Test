using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class TrajectoryPredictor : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public int numPoints = 30;     // Кількість точок для траєкторії
    public float timeStep = 0.1f;    // Інтервал часу для розрахунку траєкторії

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Обчислюємо прогнозовану траєкторію з урахуванням гравітації
    public void PredictTrajectory(Vector3 startPos, Vector3 initialVelocity)
    {
        lineRenderer.positionCount = numPoints;
        for (int i = 0; i < numPoints; i++)
        {
            float t = i * timeStep;
            Vector3 position = startPos + initialVelocity * t + 0.5f * Physics.gravity * t * t;
            lineRenderer.SetPosition(i, position);
        }
    }

    public void ClearTrajectory()
    {
        lineRenderer.positionCount = 0;
    }
}
