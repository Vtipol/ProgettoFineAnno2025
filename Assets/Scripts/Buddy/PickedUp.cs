using UnityEngine;
using System;
public class PickedUp : MonoBehaviour
{
    private bool isPickedUp = false;
    [SerializeField] private Collider2D groundCollider;
    [SerializeField] private LayerMask groundLayers;
    private Animator _animator;
    [SerializeField] private Rigidbody2D rb;
    private bool hasBeenThrown = false;
    public bool IsPickedUp
    {
        get => isPickedUp;
        set
        {
            isPickedUp = value;
            if (!value)
            {
                hasBeenThrown = true;
                Invoke(nameof(ResetThrowFlag), 0.2f);
            }
        }
    }

    private FollowPlayer followPlayer;
    private BuddyStateController stateController;

    void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        followPlayer = GetComponent<FollowPlayer>();
        stateController = GetComponent<BuddyStateController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateAnimation();
    }

    private bool _wasPickedUpLastFrame;

    private void UpdateAnimation()
    {
        _animator.SetBool("PickedUp", isPickedUp);
        if (!isPickedUp && _wasPickedUpLastFrame)
        {
            _animator.SetTrigger("Thrown");
        }
        _wasPickedUpLastFrame = isPickedUp;
    }
    private void ResetThrowFlag()
    {
        hasBeenThrown = false;
    }
    
}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (!hasBeenThrown && isPickedUp && ((1 << collision.gameObject.layer) & groundLayers) != 0)
    //    {
    //        Debug.Log("Buddy hit ground while being picked up. Releasing...");
    //        transform.parent = null;
    //        IsPickedUp = false;

    //            rb.bodyType = RigidbodyType2D.Dynamic;
    //            rb.linearVelocity = Vector2.zero;
            
    //    }
    //}

