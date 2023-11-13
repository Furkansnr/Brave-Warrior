using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public Vector3 movespeed = new Vector3(0, 75, 0);
    public float timeToFade = 1f;
    private RectTransform textTransform;
    private TextMeshProUGUI textMeshPro;
    private float timeElapsed;
    private Color startcolor;

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startcolor = textMeshPro.color;
    }

    private void Update()
    {
        textTransform.position += movespeed * Time.deltaTime;

        timeElapsed += Time.deltaTime;

        if (timeElapsed < timeToFade)
        {
            float fadeAlpha = startcolor.a * (1 - (timeElapsed / timeToFade));
            textMeshPro.color = new Color(startcolor.r, startcolor.g, startcolor.b,fadeAlpha);
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
