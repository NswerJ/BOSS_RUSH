using Cinemachine;
using DG.Tweening;
using FD.Dev;
using FSM_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Pattern_10_State : IceAwakeState
{

    private CinemachineImpulseSource source;
    private Transform point;

    public Ice_Pattern_10_State(FSM_Controller<EnumIceAwakeState> controller) : base(controller)
    {

        source = GetComponent<CinemachineImpulseSource>();

    }

    protected override void EnterState()
    {

        Sequence seq = DOTween.Sequence();

        point = Random.value > 0.5f ? bossPointsRoot.Find("Pattern_10_A") : bossPointsRoot.Find("Pattern_10_B");

        StartCoroutine(CameraSettingCo());

        seq.Append(transform.DOShakePosition(2));
        seq.AppendCallback(() =>
        {

            StartCoroutine(SpearSpawn());

        });
        source.GenerateImpulse(2);


    }

    private IEnumerator SpearSpawn()
    {

        for(int i = 0; i < 50; i++)
        {

            FAED.TakePool<IceSpear_Awake>("IceSpear_A", transform.position + (Vector3)Random.insideUnitCircle * 2).Spawn(target, 0.5f);

            yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));

        }

    }
    
    private IEnumerator CameraSettingCo()
    {

        ChangeCamera(transform, 5);
        yield return new WaitForSeconds(1.5f);
        ChangeCamera(cameraPivot, 6.3f);

    }

}
