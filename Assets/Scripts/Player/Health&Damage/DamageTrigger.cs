using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    [Tooltip("Damage to apply when triggered.")]
    public int damageAmount = 1;

    [Tooltip("Set this to the layer of the enemy objects.")]
    public LayerMask enemyLayer;

    private PlayerHealth playerHealth;

    private void Awake()
    {
        // Attempt to get the PlayerHealth component from the same GameObject.
        playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            Debug.LogError("DamageTrigger: No PlayerHealth component found on this GameObject.");
        }
    }

    // Example: Use collision to trigger damage based on layer.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object's layer is within the enemyLayer LayerMask.
        if ((enemyLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            playerHealth.TakeDamage(damageAmount);
        }
    }

    // Alternatively, you can expose a public method to apply damage externally.
    public void ApplyDamage()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
        }
    }
}
