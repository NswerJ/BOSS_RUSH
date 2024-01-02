using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBossController : MonoBehaviour
{
    [Header("Main Boss")]
    [SerializeField]
    private BookHit _mainHitObject;

    [Header("Shuffle Pattern")]
    [SerializeField]
    private List<float> _triggerHP = new List<float>();
    private int _index = 0;

    [SerializeField]
    private Shuffle _shuffle;

    private void Awake()
    {
        _mainHitObject.hitEvent += HandleHPTrigger;
    }

    private void HandleHPTrigger(float hp)
    {
        if(hp <= _triggerHP[_index])
        {
            StopBossAI();
            _shuffle.ShuffleBook(_index);

            _index++;
        }
    }

    private void StopBossAI()
    {

    }
}
