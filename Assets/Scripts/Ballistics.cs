using UnityEngine;

public class Ballistics : MonoBehaviour
{
    private Rigidbody rb;
    public float forceMultiplier = 10f;  
    public float lifetime = 10f;       

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on the ball object!");
        }
        // Gravity is disabled initially.
        rb.useGravity = false;
    }

    // Method to launch the ball
    public void Shoot(Vector3 launchVector)
    {
        if (rb == null)
        {
            Debug.LogError("Rigidbody is null. Cannot shoot the ball.");
            return;
        }

        rb.velocity = Vector3.zero;

        rb.AddForce(launchVector * forceMultiplier, ForceMode.Impulse);

        rb.useGravity = true;

        Destroy(gameObject, lifetime);
    }
}
