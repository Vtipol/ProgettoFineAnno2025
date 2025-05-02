using UnityEngine;

public class Gum : MonoBehaviour
{
    [SerializeField] private CircleCollider2D gumCollider;
    private Rigidbody2D rb;
    private bool isStuck = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Activate()
    {
        Debug.Log("Gum Activated");
        gumCollider.enabled = true;
    }

    public void Deactivate()
    {
        Debug.Log("Gum Deactivated");
        gumCollider.enabled = false;
        Unstick();
    }

    private void Stick()
    {
        if (isStuck) return;
        isStuck = true;
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        Debug.Log("Buddy stuck to wall");
    }

    private void Unstick()
    {
        if (!isStuck) return;
        isStuck = false;
        rb.gravityScale = 1f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        Debug.Log("Buddy unstuck");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            Stick();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            Unstick();
        }
    }
}
