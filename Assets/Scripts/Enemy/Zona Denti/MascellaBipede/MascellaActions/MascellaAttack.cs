using UnityEngine;
using System.Collections;

public class MascellaAttack : MonoBehaviour
{
    [SerializeField] private MascellaBipedeScriptable mascellaStats;
    [SerializeField] public Rigidbody2D rb;
    private Transform target;
    [SerializeField] private PolygonCollider2D attackCollider;
    public bool HasMissed { get; private set; } = false;
    private void Awake()
    {
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                target = playerObj.transform;
            }
            else
            {
                Debug.LogWarning("MascellaChase could not find a GameObject tagged 'Player' in Awake.");
            }
        }
    }
    public void PerformLunge()
    {
        if (target == null)
            return;

        attackCollider.enabled = true;

        rb.linearVelocity = Vector2.zero;

        rb.gravityScale = mascellaStats.lungeGravityScale;

        Vector2 lungeDirection = (target.position - transform.position).normalized;

        Vector2 lungeForce = new Vector2(
            lungeDirection.x * mascellaStats.mascellaLungeHorizontalForce,
            mascellaStats.mascellaLungeVerticalForce
        );

        rb.AddForce(lungeForce, ForceMode2D.Impulse);
        
        StartCoroutine(HandlePostLunge());
    }

    
    public void ResetAttackFlags()
    {
        HasMissed = false;
        mascellaStats.isCrashed = false;
    }
    private IEnumerator HandlePostLunge()
    {
        yield return new WaitForSeconds(0.3f);
        attackCollider.enabled = false;
        yield return new WaitForSeconds(0.05f);
        HasMissed = true;
        mascellaStats.isCrashed = true;
    }
}
