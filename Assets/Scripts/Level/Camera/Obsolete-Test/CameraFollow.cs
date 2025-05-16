using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;  // Assign the player GameObject here
    public Vector3 offset = new Vector3(0f, 2f, -10f); // Adjust as needed
    public float smoothSpeed = 5f; // Speed of the camera follow

    // Optional: Camera Bounds (Set to false if not needed)
    public bool useBounds = false;
    public Vector2 minLimits = new Vector2(-10f, -5f);
    public Vector2 maxLimits = new Vector2(10f, 5f);

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("CameraFollow2D: No target assigned!");
            return;
        }

        // Target position with offset
        Vector3 desiredPosition = target.position + offset;

        // Apply camera bounds if enabled
        if (useBounds)
        {
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minLimits.x, maxLimits.x);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minLimits.y, maxLimits.y);
        }

        // Smooth camera movement
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}

