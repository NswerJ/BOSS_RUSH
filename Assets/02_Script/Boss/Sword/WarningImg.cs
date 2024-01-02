using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WarningImg : MonoBehaviour
{
    float lifeTime;
    SpriteRenderer rend;

    public void ResetLifeTime()
    {
        lifeTime = 0.8f;
    }

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;

        rend.enabled = lifeTime > 0;
    }
}
