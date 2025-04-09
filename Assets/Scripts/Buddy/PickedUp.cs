using UnityEngine;

public class PickedUp : MonoBehaviour
{
    private bool isPickedUp = false;
    [SerializeField] private CircleCollider2D groundCollider;

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
    
/*
   public void OnCollisionEnter2D(Collision2D collision)
   {
       if (collision.collider.gameObject == groundCollider.gameObject)       {
            Debug.Log("PickedUp is false");
          IsPickedUp = false;
        }

    }*/
//    //void Update()
//    //{
//    //    if (Input.GetKeyDown(KeyCode.P))
//    //    {
//    //        PickedUp pickup = buddy.GetComponent<PickedUp>();
//    //        pickup.IsPickedUp = true; // This will trigger DisableOtherScripts() esempio per chiamare lo script
//    //    }
//    //}
}
