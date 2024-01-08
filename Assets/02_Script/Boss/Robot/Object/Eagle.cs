using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{

    private void Update()
    {

        transform.position -= Vector3.right * 60 * Time.deltaTime;

    }

    public void Spawn()
    {

        StartCoroutine(DeSpawnCo());

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {


            FAED.TakePool("EagleBoom", transform.position);
            collision.GetComponent<HitObject>().TakeDamage(30);
            FAED.InsertPool(gameObject);

        }

    }

    private IEnumerator DeSpawnCo()
    {

        yield return new WaitForSeconds(10);
        FAED.InsertPool(gameObject);

    }

}
