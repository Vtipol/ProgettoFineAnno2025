using UnityEngine;

public class PickThrow : MonoBehaviour
{
    [Header("References")]
    public PlayerStats Stats;
    private PickedUp PickedUp;
    private Player Player;
    public SoapState Soap;
    [SerializeField] private GameObject _pickUpPosition;
    [SerializeField] private Collider2D _pickTrigger;
    [SerializeField] private Animator _animator;

    [SerializeField] private LayerMask shellLayer;

    [SerializeField] private GameObject _pickTarget;
    [SerializeField] private GameObject _soapMode;
    private Rigidbody2D _targetRB;
    private Collider2D soapCollider;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Player = GetComponent<Player>();
        soapCollider = _soapMode.GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (_pickTarget != null)
        {
            _targetRB = _pickTarget.GetComponent<Rigidbody2D>();            
            PickedUp = _pickTarget.GetComponent<PickedUp>();

            // While being picked up, follow the pickup position
            if (PickedUp != null && PickedUp.IsPickedUp)
            {
                _pickTarget.transform.position = _pickUpPosition.transform.position;
            }
            else
            {
                // Just in case the Buddy somehow got stuck parented
                if (_pickTarget.transform.parent == _pickUpPosition.transform)
                    _pickTarget.transform.parent = null;
                _targetRB.bodyType = RigidbodyType2D.Dynamic;
                PickedUp.IsPickedUp = false;
            }

            // Throw if we stop holding Run
            if (PickedUp != null && PickedUp.IsPickedUp && !InputManager.RunIsHeld)
            {
                ThrowObject();
            }
        }

        UpdateAnimation();
    }

    #region PickUp

    private void OnTriggerEnter2D(Collider2D _pickTrigger)
    {
        // Check if the player is holding Run AND if the object collided is the one you're targeting
        if (InputManager.RunIsHeld == true && _pickTrigger.gameObject == _pickTarget && PickedUp.IsPickedUp == false)
        {
            //Debug.Log("Correct object triggered while running!");
            if (!Soap.IsSoapy)
            {
                // Set state
                PickedUp.IsPickedUp = true;

                // Optional: make it kinematic so it doesn't fall
                _targetRB.bodyType = RigidbodyType2D.Kinematic;
                _targetRB.linearVelocity = Vector2.zero;

                // Attach the object to follow the pickup position
                _pickTarget.transform.position = _pickUpPosition.transform.position;
                _pickTarget.transform.parent = _pickUpPosition.transform;

                // Play pickup animation
                //_animator.SetTrigger("PickUp");
            }
            else if (Soap.IsSoapy)
            {
                if (soapCollider != null)
                {
                    Soap soapScript = soapCollider.GetComponentInParent<Soap>();
                    if (soapScript != null)
                    {
                        Vector2 kickDirection = Player._isFacingRight ? Vector2.right : Vector2.left;

                        // Apply a stronger force when kicking
                        Rigidbody2D soapRB = soapCollider.GetComponentInParent<Rigidbody2D>();
                        if (soapRB != null)
                        {
                            soapRB.AddForce(kickDirection * Stats.KickForce, ForceMode2D.Impulse);  // Kick with force
                            Debug.Log($"[KICK] Soap kicked in direction: {kickDirection} with force: {Stats.KickForce}");
                        }
                        else
                        {
                            Debug.LogWarning("[KICK] soapCollider found, but no Rigidbody2D component on Soap object!");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("[KICK] soapCollider found, but Soap script missing!");
                    }
                }
                else
                {
                    Debug.LogWarning("[KICK] soapCollider reference is null!");
                }
            }
        }
    }

    #endregion

    #region Throw

    private void ThrowObject()
    {

        if (_pickTarget == null || _targetRB == null || Stats == null)
            return;

        // Mark as not picked up
        PickedUp.IsPickedUp = false;

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

        _animator.SetTrigger("Throw");
        Debug.Log($"[THROW] Threw object at direction {throwDir}, force {Stats.ThrowForce}");
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
