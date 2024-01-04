using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordArrow : MonoBehaviour
{
    GameObject sword;
    GameObject player;
    GameObject arrow;

    private void Awake()
    {
        sword = GameObject.Find("SwordBoss");
        player = GameObject.Find("Player");
        arrow = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        transform.position = player.transform.position;

        Vector2 dir = sword.transform.position - transform.position;

        transform.up = dir;

        arrow.SetActive(Vector2.Distance(transform.position, sword.transform.position) > 15);
    }
}
