using UnityEngine;

public class MascellaIdle : MonoBehaviour
{
    [SerializeField] private MascellaBipedeScriptable mascellaStats;
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform rootTransform;
    [SerializeField] private GroundChecker groundChecker;
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
    }
    public void Wander()
    {
        if (mascellaStats.isPaused)
        {
            mascellaStats.pauseTimer += Time.deltaTime;
            if (mascellaStats.pauseTimer >= mascellaStats.pauseDuration)
            {
                mascellaStats.isPaused = false;
                mascellaStats.wanderDirection = Random.Range(0, 2) == 0 ? -1 : 1;
                mascellaStats.currentWanderDuration = Random.Range(1f, 3f);
                mascellaStats.wanderTimer = 0f;
            }
            return;
        }
        Vector2 movement = Vector2.right * mascellaStats.wanderDirection * mascellaStats.walkSpeed * Time.deltaTime;

        if (mascellaStats.wanderDirection != 0 && groundChecker.IsGroundAhead())
        {
            rootTransform.Translate(movement);
            rootTransform.localScale = new Vector3(mascellaStats.wanderDirection, 1, 1);
        }
        else
        {
            mascellaStats.wanderDirection = 0;
            mascellaStats.isPaused = true;
            mascellaStats.pauseTimer = 0f;
        }
        mascellaStats.wanderTimer += Time.deltaTime;
        if (mascellaStats.wanderTimer >= mascellaStats.currentWanderDuration)
        {
            mascellaStats.wanderDirection = 0;
            mascellaStats.isPaused = true;
            mascellaStats.pauseTimer = 0f;
            return;
        }

        if (mascellaStats.wanderDirection != 0)
        {
            rootTransform.localScale = new Vector3(mascellaStats.wanderDirection, 1, 1);
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
