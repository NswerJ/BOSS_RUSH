using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private readonly int HASH_GROUND = Animator.StringToHash("Ground");
    private readonly int HASH_NOT_GROUNT = Animator.StringToHash("NotGround");
    private readonly int HASH_XVAL = Animator.StringToHash("XVal");
    private readonly int HASH_YVAL = Animator.StringToHash("YVal");

    private Animator animator;
    private GroundSencer groundSencer;
    private Rigidbody2D rigid;

    private void Awake()
    {
        
        animator = GetComponent<Animator>();
        groundSencer = GetComponentInChildren<GroundSencer>();
        rigid = GetComponent<Rigidbody2D>();

        groundSencer.OnTriggerd += HandleTriggerd;

    }

    private void HandleTriggerd(bool ground)
    {

        if (ground)
        {

            animator.SetTrigger(HASH_GROUND);

        }
        else
        {

            animator.SetTrigger(HASH_NOT_GROUNT);

        }

    }

    private void Update()
    {
        
        animator.SetFloat(HASH_XVAL, Mathf.Abs(rigid.velocity.x));
        animator.SetFloat(HASH_YVAL, rigid.velocity.y);

    }

}
