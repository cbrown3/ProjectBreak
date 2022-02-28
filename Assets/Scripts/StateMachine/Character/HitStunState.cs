using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStunState : IState<CharController>
{
    public HitStunState()
    {
        stateType = StateType.HitStun;
    }

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
        else if(c.isPlayer1 && (CharManager.player2.stateMachine.GetCurrentState().stateType != StateType.SpecialAttack || 
            CharManager.player2.stateMachine.GetCurrentState().stateType != StateType.NormalAttack))
        {
            currentHitStunFrame = 0;
            CharManager.P2HitStunLength = 0;
            c.EnterState(c.idleState);
        }
        else if (!c.isPlayer1 && (CharManager.player1.stateMachine.GetCurrentState().stateType != StateType.SpecialAttack ||
            CharManager.player1.stateMachine.GetCurrentState().stateType != StateType.NormalAttack))
        {
            currentHitStunFrame = 0;
            CharManager.P1HitStunLength = 0;
            c.EnterState(c.idleState);
        }
    }

    public override void Exit(CharController c)
    {

    }
}
