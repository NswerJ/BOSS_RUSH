using DG.Tweening;
using FD.Dev;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShard : MonoBehaviour
{

    private readonly int HASH_FADE = Shader.PropertyToID("_DirectionalGlowFadeFade");

    [SerializeField] private AudioClip showSound;
    [SerializeField] private AudioClip destroySound;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private ParticleSystem createParticle;
    [SerializeField] private ParticleSystem moveParicle;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigid;

    private void Awake()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();    

    }

    public void Spawn(Transform target, float shootDelay, bool showShowPlay = true)
    {

        if (showShowPlay)
        {

            SoundManager.Instance.SFXPlay("IceShow", showSound);

        }

        createParticle.Play();

        spriteRenderer.material.SetFloat(HASH_FADE, 0);
        transform.localScale = Vector2.one / 2;

        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutSine);
        StartCoroutine(SpawnCo(target, shootDelay));

    }

    public void Spawn(Vector2 dir, float shootDelay, bool showShowPlay = true)
    {

        if (showShowPlay)
        {

            SoundManager.Instance.SFXPlay("IceShow", showSound);

        }

        spriteRenderer.material.SetFloat(HASH_FADE, 0);
        transform.localScale = Vector2.one / 2;

        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutSine);
        StartCoroutine(SpawnCo(dir, shootDelay));

    }

    public void ImmediatelySpawn(Vector2 dir)
    {

        transform.up = dir;

        Shooting(dir);

    }

    private IEnumerator SpawnCo(Transform target, float shootDelay)
    {

        var dir = target.position - transform.position;
        dir.z = 0;
        dir.Normalize();

        float per = 0;

        var co = StartCoroutine(ShardRotateCo(dir));

        while(per < 1)
        {

            per += Time.deltaTime * 1.5f;
            spriteRenderer.material.SetFloat(HASH_FADE, Mathf.Lerp(0, 1.3f, FAED.Easing(FAED_Easing.OutSine, per)));
            yield return null;

        }

        yield return co;

        createParticle.Stop();

        yield return new WaitForSeconds(shootDelay);

        Shooting(dir);

    }

    private IEnumerator SpawnCo(Vector2 dir, float shootDelay)
    {

        float per = 0;

        var co = StartCoroutine(ShardRotateCo(dir));

        while (per < 1)
        {

            per += Time.deltaTime * 2;
            spriteRenderer.material.SetFloat(HASH_FADE, Mathf.Lerp(0, 1.3f, FAED.Easing(FAED_Easing.OutSine, per)));
            yield return null;

        }

        yield return co;

        yield return new WaitForSeconds(shootDelay);

        Shooting(dir);

    }

    private IEnumerator ShardRotateCo(Vector2 dir)
    {

        float per = 0;

        var ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        ang -= 90;
        transform.eulerAngles = new Vector3(0, 0, -ang);

        yield return null;

        while (per <= 1)
        {

            per += Time.deltaTime * 1.3f;
            transform.eulerAngles = Vector3.Lerp(new Vector3(0, 0, -ang), new Vector3(0, 0, ang), FAED.Easing(FAED_Easing.OutSine, per));
            yield return null;

        }


    }

    private void Shooting(Vector2 dir)
    {

        SoundManager.Instance.SFXPlay("Shot", shootSound);
        moveParicle.Play();

        rigid.velocity = dir * 15;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("HitAble") || collision.CompareTag("Player"))
        {

            if (collision.TryGetComponent<HitObject>(out var hit))
            {

                hit.TakeDamage(5f);

            }


            moveParicle.Stop();


            FAED.TakePool("BoomDestroy", transform.position);
            SoundManager.Instance.SFXPlay("Dest", destroySound);

            FAED.InsertPool(gameObject);

        }


    }

}
