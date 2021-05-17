using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDashState : IState<CharController>
{
    static readonly AirDashState instance =
        new AirDashState();

    public static AirDashState Instance
    {
        get
        {
            return instance;
        }
    }

    static AirDashState() { }

    private AirDashState() { }

    Vector2 airDashDir;

    int frameRate;


    public override void Enter(CharController c)
    {
        if (!c.isGrounded && c.canDash)
        {
            //c.dashFrames = 12;
            c.dashFrameLength = 0;

            c.rigid.velocity = Vector2.zero;

            c.canDash = false;
            c.canDJump = false;

            airDashDir = c.charControls.Character.AirDashDir.ReadValue<Vector2>();
        }
        else
        {
            c.RevertToPreviousState();
        }
    }

    public override void Continue(CharController c)
    {
        if(frameRate < c.dashStartup)
        {
            c.rigid.velocity = Vector2.zero;
        }
        else if(c.dashFrameLength > c.dashFrameLength - 1)
        {
            c.rigid.velocity = Vector2.zero;

            c.EnterState(FallState.Instance);
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
