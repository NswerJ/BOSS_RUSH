using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineAnimation : MonoBehaviour
{
    PlayableDirector pd;
    bool isEnd = false;

    private void Awake()
    {
        pd = GetComponent<PlayableDirector>();
    }

    void Start()
    {
        if (FindObjectOfType<Data>() != null)
        {
            isEnd = true;
            for(int i = 0; i < 3; i++)
            {
                if(PlayerPrefs.GetInt("File" + FindObjectOfType<Data>().name + "Boss" + (i + 1), 0) == 0)
                {
                    isEnd = false;
                }
            }


            if(isEnd)
            {
                if(PlayerPrefs.GetInt("File" + FindObjectOfType<Data>().name + "Anim", 0) == 0)
                {
                    pd.Play();
                    PlayerPrefs.SetInt("File" + FindObjectOfType<Data>().name + "Anim", 1);
                }
            }

        }
    }
}
