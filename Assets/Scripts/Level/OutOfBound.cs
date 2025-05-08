using UnityEngine;

public class OutOfBound : MonoBehaviour
{
    public GameObject startPoint;
    public GameObject Player;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.transform.position = startPoint.transform.position;
        }
    }
}
