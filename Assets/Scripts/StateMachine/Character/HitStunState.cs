using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStunState : IState<CharController>
{
    public HitStunState() { }

    int currentHitStunFrame;

    public override void Enter(CharController c)
    {
        c.animator.Play(c.aHitStunAnim);

        c.canAttack = false;

        c.interuptible = false;

        c.canDash = false;

        if (c.isPlayer1)
        {
            currentHitStunFrame = CharManager.P2HitStunLength;
        }
        else
        {
            currentHitStunFrame = CharManager.P1HitStunLength;
        }
    }

    public override void Continue(CharController c)
    {
        currentHitStunFrame--;

        if (currentHitStunFrame == 0)
        {
            if (c.isGrounded)
            {
                c.EnterState(c.idleState);
            }
            else
            {
                c.EnterState(c.fallState);
            }
        }
    }

    public override void Exit(CharController c)
    {

    }
}
