using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Damageble : MonoBehaviour
{
    public UnityEvent<int, Vector2> damagebleHit;

    [SerializeField]Animator animator;
    [SerializeField]
    private int _maxHealth = 100;

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private int _health = 100;
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;

            // If health below or equal to 0, character is no longer alive
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    public bool _isAlive = true;
    [SerializeField]
    private bool isInvincible = false;
    [SerializeField]
    private float timeSinceHit = 0;
    [SerializeField]
    private float invincibilityTime = 0.25f;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        private set
        {
            _isAlive = value;
            animator.SetBool("isAlive", value);
        }
    }

    /*public bool LockVelocity
    {
        get
        {
            return animator.GetBool("LockVelocity");
        }
        set
        {
            animator.SetBool("LockVelocity", value);
        }
    }*/

    private void Awake()
    {
        //animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                // Remove Invincibility
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }

    /*public void Hit(int damage, Vector2 knokback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            // Notify the othe conponent to handle the knokback
            animator.SetTrigger("Hit");
            LockVelocity = true;
            damagebleHit?.Invoke(damage, knokback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
        }
    }*/

    public void Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            animator.SetTrigger("Hit");
            //LockVelocity = true;

            // Apply knockback directly here
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero; // Optional: reset velocity
                rb.AddForce(knockback, ForceMode2D.Impulse);
            }

            damagebleHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged?.Invoke(gameObject, damage);
        }
    }
}
