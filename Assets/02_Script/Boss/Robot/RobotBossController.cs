using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM_System;

public enum EnumRobotBossState
{

    Pattern_1, //하늘에서 미사일 떨어짐
    Pattern_2, //독수리 날아다님
    Pattern_3, //유도 미사일 발사
    Pattern_4, //직선 미사일 발사
    Pattern_5, //미사일 난사

}

public class RobotBossController : FSM_Controller<EnumRobotBossState>
{

    [SerializeField] private EnumRobotBossState startState;

    protected override void Awake()
    {

        base.Awake();

        ChangeState(startState);

    }

}
