using UnityEngine;

public class GameController : MonoBehaviour
{
    private Vector3 dragStartPos;
    private Vector3 dragCurrentPos;
    private bool isDragging = false;
    private LineRenderer lineRenderer;
    private BallSpawner ballSpawner;

    public float maxDragDistance = 5f;  // ����������� ������� ������
    public float launchForceMultiplier = 1f;  // ������� ��� ������������ ������� ������ � ����

    void Start()
    {
        // �������������, �� ��� ��'��� �� LineRenderer
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer �� �������� �� GameController!");
        }
        else
        {
            lineRenderer.positionCount = 2;
            lineRenderer.enabled = false;
        }

        ballSpawner = BallSpawner.Instance;
        if (ballSpawner == null)
        {
            Debug.LogError("BallSpawner �� �������� �� ����!");
        }
    }

    // �������� ����� �������� �������� ������� � �������������� �������� (�� spawnPoint)
    Vector3 GetMouseWorldPos()
    {
        Plane plane = new Plane(Vector3.up, ballSpawner.spawnPoint.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter = 0;
        if (plane.Raycast(ray, out enter))
        {
            return ray.GetPoint(enter);
        }
        return Vector3.zero;
    }

    void Update()
    {
        // ������� �����������
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            dragStartPos = GetMouseWorldPos();
            lineRenderer.enabled = true;
        }

        // ϳ� ��� �����������
        if (isDragging)
        {
            dragCurrentPos = GetMouseWorldPos();
            Vector3 dragVector = dragStartPos - dragCurrentPos;
            // �������� ���� ������
            if (dragVector.magnitude > maxDragDistance)
                dragVector = dragVector.normalized * maxDragDistance;
            UpdateTrajectory(dragStartPos, dragStartPos - dragVector);
        }

        // ³��������� ���� � ������ ������
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            Vector3 dragVector = dragStartPos - dragCurrentPos;
            if (dragVector.magnitude > maxDragDistance)
                dragVector = dragVector.normalized * maxDragDistance;
            lineRenderer.enabled = false;

            // �������� �������: ���� �� ��������� �������, ������ �� ������� � ������������ �������� �� ������� ������� ���� �� �������� �����
            Vector3 launchVector = dragVector * launchForceMultiplier;
            // �������� ������ ����� BallSpawner, ��������� launchVector
            ballSpawner.SpawnBall(launchVector);
        }
    }

    void UpdateTrajectory(Vector3 start, Vector3 end)
    {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}
