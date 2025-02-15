using UnityEngine;

public class PlanetSegment : MonoBehaviour
{
    public string requiredColor; // Колір, що потрібен для знищення цього сегмента
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Перевірка чи кулька має правильний колір
            if (collision.gameObject.GetComponent<Renderer>().material.color.ToString() == requiredColor)
            {
                Destroy(gameObject); // Знищення сегмента
            }
        }
    }
}
