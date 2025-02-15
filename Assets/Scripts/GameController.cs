using UnityEngine;

public class GameController : MonoBehaviour
{
    private Vector3 dragStartPos;
    private Vector3 dragCurrentPos;
    private bool isDragging = false;
    private LineRenderer lineRenderer;
    private BallSpawner ballSpawner;

    public float maxDragDistance = 5f;  // Максимальна відстань натягу
    public float launchForceMultiplier = 1f;  // Множник для перетворення вектора натягу в силу

    void Start()
    {
        // Переконайтеся, що цей об'єкт має LineRenderer
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer не знайдено на GameController!");
        }
        else
        {
            lineRenderer.positionCount = 2;
            lineRenderer.enabled = false;
        }

        ballSpawner = BallSpawner.Instance;
        if (ballSpawner == null)
        {
            Debug.LogError("BallSpawner не знайдено на сцені!");
        }
    }

    // Отримуємо точку перетину мишиного променя з горизонтальною площиною (де spawnPoint)
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
        // Початок натягування
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            dragStartPos = GetMouseWorldPos();
            lineRenderer.enabled = true;
        }

        // Під час натягування
        if (isDragging)
        {
            dragCurrentPos = GetMouseWorldPos();
            Vector3 dragVector = dragStartPos - dragCurrentPos;
            // Обмежуємо силу натягу
            if (dragVector.magnitude > maxDragDistance)
                dragVector = dragVector.normalized * maxDragDistance;
            UpdateTrajectory(dragStartPos, dragStartPos - dragVector);
        }

        // Відпускання миші – запуск кульки
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            Vector3 dragVector = dragStartPos - dragCurrentPos;
            if (dragVector.magnitude > maxDragDistance)
                dragVector = dragVector.normalized * maxDragDistance;
            lineRenderer.enabled = false;

            // Напрямок запуску: якщо ви натягнули рогатку, кулька має полетіти у протилежному напрямку від поточної позиції миші до стартової точки
            Vector3 launchVector = dragVector * launchForceMultiplier;
            // Спавнимо кульку через BallSpawner, передаючи launchVector
            ballSpawner.SpawnBall(launchVector);
        }
    }

    void UpdateTrajectory(Vector3 start, Vector3 end)
    {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}
