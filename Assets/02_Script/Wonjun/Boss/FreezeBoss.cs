using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBoss : MonoBehaviour
{
    public float BossHp = 10000;
    public bool phase1 = false;
    public bool phase2 = false;
    public bool phase3 = false;
    public float FreezeTime = 20;
    public float HillCool = 2;
    public float Phase2Hill = 50;
    public float Phase3Hill = 200;
    bool iceAttackTarget = false;
    private float nextIncreaseTime = 0;
    IceAttack iceAttack;
    HitObject BossHit;

    Animator anim;

    private void Awake()
    {
        BossHit = GetComponent<HitObject>();
        anim = GetComponent<Animator>();    
        iceAttack = GameObject.Find("IceAttack").GetComponent<IceAttack>();
        if(iceAttack != null )
        {
            Debug.Log("들어옴");
        }
        BossHp = BossHit.maxHP;
    }

    private void Update()
    {

        /*if (Input.GetMouseButtonDown(0))
        {
            BossHp -= 200;
            
            Debug.Log(BossHp);
        }*/

        if (phase1)
        {
            Debug.Log("페이즈 1");
            Phase1();
        }
        else if (phase2)
        {
            Debug.Log("페이즈 2");

            Phase2();
        }
        else if (phase3)
        {
            Debug.Log("페이즈 3");
            Phase3();
        }
    }
    #region 페이즈

    #region 페이즈 1
    private void Phase1()
    {
        iceAttack.Paze1Pattern();

        if (BossHp <= 6000)
        {
            FreezeTime = 0;
            phase1 = false;
            phase2 = true;
        }
    }
    #endregion

    #region 페이즈 2
    private void Phase2()
    {
        if (FreezeTime > 0)
        {
            FreezeTime -= Time.deltaTime;
            iceAttackTarget = false;
            anim.SetBool("Hill", false);

        }
        else
        {
            FreezeTime = 0;
            if (!iceAttackTarget)
            {
                iceAttack.IceObjectTarget(true);
                iceAttackTarget = true;
                iceAttack.IceBlockHp = 1;
            }
            if (BossHp < 10000)
            {
                float increaseRate = Phase2Hill;
                float increaseInterval = 2f;
                iceAttack.HillExit();
                anim.SetBool("Hill", true);

                if (Time.time > nextIncreaseTime)
                {
                    nextIncreaseTime = Time.time + increaseInterval;
                    BossHp += increaseRate;
                }
                if (iceAttack.IceBlockHp == 0)
                {
                    FreezeTime = 20;
                    iceAttack.IceObjectTarget(false);
                }
            }
        }

        if (BossHp <= 3000)
        {
            FreezeTime = 0;
            phase2 = false;
            phase3 = true;
        }
        iceAttack.Paze2Pattern();
    }
    #endregion
    #region 페이즈 3
    private void Phase3()
    {
        iceAttack.Paze3Pattern();
        if (FreezeTime > 0)
        {
            FreezeTime -= Time.deltaTime;
            iceAttackTarget = false;
            anim.SetBool("Hill", false);

        }
        else
        {
            FreezeTime = 0;
            if (!iceAttackTarget)
            {
                iceAttack.IceObjectTarget(true);
                iceAttackTarget = true;
                iceAttack.IceBlockHp = 1;
            }
            if (BossHp < 10000)
            {
                float increaseRate = Phase3Hill;
                float increaseInterval = 1f;
                iceAttack.HillExit();
                anim.SetBool("Hill", true);



                if (Time.time > nextIncreaseTime)
                {
                    nextIncreaseTime = Time.time + increaseInterval;
                    BossHp += increaseRate;
                }
                if (iceAttack.IceBlockHp == 0)
                {
                    FreezeTime = 10;
                    iceAttack.IceObjectTarget(false);
                }
            }
        }
    }
    #endregion
    #endregion
}
