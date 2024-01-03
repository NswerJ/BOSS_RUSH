using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBossDie : BossDieCheck
{
    [SerializeField] private GameObject chest;

    public override void DieEvent()
    {
        chest.SetActive(true);
        EndFun();
    }
}
