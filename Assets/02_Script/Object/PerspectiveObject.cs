using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveObject : MonoBehaviour
{

    [SerializeField] private float speed;

    private Transform player;
    private Vector3 old;

    private void Awake()
    {

        player = FindObjectOfType<PlayerController>().transform;
        old = player.transform.position;

    }

    private void Update()
    {

        if (old == player.transform.position) return;

        var pt = (old - player.transform.position).normalized;
        pt.z = 0;

        transform.Translate(pt * speed * Time.deltaTime);

        old = player.transform.position;

    }

}
