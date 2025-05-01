using UnityEngine;

public class MascellaIdle : MonoBehaviour
{
    [SerializeField] private MascellaBipedeScriptable mascellaStats;
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

        if (mascellaStats.isPaused)
        {
            mascellaStats.pauseTimer += Time.deltaTime;
            if (mascellaStats.pauseTimer >= mascellaStats.pauseDuration)
            {
                mascellaStats.isPaused = false;
                mascellaStats.wanderDirection = Random.Range(0, 2) == 0 ? -1 : 1;
                mascellaStats.currentWanderDuration = Random.Range(1f, 5f);
                mascellaStats.wanderTimer = 0f;
            }
            rb.linearVelocity = Vector2.zero; 
            return;
        }

        if (mascellaStats.wanderDirection != 0)
        {
            if (groundChecker.IsGroundAhead() && !groundChecker.IsWallAhead(mascellaStats.wanderDirection))
            {
                Vector2 targetVelocity = new Vector2(mascellaStats.wanderDirection * mascellaStats.walkSpeed, rb.linearVelocity.y);
                rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, Time.deltaTime * 5f);
                rootTransform.localScale = new Vector3(mascellaStats.wanderDirection, 1, 1);
            }
            else if (flipCooldownTimer <= 0f)
            {
                mascellaStats.wanderDirection *= -1;
                rootTransform.localScale = new Vector3(mascellaStats.wanderDirection, 1, 1);
                flipCooldownTimer = flipCooldownDuration;

                Vector2 retreatVelocity = new Vector2(mascellaStats.wanderDirection * 0.5f, rb.linearVelocity.y);
                rb.linearVelocity = retreatVelocity;
            }

        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        mascellaStats.wanderTimer += Time.deltaTime;
        if (mascellaStats.wanderTimer >= mascellaStats.currentWanderDuration)
        {
            mascellaStats.wanderDirection = 0;
            mascellaStats.isPaused = true;
            mascellaStats.pauseTimer = 0f;
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
        playerSearch = Physics2D.Raycast(transform.position, direction, mascellaStats.sightRange, playerLayer);
        Debug.DrawRay(transform.position, direction * mascellaStats.sightRange, Color.red);

        if (playerSearch.collider != null)
        {
            lastSeenTime = Time.time;
        }

        playerSighted = (Time.time - lastSeenTime) < mascellaStats.forgetDelay;
    }
}
