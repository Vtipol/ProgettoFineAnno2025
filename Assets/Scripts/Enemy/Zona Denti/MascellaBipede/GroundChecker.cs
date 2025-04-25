using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] public Transform mascellaGroundCheck;
    public float checkDistance = 1f;
    public LayerMask groundLayer;

    public bool IsGroundAhead()
    {
        RaycastHit2D hit = Physics2D.Raycast(mascellaGroundCheck.position, Vector2.down, checkDistance, groundLayer);
        Debug.DrawRay(mascellaGroundCheck.position, Vector2.down * checkDistance, Color.green);
        Debug.Log("Ground ahead: " + (hit.collider != null));
        return hit.collider != null;
    }
}
