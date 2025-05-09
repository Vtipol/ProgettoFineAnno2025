using UnityEngine;

public class OutOfBound : MonoBehaviour
{
    public GameObject startPoint;
    public GameObject player;

    private Transform currentRespawnPoint;

    private void Start()
    {
        if (startPoint != null)
            currentRespawnPoint = startPoint.transform;

        if (player != null && currentRespawnPoint != null)
            player.transform.position = currentRespawnPoint.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.position = currentRespawnPoint.position;
        }
    }

    public void SetCheckpoint(Transform newCheckpoint)
    {
        currentRespawnPoint = newCheckpoint;
    }

    public void LoadCheckpoint()
    {
        if (player != null && currentRespawnPoint != null)
            player.transform.position = currentRespawnPoint.position;
    }
}
