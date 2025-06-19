using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    public AIFollowSettings aiSettings;
    public Transform target;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public PickedUp pickedUp;
    [SerializeField] private Transform buddyWallcheck;
    [SerializeField] private Animator _animator;
    private BuddyStateController controller;
    private PickThrow pickThrow;
    private AvoidWallIssuesBuddy wallFailSafe;
    private Rigidbody2D rb;
    public bool startFollow = false;
    public bool targetInView;
    public AudioSource audioSource;
    public AudioClip trampClip;
    public AudioClip miaoClip;
    private bool isGrounded;
    private bool needJump;
    private bool isTrasformed;
    private int directionToTarget;
    //private int originalLayer;
    //private int pickedUpLayer;
    //private int currentLayer;

    // Animation flags
    public bool _isWalking;
    public bool _isJumping;
    public bool _isFalling;

    public bool IsTrasformed
    {
        get => isTrasformed;
        set => isTrasformed = value;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<BuddyStateController>();
        _animator = GetComponent<Animator>();
        pickThrow = FindAnyObjectByType<PickThrow>();
        wallFailSafe = GetComponentInChildren<AvoidWallIssuesBuddy>();
        //pickedUpLayer = LayerMask.NameToLayer("PickUp");
        //originalLayer = gameObject.layer;
    }

    void Update()
    {
        DetectTarget();
        UpdateAnimation();
        UpdateMovementState();
        CheckGroundStatus();
        TeleportInput();
    }
    

    void FixedUpdate()
    {
        directionToTarget = (target.position.x - transform.position.x) >= 0 ? 1 : -1;
        if (directionToTarget == 0) directionToTarget = transform.localScale.x > 0 ? 1 : -1;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, aiSettings.groundCheckRadius, groundLayer);

        if (targetInView && !isTrasformed && !pickedUp.IsPickedUp && !pickThrow.isPickThrow)
        {
            MoveTowardsTarget();
        }
        else startFollow = false;
        //int targetLayer = (pickedUp != null && pickedUp.IsPickedUp) ? pickedUpLayer : originalLayer;

        //if (currentLayer != targetLayer)
        //{
        //    SetLayerRecursively(gameObject, targetLayer);
        //    currentLayer = targetLayer;
        //}
        HandleMovementLogic();
        HandleJumping();
        FastFall();
        FlipSprite();
    }


    void DetectTarget()
    {
        targetInView = Physics2D.OverlapCircle(transform.position, aiSettings.fieldOfViewRadius, aiSettings.targetLayer);
    }

    void CheckGroundStatus()
    {
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, aiSettings.groundCheckRadius, groundLayer);

        if (!wasGrounded && isGrounded && pickedUp.IsPickedUp && !wallFailSafe.FailSafe)
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
  /*
    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (obj == null) return;

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (child != null)
            {
                SetLayerRecursively(child.gameObject, newLayer);
            }
        }
    }
    */
    void HandleMovementLogic()
    {
        if (!target || !targetInView) return;

        //float direction = Mathf.Sign(target.position.x - transform.position.x);

        if (directionToTarget == 0) directionToTarget = transform.localScale.x > 0 ? 1 : -1;
        bool wallAhead = IsWallAhead(directionToTarget);
        /*RaycastHit2D groundFront = Physics2D.Raycast(transform.position, new Vector2(direction, 0), 2f, aiSettings.groundLayer);
        Debug.DrawRay(transform.position, new Vector2(direction, 0), Color.gray);*/
        RaycastHit2D gapAhead = Physics2D.Raycast(transform.position + new Vector3(directionToTarget - 1, 0, 0), Vector2.down, 2f, aiSettings.groundLayer);
        Debug.DrawRay(transform.position + new Vector3(directionToTarget, -1, 0), Vector2.down, Color.yellow);
        RaycastHit2D platformOverhead = Physics2D.Raycast(transform.position, Vector2.up, 2f, aiSettings.groundLayer);
        Debug.DrawRay(transform.position, Vector2.up, Color.red);
        bool isTargetAirborne = Physics2D.Raycast(transform.position, Vector2.up, 3f, 1 << target.gameObject.layer);

        if ((!gapAhead.collider))
        {
            needJump = true;
        }
        else if (isTargetAirborne && platformOverhead.collider)
        {
            needJump = true;
        }
        else if (wallAhead && (target.position.y - transform.position.y > 1f))
        {
            needJump = true;
        }
        else
        {
            needJump = false;
        }
    }

    private void TeleportInput()
    {
        if (InputManager.TeleportWasPressed)
        {
            Vector3 offset = new Vector3(0f, 0f, 0f);
            controller.transform.position = target.position + offset;
            controller.SwitchState(controller.neutralState); 
            controller.EnableOnlyCollider(null);
            Rigidbody2D rb = controller.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }

            _animator.SetTrigger("Teleport");
        }
    }
    void HandleJumping()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        if (isGrounded && needJump && !isTrasformed && distance > aiSettings.stopDistance)
        {

            bool wallAhead = IsWallAhead(directionToTarget);
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
        if (!startFollow) StartCoroutine(AwaitFollow());
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

        if (Random.Range(0, 600) == 1 && miaoClip != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(miaoClip);
        }
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
    private IEnumerator AwaitFollow()
    {
        yield return new WaitForSeconds(0.1f);
        startFollow = true;
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
        // Se il buddy è raccolto, sincronizza la direzione con quella del player
        if (pickedUp.IsPickedUp)
        {
            // Controlla la rotazione del player per determinare la direzione
            if (Mathf.Approximately(target.eulerAngles.y, 180f))
            {
                // Ruota il buddy verso sinistra
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
               // Debug.Log("Direzione sincronizzata con il player: Sinistra");
            }
            else if (Mathf.Approximately(target.eulerAngles.y, 0f))
            {
                // Ruota il buddy verso destra
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
              //  Debug.Log("Direzione sincronizzata con il player: Destra");
            }
            return;
        }

        float direction;


        // Logica per il movimento normale
        if (!targetInView || isTrasformed || !isGrounded)
        {
            direction = rb.linearVelocity.x;
        }
        else
        {
            direction = target.position.x - transform.position.x;
        }
        if (!pickThrow.isPickThrow)
        {
            if (direction > 0 && !isTrasformed)
            {
                // Ruota il buddy verso destra
                transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                Debug.Log("Direzione aggiornata: Destra");
            }
            else
            {
                // Ruota il buddy verso sinistra
                transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                Debug.Log("Direzione aggiornata: Sinistra");
            }
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