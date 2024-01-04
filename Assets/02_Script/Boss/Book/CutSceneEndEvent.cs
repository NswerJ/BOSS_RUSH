using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneEndEvent : MonoBehaviour
{
    [SerializeField]
    private PlayerController _controller;

    private void Awake()
    {
        if (_controller == null)
            GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnDisable()
    {
        _controller.ChangeState(EnumPlayerState.Move);
    }
}
