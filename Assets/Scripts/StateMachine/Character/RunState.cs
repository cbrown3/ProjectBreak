using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState<CharController>
{
    public RunState()
    {
        
    }

    public override void Enter(CharController c)
    {
        c.ResetGlow();

        c.moveInput = Mathf.Round(c.playerInput.actions.FindAction("Move").ReadValue<float>());

        c.canDash = true;

        c.canAttack = true;

        c.interuptible = true;

        c.rigid.AddForce(new Vector2(c.groundSpeed * c.moveInput, 0), ForceMode2D.Impulse);

        c.animator.Play(c.aRunAnim);
    }

    public override void Continue(CharController c)
    {
        c.moveInput = Mathf.Round(c.playerInput.actions.FindAction("Move").ReadValue<float>());


        c.rigid.AddForce(new Vector2(c.groundSpeed * c.moveInput, 0), ForceMode2D.Impulse);

        if (c.rigid.velocity.x > c.groundSpeed)
        {
            c.rigid.velocity = new Vector2(c.groundSpeed, c.rigid.velocity.y);
        }
        else if (c.rigid.velocity.x < -c.groundSpeed)
        {
            c.rigid.velocity = new Vector2(-c.groundSpeed, c.rigid.velocity.y);
        }
        else if(c.moveInput == 0)
        {
            c.EnterState(c.idleState);
        }
    }

    public override void Exit(CharController c)
    {

    }
}
