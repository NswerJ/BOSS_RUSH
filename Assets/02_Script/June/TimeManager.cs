using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : DontDestroyOnLoad
{
    public static TimeManager Instance;
    protected override void Awake()
    {
        base.Awake();
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        float time = 0f;
        time += Time.deltaTime;
        PlayerPrefs.SetFloat("PlayTime", PlayerPrefs.GetFloat("PlayTime", 0f) + time);
    }
}