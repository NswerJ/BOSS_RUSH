using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpear : MonoBehaviour
{
    public float Damage = 100f;

    public AudioClip SpearClip;


    private void Start()
    {
        SoundManager.Instance.SFXPlay("SFX", SpearClip);
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HitObject pHit = collision.GetComponent<HitObject>();
            pHit.TakeDamage(Damage);
        }
    }
}
