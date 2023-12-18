using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RandomLightning : MonoBehaviour
{
    #region Serializables

    [SerializeField]
    private int _minimalSeccondsInterval;
    [SerializeField]
    private float _decreaseScale;

    #endregion

    #region Miembros privados

    private Light2D _light;
    private Stopwatch _sw;
    private TimeSpan _ts;
    private System.Random _random;
    private AudioSource _sound;
    private Coroutine _coroutine;

    #endregion

    #region Eventos de Unity

    // Start is called before the first frame update
    void Start() 
    { 
        _light = GetComponent<Light2D>();
        _sound = GetComponent<AudioSource>();
        _sw = new Stopwatch();
        _ts = new TimeSpan(0, 0, _minimalSeccondsInterval);
        _random = new();
        _sw.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (_sw.Elapsed >= _ts)
            if (_random.Next(1, 10) > 5)
            { 
                _coroutine = StartCoroutine(Lightning());
                _sw.Stop();
                _sw.Reset();
            }
    }

    #endregion

    #region Metodos privados

    private void IncreaseIntensity() => _light.intensity = 5;

    private void DecreaseIntensity() 
    {
        _light.intensity -= _decreaseScale;
        if (_light.intensity < 1)
        { 
            _light.intensity = 1;
            StopCoroutine(_coroutine);
            _sw.Start();
        }
    }

    private IEnumerator Lightning()
    {
        _sound.Play();
        IncreaseIntensity();
        while (true) 
        {
            DecreaseIntensity();
            yield return new WaitForSeconds(.2F);
        }

    }

    #endregion
}
