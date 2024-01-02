using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : DontDestroyOnLoad
{
    public static DataManager Instance;
    private int _dataIndex;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            if(FindObjectOfType<Data>() != null)
            {
                _dataIndex = int.Parse(FindObjectOfType<Data>().name);

                if(PlayerPrefs.GetInt("File" + _dataIndex,-1) == -1)
                    PlayerPrefs.SetInt("File" + _dataIndex, 0);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Main()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void InitAndMain()
    {
        Main();
    }
}