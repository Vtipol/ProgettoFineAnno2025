using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHearts = 5;
    public int currentHearts;

    [Header("Immunity Settings")]
    [Tooltip("Duration (in seconds) for which the player is invulnerable after taking damage.")]
    public float invulnerabilityDuration = 1f;
    private bool isInvulnerable = false;

    [Header("UI Reference")]
    public LifeUI lifeUI;

    [Header("References")]
    [SerializeField] private Animator _animator;           // Reference to the Animator component
    [SerializeField] private PlayerMovement _playerMovement; // Reference to the player movement script

    private void Start()
    {
        // Set starting health to max hearts
        currentHearts = maxHearts;

        // Initialize the UI if assigned
        if (lifeUI != null)
        {
            lifeUI.Reset();
        }
        else
        {
            Debug.LogWarning("PlayerHealth: No LifeUI assigned!");
        }

        // Auto-find references if not assigned in Inspector.
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
        if (_playerMovement == null)
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }
    }

    /// <summary>
    /// Call this method to deal damage to the player.
    /// </summary>
    /// <param name="damageAmount">The amount of damage (number of hearts to remove)</param>
    public void TakeDamage(int damageAmount = 1)
    {
        // If the player is currently invulnerable, ignore damage.
        if (isInvulnerable)
            return;

        currentHearts = Mathf.Max(currentHearts - damageAmount, 0);

        if (lifeUI != null)
        {
            lifeUI.UpdateUI(currentHearts);
        }

        if (_animator != null)
        {
            _animator.SetTrigger("Damaged");
        }

        // Start the invulnerability window
        StartCoroutine(InvulnerabilityCoroutine());

        if (currentHearts <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Call this method to heal the player.
    /// </summary>
    /// <param name="healAmount">The amount of hearts to add</param>
    public void Heal(int healAmount = 1)
    {
        currentHearts = Mathf.Min(currentHearts + healAmount, maxHearts);

        if (lifeUI != null)
        {
            lifeUI.UpdateUI(currentHearts);
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");

        if (_animator != null)
        {
            _animator.SetTrigger("Dead");
        }

        if (_playerMovement != null)
        {
            _playerMovement.enabled = false;
        }

        // Stop sliding by zeroing the Rigidbody's velocity and disabling its simulation.
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }
    }

    // Coroutine to handle temporary invulnerability.
    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }

    // Detect collision with enemy objects to trigger damage.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            TakeDamage(1);
        }
    }
}
