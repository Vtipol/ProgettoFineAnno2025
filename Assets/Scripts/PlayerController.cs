using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private bool isGrounded;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DropThroughPlatform());
        }
    }
    IEnumerator DropThroughPlatform()
    {
        Collider2D playerCollider = GetComponent<CapsuleCollider2D>();
        playerCollider.enabled = false;
        yield return new WaitForSeconds(1f); 
        playerCollider.enabled = true;
    }
}