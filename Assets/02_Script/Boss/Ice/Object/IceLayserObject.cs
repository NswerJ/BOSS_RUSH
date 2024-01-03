using DG.Tweening;
using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceLayserObject : MonoBehaviour
{
    
    public void Spawn()
    {

        Sequence seq = DOTween.Sequence();

        transform.localScale = Vector3.one / 3;
        transform.eulerAngles = new Vector3(0, 0, 90);

        seq.Append(transform.DOScale(Vector2.one * 2, 0.5f).SetEase(Ease.InOutBounce));
        seq.Join(transform.DORotate(Vector3.zero, 0.5f).SetEase(Ease.InOutBounce));
        seq.AppendCallback(() =>
        {

            StartCoroutine(SpawnCo());

        });
          
            

    }

    private IEnumerator SpawnCo()
    {

        float randomAngle = Random.Range(0, 180f);
        float angle = 360 / 6;

        for(int i = 0; i < 6; i++)
        {

            var obj = FAED.TakePool<IceLayser>("IceLayser", transform.position, Quaternion.Euler(0, 0, (angle * i) + randomAngle));
            obj.Show(transform.position, obj.transform.right);

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
