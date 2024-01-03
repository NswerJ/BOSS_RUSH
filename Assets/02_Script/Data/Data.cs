using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : DontDestroyOnLoad
{
    Data[] datas;
    protected override void Awake()
    {
        datas = FindObjectsOfType<Data>();
        if (datas.Length > 1)
            Destroy(this);
    }
}
