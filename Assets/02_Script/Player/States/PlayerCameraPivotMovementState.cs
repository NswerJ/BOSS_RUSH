using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraPivotMovementState : PlayerState
{

    private Transform pivot;
    private float per;

    public PlayerCameraPivotMovementState(PlayerController controller) : base(controller)
    {

        pivot = transform.Find("CameraPivot");
        per = -1;

    }

    protected override void UpdateState()
    {

        if(playerInputController.LastInputVector.x == -1)
        {

            per -= Time.deltaTime * 2f;

        }
        else
        {

            per += Time.deltaTime * 2f;

        }

        per = Mathf.Clamp(per, 0, 1);

        pivot.transform.localPosition = new Vector3(Mathf.Lerp(-1, 1, FAED.Easing(FAED_Easing.InSine, per)), 0, 0);

    }

}
