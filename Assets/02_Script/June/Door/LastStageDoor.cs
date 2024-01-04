using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastStageDoor : StageDoor
{
    bool StageAllClear = false;
    [SerializeField] GameObject lockObj;

    protected override void Start()
    {
        base.Start();

        if(FindObjectOfType<Data>() != null)
        {
            StageAllClear = true;
            for (int i = 0; i < 3; i++)
            {
                if (PlayerPrefs.GetInt("File" + int.Parse(FindObjectOfType<Data>().name)
                    + "Boss" + (i + 1)) == 0)
                {
                    StageAllClear = false;
                }
            }
        }
        if (StageAllClear)
            lockObj.SetActive(false);
    }

    protected override void Update()
    {
        if (StageAllClear && isCollision && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(DoFadeAndChangeScene());
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (collision.transform.CompareTag("Player"))
            {
                isCollision = true;

                if(StageAllClear)
                    _doorTxt.DOText("F키를 눌러 입장", 1f);
                else
                    _doorTxt.DOText("아직 입장할 수 없다.", 1f);
            }
        }
    }
}