using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDropper : MonoBehaviour
{
    Rigidbody2D rb;
    public float DropSpeed;


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
            //ÇÃ·¹ÀÌ¾î ´êÀ¸¸é ÇÇ ´â°Ô 
            Destroy(gameObject);
        }
    }
}
