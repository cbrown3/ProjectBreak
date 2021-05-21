using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<CharController>
{
    static readonly IdleState instance =
        new IdleState();

    public static IdleState Instance
    {
        get
        {
            return instance;
        }
    }

    static IdleState() { }

    private IdleState() { }
    
    public override void Enter(CharController c)
    {
        if(c.isGrounded)
        {
            c.rigid.velocity = new Vector2(0, c.rigid.velocity.y);

            c.canDash = false;

            c.canAttack = true;

            c.animator.Play(c.aIdleAnim);

        }
        else
        {
            c.RevertToPreviousState();
        }
    }

    public override void Continue(CharController c)
    {
        //if (Mathf.Round(c.rigid.velocity.x) != 0)
        if(c.moveInput != 0)
        {
            c.EnterState(RunState.Instance);
        }
        else if(c.jumpInput != 0)
        {
            c.EnterState(JumpState.Instance);
        }
    }

    public override void Exit(CharController c)
    {
        
    }
}
