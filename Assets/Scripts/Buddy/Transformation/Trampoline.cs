using UnityEngine;
using System.Collections;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D trampolineCollider;
    [SerializeField] private Player player;
    [SerializeField] private float trampolineBounceForce = 25f;
    [SerializeField] private BoxCollider2D trampGrabCollider;
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
    private void Update()
    {
        if (trampGrabCollider.enabled)
        {
            Debug.Log("TrampGrabCollider was Enabled");
        }
    }
    private void BouncePlayer(Player player)
    {
        player.OverrideJump(trampolineBounceForce);
        StartCoroutine(TrampGrab());
    }
    private IEnumerator TrampGrab()
    {
        trampGrabCollider.enabled = true;
        yield return new WaitForSeconds(0.3f);
        trampGrabCollider.enabled = false;
    }
}