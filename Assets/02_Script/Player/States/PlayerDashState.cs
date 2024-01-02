using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{

    private ParticleSystem dashParticle;
    private AddGravity addGravity;
    private Coroutine coroutine;

    public PlayerDashState(PlayerController controller) : base(controller)
    {

        addGravity = GetComponent<AddGravity>();

        dashParticle = transform.Find("DashParticle").GetComponent<ParticleSystem>();

    }

    protected override void EnterState()
    {

        addGravity.enabled = false;
        rigid.gravityScale = 0;

        rigid.velocity = playerInputController.LastInputVector * 50;

        dashParticle.transform.localScale = spriteRenderer.flipX ? new Vector2(-1, 1) : new Vector2(1, 1);
        dashParticle.Play();

        coroutine = StartCoroutine(DashEndCo());

    }

    protected override void ExitState()
    {

        rigid.gravityScale = 1;
        addGravity.enabled = true;
        StopCoroutine(coroutine);
        dashParticle.Stop();
        coroutine = null;

    }

    public override void CollisonEnter()
    {

        controller.ChangeState(EnumPlayerState.Move);

    }

    private IEnumerator DashEndCo()
    {

        yield return new WaitForSeconds(0.1f);
        controller.ChangeState(EnumPlayerState.Move);
        yield return null;

    }

}
