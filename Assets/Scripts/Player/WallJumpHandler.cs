using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WallJumpHandler : MonoBehaviour
{
    public float wallJumpForceX = 8f;
    public float wallJumpForceY = 12f;
    public float wallSlideSpeed = 1f;
    public LayerMask wallLayer;
    public int maxWallJumps = 3;

    public Transform wallCheckPoint;
    public float wallCheckDistance = 0.3f;

    private Rigidbody2D rb;
    private bool isTouchingWall;
    private int wallDirection; // -1 for left, 1 for right
    private bool wallJumpEnabled;
    private int remainingWallJumps;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!wallJumpEnabled) return;

        CheckWall();

        if (isTouchingWall && Input.GetAxisRaw("Horizontal") == wallDirection)
        {
            if (rb.linearVelocity.y < 0)
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallSlideSpeed);
        }

        if (Input.GetButtonDown("Jump") && isTouchingWall && remainingWallJumps > 0)
        {
            WallJump();
        }
    }

    private void CheckWall()
    {
        RaycastHit2D hitLeft = Physics2D.Raycast(wallCheckPoint.position, Vector2.left, wallCheckDistance, wallLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(wallCheckPoint.position, Vector2.right, wallCheckDistance, wallLayer);

        if (hitLeft)
        {
            isTouchingWall = true;
            wallDirection = -1;
            Debug.Log("Touching wall on the LEFT");
        }
        else if (hitRight)
        {
            isTouchingWall = true;
            wallDirection = 1;
            Debug.Log("Touching wall on the RIGHT");
        }
        else
        {
            isTouchingWall = false;
            wallDirection = 0;
        }
    }

    private void WallJump()
    {
        Vector2 jumpForce = new Vector2(-wallDirection * wallJumpForceX, wallJumpForceY);
        rb.linearVelocity = jumpForce;
        remainingWallJumps--;
        Debug.Log("Wall Jump! Remaining: " + remainingWallJumps);
    }

    public void EnableWallJump(int maxJumps)
    {
        wallJumpEnabled = true;
        remainingWallJumps = maxJumps;
        Debug.Log("Wall Jump ENABLED with " + maxJumps + " jumps.");
    }

    public void DisableWallJump()
    {
        wallJumpEnabled = false;
        remainingWallJumps = 0;
        Debug.Log("Wall Jump DISABLED.");
    }

    public void ResetWallJumps()
    {
        remainingWallJumps = maxWallJumps;
        Debug.Log("Wall Jumps RESET to " + maxWallJumps);
    }
}
