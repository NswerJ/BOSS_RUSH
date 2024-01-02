using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSencer : MonoBehaviour
{

    public event Action<bool> OnTriggerd;
    public bool isGround;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        isGround = true;
        OnTriggerd?.Invoke(true);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        isGround = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        isGround = false;
        OnTriggerd?.Invoke(false);

    }

}
