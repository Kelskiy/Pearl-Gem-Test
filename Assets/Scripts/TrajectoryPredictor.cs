using UnityEngine;

public class TrajectoryPredictor : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;

    public int numPoints = 30; 

    public float timeStep = 0.1f;  

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogWarning("LineRenderer not found, adding it automatically...");
            lineRenderer = gameObject.AddComponent<LineRenderer>();

            lineRenderer.useWorldSpace = true;
            lineRenderer.startWidth = 0.05f;
            lineRenderer.endWidth = 0.05f;
            lineRenderer.positionCount = numPoints;
        }
    }

    public void PredictTrajectory(Vector3 startPos, Vector3 initialVelocity)
    {
        if (lineRenderer == null)
            return;

        lineRenderer.positionCount = numPoints;
        for (int i = 0; i < numPoints; i++)
        {
            float t = i * timeStep;
            // Calculate the position of the ball at time t
            Vector3 pos = startPos + initialVelocity * t + 0.5f * Physics.gravity * t * t;
            lineRenderer.SetPosition(i, pos);
        }
    }

    public void ClearTrajectory()
    {
        if (lineRenderer != null)
            lineRenderer.positionCount = 0;
    }
}
