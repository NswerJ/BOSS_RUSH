using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBossDie : BossDieCheck
{
    [SerializeField] GameObject PowerUpDoor;
    public float HiddenTime = 0;

    private void Start()
    {
        HiddenTime = 0;
    }

    public override void DieEvent()
    {
        FindObjectOfType<PlayerIceSpearSkill>().ConnectEvent();

        if(HiddenTime <= 150)
        PowerDoor();
        else
        Invoke("EndFun", 1.65f);
    }

    private void Update()
    {
        HiddenTime += Time.deltaTime;
    }

    public void PowerDoor() => PowerUpDoor.SetActive(true);
}
