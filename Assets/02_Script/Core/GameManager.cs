using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector]
    public GameObject player;

    private void Awake()
    {
        #region 싱글톤
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogError($"{transform} : GameManager is Multiply running!");
            Destroy(this);
        }
        #endregion
        player = GameObject.Find("Player");
    }
}
