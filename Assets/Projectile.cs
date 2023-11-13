using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 movespeed = new Vector2(3f, 0);
    public int damage = 10;
    private Rigidbody2D rb;
    public Vector2 knockback = new Vector2(0, 0);

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.velocity = new Vector2(movespeed.x * transform.localScale.x, movespeed.y);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Damageable damageable = col.GetComponent<Damageable>();

        if (damageable != null)
        {
            Vector2 deliveredKnockback =
                transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            bool gotHit = damageable.Hit(damage,deliveredKnockback);
            if (gotHit)
            {
                Debug.Log(col.name + "hit" + damage); 
                //juice icin yapılandırabilirsin
                Destroy(gameObject);
            }  
        }
    }
}
