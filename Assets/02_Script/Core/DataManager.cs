using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : DontDestroyOnLoad
{
    public static DataManager Instance;
    private int _dataIndex;
    public int DataIndex => _dataIndex;

    int bossCnt;

    [SerializeField] private GameObject _settingPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if (FindObjectOfType<Data>() != null)
            {
                _dataIndex = int.Parse(FindObjectOfType<Data>().name);

                if (PlayerPrefs.GetInt("File" + _dataIndex, -1) == -1)
                    PlayerPrefs.SetInt("File" + _dataIndex, 0);
            }
        }
        else
        {
            Destroy(gameObject);
        }

        bossCnt = FindObjectsOfType<StageDoor>().Length;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            _settingPanel.SetActive(true);
        }
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        _settingPanel.SetActive(false);
    }
    public void Main()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("IntroScene");
    }
    public void InitAndMain()
    {
        InitData();
        Main();
    }

    public void PlayerDie()
    {
        PlayerPrefs.SetInt("File" + _dataIndex, PlayerPrefs.GetInt("File" + _dataIndex) + 1);
    }

    private void InitData()
    {
        PlayerPrefs.SetInt("File" + _dataIndex, -1);
        PlayerPrefs.SetFloat("File" + _dataIndex + "XPos", -5);
        PlayerPrefs.SetFloat("File" + _dataIndex + "YPos", 0);
    }

}