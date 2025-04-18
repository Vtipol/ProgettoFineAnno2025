using UnityEngine;

public class MascellaChase : MonoBehaviour
{
    [SerializeField] private MascellaBipedeScriptable mascellaStats;
    [SerializeField] private CircleCollider2D biteTrigger;
    private bool playerInRange = false;
    private bool isTired = false;
    public bool PlayerInRange
    {
        get => playerInRange;
        set
        {
            playerInRange = value;
        }
    }
    private void ChasePlayer()
    {
        while(!playerInRange && !isTired)
        {

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerInRange = true;
        }
    }

}
