using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WarningImg : MonoBehaviour
{
    float lifeTime;
    SpriteRenderer rend;
    private Light2D warningLight;

    public void ResetLifeTime()
    {
        lifeTime = 0.8f;
    }

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        warningLight = transform.GetChild(0).GetComponent<Light2D>();
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;

        bool active = lifeTime > 0;
        rend.enabled = active;
        warningLight.intensity = Mathf.Clamp(lifeTime / 2, 0, 1);
    }
}
