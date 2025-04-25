using UnityEngine;

public class Attacking : MonoBehaviour
{
    Collider2D attackCollider;
    public int attackDamage = 10;
    public Vector2 knokback = Vector2.zero;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // See if it can be hit
        Damageble damageble = collision.GetComponent<Damageble>();

        if (damageble != null)
        {
            Vector2 deliverKnokback = transform.parent.localScale.x > 0 ? knokback : new Vector2(-knokback.x, knokback.y);

            // Hit the target
            damageble.Hit(attackDamage, deliverKnokback);
        }
    }
}
