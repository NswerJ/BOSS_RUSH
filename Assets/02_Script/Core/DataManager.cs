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
    public int BossCount => bossCnt;

    Dictionary<string, int> clearDic = new Dictionary<string, int>();

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

    private void Start()
    {
        for (int i = 0; i < BossCount; i++)
        {
            string s = "Boss" + i + 1;
            string s1 = "File" + DataIndex;
            clearDic.Add(s, PlayerPrefs.GetInt(s1 + s, 0));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            PlayerPosSave.Instance.SavePos();
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
        for(int i = 0; i < bossCnt; i++)
            PlayerPrefs.SetInt("File" + _dataIndex + "Boss" + (i + 1), 0);
    }

    bool GetClear(string key)
    {
        if (!clearDic.ContainsKey(key))
            Debug.LogError("Can not find Key");
        return clearDic[key] == 1;
    }

    bool GetClear(int key)
    {
        string keyStr = "Boss" + key;
        if (!clearDic.ContainsKey(keyStr))
            Debug.LogError("Can not find Key");
        return clearDic[keyStr] == 1;
    }

    void ClearMap(int key)
    {
        string keyStr = "Boss" + key;
        clearDic[keyStr] = 1;
        PlayerPrefs.SetInt("File" + DataIndex + "Boss" + key, 1);
    }
}