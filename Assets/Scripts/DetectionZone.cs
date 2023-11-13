using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public UnityEvent noColliderRemain;
    public List<Collider2D> DetectedColliders = new List<Collider2D>();
    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
      DetectedColliders.Add(col); 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        DetectedColliders.Remove(other);
        if (DetectedColliders.Count <= 0)
        {
          noColliderRemain.Invoke();  
        }
    }
}
