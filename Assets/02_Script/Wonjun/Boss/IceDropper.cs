using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDropper : MonoBehaviour
{
    Rigidbody2D rb;
    public float DropSpeed;
    public float Damage = 10f;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    private void Update()
    {
        rb.velocity = Vector3.down * DropSpeed;
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
