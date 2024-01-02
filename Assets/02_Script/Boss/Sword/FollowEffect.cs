using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowEffect : MonoBehaviour
{
    [SerializeField] GameObject root;
    ParticleSystem particle;

    ParticleSystem.MainModule mainModule;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
        mainModule = particle.main;
    }

    private void Update()
    {
        mainModule.startRotation = -transform.eulerAngles.z * Mathf.Deg2Rad;
    }
}