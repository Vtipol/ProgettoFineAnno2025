using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public OutOfBound outOfBound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            outOfBound.SetCheckpoint(transform);
        }
    }
}
