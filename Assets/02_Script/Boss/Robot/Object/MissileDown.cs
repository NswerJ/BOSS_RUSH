using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileDown : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player") || collision.CompareTag("Ground"))
        {

            FAED.TakePool("MissileBoom", transform.position);
            var hit = Physics2D.OverlapBox(transform.position, Vector2.one, 0, LayerMask.GetMask("Player"));

            if(hit != null)
            {

                hit.GetComponent<HitObject>().TakeDamage(10);

            }

            Destroy(gameObject);

        }

    }

}
