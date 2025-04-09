using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D trampolineCollider;
    public PickedUp pickedUp;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !pickedUp.IsPickedUp)
        {
            Debug.Log("Entered Trampoline Collider");
        }

    }
}