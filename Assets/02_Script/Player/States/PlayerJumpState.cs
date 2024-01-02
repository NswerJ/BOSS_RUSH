using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{

    private int jumpCnt;

    public PlayerJumpState(PlayerController controller) : base(controller)
    {
    }

    protected override void EnterState()
    {

        groundSencer.OnTriggerd += HandleTrigger;
        playerInputController.JumpKeyPressdEvent += HandleJumpKeyPressd;

    }

    private void HandleTrigger(bool obj)
    {

        if (obj)
        {

            jumpCnt = 2;

        }

    }

    protected override void ExitState()
    {

        groundSencer.OnTriggerd -= HandleTrigger;
        playerInputController.JumpKeyPressdEvent -= HandleJumpKeyPressd;

    }

    private void HandleJumpKeyPressd()
    {

        if (jumpCnt > 0)
        {

            jumpCnt--;
            playerEventSystem.JumpEventExecute();

            rigid.velocity = new Vector2(rigid.velocity.x, playerValues.JumpPower.GetValue());

        }

    }

}
