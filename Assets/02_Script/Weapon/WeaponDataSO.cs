using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =("SO/Weapon/Sword"))]
public class WeaponDataSO : ScriptableObject
{
    [SerializeField] private Sprite weaponImage;

    [SerializeField] public GameObject attackPrefab;
    [SerializeField] public float AttackPower;
    [SerializeField] public float AttackCool;


    public bool isAttackCoolDown;
}
