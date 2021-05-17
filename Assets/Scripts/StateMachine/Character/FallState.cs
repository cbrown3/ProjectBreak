using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : IState<CharController>
{
    static readonly FallState instance =
        new FallState();

    public static FallState Instance
    {
        get
        {
            return instance;
        }
    }

    static FallState() { }

    private FallState() { }

    public override void Enter(CharController c)
    {
        
    }

    public override void Continue(CharController c)
    {
        if (Mathf.Round(c.rigid.velocity.y) == 0)
        {
            if (Mathf.Round(c.rigid.velocity.x) != 0)
            {
                c.EnterState(RunState.Instance);
            }
            else
            {
                c.EnterState(IdleState.Instance);
            }
        }

        c.moveInput = c.charControls.Character.Move.ReadValue<float>();

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
