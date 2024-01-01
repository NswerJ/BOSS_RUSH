using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM_System;

public enum EnumPlayerState
{

    Idle, //움직임이 불가능 할때
    Move, //움직임이 가능할 때

}

public class PlayerController : FSM_Controller<EnumPlayerState>
{

    [field:SerializeField] public PlayerValues playerValues { get; private set; }
    [SerializeField] private EnumPlayerState startState = EnumPlayerState.Move;

    public PlayerInputController playerInputController { get; private set; }
    public PlayerEventSystem playerEventSystem { get; private set; }

    protected override void Awake()
    {

        base.Awake();

        playerValues = Instantiate(playerValues);

        playerInputController = new();
        playerEventSystem = new();

        AddState<PlayerState>(new PlayerIdleState(this), EnumPlayerState.Idle);

        var move = new PlayerMoveState(this);
        var jump = new PlayerJumpState(this);

        AddState(move, EnumPlayerState.Move);
        AddState(jump, EnumPlayerState.Move);

        ChangeState(startState);

    }

    protected override void Update()
    {
        
        base.Update();

        playerInputController.Update();

    }

}