using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerController controller) : base(controller)
    {
    }

    protected override void EnterState()
    {

        playerInputController.JumpKeyPressdEvent += HandleJumpKeyPressd;

    }

    protected override void ExitState()
    {

        playerInputController.JumpKeyPressdEvent -= HandleJumpKeyPressd;

    }

    private void HandleJumpKeyPressd()
    {

        if (isGround)
        {

            playerEventSystem.JumpEventExecute();

            rigid.velocity += new Vector2(0, playerValues.JumpPower.GetValue());

        }

    }

}
