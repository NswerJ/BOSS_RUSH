using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBoss : MonoBehaviour
{
    public float BossHp = 10000;
    public bool phase1 = true;
    public bool phase2 = false;
    public bool phase3 = false;
    public float FreezeTime = 20;
    public float HillCool = 2;
    private float nextIncreaseTime = 0;
    IceAttack iceAttack;

    private void Awake()
    {
        iceAttack = GameObject.Find("IceAttack").GetComponent<IceAttack>();
        if(iceAttack != null )
        {
            Debug.Log("들어옴");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BossHp -= 200;
            
            Debug.Log(BossHp);
        }

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

    private void Phase1()
    {
        if (BossHp <= 6000)
        {
            phase1 = false;
            phase2 = true;
        }
        iceAttack.Paze1Pattern();

    }

    private void Phase2()
    {
        if (FreezeTime > 0)
        {
            FreezeTime -= Time.deltaTime;
        }
        else
        {
            FreezeTime = 0;

            if (BossHp < 10000)
            {
                float increaseRate = 20f;
                float increaseInterval = 2f;

                if (Time.time > nextIncreaseTime)
                {
                    nextIncreaseTime = Time.time + increaseInterval;
                    BossHp += increaseRate;
                }
            }
            else
            {
                FreezeTime = 20;
            }
        }

        if (BossHp <= 3000)
        {
            phase2 = false;
            phase3 = true;
        }
        iceAttack.Paze2Pattern();

    }

    private void Phase3()
    {
        if (FreezeTime > 0)
        {
            FreezeTime -= Time.deltaTime;
        }
        else
        {
            FreezeTime = 0;

            if (BossHp < 10000)
            {
                float increaseRate = 50f;
                float increaseInterval = 1f;

                if (Time.time > nextIncreaseTime)
                {
                    nextIncreaseTime = Time.time + increaseInterval;
                    BossHp += increaseRate;
                }
            }
            else
            {
                FreezeTime = 10;
            }
        }
        iceAttack.Paze3Pattern();
    }
    #endregion
}
