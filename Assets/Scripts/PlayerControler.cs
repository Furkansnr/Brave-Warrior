using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D),typeof(TouchingDirections),typeof(Damageable))]
public class PlayerControler : MonoBehaviour
{
    private Vector2 moveinput;
    [SerializeField]
    private bool _isMoving = false;
    [SerializeField]
    private bool _isRunning = false;
    public bool _isFacingRight = true;
    private TouchingDirections touchingDirections;
    public float jumpImpuls;
    private Damageable damageable;
    
    public bool Ismoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }
    // "isMoving"
    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }
    
    public float walkspeed = 5f;
    public float runspeed = 8f;
    public float airwalkspeed = 3f;
    private Rigidbody2D rb;
    private Animator animator;

    public float currentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (Ismoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.isGrounded)
                    {
                        if (IsRunning)
                        {
                            return runspeed;
                        }
                        else
                        {
                            return walkspeed;
                        }  
                    }
                    else
                    {
                        return airwalkspeed;
                    }
                }
                else
                {
                    //Movement Locked
                    return 0;
                }
            }
            
            
              // idle speed 0 olur.
            return 0;
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
        set
        {
            
        }
    }
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
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
    
    private void FixedUpdate()
    {
        if (damageable.LockVelocity)
        {
            rb.velocity = new Vector2(moveinput.x * currentMoveSpeed, rb.velocity.y);  
        }
        //rb.velocity = new Vector2(moveinput.x * currentMoveSpeed, rb.velocity.y);
        
        animator.SetFloat(AnimationStrings.yVelocity,rb.velocity.y);
    }

   public void OnMove(InputAction.CallbackContext context)
    {
        moveinput = context.ReadValue<Vector2>();

       // Ismoving = moveinput != Vector2.zero;
        if (IsAlive)
        {
            Ismoving = moveinput != Vector2.zero;  
            SetFacingDirection(moveinput);
        }
       // SetFacingDirection(moveinput);
        else
        {
            Ismoving = false;
        }
    }

   private void SetFacingDirection(Vector2 moveInput)
   {
       if (moveInput.x > 0 && !IsFacingRight)
       {
           IsFacingRight = true;
       } 
       else if (moveInput.x < 0 && IsFacingRight)
       {
           IsFacingRight = false;
       }
   }

   public bool IsFacingRight
   {
       get
       {
           return _isFacingRight;
       }
       set
       {
           if (_isFacingRight != value)
           {
               transform.localScale *= new Vector2(-1, 1);
           }

           _isFacingRight = value;
       }
   }

   public void OnRun(InputAction.CallbackContext context)
   {
       if (context.started)
       {
           IsRunning = true;
       }
       
       else if (context.canceled)
       {
           IsRunning = false;
       }
       
   }

   public void OnJump(InputAction.CallbackContext context)
   {
       // ölümü diye kontrol etmelisin
       if (context.started && touchingDirections.isGrounded && CanMove)
       {
          Debug.Log("zıpladı");
           animator.SetTrigger(AnimationStrings.jumpTrigger);
          rb.velocity = new Vector2(rb.velocity.x, jumpImpuls);
       }  
   }

   public void OnAttack(InputAction.CallbackContext context)
   {
       if (context.started)
       {
           animator.SetTrigger(AnimationStrings.attackTrigger);
       }  
   }

   public void OnRangedAttack(InputAction.CallbackContext context)
   {
       if (context.started)
       {
           animator.SetTrigger(AnimationStrings.rangedAttackTrigger);
       }  
   }
   
   public void OnHit(int damage, Vector2 knockback)
   {
       rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
   }
}
