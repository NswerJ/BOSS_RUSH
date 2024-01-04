using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    protected virtual void Awake()
    {
        DontDestroyOnLoad(this);
    }
}