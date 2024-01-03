using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceHit : MonoBehaviour
{
    BoxCollider2D boxCol;
    public float Damange;
    private void Awake()
    {
        boxCol = GetComponent<BoxCollider2D>();
        boxCol.enabled = false;
        StartCoroutine(boxColShow());
    }
    private void Start()
    {
        Destroy(gameObject, 4f);
    }

    private IEnumerator boxColShow()
    {
        yield return new WaitForSeconds(.9f);
        boxCol.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HitObject pHit = collision.GetComponent<HitObject>();
            pHit.TakeDamage(Damange);
        }
    }
}
