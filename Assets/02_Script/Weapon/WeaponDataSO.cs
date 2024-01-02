using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =("SO/Weapon/Sword"))]
public class WeaponDataSO : ScriptableObject
{

    public Sprite weaponImage;
    public GameObject attackPrefab;
    public float AttackPower;
    public float AttackCool;


    public bool isAttackCoolDown;
}
