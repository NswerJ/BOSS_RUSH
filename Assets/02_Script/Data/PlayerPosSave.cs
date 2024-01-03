using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosSave : MonoBehaviour
{
    public static PlayerPosSave Instance;

    int fileNum = 0;
    string fileStr = "";
    Transform player;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogError($"{transform} : PlayerPosSave is Multiply running!");
            Destroy(this);
        }
    }

    private void Start()
    {
        fileNum = DataManager.Instance.DataIndex;
        player = GameObject.Find("Player").transform;
        fileStr = "File" + fileNum;
        player.transform.position = new 
            Vector2(PlayerPrefs.GetFloat(fileStr + "XPos",-5),
            PlayerPrefs.GetFloat(fileStr + "YPos", 0));
    }

    private void Update()
    {
        SavePos(player.position.x, player.position.y);
    }

    public void SavePos(float x, float y)
    {
        PlayerPrefs.SetFloat(fileStr + "XPos", x);
        PlayerPrefs.SetFloat(fileStr + "YPos", y);
    }
}