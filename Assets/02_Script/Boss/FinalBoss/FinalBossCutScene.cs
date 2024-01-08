using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FinalBossCutScene : MonoBehaviour
{
    
    [SerializeField] private TMP_Text bossText;
    [SerializeField] private UnityEvent blinkEvt;
    [SerializeField] private UnityEvent endEvent;

    private void Start()
    {

        bossText.color = new Color(0, 0, 1, 0);
        FadeEnd();

    }

    private void FadeEnd()
    {

        StartCoroutine(TextShowing());

    }

    private void TextShow(string text)
    {

        bossText.text = text;

        Sequence seq = DOTween.Sequence();

        seq.Append(bossText.DOFade(1, 1f).SetEase(Ease.OutQuart));
        seq.AppendInterval(0.1f);
        seq.Append(bossText.DOFade(0, 1f).SetEase(Ease.InSine));

    }

    private IEnumerator TextShowing()
    {

        yield return new WaitForSeconds(0.5f);

        TextShow("반갑다...");
        yield return new WaitForSeconds(2.7f);
        bossText.color = new Color(1, 0, 0, 0);
        TextShow("그리고.... 잘가라..!");
        yield return new WaitForSeconds(2.7f);

        yield return new WaitForSeconds(0.1f);
        blinkEvt?.Invoke();

        yield return new WaitForSeconds(.2f);
        endEvent?.Invoke();

    }

}
