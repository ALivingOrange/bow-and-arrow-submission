using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody rb; // Physics engine reference
    public float stabilizationStrength = 7.5f; // How hard the arrow fights to stay straight
    private bool inFlight = false; // A flag to check if we have been fired yet

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // Center of Mass Trick: Real arrows are front-heavy.
        // We shift the balance point forward (Z-axis) to make it fly better.
        rb.centerOfMass = new Vector3(0, 0, 0.5f);
    }

    private void FixedUpdate()
    {
        // Only run physics code if the arrow is actually flying
        if (inFlight) Stabilize();
    }

    private void Stabilize()
    {
        // Optimization: Don't calculate if we are barely moving
        if (rb.velocity.sqrMagnitude < 0.1f) return;

        // Math: Calculate the difference between "Where we are pointing" and "Where we are going"
        Vector3 torque = Vector3.Cross(transform.forward, rb.velocity.normalized);
       
        // Apply rotational force to correct the angle
        rb.AddTorque(torque * stabilizationStrength);
    }

    public void Fire(float force)
    {
        transform.parent = null; // Detach from the bow string
        rb.isKinematic = false;  // Turn on physics simulation
        rb.useGravity = true;    // Enable gravity
       
        // Push the arrow forward with an Impulse (sudden force)
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
       
        inFlight = true; // Start the stabilization loop
        Destroy(gameObject, 10f); // Clean up: delete arrow after 10 seconds
    }
}
