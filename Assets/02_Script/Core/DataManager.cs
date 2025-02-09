using System;
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
    public List<WeaponDataSO> weapons;

    private int weaponIndex;

    [SerializeField] private GameObject _settingPanel;

    protected override void Awake()
    {
        base.Awake();
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
        if (FindObjectOfType<Data>() != null)
        {
            _dataIndex = int.Parse(FindObjectOfType<Data>().name);

            if (PlayerPrefs.GetInt("File" + _dataIndex, -1) == -1)
                PlayerPrefs.SetInt("File" + _dataIndex, 0);
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
        weaponIndex = PlayerPrefs.GetInt("File" + _dataIndex + "Weapon", 0);
        if(GameObject.Find("Player") != null)
        GameObject.Find("Player").GetComponentInChildren<WeaponController>()
            .Data = weapons[weaponIndex];
        SceneManager.sceneLoaded += ChangeWeapon;
    }

    private void ChangeWeapon(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "IntroScene")
            Destroy(this);

        if (GameObject.Find("Player") != null)
            GameObject.Find("Player").GetComponentInChildren<WeaponController>()
                .Data = weapons[weaponIndex];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_settingPanel == null)
            {
                _settingPanel = GameObject.Find("Option");
            }

            if (_settingPanel != null)
            {
                Time.timeScale = 0f;
                PlayerPosSave.Instance.SavePos();
                _settingPanel.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
            }
        }
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        _settingPanel.GetComponent<RectTransform>().localScale = new Vector2(0, 0);
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

    public void InitData()
    {
        PlayerPrefs.SetInt("File" + _dataIndex, -1);
        PlayerPrefs.SetFloat("File" + _dataIndex + "XPos", -5);
        PlayerPrefs.SetFloat("File" + _dataIndex + "YPos", 0);
        for (int i = 0; i < bossCnt; i++)
            PlayerPrefs.SetInt("File" + _dataIndex + "Boss" + (i + 1), 0);
        PlayerPrefs.SetInt("File" + _dataIndex + "Weapon", 0);
        PlayerPrefs.SetInt("File" + _dataIndex + "Anim", 0);
    }

    public bool GetClear(string key)
    {
        if (!clearDic.ContainsKey(key))
            Debug.LogError("Can not find Key");
        return clearDic[key] == 1;
    }

    public bool GetClear(int key)
    {
        string keyStr = "Boss" + key;
        if (!clearDic.ContainsKey(keyStr))
            Debug.LogError("Can not find Key");
        return clearDic[keyStr] == 1;
    }

    public void ClearMap(int key)
    {
        string keyStr = "Boss" + key;
        clearDic[keyStr] = 1;
        PlayerPrefs.SetInt("File" + DataIndex + "Boss" + key, 1);
    }

    public void SaveWeapon(WeaponDataSO key)
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i] == key)
                weaponIndex = i;
        }
        PlayerPrefs.SetInt("File" + _dataIndex + "Weapon", weaponIndex);
    }
}