using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Rain : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private bool _isRain;
    public Light directionalLight;

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        StartCoroutine(Weather());
    }

    private void Update()
    {
        if (_isRain && directionalLight.intensity > 0.25f)
        {
            LightIntensity(-1);
        } 
        else if (!_isRain && directionalLight.intensity < 0.5f)
        {
            LightIntensity(1);
        }
        
    }

    private void LightIntensity(int multiply)
    {
        directionalLight.intensity += 0.1f * Time.deltaTime * multiply;
    }

    IEnumerator Weather()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(60.0f,180.0f));

            if (_isRain)
            {
                _particleSystem.Stop();
            }
            else
            {
                _particleSystem.Play();
            }

            _isRain = !_isRain; 
        }
    }
    
}
