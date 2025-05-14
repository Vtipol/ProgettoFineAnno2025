using UnityEngine;

public class AvoidWallIssuesBuddy : MonoBehaviour
{
    private bool failSafe = false;
    private PickedUp isPickedUp;
    public bool FailSafe
    {
        get => failSafe;
        set => failSafe = value;
    }
    private void Awake()
    {
        isPickedUp = GetComponentInParent<PickedUp>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (isPickedUp)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                failSafe = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (isPickedUp)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                failSafe = false;
            }
        }
    }
}