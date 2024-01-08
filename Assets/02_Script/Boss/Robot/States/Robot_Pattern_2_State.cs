using FSM_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_Pattern_2_State : RobotBossState
{

    private Transform bossWarningObject;

    public Robot_Pattern_2_State(FSM_Controller<EnumRobotBossState> controller) : base(controller)
    {

        bossWarningObject = GameObject.Find("BossWarning").transform;

    }


    protected override void EnterState()
    {




    }

}
