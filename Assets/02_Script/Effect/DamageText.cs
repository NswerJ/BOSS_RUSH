using DG.Tweening;
using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{

    private TMP_Text text;

    private void Awake()
    {
        
        text = GetComponent<TMP_Text>();

    }

    public void Set(float damage)
    {

        text.text = damage.ToString("0");

        transform.localScale = new Vector3(1.75f, 0.15f, 1);
        text.color = Color.white;

        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOMoveY(transform.position.y + 0.2f, 0.25f).SetEase(Ease.InOutElastic));
        seq.Join(transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBounce));
        seq.Append(transform.DOScale(new Vector3(0.6f, 1.75f), 0.1f).SetEase(Ease.InOutBounce));
        seq.Append(transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.OutBounce));
        seq.InsertCallback(0.2f, () =>
        {

            text.color = Color.red;

        });
        seq.AppendInterval(0.15f);
        seq.Append(text.DOFade(0, 0.15f).SetEase(Ease.InOutSine));
        seq.AppendCallback(() =>
        {

            FAED.InsertPool(gameObject);

        });

    }

}
