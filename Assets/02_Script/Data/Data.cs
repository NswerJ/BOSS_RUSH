using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Data : DontDestroyOnLoad
{
    Data[] datas;
    protected override void Awake()
    {
        base.Awake();
        datas = FindObjectsOfType<Data>();
        if (datas.Length > 1)
            Destroy(gameObject);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += ChangeScene;
    }

    private void ChangeScene(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "IntroScene")
            gameObject.name = "DataObject";
    }
}
