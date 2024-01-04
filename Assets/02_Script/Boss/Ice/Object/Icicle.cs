using DG.Tweening;
using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour
{

    private AddGravity addGravity;

    private void Awake()
    {
        
        addGravity = GetComponent<AddGravity>();
        addGravity.enabled = false;

    }

    public void Spawn(Transform point)
    {

        transform.position = point.position + new Vector3(Random.Range(-15f, 15f), 0);
        transform.localScale = Vector3.one / 4;
        
        addGravity.enabled = false;

        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(0.3f);
        seq.Append(transform.DOShakeRotation(0.3f).SetEase(Ease.OutBounce));
        seq.Join(transform.DOScale(Vector2.one / 3, 0.3f).SetEase(Ease.OutBounce));
        seq.AppendInterval(0.1f);
        seq.Append(transform.DOShakeRotation(0.3f).SetEase(Ease.OutBounce));
        seq.Join(transform.DOScale(Vector2.one / 2, 0.3f).SetEase(Ease.OutBounce));
        seq.AppendInterval(0.1f);
        seq.Append(transform.DOShakeRotation(0.3f).SetEase(Ease.OutBounce));
        seq.Join(transform.DOScale(Vector2.one, 0.3f).SetEase(Ease.OutBounce));
        seq.AppendInterval(0.2f);
        seq.AppendCallback(() =>
        {

            addGravity.enabled = true;

        });

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (addGravity.enabled == false) return;

        if(collision.CompareTag("Ground") || collision.CompareTag("Player"))
        {

            if(collision.TryGetComponent<HitObject>(out var hit))
            {

                hit.TakeDamage(10);

            }

            FAED.TakePool("BoomDestroy", transform.position);
            FAED.InsertPool(gameObject);

        }


    }

}
