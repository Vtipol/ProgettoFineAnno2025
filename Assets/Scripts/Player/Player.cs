using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UIElements.UxmlAttributeDescription;

[RequireComponent(typeof(Rigidbody2D), typeof(Damageble))]
public class Player : MonoBehaviour
{
    [Header("References")]
    public PlayerStats Stats;
    public Damageble Damage;
    //public Attacking Attacking;
    [SerializeField] private Collider2D _feetColl;
    [SerializeField] private Collider2D _bodyColl;
    [SerializeField] private Collider2D _attackColl;
    [SerializeField] private Collider2D _downAttackColl;
    [SerializeField] private Animator _animator;

    private Rigidbody2D _rb;

    //movement vars
    private Vector2 _moveVelocity;
    public bool _isFacingRight;

    //collision check vars
    private RaycastHit2D _groundHit;
    private RaycastHit2D _headHit;
    public bool _isGrounded;
    private bool _bumpedHead;

    //jump vard
    public float VerticalVelocity { get; private set; }
    public bool _isJumping;
    private bool _isFastFalling;
    private bool _isFalling;
    public bool IsFalling => _isFalling;
    private float _fastFallTime;
    private float _fastFallReleaseSpeed;
    public int _numberOfJumpsUsed;

    //apex vars
    private float _apexPoint;
    private float _timePastApexThreshold;
    private bool _isPastApexThreshold;

    //jump buffer vars
    private float _jumpBufferTimer;
    private bool _jumpReleasedDuringBuffer;

    //coyote time vars
    private float _coyoteTimer;

    //jump cut vars
    private float _jumpDuration;
    private bool _canCutJump = false;

    // wall check <-new
    private bool _isTouchingWall;
    private bool _isWallSticking;
    private bool _canWallJump;
    private RaycastHit2D _wallHit;

    // attack vars
    private float _attackDuration = 1f;
    private bool _isAttacking;
    private bool _lockMovement;

    private void Awake()
    {
        _isFacingRight = true;
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        Damage = GetComponent<Damageble>();
        //Attacking = GetComponent<Attacking>();

        Damage.damagebleHit.AddListener(OnHit);
    }

    private void Update()
    {
        CountTimer();
        JumpChecks();
        Attack();
        AttackCheck();
        Die();
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        CollisionChecks();
        Jump();

        if (_lockMovement)
        {
            if (_isGrounded)
            {
                _rb.linearVelocity = Vector2.zero;
            }            
            return;
        }
        

        if (_isGrounded)
        {
            Move(Stats.GroundAcceleration, Stats.GroundDeceleration, InputManager.Movement);
        }
        else
        {
            Move(Stats.AirAcceleration, Stats.AirDeceleration, InputManager.Movement);
        }
    }

    #region Movement

