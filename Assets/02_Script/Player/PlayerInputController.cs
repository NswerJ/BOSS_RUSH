using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : IDisposable
{

    public event Action JumpKeyPressdEvent;
    public event Action DashKeyPressdEvent;

    public Vector2 LastInputVector { get; private set; } = Vector2.left;
    public Vector2 InputVector { get; private set; }

    public void Update()
    {

        SettingMove();
        JumpKeyPress();

    }

    private void SettingMove()
    {

        float x = Input.GetAxisRaw("Horizontal");

        InputVector = new Vector2(x, 0);

        if(x != 0)
        {

            LastInputVector = new Vector2(x, 0);

        }

    }

    private void JumpKeyPress()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {

            JumpKeyPressdEvent?.Invoke(); 

        }

    }

    public void Dispose()
    {

        JumpKeyPressdEvent = null;

    }

}
