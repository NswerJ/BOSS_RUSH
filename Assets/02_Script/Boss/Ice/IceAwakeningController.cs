using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM_System;

public enum EnumIceAwakeState
{

    Pattern_1,  //상단부 이동후 일직선으로 샤드 날림
    Pattern_2,  //뫼비우스띠 모양으로 이동하며 일정 시간마다 플레이어 방향으로 샤드 날림
    Pattern_3,  //보스가 중앙으로 이동 후 폭팔하는 코어를 소환
    Pattern_4,  //무수히 많은 샤드를 플레이어를 향해 발사 && 카메라 보스에게
    Pattern_5,  //패턴 1 && 4 혼합형
    Pattern_6,  //창 날아옴
    Pattern_7,  //패턴 3 && 2 혼합형
    Pattern_8,  //패턴 1의 창 버젼
    Pattern_9,  //자신의 주변에 레이저를 발사하는 오브젝트 생성
    Pattern_10, //차징후 강력한 공격
    Pattern_11, //루시드 레이저
    Pattern_12, //광범위공격 버티면 HP회복
    Pattern_13, //얼어서 뛰어내리기

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

        var p3 = new Ice_Pattern_3_State(this);
        AddState(p3, EnumIceAwakeState.Pattern_3);

        var p4 = new Ice_Pattern_4_State(this);
        AddState(p4 , EnumIceAwakeState.Pattern_4);

        var p5 = new Ice_Pattern_5_State(this);
        AddState(p5 , EnumIceAwakeState.Pattern_5);

        var p6 = new Ice_Pattern_6_State(this);
        AddState(p6, EnumIceAwakeState.Pattern_6);

        var p7 = new Ice_Pattern_7_State(this);
        AddState(p7 , EnumIceAwakeState.Pattern_7);

        var p8 = new Ice_Pattern_8_State(this);
        AddState(p8 , EnumIceAwakeState.Pattern_8);

        var p9 = new Ice_Pattern_9_State(this);
        AddState(p9 , EnumIceAwakeState.Pattern_9);

        var p10 = new Ice_Pattern_10_State(this);
        AddState(p10 , EnumIceAwakeState.Pattern_10);

        ChangeState(startState);

    }

}
