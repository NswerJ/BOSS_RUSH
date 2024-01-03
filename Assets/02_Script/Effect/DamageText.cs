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

        transform.localScale = new Vector3(7, 7, 1);
        text.color = Color.white;

        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOScale(Vector2.one / 4, 0.5f).SetEase(Ease.OutExpo));
        seq.Append(transform.DOScale(Vector2.one, 0.3f).SetEase(Ease.OutExpo));
        seq.Join(text.DOColor(Color.red, 0.3f));
        seq.AppendInterval(0.1f);
        seq.Append(text.DOFade(0, 0.3f));
        seq.AppendCallback(() => FAED.InsertPool(gameObject));

    }

}
