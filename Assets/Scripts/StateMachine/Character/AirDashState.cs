using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AirDashState : IState<CharController>
{
    public AirDashState()
    {
        stateType = StateType.AirDash;
    }

    InputAction airDashAction;

    Vector2 airDashDir;

    int frameCount;

    public override void Enter(CharController c)
    {
        if(c.playerInput.actions.FindAction("AirDash").phase == InputActionPhase.Waiting)
        {
            return;
        }

        //c.dashFrames = 12;
        frameCount = 0;

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
        if(frameCount < c.dashStartup)
        {
            c.rigid.velocity = Vector2.zero;
        }
        else if(frameCount > (c.dashFrameLength + c.dashStartup - 1))
        {
            c.rigid.velocity = Vector2.zero;

            c.EnterState(c.fallState);
        }
        else
        {
            if(airDashDir.x == 0)
            {
                if (c.GetComponent<SpriteRenderer>().flipX)
                {
                    c.rigid.velocity = new Vector2(-c.dashSpeed, c.dashSpeed * airDashDir.y);
                }
                else
                {
                    c.rigid.velocity = new Vector2(c.dashSpeed, c.dashSpeed * airDashDir.y);
                }
            }
            else
            {
                c.rigid.velocity = new Vector2(c.dashSpeed * airDashDir.x, c.dashSpeed * airDashDir.y);
            }
        }

        frameCount++;
    }

    public override void Exit(CharController c)
    {

    }
}
