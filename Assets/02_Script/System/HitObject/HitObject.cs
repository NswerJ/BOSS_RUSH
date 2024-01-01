using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HitFeedbackPlayer))]
public class HitObject : MonoBehaviour
{

    [SerializeField] private float maxHP;
    [SerializeField] private Stats defecnces;

    private HitFeedbackPlayer hitPlayer;

    private float hp;

    public event Action DieEvent;

    private void Awake()
    {

        hp = maxHP;
        hitPlayer = GetComponent<HitFeedbackPlayer>();

    }

    public virtual void TakeDamage(float damage)
    {

        if (hp <= 0) return;

        var value = damage - defecnces.GetValue();

        value = Mathf.Clamp(damage, 0, float.MaxValue);

        hp -= value;
        hitPlayer.Play(hp);

        if(hp <= 0)
        {

            DieEvent?.Invoke();

        }

    }

}
