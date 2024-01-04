using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneChest : MonoBehaviour
{
    [SerializeField] WeaponDataSO weaponDataSO;
    WeaponStand weaponStand;

    private void Awake()
    {
        weaponStand = GetComponent<WeaponStand>();
    }

    private void Start()
    {
        if (FindObjectOfType<Data>() != null)
            gameObject.SetActive(PlayerPrefs.GetInt("File" +
            FindObjectOfType<Data>().name + "Boss" + 1) == 1);

        if (FindObjectOfType<Data>() != null)
        if(PlayerPrefs.GetInt("File" +
            FindObjectOfType<Data>().name + "Weapon") == 1)
        weaponStand.ChangeWeapon(weaponDataSO);
    }
}
