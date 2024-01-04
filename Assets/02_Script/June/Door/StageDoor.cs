using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageDoor : MonoBehaviour
{
    protected TextMeshPro _doorTxt;

    protected bool isCollision = false;

    public bool isUseFire = false;
    public int roomIdx;
    [SerializeField] GameObject notclear;
    [SerializeField] GameObject clear;

    protected void Awake()
    {
        _doorTxt = transform.GetComponentInChildren<TextMeshPro>();
    }

    protected virtual void Start()
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

    protected virtual void Update()
    {
        if(isCollision && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(DoFadeAndChangeScene());
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isCollision = true;
            _doorTxt.DOText("F키를 눌러 입장", 1f);
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            _doorTxt.DOKill();
            _doorTxt.text = "";
            isCollision = false;
        }
    }

    protected IEnumerator DoFadeAndChangeScene()
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
