using FD.Dev;
using FSM_System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{

    private bool isDashCool;

    public PlayerMoveState(PlayerController controller) : base(controller)
    {
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

        if (isDashCool) return;
        isDashCool = true;

        controller.ChangeState(EnumPlayerState.Dash);

        FAED.InvokeDelay(() =>
        {

            isDashCool = false;

        }, 0.7f);

    }

    protected override void UpdateState()
    {

        Move();

    }

    private void Move()
    {

        Vector2 origin = rigid.velocity;

        origin.x = playerInputController.InputVector.x * playerValues.MoveSpeed.GetValue();

        rigid.velocity = origin;

    }

}
