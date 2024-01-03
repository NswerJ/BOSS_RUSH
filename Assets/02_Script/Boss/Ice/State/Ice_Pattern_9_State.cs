using DG.Tweening;
using FSM_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Pattern_9_State : IceAwakeState
{

    private Transform point;

    public Ice_Pattern_9_State(FSM_Controller<EnumIceAwakeState> controller) : base(controller)
    {
    }

    protected override void EnterState()
    {

        point = Random.value > 5 ? bossPointsRoot.Find("Pattern_9_A") : bossPointsRoot.Find("Pattern_9_B");

        movePtc.Play();

        transform.DOMove(point.position, 1.5f).SetEase(Ease.OutSine).OnComplete(() =>
        {

            

        });

    }

}