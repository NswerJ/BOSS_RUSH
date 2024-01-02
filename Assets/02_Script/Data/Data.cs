using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : DontDestroyOnLoad
{
    Data[] datas;
    private void Start()
    {
        datas = transform.parent.GetComponentsInChildren<Data>();
        if (datas.Length > 1)
            Destroy(this);
    }
}
