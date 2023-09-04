using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class JumpState : IState<CharController>
{
public JumpState()
{
stateType = StateType.Jump;
}

int frameCount;

float storedVelX;

public override void Enter(CharController c)
{
if(c.playerInput.actions.FindAction("Jump").phase == UnityEngine.InputSystem.InputActionPhase.Waiting)
{
    return;
}

c.canDash = true;

c.rigid.gravityScale = 1;

if (c.isGrounded) //|| c.canDJump)
{
    c.isGrounded = false;

    c.canAttack = false;
    //c.canDash = c.canDJump;

    c.interuptible = true;

    frameCount = 0;

    c.animator.Play(c.aJumpAnim);

    storedVelX = c.rigid.velocity.x;

    c.maxAerialSpeed = c.groundSpeed + c.aerialDrift;
}
else
{
    //c.RevertToPreviousState();
}
}

public override void Continue(CharController c)
{
if(frameCount == c.jumpSquatFrames)
{
    c.rigid.velocity = new Vector2(storedVelX, c.jumpVelocity);
    c.canAttack = true;
}
else
{
    c.moveInput = c.playerInput.actions.FindAction("Move").ReadValue<float>();

    if (Mathf.Round(c.rigid.velocity.y) < 0)
    {
        c.rigid.velocity += Vector2.up * Physics2D.gravity.y * (c.fallGravityMultiplier - 1) * Time.deltaTime;
        c.EnterState(c.fallState);
        return;
    }
    else if(Mathf.Round(c.rigid.velocity.y) > 0 &&
        !c.playerInput.actions.FindAction("Jump").IsPressed())
    {
        c.rigid.velocity += Vector2.up * Physics2D.gravity.y * (c.lowJumpMultiplier - 1) * Time.deltaTime;
    }

    c.rigid.AddForce(new Vector2(c.maxAerialSpeed * c.moveInput, 0), ForceMode2D.Impulse);

    /*
    if (c.rigid.velocity.x > c.maxAerialSpeed)
    {
        c.rigid.velocity = new Vector2(c.maxAerialSpeed, c.rigid.velocity.y);
    }
    else if (c.rigid.velocity.x < -c.maxAerialSpeed)
    {
        c.rigid.velocity = new Vector2(-c.maxAerialSpeed, c.rigid.velocity.y);
    }
    */

//c.rigid.velocity = new Vector2(Mathf.Clamp(c.rigid.velocity.x, -c.maxAerialSpeed, c.maxAerialSpeed), c.rigid.velocity.y);
            /*else
            {
                c.rigid.velocity = new Vector2(c.aerialDrift * c.moveInput, c.rigid.velocity.y);
            }*/

            //Mathf.Clamp(c.rigid.velocity.x, -c.aerialDrift, c.aerialDrift);
/*
            c.transform.position = new Vector3(c.transform.position.x, Mathf.Clamp(c.transform.position.y, 0, c.jumpHeight), c.transform.position.z);

            if (c.transform.position.y >= c.jumpHeight)
            {
                c.rigid.velocity = new Vector2(c.rigid.velocity.x, -0.1f);
                c.rigid.velocity += Vector2.up * Physics2D.gravity.y * (c.fallGravityMultiplier - 1) * Time.deltaTime;
                c.EnterState(c.fallState);
                return;
            }

        }

        frameCount++;
    }

    public override void Exit(CharController c)
    {

    }
}
*/