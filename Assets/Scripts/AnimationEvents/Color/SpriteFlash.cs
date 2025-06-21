using UnityEngine;

public class SpriteFlash : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    // Call this from an Animation Event
    public void FlashRed()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
        }
    }

    public void FlashBlue()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.blue;
        }
    }

    public void FlashYellow()
    {
        if(spriteRenderer != null)
        {
            spriteRenderer.color = Color.yellow;
        }
    }

    public void ResetColor()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }
}
