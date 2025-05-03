using UnityEngine;
public class FollowPlayer : MonoBehaviour
{
    public AIFollowSettings aiSettings;
    public Transform target;
    public Transform groundCheck;
    public PickedUp pickedUp;
    [SerializeField] private Animator _animator;

    private Rigidbody2D rb;
    private bool isGrounded;
    public bool targetInView;
    private bool needJump;
    private bool isTrasformed;

    // animation var
    private bool _isWalking;
    //private bool _isRunning;
    private bool _isJumping;
    private bool _isFalling;

    public bool IsTrasformed
    {
        get { return isTrasformed; }
        set { isTrasformed = value; }
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
        HandleMovementLogic();
        FlipSprite();
        UpdateAnimation();
        JumpFall();
        WalkRunStop();
    }
    void FixedUpdate()
    {
        if (targetInView && !isTrasformed && !pickedUp.IsPickedUp && isGrounded)
        {
            MoveTowardsTarget();
        }
    }
    void DetectTarget()
    {
        targetInView = Physics2D.OverlapCircle(transform.position, aiSettings.fieldOfViewRadius, aiSettings.targetLayer) != null;
    }
    void CheckGroundStatus()
    {
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, aiSettings.groundCheckRadius, aiSettings.groundLayer);
        if (!wasGrounded && isGrounded && pickedUp.IsPickedUp)
        {
            pickedUp.IsPickedUp = false;
        }
    }
    void HandleMovementLogic()
    {
        if (!target || !targetInView) return;

        float direction = Mathf.Sign(target.position.x - transform.position.x);
        bool isTargetAirborne = Physics2D.Raycast(transform.position, Vector2.up, 3f, 1 << target.gameObject.layer);

        RaycastHit2D groundFront = Physics2D.Raycast(transform.position, new Vector2(direction, 0), 2f, aiSettings.groundLayer);
        RaycastHit2D gapAhead = Physics2D.Raycast(transform.position + new Vector3(direction, 0, 0), Vector2.down, 2f, aiSettings.groundLayer);
        RaycastHit2D platformOverhead = Physics2D.Raycast(transform.position, Vector2.up, 2f, aiSettings.groundLayer);

        if (!groundFront.collider && !gapAhead.collider)
        {
            needJump = true;
        }
        else if (isTargetAirborne && platformOverhead.collider)
        {
            needJump = true;
        }
        else
        {
            needJump = false;
        }
    }
    void MoveTowardsTarget()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance < aiSettings.stopDistance) return;
        if (isGrounded && target.position.y > transform.position.y + 4f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, aiSettings.jumpForce);
        }
        Vector2 direction = (target.position - transform.position).normalized;
        float runBonus = InputManager.RunIsHeld ? aiSettings.runSpeedBonus : 0f;
        float moveSpeed = aiSettings.speed + runBonus;
        if (distance < aiSettings.decelDistance)
        {
            float t = distance / aiSettings.decelDistance; 
            moveSpeed = Mathf.Lerp(0, moveSpeed, t);
        }
        rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y - aiSettings.fallingSpeed * Time.deltaTime);
        if (isGrounded && needJump)
        {
            needJump = false;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, aiSettings.jumpForce);
        }
      //  if (isGrounded && target.position.y > transform.position.y - 2f)
       // {
           // StartCoroutine(BuddyDropThroughPlatform());
       // }
    }
    /*
    IEnumerator BuddyDropThroughPlatform()
    {
        Collider2D buddyCollider = GetComponent<CircleCollider2D>();
        buddyCollider.enabled = false;
        yield return new WaitForSeconds(1f);
        buddyCollider.enabled = true;
    }*/

    #region Animation

    private void UpdateAnimation()
    {
        bool walking = _isWalking && isGrounded;
        _animator.SetBool("IsWalking", walking);
        bool running = InputManager.RunIsHeld && isGrounded;
        _animator.SetBool("IsRunning", walking && running);
        bool jumping = _isJumping && !isGrounded;
        _animator.SetBool("IsJumping", jumping);
        bool falling = !_isJumping && !isGrounded;
        _animator.SetBool("IsFalling", falling);
    }
    private void FlipSprite()
    {
        if (!target) return;

        float direction = target.position.x - transform.position.x;

        if (Mathf.Abs(direction) > 0.01f)
        {
            Vector3 scale = transform.localScale;
            scale.x = direction > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }
    private void JumpFall()
    {
        if (rb.linearVelocity.y < 0.1f && !isGrounded)
        {
            _isJumping = true;
        }
        if (_isJumping && isGrounded)
        {
            _isJumping = false; 
        }
        if (!_isJumping && !isGrounded)
        {
            _isFalling = true;
        }
        else if (!_isJumping && isGrounded)
        {
            _isFalling = false;
        }
    }

    private void WalkRunStop()
    {
        if (rb.linearVelocity.magnitude < 0.01f && isGrounded)
        {
            _isWalking = false;
        }
        else if (rb.linearVelocity.magnitude > 0.01f && isGrounded)
        {
            _isWalking = true;
        }
    }

    #endregion
}
