using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventSystem : IDisposable
{

    public event Action<Vector2> MoveEvent;
    public event Action AttackEvent;
    public event Action JumpEvent;

    public void MoveEventExecute(Vector2 move)
    {

        MoveEvent?.Invoke(move);

    }

    public void DashEventExecute()
    {

        JumpEvent?.Invoke();

    }

    public void AttackEventExecute()
    {

        AttackEvent?.Invoke();

    }

    public void Dispose()
    {

        JumpEvent = null;
        MoveEvent = null;
        AttackEvent = null;

    }

}
