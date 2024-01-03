using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM_System;
using System;

public class IceAwakeState : FSM_State<EnumIceAwakeState>
{

    public IceAwakeState(FSM_Controller<EnumIceAwakeState> controller) : base(controller)
    {

        target = UnityEngine.Object.FindObjectOfType<PlayerController>().transform;
        bossPointsRoot = GameObject.Find("BossPoints").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        movePtc = transform.Find("MoveParticle").GetComponent<ParticleSystem>();

    }

    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rigid;
    protected Transform target;
    protected Transform bossPointsRoot;
    protected ParticleSystem movePtc;

    protected void ChangeState(EnumIceAwakeState thisState)
    {

        var vel = Enum.GetValues(typeof(EnumIceAwakeState));

        var cur = new List<EnumIceAwakeState>();

        foreach(var item in vel)
        {

            if ((EnumIceAwakeState)item == thisState) continue;

            cur.Add((EnumIceAwakeState)item);

        }

        int idx =  UnityEngine.Random.Range(0, cur.Count);

        controller.ChangeState(cur[idx]);

    }

}
