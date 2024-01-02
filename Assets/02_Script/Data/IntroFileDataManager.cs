using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroFileDataManager : MonoBehaviour
{
    FileSelectBoarder[] slots;

    private void Awake()
    {
        slots = GetComponentsInChildren<FileSelectBoarder>();
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("File1",-1) == -1)
        {
            slots[0].SetEmpty();
        }
        else
        {
            slots[0].SetFill(PlayerPrefs.GetInt("File1"));
        }

        if (PlayerPrefs.GetInt("File2",-1) == -1)
        {
            slots[1].SetEmpty();
        }
        else
        {
            slots[1].SetFill(PlayerPrefs.GetInt("File2"));
        }

        if (PlayerPrefs.GetInt("File3", -1) == -1)
        {
            slots[2].SetEmpty();
        }
        else
        {
            slots[2].SetFill(PlayerPrefs.GetInt("File3"));
        }
    }
}