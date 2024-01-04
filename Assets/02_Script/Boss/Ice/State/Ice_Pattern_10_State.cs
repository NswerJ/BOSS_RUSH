using Cinemachine;
using DG.Tweening;
using FD.Dev;
using FSM_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Pattern_10_State : IceAwakeState
{

    private CinemachineBasicMultiChannelPerlin cbmcp;
    private Transform point;

    public Ice_Pattern_10_State(FSM_Controller<EnumIceAwakeState> controller) : base(controller)
    {

        cvcam = Object.FindObjectOfType<CinemachineVirtualCamera>();
        cbmcp = cvcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

    }

    protected override void EnterState()
    {

        warning.SetText("아이세스가 모든것을 얼리기 위하여 힘을 모읍니다!", 2);

        cbmcp.m_AmplitudeGain = 0.8f;
        cbmcp.m_FrequencyGain = 0.8f;

        Sequence seq = DOTween.Sequence();


        point = Random.value > 0.5f ? bossPointsRoot.Find("Pattern_10_A") : bossPointsRoot.Find("Pattern_10_B");

        StartCoroutine(CameraSettingCo());

        seq.AppendInterval(0.5f);
        seq.AppendCallback(() =>
        {

            transform.position = point.position;
            FAED.TakePool("ExpEffect", transform.position);

        });
        seq.Append(transform.DOShakePosition(2));
        seq.AppendCallback(() =>
        {

            StartCoroutine(SpearSpawn());

        });
        

    }

    private IEnumerator SpearSpawn()
    {

        var dir = target.position - transform.position;
        dir.Normalize();

        for(int i = 0; i < 50; i++)
        {

            FAED.TakePool<IceSpear_Awake>("IceSpear_A", transform.position + (Vector3)Random.insideUnitCircle * 2).Spawn(dir, 0.5f);

            yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));

        }

        cbmcp.m_AmplitudeGain = 0f;
        cbmcp.m_FrequencyGain = 0f;

        yield return new WaitForSeconds(2f);

        ChangeState(EnumIceAwakeState.Pattern_10);

    }
    
    private IEnumerator CameraSettingCo()
    {

        ChangeCamera(transform, 5);
        yield return new WaitForSeconds(2f);
        ChangeCamera(cameraPivot, 6.3f);

    }

}
