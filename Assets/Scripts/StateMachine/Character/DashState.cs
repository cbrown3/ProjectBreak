using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashState : IState<CharController>
{
    public DashState()
    {
        stateType = StateType.Dash;
    }

    InputAction dashAction;

    Vector2 dashDir;

    int frameCount;

    public override void Enter(CharController c)
    {
        if(c.playerInput.actions.FindAction("Dash").phase == InputActionPhase.Waiting)
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

        c.animator.Play(c.aDashAnim);

        dashDir = c.playerInput.actions.FindAction("DirectionalInput").ReadValue<Vector2>();
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

            if (c.isGrounded)
            {
                c.EnterState(c.idleState);
            }
            else
            {
                c.EnterState(c.fallState);
            }
        }
        else
        {
            if(dashDir.x == 0)
            {
                if (c.GetComponent<SpriteRenderer>().flipX)
                {
                    c.rigid.velocity = new Vector2(-c.dashSpeed, c.dashSpeed * dashDir.y);
                }
                else
                {
                    c.rigid.velocity = new Vector2(c.dashSpeed, c.dashSpeed * dashDir.y);
                }
            }
            else
            {
                c.rigid.velocity = new Vector2(c.dashSpeed * dashDir.x, c.dashSpeed * dashDir.y);
            }
        }

        frameCount++;
    }

    public override void Exit(CharController c)
    {

    }
}
