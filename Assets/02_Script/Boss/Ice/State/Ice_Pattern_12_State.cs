using Cinemachine;
using DG.Tweening;
using FD.Dev;
using FSM_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Pattern_12_State : IceAwakeState
{

    private CinemachineBasicMultiChannelPerlin cbmcp;
    private Transform point;

    public Ice_Pattern_12_State(FSM_Controller<EnumIceAwakeState> controller) : base(controller)
    {

        point = bossPointsRoot.Find("Pattern_12");
        cbmcp = cvcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

    }

    protected override void EnterState()
    {

        warning.SetText("아이세스가 모든 것을 얼리기 위하여 힘을 모읍니다!", 2);

        Sequence seq = DOTween.Sequence();

        ChangeCamera(transform, 5f);

        cbmcp.m_FrequencyGain = 0.8f;
        cbmcp.m_AmplitudeGain = 0.8f;

        seq.AppendInterval(0.3f);
        seq.Join(transform.DOShakePosition(3f, 2));
        seq.AppendCallback(() =>
        {

            transform.position = point.position;
            FAED.TakePool("ExpEffect", transform.position);

        });
        seq.AppendInterval(0.5f);
        seq.AppendCallback(() =>
        {

            ChangeCamera(cameraPivot, 6.3f);

        });
        seq.AppendInterval(0.2f);
        seq.AppendCallback(() =>
        {

            StartCoroutine(AttackCo());

        });


    }

    private IEnumerator AttackCo()
    {

        for(int i = 0; i < 7; i++)
        {

            for(int j = 0; j < 36; j++)
            {

                float angle = 360 / 36;
                var shard = FAED.TakePool<IceShard>("IceShard", transform.position, Quaternion.Euler(0, 0, j * angle));
                shard.transform.position += shard.transform.up;
                shard.Spawn(shard.transform.up, 0f);

            }

            yield return new WaitForSeconds(0.7f);

        }

        cbmcp.m_FrequencyGain = 0f;
        cbmcp.m_AmplitudeGain = 0f;

        yield return new WaitForSeconds(1f);

        ChangeState(EnumIceAwakeState.Pattern_12);

        target.GetComponent<HitObject>().HealingObject(30);

    }

}