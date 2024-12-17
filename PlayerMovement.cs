using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f; // Movement speed

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input from keyboard (Horizontal and Vertical axes)
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow keys
        float moveVertical = Input.GetAxis("Vertical"); // W/S or Up/Down arrow keys

        // Create a movement vector based on input
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        // Apply movement to the Rigidbody
        rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, movement.z * speed);

        // Optional: Lock the sphere's rotation to prevent it from rotating while moving
        rb.freezeRotation = true;
    }
}
