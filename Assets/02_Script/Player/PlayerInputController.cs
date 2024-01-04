using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        DashKeyPress();

        #region Debug

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {

            SceneManager.LoadScene("IceEnd");

        }
        else if(Input.GetKeyDown(KeyCode.Alpha9)) 
        {

            var obj = UnityEngine.Object.FindObjectsOfType<HitObject>();

            foreach(var item in obj)
            {

                if (item.name == "Player") continue;


                item.defecnces.AddMod(-1000);
            }

        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {

            var obj = UnityEngine.Object.FindObjectsOfType<HitObject>();

            foreach (var item in obj)
            {

                if (item.name == "Player")
                {

                    item.HealingObject(100);

                }

            }


        }

        #endregion

    }

    private void DashKeyPress()
    {

        if(Input.GetMouseButtonDown(1)) 
        { 
            
            DashKeyPressdEvent?.Invoke();

        }

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
