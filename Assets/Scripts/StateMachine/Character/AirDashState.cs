using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AirDashState : IState<CharController>
{
    public AirDashState() { }

    Vector2 airDashDir;

    int frameRate;


    public override void Enter(CharController c)
    {
        //c.dashFrames = 12;
        frameRate = 0;

        c.rigid.velocity = Vector2.zero;

        c.canDash = false;

        c.canAttack = false;
        //c.canDJump = false;

        c.interuptible = true;

        c.animator.Play(c.aAirDashAnim);

        airDashDir = c.playerInput.actions.FindAction("DirectionalInput").ReadValue<Vector2>();
    }

    public override void Continue(CharController c)
    {
        if(frameRate < c.dashStartup)
        {
            c.rigid.velocity = Vector2.zero;
        }
        else if(frameRate > c.dashFrameLength - 1)
        {
            c.rigid.velocity = Vector2.zero;

            c.EnterState(c.fallState);
        }
        else
        {
            c.rigid.velocity = new Vector2(c.dashSpeed * airDashDir.x, c.dashSpeed * airDashDir.y);
        }

        frameRate++;
    }

    public override void Exit(CharController c)
    {

    }
}
