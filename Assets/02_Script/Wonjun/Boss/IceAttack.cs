using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAttack : MonoBehaviour
{
    public GameObject[] attacker;
    public GameObject IceBlock;
    public Transform Target;
    public float BulletSpeed = 10f;
    [SerializeField] private List<GameObject> list;

    private int currentAttackerIndex = 0;
    private bool isAttacking = false;

    private void Awake()
    {
        list = new List<GameObject>();
    }

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

            list.Add(Instantiate(IceBlock, attacker[currentAttackerIndex].transform.position, Quaternion.identity));

            var dir = (Target.position - list[i].transform.position).normalized;

            list[i].transform.up = dir;
            
            yield return new WaitForSeconds(.2f); 
        }

        for (int i = 0; i < 4; i++)
        {
            Rigidbody2D iceRb = list[i].GetComponent<Rigidbody2D>(); 

            if( iceRb != null )
            {
                iceRb.velocity = list[i].transform.forward * BulletSpeed;
            }
            else
            {
                Debug.Log("¾ø¾î");
            }
        }
        currentAttackerIndex = (currentAttackerIndex + 1) % attacker.Length;
        yield return new WaitForSeconds(.4f);
    }

    public void Paze2Pattern()
    {
        StartCoroutine(AttackPattern());
    }

    public void Paze3Pattern()
    {
        StartCoroutine(AttackPattern());
    }
}
