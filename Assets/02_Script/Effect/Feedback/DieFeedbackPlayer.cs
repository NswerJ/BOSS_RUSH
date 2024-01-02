using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieFeedbackPlayer : MonoBehaviour
{

    private readonly int HASH_FADE = Shader.PropertyToID("_FullGlowDissolveFade");

    private SpriteRenderer spriteRenderer;
    private WeaponController playerWeapon;
    private PlayerController controller;
    private HitObject hitObj;

    private void Awake()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerWeapon = GetComponentInChildren<WeaponController>();
        controller = GetComponent<PlayerController>();
        hitObj = GetComponent<HitObject>();
        hitObj.DieEvent += Die;

    }

    public void Die()
    {

        controller.ChangeIdle();
        Destroy(playerWeapon.gameObject);

        StartCoroutine(DissolveCo());

    }

    private IEnumerator DissolveCo()
    {

        float per = 0;

        while (per < 1)
        {

            yield return null;
            per += Time.deltaTime * 1.5f;

            spriteRenderer.material.SetFloat(HASH_FADE, 1 - per);

        }

    }

}
