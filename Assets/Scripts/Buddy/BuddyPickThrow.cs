using System;
using Unity.VisualScripting;
using UnityEngine;

public class BuddyPickThrow : MonoBehaviour
{
    private CircleCollider2D collider;
    private PickThrow pickThrow;
    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
        pickThrow = FindAnyObjectByType<PickThrow>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collider.IsTouchingLayers(LayerMask.GetMask("Ground"))) 
        {
            pickThrow.isPickThrow = false;
        }
    }
}
