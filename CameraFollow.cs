using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public Vector3 offset;    // Offset distance between the player and camera
    public float smoothSpeed = 0.125f;  // Smoothing speed

    void Start()
    {
        // Set the initial offset (can be adjusted in the Inspector)
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Calculate the desired position based on the player's position and the offset
        Vector3 desiredPosition = player.position + offset;
        
        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        // Update the camera's position
        transform.position = smoothedPosition;

        // Optionally, make sure the camera is always facing the player
        transform.LookAt(player);
    }
}
