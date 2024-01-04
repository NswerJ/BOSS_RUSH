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

        transform.localScale = new Vector3(3, 3, 1);
        text.color = Color.white;

        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOPunchScale(Vector2.one * 5f, 0.05f).SetEase(Ease.OutExpo));
        seq.AppendInterval(0.1f);
        seq.Append(transform.DOScale(Vector2.one, 0.3f).SetEase(Ease.OutExpo));
        seq.Append(text.DOColor(Color.red, 0.1f));
        seq.AppendInterval(0.1f);
        seq.Append(text.DOFade(0, 0.5f));
        seq.AppendCallback(() => FAED.InsertPool(gameObject));

        //seq.Append(transform.DOScale(Vector2.one * 5f, 0.1f).SetEase(Ease.OutExpo))
        //    .Append(transform.DOShakeScale(0.1f, 10f));
        //seq.AppendInterval(0.2f);
        //seq.Append(transform.DOScale(Vector2.one, 0)).SetEase(Ease.OutExpo)
        //    .Append(transform.DOShakeScale(0.1f, 5f));
        //seq.Append(text.DOColor(Color.red, 0.1f));
        //seq.AppendInterval(0.1f);
        //seq.Append(text.DOFade(0, 0.5f));
        //seq.AppendCallback(() => FAED.InsertPool(gameObject));
    }

}
