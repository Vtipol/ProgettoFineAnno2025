using Unity.Cinemachine;
using UnityEngine;

public class CameraLookDown : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Collider2D feetCollider;
    public LayerMask groundLayer;

    [Header("Var")]
    public float forwardOffset = 1f;
    public float downwardCheckDistance = 2f;
    public float ledgeLookDownOffset = -2f;
    public float smoothSpeed = 5f;

    private float currentYOffset = 0f;
    private float direction = 1f; // 1 = right, -1 = left
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (player == null || feetCollider == null) return;

        UpdateDirection(); // Detect which way player is facing

        // Cast ahead and down from player's feet
        Vector2 castOrigin = new Vector2(
            feetCollider.bounds.center.x + direction * forwardOffset,
            feetCollider.bounds.min.y
        );

        Vector2 castSize = new Vector2(feetCollider.bounds.size.x, 0.1f); // Thin horizontal slice
        float castDistance = downwardCheckDistance;

        RaycastHit2D hit = Physics2D.BoxCast(castOrigin, castSize, 0f, Vector2.down, castDistance, groundLayer);

        float targetYOffset = (hit.collider == null) ? ledgeLookDownOffset : 0f;

        currentYOffset = Mathf.Lerp(currentYOffset, targetYOffset, Time.deltaTime * smoothSpeed);

        Vector3 targetPosition = new Vector3(player.position.x, player.position.y + currentYOffset, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.05f);
    }

    void UpdateDirection()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal != 0)
            direction = Mathf.Sign(horizontal);
    }

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
