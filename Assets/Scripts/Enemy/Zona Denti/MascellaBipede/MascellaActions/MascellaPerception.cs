using UnityEngine;

public class MascellaPerception : MonoBehaviour
{
    [SerializeField] private MascellaBipedeScriptable mascellaStats;
    [SerializeField] private Transform rootTransform;
    [SerializeField] private Transform origin;
    private LayerMask playerLayer;
    private float lastSeenTime = -Mathf.Infinity;

    private void Awake()
    {
        rootTransform = transform.parent;
        playerLayer = LayerMask.GetMask("Player");
    }
   /* public void ClearLastSeenTime()
    {
        lastSeenTime = -Mathf.Infinity;
    }*/
    public bool CanSeePlayer()
    {
        Vector2 direction = new Vector2(rootTransform.localScale.x , 0f).normalized;
        
        RaycastHit2D playerSearch = Physics2D.Raycast(origin.position , direction, mascellaStats.sightRange, playerLayer);
        Debug.DrawRay(origin.position, direction * mascellaStats.sightRange, Color.red);

        if (playerSearch.collider != null)
        {
            lastSeenTime = Time.time;
        }

        return (Time.time - lastSeenTime) < mascellaStats.forgetDelay;
    }
}