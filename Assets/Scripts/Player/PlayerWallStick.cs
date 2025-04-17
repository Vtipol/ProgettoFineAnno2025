using UnityEngine;

public class PlayerWallStick : MonoBehaviour
{
    [Header("Wall Stick Settings")]
    [SerializeField] private float wallCheckRadius = 0.1f;
    [SerializeField] private Transform wallCheckPoint;
    [SerializeField] private LayerMask wallLayer;

    [Header("Jump Settings")]
    [SerializeField] private float wallJumpHorizontalForce = 5f;
    [SerializeField] private float initialJumpVelocity = 10f;

    private bool isTouchingWall = false;
    private bool isWallSticking = false;
    private float wallStickCounter = 0f;
    private Rigidbody2D _rb;

    private Player Player;

    public bool CanWallStick { get; private set; } = false;

    private void Awake()
    {
        Player = GetComponent<Player>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (CanWallStick)
        {
            CheckWallContact();
            HandleWallStick();
        }
    }

    private void CheckWallContact()
    {
        isTouchingWall = Physics2D.OverlapCircle(wallCheckPoint.position, wallCheckRadius, wallLayer);
    }

    private void HandleWallStick()
    {
        if (isTouchingWall && !Player._isGrounded && _rb.linearVelocity.y < 0f)
        {
            isWallSticking = true;
            wallStickCounter = 0.2f;  // Set your own stick duration time

            // Stick to the wall by canceling gravity and setting velocity to 0
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);
        }

        if (isWallSticking)
        {
            wallStickCounter -= Time.deltaTime;

            if (wallStickCounter <= 0f || Player._isGrounded)
            {
                isWallSticking = false;
            }

            // Wall Jump
            if (InputManager.JumpWasPressed)
            {
                int direction = Player._isFacingRight ? -1 : 1;  // Jump in the opposite direction
                WallJump(direction);
            }
        }
    }

    private void WallJump(int direction)
    {
        isWallSticking = false;
        _rb.linearVelocity = new Vector2(direction * wallJumpHorizontalForce, initialJumpVelocity);
    }

    // Methods to be called by the Gum script
    public void EnableWallStick()
    {
        CanWallStick = true;
    }

    public void DisableWallStick()
    {
        CanWallStick = false;
    }
}

