using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{

    [SerializeField] private Slider hpBarSlider;
    [SerializeField] private HitObject hitObject;


    private void Awake()
    {

        hitObject.HitEventHpChanged += HandleHPChanged;

    }

    private void HandleHPChanged(float hp, float maxHP)
    {

        hpBarSlider.value = hp / maxHP;

    }

}
