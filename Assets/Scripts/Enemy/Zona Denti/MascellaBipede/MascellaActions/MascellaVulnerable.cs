using UnityEngine;

public class MascellaVulnerable : MonoBehaviour
{
    private Rigidbody2D _rb;
    public Damageble Damage;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Damage = GetComponent<Damageble>();
        Damage.damagebleHit.AddListener(OnHit);
    }
    
    public void OnHit(int damage, Vector2 knokback)
    {
        _rb.linearVelocity = new Vector2(knokback.x, _rb.linearVelocity.y + knokback.y);
    }

}
