using UnityEngine;

public class PickedUp : MonoBehaviour
{
    private bool isPickedUp = false;

    public bool IsPickedUp
    {
        get => isPickedUp;
        set
        {
            //if (value && !isPickedUp)
            //{
            //    DisableOtherScripts();
            //}
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
//<<<<<<< Updated upstream

//    private void DisableOtherScripts()
//    {
//        if (followPlayer != null) followPlayer.enabled = false;
//        if (stateController != null) stateController.enabled = false;
//    }
//    public void OnCollisionEnter2D(Collision2D collision)
//    {
//        IsPickedUp = false;
//        if (followPlayer != null) followPlayer.enabled = true;
//        if (stateController != null) stateController.enabled = true;
//=======
//    public void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.collider == groundCollider)
//        {
//            IsPickedUp = false;
//        }
//>>>>>>> Stashed changes
//    }
//    //void Update()
//    //{
//    //    if (Input.GetKeyDown(KeyCode.P))
//    //    {
//    //        PickedUp pickup = buddy.GetComponent<PickedUp>();
//    //        pickup.IsPickedUp = true; // This will trigger DisableOtherScripts() esempio per chiamare lo script
//    //    }
//    //}
}
