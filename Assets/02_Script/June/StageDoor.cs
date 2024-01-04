using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageDoor : MonoBehaviour
{
    TextMeshPro _doorTxt;

    bool isCollision = false;

    public bool isUseFire = false;
    public int roomIdx;
    [SerializeField] GameObject notclear;
    [SerializeField] GameObject clear;

    private void Awake()
    {
        _doorTxt = transform.GetComponentInChildren<TextMeshPro>();
    }

    private void Start()
    {
        if(isUseFire)
        {
            if (PlayerPrefs.GetInt("File" + DataManager.Instance.DataIndex + "Boss" + roomIdx,0) == 1)
            {
                clear.SetActive(true);
            }
            else
            {
                notclear.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if(isCollision && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(DoFadeAndChangeScene());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isCollision = true;
            _doorTxt.DOText("F키를 눌러 입장", 1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            _doorTxt.DOKill();
            _doorTxt.text = "";
            isCollision = false;
        }
    }

    IEnumerator DoFadeAndChangeScene()
    {
        MainFadeImage.Instance.FadeIn();
        yield return new WaitForSeconds(2f);
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance._bgSound.volume = 0.5f;
        }
        if (PlayerPosSave.Instance != null )
        {
            PlayerPosSave.Instance.SavePos(transform.position.x,transform.position.y);
        }
        SceneManager.LoadScene(transform.name);
    }
}
