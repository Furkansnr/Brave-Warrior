using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class togglebut : MonoBehaviour
{
    public Toggle secim;
    //public TextMeshProUGUI metin;
    public GameObject panel;
    
    void Start()
    {
        panel.SetActive(false);
        secim = GetComponent<Toggle>();
        
    }


    public void Bolumsec()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void nasıl()
    {
       panel.SetActive(true); 
    }

    public void panelkapa()
    {
        panel.SetActive(false);
    }

    public void cıkıs()
    {
        Application.Quit();
    }
    public void durumdegis(bool isOn )
    {
        if (isOn == true)
        {
          PlayerPrefs.SetInt("juice", 1);
          Debug.Log("acık");
          PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt("juice",0);
            Debug.Log("kapalı");
            PlayerPrefs.Save();
        }
        
        
    }
    
}
