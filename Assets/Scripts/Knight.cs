using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D),typeof(TouchingDirections),typeof(Damageable))]
public class Knight : MonoBehaviour
{
    public float walkAcceleration = 3f;
    public float maxspeed = 3f;
    public float walkStopRate = 0.6f;
    private Rigidbody2D rb;
    public enum WalkableDirection{Right,Left}

    private Animator animator;
    private Vector2 walkDirectionVector = Vector2.right;
    private WalkableDirection _walkDirection;
    private TouchingDirections touchingDirections;
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;
    public bool _hasTarget = false;
    private Damageable damageable;
    
    public WalkableDirection WalkDirection
    {
        get
        {
            return _walkDirection;
        }
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1,
                    gameObject.transform.localScale.y);
                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
        
        
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            
        }
    }

    public bool HasTarget
    {
        get
        {
            return _hasTarget;
        }
        set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget,value);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public float AttackCoolDown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.attackCoolDown);
        }
        set
        {
            animator.SetFloat(AnimationStrings.attackCoolDown,Mathf.Max(value,0));
        }
    }
    
    
    private void Update()
    {
        HasTarget = attackZone.DetectedColliders.Count > 0;
        
        if (AttackCoolDown > 0)
        {
            AttackCoolDown -= Time.deltaTime;  
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    private void FixedUpdate()
    {
        if (touchingDirections.isGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }

        if (damageable.LockVelocity)
        {
            if (CanMove && touchingDirections.isGrounded)
            {
              //  float xVelocity =
                //    Mathf.Clamp(rb.velocity.x + (walkAcceleration * walkDirectionVector.x * Time.fixedTime), -maxspeed,
                   //     maxspeed);
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + (walkAcceleration * walkDirectionVector.x * Time.fixedTime), -maxspeed,
                     maxspeed), rb.velocity.y);   
            }
            else
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x,0,walkStopRate), rb.velocity.y);
                
            }  
        }
        
    }

    public void OnHit(int damage,Vector2 knockback)
    {
       
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);   
    }

    public void OnCliffDetected()
    {
        if (touchingDirections.isGrounded)
        {
           FlipDirection(); 
        }
    }
    
}
