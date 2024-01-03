using DG.Tweening;
using FD.Dev;
using FSM_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Pattern_3_State : IceAwakeState
{


    private Transform point;

    public Ice_Pattern_3_State(FSM_Controller<EnumIceAwakeState> controller) : base(controller)
    {

        point = bossPointsRoot.Find("Pattern_3");

    }

    protected override void EnterState()
    {

        movePtc.Play();

        transform.DOMove(point.position, 1.5f).SetEase(Ease.InSine).OnComplete(() =>
        {

            movePtc.Stop();

            StartCoroutine(BoomAttack());

        });


    }

    private IEnumerator BoomAttack()
    {

        int cnt = Random.Range(3, 8);

        for(int i = 0; i < cnt; i++)
        {

            FAED.TakePool<IceBoom>("IceBoom").Spawn(transform.position + (Vector3)Random.insideUnitCircle * 4);

            yield return new WaitForSeconds(Random.Range(0.3f, 0.7f));

        }

        yield return new WaitForSeconds(1);

        ChangeState(EnumIceAwakeState.Pattern_3);

    }


}