    private void Move(float acceleration, float deceleration, Vector2 moveInput)
    {
        if (_lockMovement) return;
        
            if (moveInput != Vector2.zero)
            {
                //check if he needs to turn
                TurnCheck(moveInput);

                Vector2 targetVelocity = Vector2.zero;
                if (InputManager.RunIsHeld)
                {
                    targetVelocity = new Vector2(moveInput.x, 0f) * Stats.MaxRunSpeed;
                }
                else { targetVelocity = new Vector2(moveInput.x, 0f) * Stats.MaxWalkSpeed; }

                _moveVelocity = Vector2.Lerp(_moveVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
                _rb.linearVelocity = new Vector2(_moveVelocity.x, _rb.linearVelocity.y);
            }

            else if (moveInput == Vector2.zero)
            {
                _moveVelocity = Vector2.Lerp(_moveVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
                _rb.linearVelocity = new Vector2(_moveVelocity.x, _rb.linearVelocity.y);
            }
        
    }

    private void TurnCheck(Vector2 moveInput)
    {
        if (_isFacingRight && moveInput.x < 0)
        {
            Turn(false);
        }
        else if (!_isFacingRight && moveInput.x > 0)
        {
            Turn(true);
        }
    }

    public void Turn(bool turnRight)
    {
        if (turnRight)
        {
            _isFacingRight = true;
            transform.Rotate(0f, 180f, 0f);
        }
        else
        {
            _isFacingRight = false;
            transform.Rotate(0f, -180f, 0f);
        }
    }

    #endregion

    #region Jump

    private void JumpChecks()
    {
        if (_lockMovement && _isGrounded) return;

        // If the lockout timer is active, we skip the jump initiation
        if (Stats.JumpLockoutTimer > 0f)
        {
            Stats.JumpLockoutTimer -= Time.deltaTime;
            return;  // Skip the jump logic until the lockout is over
        }

        //press jump
        if (InputManager.JumpWasPressed)
        {
            _jumpBufferTimer = Stats.JumpBufferTime;
            _jumpReleasedDuringBuffer = false;
        }

        //relede jump
        if (InputManager.JumpWasRelesed)
        {
            if (_jumpBufferTimer > 0f)
            {
                _jumpReleasedDuringBuffer = true;
            }
            if (_isJumping && VerticalVelocity > 0f /*&& _canCutJump*/)
            {
                if (_isPastApexThreshold)
                {
                    _isPastApexThreshold = false;
                    _isFastFalling = true;
                    _fastFallTime = Stats.TimeForUpwardsCancel;
                    VerticalVelocity = 0f;
                }
                else
                {
                    _isFastFalling = true;
                    _fastFallReleaseSpeed = VerticalVelocity;
                }
            }
        }
        //jump buffering and coyote time
        if (_jumpBufferTimer > 0 && !_isJumping && (_isGrounded || _coyoteTimer > 0))
        {
            InitiateJump(3);

            /*if (_jumpReleasedDuringBuffer)
            {
                _isFastFalling = true;
                _fastFallReleaseSpeed = VerticalVelocity;
            }*/

        }

        //double jump
       /* else if (_jumpBufferTimer >0 && _isJumping && _numberOfJumpsUsed < MoveStats.NumberOfJumpsAllowed)
        {
            _isFastFalling= false;
            InitiateJump(1);
        }*/

        //fall/air jump
        else if (_jumpBufferTimer > 0 && _isFalling && _numberOfJumpsUsed < Stats.NumberOfJumpsAllowed -1)
        {
            InitiateJump(3); //<- means that if you fall/air jump, you CAN NOT double jump
            _isFastFalling = false;
        }

        //landed
        if ((_isFalling || _isJumping) && _isGrounded && VerticalVelocity <= 0f)
        {
            _isJumping = false;
            _isFalling = false;
            _isFastFalling = false;
            _isPastApexThreshold = false;
            _fastFallTime = 0f;
            _numberOfJumpsUsed = 0;
            //_canCutJump = false;
            Stats.JumpLockoutTimer = Stats.JumpLockoutTime;  // Reset the lockout timer on landing

            _animator.SetTrigger("Land");
            VerticalVelocity = Physics2D.gravity.y;
        }
    }

    public void InitiateJump(int numberOfJumpsUsed)
    {
        if (!_isJumping)
        {
            _isJumping = true;
            Stats.JumpLockoutTimer = Stats.JumpLockoutTime;  // Start the lockout after initiating a jump
        }

        _jumpBufferTimer = 0f;
        _jumpDuration = 0f;
        //_canCutJump = false;
        //StartCoroutine(EnableJumpCutAfterDelay(Stats.MinJumpTimeBeforeCut));
        _numberOfJumpsUsed += numberOfJumpsUsed;
        VerticalVelocity = Stats.InitialJumpVelocity;
    }

    private void Jump()
    {
        if (_isJumping)
        {
            _jumpDuration += Time.fixedDeltaTime;

            // Head bump check: if the player hits their head, force fast fall
            if (_bumpedHead)
            {
                _isFastFalling = true;
            }

            // **Going UP (Jumping)**
            if (VerticalVelocity > 0f)
            {
                // Apex detection
                _apexPoint = Mathf.InverseLerp(Stats.InitialJumpVelocity, 0f, VerticalVelocity);

                if (_apexPoint > Stats.ApexThreshold)
                {
                    if (!_isPastApexThreshold)
                    {
                        _isPastApexThreshold = true;
                        _timePastApexThreshold = 0f;
                    }

                    // Slightly reduce speed near the apex for a hang effect
                    _timePastApexThreshold += Time.fixedDeltaTime;
                    if (_timePastApexThreshold < Stats.ApexHangTime)
                    {
                        VerticalVelocity *= 0.9f; // Gradual slow down near apex
                    }
                    else
                    {
                        VerticalVelocity -= Stats.Gravity * Time.fixedDeltaTime; // Allow falling naturally
                    }
                }
                else
                {
                    // Normal upward gravity
                    VerticalVelocity += Stats.Gravity * Time.fixedDeltaTime;
                    _isPastApexThreshold = false; // Reset if below apex threshold
                }
            }

            // **Falling Phase**
            else if (VerticalVelocity < 0f)
            {
                _isFalling = true;

                // Apply different gravity multipliers for normal and fast fall
                float fallMultiplier = _isFastFalling ? Stats.GravityOnReleaseMultiplier : Stats.FallGravityMultiplier;
                VerticalVelocity += Stats.Gravity * fallMultiplier * Time.fixedDeltaTime;
            }
        }

        // **Jump Cut (Fast Fall)**
        if (_isFastFalling)
        {
            if (_fastFallTime >= Stats.TimeForUpwardsCancel)
            {
                VerticalVelocity += Stats.Gravity * Stats.GravityOnReleaseMultiplier * Time.fixedDeltaTime;
            }
            else
            {
                VerticalVelocity = Mathf.Lerp(_fastFallReleaseSpeed, 0f, (_fastFallTime / Stats.TimeForUpwardsCancel));
            }

            _fastFallTime += Time.fixedDeltaTime;
        }

        // **Normal Falling Gravity**
        if (!_isGrounded && !_isJumping)
        {
            _isFalling = true;
            VerticalVelocity += Stats.Gravity * Stats.FallGravityMultiplier * Time.fixedDeltaTime;
        }

        // **Clamp Fall Speed to Avoid Unrealistic Speeds**
        VerticalVelocity = Mathf.Clamp(VerticalVelocity, -Stats.MaxFallSpeed, 50f);

        // Apply the vertical velocity to the Rigidbody
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, VerticalVelocity);
    }
    public void OverrideJump(float force)
    {
        _isJumping = true;
        _isFastFalling = false;
        _isFalling = false;
        _isPastApexThreshold = false;
       // _numberOfJumpsUsed = 1;
        VerticalVelocity = force;
    }

    #endregion

    #region Attack

    private void Attack()
    {
        if (_isAttacking) return;
        
        if (InputManager.AttackDownExecuted && !_isGrounded)
        {
            _isAttacking = true;

            _downAttackColl.enabled = true;
            _animator.SetTrigger("Attack");

            StartCoroutine(DisableColliderAfterDelay(_downAttackColl));            
            StartCoroutine(EndAttackCooldown());           
        }
        else if (InputManager.AttackDownExecuted || InputManager.AttackIsPressed)
        {
            _isAttacking = true;
            _lockMovement = true;

            _attackColl.enabled = true;
            _animator.SetTrigger("Attack");

            StartCoroutine(DisableColliderAfterDelay(_attackColl));            
            StartCoroutine(EndAttackCooldown());            
        }
    }

    public void OnHit(int damage, Vector2 knokback)
    {
        _rb.linearVelocity = new Vector2(knokback.x, _rb.linearVelocity.y + knokback.y);
    }

    #endregion

    #region Checks

    private void IsGrounded()
    {
        Vector2 boxCastOrigin = new Vector2(_feetColl.bounds.center.x, _feetColl.bounds.min.y);
        Vector2 boxCastSize = new Vector2(_feetColl.bounds.size.x, Stats.GroundDetectionRayLength);
        _groundHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.down, Stats.GroundDetectionRayLength, Stats.GroundLayer);

        if (_groundHit.collider != null)
        {
            _isGrounded = true;
        }
        else { _isGrounded = false; }

    }

