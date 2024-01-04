using DG.Tweening;
using FD.Dev;
using FSM_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Pattern_1_State : IceAwakeState
{

    protected Transform point;
    protected Transform camOrigin;
   

    public Ice_Pattern_1_State(FSM_Controller<EnumIceAwakeState> controller) : base(controller)
    {

        point = bossPointsRoot.Find("Pattern_1");
        camOrigin = GameObject.Find("FirstCamPoint").transform;

    }

    protected override void EnterState()
    {

        ChangeCamera(camOrigin, 10);
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

        for (int i = -15; i <= 15; i++)
        {

            FAED.TakePool<IceShard>("IceShard", transform.position + new Vector3(i, 0), Quaternion.identity).Spawn(target, 0.3f);
            yield return new WaitForSeconds(0.05f);

        }

        yield return new WaitForSeconds(2);

        ChangeState(EnumIceAwakeState.Pattern_1);

    }

}
