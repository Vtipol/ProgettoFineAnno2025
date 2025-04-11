using UnityEngine;

public class Gum : MonoBehaviour
{
    [SerializeField] private CircleCollider2D gumCollider;
    public PickedUp pickedUp;

    private void LateUpdate()
    {
        if (pickedUp.IsPickedUp)
        {
            if (gumCollider.enabled)
                gumCollider.enabled = false;
            else gumCollider.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !pickedUp.IsPickedUp)
        {
            Debug.Log("Entered Gum Collider");
        }

    }
}
