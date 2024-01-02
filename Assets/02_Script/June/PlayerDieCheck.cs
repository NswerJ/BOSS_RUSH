using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieCheck : MonoBehaviour
{
    private HitObject hitObject;

    bool isdie = false;

    private void Awake()
    {
        hitObject = GameObject.Find("Player").GetComponent<HitObject>();
    }

    private void Update()
    {
        if(hitObject.hp <= 0 && !isdie)
        {
            isdie = true;
        }

        if(hitObject.hp > 0)
        {
            isdie = false;
        }
    }
}
