using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    private Transform point;
    private Collider2D col;

    private void Awake()
    {
        
        point =  FindObjectOfType<PlayerController>().transform.Find("JumpParticlePivot");
        col = GetComponent<Collider2D>();

    }

    private void Update()
    {
        
        col.enabled = point.transform.position.y > transform.position.y;

    }

}
