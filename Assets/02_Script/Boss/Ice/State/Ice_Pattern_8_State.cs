using DG.Tweening;
using FD.Dev;
using FSM_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Pattern_8_State : IceAwakeState
{

    protected Transform point;


    public Ice_Pattern_8_State(FSM_Controller<EnumIceAwakeState> controller) : base(controller)
    {

        point = bossPointsRoot.Find("Pattern_1");


    }

    protected override void EnterState()
    {

        movePtc.Play();

        transform.DOMove(point.position, 1.5f).SetEase(Ease.InSine).OnComplete(() =>
        {

            ShardAttack();
            movePtc.Stop();

        });

    }

    private void ShardAttack()
    {

        StartCoroutine(ShardSpawnCo());

    }

    private IEnumerator ShardSpawnCo()
    {

        for (int i = -5; i <= 5; i++)
        {

            FAED.TakePool<IceSpear_Awake>("IceSpear_A", transform.position + new Vector3(i, 0), Quaternion.identity).Spawn(target, 0.3f);
            yield return new WaitForSeconds(0.05f);

        }


        yield return new WaitForSeconds(1);

        ChangeState(EnumIceAwakeState.Pattern_8);

    }

}
