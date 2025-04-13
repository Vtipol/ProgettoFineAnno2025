using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D trampolineCollider;
    [SerializeField] private Player player;
    [SerializeField] private float trampolineBounceForce = 25f;
    public PickedUp pickedUp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !pickedUp.IsPickedUp)
        {
            Player player = collision.GetComponent<Player>() ?? collision.GetComponentInParent<Player>();

            if (player == null)
            {
                Debug.LogWarning($"Trampoline triggered by Player layer object, but Player script not found on {collision.gameObject.name}!");
                return;
            }

            if (player.IsFalling)
            {
                Debug.Log("Player bounced on trampoline");
                BouncePlayer(player);
            }
        }
    }
    private void BouncePlayer(Player player)
    {
        player.OverrideJump(trampolineBounceForce);
    }
}