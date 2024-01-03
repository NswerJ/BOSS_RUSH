using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBossDie : BossDieCheck
{
    [SerializeField] GameObject PowerUpDoor;
    public override void DieEvent()
    {
        FindObjectOfType<PlayerIceSpearSkill>().ConnectEvent();

        //if(시간안에 잡으면)
        //PowerDoor();
        //else
        Invoke("EndFun", 1.65f);
    }

    public void PowerDoor() => PowerUpDoor.SetActive(true);
}
