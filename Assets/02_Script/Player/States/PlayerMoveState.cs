using FD.Dev;
using FSM_System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{

    private bool dashAble = true;
    private bool isDashCool;

    public PlayerMoveState(PlayerController controller) : base(controller)
    {

        groundSencer.OnTriggerd += HandleDashAble;

    }

    private void HandleDashAble(bool obj)
    {

        if (obj)
        {

            dashAble = true;

        }

    }

    protected override void EnterState()
    {

        playerInputController.DashKeyPressdEvent += HandleDashKeyPressed;

    }

    protected override void ExitState()
    {

        playerInputController.DashKeyPressdEvent -= HandleDashKeyPressed;

    }

    private void HandleDashKeyPressed()
    {

        if (isDashCool || !dashAble) return;
        isDashCool = true;
        dashAble = false;

        controller.ChangeState(EnumPlayerState.Dash);

        FAED.InvokeDelay(() =>
        {

            isDashCool = false;

        }, 0.7f);

    }

    protected override void UpdateState()
    {

        Move();

        if(isGround && !dashAble && rigid.velocity.y <= 0)
        {

            dashAble = true;

        }

    }

    private void Move()
    {

        Vector2 origin = rigid.velocity;

        origin.x = playerInputController.InputVector.x * playerValues.MoveSpeed.GetValue();

        rigid.velocity = origin;

    }

}
