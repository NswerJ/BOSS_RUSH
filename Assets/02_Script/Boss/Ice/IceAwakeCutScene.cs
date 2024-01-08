using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IceAwakeCutScene : MonoBehaviour
{

    [SerializeField] private Image fadeImage;
    [SerializeField] private Image blinkImage;
    [SerializeField] private TMP_Text bossText;
    [SerializeField] private UnityEvent blinkEvt;
    [SerializeField] private UnityEvent endEvent;
    [SerializeField] private AudioClip ding, chgit;

    private void Start()
    {

        bossText.color = new Color(0, 0, 1, 0);

        fadeImage.DOFade(1, 0);
        fadeImage.DOFade(0, 0.5f).OnComplete(() =>
        {

            FadeEnd();

        });

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

        TextShow("?òÎ? ?¨Í∏∞ÍπåÏ? Î∞Ä??Î∂ôÏù¥?§Îãà");
        SoundManager.Instance.SFXPlay("ding", ding);
        yield return new WaitForSeconds(2.7f);
        TextShow("ÏßÄÍ∏àÎ???ÏßÑÏã¨?ºÎ°ú ?ÅÎ??¥Ï£ºÏßÄ");
        SoundManager.Instance.SFXPlay("ding", ding);
        yield return new WaitForSeconds(2.7f);

        blinkImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        SoundManager.Instance.SFXPlay("chgit", chgit);
        blinkEvt?.Invoke();
        blinkImage.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.5f);
        endEvent?.Invoke();

    }

}
