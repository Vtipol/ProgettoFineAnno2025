using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D trampolineCollider;
    [SerializeField] private float trampolineBounceForce = 25f;
    public PickedUp pickedUp;
    public BuddyState buddyState;

    private Rigidbody2D _playerRb;
    private bool _shouldBouncePlayer;

    private void LateUpdate()
    {
        if (pickedUp.IsPickedUp)
        {
            trampolineCollider.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !pickedUp.IsPickedUp)
        {
            Debug.Log("Entered Trampoline Collider");

        }

    }
}