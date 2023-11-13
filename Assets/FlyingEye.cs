using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    public float flightSpeed = 2f;
    public DetectionZone biteDetectionZone;
    private Animator animator;
    private Rigidbody2D rb;
    public List<Transform> waypoints;
    private Damageable damageable;
    private int waypointnum = 0;
    private Transform nextWaypoint;
    public float waypointReachedDistance = 0.1f;
    public Collider2D deathcollider;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }

    private void Start()
    {
        nextWaypoint = waypoints[waypointnum];
    }


    public bool _hasTarget = false;
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
    void Update()
    {

        HasTarget = biteDetectionZone.DetectedColliders.Count > 0;

    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }
    
    
    
    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {
            if (CanMove)
            {
                Flight();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
            
        }
        else
        {
           /* rb.gravityScale = 2f;
            rb.velocity = new Vector2(0, rb.velocity.y);
            deathcollider.enabled = true; */
        }
        
    }

    private void Flight()
    {
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;

        float distance = Vector2.Distance(nextWaypoint.position, transform.position);

        rb.velocity = directionToWaypoint * flightSpeed;
        UpdateDirection();

        if (distance <= waypointReachedDistance)
        {
            waypointnum++;
            if (waypointnum >= waypoints.Count)
            {
                waypointnum = 0;
            }

            nextWaypoint = waypoints[waypointnum];
        }
    }

    public void UpdateDirection()
    {
        Vector3 LocScale = transform.localScale;
        
        if (transform.localScale.x > 0)
        {
            if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1 * LocScale.x, LocScale.y, LocScale.z);
            }  
        }
        else
        {
            if (rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(-1 * LocScale.x, LocScale.y, LocScale.z);
            }    
        }
        
    }

    private void OnEnable()
    {
        damageable.damageableDeath.AddListener(OnDeath); 
    }

    public void OnDeath()
    {
        rb.gravityScale = 2f;
        rb.velocity = new Vector2(0, rb.velocity.y);
        deathcollider.enabled = true;  
    }
}
