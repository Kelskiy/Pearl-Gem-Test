using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    public GameObject ballPrefab; // Префаб кульки
    public float radius = 5f;  // Радіус сфери
    public int totalBalls = 500;  // Загальна кількість кульок
    public float ballScaleFactor = 1.2f; // Масштаб кульки

    void Start()
    {
        GeneratePlanet();
    }

    void GeneratePlanet()
    {
        // Це дозволить рівномірно заповнити сферу, використовуючи сферичні координати
        float phiStep = Mathf.PI * (3f - Mathf.Sqrt(5f)); // Крок для рівномірного розподілу кульок на сфері

        for (int i = 0; i < totalBalls; i++)
        {
            // Використовуємо сферичні координати для рівномірного розподілу кульок
            float y = 1 - (i / (float)(totalBalls - 1)) * 2; // Коефіцієнт для вертикальної координати
            float radiusAtY = Mathf.Sqrt(1 - y * y); // Радіус на певній висоті

            float x = radiusAtY * Mathf.Cos(i * phiStep); // X координата
            float z = radiusAtY * Mathf.Sin(i * phiStep); // Z координата

            // Створюємо кульку на розрахованій позиції
            Vector3 position = new Vector3(x, y, z) * radius + transform.position;
            GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
            ball.transform.parent = transform;
            ball.transform.localScale = Vector3.one * ballScaleFactor; // Масштаб кульки
        }
    }
}
