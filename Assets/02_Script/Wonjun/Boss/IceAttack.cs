using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAttack : MonoBehaviour
{
    public GameObject[] attacker;
    public GameObject IceBlock;
    public float BulletSpeed = 10f;
    public Vector3 BulletPos;
    GameObject TargeticeBlock;
    public Transform Target;

    [SerializeField] private Transform[] IceDropPos;
    [SerializeField] private GameObject WarnigRazer;
    [SerializeField] private GameObject iceSpear;
    
    private int BulletPosCount = 1;
    public float IceBlockHp = 1;
    
    [SerializeField] private List<GameObject> list;

    private int currentAttackerIndex = 0;
    private bool isAttacking = false;

    private void Awake()
    {
        list = new List<GameObject>();
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
            list.Add(Instantiate(IceBlock, attacker[currentAttackerIndex].transform.position + BulletPos, Quaternion.identity));
            yield return new WaitForSeconds(.2f);
        }

        Vector2 finalDirection = (Target.position - list[0].transform.position).normalized;
        for (int i = 0; i < 4; i++)
        {
            Rigidbody2D iceRb = list[i].GetComponent<Rigidbody2D>();
            if (iceRb != null)
            {
                list[i].transform.up = finalDirection;
                iceRb.velocity = finalDirection * BulletSpeed;
                StartCoroutine(RemoveIce(list[i], 2f));
            }
            else
            {
                Debug.Log("없어");
            }
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
            Debug.Log(bulletPosCount);
        }
        else if (bulletPosCount == 2)
        {
            BulletPos = new Vector3(0.5f, 0.5f, 0);
            BulletPosCount++;
            Debug.Log(bulletPosCount);
        }
        else if (bulletPosCount == 3)
        {
            BulletPos = new Vector3(-0.5f, -0.5f, 0);
            BulletPosCount++;
            Debug.Log(bulletPosCount);
        }
        else if (bulletPosCount == 4)
        {
            BulletPos = new Vector3(0.5f, -0.5f, 0);
            BulletPosCount = 1;
            Debug.Log(bulletPosCount);
        }
        yield return null;
    }

    IEnumerator RemoveIce(GameObject iceObject, float delay)
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
    }

    public void IceObjectTarget(bool v)
    {
        if (v == false)
        {
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

    public void HillExit()
    {
        //피격시를 넣기
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(TargeticeBlock.name);
            IceBlockHp = 0;
            Debug.Log("sd");
        }
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
            if (!isAttacking)
            {
                isAttacking = true;

                yield return StartCoroutine(SecondAttackSequence());

                yield return new WaitForSeconds(3f);

                isAttacking = false;
            }
            yield return null;
        }
    }

    private IEnumerator SecondAttackSequence()
    {
        for(int i=0; i<IceDropPos.Length; i++)
        {
            Instantiate(WarnigRazer, IceDropPos[i].position, Quaternion.identity);
        }

        yield return new WaitForSeconds(.8f);

        for (int i = 0; i < IceDropPos.Length; i++)
        {
            Instantiate(iceSpear, IceDropPos[i].position, Quaternion.identity);
        }
    }
    #endregion

    public void Paze3Pattern()
    {
        StartCoroutine(AttackPattern());
    }
}
