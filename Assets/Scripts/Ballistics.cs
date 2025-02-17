using UnityEngine;

public class Ballistics : MonoBehaviour
{
    private Rigidbody playerRigidBody;
    public float forceMultiplier = 10f;  
    public float lifetime = 10f;       

    void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        if (playerRigidBody == null)
        {
            Debug.LogError("Rigidbody not found on the ball object!");
        }
        // Gravity is disabled initially.
        playerRigidBody.useGravity = false;
    }

    // Method to launch the ball
    public void Shoot(Vector3 launchVector)
    {
        if (playerRigidBody == null)
        {
            Debug.LogError("Rigidbody is null. Cannot shoot the ball.");
            return;
        }

        playerRigidBody.velocity = Vector3.zero;

        playerRigidBody.AddForce(launchVector * forceMultiplier, ForceMode.Impulse);

        playerRigidBody.useGravity = true;

        Destroy(gameObject, lifetime);
    }
}
