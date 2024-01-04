using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HillIce : MonoBehaviour
{
    public GameObject IceBlockEffect;
    IceAttack iceAttack;
    public float iceHp;
    HitObject hitObject;
    public bool Hill = false;
    // Start is called before the first frame update
    void Start()
    {
        hitObject = GetComponent<HitObject>();
        iceAttack = GetComponentInParent<IceAttack>();
        hitObject.DieEvent += HillStop;
        iceHp = hitObject.hp;
        
    }

    private void HillStop()
    {
        GameObject iceEffect = Instantiate(IceBlockEffect, transform.position, Quaternion.identity);
        iceAttack.HillExit();
        iceAttack.IceObjectTarget(false);
        iceHp = 0;
        Hill = false;
        Destroy(iceEffect, 1.4f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,Time.deltaTime * UnityEngine.Random.Range(20f, 40f));
        hitObject.hp = iceHp;
    }

    
}
