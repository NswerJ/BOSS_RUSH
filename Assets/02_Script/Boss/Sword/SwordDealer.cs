using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDealer : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent<HitObject>(out HitObject ho))
            {
                ho.TakeDamage(10);
            }
        }
    }
}
