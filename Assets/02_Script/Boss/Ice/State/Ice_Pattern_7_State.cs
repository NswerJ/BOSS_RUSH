using DG.Tweening;
using FD.Dev;
using FSM_System;
using System.Collections;
using UnityEngine;

public class Ice_Pattern_7_State : IceAwakeState
{

    private Transform point;
    private float mainRadius = 3f;
    private float speed = 1.5f;
    private bool isMoveStarted;
    private float per = 1f;

    public Ice_Pattern_7_State(FSM_Controller<EnumIceAwakeState> controller) : base(controller)
    {

        point = bossPointsRoot.Find("Pattern_2");

    }

    protected override void EnterState()
    {

        warning.SetText("아이세스가 무한한 힘을 이끌어 냅니다", 2);

        ChangeCamera(cameraPivot, 6.3f);

        movePtc.Play();

        transform.DOMove(point.position, 1.5f).SetEase(Ease.InSine).OnComplete(() =>
        {

            StartCoroutine(BoomAttack());
            StartCoroutine(ShardAttack());

            isMoveStarted = true;

        });


    }

    private IEnumerator BoomAttack()
    {

        int cnt = Random.Range(10, 15);

        for (int i = 0; i < cnt; i++)
        {

            FAED.TakePool<IceBoom>("IceBoom").Spawn(transform.position + (Vector3)Random.insideUnitCircle * 4);

            yield return new WaitForSeconds(Random.Range(0.7f, 1.2f));

        }

    }

    protected override void ExitState()
    {

        isMoveStarted = false;
        per = 1;

    }

    private IEnumerator ShardAttack()
    {

        float time = Time.time;

        while (Time.time - time < 18)
        {

            FAED.TakePool<IceShard>("IceShard", transform.position + (Vector3)Random.insideUnitCircle, Quaternion.identity).Spawn(target, 0.3f);
            yield return new WaitForSeconds(Random.Range(0.2f, 0.7f));

        }

        yield return new WaitForSeconds(3);

        ChangeState(EnumIceAwakeState.Pattern_7);

    }

    protected override void UpdateState()
    {
        if (!isMoveStarted) return;

        transform.position = point.position + MoveToInf(per * speed);

    }

    private Vector3 MoveToInf(float t)
    {

        float x = Mathf.Cos(t) * mainRadius;
        float y = Mathf.Sin(t) * Mathf.Cos(t) * mainRadius;

        per += Time.deltaTime;

        return new Vector2(x, y);

    }


}
