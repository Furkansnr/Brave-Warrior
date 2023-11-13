using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestore = 20;

    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);

    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Damageable damageable = col.GetComponent<Damageable>();
        if (damageable)
        {
          bool wasHealed = damageable.Heal(healthRestore);
          if (wasHealed)
              Destroy(gameObject);
        }
    }
}
