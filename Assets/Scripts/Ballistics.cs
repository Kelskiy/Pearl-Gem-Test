using UnityEngine;

public class Ballistics : MonoBehaviour
{
    private Rigidbody rb;
    public float forceMultiplier = 10f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            Debug.LogError("Rigidbody not found on the ball prefab!");
        else
            rb.useGravity = false; // ¬имикаЇмо грав≥тац≥ю до постр≥лу
    }

    public void Shoot(Vector3 launchVector)
    {
        if (rb == null)
        {
            Debug.LogError("Rigidbody is null. Cannot shoot the ball.");
            return;
        }
        rb.velocity = Vector3.zero;
        rb.AddForce(launchVector, ForceMode.Impulse);
        rb.useGravity = true; // ¬микаЇмо грав≥тац≥ю п≥сл€ постр≥лу
    }
}
