using DG.Tweening;
using FD.Dev;
using FSM_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ice_Pattern_14_State : IceAwakeState
{

    private Transform point;
    private Transform camOrigin;
    private List<Transform> spawnPoints = new();

    public Ice_Pattern_14_State(FSM_Controller<EnumIceAwakeState> controller) : base(controller)
    {

        camOrigin = GameObject.Find("FirstCamPoint").transform;

        for (int i = 0; i < bossPointsRoot.Find("Pattern_14").childCount; i++)
        {

            spawnPoints.Add(bossPointsRoot.Find("Pattern_14").GetChild(i));

        }


        point = bossPointsRoot.Find("Pattern_1");

    }

    protected override void EnterState()
    {

        warning.SetText("아이세스가 죽은 검의 힘을 빌려 플레이어를 심판합니다", 2);

        ChangeCamera(camOrigin, 10);

        transform.DOMove(point.position, 0.7f).OnComplete(() =>
        {

            StartCoroutine(SpawnCo());

        });

    }

    private IEnumerator SpawnCo()
    {

        foreach(var p in spawnPoints)
        {

            FAED.TakePool<IceSword>("IceSword", p.position).Spawn();

            yield return new WaitForSeconds(0.3f);

        }

        yield return new WaitForSeconds(3f);

        ChangeState(EnumIceAwakeState.Pattern_14);

    }

}
