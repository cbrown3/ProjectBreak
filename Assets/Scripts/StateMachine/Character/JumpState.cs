using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : IState<CharController>
{
    public JumpState() { }

    int frameRate;

    float storedVelX;

    public override void Enter(CharController c)
    {
        if (c.isGrounded) //|| c.canDJump)
        {
            c.canDash = true ? c.isGrounded : !c.isGrounded;
            c.isGrounded = false;

            c.canAttack = true;
            //c.canDash = c.canDJump;

            c.interuptible = true;

            frameRate = 0;

            c.animator.Play(c.aJumpAnim);

            storedVelX = c.rigid.velocity.x;
        }
        else
        {
            //c.RevertToPreviousState();
        }
    }

    public override void Continue(CharController c)
    {
        if (frameRate < 2)
        {
            c.rigid.velocity = Vector2.zero;
        }
        else if(frameRate == c.jumpSquatFrames)
        {
            c.rigid.velocity = new Vector2(storedVelX, c.jumpHeight);
        }
        else
        {
            if (Mathf.Round(c.rigid.velocity.y) < 0)
            {
                c.EnterState(c.fallState);
                return;
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

        frameRate++;
    }

    public override void Exit(CharController c)
    {

    }
}
