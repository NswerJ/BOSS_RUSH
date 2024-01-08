using DG.Tweening;
using FD.Dev;
using FSM_System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Ice_Pattern_15_State : IceAwakeState
{

    private Transform pointA, pointB, pointC, camOrigin;
    private float per;
    private bool isMove;

    public Ice_Pattern_15_State(FSM_Controller<EnumIceAwakeState> controller) : base(controller)
    {

        pointA = bossPointsRoot.Find("Pattern_15_A");
        pointB = bossPointsRoot.Find("Pattern_15_B");
        pointC = bossPointsRoot.Find("Pattern_15_C");
        camOrigin = GameObject.Find("FirstCamPoint").transform;

    }

    protected override void EnterState()
    {

        per = 0;

        warning.SetText("아이세스가 죽은 책의 힘을 빌려 분신을 소환합니다", 2);

        ChangeCamera(camOrigin, 10);

        transform.DOMove(pointB.position, 0.7f).SetEase(Ease.OutSine)
            .OnComplete(() =>
            {

                FAED.TakePool<IceClone>("IceClone", pointA.position).Spawn(target);
                FAED.TakePool<IceClone>("IceClone", pointC.position).Spawn(target);
                StartCoroutine(SpawnCo());

            });

    }

    private IEnumerator SpawnCo()
    {


        float angle = 360 / 6;

        float cur = Time.time;

        while (Time.time - cur < 13)
        {

            float randomAngle = Random.Range(0, 180f);

            for (int i = 0; i < 6; i++)
            {

                var obj = FAED.TakePool<IceLayser>("IceLayser", transform.position, Quaternion.Euler(0, 0, (angle * i) + randomAngle));
                obj.Show(transform.position, obj.transform.right);
                FAED.TakePool("ExpEffect", transform.position);

            }

            yield return new WaitForSeconds(Random.Range(1.5f, 1.7f));

        }


        yield return new WaitForSeconds(5f);

        //ChangeState(EnumIceAwakeState.Pattern_15);


    }


}
