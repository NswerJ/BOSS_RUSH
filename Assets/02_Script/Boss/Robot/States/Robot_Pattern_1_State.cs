using FD.Dev;
using FSM_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_Pattern_1_State : RobotBossState
{

    protected Transform missileSpawnPoint;

    public Robot_Pattern_1_State(FSM_Controller<EnumRobotBossState> controller) : base(controller)
    {

        missileSpawnPoint = bossPoints.Find("Spawn");

    }

    protected override void EnterState()
    {

        StartCoroutine(SpawnMissile());

    }

    private IEnumerator SpawnMissile()
    {

        int cnt = Random.Range(20, 30);

        for(int i = 0; i < cnt; i++)
        {

            FAED.TakePool("MissileDown", missileSpawnPoint.transform.position + new Vector3(Random.Range(-14f, 14f), 0), Quaternion.Euler(0, 0, 90));

            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));

        }

        yield return new WaitForSeconds(5f);

    }

}
