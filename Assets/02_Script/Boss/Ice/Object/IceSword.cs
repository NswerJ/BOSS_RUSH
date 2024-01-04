using Cinemachine;
using DG.Tweening;
using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSword : MonoBehaviour
{

    private CinemachineImpulseSource source;

    private void Awake()
    {
        
        source = GetComponent<CinemachineImpulseSource>();

    }

    public void Spawn()
    {

        Sequence seq = DOTween.Sequence();

        transform.localScale = Vector3.one / 3;
        transform.eulerAngles = new Vector3(0, 0, 90);

        FAED.TakePool("ExpEffect", transform.position);

        seq.Append(transform.DOScale(Vector2.one * 2, 0.5f).SetEase(Ease.InOutBounce));
        seq.Join(transform.DORotate(Vector3.zero, 0.5f).SetEase(Ease.InOutBounce));
        seq.AppendCallback(() =>
        {

            source.GenerateImpulse(0.1f);
            StartCoroutine(SpawnCo());

        });



    }

    private IEnumerator SpawnCo()
    {

        float randomAngle = Random.Range(0, 180f);
        float angle = 360 / 6;

        for (int i = 0; i < 6; i++)
        {

            var obj = FAED.TakePool<IceShard>("IceSword_A", transform.position, Quaternion.Euler(0, 0, (angle * i) + randomAngle));
            obj.ImmediatelySpawn(obj.transform.up);

        }

        yield return new WaitForSeconds(1f);

        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOScale(Vector2.zero, 0.5f).SetEase(Ease.InOutBounce));
        seq.Join(transform.DORotate(new Vector3(0, 0, 90), 0.5f).SetEase(Ease.InOutBounce));
        seq.AppendCallback(() =>
        {

            FAED.InsertPool(gameObject);

        });

    }

}
