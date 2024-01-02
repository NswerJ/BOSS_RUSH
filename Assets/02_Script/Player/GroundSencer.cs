using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSencer : MonoBehaviour
{

    [SerializeField] private List<string> jumpAbleTag;
    public event Action<bool> OnTriggerd;
    public bool isGround;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        foreach (var tag in jumpAbleTag)
        {

            if (collision.CompareTag(tag))
            {

                isGround = true;
                OnTriggerd?.Invoke(true);

            }

        }



    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        foreach (var tag in jumpAbleTag)
        {

            if (collision.CompareTag(tag))
            {

                isGround = true;

            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        foreach (var tag in jumpAbleTag)
        {

            if (collision.CompareTag(tag))
            {

                isGround = false;
                OnTriggerd?.Invoke(false);

            }

        }

    }

}
