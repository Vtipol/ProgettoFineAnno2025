using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public float jumpForce = 10f; 
    public float stopDistance = 1f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 1.2f;
    private Rigidbody2D rb;
    private bool isGrounded;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance >= stopDistance)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);

            if (isGrounded && target.position.y > transform.position.y + 0.5f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
        }
    }
}