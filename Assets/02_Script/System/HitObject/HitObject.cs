using FD.Dev;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate void HitFeedback(float hp, float maxHP);

[RequireComponent(typeof(HitFeedbackPlayer))]
public class HitObject : MonoBehaviour
{

    [field: SerializeField] public float maxHP { get; protected set; }
    [SerializeField] public Stats defecnces;
    [SerializeField] private UnityEvent die;
    [SerializeField] private bool resetHPNo;

    private HitFeedbackPlayer hitPlayer;

    public float hp { get; set; }
    protected bool _isActivated = true;
    
    public event HitFeedback HitEventHpChanged;
    public event Action DieEvent;
    public event Action HitEvent;

    private void Awake()
    {

        hp = maxHP;
        hitPlayer = GetComponent<HitFeedbackPlayer>();

    }

    private void OnEnable()
    {

        if (resetHPNo) return;

        hp = maxHP;

    }

    public virtual void TakeDamage(float damage)
    {
        if (_isActivated == false) return;
        if (hp <= 0) return;

        HitEvent?.Invoke();


        var value = damage - defecnces.GetValue();

        value = Mathf.Clamp(value, 0, float.MaxValue);

        hp -= value;
        hitPlayer.Play(value);

        HitEventHpChanged?.Invoke(hp, maxHP);

        if (hp <= 0)
        {

            DieEvent?.Invoke();
            die?.Invoke();

            if (DieEvent == null)
            {

                FAED.InsertPool(gameObject);

            }
            ///
        }

    }

    public void HealingObject(float value)
    {
        hp += value;
        hp = Mathf.Clamp(hp, 0f, maxHP);

        HitEventHpChanged?.Invoke(hp, maxHP);

    }

    public void SetHP(float value)
    {
        hp = value;
    }

    public void SetActivate(bool value)
    {
        _isActivated = value;
    }

}