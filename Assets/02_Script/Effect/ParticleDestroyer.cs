using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    [SerializeField] float time;

    private void Awake()
    {
        Destroy(gameObject, time);
    }
}
