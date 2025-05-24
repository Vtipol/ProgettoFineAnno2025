using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class PickThrow : MonoBehaviour
{
    [Header("References")]
    public PlayerStats Stats;
    private PickedUp PickedUp;
    private Player Player;
    public SoapState Soap;
    //private Gum gum;
    [Header("Object")]
    [SerializeField] private GameObject _pickUpPosition;
    [SerializeField] private Collider2D _pickTrigger;
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask shellLayer;
    [SerializeField] private GameObject _pickTarget;
    [SerializeField] private GameObject _soapMode;
    private AvoidWallIssuesBuddy buddyWall;
    private Trampoline buddyTrampoline;
    private Rigidbody2D _targetRB;
    // private Collider2D soapCollider;
    [Header("Var")]
    [SerializeField]private bool isPickUpBlocked = false;
    [SerializeField] private float pickUpBlockDuration = 1f;

    public bool isPickThrow = false;
    public bool IsPickThrow
    {
        get => isPickThrow;
        set
        {
            isPickThrow = value;
            
        }
    }
    private void Awake()
    {
       // gum = FindAnyObjectByType<Gum>();
        _animator = GetComponent<Animator>();
        _targetRB = _pickTarget.GetComponent<Rigidbody2D>();            
        PickedUp = _pickTarget.GetComponent<PickedUp>();
    }

    private void Start()
    {
        Player = GetComponent<Player>();
    //   soapCollider = _soapMode.GetComponent<Collider2D>();
        buddyWall = _pickTarget.GetComponentInChildren<AvoidWallIssuesBuddy>();
        buddyTrampoline = _pickTarget.GetComponentInChildren<Trampoline>();
    }

    private void Update()
    {
        if (_pickTarget != null)
        {
            //_targetRB = _pickTarget.GetComponent<Rigidbody2D>();            
            //PickedUp = _pickTarget.GetComponent<PickedUp>();

            // While being picked up, follow the pickup position
            if (PickedUp != null && PickedUp.IsPickedUp)
            {
                _pickTarget.transform.position = _pickUpPosition.transform.position;
            }
            //else
            //{
            //    // Just in case the Buddy somehow got stuck parented
            //    if (_pickTarget.transform.parent == _pickUpPosition.transform)
            //        _pickTarget.transform.parent = null;
            //    _targetRB.bodyType = RigidbodyType2D.Dynamic;
            //    PickedUp.IsPickedUp = false;
            //}

            // Handle throw input (RMB held + LMB pressed)
            if (PickedUp != null && PickedUp.IsPickedUp && InputManager.RunIsHeld && Input.GetMouseButtonDown(0) && !buddyWall.FailSafe)
            {
                ThrowObject();
            }
            // Handle release input (RMB released)
            else if (PickedUp != null && PickedUp.IsPickedUp && !InputManager.RunIsHeld)
            {
                ReleaseObject();
            }

        }
        UpdateAnimation();
    }

    #region PickUp
    public void TryAutoPickBuddy()
    {
        if (!isPickUpBlocked && InputManager.RunIsHeld && !PickedUp.IsPickedUp && !Soap.IsSoapy && buddyTrampoline.trampGrabCollider.enabled)
        {

            PickedUp.IsPickedUp = true;
            _targetRB.bodyType = RigidbodyType2D.Kinematic;
            _targetRB.linearVelocity = Vector2.zero;
            _pickTarget.transform.position = _pickUpPosition.transform.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D _pickTrigger)
    {
        if (!isPickUpBlocked && InputManager.RunIsHeld && _pickTrigger.gameObject == _pickTarget && PickedUp.IsPickedUp == false)
        {;
            if (!Soap.IsSoapy)
            {
                // Set state
                PickedUp.IsPickedUp = true;

                // Optional: make it kinematic so it non cade
                _targetRB.bodyType = RigidbodyType2D.Kinematic;
                _targetRB.linearVelocity = Vector2.zero;

                // Attach the object to follow the pickup position
                Vector3 offset = _pickUpPosition.transform.position - _pickTarget.transform.position;
                _targetRB.MovePosition(_pickTarget.transform.position + offset);

                // Sincronizza la direzione del buddy con quella del player
                Vector3 scale = _pickTarget.transform.localScale;
                scale.x = transform.localScale.x > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
                _pickTarget.transform.localScale = scale;

                Debug.Log("Buddy raccolto: direzione sincronizzata con il player.");
            }
        }
    }
    private void OnTriggerStay2D(Collider2D _pickTrigger)
    {
        if (!isPickUpBlocked && InputManager.RunIsHeld && _pickTrigger.gameObject == _pickTarget && PickedUp.IsPickedUp == false)
        {
            if (!Soap.IsSoapy)
            {
                // Set state
                PickedUp.IsPickedUp = true;

                // Optional: make it kinematic so it non cade
                _targetRB.bodyType = RigidbodyType2D.Kinematic;
                _targetRB.linearVelocity = Vector2.zero;

                // Attach the object to follow the pickup position
                Vector3 offset = _pickUpPosition.transform.position - _pickTarget.transform.position;
                _targetRB.MovePosition(_pickTarget.transform.position + offset);

                // Sincronizza la direzione del buddy con quella del player
                Vector3 scale = _pickTarget.transform.localScale;
                scale.x = transform.localScale.x > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
                _pickTarget.transform.localScale = scale;

                Debug.Log("Buddy raccolto: direzione sincronizzata con il player.");
            }
        }
    }
    #endregion

    #region Throw & Release

    private void ThrowObject()
    {
        if (_pickTarget == null || _targetRB == null || Stats == null)
            return;

        // Mark as not picked up
        PickedUp.IsPickedUp = false;
        isPickThrow = true;

        // Detach from player or pickup position
        _pickTarget.transform.parent = null;

        // Disable kinematic, reset velocity
        _targetRB.bodyType = RigidbodyType2D.Dynamic;
        _targetRB.linearVelocity = Vector2.zero;
        _targetRB.angularVelocity = 0f;

        // Re-enable collision if it was disabled
        Collider2D objCollider = _pickTarget.GetComponent<Collider2D>();
        if (objCollider != null)
            objCollider.enabled = true;

        // Optional: Add a small offset so it doesn’t overlap the player collider
        _pickTarget.transform.position += new Vector3(0.2f * transform.localScale.x, 0.2f, 0f);

        // Calculate direction based on player facing
        float angleRad = Stats.ThrowAngle * Mathf.Deg2Rad;
        Vector2 throwDir = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized;

        // Adjust direction based on whether the player is facing right or left
        if (!Player._isFacingRight)
        {
            throwDir.x *= -1f; // Flip direction if the player is facing left
        }

        // Apply force
        _targetRB.AddForce(throwDir * Stats.ThrowForce, ForceMode2D.Impulse);

        // Sincronizza la direzione del buddy con quella del lancio
    /*  Vector3 scale = _pickTarget.transform.localScale;
        scale.x = throwDir.x > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        _pickTarget.transform.localScale = scale;
    */
        _animator.SetTrigger("Throw");
        Debug.Log($"[THROW] Threw object at direction {throwDir}, force {Stats.ThrowForce}");

        StartCoroutine(PickUpBlock());
    }

    private void ReleaseObject()
    {
        if (_pickTarget == null || _targetRB == null)
            return;

        PickedUp.IsPickedUp = false;
        isPickThrow = false;

        _pickTarget.transform.parent = null;

        _targetRB.bodyType = RigidbodyType2D.Dynamic;
        _targetRB.linearVelocity = Vector2.zero;
        _targetRB.angularVelocity = 0f;

        Collider2D objCollider = _pickTarget.GetComponent<Collider2D>();
        if (objCollider != null)
            objCollider.enabled = true;

        Debug.Log("[RELEASE] Object gently released without throwing.");

        StartCoroutine(PickUpBlock());
    }


    #endregion

    #region Timer

    private IEnumerator PickUpBlock()
    {
        isPickUpBlocked = true;
        yield return new WaitForSeconds(pickUpBlockDuration);
        isPickUpBlocked = false;
    }

    #endregion

    #region Animation

    private void UpdateAnimation()
    {
        bool PickUp = PickedUp.IsPickedUp;
        _animator.SetBool("PickUp", PickUp);
        
    }

    #endregion
}
