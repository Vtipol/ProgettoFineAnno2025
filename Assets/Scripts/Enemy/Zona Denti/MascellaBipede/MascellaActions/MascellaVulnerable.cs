using System;
using UnityEngine;

public class MascellaVulnerable : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D VulnerableSpot; 
    private Rigidbody2D _rb;
    public bool receivedHit = false;
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
        receivedHit = true;
    }
    public void Die()
    {
        if (!Damage._isAlive)
        {
            Destroy(transform.parent.gameObject);
        }
    }

}
