using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class kapı1 : MonoBehaviour
{
    public DetectionZone canavarzone;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (canavarzone.DetectedColliders.Count <= 0)
        {
            SceneManager.LoadScene(2);
        } 
    }

    public void OnDetected()
    {
        
    }
    
}
