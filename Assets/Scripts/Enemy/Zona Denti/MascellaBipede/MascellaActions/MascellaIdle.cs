using UnityEngine;

public class MascellaIdle : MonoBehaviour
{
    [SerializeField] private MascellaBipedeScriptable mascellaStats;
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask playerLayer;
    private RaycastHit2D playerSearch;
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
        playerLayer = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        SearchPlayer();
    }

    private void SearchPlayer()
    {
        playerSearch = Physics2D.Raycast(transform.position, (target.position - transform.position).normalized, mascellaStats.sightRange, playerLayer);
        if(playerSearch)
        {
            Debug.Log("Spotted Player");
            playerSighted = true;
        }
        else
        {
            playerSighted = false;
        }
    }
}
