using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100, _health = 100;
    private Animator animator;
    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent damageableDeath;
    public cinemachineshake cine;
    

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
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
        
    }
[SerializeField]
    private bool _isAlive = true;
    [SerializeField]
    private bool isInvincible;
    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive,value);
            Debug.Log("isAliveSet "+ value);

            if (value == false)
            {
              damageableDeath.Invoke();  
            }
        }
    }
    
    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
        
    }
    public bool LockVelocity
    {
        get
        {
            return  animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity,value); 
        }
    }
  /*  public bool IsHit
    {
        get
        {
           return animator.GetBool(AnimationStrings.isHit);
        }
        set
        {
           animator.SetBool(AnimationStrings.isHit, value); 
        }
    }
    */
    private void Awake()
    {
        animator = GetComponent<Animator>();
        cine = GameObject.FindWithTag("shake").GetComponent<cinemachineshake>();
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            damageableHit?.Invoke(damage,knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
           cine.ShakeCamera(3f);
            return true;
        }

        return false;
    }

    public bool Heal(int healthRestored)
    {
        if (IsAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestored);
            Health += actualHeal;
            CharacterEvents.characterHealed(gameObject, actualHeal);
            return true;
        }
        return false;
    }
    
    
}
