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
    private AvoidWallIssuesBuddy buddyWall;
    private Rigidbody2D _targetRB;
    private Collider2D soapCollider;
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
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Player = GetComponent<Player>();
        soapCollider = _soapMode.GetComponent<Collider2D>();
        buddyWall = _pickTarget.GetComponentInChildren<AvoidWallIssuesBuddy>();
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
            //else
            //{
            //    // Just in case the Buddy somehow got stuck parented
            //    if (_pickTarget.transform.parent == _pickUpPosition.transform)
            //        _pickTarget.transform.parent = null;
            //    _targetRB.bodyType = RigidbodyType2D.Dynamic;
            //    PickedUp.IsPickedUp = false;
            //}

            // Throw if we stop holding Run
            if (PickedUp != null && PickedUp.IsPickedUp && !InputManager.RunIsHeld && !buddyWall.FailSafe)
            {
                ThrowObject();
            }
        }

        UpdateAnimation();
    }

    #region PickUp

    private void OnTriggerEnter2D(Collider2D _pickTrigger)
    {
        if (InputManager.RunIsHeld == true && _pickTrigger.gameObject == _pickTarget && PickedUp.IsPickedUp == false)
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

    #region Throw

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
