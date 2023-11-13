using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEngine;

public class cinemachineshake : MonoBehaviour
{
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private void Awake()
    {
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float shakeİntensity)
    {
        if (PlayerPrefs.GetInt("juice")== 1)
        {
            CinemachineBasicMultiChannelPerlin cbmp =
                _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cbmp.m_AmplitudeGain = shakeİntensity;
              Debug.Log("sallandı");
            StartCoroutine(StopShake());
        }
    }

    IEnumerator StopShake()
    {
        CinemachineBasicMultiChannelPerlin cbmp=_cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        yield return new WaitForSeconds(0.2f);
        cbmp.m_AmplitudeGain = 0f;
    }
   
}

