using DG.Tweening;
using FD.Dev;
using FSM_System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Ice_Pattern_4_State : IceAwakeState
{
    public Ice_Pattern_4_State(FSM_Controller<EnumIceAwakeState> controller) : base(controller)
    {
    }

    protected override void EnterState()
    {

        movePtc.Stop();

        StartCoroutine(Shoot());
        StartCoroutine(CameraSetting());

    }

    private IEnumerator Shoot()
    {

        int cnt = Random.Range(45, 100);

        for (int i = 0; i < cnt; i++)
        {

            FAED.TakePool<IceShard>("IceShard", transform.position + (Vector3)Random.insideUnitCircle * 1.5f, Quaternion.identity).Spawn(target, 0.3f);

            yield return new WaitForSeconds(Random.Range(0.1f, 0.25f));

        }

        yield return new WaitForSeconds(1);

        ChangeState(EnumIceAwakeState.Pattern_4);

    }

    private IEnumerator CameraSetting()
    {

        ChangeCamera(transform, 7);

        yield return new WaitForSeconds(2f);

        ChangeCamera(cameraPivot, 6.3f);

    }

}
