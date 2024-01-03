using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDieCheck : MonoBehaviour
{
    private HitObject hitObject;

    bool isdie = false;

    private void Awake()
    {
        hitObject = GameObject.Find("Player").GetComponent<HitObject>();
    }

    private void Update()
    {
        if(hitObject.hp <= 0 && !isdie)
        {
            StartCoroutine(IsDie());
            isdie = true;
        }

        if(hitObject.hp > 0)
        {
            isdie = false;
        }
    }

    IEnumerator IsDie()
    {
        if(DataManager.Instance != null)
            DataManager.Instance.PlayerDie();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Main");
    }
}
