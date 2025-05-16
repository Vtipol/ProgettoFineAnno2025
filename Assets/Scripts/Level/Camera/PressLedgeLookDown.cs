using UnityEngine;
using Unity.Cinemachine;

public class PressLedgeLookDown : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Collider2D feetCollider;
    public LayerMask groundLayer;
    public CinemachineCamera cinemachineCamera;

    [Header("Settings")]
    public float forwardOffset = 1f;          // How far ahead to check for ledge
    public float downwardCheckDistance = 2f;  // How far down to check for ground
    public float ledgeLookDownOffset = -2f;   // Camera Y offset when ledge detected
    public float smoothSpeed = 5f;

    private float direction = 1f;  // Player facing direction: 1 = right, -1 = left
    private Vector3 originalOffset;
    private Vector3 targetOffset;
    private CinemachineFollow cinemachineFollow;

    void Start()
    {
        if (cinemachineCamera == null)
        {
            Debug.LogError("CinemachineCamera reference not assigned.");
            enabled = false;
            return;
        }

        // Get the CinemachineFollow component
        cinemachineFollow = cinemachineCamera.GetComponent<CinemachineFollow>();
        if (cinemachineFollow == null)
        {
            Debug.LogError("CinemachineFollow component not found on CinemachineCamera.");
            enabled = false;
            return;
        }

        originalOffset = cinemachineFollow.FollowOffset;
        targetOffset = originalOffset;
    }

    void Update()
    {
        if (player == null || feetCollider == null) return;

        UpdateDirection();

        Vector2 castOrigin = new Vector2(
            feetCollider.bounds.center.x + direction * forwardOffset,
            feetCollider.bounds.min.y
        );

        Vector2 castSize = new Vector2(feetCollider.bounds.size.x, 0.1f);
        float castDistance = downwardCheckDistance;

        RaycastHit2D hit = Physics2D.BoxCast(castOrigin, castSize, 0f, Vector2.down, castDistance, groundLayer);

        // Check if the player is pressing DownInput
        bool isPressingDown = InputManager.DownisHeld;

        // If no ground detected ahead and player is pressing down, move camera down
        float desiredYOffset = (hit.collider == null && isPressingDown) ? ledgeLookDownOffset : originalOffset.y;

        Vector3 currentOffset = cinemachineFollow.FollowOffset;
        targetOffset = new Vector3(originalOffset.x, desiredYOffset, originalOffset.z);

        // Smoothly lerp to the target offset
        cinemachineFollow.FollowOffset = Vector3.Lerp(currentOffset, targetOffset, Time.deltaTime * smoothSpeed);
    }

    void UpdateDirection()
    {
        float horizontal = InputManager.Movement.x;
        if (horizontal != 0)
            direction = Mathf.Sign(horizontal);
    }

    // Optional: visualize the BoxCast in editor
    void OnDrawGizmosSelected()
    {
        if (feetCollider == null) return;

        float dir = Application.isPlaying ? direction : 1f;

        Vector2 castOrigin = new Vector2(
            feetCollider.bounds.center.x + dir * forwardOffset,
            feetCollider.bounds.min.y
        );

        Vector2 castSize = new Vector2(feetCollider.bounds.size.x, 0.1f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(castOrigin + Vector2.down * (downwardCheckDistance / 2f), new Vector2(castSize.x, downwardCheckDistance));
    }
}
