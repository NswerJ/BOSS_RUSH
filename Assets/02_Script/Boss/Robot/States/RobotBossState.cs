using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM_System;
using System;

public class RobotBossState : FSM_State<EnumRobotBossState>
{

    public RobotBossState(FSM_Controller<EnumRobotBossState> controller) : base(controller)
    {

        bossPoints = GameObject.Find("BossPoints").transform;

    }

    protected Transform bossPoints;

    protected void ChangeState(EnumRobotBossState thisState)
    {

        var vel = Enum.GetValues(typeof(EnumRobotBossState));

        var cur = new List<EnumRobotBossState>();

        foreach (var item in vel)
        {

            if ((EnumRobotBossState)item == thisState) continue;

            cur.Add((EnumRobotBossState)item);

        }

        int idx = UnityEngine.Random.Range(0, cur.Count);

        controller.ChangeState(cur[idx]);

    }

}
