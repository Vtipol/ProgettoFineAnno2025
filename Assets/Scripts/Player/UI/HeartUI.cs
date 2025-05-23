using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    [SerializeField] private Damageble damageble;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private Image[] hearts;

    private void Start()
    {
        if (damageble != null)
        {
            UpdateHearts(damageble.Health);
            damageble.damagebleHit.AddListener(OnDamageTaken);
        }
    }

    private void OnDestroy()
    {
        if (damageble != null)
        {
            damageble.damagebleHit.RemoveListener(OnDamageTaken);
        }
    }

    private void OnDamageTaken(int damage, Vector2 knockback)
    {
        UpdateHearts(damageble.Health);
    }

    private void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = (i < currentHealth) ? fullHeart : emptyHeart;
        }
    }
}
