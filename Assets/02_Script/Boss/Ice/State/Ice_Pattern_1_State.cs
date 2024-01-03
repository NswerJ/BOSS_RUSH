using DG.Tweening;
using FSM_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Pattern_1_State : IceAwakeState
{

    protected Transform point;

    public Ice_Pattern_1_State(FSM_Controller<EnumIceAwakeState> controller) : base(controller)
    {

        point = bossPointsRoot.Find("Pattern_1");

    }

    protected override void EnterState()
    {

        transform.DOMove(point.position, 1.5f).SetEase(Ease.InSine).OnComplete(() =>
        {

            ShardAttack();

        });

    }

    private void ShardAttack()
    {

        Debug.Log("Asdf");

    }

}
