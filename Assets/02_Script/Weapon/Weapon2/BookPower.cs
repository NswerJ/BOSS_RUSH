using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPower : MonoBehaviour
{
    [SerializeField]
    private bool _on = false;

    [SerializeField]
    private Transform _shotPos;
    [SerializeField]
    private Bullet _playerBullet;
    [SerializeField]
    private WeaponController _weaponController;

    private void Start()
    {
        LoadData(); // Get Save Data And Setting

        if (_on)
            OnAbility();
    }

    private void OnAbility()
    {
        SetAbility(true);
    }

    public void SetAbility(bool on)
    {
        _on = on;

        if (on)
            _weaponController.attackEvent += HandleAttack;
        else
            _weaponController.attackEvent -= HandleAttack;
    }

    private void HandleAttack(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Bullet spawnBullet = Instantiate(_playerBullet, _shotPos.position, Quaternion.identity);
        spawnBullet.BulletRotate(angle, dir);

        Destroy(spawnBullet.gameObject, 1f);
    }

    public void LoadData()
    {
        if(DataManager.Instance != null)
        _on = PlayerPrefs.GetInt("File" + 
            DataManager.Instance.DataIndex + "Boss" + 2) == 1;
    }
}
