using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBossController : MonoBehaviour
{
    [Header("Main Boss")]
    [SerializeField]
    private BookHit _mainHitObject;
    [SerializeField]
    private MainBook _mainObject;

    [Header("Shuffle Pattern")]
    [SerializeField]
    private List<float> _triggerHP = new List<float>();
    private int _index = 0;

    [SerializeField]
    private List<DemonBook> _demonBookList = new List<DemonBook>();

    [SerializeField]
    private Shuffle _shuffle;
    [SerializeField]
    private AudioClip _impactClip;

    private void Awake()
    {
        _mainHitObject.hitEvent += HandleHPTrigger;

        for (int i = 0; i < _demonBookList.Count; ++i)
            _demonBookList[i].hit.DieEvent += HandleDemonBookDie;
    }

    private void HandleDemonBookDie()
    {
        _shuffle.ShuffleBook(_index - 1);
    }

    private void HandleHPTrigger(float hp)
    {
        if(hp <= _triggerHP[_index])
        {
            SoundManager.Instance.SFXPlay("Impact", _impactClip);
            _shuffle.DamageOff();
            _shuffle.PowerMaterial();
            _mainObject.PlayParticle();
            _index++;
            StopBossAI();

            StartCoroutine(Shuffle());
            
        }
    }

    IEnumerator Shuffle()
    {
        yield return new WaitForSeconds(1f);
        
        _shuffle.ShuffleBook(_index - 1);
    }

    private void StopBossAI()
    {
        _mainObject.AIStop();
    }
}
