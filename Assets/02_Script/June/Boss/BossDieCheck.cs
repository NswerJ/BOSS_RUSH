using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossDieCheck : MonoBehaviour
{
    bool isFirst = true;
    [SerializeField] private GameObject _door;
    [SerializeField] int index = 0;
    private HitObject _hitObject;

    private void Awake()
    {
        _hitObject = GetComponent<HitObject>();
    }

    private void Start()
    {
        _hitObject.HitEventHpChanged += DieCheck;
    }

    private void DieCheck(float hp, float maxHP)
    {
        if (hp <= 0 && isFirst)
        {
            isFirst = false;
            DieEvent();
        }
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.C)
            && Input.GetKey(KeyCode.G))
        {
            _hitObject.hp = 1;
        }
    }

    public abstract void DieEvent();

    protected void EndFun()
    {
        _door.SetActive(true);
        if(DataManager.Instance != null )
            DataManager.Instance.ClearMap(index);
    }
}