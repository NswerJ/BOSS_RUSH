using DG.Tweening;
using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossDieCutScene : MonoBehaviour
{

    [SerializeField] private ParticleSystem boomParticle;
    [SerializeField] private Image fadeImage;
    [SerializeField] private UnityEvent cutsceneEnd;

    private void Awake()
    {

        //Play();

    }

    public void Play()
    {

        boomParticle.Play();

        FAED.InvokeDelay(() =>
        {

            fadeImage.DOFade(1, 3).OnComplete(() =>
            {

                boomParticle.Stop();

                FAED.InvokeDelay(() =>
                {

                    fadeImage.DOFade(0, 0.5f).OnComplete(() =>
                    {

                        cutsceneEnd?.Invoke();

                    });


                }, 0.25f);

            });

        }, 0.2f);


    }

    public void SceneChange(string sceneName)
    {

        SceneManager.LoadScene(sceneName);

    }

}