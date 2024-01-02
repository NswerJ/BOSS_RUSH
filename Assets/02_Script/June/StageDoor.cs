using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageDoor : MonoBehaviour
{
    [SerializeField] Image _blackImage;

    TextMeshPro _doorTxt;
    Canvas _canvas;

    bool isCollision = false;

    private void Awake()
    {
        _doorTxt = transform.GetComponentInChildren<TextMeshPro>();
        _canvas = transform.GetComponentInChildren<Canvas>();
        _canvas.worldCamera = Camera.main;
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
        _blackImage.DOFade(1, 2f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(transform.name);
    }
}
