using UnityEngine;

public class Soap : MonoBehaviour
{
    [SerializeField] private BoxCollider2D soapCollider;
    public PickedUp pickedUp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !pickedUp.IsPickedUp)
        {
            Debug.Log("Entered Soap Collider");
        }

    }
}
