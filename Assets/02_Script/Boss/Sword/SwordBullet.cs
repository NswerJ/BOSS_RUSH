using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SwordBullet : MonoBehaviour
{
    [SerializeField] float speed;
    float current = 0.5f;

    private void Start()
    {
        StartCoroutine(BulletCO());
    }

    IEnumerator BulletCO()
    {
        yield return new WaitForSeconds(0.2f);
        current = speed;
    }

    private void Update()
    {
        transform.position += transform.up * current * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("¶Ñ½Ã");
        if (collision.CompareTag("Player"))
            if (collision.TryGetComponent<HitObject>(out HitObject ho))
            {
                ho.TakeDamage(10);
                Destroy(gameObject);
            }
    }

}
