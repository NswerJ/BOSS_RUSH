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


        var mpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var dir = (mpos - transform.position).normalized;
        dir.z = 0;

        spriteRenderer.flipX = dir.x switch
        {

            var x when x > 0 => true,
            var x when x < 0 => false,
            _ => spriteRenderer.flipX

        };

    }

}
