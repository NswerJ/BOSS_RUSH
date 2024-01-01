using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraPivotMovementState : PlayerState
{

    private Transform pivot;

    public PlayerCameraPivotMovementState(PlayerController controller) : base(controller)
    {

        pivot = transform.Find("");

    }

}
