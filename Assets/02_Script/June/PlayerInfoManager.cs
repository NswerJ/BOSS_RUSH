using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI playTimeTxt;
    [SerializeField]TextMeshProUGUI clearCntTxt;
    [SerializeField]TextMeshProUGUI killCntTxt;
    [SerializeField]TextMeshProUGUI deathTxt;

    private void Start()
    {
        clearCntTxt.text = 
            $"클리어 횟수 : {PlayerPrefs.GetInt("TotalClear",0)}회";
        killCntTxt.text = 
            $"보스 처치 횟수 : {PlayerPrefs.GetInt("TotalBossClear", 0)}회";
        deathTxt.text = 
            $"사망 횟수 : {PlayerPrefs.GetInt("TotalDeath", 0)}회";
    }

    private void Update()
    {
        string s = "";
        s += "플레이 시간 : ";
        int totalPlay = (int)PlayerPrefs.GetFloat("PlayTime", 0);
        if (totalPlay / 3600 > 0)
        {
            s += $"{totalPlay / 3600}시간 ";
            totalPlay %= 3600;
        }

        if (totalPlay / 60 > 0)
        {
            s += $"{totalPlay / 60}분 ";
            totalPlay %= 60;
        }

        s += $"{totalPlay}초 ";

        playTimeTxt.text = s;
    }
}
