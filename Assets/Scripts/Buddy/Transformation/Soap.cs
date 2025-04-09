using UnityEngine;

public class Soap : MonoBehaviour
{
    [SerializeField] private BoxCollider2D SoapCollider;
    public PickedUp pickedUp;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !pickedUp.IsPickedUp)
        {
            Debug.Log("Entered Soap Collider");
        }

    }
}
