using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HitFeedbackPlayer))]
public class HitObject : MonoBehaviour
{

    [SerializeField] private float maxHP;
    [SerializeField] private Stats defecnces;

    public float MaxHP => maxHP;
    private HitFeedbackPlayer hitPlayer;

    protected float hp;
    protected bool _isActivated = true;

    public event Action DieEvent;

    private void Awake()
    {

        hp = maxHP;
        hitPlayer = GetComponent<HitFeedbackPlayer>();

    }

    public virtual void TakeDamage(float damage)
    {
        if (_isActivated == false) return;
        if (hp <= 0) return;

        var value = damage - defecnces.GetValue();

        value = Mathf.Clamp(value, 0, float.MaxValue);

        hp -= value;
        hitPlayer.Play(value);

        if(hp <= 0)
        {

            DieEvent?.Invoke();

        }

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
