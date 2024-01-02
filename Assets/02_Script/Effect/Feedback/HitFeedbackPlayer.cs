using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFeedbackPlayer : MonoBehaviour
{

    private readonly int HASH_BLINK = Shader.PropertyToID("_StrongTintFade");
    private readonly int HASH_SHAKE = Shader.PropertyToID("_VibrateFade");

    private SpriteRenderer spriteRenderer;

    private bool isMaterialFeedbackPlaying;

    private void Awake()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {

#if UNITY_EDITOR

        if(Input.GetKeyDown(KeyCode.Escape))
        {

            Play(Random.value);

        }

#endif

    }

    public void Play(float damage)
    {

        FAED.TakePool<DamageText>("DamageText", transform.position + (Vector3)Random.insideUnitCircle).Set(damage);

        MaterialFeedback();

    }

    private void MaterialFeedback()
    {

        if (isMaterialFeedbackPlaying) return;

        isMaterialFeedbackPlaying = true;

        spriteRenderer.material.SetFloat(HASH_BLINK, 1);
        spriteRenderer.material.SetFloat(HASH_SHAKE, 1);

        FAED.InvokeDelay(() =>
        {

            if (spriteRenderer != null)
            {

                isMaterialFeedbackPlaying = false;

                spriteRenderer.material.SetFloat(HASH_BLINK, 0);
                spriteRenderer.material.SetFloat(HASH_SHAKE, 0);

            }

        }, 0.07f);

    }

}
