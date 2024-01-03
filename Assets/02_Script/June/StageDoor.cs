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

    [Header("?±ë¶ˆ ê´€??")]
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
                Debug.Log("1");
                clear.SetActive(true);
            }
            else
            {
                Debug.Log("2");
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
            _doorTxt.DOText("F?¤ë? ?ŒëŸ¬ ?…ìž¥", 1f);
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
        if(PlayerPosSave.Instance != null )
        {
            PlayerPosSave.Instance.SavePos(transform.position.x,transform.position.y);
        }
        SceneManager.LoadScene(transform.name);
    }
}
