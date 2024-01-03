using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using FD.Dev;
using FSM_System;
using System.Drawing;

public class Ice_Pattern_5_State : IceAwakeState
{

    protected Transform point;

    public Ice_Pattern_5_State(FSM_Controller<EnumIceAwakeState> controller) : base(controller)
    {

        point = bossPointsRoot.Find("Pattern_1");


    }

    private IEnumerator Shoot()
    {

        int cnt = Random.Range(45, 100);

        for (int i = 0; i < cnt; i++)
        {

            FAED.TakePool<IceShard>("IceShard", transform.position + (Vector3)Random.insideUnitCircle * 1.5f, Quaternion.identity).Spawn(target, 0.3f);

            yield return new WaitForSeconds(Random.Range(0.1f, 0.25f));

        }

        yield return new WaitForSeconds(4);

        ChangeState(EnumIceAwakeState.Pattern_5);

    }

    protected override void EnterState()
    {

        movePtc.Play();

        transform.DOMove(point.position, 1.5f).SetEase(Ease.InSine).OnComplete(() =>
        {

            ShardAttack();
            movePtc.Stop();

            StartCoroutine(Shoot());

        });

    }

    private void ShardAttack()
    {

        StartCoroutine(ShardSpawnCo());

    }

    private IEnumerator ShardSpawnCo()
    {

        for (int n = 0; n < 5; n++)
        {

            for (int i = -5; i <= 5; i++)
            {

                FAED.TakePool<IceShard>("IceShard", transform.position + new Vector3(i, 0), Quaternion.identity).Spawn(target, 0.3f);
                yield return new WaitForSeconds(0.05f);

            }

            yield return new WaitForSeconds(1.5f);

        }



    }

}
