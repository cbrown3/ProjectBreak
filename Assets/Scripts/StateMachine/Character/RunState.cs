using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState<CharController>
{
    static readonly RunState instance =
        new RunState();

    public static RunState Instance
    {
        get
        {
            return instance;
        }
    }

    static RunState() { }

    private RunState() { }

    public override void Enter(CharController c)
    {
        if(c.isGrounded)
        {
            c.moveInput = c.charControls.Character.Move.ReadValue<float>();
            c.canDash = false;

            c.canAttack = true;

            c.rigid.AddForce(new Vector2(c.groundSpeed * c.moveInput, 0), ForceMode2D.Impulse);

            c.animator.Play(c.aRunAnim);

            if (c.moveInput > 0)
            {
                c.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if(c.moveInput < 0)
            {
                c.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else
        {
            c.RevertToPreviousState();
        }
    }

    public override void Continue(CharController c)
    {
        c.moveInput = c.charControls.Character.Move.ReadValue<float>();

        c.rigid.AddForce(new Vector2(c.groundSpeed * c.moveInput, 0), ForceMode2D.Impulse);

        if (c.rigid.velocity.x > c.groundSpeed)
        {
            c.rigid.velocity = new Vector2(c.groundSpeed, c.rigid.velocity.y);
        }
        else if (c.rigid.velocity.x < -c.groundSpeed)
        {
            c.rigid.velocity = new Vector2(-c.groundSpeed, c.rigid.velocity.y);
        }
        else
        {
            c.EnterState(IdleState.Instance);
        }
    }

    public override void Exit(CharController c)
    {

    }
}
