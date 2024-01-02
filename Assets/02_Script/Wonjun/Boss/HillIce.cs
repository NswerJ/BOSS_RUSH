using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HillIce : MonoBehaviour
{
    IceAttack iceAttack;
    public float iceHp;
    HitObject hitObject;
    public bool Hill = false;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();    
        hitObject = GetComponent<HitObject>();
        iceAttack = GetComponentInParent<IceAttack>();
        hitObject.DieEvent += HillStop;
        iceHp = hitObject.hp;
        
    }

    private void HillStop()
    {
        Debug.Log("dsd");
        iceAttack.HillExit();
        iceAttack.IceObjectTarget(false);
        iceHp = 0;
        Hill = false;
    }

    // Update is called once per frame
    void Update()
    {
        hitObject.hp = iceHp;
    }

    
}
