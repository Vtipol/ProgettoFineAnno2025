using UnityEngine;

public class Follow : MonoBehaviour
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
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void MoveTowardsTarget()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance < stopDistance) return;

        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);

        if (isGrounded && target.position.y > transform.position.y + 2f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
}