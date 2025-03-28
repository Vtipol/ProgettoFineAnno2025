using UnityEngine;

public class AIFollow : MonoBehaviour
{
   public Transform target;
   public float speed = 2f;
   public float jumpStrenght = 3f;
   public LayerMask groundLayer;
   
   private Rigidbody2D rb;
   private bool isGrounded;
   private bool needJump;
    void Start()
    {
     rb = GetComponent<Rigidbody2D>();   
    }
    void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 2f, groundLayer);
        float direction = Mathf.Sign(target.position.x - transform.position.x);
        bool isTargetAirbone = Physics2D.Raycast(transform.position, 
            Vector2.up, 3f, 1<<target.gameObject.layer);
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
            RaycastHit2D groundFront = Physics2D.Raycast(transform.position,new Vector2(direction,0),
                2f, groundLayer);
            RaycastHit2D gapAhead = Physics2D.Raycast(transform.position + new Vector3(direction,0,0),
                Vector2.down,2f,groundLayer);
            RaycastHit2D platformOverhead = Physics2D.Raycast(transform.position, Vector2.up,
                2f,groundLayer);
            if (!groundFront.collider && !gapAhead.collider)
            {
                needJump = true;
            }
            else if (isTargetAirbone && platformOverhead.collider)
            {
                needJump = true;
            }
        }
    }
    private void FixedUpdate()
    {
        if (isGrounded && needJump)
        {
            needJump = false;
            Vector2 direction = (target.position - transform.position).normalized;
            Vector2 jumpDirection = direction * jumpStrenght;
            rb.AddForce(new Vector2(jumpDirection.x, jumpStrenght), ForceMode2D.Impulse);
        }
    }
}
