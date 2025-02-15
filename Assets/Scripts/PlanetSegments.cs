using UnityEngine;

public class PlanetSegment : MonoBehaviour
{
    public string requiredColor; // ����, �� ������� ��� �������� ����� ��������
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // �������� �� ������ �� ���������� ����
            if (collision.gameObject.GetComponent<Renderer>().material.color.ToString() == requiredColor)
            {
                Destroy(gameObject); // �������� ��������
            }
        }
    }
}
