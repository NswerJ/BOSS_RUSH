using FSM_System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
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

        controller.ChangeState(EnumPlayerState.Dash);

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
