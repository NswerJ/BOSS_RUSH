using DG.Tweening;
using FD.Dev;
using FSM_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Pattern_9_State : IceAwakeState
{

    private Transform point;

    public Ice_Pattern_9_State(FSM_Controller<EnumIceAwakeState> controller) : base(controller)
    {

        point = bossPointsRoot.Find("Pattern_9");

    }

    protected override void EnterState()
    {


        ChangeCamera(transform, 5);

        movePtc.Play();

        transform.DOMove(point.position, 1.5f).SetEase(Ease.OutSine).OnComplete(() =>
        {

            ChangeCamera(cameraPivot, 6.3f);

            for(int i = 0; i < 3; i++)
            {

                FAED.TakePool<IceLayserObject>("IceLayserObject", transform.position + (Vector3)Random.insideUnitCircle * 5).Spawn();

            }

            StartCoroutine(ChangeCo());

        });

    }


    private IEnumerator ChangeCo()
    {

        yield return new WaitForSeconds(3f);
        ChangeState(EnumIceAwakeState.Pattern_9);

    }

}