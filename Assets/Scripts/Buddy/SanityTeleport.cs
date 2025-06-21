using UnityEngine;
using System.Collections;
public class SanityTeleport : MonoBehaviour
{
    private Rigidbody2D rb;
    private FollowPlayer follow;
    public float velocityThreshold = 0.01f;
    [SerializeField] private AIFollowSettings stats;
    [SerializeField] private BuddyStateController controller;
    [SerializeField] private Transform target;
    [SerializeField] private Animator _animator;
    public bool isStill ;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        follow = GetComponent<FollowPlayer>();
    }

    void Update()
    {
        TeleportInput();
    }

    private void TeleportInput()
    {
        isStill = rb.linearVelocity.magnitude < velocityThreshold;
        float distance = Vector2.Distance(transform.position, target.position);
        if (isStill && follow.startFollow && distance > stats.stopDistance + 1.15f)
        {
            StartCoroutine(AwaitTele());
            if (!isStill || !follow.startFollow || distance < stats.stopDistance) return;
            Vector3 offset = new Vector3(0f, 0f, 0f);
            controller.transform.position = target.position + offset;
            controller.SwitchState(controller.neutralState);
            controller.EnableOnlyCollider(null);
            Rigidbody2D rb = controller.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
            isStill = false;
            _animator.SetTrigger("Teleport");
        }
    }
    private IEnumerator AwaitTele()
    {
        yield return new WaitForSeconds(0.35f);
    }
}
