using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =("SO/Weapon/Sword"))]
public class WeaponDataSO : ScriptableObject
{
    [SerializeField] public float AttackPower;
    [SerializeField] public float AttackCool;

    private WeaponRoot owner;

    public bool isAttackCoolDown { get; protected set; }

    public void Init(WeaponRoot owner)
    {

        this.owner = owner;

    }

    public void SetCoolDown()
    {

        owner.StartCoroutine(SetCoolDownCo());

    }

    private IEnumerator SetCoolDownCo()
    {

        isAttackCoolDown = true;

        yield return new WaitForSeconds(AttackCool);

        isAttackCoolDown = false;

    }

}
