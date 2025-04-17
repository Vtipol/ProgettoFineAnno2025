using System.Collections;
using UnityEngine;

public class Gum : MonoBehaviour
{
    [SerializeField] private CircleCollider2D gumCollider;
    public PickedUp pickedUp;
    public PickThrow pickThrow;
    
    private void Start()
    {
        pickedUp = GetComponentInParent<PickedUp>();
        pickThrow = GetComponent<PickThrow>();
    }

    private void Update()
    {
        if (pickedUp.IsPickedUp)
        {
            EnableWallStick();
        }
        else
        {
            DisableWallStick();
        }
    }

    private void EnableWallStick()
    {
        // Activate wall stick behavior when Gum is picked up
        PlayerWallStick playerWallStick = transform.root.GetComponent<PlayerWallStick>();
        if (playerWallStick != null)
        {
            playerWallStick.EnableWallStick();
        }
    }

    private void DisableWallStick()
    {
        // Deactivate wall stick behavior when Gum is not picked up
        PlayerWallStick playerWallStick = transform.root.GetComponent<PlayerWallStick>();
        if (playerWallStick != null)
        {
            playerWallStick.DisableWallStick();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !pickedUp.IsPickedUp)
        {
            Debug.Log("Entered Gum Collider");
        }

    }
}