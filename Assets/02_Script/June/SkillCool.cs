using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCool : MonoBehaviour
{
    [SerializeField] int _num;
    [SerializeField] GameObject _skillIcon;
    private void Update()
    {
        if (FindObjectOfType<Data>() != null)
        {
            _skillIcon.SetActive(PlayerPrefs.GetInt("File" +
            FindObjectOfType<Data>().name + "Boss" + _num) == 1);
        }
    }
}
