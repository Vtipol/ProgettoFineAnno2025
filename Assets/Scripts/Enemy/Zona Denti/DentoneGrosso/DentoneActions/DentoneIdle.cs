using UnityEngine;

public class DentoneIdle : MonoBehaviour
{
    [SerializeField] private DentoneGrossoScriptable dentoneStats;
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform rootTransform;
    [SerializeField] private GroundChecker groundChecker;
    private Rigidbody2D rb;
    private float flipCooldownTimer = 0f;
    private const float flipCooldownDuration = 0.5f;
    private RaycastHit2D playerSearch;
    private float lastSeenTime = -Mathf.Infinity;
    private bool playerSighted = false;
    public bool PlayerSighted
    {
        get => playerSighted;
        set
        {
            playerSighted = value;
        }
    }

    private void Awake()
    {
        rootTransform = transform.parent;
        playerLayer = LayerMask.GetMask("Player");
        rb = rootTransform.GetComponent<Rigidbody2D>();
    }

    public void Wander()
    {
        if (flipCooldownTimer > 0f)
        {
            flipCooldownTimer -= Time.deltaTime;
        }

        if (dentoneStats.isPaused)
        {
            dentoneStats.pauseTimer += Time.deltaTime;
            if (dentoneStats.pauseTimer >= dentoneStats.pauseDuration)
            {
                dentoneStats.isPaused = false;
                dentoneStats.wanderDirection = Random.Range(0, 2) == 0 ? -1 : 1;
                dentoneStats.currentWanderDuration = Random.Range(1f, 5f);
                dentoneStats.wanderTimer = 0f;
            }
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (dentoneStats.wanderDirection != 0)
        {
            if (groundChecker.IsGroundAhead() && !groundChecker.IsWallAhead(dentoneStats.wanderDirection))
            {
                Vector2 targetVelocity = new Vector2(dentoneStats.wanderDirection * dentoneStats.walkSpeed, rb.linearVelocity.y);
                rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, Time.deltaTime * 5f);
                rootTransform.localScale = new Vector3(dentoneStats.wanderDirection, 1, 1);
            }
            else if (flipCooldownTimer <= 0f)
            {
                dentoneStats.wanderDirection *= -1;
                rootTransform.localScale = new Vector3(dentoneStats.wanderDirection, 1, 1);
                flipCooldownTimer = flipCooldownDuration;

                Vector2 retreatVelocity = new Vector2(dentoneStats.wanderDirection * 0.5f, rb.linearVelocity.y);
                rb.linearVelocity = retreatVelocity;
            }

        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        dentoneStats.wanderTimer += Time.deltaTime;
        if (dentoneStats.wanderTimer >= dentoneStats.currentWanderDuration)
        {
            dentoneStats.wanderDirection = 0;
            dentoneStats.isPaused = true;
            dentoneStats.pauseTimer = 0f;
            return;
        }
    }

    public void FlipTowards(Vector2 targetPosition)
    {
        Vector2 dir = targetPosition - (Vector2)transform.position;
        if (dir.x > 0)
            rootTransform.localScale = new Vector3(1, 1, 1);
        else
            rootTransform.localScale = new Vector3(-1, 1, 1);

    }

    public void SearchPlayer()
    {
        Vector2 direction = new Vector2(rootTransform.localScale.x, 0f).normalized;
        playerSearch = Physics2D.Raycast(transform.position, direction, dentoneStats.sightRange, playerLayer);
        Debug.DrawRay(transform.position, direction * dentoneStats.sightRange, Color.red);

        if (playerSearch.collider != null)
        {
            lastSeenTime = Time.time;
        }

        playerSighted = (Time.time - lastSeenTime) < dentoneStats.forgetDelay;
    }
}