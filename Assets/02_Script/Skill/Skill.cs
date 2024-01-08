using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{

    public bool skillenable { get; protected set; }
    public bool skillActive { get; protected set; }

    public abstract void DoSkill();

}