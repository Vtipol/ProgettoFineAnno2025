using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] public Transform mascellaGroundCheck;
    [SerializeField] public Transform mascellaWallCheck;
    public float checkDistance = 1f;
    public LayerMask groundLayer;

    public bool IsGroundAhead()
    {
        RaycastHit2D hit = Physics2D.Raycast(mascellaGroundCheck.position, Vector2.down, checkDistance, groundLayer);
        Debug.DrawRay(mascellaGroundCheck.position, Vector2.down * checkDistance, Color.green);
        return hit.collider != null;
    }

    public bool IsWallAhead(int direction)
    {
        Vector2 rayDirection = Vector2.right * direction;
        RaycastHit2D hit = Physics2D.Raycast(mascellaWallCheck.position, rayDirection, checkDistance, groundLayer);
        Debug.DrawRay(mascellaWallCheck.position, rayDirection * checkDistance, Color.blue);
        return hit.collider != null;
    }
}
