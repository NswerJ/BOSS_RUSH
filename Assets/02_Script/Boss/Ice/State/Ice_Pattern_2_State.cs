using DG.Tweening;
using FD.Dev;
using FSM_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Pattern_2_State : IceAwakeState
{

    private Transform point;

    public Ice_Pattern_2_State(FSM_Controller<EnumIceAwakeState> controller) : base(controller)
    {

        point = bossPointsRoot.Find("Pattern_2");

    }

    private float mainRadius = 3f;
    private float speed = 1.5f;
    private bool isMoveStarted;
    private float per = 1f;

    protected override void EnterState()
    {


        warning.SetText("?„ì´?¸ìŠ¤ê°€ ë¬´í•œ???˜ì„ ?´ëŒ???…ë‹ˆ??", 2);

        ChangeCamera(transform, 5f);

        movePtc.Play();

        transform.DOMove(point.position, 1.5f).SetEase(Ease.InSine).OnComplete(() =>
        {

            ChangeCamera(cameraPivot, 6.3f);
            isMoveStarted = true;

            StartCoroutine(ShardAttack());

        });


    }

    protected override void ExitState()
    {

        isMoveStarted = false;
        per = 1;

    }

    private IEnumerator ShardAttack()
    {

        float time = Time.time;

        while (Time.time - time < 13) 
        {

            FAED.TakePool<IceShard>("IceShard", transform.position + (Vector3)Random.insideUnitCircle, Quaternion.identity).Spawn(target, 0.3f);
            yield return new WaitForSeconds(Random.Range(0.2f, 0.7f));

        }

        yield return new WaitForSeconds(2);

        ChangeState(EnumIceAwakeState.Pattern_2);

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
