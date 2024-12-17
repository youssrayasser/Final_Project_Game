using UnityEngine;

public class CarGame : MonoBehaviour
{
    public float speed = 10f;           // Speed of the car
    public float turnSpeed = 50f;       // Turning speed of the car

    private Rigidbody carRigidbody;

    void Start()
    {
        // Get the Rigidbody component attached to the car
        carRigidbody = GetComponent<Rigidbody>();
        if (carRigidbody == null)
        {
            Debug.LogError("No Rigidbody attached to the car!");
        }
    }

    void FixedUpdate()
    {
        // Always move the car forward, even without input
        Vector3 forwardMovement = transform.forward * speed * Time.fixedDeltaTime;
        carRigidbody.MovePosition(carRigidbody.position + forwardMovement);

        // Debug: Print current position to see if it's updating
        Debug.Log("Car Position: " + carRigidbody.position);

        // Get input for turning (left or right)
        float turnInput = Input.GetAxis("Horizontal");  // Left/Right turning (arrow keys or A/D)

        // Turn the car left or right based on input
        float turnAmount = turnInput * turnSpeed * Time.fixedDeltaTime;
        carRigidbody.MoveRotation(carRigidbody.rotation * Quaternion.Euler(0f, turnAmount, 0f));

        // Debug: Print current rotation to see if it's updating
        Debug.Log("Car Rotation: " + carRigidbody.rotation.eulerAngles);


        
    }
}
