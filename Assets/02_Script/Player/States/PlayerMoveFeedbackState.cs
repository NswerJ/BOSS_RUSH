using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveFeedbackState : PlayerState
{

    private ParticleSystem walkParticle;
    private Transform jumpParticlePivot;


    public PlayerMoveFeedbackState(PlayerController controller) : base(controller)
    {

        jumpParticlePivot = transform.Find("JumpParticlePivot");
        walkParticle = transform.Find("PlayerWalkParicle").GetComponent<ParticleSystem>();

    }

    protected override void EnterState()
    {

        groundSencer.OnTriggerd += HandleTriggerd;

    }

    protected override void UpdateState()
    {

        if(rigid.velocity.x != 0 && isGround)
        {

            if(walkParticle.isPlaying == false)
            {

                walkParticle.Play();

            }

        }
        else
        {

            if (walkParticle.isPlaying == true)
            {

                walkParticle.Stop();

            }

        }

    }

    private void HandleTriggerd(bool obj)
    {

        FAED.TakePool("Player_JumpParticle", jumpParticlePivot.position);

    }

    protected override void ExitState()
    {

        groundSencer.OnTriggerd -= HandleTriggerd;

    }

}
