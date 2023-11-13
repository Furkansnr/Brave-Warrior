using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Collider2D attackColllider;
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;
    private void Awake()
    {
        attackColllider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Damageable damageable = col.GetComponent<Damageable>();

        if (damageable != null)
        {
            Vector2 deliveredKnockback =
                transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            bool gotHit = damageable.Hit(attackDamage,deliveredKnockback);
         if (gotHit)
         {
             Debug.Log(col.name + "hit" + attackDamage);  
         }
          //Debug.Log(col.name + "hit" + attackDamage);
        }
    }
}
