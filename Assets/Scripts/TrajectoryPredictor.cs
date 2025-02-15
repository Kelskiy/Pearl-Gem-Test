using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class TrajectoryPredictor : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public int numPoints = 30;     // ʳ������ ����� ��� �������
    public float timeStep = 0.1f;    // �������� ���� ��� ���������� �������

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // ���������� ������������ �������� � ����������� ���������
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
