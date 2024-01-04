using DG.Tweening;
using FD.Dev;
using FSM_System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Ice_Pattern_13_State : IceAwakeState
{

    private Transform point;
    private float per;
    private bool isMoveable;

    public Ice_Pattern_13_State(FSM_Controller<EnumIceAwakeState> controller) : base(controller)
    {

        point = bossPointsRoot.Find("Pattern_12");

    }

    protected override void EnterState()
    {

        warning.SetText("아이세스가 모든 곳을 공격합니다!", 2);

        movePtc.Play();

        ChangeCamera(transform, 5f);

        transform.DOMove(point.position, 1.5f).OnComplete(() =>
        {

            ChangeCamera(cameraPivot, 6.3f);
            isMoveable = true;
            StartCoroutine(AttackCo());

        });

    }

    protected override void UpdateState()
    {

        if (isMoveable)
        {

            transform.position = point.position + MoveToInf(per * 2);

        }

    }

    protected override void ExitState()
    {

        isMoveable = false;
        per = 0.0f;
        movePtc.Stop();

    }

    private Vector3 MoveToInf(float t)
    {

        float x = Mathf.Cos(t) * 5;
        float y = Mathf.Sin(t) * Mathf.Cos(t) * 2;

        per += Time.deltaTime;

        return new Vector2(x, y);

    }

    private IEnumerator AttackCo()
    {

        for(int i = 0; i < 40; i++)
        {

            var dir = target.transform.position - transform.position;
            dir.z = 0;
            var nd = Quaternion.Euler(0, 0, Random.Range(0, 360f)) * dir.normalized;
            var pt = nd * -30 + (Vector3)Random.insideUnitCircle;


            FAED.TakePool<IceLayser>("IceLayser", pt).Show(pt, nd);

            yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));

        }

        yield return new WaitForSeconds(3);

        ChangeState(EnumIceAwakeState.Pattern_13);

    }

}