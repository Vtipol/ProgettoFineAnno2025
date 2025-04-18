using UnityEngine;
using System.Collections;

public class MascellaChase : MonoBehaviour
{
    [SerializeField] private MascellaBipedeScriptable mascellaStats;
    [SerializeField] private CircleCollider2D biteTrigger;
    [SerializeField] private Transform player;
    private bool isTired = false;
    private bool isChasing = false;
    private bool playerInBiteRange = false;

    public bool PlayerInRange
    {
        get => playerInBiteRange;
        set => playerInBiteRange = value;
    }

    private void Update()
    {
        if (isChasing && !isTired && !playerInBiteRange)
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * mascellaStats.speed * Time.deltaTime);
    }

    public void StartChasing()
    {
        if (!isChasing)
        {
            isChasing = true;
            StartCoroutine(ChaseRoutine());
        }
    }

    private IEnumerator ChaseRoutine()
    {
        float runDuration = mascellaStats.runTimer;
        yield return new WaitForSeconds(runDuration);

        isTired = true;
        isChasing = false;

        yield return new WaitForSeconds(mascellaStats.tiredTime);
        isTired = false;
    }

    public void OnPlayerDetected()
    {
        if (!isTired)
            StartChasing();
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
