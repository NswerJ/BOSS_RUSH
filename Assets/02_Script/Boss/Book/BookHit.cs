using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookHit : HitObject
{
    public event Action<float> hitEvent;

    public override void TakeDamage(float damage)
    {
        hitEvent?.Invoke(hp);
        base.TakeDamage(damage);
    }
}
