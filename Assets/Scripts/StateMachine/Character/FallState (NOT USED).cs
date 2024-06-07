using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FightLogic;

public class FallState : IState<CharController>
{
    public FallState()
    {
        
    }

    public override void Enter(CharController c)
    {
        //c.animator.Play(c.aFallAnim);

        c.canAttack = true;

        c.interuptible = true;

        //c.rigid.gravityScale = 1;
    }

    public override void Continue(CharController c)
    {
        /*
        if (Mathf.Round(c.rigid.velocity.y) == 0)
        {
            if (Mathf.Round(c.rigid.velocity.x) != 0)
            {
                c.EnterState(c.runState);

                return;
            }
            else
            {
                c.EnterState(c.idleState);

                return;
            }
        }

        c.moveInput = c.playerInput.actions.FindAction("Move").ReadValue<float>();
        /*
        c.rigid.AddForce(new Vector2(c.aerialDrift * c.moveInput, 0), ForceMode2D.Impulse);

        if (c.rigid.velocity.x > c.maxAerialSpeed)
        {
            c.rigid.velocity = new Vector2(c.maxAerialSpeed, c.rigid.velocity.y);
        }
        else if (c.rigid.velocity.x < -c.maxAerialSpeed)
        {
            c.rigid.velocity = new Vector2(-c.maxAerialSpeed, c.rigid.velocity.y);
        }*/
        /*else
        {
            c.rigid.velocity = new Vector2(c.aerialDrift * c.moveInput, c.rigid.velocity.y);
        }*/

        //Mathf.Clamp(c.rigid.velocity.x, -c.aerialDrift, c.aerialDrift);
    }

    public override void Exit(CharController c)
    {

    }
}
