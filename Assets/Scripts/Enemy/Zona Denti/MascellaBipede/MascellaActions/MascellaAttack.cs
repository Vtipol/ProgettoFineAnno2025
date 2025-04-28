using UnityEngine;

public class MascellaAttack : MonoBehaviour
{
    [SerializeField] private MascellaBipedeScriptable mascellaStats;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform target;
    [SerializeField] private PolygonCollider2D attackCollider;
    public bool HasMissed { get; private set; } = false; 

    private void Awake()
    {
       
    }

    public void PerformLunge()
    {
        if (target == null)
            return;

        attackCollider.enabled = true;

        rb.linearVelocity = Vector2.zero;

        Vector2 lungeDirection = (target.position - transform.position).normalized;
        Vector2 lungeForce = new Vector2(lungeDirection.x, 1f).normalized * mascellaStats.mascellaLungeForce;

        rb.AddForce(lungeForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Mascella hit the Player!");
            // TODO: damage logic

            attackCollider.enabled = false;
        }
        else
        {
            if (!collision.isTrigger) 
            {
                Debug.Log("Mascella missed and crashed!");

                HasMissed = true;
                mascellaStats.isCrashed = true; 


                attackCollider.enabled = false;
            }
        }
    }

    public void ResetAttackFlags()
    {
        HasMissed = false;
        mascellaStats.isCrashed = false;
    }
}
