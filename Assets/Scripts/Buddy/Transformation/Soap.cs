using UnityEngine;

public class Soap : MonoBehaviour
{
    [SerializeField] private BoxCollider2D soapCollider;
    public PickedUp pickedUp;
    public PickThrow pickThrow;
    public Player player;
    public PlayerStats Stats;
    public SoapState soap;

    [SerializeField] private float trampolineBounceForce = 25f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !pickedUp.IsPickedUp)
        {
            Debug.Log("Entered Soap Collider");
        }
    }

    private bool isMoving = false;
    [SerializeField] private GameObject _pickTarget;
    private Rigidbody2D _targetRB;

    private void Awake()
    {
        _targetRB = _pickTarget.GetComponent<Rigidbody2D>();
    }


    #region Kick

    public void Kick(Vector2 direction)
    {
        isMoving = true;
        _targetRB.linearVelocity = Vector2.zero;
        _targetRB.AddForce(direction.normalized * Stats.KickForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Example: Damage an enemy or bounce off walls
        if (isMoving)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                // Or trigger a damage function
            }

            // Stop on hitting a wall or player depending on your logic
            if (collision.gameObject.CompareTag("Ground"))
            {
                StopShell();
            }
        }
    }     

    public void StopShell()
    {
        isMoving = false;
        _targetRB.linearVelocity = Vector2.zero;
    }

    #endregion

    #region Bounce

    public void Bounce()
    {
        if (InputManager.AttackDownExecuted && player.IsFalling)
        {
           // BouncePlayer(player);
        }
    }
    private void BouncePlayer(Player player)
    {
        player.OverrideJump(trampolineBounceForce);
    }

    #endregion
}