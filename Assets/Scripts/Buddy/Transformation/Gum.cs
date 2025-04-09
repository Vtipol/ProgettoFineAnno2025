using UnityEngine;

public class Gum : MonoBehaviour
{
    [SerializeField] private CircleCollider2D gumCollider;
    public PickedUp pickedUp;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !pickedUp.IsPickedUp)
        {
            Debug.Log("Entered Gum Collider");
        }

    }
}
