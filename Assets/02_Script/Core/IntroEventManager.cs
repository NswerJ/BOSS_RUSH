using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroEventManager : MonoBehaviour
{
    public static IntroEventManager Instance;

    [SerializeField] RectTransform _startPaenl;
    [SerializeField] RectTransform _uiPaenl;
    [SerializeField] RectTransform _settingPaenl;
    [SerializeField] Image blackImage;
    [SerializeField] TextMeshProUGUI _tmpro;

    [SerializeField] private float moveSpeed = 0.8f;
    [SerializeField] private AudioClip _btnClip;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogError($"{transform} : IntroEventManager is Multiple running!");
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.E))
        {
            PlayerPrefs.SetInt("Tuto", 0);
        }
    }

    public void GameStart()
    {
        PlaySound();
        _startPaenl.DOLocalMoveX(0, moveSpeed);
        _uiPaenl.DOLocalMoveX(1920, moveSpeed);
        _settingPaenl.DOLocalMoveX(3840, moveSpeed);
    }

    public void Setting()
    {
        PlaySound();
        _startPaenl.DOLocalMoveX(-3840, moveSpeed);
        _uiPaenl.DOLocalMoveX(-1920, moveSpeed);
        _settingPaenl.DOLocalMoveX(0, moveSpeed);
    }

    public void Back()
    {
        PlaySound();
        _startPaenl.DOLocalMoveX(-1920, moveSpeed);
        _uiPaenl.DOLocalMoveX(0, moveSpeed);
        _settingPaenl.DOLocalMoveX(1920, moveSpeed);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void TutoBtn()
    {
        PlaySound();
        StartCoroutine(FadeOutAndTuto());
        SceneManager.LoadScene("TutorialScene");
    }

    public void StartGame(int num)
    {
        PlaySound();
        StartCoroutine(FadeOutAndStart());
        FindObjectOfType<Data>().name = num.ToString();
    }

    IEnumerator FadeOutAndTuto()
    {
        blackImage.gameObject.SetActive(true);
        blackImage.DOFade(1, 1f);

        yield return new WaitForSeconds(1.5f);
        
            SceneManager.LoadScene("TutorialScene");
        
    }

    IEnumerator FadeOutAndStart()
    {
        blackImage.gameObject.SetActive(true);
        blackImage.DOFade(1, 1f);

        yield return new WaitForSeconds(1.5f);
        if (PlayerPrefs.GetInt("Tuto") == 0)
        {
            SceneManager.LoadScene("TutorialScene");
        }
        else
        {
            SceneManager.LoadScene("Main");
        }
    }

    public void PlaySound()
    {
        SoundManager.Instance.SFXPlay("ButtonClick", _btnClip);
    }
}