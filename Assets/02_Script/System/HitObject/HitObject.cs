using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HitFeedback(float hp, float maxHP);

[RequireComponent(typeof(HitFeedbackPlayer))]
public class HitObject : MonoBehaviour
{

    [field: SerializeField] public float maxHP { get; protected set; }
    [SerializeField] public Stats defecnces;

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

            if (DieEvent == null)
            {

                Destroy(gameObject);

            }
            ///
        }

    }

    public void HealingObject(float value)
    {

        hp += value;

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