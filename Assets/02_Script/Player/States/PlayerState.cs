using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM_System;

public abstract class PlayerState : FSM_State<EnumPlayerState>
{

    protected PlayerValues playerValues;
    protected PlayerInputController playerInputController;
    protected PlayerEventSystem playerEventSystem;
    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rigid;
    protected GroundSencer groundSencer;
    protected bool isGround => groundSencer.isGround;

    protected PlayerState(PlayerController controller) : base(controller)
    {


        playerInputController = controller.playerInputController;
        playerEventSystem = controller.playerEventSystem;
        playerValues = controller.playerValues;

        spriteRenderer = controller.GetComponent<SpriteRenderer>();
        rigid = controller.GetComponent<Rigidbody2D>();
        groundSencer = controller.GetComponentInChildren<GroundSencer>();

    }

}
