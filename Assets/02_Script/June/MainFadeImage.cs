using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainFadeImage : MonoBehaviour
{
    Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    void Start()
    {
        transform.localScale = Vector3.one;
        image.DOFade(0, 0.8f);
    }
}
