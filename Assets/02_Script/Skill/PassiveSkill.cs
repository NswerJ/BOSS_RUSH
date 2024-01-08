using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveSkill : Skill
{
    
    protected virtual void Start()
    {

        if (!skillActive) return;

        if (skillenable)
        {

            DoSkill();

        }

    }

}
