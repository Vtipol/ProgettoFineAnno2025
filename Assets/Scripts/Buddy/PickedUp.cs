using UnityEngine;
using System;
public class PickedUp : MonoBehaviour
{
    private bool isPickedUp = false;
    [SerializeField] private Collider2D groundCollider;
    public bool IsPickedUp
    {
        get => isPickedUp;
        set
        {
            isPickedUp = value;
        }
    }
    private FollowPlayer followPlayer;
    private BuddyStateController stateController;
    void Awake()
    {
        followPlayer = GetComponent<FollowPlayer>();
        stateController = GetComponent<BuddyStateController>();
    }
    public void OnCollisionEnter2D(Collision2D groundCollision)
    {
        IsPickedUp = false;
        if (stateController != null) stateController.enabled = true;
    }
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.P))
    //    {
    //        PickedUp pickup = buddy.GetComponent<PickedUp>();
    //        pickup.IsPickedUp = true; // This will trigger DisableOtherScripts() esempio per chiamare lo script
    //    }
    //}
}
