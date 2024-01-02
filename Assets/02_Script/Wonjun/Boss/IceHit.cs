using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceHit : MonoBehaviour
{
    BoxCollider2D boxCol;
    private void Awake()
    {
        boxCol = GetComponent<BoxCollider2D>();
        boxCol.enabled = false;
        StartCoroutine(boxColShow());
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
            Destroy(gameObject);
        }
    }
}
