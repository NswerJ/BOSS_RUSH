using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{

    private ParticleSystem dashParticle;
    private AddGravity addGravity;
    private Coroutine coroutine;
    private PlayerAnimator animator;

    public PlayerDashState(PlayerController controller) : base(controller)
    {

        addGravity = GetComponent<AddGravity>();
        animator = GetComponent<PlayerAnimator>();
        dashParticle = transform.Find("DashParticle").GetComponent<ParticleSystem>();

    }

    protected override void EnterState()
    {

        playerEventSystem.DashEventExecute();
        addGravity.enabled = false;
        rigid.gravityScale = 0;
        animator.Dash();

        var mpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var dir = mpos - transform.position;
        dir.z = 0;

        rigid.velocity = dir.normalized * 40;

        dashParticle.transform.localScale = spriteRenderer.flipX ? new Vector2(-1, 1) : new Vector2(1, 1);
        dashParticle.Play();

        coroutine = StartCoroutine(DashEndCo());

    }

    protected override void ExitState()
    {

        rigid.gravityScale = 1;
        addGravity.enabled = true;
        StopCoroutine(coroutine);
        animator.DashEnd();
        dashParticle.Stop();
        rigid.velocity = Vector2.zero;
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
