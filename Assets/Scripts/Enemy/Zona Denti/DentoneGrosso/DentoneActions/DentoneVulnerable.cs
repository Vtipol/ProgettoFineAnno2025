using UnityEngine;

public class DentoneVulnerable : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D VulnerableSpot;
    private Rigidbody2D _rb;
    public Damageble Damage;
    private void Awake()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
        Damage = GetComponent<Damageble>();
        Damage.damagebleHit.AddListener(OnHit);
    }
    public bool ActivateWeakSpot(bool Activate)
    {
        if (Activate)
        {
            VulnerableSpot.enabled = Activate;
        }
        else
        {
            VulnerableSpot.enabled = Activate;
        }
        return Activate;
    }

    public void OnHit(int damage, Vector2 knokback)
    {
        _rb.linearVelocity = new Vector2(knokback.x, _rb.linearVelocity.y + knokback.y);
    }
}
