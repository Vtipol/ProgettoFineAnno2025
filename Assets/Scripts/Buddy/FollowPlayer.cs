using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public AIFollowSettings aiSettings;
    public Transform target;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public PickedUp pickedUp;

    [SerializeField] private Transform buddyWallcheck;
    [SerializeField] private Animator _animator;

    private Rigidbody2D rb;
    public bool targetInView;
    private bool isGrounded;
    private bool needJump;
    private bool isTrasformed;

    // Animation flags
    private bool _isWalking;
    private bool _isJumping;
    private bool _isFalling;

    public bool IsTrasformed
    {
        get => isTrasformed;
        set => isTrasformed = value;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        DetectTarget();
        CheckGroundStatus();
        FlipSprite();
        UpdateAnimation();
        UpdateMovementState();

    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, aiSettings.groundCheckRadius, groundLayer);

        if (targetInView && !isTrasformed && !pickedUp.IsPickedUp /*&& isGrounded*/)
        {
            MoveTowardsTarget();
        }
        HandleMovementLogic();
        HandleJumping();
        FastFall();
    }

    void DetectTarget()
    {
        targetInView = Physics2D.OverlapCircle(transform.position, aiSettings.fieldOfViewRadius, aiSettings.targetLayer);
    }

    void CheckGroundStatus()
    {
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, aiSettings.groundCheckRadius, groundLayer);

        if (!wasGrounded && isGrounded && pickedUp.IsPickedUp)
        {
            pickedUp.IsPickedUp = false;
        }
    }
    void FastFall()
    {
        if (!isGrounded && target != null)
        {
            float verticalDifference = transform.position.y - target.position.y;
            if (verticalDifference > 2f)
            {
                rb.linearVelocity += Vector2.down * aiSettings.fastFallSpeed * Time.deltaTime;
            }
        }
    }

    void HandleMovementLogic()
    {
        if (!target || !targetInView) return;

        //float direction = Mathf.Sign(target.position.x - transform.position.x);

        int direction = (target.position.x - transform.position.x) >= 0 ? 1 : -1;
        if (direction == 0) direction = transform.localScale.x > 0 ? 1 : -1;
        bool wallAhead = IsWallAhead(direction);
        RaycastHit2D groundFront = Physics2D.Raycast(transform.position, new Vector2(direction, 0), 2f, aiSettings.groundLayer);
        Debug.DrawRay(transform.position, new Vector2(direction, 0), Color.gray);
        RaycastHit2D gapAhead = Physics2D.Raycast(transform.position + new Vector3(direction, 0, 0), Vector2.down, 2f, aiSettings.groundLayer);
        Debug.DrawRay(transform.position + new Vector3(direction, -1, 0), Vector2.down, Color.yellow);
        RaycastHit2D platformOverhead = Physics2D.Raycast(transform.position, Vector2.up, 2f, aiSettings.groundLayer);
        Debug.DrawRay(transform.position, Vector2.up, Color.red);
        bool isTargetAirborne = Physics2D.Raycast(transform.position, Vector2.up, 3f, 1 << target.gameObject.layer);

        if ((!gapAhead.collider) )
        {
            needJump = true;
        }
        else if (isTargetAirborne && platformOverhead.collider)
        {
            needJump = true;
        }
        else if(wallAhead)
        {
            needJump = true;
        }
        else
        {
            needJump = false;
        }
    }

    void HandleJumping()
    {
        if (isGrounded && needJump && !isTrasformed)
        {
            int direction = (target.position.x - transform.position.x) >= 0 ? 1 : -1;
            bool wallAhead = IsWallAhead(direction);
            if (wallAhead)
            {
                Debug.Log("Performed Wall Jump");
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                rb.AddForce(Vector2.up * aiSettings.wallJumpForce, ForceMode2D.Impulse);
            }
            else
            {
                Debug.Log("Performed Normal Jump");
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                rb.AddForce(Vector2.up * aiSettings.jumpForce, ForceMode2D.Impulse);
            }
            needJump = false;
            _isJumping = true;
        }
    }

    void MoveTowardsTarget()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance < aiSettings.stopDistance) return;

        Vector2 direction = (target.position - transform.position).normalized;
        float runBonus = InputManager.RunIsHeld ? aiSettings.runSpeedBonus : 0f;
        float moveSpeed = aiSettings.speed + runBonus;

        if (distance < aiSettings.decelDistance)
        {
            float t = distance / aiSettings.decelDistance;
            moveSpeed = Mathf.Lerp(0, moveSpeed, t);
        }

        rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y - aiSettings.fallingSpeed * Time.deltaTime);
    }

    public bool IsWallAhead(int direction)
    {
        Vector2 rayDirection = Vector2.right * direction;
        Vector2 origin = buddyWallcheck.position + Vector3.up * 0.1f; 
        float rayLength = 2.5f;
       
        RaycastHit2D hit = Physics2D.Raycast(origin, rayDirection, rayLength, groundLayer);
        Debug.DrawRay(origin, rayDirection * rayLength, Color.blue);
        if (hit.collider)
        {
            Debug.Log("Wall Ahead");
        }
        return hit.collider != null;
    }

    #region Animation

    void UpdateAnimation()
    {
        _animator.SetBool("IsWalking", _isWalking && isGrounded);
        _animator.SetBool("IsRunning", _isWalking && InputManager.RunIsHeld && isGrounded);
        _animator.SetBool("IsJumping", _isJumping);
        _animator.SetBool("IsFalling", _isFalling);
    }

    void FlipSprite()
    {
        if (pickedUp.IsPickedUp) return; 
        float direction;

        if (!targetInView || isTrasformed || pickedUp.IsPickedUp || !isGrounded)
        {
            direction = rb.linearVelocity.x;
        }
        else
        {
            direction = target.position.x - transform.position.x;
        }

        if (Mathf.Abs(direction) > 0.01f)
        {
            Vector3 scale = transform.localScale;
            scale.x = direction > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    void UpdateMovementState()
    {
        _isWalking = Mathf.Abs(rb.linearVelocity.x) > 0.01f && isGrounded;

        if (rb.linearVelocity.y < -0.1f && !isGrounded)
        {
            _isFalling = true;
            _isJumping = false;
        }
        else if (isGrounded)
        {
            _isFalling = false;
            _isJumping = false;
        }
    }

    #endregion
}
