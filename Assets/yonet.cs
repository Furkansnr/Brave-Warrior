using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class yonet : MonoBehaviour
{
    public GameObject panel;


    private void Start()
    {
        panel.SetActive(false);
    }

    public void panelac()
    {
      panel.SetActive(true);
      Time.timeScale = 0;
    }

    public void panelkapa()
    {
      panel.SetActive(false);
      Time.timeScale = 1;
    }

    public void menu()
    {
        SceneManager.LoadScene(0);
    }

    public void cıkıs()
    {
        Application.Quit();
    }
    
    
}
