using System;
using UnityEngine;

public class AIMerge : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public float jumpForce = 10f;
    public float stopDistance = 1f;
    public float fieldOfViewRadius = 10f; 
    public LayerMask groundLayer;
    public LayerMask targetLayer; 
    public Transform groundCheck;
    public float groundCheckRadius = 1.2f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool targetInView;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        DetectTarget();
        CheckGroundStatus();
    }

    void FixedUpdate()
    {
        if (targetInView)
        {
            MoveTowardsTarget();
        }
    }

    void DetectTarget()
    {
        targetInView = Physics2D.OverlapCircle(transform.position, fieldOfViewRadius, targetLayer) != null;
    }

    void CheckGroundStatus()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer) != null;
    }

    void MoveTowardsTarget()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance < stopDistance) return;

        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);

        if (isGrounded && target.position.y > transform.position.y + 5f)
        {
            RaycastHit2D gapAhead = Physics2D.Raycast(transform.position + new Vector3(distance, 0, 0),
                Vector2.down, 2f, groundLayer);

            if (gapAhead.collider == null)  
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
    }
}
