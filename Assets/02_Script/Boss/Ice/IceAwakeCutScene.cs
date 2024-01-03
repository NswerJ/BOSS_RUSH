using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IceAwakeCutScene : MonoBehaviour
{

    [SerializeField] private Image fadeImage;
    [SerializeField] private TMP_Text text;

    private void Start()
    {

        fadeImage.DOFade(1, 0);
        fadeImage.DOFade(0, 0.5f).OnComplete(() =>
        {

            FadeEnd();

        });

    }

    private void FadeEnd()
    {



    }

}
