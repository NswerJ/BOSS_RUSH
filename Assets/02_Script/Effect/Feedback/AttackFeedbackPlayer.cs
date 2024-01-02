using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFeedbackPlayer : MonoBehaviour
{

    private PlayerEventSystem playerEventSystem;
    private CinemachineImpulseSource source;

    private void Start()
    {

        playerEventSystem = FindObjectOfType<PlayerController>().playerEventSystem;
        playerEventSystem.AttackEvent += HandleAttack;
        source = GetComponent<CinemachineImpulseSource>();

    }

    private void HandleAttack(float value)
    {

        source.GenerateImpulse(0.1f);

    }

}
