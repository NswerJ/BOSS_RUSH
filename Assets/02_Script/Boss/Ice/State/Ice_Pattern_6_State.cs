using FD.Dev;
using FSM_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Pattern_6_State : IceAwakeState
{
    public Ice_Pattern_6_State(FSM_Controller<EnumIceAwakeState> controller) : base(controller)
    {
    }

    protected override void EnterState()
    {

        movePtc.Stop();
        ChangeCamera(transform, 7f);
        StartCoroutine(Shoot());

    }

    private IEnumerator Shoot()
    {

        int cnt = Random.Range(3, 9);

        for (int i = 0; i < cnt; i++)
        {

            FAED.TakePool<IceSpear_Awake>("IceSpear_A", transform.position + (Vector3)Random.insideUnitCircle * 1.5f, Quaternion.identity).Spawn(target, 0.3f);

            yield return new WaitForSeconds(Random.Range(0.1f, 0.25f));

        }

        yield return new WaitForSeconds(1);

        ChangeState(EnumIceAwakeState.Pattern_6);

    }
}
