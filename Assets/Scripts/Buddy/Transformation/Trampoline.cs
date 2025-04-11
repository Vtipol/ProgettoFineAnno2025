using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D trampolineCollider;
    [SerializeField] private float trampolineBounceForce = 25f;
    public PickedUp pickedUp;

    private Rigidbody2D _playerRb;
    private bool _shouldBouncePlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !pickedUp.IsPickedUp)
        {
            Debug.Log("Entered Trampoline Collider");

        }

    }
}