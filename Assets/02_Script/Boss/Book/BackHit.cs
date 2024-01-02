using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackHit : HitObject
{
    [Header("Book's root")]

    [SerializeField]
    private Book _root;
    public event Action<Book> HitEvent;

    public override void TakeDamage(float damage)
    {
        if (_isActivated == false) return;
        base.TakeDamage(damage);
        _root.Flip(false);

        HitEvent?.Invoke(_root);
    }
}
