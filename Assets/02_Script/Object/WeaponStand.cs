using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStand : MonoBehaviour
{
    Sprite image;

    private void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, 1, 1 << 7))
        {
            
        }
    }
}
