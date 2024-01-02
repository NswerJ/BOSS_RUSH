using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartIceBoss : MonoBehaviour
{
    FreezeBoss freezeBoss;
    // Start is called before the first frame update
    void Start()
    {
        freezeBoss = GameObject.Find("Boss").GetComponent<FreezeBoss>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            freezeBoss.phase1 = true;
            Destroy(gameObject);
        }
    }
}
