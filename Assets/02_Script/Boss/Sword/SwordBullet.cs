using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBullet : MonoBehaviour
{
    [SerializeField] float speed;

    private void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("¶Ñ½Ã");
        if (collision.CompareTag("Player"))
            if (collision.TryGetComponent<HitObject>(out HitObject ho))
            {
                ho.TakeDamage(20);
                Destroy(gameObject);
            }
    }

}
