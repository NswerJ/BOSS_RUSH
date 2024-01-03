using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventSystem : IDisposable
{

    public event Action<Vector2> MoveEvent;
    public event Action<float> AttackEvent;
    public event Action JumpEvent;
    public event Action DashEvent;

    public void MoveEventExecute(Vector2 move)
    {

        MoveEvent?.Invoke(move);

    }

    public void JumpEventExecute()
    {

        JumpEvent?.Invoke();

    }

    public void AttackEventExecute(float damage)
    {

        AttackEvent?.Invoke(damage);

    }

    public void DashEventExecute()
    {

        DashEvent?.Invoke();

    }

    public void Dispose()
    {

        JumpEvent = null;
        MoveEvent = null;
        AttackEvent = null;

    }

}
