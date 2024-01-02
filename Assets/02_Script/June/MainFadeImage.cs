using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainFadeImage : MonoBehaviour
{
    public static MainFadeImage Instance;

    Image image;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
        {
            Debug.LogError($"{transform} : MainFadeImage is Multiply running!");
            Destroy(this);
        }
        image = GetComponent<Image>();
    }

    void Start()
    {
        transform.localScale = Vector3.one;
        image.DOFade(0, 0.8f);
    }

    public void FadeIn() => image.DOFade(1, 1.2f);
}