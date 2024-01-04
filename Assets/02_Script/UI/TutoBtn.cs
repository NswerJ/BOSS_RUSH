using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoBtn : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt("Tuto") == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
