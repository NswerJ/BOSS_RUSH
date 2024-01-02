using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{

    private AddGravity addGravity;

    public PlayerDashState(PlayerController controller) : base(controller)
    {

        addGravity = GetComponent<AddGravity>();

    }

    public override void CollisonEnter()
    {



    }

}
