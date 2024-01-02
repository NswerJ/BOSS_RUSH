using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlipState : PlayerState
{
    public PlayerFlipState(PlayerController controller) : base(controller)
    {
    }

    protected override void UpdateState()
    {

        spriteRenderer.flipX = playerInputController.InputVector.x switch
        {

            -1 => false,
            1 => true,
            _ => spriteRenderer.flipX

        };

    }

}
