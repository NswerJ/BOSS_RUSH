using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{

    [SerializeField] private Slider hpBarSlider;

    private HitObject hitObject;

    private void Awake()
    {
        
        hitObject = FindObjectOfType<PlayerController>().GetComponent<HitObject>();
        hitObject.HitEventHpChanged += HandleHPChanged;

    }

    private void HandleHPChanged(float hp, float maxHP)
    {

        hpBarSlider.value = hp / maxHP; 

    }

}
