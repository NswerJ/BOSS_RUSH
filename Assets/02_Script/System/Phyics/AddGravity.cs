using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGravity : MonoBehaviour
{

    private GroundSencer groundSencer;
    private Rigidbody2D rigid;

    private void Awake()
    {
        
        groundSencer = GetComponentInChildren<GroundSencer>();
        rigid = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {

        if(groundSencer == null)
        {

            rigid.velocity -= new Vector2(0, 9.81f * 2f) * Time.deltaTime;
            return;

        }

        if (!groundSencer.isGround)
        {

            rigid.velocity -= new Vector2(0, 9.81f * 2f) * Time.deltaTime;

        }

    }

}
