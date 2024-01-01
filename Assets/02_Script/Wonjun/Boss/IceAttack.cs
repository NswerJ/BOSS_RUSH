using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAttack : MonoBehaviour
{
    public GameObject[] attacker;
    public GameObject IceBlock;
    public Transform Target;

    private int currentAttackerIndex = 0;
    private bool isAttacking = false;

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

                yield return new WaitForSeconds(4f); 

                isAttacking = false;
            }
            yield return null;
        }
    }

    IEnumerator AttackSequence()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject ice = Instantiate(IceBlock, attacker[currentAttackerIndex].transform.position, Quaternion.identity);
            ice.transform.LookAt(Target);

            /*Rigidbody2D iceRB = ice.GetComponent<Rigidbody2D>();
            if (iceRB != null)
            {
                iceRB.velocity = ice.transform.forward * 10f;
            }*/

            yield return new WaitForSeconds(0.5f);

            yield return new WaitForSeconds(1f); 

            currentAttackerIndex = (currentAttackerIndex + 1) % attacker.Length; 
        }
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
