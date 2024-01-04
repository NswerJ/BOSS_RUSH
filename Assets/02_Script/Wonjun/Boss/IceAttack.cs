using FD.Dev;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAttack : MonoBehaviour
{

    public GameObject[] attacker;
    public float BulletSpeed = 10f;
    public Vector3 BulletPos;
    GameObject TargeticeBlock;
    public Transform Target;
    public Transform Boss;

    public LineRenderer HillLine;
    [HideInInspector] public LineRenderer hillLine;

    [SerializeField] private Transform[] IceDropPos;
    [SerializeField] private Transform[] IceDropPos2;
    public float IceDropCool;
    [SerializeField] private GameObject WarnigRazer;
    [SerializeField] private GameObject icedrop;
    private GameObject[] Razer;

    [SerializeField] private GameObject IceSpear;
    [SerializeField] private GameObject WarnigIceSpear;
    public float IceSpearCool;

    public GameObject HillStopEffect;
    public ParticleSystem HillEffect;
    ParticleSystem hillEf;

    private int BulletPosCount = 1;
    public float IceBlockHp = 1;
    public AudioClip BulletClip;

    [SerializeField] public List<GameObject> list;

    private int currentAttackerIndex = 0;
    private bool isAttacking = false;
    private bool isSecondAttacking = false;
    private bool isThirdAttacking = false;

    public bool BulletSpawn = true;
    public bool HillLineShow = false;

    public GameObject freezeboss;

    private void Awake()
    {
        list = new List<GameObject>();
        Razer = new GameObject[IceDropPos.Length];
    }
    private void Update()
    {
        if (HillLineShow)
        {
            hillLine.SetPosition(1, Boss.position);
        }
    }

    #region 페이즈 1
    public void Paze1Pattern()
    {
        StartCoroutine(AttackPattern());
    }

    IEnumerator AttackPattern()
    {
        while (true)
        {
            if (!isAttacking)
            {
                isAttacking = true;

                yield return StartCoroutine(AttackSequence());

                yield return new WaitForSeconds(1f);


                isAttacking = false;
            }
            yield return null;
        }
    }

    IEnumerator AttackSequence()
    {

        for (int i = 0; i < 4; i++)
        {
            StartCoroutine(BulletPosSet(BulletPosCount));
            FAED.TakePool<IceShard>("IceShard", attacker[currentAttackerIndex].transform.position + BulletPos).Spawn(Target, 0.3f);
            yield return new WaitForSeconds(.2f);
        }

        currentAttackerIndex = (currentAttackerIndex + 1) % attacker.Length;
    }


    private IEnumerator BulletPosSet(int bulletPosCount)
    {
        if (bulletPosCount == 1)
        {
            BulletPos = new Vector3(-0.5f, 0.5f, 0);
            BulletPosCount++;
        }
        else if (bulletPosCount == 2)
        {
            BulletPos = new Vector3(0.5f, 0.5f, 0);
            BulletPosCount++;
        }
        else if (bulletPosCount == 3)
        {
            BulletPos = new Vector3(-0.5f, -0.5f, 0);
            BulletPosCount++;
        }
        else if (bulletPosCount == 4)
        {
            BulletPos = new Vector3(0.5f, -0.5f, 0);
            BulletPosCount = 1;
        }
        yield return null;
    }

    /*public IEnumerator RemoveIce(GameObject iceObject, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (list.Contains(iceObject))
        {
            list.Remove(iceObject);
            Destroy(iceObject);
        }
        else if (list == null)
        {
            Debug.Log("널");
        }
    }*/

    public void IceObjectTarget(bool v)
    {
        if (v == false)
        {
            if (hillLine != null)
            {
                StartCoroutine(DecreaseLineSizeOverTime(hillLine, 0.25f, 0.0f, 2.5f));
            }
            for (int i = 0; i < 4; i++)
            {
                GameObject TargeticeBlock = attacker[i];
                Animator TargetAnim = TargeticeBlock.GetComponent<Animator>();
                TargetAnim.SetBool("Target", v);
            }
        }
        else
        {
            int attackice = UnityEngine.Random.Range(0, attacker.Length);
            TargeticeBlock = attacker[attackice];
            Animator TargetAnim = TargeticeBlock.GetComponent<Animator>();
            HillIce Targethit = TargeticeBlock.GetComponent<HillIce>();
            Targethit.iceHp = 1;
            hillLine = Instantiate(HillLine, TargeticeBlock.transform.position, Quaternion.identity).GetComponent<LineRenderer>();
            hillLine.SetPosition(0, TargeticeBlock.transform.position);
            HillLineShow = true;
            StartCoroutine(ChangeLineSizeOverTime(hillLine, 0.0f, 0.25f, 2.0f));
            if (TargetAnim != null)
            {
                TargetAnim.SetBool("Target", v);
            }
            else
            {
                Debug.Log("없어");
            }
        }
    }

    private IEnumerator DecreaseLineSizeOverTime(LineRenderer line, float startSize, float endSize, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float newSize = Mathf.Lerp(startSize, endSize, elapsedTime / duration);
            line.startWidth = newSize;
            line.endWidth = newSize;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        line.startWidth = endSize;
        line.endWidth = endSize;

        if (line.startWidth <= 0.1f)
        {
            Destroy(line.gameObject);
            HillLineShow = false;
        }
    }

    private IEnumerator ChangeLineSizeOverTime(LineRenderer line, float startSize, float endSize, float duration)
    {
        HillLineShow = true;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            hillLine.SetPosition(1, Boss.position);
            float newSize = Mathf.Lerp(startSize, endSize, elapsedTime / duration);
            line.startWidth = newSize;
            line.endWidth = newSize;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        line.startWidth = endSize;
        line.endWidth = endSize;
    }


    public void HillExit()
    {
        hillEf = Instantiate(HillEffect, Target.position, Quaternion.identity);
        hillEf.Play();
        GameObject StopEffect = Instantiate(HillStopEffect, Boss.position, Quaternion.identity);
        HitObject BoomAttack = freezeboss.GetComponent<HitObject>();
        HitObject PlayerHit = Target.GetComponent<HitObject>();
        PlayerHit.HealingObject(20f);
        BoomAttack.TakeDamage(350f);
        IceBlockHp = 0;
        Destroy(StopEffect, 1f);
        Destroy(hillEf, 0.3f);
    }
    #endregion

    #region 페이즈 2
    public void Paze2Pattern()
    {
        StartCoroutine(AttackPattern());
        StartCoroutine(SecondAttackPattern());

    }

    private IEnumerator SecondAttackPattern()
    {
        while (true)
        {
            if (!isSecondAttacking)
            {
                isSecondAttacking = true;

                yield return StartCoroutine(SecondAttackSequence());

                yield return new WaitForSeconds(IceDropCool);

                isSecondAttacking = false;
            }
            yield return null;
        }
    }

    private IEnumerator SecondAttackSequence()
    {
        float random = UnityEngine.Random.Range(1, 3);
        Debug.Log(random);
        List<GameObject> spawnedRazers = new List<GameObject>();
        if (random == 1)
        {
            for (int i = 0; i < IceDropPos.Length; i++)
            {
                GameObject currentRazer = Instantiate(WarnigRazer, IceDropPos[i].position, Quaternion.identity);
                spawnedRazers.Add(currentRazer);
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.2f);

            foreach (GameObject razer in spawnedRazers)
            {
                Destroy(razer);
            }

            List<GameObject> iceDrops = new List<GameObject>();
            for (int i = 0; i < IceDropPos.Length; i++)
            {
                GameObject iceDropInstance = Instantiate(icedrop, IceDropPos[i].position, Quaternion.identity);
                iceDrops.Add(iceDropInstance);
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.3f);

            for (int i = 0; i < iceDrops.Count; i++)
            {
                Destroy(iceDrops[i]);
            }
        }
        else
        {
            for (int i = 0; i < IceDropPos2.Length; i++)
            {
                GameObject currentRazer = Instantiate(WarnigRazer, IceDropPos2[i].position, Quaternion.identity);
                spawnedRazers.Add(currentRazer);
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.2f);

            foreach (GameObject razer in spawnedRazers)
            {
                Destroy(razer);
            }

            List<GameObject> iceDrops = new List<GameObject>();
            for (int i = 0; i < IceDropPos2.Length; i++)
            {
                GameObject iceDropInstance = Instantiate(icedrop, IceDropPos2[i].position, Quaternion.identity);
                iceDrops.Add(iceDropInstance);
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.3f);

            for (int i = 0; i < iceDrops.Count; i++)
            {
                Destroy(iceDrops[i]);
            }
        }



    }



    #endregion

    #region 페이즈 3
    public void Paze3Pattern()
    {
        IceDropCool = 1f;
        BulletSpeed = 20f;
        StartCoroutine(AttackPattern());
        StartCoroutine(SecondAttackPattern());
        StartCoroutine(ThirdAttackPattern());
    }

    private IEnumerator ThirdAttackPattern()
    {
        while (true)
        {
            if (!isThirdAttacking)
            {
                isThirdAttacking = true;

                yield return StartCoroutine(ThirdAttackSequence());

                yield return new WaitForSeconds(IceSpearCool);

                isThirdAttacking = false;
            }
            yield return null;
        }
    }

    private IEnumerator ThirdAttackSequence()
    {
        Vector3 Targetpos = new Vector3(-35f, Target.position.y, 0);

        GameObject WarningIceSpears = Instantiate(WarnigIceSpear, Targetpos, Quaternion.Euler(0, 0, 90));
        yield return new WaitForSeconds(0.7f);
        Destroy(WarningIceSpears);

        GameObject IceSpears = Instantiate(IceSpear, Targetpos, Quaternion.identity);
        Rigidbody2D IceSpearRb = IceSpears.GetComponent<Rigidbody2D>();

        if (IceSpearRb != null)
        {
            IceSpearRb.velocity = IceSpears.transform.right * 80f;
            Debug.Log("sdsddsdsd");
        }
    }
    #endregion
}
