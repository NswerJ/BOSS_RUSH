using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBossDie : BossDieCheck
{
    public override void DieEvent()
    {
        FindObjectOfType<BookPower>().SetAbility(true);
        Invoke("EndFun", 3f);
    }
}
