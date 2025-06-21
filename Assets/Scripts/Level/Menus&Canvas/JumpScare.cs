using System.Collections;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject endLevelObject; // The object that moves toward the player
    [SerializeField] private Animator endLevelAnimator;
    [SerializeField] private float moveSpeed = 2f;

    [Header("Timing")]
    [SerializeField] private float delayBeforeRun = 2f;

    private Transform playerTransform;
    private Rigidbody2D endLevelRb;
    private bool hasStarted = false;
    private bool isChasing = false;

    private void Start()
    {
        if (endLevelObject != null)
            endLevelRb = endLevelObject.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasStarted && other.CompareTag("Player"))
        {
            hasStarted = true;

            playerTransform = other.transform;

            // Freeze player
            FreezePlayer(other.gameObject);

            // Play "Look" animation
            if (endLevelAnimator != null)
                endLevelAnimator.Play("Look");

            // Start coroutine to play "Run" and move
            StartCoroutine(StartChase());
        }
    }

    private IEnumerator StartChase()
    {
        yield return new WaitForSecondsRealtime(delayBeforeRun);

        if (endLevelAnimator != null)
            endLevelAnimator.Play("Run");

        isChasing = true;
    }

    private void Update()
    {
        if (isChasing && playerTransform != null && endLevelRb != null)
        {
            Vector2 direction = (playerTransform.position - endLevelObject.transform.position).normalized;
            endLevelRb.linearVelocity = direction * moveSpeed;
        }
    }

    private void FreezePlayer(GameObject player)
    {
        // Example: Disable movement script or input
        var playerInput = player.GetComponent<UnityEngine.InputSystem.PlayerInput>();
        if (playerInput != null)
            playerInput.enabled = false;

        var rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
}
