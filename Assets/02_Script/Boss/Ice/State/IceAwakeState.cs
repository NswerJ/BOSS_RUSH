using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM_System;

public class IceAwakeState : FSM_State<EnumIceAwakeState>
{

    public IceAwakeState(FSM_Controller<EnumIceAwakeState> controller) : base(controller)
    {

        target = Object.FindObjectOfType<PlayerController>().transform;
        bossPointsRoot = GameObject.Find("BossPoints").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();  

    }

    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rigid;
    protected Transform target;
    protected Transform bossPointsRoot;

}
