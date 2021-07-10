using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : IState<CharController>
{
    public FallState() { }

    public override void Enter(CharController c)
    {
        c.animator.Play(c.aFallAnim);

        c.canAttack = true;

        c.interuptible = true;
    }

    public override void Continue(CharController c)
    {
        if (Mathf.Round(c.rigid.velocity.y) == 0)
        {
            if (Mathf.Round(c.rigid.velocity.x) != 0)
            {
                c.EnterState(c.runState);
            }
            else
            {
                c.EnterState(c.idleState);
            }
        }

        c.moveInput = c.playerInput.actions.FindAction("Move").ReadValue<float>();

        c.rigid.AddForce(new Vector2(c.aerialDrift * c.moveInput, 0), ForceMode2D.Impulse);

        if (c.rigid.velocity.x > c.aerialDrift)
        {
            c.rigid.velocity = new Vector2(c.aerialDrift, c.rigid.velocity.y);
        }
        else if (c.rigid.velocity.x < -c.aerialDrift)
        {
            c.rigid.velocity = new Vector2(-c.aerialDrift, c.rigid.velocity.y);
        }
        else
        {
            c.rigid.velocity = new Vector2(c.aerialDrift * c.moveInput, c.rigid.velocity.y);
        }
    }

    public override void Exit(CharController c)
    {

    }
}
