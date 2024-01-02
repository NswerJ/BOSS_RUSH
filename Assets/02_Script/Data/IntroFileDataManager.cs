using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroFileDataManager : MonoBehaviour
{
    GameObject[] slots;

    private void Awake()
    {
        slots = GetComponentsInChildren<GameObject>();
    }


}
