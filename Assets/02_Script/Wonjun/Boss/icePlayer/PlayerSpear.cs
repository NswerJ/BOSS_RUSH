using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpear : MonoBehaviour
{
    public float Damage = 150f;


    private void Start()
    {
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            HitObject pHit = collision.GetComponent<HitObject>();
            pHit.TakeDamage(Damage);
        }
    }
}
