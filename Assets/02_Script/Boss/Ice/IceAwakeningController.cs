using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM_System;

public enum EnumIceAwakeState
{

    Pattern_1, //상단부 이동후 일직선으로 샤드 날림
    Pattern_2, //뫼비우스띠 모양으로 이동하며 일정 시간마다 플레이어 방향으로 샤드 날림 && 카메라 주도권이 보스에게 넘어감

}

public class IceAwakeningController : FSM_Controller<EnumIceAwakeState>
{

    [SerializeField] private EnumIceAwakeState startState;

    protected override void Awake()
    {

        var p1 = new Ice_Pattern_1_State(this);
        AddState(p1, EnumIceAwakeState.Pattern_1);

        var p2 = new Ice_Pattern_2_State(this);
        AddState(p2 , EnumIceAwakeState.Pattern_2);

        ChangeState(startState);

    }

}
