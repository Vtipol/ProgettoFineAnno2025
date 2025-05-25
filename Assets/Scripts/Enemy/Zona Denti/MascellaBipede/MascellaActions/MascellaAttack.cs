using UnityEngine;
using System.Collections;

public class MascellaAttack : MonoBehaviour
{
    [SerializeField] private MascellaBipedeScriptable mascellaStats;
    [SerializeField] public Rigidbody2D rb;
    private Transform target;
    [SerializeField] private PolygonCollider2D attackCollider;
    public bool HasMissed { get;  set; } = false;
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

        float directionX = Mathf.Sign(target.position.x - transform.position.x);
        Vector2 lungeDirection = new Vector2(directionX, 1f).normalized;

        if (directionX > 0 && transform.parent.localScale.x < 0)
            transform.parent.localScale = new Vector3(1, 1, 1);
        else if (directionX < 0 && transform.parent.localScale.x > 0)
            transform.parent.localScale = new Vector3(-1, 1, 1);

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
        yield return new WaitForSeconds(0.4f);
        mascellaStats.isCrashed = true;
        HasMissed = true;
    }
}
