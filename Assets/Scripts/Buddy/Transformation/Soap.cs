using UnityEngine;

public class Soap : MonoBehaviour
{
    [SerializeField] private BoxCollider2D soapCollider;
    public PickedUp pickedUp;

    private void LateUpdate()
    {
        if (pickedUp.IsPickedUp)
        {
            if (soapCollider.enabled)
                soapCollider.enabled = false;
            else soapCollider.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !pickedUp.IsPickedUp)
        {
            Debug.Log("Entered Soap Collider");
        }

    }
}