    private void BumpedHead()
    {
        Vector2 boxCastOrigin = new Vector2(_feetColl.bounds.center.x, _bodyColl.bounds.max.y);
        Vector2 boxCastSize = new Vector2(_feetColl.bounds.size.x * Stats.HeadWidth, Stats.HeadDetectionRayLength);

        _headHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.up, Stats.HeadDetectionRayLength, Stats.GroundLayer);
        if (_headHit.collider != null)
        {
            _bumpedHead = true;
        }
        else { _bumpedHead = false; }
    }

    private void CheckWall()//<-new
    {
        Vector2 direction = _isFacingRight ? Vector2.right : Vector2.left;
        Vector2 origin = _bodyColl.bounds.center;
        _wallHit = Physics2D.Raycast(origin, direction, Stats.WallCheckDistance, Stats.GroundLayer);
        _isTouchingWall = _wallHit.collider != null;
    }

    private void CollisionChecks()
    {
        IsGrounded();
        BumpedHead();
        CheckWall();//<-new
    }

    private void AttackCheck()
    {
        if(InputManager.AttackDownExecuted == true && _isGrounded == false)
        {
            Debug.Log("DownwaedAttack");
        }
        else if (InputManager.AttackIsPressed == true)
        {
            Debug.Log("NormalAttack");
        }
    }

    #endregion

    #region Timers

    private void CountTimer()
    {
        _jumpBufferTimer -= Time.deltaTime;

        if (!_isGrounded)
        {
            _coyoteTimer -= Time.deltaTime;
        }
        else { _coyoteTimer = Stats.JumpCoyoteTime; }
    }

    private IEnumerator DisableColliderAfterDelay(Collider2D collider)
    {
        yield return new WaitForSeconds(_attackDuration);
        collider.enabled = false;
    }

    private IEnumerator EndAttackCooldown()
    {
        yield return new WaitForSeconds(1f); // your attack duration
        _isAttacking = false;
        _lockMovement = false;
        
    }

    #endregion

    #region Animation

    private void UpdateAnimations()
    {
        bool running = InputManager.Movement.x != 0 && _isGrounded;
        _animator.SetBool("isRunning", running);
        _animator.SetBool("isJumping", _isJumping);

        // Use vertical velocity check for falling
        bool falling = !_isGrounded && _rb.linearVelocity.y < -0.1f;
        _animator.SetBool("isFalling", falling);

       // Debug.Log($"Running: {running}, Jumping: {_isJumping}, Falling: {falling}");
    }


    /*public void TakeDamage()
    {
        _animator.SetTrigger("Damaged");
    }*/

    public void Die()
    {
        //_animator.SetTrigger("Dead");
        if (!Damage._isAlive)
        {
            this.enabled = false; // Disable player controls on death
        }        
    }
    #endregion
}
