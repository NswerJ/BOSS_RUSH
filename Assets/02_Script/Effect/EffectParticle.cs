using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectParticle : MonoBehaviour
{
    ParticleSystem _effect;

    private void Awake()
    {
        _effect = GetComponent<ParticleSystem>();
        if (_effect == null)
            Debug.LogError("ParticleSystem is null");
    }

    public void PlayParticle()
    {
        _effect.Play();
    }
    public void PlayParticle(Vector3 scale)
    {
        transform.localScale = scale;
        _effect.Play();
    }

    public void StopParticle()
    {
        _effect.Stop();
    }

}
