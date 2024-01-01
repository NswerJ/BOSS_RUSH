using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM_System;

public abstract class PlayerState : FSM_State<EnumPlayerState>
{

    protected PlayerInputController playerInputController;
    protected PlayerEventSystem playerEventSystem;
    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rigid;

    protected PlayerState(PlayerController controller) : base(controller)
    {

        spriteRenderer = controller.GetComponent<SpriteRenderer>();
        rigid = controller.GetComponent<Rigidbody2D>();

    }

}
