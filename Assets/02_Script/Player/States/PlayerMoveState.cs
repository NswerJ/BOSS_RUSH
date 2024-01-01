using FSM_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(PlayerController controller) : base(controller)
    {
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
