using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBossDie : BossDieCheck
{
    [SerializeField] GameObject PowerUpDoor;
    private float HiddenTime = 0;

    

    public override void DieEvent()
    {
        FindObjectOfType<PlayerIceSpearSkill>().ConnectEvent();

        if (HiddenTime <= 60)
            PowerDoor();
        else
            Invoke("EndFun", 1.65f);
    }

    private void LateUpdate()
    {
        HiddenTime += Time.deltaTime;
    }

    public void PowerDoor() => PowerUpDoor.SetActive(true);
}
