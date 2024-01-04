using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM_System;
using System;
using Cinemachine;

public class IceAwakeState : FSM_State<EnumIceAwakeState>
{

    public IceAwakeState(FSM_Controller<EnumIceAwakeState> controller) : base(controller)
    {

        target = UnityEngine.Object.FindObjectOfType<PlayerController>().transform;
        bossPointsRoot = GameObject.Find("BossPoints").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        movePtc = transform.Find("MoveParticle").GetComponent<ParticleSystem>();
        cvcam = UnityEngine.Object.FindObjectOfType<CinemachineVirtualCamera>();
        cameraPivot = target.Find("CameraPivot");
        warning = UnityEngine.Object.FindObjectOfType<WarningText>();

    }

    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rigid;
    protected Transform target;
    protected Transform bossPointsRoot;
    protected Transform cameraPivot;
    protected ParticleSystem movePtc;
    protected CinemachineVirtualCamera cvcam;
    protected WarningText warning;

    protected void ChangeState(EnumIceAwakeState thisState)
    {

        var vel = Enum.GetValues(typeof(EnumIceAwakeState));

        var cur = new List<EnumIceAwakeState>();

        foreach(var item in vel)
        {

            if ((EnumIceAwakeState)item == thisState) continue;

            cur.Add((EnumIceAwakeState)item);

        }

        int idx =  UnityEngine.Random.Range(0, cur.Count);

        controller.ChangeState(cur[idx]);

    }

    public void ChangeCamera(Transform trm, float camSize)
    {

        if(trm == cameraPivot)
        {

            cvcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset.y = 3;

        }
        else
        {

            cvcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset.y = 0;

        }

        cvcam.Follow = trm;
        StartCoroutine(ResizeCam(camSize));

    }

    private IEnumerator ResizeCam(float camSize) 
    {

        float cam = cvcam.m_Lens.OrthographicSize;

        float per = 0;

        while(per < 1)
        {

            per += Time.deltaTime * 2;
            cvcam.m_Lens.OrthographicSize = Mathf.Lerp(cam, camSize, per);
            yield return null;

        }

    
    }

}
