using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HitFeedbackPlayer))]
public class HitObject : MonoBehaviour
{

    [field:SerializeField] public float maxHP { get; protected set; }
    [SerializeField] private Stats defecnces;

    private HitFeedbackPlayer hitPlayer;

    public float hp { get; set; }

    public event Action DieEvent;
    public event Action HitEvent;

    private void Awake()
    {

        hp = maxHP;
        hitPlayer = GetComponent<HitFeedbackPlayer>();

    }

    public virtual void TakeDamage(float damage)
    {

        if (hp <= 0) return;

        HitEvent?.Invoke();

        var value = damage - defecnces.GetValue();

        value = Mathf.Clamp(value, 0, float.MaxValue);

        hp -= value;
        hitPlayer.Play(value);

        if(hp <= 0)
        {

            DieEvent?.Invoke();

            if(DieEvent == null)
            {

                Destroy(gameObject);

            }

        }

    }

}
