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
        // 플레이어 피 까줘야함
        //Destroy(gameObject);
    }

}
