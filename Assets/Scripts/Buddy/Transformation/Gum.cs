using System.Collections;
using UnityEngine;

public class Gum : MonoBehaviour
{
    [SerializeField] private CircleCollider2D gumCollider;
    public PickThrow pickThrow;

    [SerializeField] private GameObject player; // Drag your player GameObject here
    [SerializeField] private PickedUp pickedUp; // Drag the script that has IsPickedUp

    private WallJumpHandler wallJumpHandler;
    private bool wasPickedUp = false;

    private void Start()
    {
        if (player != null)
        {
            wallJumpHandler = player.GetComponent<WallJumpHandler>();
            if (wallJumpHandler == null)
            {
                Debug.LogWarning("WallJumpHandler not found on player.");
            }
        }
    }

    private void Update()
    {
        if (pickedUp == null || wallJumpHandler == null)
            return;

        if (pickedUp.IsPickedUp && !wasPickedUp)
        {
            wallJumpHandler.EnableWallJump(3);
            wasPickedUp = true;
        }
        else if (!pickedUp.IsPickedUp && wasPickedUp)
        {
            wallJumpHandler.DisableWallJump();
            wasPickedUp = false;
        }
    }
}