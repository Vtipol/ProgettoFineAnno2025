using UnityEngine;
using System.Collections;

public class MascellaChase : MonoBehaviour
{
    [SerializeField] private MascellaBipedeScriptable mascellaStats;
    [SerializeField] private CapsuleCollider2D biteTrigger;
    private Transform player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GroundChecker groundChecker;
    public bool isMascellaBackingAway = false;
    private bool isTired = false;
    private bool isChasing = false;
    public bool IsChasing => isChasing;
    private bool playerInBiteRange = false;

    public bool PlayerInRange
    {
        get => playerInBiteRange;
        set => playerInBiteRange = value;
    }
    private void Awake()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                Debug.LogWarning("MascellaChase could not find a GameObject tagged 'Player' in Awake.");
            }
        }
    }
    public void ChasePlayer()
    {
        if (player == null)
        {
           Debug.Log("Player is Null");
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                Debug.LogWarning("MascellaChaseState could not find a GameObject tagged 'Player'.");
            }
        }
        if (player == null || isTired || PlayerInRange || !groundChecker.IsGroundAhead())
        {
            StopChasing();
            return;
        }

        isChasing = true;
        Vector2 direction = (player.position - transform.position).normalized;

        if (direction.x > 0 && transform.parent.localScale.x < 0)
        {
            Flip();
        }
        else if (direction.x < 0 && transform.parent.localScale.x > 0)
        {
            Flip();
        }

        Vector2 targetVelocity = new Vector2(direction.x * mascellaStats.chaseSpeed, rb.linearVelocity.y);
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, Time.deltaTime * mascellaStats.acceleration);
    }
    public void StopChasing()
    {
        rb.linearVelocity = Vector2.zero;
        isChasing = false;
    }
    public void BackAway()
    {
        Debug.Log("Mascella is backing away");
        Vector2 retreatDirection = new Vector2(-Mathf.Sign(rb.linearVelocity.x), 0f);
        rb.linearVelocity = retreatDirection * mascellaStats.walkSpeed;
    }
    public void Flip()
    {
        Vector3 scale = transform.parent.localScale;
        scale.x *= -1;
        transform.parent.localScale = scale;
    }
    public bool IsPlayerTooFar()
    {
        return Vector2.Distance(transform.position, player.position) > mascellaStats.chaseRange;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerInRange = false;
        }
    }
}
    //public void ChasePlayer()
    //{
    //    if (player == null) return;

    //    Vector2 direction = (player.position - transform.position).normalized;
    //    Vector2 newPosition = rb.position + direction * mascellaStats.speed * Time.fixedDeltaTime;
    //    rb.MovePosition(newPosition);
    //}

    //public void StartChasing()
    //{
    //    if (!isChasing)
    //    {
    //        isChasing = true;
    //        StartCoroutine(ChaseRoutine());
    //    }
    //}

    //private IEnumerator ChaseRoutine()
    //{
    //    float runDuration = mascellaStats.runTimer;
    //    float timer = 0f;

    //    while (timer < runDuration)
    //    {
    //        if (player != null)
    //        {
    //            ChasePlayer();
    //        }

    //        timer += Time.deltaTime;
    //        yield return null;
    //    }

    //    isTired = true;
    //    isChasing = false;

    //    yield return new WaitForSeconds(mascellaStats.tiredTime);
    //    isTired = false;
    //}

    //public void OnPlayerDetected()
    //{
    //    if (!isTired)
    //        StartChasing();
    //}

