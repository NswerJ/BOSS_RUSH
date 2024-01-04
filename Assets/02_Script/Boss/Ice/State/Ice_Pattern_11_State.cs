using DG.Tweening;
using FD.Dev;
using FSM_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Pattern_11_State : IceAwakeState
{

    private Transform point;
    private Transform laserPoint;
    private Transform camOrigin;

    public Ice_Pattern_11_State(FSM_Controller<EnumIceAwakeState> controller) : base(controller)
    {

        point = bossPointsRoot.Find("Pattern_1");
        laserPoint = bossPointsRoot.Find("Pattern_11_L");
        camOrigin = GameObject.Find("FirstCamPoint").transform;

    }

    protected override void EnterState()
    {

        movePtc.Play();
        ChangeCamera(camOrigin, 10);


        transform.DOMove(point.position, 0.8f).SetEase(Ease.OutSine).OnComplete(() =>
        {

            movePtc.Stop();
            StartCoroutine(LaserSpawnCo());

        });


    }

    private IEnumerator LaserSpawnCo()
    {

        for(int i = -3; i <= 3; i++)
        {

            var pt = new Vector3(i * 5, 0);
            FAED.TakePool<IceLayser>("IceLayser", laserPoint.position + pt).Show(laserPoint.position + pt, new Vector2(Random.Range(-0.3f, 0.3f), -1));

            yield return new WaitForSeconds(0.2f);

        }

        yield return new WaitForSeconds(4);

        ChangeState(EnumIceAwakeState.Pattern_11);

    }

}
