using UnityEngine;
using System.Collections;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D trampolineCollider;
    private Player player;
    private PickThrow pickThrow;
    [SerializeField] private float trampolineBounceForce = 25f;
    [SerializeField] public BoxCollider2D trampGrabCollider;
    public PickedUp pickedUp;
    public AudioSource audioSource;
    public AudioClip trampClip;
    public AudioClip miaoClip;
    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
        pickThrow = FindAnyObjectByType<PickThrow>();
    }
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
        audioSource.PlayOneShot(trampClip);
        player.OverrideJump(trampolineBounceForce);
        StartCoroutine(TrampGrab());
    }
    private IEnumerator TrampGrab()
    {
        trampGrabCollider.enabled = true;
        float duration = 0.3f;
        float elapsed = 0f;

        while (elapsed < duration) // while loop è ammissibile in una couroutine
        {
            pickThrow.TryAutoPickBuddy();
            elapsed += Time.deltaTime;
            yield return null;
        }

        trampGrabCollider.enabled = false;
    }
}