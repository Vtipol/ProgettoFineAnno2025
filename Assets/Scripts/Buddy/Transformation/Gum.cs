using System;
using UnityEngine;
using System.Collections;

public class Gum : MonoBehaviour
{
    private CircleCollider2D gumCollider;
    [SerializeField] private AIFollowSettings stats;
    private Rigidbody2D rb;
    private Rigidbody2D rbPlayer;
    private GameObject player;
    private Player playerScript;
    private LayerMask groundLayer;
    private PickedUp pickedUp;
    public int numberOfGumJumps;
    private bool rightDirection;
    private bool gumLockOutWindow = false;
    private RigidbodyConstraints2D originalRbConstraints;
    private RigidbodyConstraints2D originalRbPlayerConstraints;
    private bool isStuck = false;
    public bool IsStuck
    {
        get => isStuck;
        set { isStuck = value; }
    }
    private bool isGumJumping = false;
    public bool IsGumJumping
    {
        get => isGumJumping;
        set  { isGumJumping = value; }   
    }
    private void Awake()
    {
        groundLayer = LayerMask.GetMask("Ground");
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        rbPlayer = player.GetComponent<Rigidbody2D>();
        rb = GetComponentInParent<Rigidbody2D>();
        pickedUp = GetComponentInParent<PickedUp>();
        gumCollider = GetComponent<CircleCollider2D>();
        originalRbConstraints = rb.constraints;
        originalRbPlayerConstraints = rbPlayer.constraints;
    }
    public void PlayerGumJump()
    {
        if (isStuck && InputManager.JumpWasPressed && pickedUp.IsPickedUp && !playerScript._isGrounded)
        {
            numberOfGumJumps++;
            Debug.Log("number of Gumjumps " + numberOfGumJumps);
            isStuck = false;
            GumStuck();
            GumPlayerCheck();
            playerScript.Turn(rightDirection);
            Vector2 gumJumpDirection = rightDirection? new Vector2(1f, 1f): new Vector2(-1f, 1f);
            gumJumpDirection.Normalize();
            float xForce = rightDirection ? stats.gumJumpXForce : -stats.gumJumpXForce;
            Vector2 jumpForce = new Vector2(xForce, stats.gumJumpYForce);
            playerScript.GumJump(jumpForce);
            StartCoroutine(JumpLockOut());
        }
        else if (!pickedUp.IsPickedUp)
        {
            IsStuck = false;
            isGumJumping = false;
        }
    }
    private void GumPlayerCheck()
    {
        if (playerScript._isFacingRight /*&& moveInput.x < 0*/)
        {
            rightDirection = false;
        }
        else if (!playerScript._isFacingRight /*&& moveInput.x > 0*/)
        {
            rightDirection = true;
        }

        //if (numberOfGumJumps >= 3)
        //{
        //    isGumJumping = false;
        //    Debug.Log("is gum jumping is false");
        //}
    }
    public void GumStuck()
    {
        if (isStuck && pickedUp.IsPickedUp)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rbPlayer.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rb.constraints = originalRbConstraints;
            rbPlayer.constraints = originalRbPlayerConstraints;
        }
    }
    public void CheckStuck()
    {
        if (!pickedUp.IsPickedUp || !gumCollider.enabled || gumLockOutWindow || numberOfGumJumps >= 4) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, gumCollider.radius * 1.1f, groundLayer);
        if (hits.Length > 0 )
        {
            isStuck = true;
        }
    }
    private IEnumerator JumpLockOut()
    {
        gumLockOutWindow = true;
        yield return new WaitForSeconds(0.05f);
        gumLockOutWindow = false;
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (pickedUp.IsPickedUp && other.IsTouchingLayers(groundLayer))
    //    {
    //        Debug.Log("Collided with: " + other.gameObject.name);
    //        isStuck = true;
    //        Debug.Log("isStuck is true");
    //    }
    //}
}
//private void Awake()
//{
//    rb = GetComponent<Rigidbody2D>();
//}

//public void Activate()
//{
//    Debug.Log("Gum Activated");
//    gumCollider.enabled = true;
//}

//public void Deactivate()
//{
//    Debug.Log("Gum Deactivated");
//    gumCollider.enabled = false;
//    Unstick();
//}

//private void Stick()
//{
//    if (isStuck) return;
//    isStuck = true;
//    rb.gravityScale = 0f;
//    rb.linearVelocity = Vector2.zero;
//    rb.constraints = RigidbodyConstraints2D.FreezeAll;
//    Debug.Log("Buddy stuck to wall");
//}

//private void Unstick()
//{
//    if (!isStuck) return;
//    isStuck = false;
//    rb.gravityScale = 1f;
//    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
//    Debug.Log("Buddy unstuck");
//}

//private void OnCollisionEnter2D(Collision2D collision)
//{
//    if (collision.collider.CompareTag("Wall"))
//    {
//        Stick();
//    }
//}

//private void OnCollisionExit2D(Collision2D collision)
//{
//    if (collision.collider.CompareTag("Wall"))
//    {
//        Unstick();
//    }
//}