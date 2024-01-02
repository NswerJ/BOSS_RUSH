using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM_System;

public enum EnumPlayerState
{

    Idle, //움직임이 불가능 할때
    Move, //움직임이 가능할 때
    Dash

}

public class PlayerController : FSM_Controller<EnumPlayerState>
{

    [field:SerializeField] public PlayerValues playerValues { get; private set; }
    [SerializeField] private EnumPlayerState startState = EnumPlayerState.Move;

    private Rigidbody2D rigid;

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
        var flip = new PlayerFlipState(this);
        var cam = new PlayerCameraPivotMovementState(this);
        var feedback = new PlayerMoveFeedbackState(this);

        AddState(move, EnumPlayerState.Move);
        AddState(jump, EnumPlayerState.Move);
        AddState(flip, EnumPlayerState.Move);
        AddState(cam, EnumPlayerState.Move);
        AddState(feedback, EnumPlayerState.Move);

        rigid = GetComponent<Rigidbody2D>();

        ChangeState(startState);

    }

    protected override void Update()
    {
        
        base.Update();

        playerInputController.Update();

    }

    public void ChangeIdle()
    {

        rigid.velocity = Vector2.zero;

        ChangeState(EnumPlayerState.Idle);

    }

    public void ChangeMove()
    {

        ChangeState(EnumPlayerState.Move);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        foreach(var state in GetState(currentState))
        {

            (state as PlayerState).CollisonEnter();

        }

    }

}