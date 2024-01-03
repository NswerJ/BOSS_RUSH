using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBoss : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float movementRange = 2.0f; 

    private bool movingUp = false; 
    private Vector3 initialPosition; 

    public bool isAttack = true;

    public float BossHp = 10000;
    public float Phase2Hp = 6000;
    public float Phase3Hp = 3000;

    public bool phase1 = false;
    public bool phase2 = false;
    public bool phase3 = false;

    public float FreezeTime = 10;
    public float SaveFreezeTime;
   
    public float HillCool = 2;
    public float Phase2Hill = 50;
    public float Phase3Hill = 200;

    public bool DefenceOnOff = false;

    bool iceAttackTarget = false;
    private float nextIncreaseTime = 0;

    IceAttack iceAttack;
    HitObject BossHit;

    CapsuleCollider2D BossCol;
    public GameObject HillEffect;
    public GameObject DieEffect;

    Animator anim;

    public AudioClip Hillclip;
    public AudioClip Dieclip;
    public AudioClip PhaseChangeClip;
    bool HillSound = false;

    private float per;

    private void Awake()
    {
        SaveFreezeTime = FreezeTime;
        BossCol = GetComponent<CapsuleCollider2D>();    
        BossHit = GetComponent<HitObject>();
        anim = GetComponent<Animator>();    
        iceAttack = GameObject.Find("IceAttack").GetComponent<IceAttack>();
        
        BossHp = BossHit.maxHP;
        anim.SetBool("Die", false);
        BossHit.DieEvent += DieBoss;
    }

    private void DieBoss()
    {
        anim.SetBool("Die", true);
        isAttack = false;
        Destroy(iceAttack.gameObject);
    }

   

    private void DieBossEffect()
    {
        SoundManager.Instance.SFXPlay("SFX", Dieclip);
        GameObject dieEffect = Instantiate(DieEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(dieEffect, 1f);
    }

    public void BossMove()
    {
        movingUp = true;
    }

    private void Update()
    {

        if(BossHit.hp > BossHit.maxHP) {
            BossHit.hp = BossHit.maxHP;
        }
        else
        {
            BossHp = BossHit.hp;

        }
        /*if (Input.GetMouseButtonDown(0))
        {
            BossHp -= 200;
            
            Debug.Log(BossHp);
        }*/

        if (phase1 && isAttack)
        {
            Debug.Log("페이즈 1");
            Phase1();
        }
        else if (phase2 && isAttack)
        {
            Debug.Log("페이즈 2");
            Phase2();
        }
        else if (phase3 && isAttack)
        {
            Debug.Log("페이즈 3");
            Phase3();
        }
        if (movingUp)
        {
            per += Time.deltaTime;
            transform.position = initialPosition + new Vector3(0, Mathf.Sin(per * moveSpeed) / movementRange);

        }

    }






    #region 페이즈

    #region 페이즈 1
    private void Phase1()
    {
        iceAttack.Paze1Pattern();

        if (BossHp <= Phase2Hp)
        {
            FreezeTime = 0;
            phase1 = false;
            phase2 = true;
            DefenceOnOff = true;
            BossHit.defecnces.AddMod(10f);
            HillSound = true;
            SoundManager.Instance.SFXPlay("SFX", PhaseChangeClip);
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
            BossCol.enabled = true;
            anim.SetBool("Hill", false);
            HillSound = true;
            movingUp = true;
        }
        else
        {
            if (HillSound)
            {
                SoundManager.Instance.SFXPlay("SFX", Hillclip);
                HillSound = false;
            }
            movingUp = false;
            BossCol.enabled = false;
            FreezeTime = 0;
            if (!iceAttackTarget)
            {
                if (DefenceOnOff)
                {
                    BossHit.defecnces.AddMod(-40f);
                    DefenceOnOff = false;
                }
                GameObject hillEffect = Instantiate(HillEffect, transform.position, Quaternion.identity);
                iceAttack.IceObjectTarget(true);
                iceAttackTarget = true;
                iceAttack.IceBlockHp = 1;
                Destroy(hillEffect, 0.5f);
            }
            if (BossHp <= 10000)
            {
                
                float increaseRate = Phase2Hill;
                float increaseInterval = 2f;
                anim.SetBool("Hill", true);

                if (Time.time > nextIncreaseTime)
                {
                    nextIncreaseTime = Time.time + increaseInterval;
                    BossHit.hp += increaseRate;
                }
                if (iceAttack.IceBlockHp == 0)
                {
                    FreezeTime = SaveFreezeTime;
                    HillSound = true;
                }
            }
        }

        if (BossHp <= Phase3Hp)
        {
            FreezeTime = 0;
            phase2 = false;
            phase3 = true;
            DefenceOnOff = true;
            BossHit.defecnces.AddMod(20f);
            HillSound = true;
            SoundManager.Instance.SFXPlay("SFX", PhaseChangeClip);
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
            BossCol.enabled = true;
            anim.SetBool("Hill", false);
            HillSound = true;
            movingUp = true;
        }
        else
        {
            if (HillSound)
            {
                SoundManager.Instance.SFXPlay("SFX", Hillclip);
                HillSound = false;
            }
            movingUp = false;
            FreezeTime = 0;
            BossCol.enabled = false;
            if (!iceAttackTarget)
            {
                if(DefenceOnOff)
                {
                    BossHit.defecnces.AddMod(-50f);
                    DefenceOnOff = false;
                }
                GameObject hillEffect = Instantiate(HillEffect, transform.position, Quaternion.identity);
                iceAttack.IceObjectTarget(true);
                iceAttackTarget = true;
                iceAttack.IceBlockHp = 1;
                Destroy(hillEffect, 0.5f);
            }
            if (BossHp <= 10000)
            {
                float increaseRate = Phase3Hill;
                float increaseInterval = 1f;
                anim.SetBool("Hill", true);


                if (Time.time > nextIncreaseTime)
                {
                    nextIncreaseTime = Time.time + increaseInterval;
                    BossHit.hp += increaseRate;
                }
                if (iceAttack.IceBlockHp == 0)
                {
                    FreezeTime = SaveFreezeTime /2;
                }
            }
        }
    }
    #endregion
    #endregion
}
